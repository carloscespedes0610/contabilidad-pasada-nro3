/********************************************************************/
/*  STORE PROCEDURE	: ctbPlanGrupo							   	    */
/*  AUTOR			: Joel Mercado	
/*  MODIFICACION	: Carlos Cespedes (15/06/2018)					*/								*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbPlanGrupoSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanGrupoSelect 
END 
GO
CREATE PROC ctbPlanGrupoSelect 
			@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO
			

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('ctbPlanGrupoInsert') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanGrupoInsert	 
END 
GO
CREATE PROC ctbPlanGrupoInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@PlanGrupoCod varchar(50),
			@PlanGrupoDes varchar(255),
			@PlanGrupoEsp varchar(255),
			@PlanGrupoTipoId int,
			@PlanGrupoTipoDetId int,
			@NroCuentas int,
			@MonedaId int,
			@VerificaMto int,
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	PlanGrupoCod 
					FROM	ctbPlanGrupo 
					WHERE	PlanGrupoCod = @PlanGrupoCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbPlanGrupo(PlanGrupoCod, 
								PlanGrupoDes,
								PlanGrupoEsp,
								PlanGrupoTipoId,
								PlanGrupoTipoDetId,
								NroCuentas,
								MonedaId,
								VerificaMto,
								EstadoId)
						VALUES (@PlanGrupoCod, 
								@PlanGrupoDes,
								@PlanGrupoEsp,
								@PlanGrupoTipoId,
								@PlanGrupoTipoDetId,
								@NroCuentas,
								@MonedaId,
								@VerificaMto,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Grupo de Cuentas Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbPlanGrupoUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbPlanGrupoUpdate 
END 
GO
CREATE PROC dbo.ctbPlanGrupoUpdate 
			@UpdateFilter smallint,
			@PlanGrupoId int,
			@PlanGrupoCod varchar(50),
			@PlanGrupoDes varchar(255),
			@PlanGrupoEsp varchar(255),
			@PlanGrupoTipoId int,
			@PlanGrupoTipoDetId int,
			@NroCuentas int,
			@MonedaId int,
			@VerificaMto int,
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	PlanGrupoId 
				FROM	ctbPlanGrupo 
				WHERE	PlanGrupoId = @PlanGrupoId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	PlanGrupoCod 
							FROM	ctbPlanGrupo 
							WHERE	PlanGrupoCod = @PlanGrupoCod 
							AND		PlanGrupoId <> @PlanGrupoId)
			BEGIN	
				UPDATE dbo.ctbPlanGrupo
				SET		PlanGrupoCod = @PlanGrupoCod, 
						PlanGrupoDes = @PlanGrupoDes, 
						PlanGrupoEsp = @PlanGrupoEsp, 
						PlanGrupoTipoId = @PlanGrupoTipoId, 
						PlanGrupoTipoDetId = @PlanGrupoTipoDetId, 
						NroCuentas = @NroCuentas, 
						MonedaId = @MonedaId, 
						VerificaMto = @VerificaMto, 
						EstadoId = @EstadoId
				WHERE  PlanGrupoId = @PlanGrupoId
			END
			ELSE
			BEGIN
				RAISERROR('Código de Grupo de Cuentas Duplicado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Grupo de Cuentas No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbPlanGrupoDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbPlanGrupoDelete 
END 
GO
CREATE PROC dbo.ctbPlanGrupoDelete 
			@DeleteFilter smallint,
			@PlanGrupoId int
AS 
BEGIN
	IF EXISTS (	SELECT	PlanGrupoId 
				FROM	ctbPlanGrupo 
				WHERE	PlanGrupoId = @PlanGrupoId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.ctbPlanGrupo
			WHERE  PlanGrupoId = @PlanGrupoId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Grupo de Cuentas No Encontrado', 16, 1)
		RETURN
    END 
END
GO

