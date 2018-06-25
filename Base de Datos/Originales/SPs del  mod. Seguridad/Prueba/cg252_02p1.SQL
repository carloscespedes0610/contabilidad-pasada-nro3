/*****************************************************************/
/*  Aplicacion    : cg252 (Balance y Cierre)                     */
/*  Procedimiento : cg252_02p1  Acumulacion de cuentas           */
/*****************************************************************/

/* Verifica si el procedimiento se encuentra creado */
if exists (select * from sysobjects where id = object_id('dbo.cg252_02p1') and sysstat & 0xf = 4)
	drop procedure dbo.cg252_02p1
GO

/* ACUMULACION DE CUENTAS (BALANCE Y CIERRE) */
CREATE PROCEDURE cg252_02p1 @ag_per_act INT, @ag_ges_act INT 
                            WITH ENCRYPTION AS

  DECLARE  @va_fec_ini   DATETIME,      --** Fecha inicial (Periodo actual)
           @va_fec_fin   DATETIME,      --** Fecha final (Periodo actual)
           @va_mdb_bol   DECIMAL(14,2), --** Monto Debe en Bs.
           @va_mdb_dol   DECIMAL(14,2), --** Monto Debe en Us.
           @va_mcr_bol   DECIMAL(14,2), --** Monto Haber en Bs.
           @va_mcr_dol   DECIMAL(14,2), --** Monto Haber en Us.

           @va_cod_cta   CHAR(06),      --** Codigo de la cuenta
           @va_cla_cta   CHAR(01),      --** Clase cuenta (M=Matriz;
                                        --** B=Anal.Bs.;U=Anal.Us.;S=Sin ajuste)                
           @va_ide_cla   CHAR(04),      --** ID. C.C.
           @va_cla_see   CHAR(01),      --** Clase del C.C. (M=Matriz,A=Analitica)
           @va_ide_reg   INT,           --** ID. Regional
           @va_mto_bol   DECIMAL(16,2), --** Monto en Bs.
           @va_mto_dol   DECIMAL(12,2), --** Monto en Us.

           @va_tdb_bol   DECIMAL(14,2), --** Total Debe en Bs.
           @va_tdb_dol   DECIMAL(14,2), --** Total Debe en Us.
           @va_tcr_bol   DECIMAL(14,2), --** Total Haber en Bs.
           @va_tcr_dol   DECIMAL(14,2), --** Total Haber en Us.

           @va_nro_reg   INT            --** Nro. Registro

/* Inhabilita mensajes numero de filas afectadas */
SET NOCOUNT ON

/* CREA TABLA TEMPORAL */
CREATE TABLE #tm_det_asi (
   va_cod_cta   CHAR(06),      --** Cod. Cuenta
   va_ide_reg   INT,           --** Regional/Sucursal
   va_ide_cla   CHAR(04),      --** ID. C.C.
   va_mdb_bol   DECIMAL(16,2), --** Monto Debe en Bs.
   va_mdb_dol   DECIMAL(12,2), --** Monto Debe en Us.
   va_mcr_bol   DECIMAL(16,2), --** Monto Haber en Bs.
   va_mcr_dol   DECIMAL(12,2)  --** Monto Haber en Us.
)


/* OBTIENE LA FECHA INICIAL Y FINAL DEL PERIODO ACTUAL */
SELECT @va_fec_ini = va_fec_ini, @va_fec_fin = va_fec_fin
  FROM as002
 WHERE (va_ges_per = @ag_ges_act) AND
       (va_per_per = @ag_per_act)


/***********************************************************/
/* OBTIENE REGISTROS DE LAS CUENTAS DEL DETALLE DE ASIENTO */
/* SEPARANDO LOS MONTOS BS. Y US. EN MONTO DEBE Y HABER EN */
/* BS. Y US.. INSERTA EN LA TABLA TEMPORAL                 */
/***********************************************************/

DECLARE vc_det_asi CURSOR LOCAL FOR
 SELECT va_cod_cta, va_ide_reg, va_ide_cla,
        va_mto_bol, va_mto_dol
   FROM cg006
  WHERE (va_fec_asi BETWEEN @va_fec_ini AND @va_fec_fin)
    AND (va_sta_tus = 'V')

