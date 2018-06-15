
IF OBJECT_ID('parTipoInfoSelect') IS NOT NULL
BEGIN 
    DROP PROC parTipoInfoSelect 
END 
GO
CREATE PROC parTipoInfoSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@TipoInfoId int,
			@TipoInfoDes varchar(255),
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 1)   --PrimaryKey
	BEGIN
		SELECT	parTipoInfo.TipoInfoId, 
				parTipoInfo.TipoInfoDes, 
				parTipoInfo.EstadoId 
		FROM	parTipoInfo 
		WHERE	TipoInfoId = @TipoInfoId 
	END	

	ELSE IF (@SelectFilter = 2) --ListBox
	AND (@WhereFilter = 7)		--EstadoId
	AND (@OrderByFilter = 2)	--TipoInfoDes
	BEGIN
		SELECT	parTipoInfo.TipoInfoId, 
				parTipoInfo.TipoInfoDes 
		FROM	parTipoInfo 
		WHERE	parTipoInfo.EstadoId = @EstadoId
		ORDER BY parTipoInfo.TipoInfoDes
	END
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

--IF OBJECT_ID('parTipoInfoInsert') IS NOT NULL
--BEGIN 
--    DROP PROC parTipoInfoInsert	 
--END 
--GO
--CREATE PROC parTipoInfoInsert
--			@InsertFilter smallint,
--			@Id int OUT, 
--			@EmpresaId int,
--			@SucursalId int,
--			@TipoInfoCod varchar(50),
--			@TipoInfoDes varchar(255),
--			@Responsable varchar(100),
--			@Direccion varchar(255),
--			@Telefono varchar(50),
--			@Fax varchar(50),
--			@TipoTipoInfoId int,
--			@sLastUpdate_id varchar(50) = NULL,
--			@dtLastUpdate_dt datetime = NULL,
--			@iConcurrency_id smallint = NULL 
--AS
--BEGIN
--	IF NOT EXISTS (	SELECT	TipoInfoCod 
--					FROM	parTipoInfo 
--					WHERE	EmpresaId = @EmpresaId 
--					AND		TipoInfoCod = @TipoInfoCod) 
--	BEGIN
--		IF @InsertFilter = 0 --All
--		BEGIN
--			INSERT INTO parTipoInfo(EmpresaId, SucursalId, TipoInfoCod, TipoInfoDes, Responsable, Direccion, Telefono, Fax, TipoTipoInfoId, sLastUpdate_id, dtLastUpdate_dt, iConcurrency_id)
--			VALUES (@EmpresaId, @SucursalId, @TipoInfoCod, @TipoInfoDes, @Responsable, @Direccion, @Telefono, @Fax, @TipoTipoInfoId, @sLastUpdate_id, @dtLastUpdate_dt, @iConcurrency_id)
		
--			SET @Id = SCOPE_IDENTITY()
--		END
--	END
--	ELSE
--	BEGIN
--		RAISERROR('Código de TipoInfo Duplicado', 16, 1)
--		RETURN
--    END 
--END
--GO

------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------

--IF OBJECT_ID('dbo.parTipoInfoUpdate') IS NOT NULL
--BEGIN 
--    DROP PROC dbo.parTipoInfoUpdate 
--END 
--GO
--CREATE PROC dbo.parTipoInfoUpdate 
--			@UpdateFilter smallint,
--			@TipoInfoId int,
--			@EmpresaId int,
--			@SucursalId int,
--			@TipoInfoCod varchar(50),
--			@TipoInfoDes varchar(255),
--			@Responsable varchar(100),
--			@Direccion varchar(255),
--			@Telefono varchar(50),
--			@Fax varchar(50),
--			@TipoTipoInfoId int,
--			@sLastUpdate_id varchar(50) = NULL,
--			@dtLastUpdate_dt datetime = NULL,
--			@iConcurrency_id smallint = NULL
--AS 
--BEGIN
--	IF EXISTS (	SELECT	TipoInfoId 
--				FROM	parTipoInfo 
--				WHERE	TipoInfoId = @TipoInfoId) 
--	BEGIN
--		IF @UpdateFilter = 0 --All
--		BEGIN
--			UPDATE dbo.parTipoInfo
--			SET    EmpresaId = @EmpresaId, SucursalId = @SucursalId, TipoInfoCod = @TipoInfoCod, TipoInfoDes = @TipoInfoDes, Responsable = @Responsable, Direccion = @Direccion, Telefono = @Telefono, Fax = @Fax, TipoTipoInfoId = @TipoTipoInfoId, sLastUpdate_id = @sLastUpdate_id, dtLastUpdate_dt = @dtLastUpdate_dt, iConcurrency_id = @iConcurrency_id
--			WHERE  TipoInfoId = @TipoInfoId
--		END
--	END
--	ELSE
--	BEGIN
--		RAISERROR('ID de TipoInfo No Encontrado', 16, 1)
--		RETURN
--    END 
--END
--GO

------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------

--IF OBJECT_ID('dbo.parTipoInfoDelete') IS NOT NULL
--BEGIN 
--    DROP PROC dbo.parTipoInfoDelete 
--END 
--GO
--CREATE PROC dbo.parTipoInfoDelete 
--			@DeleteFilter smallint,
--			@TipoInfoId int
--AS 
--BEGIN
--	IF EXISTS (	SELECT	TipoInfoId 
--				FROM	parTipoInfo 
--				WHERE	TipoInfoId = @TipoInfoId) 
--	BEGIN
--		IF @DeleteFilter = 0 --All
--		BEGIN
--			DELETE
--			FROM   dbo.parTipoInfo
--			WHERE  TipoInfoId = @TipoInfoId
--		END
--	END
--	ELSE
--	BEGIN
--		RAISERROR('ID de TipoInfo No Encontrado', 16, 1)
--		RETURN
--    END 
--END
--GO








--EXEC parTipoInfoSelect 1, 5, 0, 0, 2

--EXEC parTipoInfoInsert 0, 0, 1, 1, 'a11234', 'yo', '', '23232', '2323','',  1, 'mmercado', '01/01/2017', 1

--EXEC parTipoInfoUpdate 0, 2321, 2, 1, 'a4455', 'yoel', '', '23232', '2323','',  1, 'jmercado', '01/01/2017', 1