/********************************************************************/
/*  STORE PROCEDURE	: segModuloSelect							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('segModuloSelect') IS NOT NULL
BEGIN 
    DROP PROC segModuloSelect 
END 
GO
CREATE PROC segModuloSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('segModuloInsert') IS NOT NULL
BEGIN 
    DROP PROC segModuloInsert	 
END 
GO
CREATE PROC segModuloInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@ModuloCod		varchar(50) ,   
			@ModuloDes		varchar(255) ,	
			@ModuloEsp		varchar(255) ,	
			@EstadoId			int 
AS
BEGIN
	IF NOT EXISTS (	SELECT	ModuloCod 
					FROM	segModulo 
					WHERE	ModuloDes = @ModuloDes 
					AND		ModuloCod = @ModuloCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO segModulo(
						ModuloCod,
						ModuloDes,
						ModuloEsp,
						EstadoId)
			VALUES (
						@ModuloCod,
						@ModuloDes,
						@ModuloEsp,
						@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Modulo Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.segModuloUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.segModuloUpdate 
END 
GO
CREATE PROC dbo.segModuloUpdate 
			@UpdateFilter smallint,
			@ModuloId int,
			@ModuloCod varchar(50),
			@ModuloDes varchar(255),
			@ModuloEsp varchar(255),
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	ModuloId 
				FROM	segModulo 
				WHERE	ModuloId = @ModuloId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			UPDATE dbo.segModulo
			SET 				
				ModuloCod = @ModuloCod, 
				ModuloDes = @ModuloDes, 
				ModuloEsp = @ModuloEsp, 
				EstadoId = @EstadoId
				
			WHERE  ModuloId = @ModuloId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Modulo No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.segModuloDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.segModuloDelete 
END 
GO
CREATE PROC dbo.segModuloDelete 
			@DeleteFilter smallint,
			@ModuloId int
AS 
BEGIN
	IF EXISTS (	SELECT	ModuloId 
				FROM	segModulo 
				WHERE	ModuloId = @ModuloId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.segModulo
			WHERE  ModuloId = @ModuloId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Modulo No Encontrado', 16, 1)
		RETURN
    END 
END
GO