OPEN vc_det_asi

  FETCH NEXT FROM vc_det_asi INTO @va_cod_cta, @va_ide_reg, @va_ide_cla,
                                  @va_mto_bol, @va_mto_dol

  WHILE (@@FETCH_STATUS = 0)
    BEGIN

      /* Obtiene Monto DEBE y HABER en Bs. */
      IF @va_mto_bol > 0
        BEGIN
          SET @va_mdb_bol = @va_mto_bol
          SET @va_mcr_bol = 0
        END
      ELSE
        BEGIN
          SET @va_mdb_bol = 0
          SET @va_mcr_bol = @va_mto_bol
        END

      /* Obtiene Monto DEBE y HABER en Us. */
      IF @va_mto_dol > 0
        BEGIN
          SET @va_mdb_dol = @va_mto_dol
          SET @va_mcr_dol = 0
        END
      ELSE
        BEGIN
          SET @va_mdb_dol = 0
          SET @va_mcr_dol = @va_mto_dol
        END

      --** Valida que la cuenta exista y sea analitica
      SELECT @va_cla_cta = va_cla_cta
        FROM cg004
       WHERE (va_cod_cta = @va_cod_cta)

      IF @@ROWCOUNT = 0
        BEGIN
          RAISERROR('Existen cuentas NO definidas en el plan de cuentas.', 16, 1)
          RETURN (1)
        END

      IF @va_cla_cta = 'M'
        BEGIN
          RAISERROR('Las cuentas de los Asientos deben ser analiticas.', 16, 1)
          RETURN (1)
        END

      --** Valida que el C.C. exista y sea analitica
      IF @va_ide_cla <> ''
        BEGIN
          SELECT @va_cla_see = va_cla_see
            FROM cg003
           WHERE (va_ide_cla = @va_ide_cla)

          IF @@ROWCOUNT = 0
            BEGIN
              RAISERROR('Asientos con C.C. NO definidos.', 16, 1)
              RETURN (1)
            END

          IF @va_cla_see = 'M'
            BEGIN
              RAISERROR('La clase del C.C. de los Asientos deben ser analitica.', 16, 1)
              RETURN (1)
            END
        END

      INSERT INTO #tm_det_asi VALUES (@va_cod_cta, @va_ide_reg, @va_ide_cla,
                                      @va_mdb_bol, @va_mdb_dol, @va_mcr_bol,
                                      @va_mcr_dol)

      FETCH NEXT FROM vc_det_asi INTO @va_cod_cta, @va_ide_reg, @va_ide_cla,
                                      @va_mto_bol, @va_mto_dol

    
    END   

/* CIERRA Y ELIMINA CURSOR */
CLOSE vc_det_asi
DEALLOCATE vc_det_asi


/**********************************************************/
/* PROCESA REGISTROS DE LA TABLA TEMPORAL, REALIZANDO LA  */
/* ACUMULACION POR CUENTA y C.C..                         */
/* INSERTA/ACTUALIZA LA TABLA DE RESUMEN DE MOVIMIENTO    */
/* POR CUENTA y CLASIFICACION.                            */
/**********************************************************/

/* CURSOR SOBRE LA TABLA TEMPORAL */
DECLARE vc_tmp_det CURSOR LOCAL FOR
 SELECT va_cod_cta,      va_ide_reg,      va_ide_cla,    
        SUM(va_mdb_bol), SUM(va_mdb_dol), SUM(va_mcr_bol), SUM(va_mcr_dol)
   FROM #tm_det_asi
  GROUP BY va_cod_cta, va_ide_reg, va_ide_cla
 

OPEN vc_tmp_det

  FETCH NEXT FROM vc_tmp_det INTO @va_cod_cta, @va_ide_reg, @va_ide_cla,
                                  @va_tdb_bol, @va_tdb_dol, @va_tcr_bol,
                                  @va_tcr_dol
  WHILE (@@FETCH_STATUS = 0)
    BEGIN

      SELECT @va_nro_reg = COUNT(*)
        FROM cg023
       WHERE (va_cod_cta = @va_cod_cta) AND
             (va_ide_reg = @va_ide_reg) AND
             (va_ide_cla = @va_ide_cla)

      --** Si NO EXISTE registro en tabla resumen p/cuenta y C.C.
      IF @va_nro_reg = 0
        BEGIN
          --** Obtiene la clase de la cuenta
          SELECT @va_cla_cta = va_cla_cta
            FROM cg004
           WHERE (va_cod_cta = @va_cod_cta)

          --** Inserta registro en la tabla de resumen por cuenta y C.C.
          INSERT INTO cg023 VALUES (@ag_ges_act, @ag_per_act, @va_cod_cta,
                                    @va_ide_reg, @va_ide_cla, @va_cla_cta,
                                    0, 0,   0, 0, 0, 0,   0, 0, 0, 0)
          IF @@ERROR <> 0
             RETURN (1)
        END

      --** Actualiza registro en la tabla de resumen p/ cuenta y C.C.
      UPDATE cg023 SET va_mdb_bol=@va_tdb_bol, va_mdb_dol=@va_tdb_dol,
                       va_mcr_bol=@va_tcr_bol, va_mcr_dol=@va_tcr_dol
                 WHERE (va_cod_cta = @va_cod_cta) AND
                       (va_ide_reg = @va_ide_reg) AND
                       (va_ide_cla = @va_ide_cla) 

      IF @@ERROR <> 0
         RETURN (1)

      FETCH NEXT FROM vc_tmp_det INTO @va_cod_cta, @va_ide_reg, @va_ide_cla,
                                      @va_tdb_bol, @va_tdb_dol, @va_tcr_bol,
                                      @va_tcr_dol
    END


CLOSE vc_tmp_det
DEALLOCATE vc_tmp_det

RETURN (0)