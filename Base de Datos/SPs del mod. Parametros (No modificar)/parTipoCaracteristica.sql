/********************************************************************/
/*  STORE PROCEDURE	: parTipoCaracteristica							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('parTipoCaracteristicaSelect') IS NOT NULL
BEGIN 
    DROP PROC parTipoCaracteristicaSelect 
END 
GO
CREATE PROC parTipoCaracteristicaSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@TipoCaracteristicaId int,
			@TipoInfoId int,
			@TipoCaracteristicaDes varchar(255),
			@CaracteristicaId int,
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 1)   --PrimaryKey
	BEGIN
		SELECT	parTipoCaracteristica.TipoCaracteristicaId, 
				parTipoCaracteristica.TipoInfoId, 
				parTipoCaracteristica.TipoCaracteristicaDes, 
				parTipoCaracteristica.CaracteristicaId, 
				parTipoCaracteristica.EstadoId 
		FROM	parTipoCaracteristica 
		WHERE	TipoCaracteristicaId = @TipoCaracteristicaId 
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 3)		--Grid
	AND (@OrderByFilter = 3)	--Grid
	BEGIN
		SELECT	parTipoCaracteristica.TipoCaracteristicaId, 
				parTipoCaracteristica.TipoInfoId, 
				parTipoCaracteristica.TipoCaracteristicaDes, 
				parTipoCaracteristica.CaracteristicaId, 
				parCaracteristica.CaracteristicaDes,
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	parTipoCaracteristica  
		LEFT JOIN	parCaracteristica	ON parTipoCaracteristica.CaracteristicaId = parCaracteristica.CaracteristicaId				 
		LEFT JOIN	parEstado		ON parTipoCaracteristica.EstadoId = parEstado.EstadoId 
		ORDER BY parTipoCaracteristica.TipoCaracteristicaDes, parCaracteristica.CaracteristicaDes 
	END
	 
	ELSE IF (@SelectFilter = 2) --ListBox
	AND (@OrderByFilter = 2)	--RegTipoCaracteristicaDes
	BEGIN
		SELECT	parTipoCaracteristica.TipoCaracteristicaId ,
				parTipoCaracteristica.TipoCaracteristicaDes 
		FROM	parTipoCaracteristica 
	END	

	ELSE IF (@SelectFilter = 2) --ListBox
	AND (@WhereFilter = 6)	--EstadoId
	BEGIN
		SELECT	parTipoCaracteristica.TipoCaracteristicaId ,
				parTipoCaracteristica.TipoCaracteristicaDes 
		FROM	parTipoCaracteristica 
		WHERE	EstadoId = @EstadoId
		ORDER BY parTipoCaracteristica.TipoCaracteristicaDes 
	END	

	ELSE IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 6)		  --TipoInfoId
	BEGIN
		SELECT	parTipoCaracteristica.TipoCaracteristicaId, 
				parTipoCaracteristica.TipoInfoId, 
				parTipoCaracteristica.TipoCaracteristicaDes, 
				parTipoCaracteristica.CaracteristicaId, 
				parTipoCaracteristica.EstadoId 
		FROM	parTipoCaracteristica 
		WHERE	EstadoId = @EstadoId 
	END

END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('parTipoCaracteristicaInsert') IS NOT NULL
BEGIN 
    DROP PROC parTipoCaracteristicaInsert	 
END 
GO
CREATE PROC parTipoCaracteristicaInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@TipoInfoId int,
			@TipoCaracteristicaDes varchar(255),
			@CaracteristicaId int,
			@EstadoId int
AS
BEGIN
	IF @InsertFilter = 0 --All	
	BEGIN
		IF NOT EXISTS (	SELECT	TipoCaracteristicaId 
					FROM	parTipoCaracteristica 
					WHERE	TipoInfoId = @TipoInfoId 
					AND		TipoCaracteristicaDes = @TipoCaracteristicaDes 
					AND		CaracteristicaId = @CaracteristicaId) 
		BEGIN
			INSERT INTO parTipoCaracteristica(TipoInfoId, 
								TipoCaracteristicaDes,
								CaracteristicaId,
								EstadoId)
						VALUES (@TipoInfoId, 
								@TipoCaracteristicaDes,
								@CaracteristicaId,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
		ELSE
		BEGIN
			RAISERROR('Registro a Grupo de Cuentas Duplicado', 16, 1)
			RETURN
		END 
	END
	
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.parTipoCaracteristicaUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.parTipoCaracteristicaUpdate 
END 
GO
CREATE PROC dbo.parTipoCaracteristicaUpdate 
			@UpdateFilter smallint,
			@TipoCaracteristicaId int,
			@TipoInfoId int,
			@TipoCaracteristicaDes varchar(255),
			@CaracteristicaId int,
			@EstadoId int
AS 
BEGIN
	IF @UpdateFilter = 0 --All	
	BEGIN				
		IF EXISTS (	SELECT	TipoInfoId 
						FROM	parTipoCaracteristica 
						WHERE	TipoCaracteristicaId = @TipoCaracteristicaId)
		BEGIN	
			UPDATE dbo.parTipoCaracteristica
			SET		TipoInfoId = @TipoInfoId, 
					TipoCaracteristicaDes = @TipoCaracteristicaDes, 
					CaracteristicaId = @CaracteristicaId, 
					EstadoId = @EstadoId
			WHERE  TipoCaracteristicaId = @TipoCaracteristicaId
		END
		ELSE
		BEGIN
			RAISERROR('Registro De Tipo de Caracteristica No encontrado', 16, 1)
			RETURN
		END 
		
	END
	
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.parTipoCaracteristicaDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.parTipoCaracteristicaDelete 
END 
GO
CREATE PROC dbo.parTipoCaracteristicaDelete 
			@DeleteFilter smallint,
			@TipoCaracteristicaId int
AS 
BEGIN
	IF @DeleteFilter = 0 --All
	BEGIN
		IF EXISTS (	SELECT	TipoCaracteristicaId 
					FROM	parTipoCaracteristica 
					WHERE	TipoCaracteristicaId = @TipoCaracteristicaId) 
		BEGIN
			DELETE
			FROM   dbo.parTipoCaracteristica
			WHERE  TipoCaracteristicaId = @TipoCaracteristicaId
		END
		ELSE
		BEGIN
			RAISERROR('ID de Registro a Tipo Caratecteristica No Encontrado', 16, 1)
			RETURN
		END 
	END	
END
GO

