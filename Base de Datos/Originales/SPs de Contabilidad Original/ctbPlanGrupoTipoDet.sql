/********************************************************************/
/*  STORE PROCEDURE	: ctbPlanGrupoTipoDet							*/
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbPlanGrupoTipoDetSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanGrupoTipoDetSelect 
END 
GO
CREATE PROC ctbPlanGrupoTipoDetSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@PlanGrupoTipoDetId int,
			@PlanGrupoTipoDetCod varchar(50),
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 1)   --PrimaryKey
	BEGIN
		SELECT	ctbPlanGrupoTipoDet.PlanGrupoTipoDetId, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetCod, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetEsp, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoId, 
				ctbPlanGrupoTipoDet.EstadoId 
		FROM	ctbPlanGrupoTipoDet 
		WHERE	PlanGrupoTipoDetId = @PlanGrupoTipoDetId 
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 3)		--Grid
	AND (@OrderByFilter = 3)	--Grid
	BEGIN
		SELECT	ctbPlanGrupoTipoDet.PlanGrupoTipoDetId, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetCod, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetEsp, 
				ctbPlanGrupoTipo.PlanGrupoTipoId, 
				ctbPlanGrupoTipo.PlanGrupoTipoDes, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	ctbPlanGrupoTipoDet  
		LEFT JOIN	ctbPlanGrupoTipo	ON ctbPlanGrupoTipoDet.PlanGrupoTipoId = ctbPlanGrupoTipo.PlanGrupoTipoId				 
		LEFT JOIN	parEstado		ON ctbPlanGrupoTipoDet.EstadoId = parEstado.EstadoId 
		ORDER BY ctbPlanGrupoTipo.PlanGrupoTipoDes, ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes 
	END

	ELSE IF (@SelectFilter = 2) --ListBox
	AND (@OrderByFilter = 2)	--PlanGrupoTipoDetDes
	BEGIN
		SELECT	ctbPlanGrupoTipoDet.PlanGrupoTipoDetId, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetCod, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoId 
		FROM	ctbPlanGrupoTipoDet 
		ORDER BY ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes
	END

	ELSE IF  (@SelectFilter = 0 )	--All
	AND (@WhereFilter = 5)			--PlanGrupoTipoDetCod
	BEGIN
		SELECT	ctbPlanGrupoTipoDet.PlanGrupoTipoDetId, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetCod, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetEsp, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoId, 
				ctbPlanGrupoTipoDet.EstadoId 
		FROM	ctbPlanGrupoTipoDet 
		WHERE	PlanGrupoTipoDetCod = @PlanGrupoTipoDetCod 
	END
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('ctbPlanGrupoTipoDetInsert') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanGrupoTipoDetInsert	 
END 
GO
CREATE PROC ctbPlanGrupoTipoDetInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@PlanGrupoTipoDetCod varchar(50),
			@PlanGrupoTipoDetDes varchar(255),
			@PlanGrupoTipoDetEsp varchar(255),
			@PlanGrupoTipoId int,
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	PlanGrupoTipoDetCod 
					FROM	ctbPlanGrupoTipoDet 
					WHERE	PlanGrupoTipoDetCod = @PlanGrupoTipoDetCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbPlanGrupoTipoDet(PlanGrupoTipoDetCod, 
								PlanGrupoTipoDetDes,
								PlanGrupoTipoDetEsp,
								PlanGrupoTipoId,
								EstadoId)
						VALUES (@PlanGrupoTipoDetCod, 
								@PlanGrupoTipoDetDes,
								@PlanGrupoTipoDetEsp,
								@PlanGrupoTipoId,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Detalle de Tipo de Grupo de Cuentas Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbPlanGrupoTipoDetUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbPlanGrupoTipoDetUpdate 
END 
GO
CREATE PROC dbo.ctbPlanGrupoTipoDetUpdate 
			@UpdateFilter smallint,
			@PlanGrupoTipoDetId int,
			@PlanGrupoTipoDetCod varchar(50),
			@PlanGrupoTipoDetDes varchar(255),
			@PlanGrupoTipoDetEsp varchar(255),
			@PlanGrupoTipoId int,
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	PlanGrupoTipoDetId 
				FROM	ctbPlanGrupoTipoDet 
				WHERE	PlanGrupoTipoDetId = @PlanGrupoTipoDetId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	PlanGrupoTipoDetCod 
							FROM	ctbPlanGrupoTipoDet 
							WHERE	PlanGrupoTipoDetCod = @PlanGrupoTipoDetCod 
							AND		PlanGrupoTipoDetId <> @PlanGrupoTipoDetId)
			BEGIN	
				UPDATE dbo.ctbPlanGrupoTipoDet
				SET		PlanGrupoTipoDetCod = @PlanGrupoTipoDetCod, 
						PlanGrupoTipoDetDes = @PlanGrupoTipoDetDes, 
						PlanGrupoTipoDetEsp = @PlanGrupoTipoDetEsp, 
						PlanGrupoTipoId = @PlanGrupoTipoId, 
						EstadoId = @EstadoId
				WHERE	PlanGrupoTipoDetId = @PlanGrupoTipoDetId
			END
			ELSE
			BEGIN
				RAISERROR('Código de Detalle de Tipo de Grupo de Cuentas Duplicado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Detalle de Tipo de Grupo de Cuentas No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbPlanGrupoTipoDetDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbPlanGrupoTipoDetDelete 
END 
GO
CREATE PROC dbo.ctbPlanGrupoTipoDetDelete 
			@DeleteFilter smallint,
			@PlanGrupoTipoDetId int
AS 
BEGIN
	IF EXISTS (	SELECT	PlanGrupoTipoDetId 
				FROM	ctbPlanGrupoTipoDet 
				WHERE	PlanGrupoTipoDetId = @PlanGrupoTipoDetId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.ctbPlanGrupoTipoDet
			WHERE  PlanGrupoTipoDetId = @PlanGrupoTipoDetId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Detalle de Tipo de Grupo de Cuentas No Encontrado', 16, 1)
		RETURN
    END 
END
GO

