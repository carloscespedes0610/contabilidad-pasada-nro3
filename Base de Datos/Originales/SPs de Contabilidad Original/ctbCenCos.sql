/********************************************************************/
/*  STORE PROCEDURE	: ctbCenCos							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbCenCosSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbCenCosSelect 
END 
GO
CREATE PROC ctbCenCosSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('ctbCenCosInsert') IS NOT NULL
BEGIN 
    DROP PROC ctbCenCosInsert	 
END 
GO
CREATE PROC ctbCenCosInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@CenCosCod varchar(50),
			@CenCosDes varchar(255),
			@CenCosEsp varchar(255),
			@CenCosGrupoId int,
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	CenCosCod 
					FROM	ctbCenCos 
					WHERE	CenCosCod = @CenCosCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbCenCos(CenCosCod, 
								CenCosDes,
								CenCosEsp,
								CenCosGrupoId,
								EstadoId)
						VALUES (@CenCosCod, 
								@CenCosDes,
								@CenCosEsp,
								@CenCosGrupoId,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Centro de Costo Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbCenCosUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbCenCosUpdate 
END 
GO
CREATE PROC dbo.ctbCenCosUpdate 
			@UpdateFilter smallint,
			@CenCosId int,
			@CenCosCod varchar(50),
			@CenCosDes varchar(255),
			@CenCosEsp varchar(255),
			@CenCosGrupoId int,
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	CenCosId 
				FROM	ctbCenCos 
				WHERE	CenCosId = @CenCosId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	CenCosCod 
							FROM	ctbCenCos 
							WHERE	CenCosCod = @CenCosCod 
							AND		CenCosId <> @CenCosId)
			BEGIN	
				UPDATE dbo.ctbCenCos
				SET		CenCosCod = @CenCosCod, 
						CenCosDes = @CenCosDes, 
						CenCosEsp = @CenCosEsp, 
						CenCosGrupoId = @CenCosGrupoId, 
						EstadoId = @EstadoId
				WHERE  CenCosId = @CenCosId
			END
			ELSE
			BEGIN
				RAISERROR('Código de Centro de Costo Duplicado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Centro de Costo No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbCenCosDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbCenCosDelete 
END 
GO
CREATE PROC dbo.ctbCenCosDelete 
			@DeleteFilter smallint,
			@CenCosId int
AS 
BEGIN
	IF EXISTS (	SELECT	CenCosId 
				FROM	ctbCenCos 
				WHERE	CenCosId = @CenCosId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.ctbCenCos
			WHERE  CenCosId = @CenCosId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Centro de Costo No Encontrado', 16, 1)
		RETURN
    END 
END
GO

