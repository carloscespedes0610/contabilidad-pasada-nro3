/********************************************************************/
/*  STORE PROCEDURE	: parTipoRelacion							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('parTipoRelacionSelect') IS NOT NULL
BEGIN 
    DROP PROC parTipoRelacionSelect 
END 
GO
CREATE PROC parTipoRelacionSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('parTipoRelacionInsert') IS NOT NULL
BEGIN 
    DROP PROC parTipoRelacionInsert	 
END 
GO
CREATE PROC parTipoRelacionInsert
			@InsertFilter smallint,
			@TipoRelacionId int, 
			@TipoRelacionCod varchar(50),
			@TipoRelacionDes varchar(255),
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	TipoRelacionCod 
					FROM	parTipoRelacion 
					WHERE	TipoRelacionCod = @TipoRelacionCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO parTipoRelacion(
								TipoRelacionId,
								TipoRelacionCod, 
								TipoRelacionDes,
								EstadoId)
						VALUES (
								@TipoRelacionId,
								@TipoRelacionCod, 
								@TipoRelacionDes,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Tipo Relacion Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.parTipoRelacionUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.parTipoRelacionUpdate 
END 
GO
CREATE PROC dbo.parTipoRelacionUpdate 
			@UpdateFilter smallint,
			@TipoRelacionId int,
			@TipoRelacionCod varchar(50),
			@TipoRelacionDes varchar(255),
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	TipoRelacionId 
				FROM	parTipoRelacion 
				WHERE	TipoRelacionId = @TipoRelacionId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	TipoRelacionCod 
							FROM	parTipoRelacion 
							WHERE	TipoRelacionCod = @TipoRelacionCod)
			BEGIN	
				UPDATE dbo.parTipoRelacion
				SET		TipoRelacionCod = @TipoRelacionCod, 
						TipoRelacionDes = @TipoRelacionDes, 
						EstadoId = @EstadoId
				WHERE  TipoRelacionId = @TipoRelacionId
			END
			ELSE
			BEGIN
				RAISERROR('Código de Tipo Relacion Duplicado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Tipo Relacion No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.parTipoRelacionDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.parTipoRelacionDelete 
END 
GO
CREATE PROC dbo.parTipoRelacionDelete 
			@DeleteFilter smallint,
			@TipoRelacionId int
AS 
BEGIN
	IF EXISTS (	SELECT	TipoRelacionId 
				FROM	parTipoRelacion 
				WHERE	TipoRelacionId = @TipoRelacionId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.parTipoRelacion
			WHERE  TipoRelacionId = @TipoRelacionId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Tipo Relacion No Encontrado', 16, 1)
		RETURN
    END 
END
GO

