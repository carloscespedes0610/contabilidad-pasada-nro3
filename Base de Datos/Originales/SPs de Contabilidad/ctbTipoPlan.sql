
IF OBJECT_ID('ctbTipoPlanSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbTipoPlanSelect 
END 
GO
CREATE PROC ctbTipoPlanSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@TipoPlanId int,
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0)			--All
	AND (@WhereFilter = 1)			--PrimaryKey
	BEGIN
		SELECT	ctbTipoPlan.TipoPlanId, 
				ctbTipoPlan.TipoPlanDes, 
				ctbTipoPlan.EstadoId 
		FROM	ctbTipoPlan 
		WHERE	TipoPlanId = @TipoPlanId 
	END

	ELSE IF (@SelectFilter = 3)		--Grid
	AND (@WhereFilter = 3)			--Grid
	AND (@OrderByFilter = 3)		--Grid
	BEGIN
		SELECT	ctbTipoPlan.TipoPlanId, 
				ctbTipoPlan.TipoPlanDes, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	ctbTipoPlan  
		LEFT JOIN	parEstado ON ctbTipoPlan.EstadoId = parEstado.EstadoId				  
		ORDER BY ctbTipoPlan.TipoPlanDes 
	END

	ELSE IF (@SelectFilter = 2)		--ListBox
		AND (@WhereFilter = 5)		--EstadoId
		AND (@OrderByFilter = 2)	--TipoPlanDes
	BEGIN
		SELECT	ctbTipoPlan.TipoPlanId, 
				ctbTipoPlan.TipoPlanDes 
		FROM	ctbTipoPlan 
		WHERE	ctbTipoPlan.EstadoId = @EstadoId
		ORDER BY ctbTipoPlan.TipoPlanDes
	END
END
GO
