/********************************************************************/
/*  STORE PROCEDURE	: ctbCapituloSelect							   	    */
/*  AUTOR			: Joel Mercado      */
/*  MODIFICACION	: Carlos Cespedes (15/06/2018)					*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbCapituloSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbCapituloSelect 
END 
GO
CREATE PROC ctbCapituloSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('ctbCapituloInsert') IS NOT NULL
BEGIN 
    DROP PROC ctbCapituloInsert	 
END 
GO
CREATE PROC ctbCapituloInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@TipoCapituloId int,
			@Orden int,
			@CapituloCod varchar(50),
			@CapituloDes varchar(255),
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	CapituloCod 
					FROM	ctbCapitulo 
					WHERE	CapituloCod = @CapituloCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbCapitulo(
						TipoCapituloId,
						Orden,
						CapituloCod,
						CapituloDes,
						EstadoId )
					VALUES (
						@TipoCapituloId,
						@Orden,
						@CapituloCod,
						@CapituloDes,
						@EstadoId )
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Capitulo Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbCapituloUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbCapituloUpdate 
END 
GO
CREATE PROC dbo.ctbCapituloUpdate 
			@UpdateFilter int,
			@CapituloId int, 
			@TipoCapituloId int,
			@Orden int,
			@CapituloCod varchar(50),
			@CapituloDes varchar(255),
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	CapituloId 
				FROM	ctbCapitulo 
				WHERE	CapituloId = @CapituloId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			UPDATE dbo.ctbCapitulo
			SET    TipoCapituloId = @TipoCapituloId,
				   Orden = @Orden,
				   CapituloCod = @CapituloCod,
				   CapituloDes = @CapituloDes,
				   EstadoId = @EstadoId
			WHERE  CapituloId = @CapituloId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Capitulo No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbCapituloDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbCapituloDelete 
END 
GO
CREATE PROC dbo.ctbCapituloDelete 
			@DeleteFilter smallint,
			@CapituloId int
AS 
BEGIN
	IF EXISTS (	SELECT	CapituloId 
				FROM	ctbCapitulo 
				WHERE	CapituloId = @CapituloId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.ctbCapitulo
			WHERE  CapituloId = @CapituloId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Capitulo No Encontrado', 16, 1)
		RETURN
    END 
END
GO








--EXEC ctbCapituloSelect 1, 5, 0, 0, 2

--EXEC ctbCapituloInsert 0, 0, 1, 1, 'a11234', 'yo', '', '23232', '2323','',  1, 'mmercado', '01/01/2017', 1

--EXEC ctbCapituloUpdate 0, 2321, 2, 1, 'a4455', 'yoel', '', '23232', '2323','',  1, 'jmercado', '01/01/2017', 1