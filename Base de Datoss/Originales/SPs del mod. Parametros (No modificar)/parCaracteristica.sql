/********************************************************************/
/*  STORE PROCEDURE	: parCaracteristica							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('parCaracteristicaSelect') IS NOT NULL
BEGIN 
    DROP PROC parCaracteristicaSelect 
END 
GO
CREATE PROC parCaracteristicaSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@CaracteristicaId int,
			@TipoCaracteristicaId int,
			@CaracteristicaDes varchar(255),
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 1)   --PrimaryKey
	BEGIN
		SELECT	parCaracteristica.CaracteristicaId, 
				parCaracteristica.TipoCaracteristicaId, 
				parCaracteristica.CaracteristicaDes, 
				parCaracteristica.EstadoId 
		FROM	parCaracteristica 
		WHERE	CaracteristicaId = @CaracteristicaId 
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 3)		--Grid
	AND (@OrderByFilter = 3)	--Grid
	BEGIN
		SELECT	parCaracteristica.CaracteristicaId, 
				parCaracteristica.CaracteristicaDes, 
				parCaracteristica.TipoCaracteristicaId, 
				parTipoCaracteristica.TipoCaracteristicaDes, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	parCaracteristica  
		LEFT JOIN	parTipoCaracteristica	ON parCaracteristica.CaracteristicaId = parTipoCaracteristica.CaracteristicaId				 
		LEFT JOIN	parEstado		ON parCaracteristica.EstadoId = parEstado.EstadoId 
		ORDER BY parCaracteristica.CaracteristicaDes, parTipoCaracteristica.TipoCaracteristicaDes 
	END
	 
	ELSE IF (@SelectFilter = 2) --ListBox
	AND (@OrderByFilter = 2)	--CaracteristicaDes
	BEGIN
		SELECT	parCaracteristica.CaracteristicaId ,
				parCaracteristica.CaracteristicaDes 
		FROM	parCaracteristica 
	END

	ELSE IF (@SelectFilter = 2) --ListBox
	AND (@WhereFilter = 5)	--TipoCaracteristicaId
	BEGIN
		SELECT	parCaracteristica.CaracteristicaId ,
				parCaracteristica.CaracteristicaDes 
		FROM	parCaracteristica 
		WHERE	parCaracteristica.TipoCaracteristicaId = @TipoCaracteristicaId
	END

	ELSE IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 6)		  --CaracteristicaId
	BEGIN
		SELECT	parCaracteristica.CaracteristicaId, 
				parCaracteristica.TipoCaracteristicaId, 
				parCaracteristica.CaracteristicaDes, 
				parCaracteristica.EstadoId 
		FROM	parCaracteristica 
		WHERE	EstadoId = @EstadoId
	END

END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('parCaracteristicaInsert') IS NOT NULL
BEGIN 
    DROP PROC parCaracteristicaInsert	 
END 
GO
CREATE PROC parCaracteristicaInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@TipoCaracteristicaId int,
			@CaracteristicaDes varchar(255),
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	CaracteristicaId 
					FROM	parCaracteristica 
					WHERE	TipoCaracteristicaId = @TipoCaracteristicaId 
					AND		CaracteristicaDes = @CaracteristicaDes) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO parCaracteristica(TipoCaracteristicaId, 
								CaracteristicaDes,
								EstadoId)
						VALUES (@TipoCaracteristicaId, 
								@CaracteristicaDes,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Registro de Caracteristicas Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.parCaracteristicaUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.parCaracteristicaUpdate 
END 
GO
CREATE PROC dbo.parCaracteristicaUpdate 
			@UpdateFilter smallint,
			@CaracteristicaId int,
			@TipoCaracteristicaId int,
			@CaracteristicaDes varchar(255),
			@EstadoId int
AS 
BEGIN
	IF @UpdateFilter = 0 --All	
	BEGIN
		BEGIN
			IF EXISTS (	SELECT	TipoCaracteristicaId 
				FROM	parCaracteristica 
				WHERE	CaracteristicaId = @CaracteristicaId) 
			BEGIN	
				UPDATE dbo.parCaracteristica
				SET		TipoCaracteristicaId	= @TipoCaracteristicaId, 
						CaracteristicaDes		= @CaracteristicaDes, 
						EstadoId				= @EstadoId
				WHERE  CaracteristicaId = @CaracteristicaId
			END
			ELSE
			BEGIN
				RAISERROR('ID de Registro Caracteristicas No Encontrado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('Opcion de Actualización no seleccionado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.parCaracteristicaDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.parCaracteristicaDelete 
END 
GO
CREATE PROC dbo.parCaracteristicaDelete 
			@DeleteFilter smallint,
			@CaracteristicaId int
AS 
BEGIN
	IF @DeleteFilter = 0 --All
	BEGIN
		IF EXISTS (	SELECT	CaracteristicaId 
					FROM	parCaracteristica 
					WHERE	CaracteristicaId = @CaracteristicaId) 
		BEGIN
			DELETE
			FROM   dbo.parCaracteristica
			WHERE  CaracteristicaId = @CaracteristicaId
		END
		ELSE
		BEGIN
			RAISERROR('ID de Registro Caracteristica No Encontrado', 16, 1)
			RETURN
		END 
	END
	--ELSE IF @DeleteFilter = 1 --CaracteristicaId
	--BEGIN
	--	IF EXISTS (	SELECT	CaracteristicaId 
	--				FROM	parCaracteristica 
	--				WHERE	CaracteristicaId = @CaracteristicaId) 
	--	BEGIN
	--		DELETE
	--		FROM   dbo.parCaracteristica
	--		WHERE  CaracteristicaId = @CaracteristicaId
	--	END
	--	ELSE
	--	BEGIN
	--		RAISERROR('ID de Detalle de Grupo de Cuentas No Encontrado', 16, 1)
	--		RETURN
	--	END 
	--END
END
GO

