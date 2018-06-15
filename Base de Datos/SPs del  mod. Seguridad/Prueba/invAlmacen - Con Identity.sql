
IF OBJECT_ID('invAlmacenSelect') IS NOT NULL
BEGIN 
    DROP PROC invAlmacenSelect 
END 
GO
CREATE PROC invAlmacenSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@AlmacenId int,
			@EmpresaId int 
AS
BEGIN
	IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 1)   --PrimaryKey
	BEGIN
		SELECT	invAlmacen.AlmacenId, 
				invAlmacen.EmpresaId, 
				invAlmacen.SucursalId, 
				invAlmacen.AlmacenCod, 
				invAlmacen.AlmacenDes, 
				invAlmacen.Responsable, 
				invAlmacen.Direccion, 
				invAlmacen.Telefono, 
				invAlmacen.Fax, 
				invAlmacen.sLastUpdate_id, 
				invAlmacen.dtLastUpdate_dt, 
				invAlmacen.iConcurrency_id, 
				invAlmacen.TipoAlmacenId 
		FROM	invAlmacen 
		WHERE	AlmacenId = @AlmacenId 
	END

	ELSE IF (@SelectFilter = 1) --RowCount
	AND (@WhereFilter = 5)		--EmpresaId
	BEGIN
		SELECT	* 
		FROM	invAlmacen 
		WHERE	invAlmacen.EmpresaId = @EmpresaId 
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 3)		--Grid
	AND (@OrderByFilter = 3)	--Grid
	BEGIN
		SELECT	invAlmacen.AlmacenId, 
				invAlmacen.EmpresaId, 
				tblSucursal.SucursalId, 
				tblSucursal.SucursalDes, 
				invAlmacen.AlmacenCod, 
				invAlmacen.AlmacenDes, 
				invAlmacen.Responsable, 
				invAlmacen.Direccion, 
				invAlmacen.Telefono, 
				invAlmacen.Fax,
				invTipoAlmacen.TipoAlmacenId,  
				invTipoAlmacen.TipoAlmacenDes  
		FROM	invAlmacen, tblSucursal, invTipoAlmacen  
		WHERE	invAlmacen.EmpresaId = @EmpresaId 
		AND		invAlmacen.SucursalId = tblSucursal.SucursalId 
		AND  invAlmacen.TipoAlmacenId = invTipoAlmacen.TipoAlmacenId 
		ORDER BY tblSucursal.SucursalDes, invAlmacen.AlmacenDes 
	END

	ELSE IF (@SelectFilter = 2) --ListBox
	AND (@WhereFilter = 5)		--EmpresaId
	AND (@OrderByFilter = 2)	--AlmacenDes
	BEGIN
		SELECT	invAlmacen.AlmacenId, 
				invAlmacen.AlmacenCod, 
				invAlmacen.AlmacenDes 
		FROM	invAlmacen 
		WHERE	invAlmacen.EmpresaId = @EmpresaId 
		ORDER BY invAlmacen.AlmacenDes
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 7)		--GridAlmacenId
	BEGIN
		SELECT	invAlmacen.AlmacenId, 
				invAlmacen.EmpresaId, 
				tblSucursal.SucursalId, 
				tblSucursal.SucursalDes, 
				invAlmacen.AlmacenCod, 
				invAlmacen.AlmacenDes, 
				invAlmacen.Responsable, 
				invAlmacen.Direccion, 
				invAlmacen.Telefono, 
				invAlmacen.Fax,
				invTipoAlmacen.TipoAlmacenId,  
				invTipoAlmacen.TipoAlmacenDes  
		FROM	invAlmacen, tblSucursal, invTipoAlmacen  
		WHERE	invAlmacen.AlmacenId = @AlmacenId 
		AND		invAlmacen.SucursalId = tblSucursal.SucursalId 
		AND  invAlmacen.TipoAlmacenId = invTipoAlmacen.TipoAlmacenId 
	END
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('invAlmacenInsert') IS NOT NULL
BEGIN 
    DROP PROC invAlmacenInsert	 
END 
GO
CREATE PROC invAlmacenInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@EmpresaId int,
			@SucursalId int,
			@AlmacenCod varchar(50),
			@AlmacenDes varchar(255),
			@Responsable varchar(100),
			@Direccion varchar(255),
			@Telefono varchar(50),
			@Fax varchar(50),
			@TipoAlmacenId int,
			@sLastUpdate_id varchar(50) = NULL,
			@dtLastUpdate_dt datetime = NULL,
			@iConcurrency_id smallint = NULL 
AS
BEGIN
	IF NOT EXISTS (	SELECT	AlmacenCod 
					FROM	invAlmacen 
					WHERE	EmpresaId = @EmpresaId 
					AND		AlmacenCod = @AlmacenCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO invAlmacen(EmpresaId, SucursalId, AlmacenCod, AlmacenDes, Responsable, Direccion, Telefono, Fax, TipoAlmacenId, sLastUpdate_id, dtLastUpdate_dt, iConcurrency_id)
			VALUES (@EmpresaId, @SucursalId, @AlmacenCod, @AlmacenDes, @Responsable, @Direccion, @Telefono, @Fax, @TipoAlmacenId, @sLastUpdate_id, @dtLastUpdate_dt, @iConcurrency_id)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Almacen Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.invAlmacenUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.invAlmacenUpdate 
END 
GO
CREATE PROC dbo.invAlmacenUpdate 
			@UpdateFilter smallint,
			@AlmacenId int,
			@EmpresaId int,
			@SucursalId int,
			@AlmacenCod varchar(50),
			@AlmacenDes varchar(255),
			@Responsable varchar(100),
			@Direccion varchar(255),
			@Telefono varchar(50),
			@Fax varchar(50),
			@TipoAlmacenId int,
			@sLastUpdate_id varchar(50) = NULL,
			@dtLastUpdate_dt datetime = NULL,
			@iConcurrency_id smallint = NULL
AS 
BEGIN
	IF EXISTS (	SELECT	AlmacenId 
				FROM	invAlmacen 
				WHERE	AlmacenId = @AlmacenId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			UPDATE dbo.invAlmacen
			SET    EmpresaId = @EmpresaId, SucursalId = @SucursalId, AlmacenCod = @AlmacenCod, AlmacenDes = @AlmacenDes, Responsable = @Responsable, Direccion = @Direccion, Telefono = @Telefono, Fax = @Fax, TipoAlmacenId = @TipoAlmacenId, sLastUpdate_id = @sLastUpdate_id, dtLastUpdate_dt = @dtLastUpdate_dt, iConcurrency_id = @iConcurrency_id
			WHERE  AlmacenId = @AlmacenId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Almacen No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.invAlmacenDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.invAlmacenDelete 
END 
GO
CREATE PROC dbo.invAlmacenDelete 
			@DeleteFilter smallint,
			@AlmacenId int
AS 
BEGIN
	IF EXISTS (	SELECT	AlmacenId 
				FROM	invAlmacen 
				WHERE	AlmacenId = @AlmacenId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.invAlmacen
			WHERE  AlmacenId = @AlmacenId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Almacen No Encontrado', 16, 1)
		RETURN
    END 
END
GO








--EXEC invAlmacenSelect 1, 5, 0, 0, 2

--EXEC invAlmacenInsert 0, 0, 1, 1, 'a11234', 'yo', '', '23232', '2323','',  1, 'mmercado', '01/01/2017', 1

--EXEC invAlmacenUpdate 0, 2321, 2, 1, 'a4455', 'yoel', '', '23232', '2323','',  1, 'jmercado', '01/01/2017', 1