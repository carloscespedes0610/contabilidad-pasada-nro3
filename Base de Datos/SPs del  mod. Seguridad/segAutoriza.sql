/********************************************************************/
/*  STORE PROCEDURE	: segAutorizaSelect							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('segAutorizaSelect') IS NOT NULL
BEGIN 
    DROP PROC segAutorizaSelect 
END 
GO
CREATE PROC segAutorizaSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('segAutorizaInsert') IS NOT NULL
BEGIN 
    DROP PROC segAutorizaInsert	 
END 
GO
CREATE PROC segAutorizaInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@EmpresaId int,
			@SucursalId int,
			@AutorizaCod varchar(50),
			@AutorizaDes varchar(255),
			@Responsable varchar(100),
			@Direccion varchar(255),
			@Telefono varchar(50),
			@Fax varchar(50),
			@TipoAutorizaId int,
			@sLastUpdate_id varchar(50) = NULL,
			@dtLastUpdate_dt datetime = NULL,
			@iConcurrency_id smallint = NULL 
AS
BEGIN
	IF NOT EXISTS (	SELECT	AutorizaCod 
					FROM	segAutoriza 
					WHERE	EmpresaId = @EmpresaId 
					AND		AutorizaCod = @AutorizaCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO segAutoriza(EmpresaId, SucursalId, AutorizaCod, AutorizaDes, Responsable, Direccion, Telefono, Fax, TipoAutorizaId, sLastUpdate_id, dtLastUpdate_dt, iConcurrency_id)
			VALUES (@EmpresaId, @SucursalId, @AutorizaCod, @AutorizaDes, @Responsable, @Direccion, @Telefono, @Fax, @TipoAutorizaId, @sLastUpdate_id, @dtLastUpdate_dt, @iConcurrency_id)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Autoriza Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.segAutorizaUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.segAutorizaUpdate 
END 
GO
CREATE PROC dbo.segAutorizaUpdate 
			@UpdateFilter smallint,
			@AutorizaId int,
			@EmpresaId int,
			@SucursalId int,
			@AutorizaCod varchar(50),
			@AutorizaDes varchar(255),
			@Responsable varchar(100),
			@Direccion varchar(255),
			@Telefono varchar(50),
			@Fax varchar(50),
			@TipoAutorizaId int,
			@sLastUpdate_id varchar(50) = NULL,
			@dtLastUpdate_dt datetime = NULL,
			@iConcurrency_id smallint = NULL
AS 
BEGIN
	IF EXISTS (	SELECT	AutorizaId 
				FROM	segAutoriza 
				WHERE	AutorizaId = @AutorizaId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			UPDATE dbo.segAutoriza
			SET    EmpresaId = @EmpresaId, SucursalId = @SucursalId, AutorizaCod = @AutorizaCod, AutorizaDes = @AutorizaDes, Responsable = @Responsable, Direccion = @Direccion, Telefono = @Telefono, Fax = @Fax, TipoAutorizaId = @TipoAutorizaId, sLastUpdate_id = @sLastUpdate_id, dtLastUpdate_dt = @dtLastUpdate_dt, iConcurrency_id = @iConcurrency_id
			WHERE  AutorizaId = @AutorizaId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Autoriza No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.segAutorizaDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.segAutorizaDelete 
END 
GO
CREATE PROC dbo.segAutorizaDelete 
			@DeleteFilter smallint,
			@AutorizaId int
AS 
BEGIN
	IF EXISTS (	SELECT	AutorizaId 
				FROM	segAutoriza 
				WHERE	AutorizaId = @AutorizaId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.segAutoriza
			WHERE  AutorizaId = @AutorizaId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Autoriza No Encontrado', 16, 1)
		RETURN
    END 
END
GO

