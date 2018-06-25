/********************************************************************/
/*  STORE PROCEDURE	: ctbPlan							   	    */
/*  AUTOR			: Joel Mercado                             */
/*	UPDATE			: Carlos Cespedes(15/06/2018)								*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbPlanSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanSelect 
END 
GO
CREATE PROC ctbPlanSelect 
			@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('ctbPlanInsert') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanInsert	 
END 
GO
CREATE PROC ctbPlanInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@PlanCod varchar(50),
			@PlanDes varchar(255),
			@PlanEsp varchar(255),
			@TipoPlanId int,
			@Orden int,
			@Nivel int,
			@MonedaId int,
			@TipoAmbitoId int,
			@PlanAjusteId int,
			@CapituloId int,
			@PlanPadreId int,
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	PlanCod 
					FROM	ctbPlan 
					WHERE	PlanCod = @PlanCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbPlan(PlanCod, 
								PlanDes,
								PlanEsp,
								TipoPlanId,
								Orden,
								Nivel,
								MonedaId,
								TipoAmbitoId,
								PlanAjusteId,
								CapituloId,
								PlanPadreId,
								EstadoId)
						VALUES (@PlanCod, 
								@PlanDes,
								@PlanEsp,
								@TipoPlanId,
								@Orden,
								@Nivel,
								@MonedaId,
								@TipoAmbitoId,
								@PlanAjusteId,
								@CapituloId,
								@PlanPadreId,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Cuenta Contable Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbPlanUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbPlanUpdate 
END 
GO
CREATE PROC dbo.ctbPlanUpdate 
			@UpdateFilter smallint,
			@PlanId int,
			@PlanCod varchar(50),
			@PlanDes varchar(255),
			@PlanEsp varchar(255),
			@TipoPlanId int,
			@Orden int,
			@Nivel int,
			@MonedaId int,
			@TipoAmbitoId int,
			@PlanAjusteId int,
			@CapituloId int,
			@PlanPadreId int,
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	PlanId 
				FROM	ctbPlan 
				WHERE	PlanId = @PlanId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	PlanCod 
							FROM	ctbPlan 
							WHERE	PlanCod = @PlanCod 
							AND		PlanId <> @PlanId)
			BEGIN	
				UPDATE dbo.ctbPlan
				SET		PlanCod = @PlanCod, 
						PlanDes = @PlanDes, 
						PlanEsp = @PlanEsp, 
						TipoPlanId = @TipoPlanId, 
						Orden = @Orden, 
						Nivel = @Nivel, 
						MonedaId = @MonedaId, 
						TipoAmbitoId = @TipoAmbitoId, 
						PlanAjusteId = @PlanAjusteId, 
						CapituloId = @CapituloId, 
						PlanPadreId = @PlanPadreId, 
						EstadoId = @EstadoId
				WHERE  PlanId = @PlanId
			END
			ELSE
			BEGIN
				RAISERROR('Código de Cuenta Contable Duplicado', 16, 1)
				RETURN
			END 
		END
		
		IF @UpdateFilter = 1 -- Actualiza solo el Orden
		BEGIN
			UPDATE dbo.ctbPlan
				SET		Orden = @Orden
				WHERE  PlanId = @PlanId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Cuenta Contable No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbPlanDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbPlanDelete 
END 
GO
CREATE PROC dbo.ctbPlanDelete 
			@DeleteFilter smallint,
			@PlanId int
AS 
BEGIN
	IF EXISTS (	SELECT	PlanId 
				FROM	ctbPlan 
				WHERE	PlanId = @PlanId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.ctbPlan
			WHERE  PlanId = @PlanId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Cuenta Contable No Encontrado', 16, 1)
		RETURN
    END 
END
GO

