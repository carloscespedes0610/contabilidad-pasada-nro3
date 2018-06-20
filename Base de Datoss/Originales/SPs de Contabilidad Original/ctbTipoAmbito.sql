
IF OBJECT_ID('ctbTipoAmbitoSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbTipoAmbitoSelect 
END 
GO
CREATE PROC ctbTipoAmbitoSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@TipoAmbitoId int,
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0)			--All
	AND (@WhereFilter = 1)			--PrimaryKey
	BEGIN
		SELECT	ctbTipoAmbito.TipoAmbitoId, 
				ctbTipoAmbito.TipoAmbitoDes, 
				ctbTipoAmbito.EstadoId 
		FROM	ctbTipoAmbito 
		WHERE	TipoAmbitoId = @TipoAmbitoId 
	END

	ELSE IF (@SelectFilter = 3)		--Grid
	AND (@WhereFilter = 3)			--Grid
	AND (@OrderByFilter = 3)		--Grid
	BEGIN
		SELECT	ctbTipoAmbito.TipoAmbitoId, 
				ctbTipoAmbito.TipoAmbitoDes, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	ctbTipoAmbito  
		LEFT JOIN	parEstado ON ctbTipoAmbito.EstadoId = parEstado.EstadoId				  
		ORDER BY ctbTipoAmbito.TipoAmbitoDes 
	END

	ELSE IF (@SelectFilter = 2)		--ListBox
		AND (@WhereFilter = 5)		--EstadoId
		AND (@OrderByFilter = 2)	--TipoAmbitoDes
	BEGIN
		SELECT	ctbTipoAmbito.TipoAmbitoId, 
				ctbTipoAmbito.TipoAmbitoDes 
		FROM	ctbTipoAmbito 
		WHERE	ctbTipoAmbito.EstadoId = @EstadoId
		ORDER BY ctbTipoAmbito.TipoAmbitoDes
	END
END
GO
