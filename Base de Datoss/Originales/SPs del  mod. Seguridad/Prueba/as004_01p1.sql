/******************************************************************/
/*  PROCEDIMIENTO : as004_01p1  (Autorizaciones usuario como)     */
/*                  Copia aplicaciones autorizadas, defaults,     */
/*   autorizaciones, autorizaciones por serie y autorizaciones    */
/*   de líneas de productos del usuario origen al usuario destino */
/*  ARGUMENTOS    : @ag_usr_ori  CHAR(15)  // Usuario a copiar    */
/*                  @ag_usr_des  CHAR(15)  // Usuario destino     */
/*  AUTOR         : GENERACION 2000 - FCS (09/05/2002)            */
/******************************************************************/

/* Verifica si el procedimiento se encuentra creado */
if exists (select * from sysobjects where id = object_id('dbo.as004_01p1') and sysstat & 0xf = 4)
	drop procedure dbo.as004_01p1
GO

CREATE PROCEDURE as004_01p1 @ag_usr_ori CHAR(15),
                            @ag_usr_des CHAR(15) WITH ENCRYPTION AS

  DECLARE   @va_ide_tab  CHAR(08),  --** Nombre de la tabla
            @va_ite_aut  INT,       --** ID. del item autorizado
            @va_ide_doc  CHAR(03),  --** ID. del documento
            @va_nro_ser  INT,       --** Nro. de serie
            @va_ide_mod  INT,       --** ID. de modulo
            @va_aut_nue  CHAR(01),  --** Autorizacion nuevo (S=Si, N=No)
            @va_aut_mod  CHAR(01),  --** Autorizacion modifica (S=Si, N=No)
            @va_aut_anu  CHAR(01),  --** Autorizacion anula (S=Si, N=No)
            @va_aut_eli  CHAR(01),  --** Autorizacion elimina (S=Si, N=No)
            @va_aut_con  CHAR(01),  --** Autorizacion consulta (S=Si, N=No)
            @va_aut_imp  CHAR(01),  --** Autorizacion re-imprime (S=Si, N=No)
            @va_obs_aut  CHAR(60),  --** Observaciones de autorizacion 

            @va_nom_apl  CHAR(25),  --** Nombre aplicacion ejecutable
            @va_eti_men  CHAR(120),  --** Etiqueta del menu          
            @va_des_apl  CHAR(120),  --** Descripcion aplicacion
 
            @va_rub_inv  INT,       --** Rubro de inventario
            @va_lin_ini  CHAR(08),  --** Linea inicial
            @va_lin_fin  CHAR(08),  --** Linea final
            @va_cto_aut  INT,       --** Costo autorizado

            @va_ide_def  INT,       --** ID. Default
            @va_des_def  CHAR(40),  --** Descripcion del default
            @va_tip_def  CHAR(01),  --** Tipo default
                                    --** (E=Entero; C=Caracter;
                                    --**  D=Decimal)
            @va_def_car  CHAR(60),  --** Defaut caracter
            @va_def_ent  INT,       --** Default entero
            @va_def_dec  DECIMAL(12,2), --** Default decimal

            @va_ide_cco   CHAR(04), --** ID. Centro de Costo ->
            @va_ide_cla  INT,       --** ID. Clave
            @va_cla_ave  CHAR(15),  --** Contraseña
            @va_nro_reg  INT,       --** Numero de registros

            @va_nom_dwi  CHAR(15),  --** Nombre del datawindow
            @va_col_des  VARCHAR(2000), --** Columnas del datawindow
	        @va_cti_aut  CHAR(06),  --** Cuenta Contable Inicial Autorizada
            @va_ctf_aut  CHAR(06),  --** Cuenta Contable Final Autorizada

            @va_fec_aut  DATETIME,  --** Fecha y Hora de la autorizacion
            @va_con_aut  INT        --** Autorizacion (0=No; 1=Consulta; 2=Registro; 3=Actualiza)


/* Inhabilita mensajes numero de filas afectadas */
SET NOCOUNT ON

------------------------------------------------------------------
-- COPIA RANGO DE CUENTAS DEL USUARIO ORIGEN AL USUARIO DESTINO --
------------------------------------------------------------------
SELECT @va_cti_aut = va_cti_aut, @va_ctf_aut = va_ctf_aut
  FROM as004
 WHERE va_ide_usr = @ag_usr_ori

