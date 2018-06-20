/********************************************************************/
/*  STORE PROCEDURE	: ctbPlanGrupoDet							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  MODIFICACION	: Carlos Cespedes (15/06/2018)					*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbPlanGrupoDetSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanGrupoDetSelect 
END 
GO
CREATE PROC ctbPlanGrupoDetSelect 
			@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

------------

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

