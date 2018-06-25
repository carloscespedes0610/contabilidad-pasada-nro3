/********************************************************************/
/*  STORE PROCEDURE	: ctbCapituloSelect							   	    */
/*  AUTOR			: Joel Mercado									*/
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

IF OBJECT_ID('ctbCapituloSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbCapituloSelect 
END 
GO
CREATE PROC ctbCapituloSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@CapituloId int,
			@CapituloCod varchar(50),
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 1)   --PrimaryKey
	BEGIN
		SELECT	ctbCapitulo.CapituloId, 
				ctbCapitulo.TipoCapituloId, 
				ctbCapitulo.Orden, 
				ctbCapitulo.CapituloCod, 
				ctbCapitulo.CapituloDes, 
				ctbCapitulo.EstadoId 
		FROM	ctbCapitulo 
		WHERE	CapituloId = @CapituloId 
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 3)		--Grid
	AND (@OrderByFilter = 3)	--Grid
	BEGIN
		SELECT	ctbCapitulo.CapituloId, 
				ctbCapitulo.TipoCapituloId, 
				ctbCapitulo.Orden, 
				ctbCapitulo.CapituloCod, 
				ctbCapitulo.CapituloDes, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	ctbCapitulo  
		LEFT JOIN	parEstado ON ctbCapitulo.EstadoId = parEstado.EstadoId 
		ORDER BY Orden 
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 6)		--Grid_EstadoId
	AND (@OrderByFilter = 3)	--Grid
	BEGIN
		SELECT	ctbCapitulo.CapituloId, 
				ctbCapitulo.TipoCapituloId, 
				ctbCapitulo.Orden, 
				ctbCapitulo.CapituloCod, 
				ctbCapitulo.CapituloDes, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	ctbCapitulo  
		LEFT JOIN	parEstado ON ctbCapitulo.EstadoId = parEstado.EstadoId 
		WHERE	ctbCapitulo.EstadoId = @EstadoId 
		ORDER BY Orden 
	END

	--ELSE IF (@SelectFilter = 2) --ListBox
	--AND (@WhereFilter = 5)		--EmpresaId
	--AND (@OrderByFilter = 2)	--CapituloDes
	--BEGIN
	--	SELECT	ctbCapitulo.CapituloId, 
	--			ctbCapitulo.CapituloCod, 
	--			ctbCapitulo.CapituloDes 
	--	FROM	ctbCapitulo 
	--	WHERE	ctbCapitulo.TipoCapituloId = @TipoCapituloId
	--	ORDER BY ctbCapitulo.CapituloDes
	--END
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
			@EmpresaId int,
			@SucursalId int,
			@CapituloCod varchar(50),
			@CapituloDes varchar(255),
			@Responsable varchar(100),
			@Direccion varchar(255),
			@Telefono varchar(50),
			@Fax varchar(50),
			@TipoCapituloId int,
			@sLastUpdate_id varchar(50) = NULL,
			@dtLastUpdate_dt datetime = NULL,
			@iConcurrency_id smallint = NULL 
AS
BEGIN
	IF NOT EXISTS (	SELECT	CapituloCod 
					FROM	ctbCapitulo 
					WHERE	EmpresaId = @EmpresaId 
					AND		CapituloCod = @CapituloCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbCapitulo(EmpresaId, SucursalId, CapituloCod, CapituloDes, Responsable, Direccion, Telefono, Fax, TipoCapituloId, sLastUpdate_id, dtLastUpdate_dt, iConcurrency_id)
			VALUES (@EmpresaId, @SucursalId, @CapituloCod, @CapituloDes, @Responsable, @Direccion, @Telefono, @Fax, @TipoCapituloId, @sLastUpdate_id, @dtLastUpdate_dt, @iConcurrency_id)
		
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
			@UpdateFilter smallint,
			@CapituloId int,
			@EmpresaId int,
			@SucursalId int,
			@CapituloCod varchar(50),
			@CapituloDes varchar(255),
			@Responsable varchar(100),
			@Direccion varchar(255),
			@Telefono varchar(50),
			@Fax varchar(50),
			@TipoCapituloId int,
			@sLastUpdate_id varchar(50) = NULL,
			@dtLastUpdate_dt datetime = NULL,
			@iConcurrency_id smallint = NULL
AS 
BEGIN
	IF EXISTS (	SELECT	CapituloId 
				FROM	ctbCapitulo 
				WHERE	CapituloId = @CapituloId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			UPDATE dbo.ctbCapitulo
			SET    EmpresaId = @EmpresaId, SucursalId = @SucursalId, CapituloCod = @CapituloCod, CapituloDes = @CapituloDes, Responsable = @Responsable, Direccion = @Direccion, Telefono = @Telefono, Fax = @Fax, TipoCapituloId = @TipoCapituloId, sLastUpdate_id = @sLastUpdate_id, dtLastUpdate_dt = @dtLastUpdate_dt, iConcurrency_id = @iConcurrency_id
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