IF @@ROWCOUNT = 0
  BEGIN
    SET @va_cti_aut = '000000'
    SET @va_ctf_aut = '999999'
  END

--** Actualiza rango de cuentas al usuario destino
UPDATE as004 SET va_cti_aut = @va_cti_aut, va_ctf_aut = @va_ctf_aut
           WHERE va_ide_usr = @ag_usr_des

IF @@ERROR <> 0
   RETURN

----------------------------------------------------------
--  COPIA AUTORIZACIONES POR CENTRO DE COSTO A USUARIOS --
----------------------------------------------------------
--** ELIMINA AUTORIZACIONES P/CENTRO DE COSTO DEL USUARIO DESTINO
DELETE FROM as005 WHERE (va_ide_usr = @ag_usr_des)

IF @@ERROR <> 0
   RETURN

--** CURSOR SOBRE LAS AUTORIZACIONES P/CENTRO DE COSTO DEL USUARIO ORIGEN
DECLARE vc_aut_cco CURSOR LOCAL FOR
 SELECT va_ide_cla
   FROM as005
  WHERE (va_ide_usr = @ag_usr_ori)
		
OPEN vc_aut_cco     --** Abre cursor 
	
--** Recupera primer registro
FETCH NEXT FROM vc_aut_cco INTO @va_ide_cco
	
WHILE (@@FETCH_STATUS = 0) 
BEGIN
   --** Inserta registro en tabla "Centros de Costo Autorizados"
   INSERT INTO as005 VALUES (@ag_usr_des, @va_ide_cco)
										 
   IF @@ERROR <> 0
      RETURN
  
  --** Recupera siguiente registro
  FETCH NEXT FROM vc_aut_cco INTO @va_ide_cco
		
END

CLOSE vc_aut_cco
DEALLOCATE vc_aut_cco

----------------------------------------------------------
--          COPIA APLICACIONES AUTORIZADAS              --
----------------------------------------------------------
--** Elimina autorizaciones de aplicaciones ejecutables
--** del usuario destino.                              
DELETE FROM as007 WHERE (va_ide_usr = @ag_usr_des)

IF @@ERROR <> 0
   RETURN

--** Cursor de las aplicaciones autorizadas al usuario origen
DECLARE vc_apli_eje CURSOR LOCAL FOR
 SELECT va_nom_apl, va_ide_mod
   FROM as007
  WHERE (va_ide_usr = @ag_usr_ori)
		
OPEN vc_apli_eje    --** Abre cursor 
	
--** Recupera primer registro */
FETCH NEXT FROM vc_apli_eje INTO @va_nom_apl, @va_ide_mod
	
WHILE (@@FETCH_STATUS = 0)
BEGIN
   --** Autoriza aplicacion al usuario
   INSERT INTO as007 VALUES (@ag_usr_des, @va_nom_apl, @va_ide_mod)

   IF @@ERROR <> 0
      RETURN
		
   --** Recupera siguiente registro */
   FETCH NEXT FROM vc_apli_eje INTO @va_nom_apl, @va_ide_mod
END

CLOSE vc_apli_eje
DEALLOCATE vc_apli_eje


-----------------------------------------------------------
--  COPIA DEFAULTS DEL USUARIO ORIGEN AL USUARIO DESTINO --
-----------------------------------------------------------
DECLARE vc_def_usr CURSOR LOCAL FOR 
 SELECT va_ide_mod, va_ide_def, va_des_def,
        va_tip_def, va_def_car, va_def_ent, va_def_dec
   FROM as020
  WHERE (va_ide_usr = @ag_usr_ori)
			
OPEN vc_def_usr

--** Recupera primer default del usuario origen
FETCH NEXT FROM vc_def_usr INTO @va_ide_mod, @va_ide_def, @va_des_def, 
                                @va_tip_def, @va_def_car, @va_def_ent, 
                                @va_def_dec
								
