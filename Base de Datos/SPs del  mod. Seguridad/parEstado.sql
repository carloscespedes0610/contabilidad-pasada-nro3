/********************************************************************/
/*  STORE PROCEDURE	: parEstadoSelect							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('parEstadoSelect') IS NOT NULL
BEGIN 
    DROP PROC parEstadoSelect 
END 
GO
CREATE PROC parEstadoSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('parEstadoInsert') IS NOT NULL
BEGIN 
    DROP PROC parEstadoInsert	 
END 
GO
CREATE PROC parEstadoInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@EmpresaId int,
			@SucursalId int,
			@EstadoCod varchar(50),
			@EstadoDes varchar(255),
			@Responsable varchar(100),
			@Direccion varchar(255),
			@Telefono varchar(50),
			@Fax varchar(50),
			@TipoEstadoId int,
			@sLastUpdate_id varchar(50) = NULL,
			@dtLastUpdate_dt datetime = NULL,
			@iConcurrency_id smallint = NULL 
AS
BEGIN
	IF NOT EXISTS (	SELECT	EstadoCod 
					FROM	parEstado 
					WHERE	EmpresaId = @EmpresaId 
					AND		EstadoCod = @EstadoCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO parEstado(EmpresaId, SucursalId, EstadoCod, EstadoDes, Responsable, Direccion, Telefono, Fax, TipoEstadoId, sLastUpdate_id, dtLastUpdate_dt, iConcurrency_id)
			VALUES (@EmpresaId, @SucursalId, @EstadoCod, @EstadoDes, @Responsable, @Direccion, @Telefono, @Fax, @TipoEstadoId, @sLastUpdate_id, @dtLastUpdate_dt, @iConcurrency_id)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Estado Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.parEstadoUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.parEstadoUpdate 
END 
GO
CREATE PROC dbo.parEstadoUpdate 
			@UpdateFilter smallint,
			@EstadoId int,
			@EmpresaId int,
			@SucursalId int,
			@EstadoCod varchar(50),
			@EstadoDes varchar(255),
			@Responsable varchar(100),
			@Direccion varchar(255),
			@Telefono varchar(50),
			@Fax varchar(50),
			@TipoEstadoId int,
			@sLastUpdate_id varchar(50) = NULL,
			@dtLastUpdate_dt datetime = NULL,
			@iConcurrency_id smallint = NULL
AS 
BEGIN
	IF EXISTS (	SELECT	EstadoId 
				FROM	parEstado 
				WHERE	EstadoId = @EstadoId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			UPDATE dbo.parEstado
			SET    EmpresaId = @EmpresaId, SucursalId = @SucursalId, EstadoCod = @EstadoCod, EstadoDes = @EstadoDes, Responsable = @Responsable, Direccion = @Direccion, Telefono = @Telefono, Fax = @Fax, TipoEstadoId = @TipoEstadoId, sLastUpdate_id = @sLastUpdate_id, dtLastUpdate_dt = @dtLastUpdate_dt, iConcurrency_id = @iConcurrency_id
			WHERE  EstadoId = @EstadoId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Estado No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.parEstadoDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.parEstadoDelete 
END 
GO
CREATE PROC dbo.parEstadoDelete 
			@DeleteFilter smallint,
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	EstadoId 
				FROM	parEstado 
				WHERE	EstadoId = @EstadoId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.parEstado
			WHERE  EstadoId = @EstadoId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Estado No Encontrado', 16, 1)
		RETURN
    END 
END
GO

