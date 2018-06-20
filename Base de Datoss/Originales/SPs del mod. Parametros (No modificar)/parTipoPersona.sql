/********************************************************************/
/*  STORE PROCEDURE	: parTipoPersona							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('parTipoPersonaSelect') IS NOT NULL
BEGIN 
    DROP PROC parTipoPersonaSelect 
END 
GO
CREATE PROC parTipoPersonaSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@TipoPersonaId int,
			@TipoPersonaCod varchar(50),
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 1)   --PrimaryKey
	BEGIN
		SELECT	parTipoPersona.TipoPersonaId, 
				parTipoPersona.TipoPersonaCod, 
				parTipoPersona.TipoPersonaDes, 
				parTipoPersona.TipoRelacionId, 
				parTipoPersona.EstadoId 
		FROM	parTipoPersona 
		WHERE	TipoPersonaId = @TipoPersonaId 
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 3)		--Grid
	AND (@OrderByFilter = 3)	--Grid
	BEGIN
		SELECT	parTipoPersona.TipoPersonaId, 
				parTipoPersona.TipoPersonaCod, 
				parTipoPersona.TipoPersonaDes, 
				parTipoRelacion.TipoRelacionId, 
				parTipoRelacion.TipoRelacionDes, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	parTipoPersona  
		LEFT JOIN	parTipoRelacion	ON parTipoPersona.TipoRelacionId = parTipoRelacion.TipoRelacionId				 
		LEFT JOIN	parEstado		ON parTipoPersona.EstadoId = parEstado.EstadoId 
		ORDER BY parTipoRelacion.TipoRelacionDes, parTipoPersona.TipoPersonaDes 
	END

	ELSE IF (@SelectFilter = 2) --ListBox
	AND (@OrderByFilter = 2)	--TipoPersonaDes
	BEGIN
		SELECT	parTipoPersona.TipoPersonaId, 
				parTipoPersona.TipoPersonaCod, 
				parTipoPersona.TipoPersonaDes 
		FROM	parTipoPersona 
		ORDER BY parTipoPersona.TipoPersonaDes
	END

	ELSE IF  (@SelectFilter = 0 )	--All
	AND (@WhereFilter = 5)			--TipoPersonaCod
	BEGIN
		SELECT	parTipoPersona.TipoPersonaId, 
				parTipoPersona.TipoPersonaCod, 
				parTipoPersona.TipoPersonaDes, 
				parTipoPersona.TipoRelacionId, 
				parTipoPersona.EstadoId 
		FROM	parTipoPersona 
		WHERE	TipoPersonaCod = @TipoPersonaCod 
	END
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('parTipoPersonaInsert') IS NOT NULL
BEGIN 
    DROP PROC parTipoPersonaInsert	 
END 
GO
CREATE PROC parTipoPersonaInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@TipoPersonaCod varchar(50),
			@TipoPersonaDes varchar(255),
			@TipoRelacionId int,
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	TipoPersonaCod 
					FROM	parTipoPersona 
					WHERE	TipoPersonaCod = @TipoPersonaCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO parTipoPersona(TipoPersonaCod, 
								TipoPersonaDes,
								TipoRelacionId,
								EstadoId)
						VALUES (@TipoPersonaCod, 
								@TipoPersonaDes,
								@TipoRelacionId,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Tipo Persona Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.parTipoPersonaUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.parTipoPersonaUpdate 
END 
GO
CREATE PROC dbo.parTipoPersonaUpdate 
			@UpdateFilter smallint,
			@TipoPersonaId int,
			@TipoPersonaCod varchar(50),
			@TipoPersonaDes varchar(255),
			@TipoRelacionId int,
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	TipoPersonaId 
				FROM	parTipoPersona 
				WHERE	TipoPersonaId = @TipoPersonaId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	TipoPersonaCod 
							FROM	parTipoPersona 
							WHERE	TipoPersonaCod = @TipoPersonaCod 
							AND		TipoPersonaId <> @TipoPersonaId)
			BEGIN	
				UPDATE dbo.parTipoPersona
				SET		TipoPersonaCod = @TipoPersonaCod, 
						TipoPersonaDes = @TipoPersonaDes, 
						TipoRelacionId = @TipoRelacionId, 
						EstadoId = @EstadoId
				WHERE  TipoPersonaId = @TipoPersonaId
			END
			ELSE
			BEGIN
				RAISERROR('Código de Tipo Persona Duplicado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Tipo Persona No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.parTipoPersonaDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.parTipoPersonaDelete 
END 
GO
CREATE PROC dbo.parTipoPersonaDelete 
			@DeleteFilter smallint,
			@TipoPersonaId int
AS 
BEGIN
	IF EXISTS (	SELECT	TipoPersonaId 
				FROM	parTipoPersona 
				WHERE	TipoPersonaId = @TipoPersonaId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.parTipoPersona
			WHERE  TipoPersonaId = @TipoPersonaId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Tipo Persona No Encontrado', 16, 1)
		RETURN
    END 
END
GO