WHILE (@@FETCH_STATUS = 0)
BEGIN
   --** Verifica si defaults YA esta registrada
   SELECT @va_nro_reg = COUNT(*)
     FROM as020
    WHERE (va_ide_usr = @ag_usr_des) AND
          (va_ide_mod = @va_ide_mod) AND
          (va_ide_def = @va_ide_def)

   IF @va_nro_reg = 0
   BEGIN
      --** Copia default al usuario destino
      INSERT INTO as020 VALUES (@ag_usr_des, @va_ide_mod, @va_ide_def,
   	                        @va_des_def, @va_tip_def, @va_def_car,
                                @va_def_ent, @va_def_dec)
      IF @@ERROR <> 0
         RETURN
   END
   ELSE
   BEGIN
      --** Actualiza default al usuario destino
      UPDATE as020 SET va_des_def = @va_des_def, va_tip_def = @va_tip_def,
                       va_def_car = @va_def_car, va_def_ent = @va_def_ent,
                       va_def_dec = @va_def_dec
                 WHERE (va_ide_usr = @ag_usr_des) AND
                       (va_ide_mod = @va_ide_mod) AND
                       (va_ide_def = @va_ide_def)
      IF @@ERROR <> 0
         RETURN
   END
		              
   --** Recupera siguiente default del usuario origen
   FETCH NEXT FROM vc_def_usr INTO @va_ide_mod, @va_ide_def, @va_des_def, 
                                   @va_tip_def, @va_def_car, @va_def_ent,  
                                   @va_def_dec
END
											
CLOSE vc_def_usr
DEALLOCATE vc_def_usr


----------------------------------------------------------
--       COPIA AUTORIZACIONES A USUARIOS P/ITEM         --
----------------------------------------------------------
--** ELIMINA AUTORIZACIONES P/ITEM USUARIO DESTINO
DELETE FROM as021 WHERE (va_ide_usr = @ag_usr_des)

IF @@ERROR <> 0
   RETURN

--** CURSOR SOBRE LAS AUTORIZACIONES P/ITEM DEL USUARIO ORIGEN
DECLARE vc_aut_ite CURSOR LOCAL FOR
 SELECT va_ide_tab, va_ite_aut
   FROM as021
  WHERE (va_ide_usr = @ag_usr_ori)
		
OPEN vc_aut_ite     --** Abre cursor 
	
--** Recupera primer registro
FETCH NEXT FROM vc_aut_ite INTO @va_ide_tab, @va_ite_aut
	
WHILE (@@FETCH_STATUS = 0) 
BEGIN
   --** Inserta registro en tabla "autorizacion a usuario x item
   INSERT INTO as021 VALUES (@ag_usr_des, @va_ide_tab, 
                             @va_ite_aut, GETDATE())
										 
   IF @@ERROR <> 0
      RETURN
  
  --** Recupera siguiente registro
  FETCH NEXT FROM vc_aut_ite INTO @va_ide_tab, @va_ite_aut
		
END

CLOSE vc_aut_ite
DEALLOCATE vc_aut_ite

	
----------------------------------------------------------
--     COPIA AUTORIZACIONES DE DOCUMENTOS P/MODULO      --
----------------------------------------------------------
--** ELIMINA AUTORIZACIONES DE DOCUMENTOS USUARIO DESTINO
DELETE FROM as015 WHERE (va_ide_usr = @ag_usr_des)

IF @@ERROR <> 0
   RETURN

--** CURSOR SOBRE LAS AUTORIZACIONES DE DOCUMENTOS P/MODULO
--** DEL USUARIO ORIGEN                                    
DECLARE vc_aut_doc CURSOR LOCAL FOR
 SELECT va_ide_doc, va_nro_ser, va_ide_mod, va_aut_nue,
        va_aut_mod, va_aut_anu, va_aut_eli, va_aut_con,
        va_aut_imp, va_obs_aut
  FROM as015
 WHERE (va_ide_usr = @ag_usr_ori)
		
OPEN vc_aut_doc     --** Abre cursor 
	
--** Recupera primer registro
FETCH NEXT FROM vc_aut_doc INTO @va_ide_doc, @va_nro_ser, @va_ide_mod,
                                @va_aut_nue, @va_aut_mod, @va_aut_anu,
		                @va_aut_eli, @va_aut_con, @va_aut_imp,
                                @va_obs_aut
	
