/********************************************************************/
/*  STORE PROCEDURE	: ctbPlanGrupoDet							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbPlanGrupoDetSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanGrupoDetSelect 
END 
GO
CREATE PROC ctbPlanGrupoDetSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@PlanGrupoDetId int,
			@PlanGrupoId int,
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 1)   --PrimaryKey
	BEGIN
		SELECT	ctbPlanGrupoDet.PlanGrupoDetId, 
				ctbPlanGrupoDet.PlanGrupoId, 
				ctbPlanGrupoDet.PlanGrupoDetDes, 
				ctbPlanGrupoDet.PlanId, 
				ctbPlanGrupoDet.PlanFlujoId, 
				ctbPlanGrupoDet.SucursalId, 
				ctbPlanGrupoDet.CenCosId, 
				ctbPlanGrupoDet.Orden, 
				ctbPlanGrupoDet.EstadoId 
		FROM	ctbPlanGrupoDet 
		WHERE	PlanGrupoDetId = @PlanGrupoDetId 
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 3)		--Grid
	AND (@OrderByFilter = 3)	--Grid
	BEGIN
		SELECT	ctbPlanGrupoDet.PlanGrupoDetId, 
				ctbPlanGrupo.PlanGrupoId, 
				ctbPlanGrupo.PlanGrupoDes, 
				ctbPlanGrupoDet.PlanGrupoDetDes, 
				ctbPlan.PlanId, 
				ctbPlan.PlanDes, 
				ctbPlanGrupoDet.PlanFlujoId, 
				ctbSucursal.SucursalId, 
				ctbSucursal.SucursalDes, 
				ctbCenCos.CenCosId, 
				ctbCenCos.CenCosDes, 
				ctbPlanGrupoDet.Orden, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	ctbPlanGrupoDet  
		LEFT JOIN	ctbPlanGrupo	ON ctbPlanGrupoDet.PlanGrupoId = ctbPlanGrupo.PlanGrupoId				 
		LEFT JOIN	ctbPlan			ON ctbPlanGrupoDet.PlanId = ctbPlan.PlanId				 
		LEFT JOIN	ctbSucursal		ON ctbPlanGrupoDet.SucursalId = ctbSucursal.SucursalId				 
		LEFT JOIN	ctbCenCos		ON ctbPlanGrupoDet.CenCosId = ctbCenCos.CenCosId 		
		LEFT JOIN	parEstado		ON ctbPlanGrupoDet.EstadoId = parEstado.EstadoId 
		WHERE	ctbPlanGrupoDet.PlanGrupoId = @PlanGrupoId 
		ORDER BY ctbPlanGrupoDet.Orden 
	END

	ELSE IF (@SelectFilter = 2) --ListBox
	AND (@OrderByFilter = 2)	--PlanGrupoDetDes
	BEGIN
		SELECT	ctbPlanGrupoDet.PlanGrupoDetId, 
				ctbPlanGrupoDet.PlanGrupoId, 
				ctbPlanGrupoDet.PlanGrupoDetDes 
		FROM	ctbPlanGrupoDet 
		ORDER BY ctbPlanGrupoDet.PlanGrupoDetDes
	END

	ELSE IF  (@SelectFilter = 0 )	--All
	AND (@WhereFilter = 5)			--PlanGrupoId
	AND (@OrderByFilter = 5)		--Orden
	BEGIN
		SELECT	ctbPlanGrupoDet.PlanGrupoDetId, 
				ctbPlanGrupoDet.PlanGrupoId, 
				ctbPlanGrupoDet.PlanGrupoDetDes, 
				ctbPlanGrupoDet.PlanId, 
				ctbPlanGrupoDet.PlanFlujoId, 
				ctbPlanGrupoDet.SucursalId, 
				ctbPlanGrupoDet.CenCosId, 
				ctbPlanGrupoDet.Orden, 
				ctbPlanGrupoDet.EstadoId 
		FROM	ctbPlanGrupoDet 
		WHERE	PlanGrupoId = @PlanGrupoId 
		ORDER BY ctbPlanGrupoDet.Orden 
	END
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('ctbPlanGrupoDetInsert') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanGrupoDetInsert	 
END 
GO
CREATE PROC ctbPlanGrupoDetInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@PlanGrupoId varchar(50),
			@PlanGrupoDetDes varchar(255),
			@PlanId int,
			@PlanFlujoId int,
			@SucursalId int,
			@CenCosId int,
			@Orden int,
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	PlanGrupoId 
					FROM	ctbPlanGrupoDet 
					WHERE	PlanGrupoId = @PlanGrupoId 
					AND		PlanId = @PlanId) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbPlanGrupoDet(PlanGrupoId, 
								PlanGrupoDetDes,
								PlanId,
								PlanFlujoId,
								SucursalId,
								CenCosId,
								Orden,
								EstadoId)
						VALUES (@PlanGrupoId, 
								@PlanGrupoDetDes,
								@PlanId,
								@PlanFlujoId,
								@SucursalId,
								@CenCosId,
								@Orden,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Detalle de Grupo de Cuentas Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbPlanGrupoDetUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbPlanGrupoDetUpdate 
END 
GO
CREATE PROC dbo.ctbPlanGrupoDetUpdate 
			@UpdateFilter smallint,
			@PlanGrupoDetId int,
			@PlanGrupoId varchar(50),
			@PlanGrupoDetDes varchar(255),
			@PlanId int,
			@PlanFlujoId int,
			@SucursalId int,
			@CenCosId int,
			@Orden int,
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	PlanGrupoDetId 
				FROM	ctbPlanGrupoDet 
				WHERE	PlanGrupoDetId = @PlanGrupoDetId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	PlanGrupoId 
							FROM	ctbPlanGrupoDet 
							WHERE	PlanGrupoId = @PlanGrupoId 
							AND		PlanId = @PlanId 
							AND		PlanGrupoDetId <> @PlanGrupoDetId)
			BEGIN	
				UPDATE dbo.ctbPlanGrupoDet
				SET		PlanGrupoId = @PlanGrupoId, 
						PlanGrupoDetDes = @PlanGrupoDetDes, 
						PlanId = @PlanId, 
						PlanFlujoId = @PlanFlujoId, 
						SucursalId = @SucursalId, 
						CenCosId = @CenCosId, 
						Orden = @Orden, 
						EstadoId = @EstadoId
				WHERE  PlanGrupoDetId = @PlanGrupoDetId
			END
			ELSE
			BEGIN
				RAISERROR('Código de Detalle de Grupo de Cuentas Duplicado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Detalle de Grupo de Cuentas No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbPlanGrupoDetDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbPlanGrupoDetDelete 
END 
GO
CREATE PROC dbo.ctbPlanGrupoDetDelete 
			@DeleteFilter smallint,
			@PlanGrupoDetId int,
			@PlanGrupoId int
AS 
BEGIN
	IF @DeleteFilter = 0 --All
	BEGIN
		IF EXISTS (	SELECT	PlanGrupoDetId 
					FROM	ctbPlanGrupoDet 
					WHERE	PlanGrupoDetId = @PlanGrupoDetId) 
		BEGIN
			DELETE
			FROM   dbo.ctbPlanGrupoDet
			WHERE  PlanGrupoDetId = @PlanGrupoDetId
		END
		ELSE
		BEGIN
			RAISERROR('ID de Detalle de Grupo de Cuentas No Encontrado', 16, 1)
			RETURN
		END 
	END
	ELSE IF @DeleteFilter = 1 --PlanGrupoId
	BEGIN
		IF EXISTS (	SELECT	PlanGrupoId 
					FROM	ctbPlanGrupoDet 
					WHERE	PlanGrupoId = @PlanGrupoId) 
		BEGIN
			DELETE
			FROM   dbo.ctbPlanGrupoDet
			WHERE  PlanGrupoId = @PlanGrupoId
		END
		ELSE
		BEGIN
			RAISERROR('ID de Detalle de Grupo de Cuentas No Encontrado', 16, 1)
			RETURN
		END 
	END
END
GO

