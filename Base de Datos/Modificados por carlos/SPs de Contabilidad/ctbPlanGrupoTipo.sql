/********************************************************************/
/*  STORE PROCEDURE	: ctbPlanGrupoTipo							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  MODIFICACION	: Carlos Cespedes (15/06/2018)					*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbPlanGrupoTipoSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanGrupoTipoSelect 
END 
GO
CREATE PROC ctbPlanGrupoTipoSelect 
			@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('ctbPlanGrupoTipoInsert') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanGrupoTipoInsert	 
END 
GO
CREATE PROC ctbPlanGrupoTipoInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@PlanGrupoTipoCod varchar(50),
			@PlanGrupoTipoDes varchar(255),
			@PlanGrupoTipoEsp varchar(255),
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	PlanGrupoTipoCod 
					FROM	ctbPlanGrupoTipo 
					WHERE	PlanGrupoTipoCod = @PlanGrupoTipoCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbPlanGrupoTipo(PlanGrupoTipoCod, 
								PlanGrupoTipoDes,
								PlanGrupoTipoEsp,
								EstadoId)
						VALUES (@PlanGrupoTipoCod, 
								@PlanGrupoTipoDes,
								@PlanGrupoTipoEsp,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Tipo de Grupo de Cuentas Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbPlanGrupoTipoUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbPlanGrupoTipoUpdate 
END 
GO
CREATE PROC dbo.ctbPlanGrupoTipoUpdate 
			@UpdateFilter smallint,
			@PlanGrupoTipoId int,
			@PlanGrupoTipoCod varchar(50),
			@PlanGrupoTipoDes varchar(255),
			@PlanGrupoTipoEsp varchar(255),
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	PlanGrupoTipoId 
				FROM	ctbPlanGrupoTipo 
				WHERE	PlanGrupoTipoId = @PlanGrupoTipoId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	PlanGrupoTipoCod 
							FROM	ctbPlanGrupoTipo 
							WHERE	PlanGrupoTipoCod = @PlanGrupoTipoCod 
							AND		PlanGrupoTipoId <> @PlanGrupoTipoId)
			BEGIN	
				UPDATE dbo.ctbPlanGrupoTipo
				SET		PlanGrupoTipoCod = @PlanGrupoTipoCod, 
						PlanGrupoTipoDes = @PlanGrupoTipoDes, 
						PlanGrupoTipoEsp = @PlanGrupoTipoEsp, 
						EstadoId = @EstadoId
				WHERE	PlanGrupoTipoId = @PlanGrupoTipoId
			END
			ELSE
			BEGIN
				RAISERROR('Código de Tipo de Grupo de Cuentas Duplicado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Tipo de Grupo de Cuentas No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbPlanGrupoTipoDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbPlanGrupoTipoDelete 
END 
GO
CREATE PROC dbo.ctbPlanGrupoTipoDelete 
			@DeleteFilter smallint,
			@PlanGrupoTipoId int
AS 
BEGIN
	IF EXISTS (	SELECT	PlanGrupoTipoId 
				FROM	ctbPlanGrupoTipo 
				WHERE	PlanGrupoTipoId = @PlanGrupoTipoId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.ctbPlanGrupoTipo
			WHERE  PlanGrupoTipoId = @PlanGrupoTipoId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Tipo de Grupo de Cuentas No Encontrado', 16, 1)
		RETURN
    END 
END
GO

