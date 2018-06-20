/********************************************************************/
/*  STORE PROCEDURE	: ctbCenCosGrupo							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbCenCosGrupoSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbCenCosGrupoSelect 
END 
GO
CREATE PROC ctbCenCosGrupoSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('ctbCenCosGrupoInsert') IS NOT NULL
BEGIN 
    DROP PROC ctbCenCosGrupoInsert	 
END 
GO
CREATE PROC ctbCenCosGrupoInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@CenCosGrupoCod varchar(50),
			@CenCosGrupoDes varchar(255),
			@CenCosGrupoEsp varchar(255),
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	CenCosGrupoCod 
					FROM	ctbCenCosGrupo 
					WHERE	CenCosGrupoCod = @CenCosGrupoCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbCenCosGrupo(CenCosGrupoCod, 
								CenCosGrupoDes,
								CenCosGrupoEsp,
								EstadoId)
						VALUES (@CenCosGrupoCod, 
								@CenCosGrupoDes,
								@CenCosGrupoEsp,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Grupo de Centro de Costos Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbCenCosGrupoUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbCenCosGrupoUpdate 
END 
GO
CREATE PROC dbo.ctbCenCosGrupoUpdate 
			@UpdateFilter smallint,
			@CenCosGrupoId int,
			@CenCosGrupoCod varchar(50),
			@CenCosGrupoDes varchar(255),
			@CenCosGrupoEsp varchar(255),
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	CenCosGrupoId 
				FROM	ctbCenCosGrupo 
				WHERE	CenCosGrupoId = @CenCosGrupoId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	CenCosGrupoCod 
							FROM	ctbCenCosGrupo 
							WHERE	CenCosGrupoCod = @CenCosGrupoCod 
							AND		CenCosGrupoId <> @CenCosGrupoId)
			BEGIN	
				UPDATE dbo.ctbCenCosGrupo
				SET		CenCosGrupoCod = @CenCosGrupoCod, 
						CenCosGrupoDes = @CenCosGrupoDes, 
						CenCosGrupoEsp = @CenCosGrupoEsp, 
						EstadoId = @EstadoId
				WHERE  CenCosGrupoId = @CenCosGrupoId
			END
			ELSE
			BEGIN
				RAISERROR('Código de Grupo de Centro de Costos Duplicado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Grupo de Centro de Costos No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbCenCosGrupoDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbCenCosGrupoDelete 
END 
GO
CREATE PROC dbo.ctbCenCosGrupoDelete 
			@DeleteFilter smallint,
			@CenCosGrupoId int
AS 
BEGIN
	IF EXISTS (	SELECT	CenCosGrupoId 
				FROM	ctbCenCosGrupo 
				WHERE	CenCosGrupoId = @CenCosGrupoId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.ctbCenCosGrupo
			WHERE  CenCosGrupoId = @CenCosGrupoId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Grupo de Centro de Costos No Encontrado', 16, 1)
		RETURN
    END 
END
GO