WHILE (@@FETCH_STATUS = 0)
BEGIN
  --** Inserta registro en tabla "autorizacion de documentos
  INSERT INTO as015 VALUES (@va_ide_doc, @va_nro_ser, @ag_usr_des,
                            @va_ide_mod, @va_aut_nue, @va_aut_mod,
                            @va_aut_anu, @va_aut_eli, @va_aut_con,
			    @va_aut_imp, @va_obs_aut, SYSTEM_USER,
			    GETDATE())
										 
  IF @@ERROR <> 0
     RETURN
  
  --** Recupera siguiente registro
  FETCH NEXT FROM vc_aut_doc INTO @va_ide_doc, @va_nro_ser, @va_ide_mod,
                                  @va_aut_nue, @va_aut_mod, @va_aut_anu,
                                  @va_aut_eli, @va_aut_con, @va_aut_imp,
			          @va_obs_aut
END

CLOSE vc_aut_doc
DEALLOCATE vc_aut_doc


-------------------------------------------------------------
--  COPIA LINEA INICIAL Y FINAL DE PRODUCTOS DE LOS RUBROS --
--  AUTORIZADOS AL USUARIO DESTINO.                        --
-------------------------------------------------------------
--** ELIMINA AUTORIZACIONES LINEA INICIAL Y FINAL USUARIO DESTINO
DELETE FROM iv014 WHERE (va_ide_usr = @ag_usr_des)

IF @@ERROR <> 0
   RETURN

--** CURSOR SOBRE LAS AUTORIZACIONES LINEA INICIAL Y FINAL
--** DEL RUBRO AUTORIZADO AL USUARIO ORIGEN               
DECLARE vc_lin_pro CURSOR LOCAL FOR
 SELECT va_rub_inv, va_lin_ini, va_lin_fin, va_cto_aut
   FROM iv014
  WHERE (va_ide_usr = @ag_usr_ori)
			 
OPEN vc_lin_pro

--** Recupera primera autorizacion lineas de productos
FETCH NEXT FROM vc_lin_pro INTO @va_rub_inv, @va_lin_ini, @va_lin_fin, @va_cto_aut
  
WHILE (@@FETCH_STATUS = 0)
BEGIN	
   --** Copia autorizacion lineas de productos al usuario destino
   INSERT INTO iv014 VALUES (@ag_usr_des, @va_rub_inv, @va_lin_ini, 
                             @va_lin_fin, @va_cto_aut)
		
   IF @@ERROR <> 0
      RETURN
	
   --** Recupera siguiente autorizacion lineas de productos
   FETCH NEXT FROM vc_lin_pro INTO @va_rub_inv, @va_lin_ini, @va_lin_fin, @va_cto_aut

END
	
CLOSE vc_lin_pro
DEALLOCATE vc_lin_pro


---------------------------------------------------------------
--  COPIA CONTRASEÑAS DEL USUARIO ORIGEN AL USUARIO DESTINO  --
---------------------------------------------------------------
--** ELIMINA CONTRASEÑAS DEL USUARIO DESTINO
DELETE FROM as008 WHERE (va_ide_usr = @ag_usr_des)

IF @@ERROR <> 0
   RETURN

--** CURSOR SOBRE LAS CONTRASEÑAS DEL USUARIO ORIGEN
DECLARE vc_clv_ori CURSOR LOCAL FOR
 SELECT va_ide_mod, va_ide_cla, va_cla_ave
   FROM as008
  WHERE (va_ide_usr = @ag_usr_ori)
			 
OPEN vc_clv_ori

--** Recupera primera contraseña
FETCH NEXT FROM vc_clv_ori INTO @va_ide_mod, @va_ide_cla, @va_cla_ave
  
WHILE (@@FETCH_STATUS = 0)
BEGIN	
   --** Copia contraseña al usuario destino
   INSERT INTO as008 VALUES (@ag_usr_des, @va_ide_mod, @va_ide_cla, 
                             @va_cla_ave, SYSTEM_USER, GETDATE())
		
   IF @@ERROR <> 0
      RETURN
	
   --** Recupera siguiente contraseña
   FETCH NEXT FROM vc_clv_ori INTO @va_ide_mod, @va_ide_cla, @va_cla_ave

END
	
CLOSE vc_clv_ori
DEALLOCATE vc_clv_ori

---------------------------------------------------------------
--  COPIA ACCESOS DEL USUARIO ORIGEN AL USUARIO DESTINO      --
---------------------------------------------------------------
--** ELIMINA CONTROL ACCESO DEL USUARIO DESTINO
DELETE FROM as009 WHERE (va_ide_usr = @ag_usr_des)

