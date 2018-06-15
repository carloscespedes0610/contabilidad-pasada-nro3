/********************************************************************/
/*  STORE PROCEDURE	: ctbSucursal							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbSucursalSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbSucursalSelect 
END 
GO
CREATE PROC ctbSucursalSelect 
			@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('ctbSucursalInsert') IS NOT NULL
BEGIN 
    DROP PROC ctbSucursalInsert	 
END 
GO
CREATE PROC ctbSucursalInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@SucursalCod varchar(50),
			@SucursalDes varchar(255),
			@SucursalEsp varchar(255),
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	SucursalCod 
					FROM	ctbSucursal 
					WHERE	SucursalCod = @SucursalCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbSucursal(SucursalCod, 
								SucursalDes,
								SucursalEsp,
								EstadoId)
						VALUES (@SucursalCod, 
								@SucursalDes,
								@SucursalEsp,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Sucursal Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbSucursalUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbSucursalUpdate 
END 
GO
CREATE PROC dbo.ctbSucursalUpdate 
			@UpdateFilter smallint,
			@SucursalId int,
			@SucursalCod varchar(50),
			@SucursalDes varchar(255),
			@SucursalEsp varchar(255),
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	SucursalId 
				FROM	ctbSucursal 
				WHERE	SucursalId = @SucursalId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	SucursalCod 
							FROM	ctbSucursal 
							WHERE	SucursalCod = @SucursalCod 
							AND		SucursalId <> @SucursalId)
			BEGIN	
				UPDATE dbo.ctbSucursal
				SET		SucursalCod = @SucursalCod, 
						SucursalDes = @SucursalDes, 
						SucursalEsp = @SucursalEsp, 
						EstadoId = @EstadoId
				WHERE  SucursalId = @SucursalId
			END
			ELSE
			BEGIN
				RAISERROR('Código de Sucursal Duplicado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Sucursal No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbSucursalDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbSucursalDelete 
END 
GO
CREATE PROC dbo.ctbSucursalDelete 
			@DeleteFilter smallint,
			@SucursalId int
AS 
BEGIN
	IF EXISTS (	SELECT	SucursalId 
				FROM	ctbSucursal 
				WHERE	SucursalId = @SucursalId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.ctbSucursal
			WHERE  SucursalId = @SucursalId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Sucursal No Encontrado', 16, 1)
		RETURN
    END 
END
GO

