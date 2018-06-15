/********************************************************************/
/*  STORE PROCEDURE	: segTipoUsuarioSelect							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('segTipoUsuarioSelect') IS NOT NULL
BEGIN 
    DROP PROC segTipoUsuarioSelect 
END 
GO
CREATE PROC segTipoUsuarioSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('segTipoUsuarioInsert') IS NOT NULL
BEGIN 
    DROP PROC segTipoUsuarioInsert	 
END 
GO
CREATE PROC segTipoUsuarioInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@TipoUsuarioCod varchar(50),
			@TipoUsuarioDes varchar(255),
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	TipoUsuarioCod 
					FROM	segTipoUsuario 
					WHERE	TipoUsuarioCod = @TipoUsuarioCod) 
	BEGIN
		IF @InsertFilter = 0	--All
		BEGIN
			INSERT INTO segTipoUsuario( TipoUsuarioCod, 
										TipoUsuarioDes, 
										EstadoId)
								VALUES (@TipoUsuarioCod, 
										@TipoUsuarioDes, 
										@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Tipo Usuario Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('segTipoUsuarioUpdate') IS NOT NULL
BEGIN 
    DROP PROC segTipoUsuarioUpdate 
END 
GO
CREATE PROC segTipoUsuarioUpdate 
			@UpdateFilter smallint,
			@TipoUsuarioId int,
			@TipoUsuarioCod varchar(50),
			@TipoUsuarioDes varchar(255),
			@EstadoId int
AS 
BEGIN
	DECLARE @Id int 
	SET @Id = 0

	SELECT	@Id = TipoUsuarioId 
	FROM	segTipoUsuario 
	WHERE	TipoUsuarioId = @TipoUsuarioId 
	
	IF (@Id > 0)
	BEGIN
		IF @UpdateFilter = 0	--All
		BEGIN			
			IF NOT EXISTS (	SELECT	TipoUsuarioCod 
							FROM	segTipoUsuario 
							WHERE	TipoUsuarioCod = @TipoUsuarioCod 
							AND		TipoUsuarioId <> @TipoUsuarioId)
			BEGIN	
				UPDATE	segTipoUsuario
				SET		TipoUsuarioCod = @TipoUsuarioCod, 
						TipoUsuarioDes = @TipoUsuarioDes, 
						EstadoId = @EstadoId 
				WHERE	TipoUsuarioId = @TipoUsuarioId
			END
			ELSE
			BEGIN
				RAISERROR('Código de Tipo Usuario Duplicado', 16, 1)
				RETURN
			END 
		END
		ELSE IF @UpdateFilter = 1	--Otro1
		BEGIN
			
			UPDATE	segTipoUsuario
			SET		EstadoId = @EstadoId 
			WHERE	TipoUsuarioId = @TipoUsuarioId
		END
		ELSE IF @UpdateFilter = 2	--Otro2
		BEGIN
			UPDATE	segTipoUsuario
			SET		TipoUsuarioDes = @TipoUsuarioDes 					
			WHERE	TipoUsuarioId = @TipoUsuarioId
		END
		 
	END
	ELSE
	BEGIN
		RAISERROR('ID de TipoUsuario No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('segTipoUsuarioDelete') IS NOT NULL
BEGIN 
    DROP PROC segTipoUsuarioDelete 
END 
GO
CREATE PROC segTipoUsuarioDelete 
			@DeleteFilter smallint,
			@TipoUsuarioId int
AS 
BEGIN
	IF EXISTS (	SELECT	TipoUsuarioId 
				FROM	segTipoUsuario 
				WHERE	TipoUsuarioId = @TipoUsuarioId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   segTipoUsuario
			WHERE  TipoUsuarioId = @TipoUsuarioId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de TipoUsuario No Encontrado', 16, 1)
		RETURN
    END 
END
GO

