/********************************************************************/
/*  STORE PROCEDURE	: ctbRegTipoPersona							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  MODIFICACION	: Carlos Cespedes (15/06/2018)					*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbRegTipoPersonaSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbRegTipoPersonaSelect 
END 
GO
CREATE PROC ctbRegTipoPersonaSelect 
			@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('ctbRegTipoPersonaInsert') IS NOT NULL
BEGIN 
    DROP PROC ctbRegTipoPersonaInsert	 
END 
GO
CREATE PROC ctbRegTipoPersonaInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@TipoPersonaId int,
			@PlanGrupoId int,
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	TipoPersonaId 
					FROM	ctbRegTipoPersona 
					WHERE	TipoPersonaId = @TipoPersonaId 
					AND		PlanGrupoId = @PlanGrupoId) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbRegTipoPersona(TipoPersonaId, 
								PlanGrupoId,
								EstadoId)
						VALUES (@TipoPersonaId, 
								@PlanGrupoId,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Registro a Grupo de Cuentas Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbRegTipoPersonaUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbRegTipoPersonaUpdate 
END 
GO
CREATE PROC dbo.ctbRegTipoPersonaUpdate 
			@UpdateFilter smallint,
			@RegTipoPersonaId int,
			@TipoPersonaId int,
			@PlanGrupoId int,
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	RegTipoPersonaId 
				FROM	ctbRegTipoPersona 
				WHERE	RegTipoPersonaId = @RegTipoPersonaId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	TipoPersonaId 
							FROM	ctbRegTipoPersona 
							WHERE	TipoPersonaId = @TipoPersonaId 
							AND		PlanGrupoId = @PlanGrupoId 
							AND		RegTipoPersonaId <> @RegTipoPersonaId)
			BEGIN	
				UPDATE dbo.ctbRegTipoPersona
				SET		TipoPersonaId = @TipoPersonaId, 
						PlanGrupoId = @PlanGrupoId, 
						EstadoId = @EstadoId
				WHERE  RegTipoPersonaId = @RegTipoPersonaId
			END
			ELSE
			BEGIN
				RAISERROR('Registro a Grupo de Cuentas Duplicado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Registro a Grupo de Cuentas No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbRegTipoPersonaDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbRegTipoPersonaDelete 
END 
GO
CREATE PROC dbo.ctbRegTipoPersonaDelete 
			@DeleteFilter smallint,
			@RegTipoPersonaId int,
			@TipoPersonaId int
AS 
BEGIN
	IF @DeleteFilter = 0 --All
	BEGIN
		IF EXISTS (	SELECT	RegTipoPersonaId 
					FROM	ctbRegTipoPersona 
					WHERE	RegTipoPersonaId = @RegTipoPersonaId) 
		BEGIN
			DELETE
			FROM   dbo.ctbRegTipoPersona
			WHERE  RegTipoPersonaId = @RegTipoPersonaId
		END
		ELSE
		BEGIN
			RAISERROR('ID de Registro a Grupo de Cuentas No Encontrado', 16, 1)
			RETURN
		END 
	END
	ELSE IF @DeleteFilter = 1 --TipoPersonaId
	BEGIN
		DELETE
		FROM   dbo.ctbRegTipoPersona
		WHERE  TipoPersonaId = @TipoPersonaId
	END
END
GO