IF @@ERROR <> 0
   RETURN

--** CURSOR SOBRE CONTROL ACCESOS DEL USUARIO ORIGEN
DECLARE vc_ctr_acc CURSOR LOCAL FOR
 SELECT va_nom_apl, va_eti_men, va_des_apl
   FROM as009
  WHERE (va_ide_usr = @ag_usr_ori)
			 
OPEN vc_ctr_acc

--** Recupera primer acceso restringido
FETCH NEXT FROM vc_ctr_acc INTO @va_nom_apl, @va_eti_men, @va_des_apl
  
WHILE (@@FETCH_STATUS = 0)
BEGIN	
   --** Copia acceso restringido al usuario destino
   INSERT INTO as009 VALUES (@ag_usr_des, @va_nom_apl, @va_eti_men, @va_des_apl) 
	
   IF @@ERROR <> 0
      RETURN
	
   --** Recupera siguiente acceso restringido
   FETCH NEXT FROM vc_ctr_acc INTO @va_nom_apl, @va_eti_men, @va_des_apl

END
	
CLOSE vc_ctr_acc
DEALLOCATE vc_ctr_acc


-------------------------------------------------------------------------------
--  COPIA PERSONALIZACION DATAWINDOWS DEL USUARIO ORIGEN AL USUARIO DESTINO  --
-------------------------------------------------------------------------------
--** ELIMINA PERSONALIZACION DATAWINDOW DEL USUARIO DESTINO
DELETE FROM as024 WHERE (va_ide_usr = @ag_usr_des)

IF @@ERROR <> 0
   RETURN

--** CURSOR SOBRE PERSONALIZACION DATAWINDOW DEL USUARIO ORIGEN
DECLARE vc_per_dwi CURSOR LOCAL FOR
 SELECT va_nom_dwi, va_col_des
   FROM as024
  WHERE (va_ide_usr = @ag_usr_ori)
			 
OPEN vc_per_dwi

--** Recupera primer acceso restringido
FETCH NEXT FROM vc_per_dwi INTO @va_nom_dwi, @va_col_des
  
WHILE (@@FETCH_STATUS = 0)
BEGIN	
   --** Copia personalizacion dw al usuario destino
   INSERT INTO as024 VALUES (@ag_usr_des, @va_nom_dwi, @va_col_des) 
	
   IF @@ERROR <> 0
      RETURN
	
   --** Recupera siguiente acceso restringido
   FETCH NEXT FROM vc_per_dwi INTO @va_nom_dwi, @va_col_des

END
	
CLOSE vc_per_dwi
DEALLOCATE vc_per_dwi



-------------------------------------------------------------------------------
-- COPIA Autorizacion Archivo Digital DEL USUARIO ORIGEN AL USUARIO DESTINO  --
-------------------------------------------------------------------------------
--** ELIMINA AUTORIZACIONES Archivo Digital DEL USUARIO DESTINO
DELETE FROM as037 WHERE (va_ide_usr = @ag_usr_des)

IF @@ERROR <> 0
   RETURN

--** CURSOR SOBRE AUTORIZACIONES Archivo Digital DEL USUARIO ORIGEN
DECLARE vc_aut_dig CURSOR LOCAL FOR
 SELECT va_ide_tab, va_ite_aut, va_fec_aut, va_con_aut
   FROM as037
  WHERE (va_ide_usr = @ag_usr_ori)
			 
OPEN vc_aut_dig

--** Recupera primer acceso restringido
FETCH NEXT FROM vc_aut_dig INTO @va_ide_tab, @va_ite_aut, @va_fec_aut, @va_con_aut
  
WHILE (@@FETCH_STATUS = 0)
BEGIN	
  --** Copia autorizaciones Archivo Digital al usuario destino
  INSERT INTO as037 VALUES (@ag_usr_des, @va_ide_tab, @va_ite_aut, @va_fec_aut, @va_con_aut) 
	
  IF @@ERROR <> 0
     RETURN
	
  --** Recupera siguiente acceso restringido
  FETCH NEXT FROM vc_aut_dig INTO @va_ide_tab, @va_ite_aut, @va_fec_aut, @va_con_aut
END
	
CLOSE vc_aut_dig
DEALLOCATE vc_aut_dig
