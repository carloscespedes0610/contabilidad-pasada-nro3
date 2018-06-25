/********************************************************************/
/*  STORE PROCEDURE	: ctbPlan							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbPlanSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanSelect 
END 
GO
CREATE PROC ctbPlanSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@PlanId int,
			@PlanCod varchar(50),
			@PlanPadreId int,
			@TipoPlanId int,
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 1)   --PrimaryKey
	BEGIN
		SELECT	ctbPlan.PlanId, 
				ctbPlan.PlanCod, 
				ctbPlan.PlanDes, 
				ctbPlan.PlanEsp, 
				ctbPlan.TipoPlanId, 
				ctbPlan.Orden, 
				ctbPlan.Nivel, 
				ctbPlan.MonedaId, 
				ctbPlan.TipoAmbitoId, 
				ctbPlan.PlanAjusteId, 
				ctbPlan.CapituloId, 
				ctbPlan.PlanPadreId, 
				ctbPlan.EstadoId 
		FROM	ctbPlan 
		WHERE	PlanId = @PlanId 
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 3)		--Grid
	AND (@OrderByFilter = 3)	--Grid
	BEGIN
		SELECT	ctbPlan.PlanId, 
				ctbPlan.PlanCod, 
				ctbPlan.PlanDes, 
				ctbTipoPlan.TipoPlanId, 
				ctbTipoPlan.TipoPlanDes, 
				ctbPlan.Orden, 
				ctbPlan.Nivel, 
				parMoneda.MonedaId, 
				parMoneda.MonedaDes, 
				ctbPlan.CapituloId, 
				ctbPlan.PlanPadreId, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	ctbPlan  
		LEFT JOIN	ctbTipoPlan		ON ctbPlan.TipoPlanId = ctbTipoPlan.TipoPlanId				 
		LEFT JOIN	parMoneda		ON ctbPlan.MonedaId = parMoneda.MonedaId 		
		LEFT JOIN	parEstado		ON ctbPlan.EstadoId = parEstado.EstadoId 
		ORDER BY PlanCod, Orden 
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 6)		--Grid_PlanPadreId
	AND (@OrderByFilter = 3)	--Grid
	BEGIN
		SELECT	ctbPlan.PlanId, 
				ctbPlan.PlanCod, 
				ctbPlan.PlanDes, 
				ctbTipoPlan.TipoPlanId, 
				ctbTipoPlan.TipoPlanDes, 
				ctbPlan.Orden, 
				ctbPlan.Nivel, 
				parMoneda.MonedaId, 
				parMoneda.MonedaDes, 
				ctbPlan.CapituloId, 
				ctbPlan.PlanPadreId, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	ctbPlan  
		LEFT JOIN	ctbTipoPlan		ON ctbPlan.TipoPlanId = ctbTipoPlan.TipoPlanId				 
		LEFT JOIN	parMoneda		ON ctbPlan.MonedaId = parMoneda.MonedaId 		
		LEFT JOIN	parEstado		ON ctbPlan.EstadoId = parEstado.EstadoId 
		WHERE	ctbPlan.PlanPadreId = @PlanPadreId 
		ORDER BY Orden 
	END

	ELSE IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 7)		  --PlanPadreId
	BEGIN
		SELECT	ctbPlan.PlanId, 
				ctbPlan.PlanCod, 
				ctbPlan.PlanDes, 
				ctbPlan.PlanEsp, 
				ctbPlan.TipoPlanId, 
				ctbPlan.Orden, 
				ctbPlan.Nivel, 
				ctbPlan.MonedaId, 
				ctbPlan.TipoAmbitoId, 
				ctbPlan.PlanAjusteId, 
				ctbPlan.CapituloId, 
				ctbPlan.PlanPadreId, 
				ctbPlan.EstadoId 
		FROM	ctbPlan 
		WHERE	PlanPadreId = @PlanPadreId 
	END

	ELSE IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 8)		  --PlanHijo_MaxOrden
	BEGIN
		SELECT	ctbPlan.PlanId, 
				ctbPlan.PlanCod, 
				ctbPlan.PlanDes, 
				ctbPlan.PlanEsp, 
				ctbPlan.TipoPlanId, 
				ctbPlan.Orden, 
				ctbPlan.Nivel, 
				ctbPlan.MonedaId, 
				ctbPlan.TipoAmbitoId, 
				ctbPlan.PlanAjusteId, 
				ctbPlan.CapituloId, 
				ctbPlan.PlanPadreId, 
				ctbPlan.EstadoId 
		FROM	ctbPlan 
		WHERE	PlanPadreId = @PlanPadreId 
		AND		EstadoId = @EstadoId 
		AND		Orden = (SELECT MAX(Orden) 
						FROM	ctbPlan 
						WHERE	PlanPadreId = @PlanPadreId)
	END

	ELSE IF (@SelectFilter = 2) --ListBox
	AND (@WhereFilter = 10)		--TipoPlanId
	AND (@OrderByFilter = 2)	--PlanDes
	BEGIN
		SELECT	ctbPlan.PlanId, 
				ctbPlan.PlanCod, 
				ctbPlan.PlanDes 
		FROM	ctbPlan 
		WHERE	TipoPlanId = @TipoPlanId 
		AND		EstadoId = @EstadoId 
		ORDER BY ctbPlan.PlanDes
	END

	ELSE IF  (@SelectFilter = 0 )	--All
	AND (@WhereFilter = 5)			--PlanCod
	BEGIN
		SELECT	ctbPlan.PlanId, 
				ctbPlan.PlanCod, 
				ctbPlan.PlanDes, 
				ctbPlan.PlanEsp, 
				ctbPlan.TipoPlanId, 
				ctbPlan.Orden, 
				ctbPlan.Nivel, 
				ctbPlan.MonedaId, 
				ctbPlan.TipoAmbitoId, 
				ctbPlan.PlanAjusteId, 
				ctbPlan.CapituloId, 
				ctbPlan.PlanPadreId, 
				ctbPlan.EstadoId 
		FROM	ctbPlan 
		WHERE	PlanCod = @PlanCod 
	END
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

