
IF OBJECT_ID('segTipoUsuarioSelect') IS NOT NULL
BEGIN 
    DROP PROC segTipoUsuarioSelect 
END 
GO
CREATE PROC segTipoUsuarioSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@TipoUsuarioId int,
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0 )	--All
	AND (@WhereFilter = 1)		--PrimaryKey
	BEGIN
		SELECT	segTipoUsuario.TipoUsuarioId, 
				segTipoUsuario.TipoUsuarioCod, 
				segTipoUsuario.TipoUsuarioDes, 
				segTipoUsuario.EstadoId, 
				segTipoUsuario.UsuarioCodUlt, 
				segTipoUsuario.FechaModUlt, 
				segTipoUsuario.ConcurUlt 
		FROM	segTipoUsuario 
		WHERE	TipoUsuarioId = @TipoUsuarioId 
	END

	ELSE IF (@SelectFilter = 3)		--Grid
	AND (@WhereFilter = 3)			--Grid
	AND (@OrderByFilter = 3)		--Grid
	BEGIN
		SELECT	segTipoUsuario.TipoUsuarioId, 
				segTipoUsuario.TipoUsuarioCod, 
				segTipoUsuario.TipoUsuarioDes, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	segTipoUsuario, parEstado 
		WHERE	segTipoUsuario.EstadoId = parEstado.EstadoId 
		ORDER BY segTipoUsuario.TipoUsuarioDes 
	END

	ELSE IF (@SelectFilter = 2)		--ListBox
		AND (@WhereFilter = 5)		--EstadoId
		AND (@OrderByFilter = 2)	--TipoUsuarioDes
	BEGIN
		SELECT	segTipoUsuario.TipoUsuarioId, 
				segTipoUsuario.TipoUsuarioCod, 
				segTipoUsuario.TipoUsuarioDes 
		FROM	segTipoUsuario 
		WHERE	segTipoUsuario.EstadoId = @EstadoId
		ORDER BY segTipoUsuario.TipoUsuarioDes
	END

	ELSE IF (@SelectFilter = 3)		--Grid
		AND (@WhereFilter = 6)		--GridTipoUsuarioId
		AND (@OrderByFilter = 0)	--All
	BEGIN
		SELECT	segTipoUsuario.TipoUsuarioId, 
				segTipoUsuario.TipoUsuarioCod, 
				segTipoUsuario.TipoUsuarioDes, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	segTipoUsuario, parEstado  
		WHERE	segTipoUsuario.TipoUsuarioId = @TipoUsuarioId 
		AND		segTipoUsuario.EstadoId = parEstado.EstadoId 
	END
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
			@EstadoId int,
			@UsuarioCodUlt varchar(50) = NULL,
			@FechaModUlt datetime = NULL,
			@ConcurUlt smallint = NULL 
AS
BEGIN
	IF NOT EXISTS (	SELECT	TipoUsuarioCod 
					FROM	segTipoUsuario 
					WHERE	TipoUsuarioCod = @TipoUsuarioCod) 
	BEGIN
		IF @InsertFilter = 0	--All
		BEGIN
			INSERT INTO segTipoUsuario(TipoUsuarioCod, 
										TipoUsuarioDes, 
										EstadoId, 
										UsuarioCodUlt, 
										FechaModUlt, 
										ConcurUlt)
								VALUES (@TipoUsuarioCod, 
										@TipoUsuarioDes, 
										@EstadoId, 
										@UsuarioCodUlt, 
										@FechaModUlt, 
										@ConcurUlt)
		
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
create unique nonclustered index "TipoUsuarioCod2" on segTipoUsuario (TipoUsuarioCod,EstadoId) WHERE EstadoId <>  0
 

EXEC segTipoUsuarioUpdate 0, 5, '001', 'yoel',  101, 'jmercado', '01/01/2017', 0


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
			@EstadoId int,
			@UsuarioCodUlt varchar(50) = NULL,
			@FechaModUlt datetime = NULL,
			@ConcurUlt smallint = NULL 
AS 
BEGIN
	DECLARE @Id int 
	SET @Id = 0

	SELECT	@Id = TipoUsuarioId, 
			@ConcurUlt = ConcurUlt 
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
						EstadoId = @EstadoId, 
						UsuarioCodUlt = @UsuarioCodUlt, 
						FechaModUlt = @FechaModUlt, 
						ConcurUlt = @ConcurUlt + 1 
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
			SET		EstadoId = @EstadoId, 
					UsuarioCodUlt = @UsuarioCodUlt, 
					FechaModUlt = @FechaModUlt, 
					ConcurUlt = @ConcurUlt + 1 
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
GO[MAY]








EXEC segTipoUsuarioSelect 3, 6, 0, 2, 0

--EXEC segTipoUsuarioInsert 0, 0, 1, 1, 'a11234', 'yo', '', '23232', '2323','',  1, 'mmercado', '01/01/2017', 1

EXEC segTipoUsuarioUpdate 0 2321, 2, 1, 'a4455', 'yoel', '', '23232', '2323','',  1, 'jmercado', '01/01/2017', 1

create unique nonclustered index "NombreIndex" on Tabla (cod,id2)