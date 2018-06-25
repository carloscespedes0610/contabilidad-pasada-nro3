/********************************************************************/
/*  STORE PROCEDURE	: segUsuarioSelect							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('segUsuarioSelect') IS NOT NULL
BEGIN 
    DROP PROC segUsuarioSelect 
END 
GO
CREATE PROC segUsuarioSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('segUsuarioInsert') IS NOT NULL
BEGIN 
    DROP PROC segUsuarioInsert	 
END 
GO
CREATE PROC segUsuarioInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@UsuarioCod		varchar(50) ,   
			@UsuarioDes		varchar(255) ,	
			@TipoUsuarioId	int ,           
			@UsuarioDocPath	varchar(255) ,      
			@UsuarioFotoPath	varchar(255) ,      
			@UsuarioMaxSes	int ,           
			@EstadoId			int 
AS
BEGIN
	IF NOT EXISTS (	SELECT	UsuarioCod 
					FROM	segUsuario 
					WHERE	TipoUsuarioId = @TipoUsuarioId 
					AND		UsuarioCod = @UsuarioCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO segUsuario(
						UsuarioCod,
						UsuarioDes,
						TipoUsuarioId,
						UsuarioDocPath,
						UsuarioFotoPath,
						UsuarioMaxSes,
						EstadoId)
			VALUES (
						@UsuarioCod,
						@UsuarioDes,
						@TipoUsuarioId,
						@UsuarioDocPath,
						@UsuarioFotoPath,
						@UsuarioMaxSes,
						@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Usuario Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.segUsuarioUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.segUsuarioUpdate 
END 
GO
CREATE PROC dbo.segUsuarioUpdate 
			@UpdateFilter smallint,
			@UsuarioId int,
			@UsuarioCod varchar(50),
			@UsuarioDes varchar(255),
			@TipoUsuarioId int,
			@UsuarioDocPath varchar(255),
			@UsuarioFotoPath varchar(255),
			@UsuarioMaxSes int,
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	UsuarioId 
				FROM	segUsuario 
				WHERE	UsuarioId = @UsuarioId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			UPDATE dbo.segUsuario
			SET 				
				UsuarioCod = @UsuarioCod, 
				UsuarioDes = @UsuarioDes, 
				TipoUsuarioId = @TipoUsuarioId, 
				UsuarioDocPath = @UsuarioDocPath, 
				UsuarioFotoPath = @UsuarioFotoPath, 
				UsuarioMaxSes = @UsuarioMaxSes, 
				EstadoId = @EstadoId
				
			WHERE  UsuarioId = @UsuarioId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Usuario No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.segUsuarioDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.segUsuarioDelete 
END 
GO
CREATE PROC dbo.segUsuarioDelete 
			@DeleteFilter smallint,
			@UsuarioId int
AS 
BEGIN
	IF EXISTS (	SELECT	UsuarioId 
				FROM	segUsuario 
				WHERE	UsuarioId = @UsuarioId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.segUsuario
			WHERE  UsuarioId = @UsuarioId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Usuario No Encontrado', 16, 1)
		RETURN
    END 
END
GO

/**************  LOGIN    ************/
GO
CREATE PROC [dbo].[segUsuarioLoginInsert]			
			@UsuarioCod varchar(50),
			@UsuarioPass varchar(255)
AS
BEGIN
	IF NOT EXISTS (	SELECT	name 
					FROM	sysusers  
					WHERE	sysusers.name = @UsuarioCod) 
	BEGIN				
		exec sp_addlogin @UsuarioCod, @UsuarioPass 

		EXEC master..sp_addsrvrolemember @loginame = @UsuarioCod, @rolename = N'securityadmin'--'sysadmin'--'
		-- Creacion de Usuario  
		exec sp_grantdbaccess @UsuarioCod		
		-- Asignacion de Funciones BD 
		exec sp_addrolemember 'db_owner',@UsuarioCod									
	END
	ELSE
	BEGIN
		RAISERROR('Login de Usuario Duplicado', 16, 1)
		RETURN
    END 
END
GO
CREATE PROC [dbo].[segUsuarioLoginUpdate]			
			@UsuarioCod varchar(50),
			@UsuarioPass varchar(255)
AS
BEGIN
	IF EXISTS (	SELECT	name 
					FROM	sysusers  
					WHERE	sysusers.name = @UsuarioCod) 
	BEGIN	
		DECLARE @cmd_upd_pass varchar(250),@va_sta_tus int 
		SET @cmd_upd_pass = 'ALTER LOGIN [' + RTRIM(@UsuarioCod) + '] WITH PASSWORD = ''' + RTRIM(@UsuarioPass) + ''''         
		--** Ejecuta Prepare de Cambia Pasword
		EXEC @va_sta_tus = sp_executesql @cmd_upd_pass
		IF @va_sta_tus <> 0
			RAISERROR('No pudo ser cambiada la contraseña SQL2005/SQL2008.', 16, 1)							
		END
	ELSE
	BEGIN
		RAISERROR('Login de Usuario No Existen ', 16, 1)
		RETURN
    END 
END
GO
CREATE PROC [dbo].[segUsuarioLoginRemove]			
			@UsuarioCod varchar(50)
AS
BEGIN
	IF EXISTS (	SELECT	name 
					FROM	sysusers  
					WHERE	sysusers.name = @UsuarioCod) 
	BEGIN								
		
		BEGIN TRY
			EXEC sp_droprolemember 'db_owner', @UsuarioCod; 
			PRINT 'sp_droprolemember'		
			EXEC sp_dropuser @UsuarioCod;  							
			PRINT 'sp_dropuser'
			EXEC sp_droplogin @UsuarioCod;
			PRINT 'sp_droplogin'		
		END TRY
		BEGIN CATCH
			DECLARE @ERR varchar(150)
			SET @ERR =   (SELECT 'Error al Elimnar Usuario Login!: ' + ERROR_MESSAGE())
		   RAISERROR(@ERR, 16, 1)
		END CATCH
				
		RETURN 1					
	END
	ELSE
	BEGIN
		RAISERROR('No Existe Este Usuario ', 16, 1)
		RETURN 1
    END 
END
/****************************************/



