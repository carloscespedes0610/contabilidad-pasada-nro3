
IF OBJECT_ID('parMonedaSelect') IS NOT NULL
BEGIN 
    DROP PROC parMonedaSelect 
END 
GO
CREATE PROC parMonedaSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@MonedaId int
AS
BEGIN
	IF  (@SelectFilter = 0)			--All
	AND (@WhereFilter = 1)			--PrimaryKey
	BEGIN
		SELECT	parMoneda.MonedaId, 
				parMoneda.MonedaCod, 
				parMoneda.MonedaDes 
		FROM	parMoneda 
		WHERE	MonedaId = @MonedaId 
	END

	ELSE IF (@SelectFilter = 3)		--Grid
	AND (@WhereFilter = 3)			--Grid
	AND (@OrderByFilter = 3)		--Grid
	BEGIN
		SELECT	parMoneda.MonedaId, 
				parMoneda.MonedaCod, 
				parMoneda.MonedaDes
		FROM	parMoneda  
		ORDER BY parMoneda.MonedaDes 
	END

	ELSE IF (@SelectFilter = 2)		--ListBox
		AND (@WhereFilter = 0)		--None
		AND (@OrderByFilter = 2)	--MonedaDes
	BEGIN
		SELECT	parMoneda.MonedaId, 
				parMoneda.MonedaCod, 
				parMoneda.MonedaDes 
		FROM	parMoneda 
		ORDER BY parMoneda.MonedaDes
	END
END
GO
