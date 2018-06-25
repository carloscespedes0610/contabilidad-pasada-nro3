/********************************************************************/
/*  STORE PROCEDURE	: segAplicacionSelect							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('segAplicacionSelect') IS NOT NULL
BEGIN 
    DROP PROC segAplicacionSelect 
END 
GO
CREATE PROC segAplicacionSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('segAplicacionInsert') IS NOT NULL
BEGIN 
    DROP PROC segAplicacionInsert	 
END 
GO
CREATE PROC segAplicacionInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@EmpresaId int,
			@SucursalId int,
			@AplicacionCod varchar(50),
			@AplicacionDes varchar(255),
			@Responsable varchar(100),
			@Direccion varchar(255),
			@Telefono varchar(50),
			@Fax varchar(50),
			@TipoAplicacionId int,
			@sLastUpdate_id varchar(50) = NULL,
			@dtLastUpdate_dt datetime = NULL,
			@iConcurrency_id smallint = NULL 
AS
BEGIN
	IF NOT EXISTS (	SELECT	AplicacionCod 
					FROM	segAplicacion 
					WHERE	EmpresaId = @EmpresaId 
					AND		AplicacionCod = @AplicacionCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO segAplicacion(EmpresaId, SucursalId, AplicacionCod, AplicacionDes, Responsable, Direccion, Telefono, Fax, TipoAplicacionId, sLastUpdate_id, dtLastUpdate_dt, iConcurrency_id)
			VALUES (@EmpresaId, @SucursalId, @AplicacionCod, @AplicacionDes, @Responsable, @Direccion, @Telefono, @Fax, @TipoAplicacionId, @sLastUpdate_id, @dtLastUpdate_dt, @iConcurrency_id)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Aplicacion Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.segAplicacionUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.segAplicacionUpdate 
END 
GO
CREATE PROC dbo.segAplicacionUpdate 
			@UpdateFilter smallint,
			@AplicacionId int,
			@EmpresaId int,
			@SucursalId int,
			@AplicacionCod varchar(50),
			@AplicacionDes varchar(255),
			@Responsable varchar(100),
			@Direccion varchar(255),
			@Telefono varchar(50),
			@Fax varchar(50),
			@TipoAplicacionId int,
			@sLastUpdate_id varchar(50) = NULL,
			@dtLastUpdate_dt datetime = NULL,
			@iConcurrency_id smallint = NULL
AS 
BEGIN
	IF EXISTS (	SELECT	AplicacionId 
				FROM	segAplicacion 
				WHERE	AplicacionId = @AplicacionId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			UPDATE dbo.segAplicacion
			SET    EmpresaId = @EmpresaId, SucursalId = @SucursalId, AplicacionCod = @AplicacionCod, AplicacionDes = @AplicacionDes, Responsable = @Responsable, Direccion = @Direccion, Telefono = @Telefono, Fax = @Fax, TipoAplicacionId = @TipoAplicacionId, sLastUpdate_id = @sLastUpdate_id, dtLastUpdate_dt = @dtLastUpdate_dt, iConcurrency_id = @iConcurrency_id
			WHERE  AplicacionId = @AplicacionId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Aplicacion No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.segAplicacionDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.segAplicacionDelete 
END 
GO
CREATE PROC dbo.segAplicacionDelete 
			@DeleteFilter smallint,
			@AplicacionId int
AS 
BEGIN
	IF EXISTS (	SELECT	AplicacionId 
				FROM	segAplicacion 
				WHERE	AplicacionId = @AplicacionId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.segAplicacion
			WHERE  AplicacionId = @AplicacionId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Aplicacion No Encontrado', 16, 1)
		RETURN
    END 
END
GO

