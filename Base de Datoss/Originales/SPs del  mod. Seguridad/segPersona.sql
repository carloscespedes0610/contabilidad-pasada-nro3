/********************************************************************/
/*  STORE PROCEDURE	: parPersonaSelect							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('parPersonaSelect') IS NOT NULL
BEGIN 
    DROP PROC parPersonaSelect 
END 
GO
CREATE PROC parPersonaSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('parPersonaInsert') IS NOT NULL
BEGIN 
    DROP PROC parPersonaInsert	 
END 
GO
CREATE PROC parPersonaInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@TipoPersonaId		int,
			@PersonaCod			int,
			@TipoEntidadId		int,
			@RazonSocial		varchar(255),
			@Nit				varchar(50),
			@IdentificaId		int,
			@IdentificaNro      varchar(50),
			@ExpedidoId         int,
			@IdentificaExt      varchar(10),
			@TelTrabajo         varchar(100),
			@TelParticular      varchar(100),
			@TelFax             varchar(100),
			@TelCelular         varchar(100),
			@Email              varchar(100),
			@PaginaWeb          varchar(100),
			@PrincipalPais      varchar(100),
			@PrincipalDpto      varchar(100),
			@PrincipalCiudad    varchar(100),
			@PrincipalDir       varchar(255),
			@PrincipalObs       varchar(255),
			@EntregaPais		varchar(100),
			@EntregaDpto		varchar(100),
			@EntregaCiudad		varchar(100),
			@EntregaDir			varchar(255),
			@EntregaObsPedFact	varchar(255),
			@EntregaObsEntrEnv	varchar(255),
			@EntregaObsInstCob	varchar(255),
			@CompraPais			varchar(100),
			@CompraDpto			varchar(100),
			@CompraCiudad		varchar(100),
			@CompraDir			varchar(255),
			@SitArancelariaId	int,
			@CompraObsCompra	varchar(255),
			@CompraObsPagos		varchar(255),
			@CompraObs			varchar(255),
			@FechaNac			datetime,
			@LugarNac			varchar(100),
			@OcupacionId		int,
			@CargoId			int,
			@EmpresaTrabajo		varchar(100),
			@SexoId				int,
			@EstadoCivilId		int,
			@FechaAniversario	datetime,
			@ObservacionContRef	varchar(255),
			@ObservacionRefAnt	varchar(255),
			@CoorX				varchar(50),
			@CoorY				varchar(50),
			@CodigoAnt			varchar(100),
			@EstadoId			int
AS
BEGIN
	IF NOT EXISTS (	SELECT	PersonaCod 
					FROM	parPersona 
					WHERE	TipoPersonaId = @TipoPersonaId 
					AND		PersonaCod = @PersonaCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO parPersona(						
						TipoPersonaId,
						PersonaCod,
						TipoEntidadId,
						RazonSocial,
						Nit,
						IdentificaId,
						IdentificaNro,
						ExpedidoId,
						IdentificaExt,
						TelTrabajo,
						TelParticular,
						TelFax ,
						TelCelular,
						Email ,
						PaginaWeb ,
						PrincipalPais,
						PrincipalDpto,
						PrincipalCiudad,
						PrincipalDir,
						PrincipalObs,
						EntregaPais,
						EntregaDpto,
						EntregaCiudad,
						EntregaDir,
						EntregaObsPedFact,
						EntregaObsEntrEnv,
						EntregaObsInstCob,
						CompraPais,
						CompraDpto,
						CompraCiudad,
						CompraDir,
						SitArancelariaId,
						CompraObsCompra,
						CompraObsPagos,
						CompraObs,
						FechaNac,
						LugarNac,
						OcupacionId,
						CargoId,
						EmpresaTrabajo,
						SexoId,
						EstadoCivilId,
						FechaAniversario,
						ObservacionContRef,
						ObservacionRefAnt,
						CoorX,
						CoorY,
						CodigoAnt,
						EstadoId)
			VALUES (
						@TipoPersonaId,
						@PersonaCod,
						@TipoEntidadId,
						@RazonSocial,
						@Nit,
						@IdentificaId,
						@IdentificaNro,
						@ExpedidoId,
						@IdentificaExt,
						@TelTrabajo,
						@TelParticular,
						@TelFax ,
						@TelCelular,
						@Email ,
						@PaginaWeb ,
						@PrincipalPais,
						@PrincipalDpto,
						@PrincipalCiudad,
						@PrincipalDir,
						@PrincipalObs,
						@EntregaPais,
						@EntregaDpto,
						@EntregaCiudad,
						@EntregaDir,
						@EntregaObsPedFact,
						@EntregaObsEntrEnv,
						@EntregaObsInstCob,
						@CompraPais,
						@CompraDpto,
						@CompraCiudad,
						@CompraDir,
						@SitArancelariaId,
						@CompraObsCompra,
						@CompraObsPagos,
						@CompraObs,
						@FechaNac,
						@LugarNac,
						@OcupacionId,
						@CargoId,
						@EmpresaTrabajo,
						@SexoId,
						@EstadoCivilId,
						@FechaAniversario,
						@ObservacionContRef,
						@ObservacionRefAnt,
						@CoorX,
						@CoorY,
						@CodigoAnt,
						@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Persona Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.parPersonaUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.parPersonaUpdate 
END 
GO
CREATE PROC dbo.parPersonaUpdate 
			@UpdateFilter smallint,
			@PersonaId int,
			@TipoPersonaId		int,
			@PersonaCod			int,
			@TipoEntidadId		int,
			@RazonSocial		varchar(255),
			@Nit				varchar(50),
			@IdentificaId		int,
			@IdentificaNro      varchar(50),
			@ExpedidoId         int,
			@IdentificaExt      varchar(10),
			@TelTrabajo         varchar(100),
			@TelParticular      varchar(100),
			@TelFax             varchar(100),
			@TelCelular         varchar(100),
			@Email              varchar(100),
			@PaginaWeb          varchar(100),
			@PrincipalPais      varchar(100),
			@PrincipalDpto      varchar(100),
			@PrincipalCiudad    varchar(100),
			@PrincipalDir       varchar(255),
			@PrincipalObs       varchar(255),
			@EntregaPais		varchar(100),
			@EntregaDpto		varchar(100),
			@EntregaCiudad		varchar(100),
			@EntregaDir			varchar(255),
			@EntregaObsPedFact	varchar(255),
			@EntregaObsEntrEnv	varchar(255),
			@EntregaObsInstCob	varchar(255),
			@CompraPais			varchar(100),
			@CompraDpto			varchar(100),
			@CompraCiudad		varchar(100),
			@CompraDir			varchar(255),
			@SitArancelariaId	int,
			@CompraObsCompra	varchar(255),
			@CompraObsPagos		varchar(255),
			@CompraObs			varchar(255),
			@FechaNac			datetime,
			@LugarNac			varchar(100),
			@OcupacionId		int,
			@CargoId			int,
			@EmpresaTrabajo		varchar(100),
			@SexoId				int,
			@EstadoCivilId		int,
			@FechaAniversario	datetime,
			@ObservacionContRef	varchar(255),
			@ObservacionRefAnt	varchar(255),
			@CoorX				varchar(50),
			@CoorY				varchar(50),
			@CodigoAnt			varchar(100),
			@EstadoId			int
AS 
BEGIN
	IF EXISTS (	SELECT	PersonaId 
				FROM	parPersona 
				WHERE	PersonaId = @PersonaId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			UPDATE dbo.parPersona
			SET 
				TipoPersonaId	=	@TipoPersonaId,
				PersonaCod	=	@PersonaCod,
				TipoEntidadId	=	@TipoEntidadId,
				RazonSocial	=	@RazonSocial,
				Nit	=	@Nit,
				IdentificaId	=	@IdentificaId,
				IdentificaNro	=	@IdentificaNro,
				ExpedidoId	=	@ExpedidoId,
				IdentificaExt	=	@IdentificaExt,
				TelTrabajo	=	@TelTrabajo,
				TelParticular	=	@TelParticular,
				TelFax 	=	@TelFax ,
				TelCelular	=	@TelCelular,
				Email 	=	@Email ,
				PaginaWeb 	=	@PaginaWeb ,
				PrincipalPais	=	@PrincipalPais,
				PrincipalDpto	=	@PrincipalDpto,
				PrincipalCiudad	=	@PrincipalCiudad,
				PrincipalDir	=	@PrincipalDir,
				PrincipalObs	=	@PrincipalObs,
				EntregaPais	=	@EntregaPais,
				EntregaDpto	=	@EntregaDpto,
				EntregaCiudad	=	@EntregaCiudad,
				EntregaDir	=	@EntregaDir,
				EntregaObsPedFact	=	@EntregaObsPedFact,
				EntregaObsEntrEnv	=	@EntregaObsEntrEnv,
				EntregaObsInstCob	=	@EntregaObsInstCob,
				CompraPais	=	@CompraPais,
				CompraDpto	=	@CompraDpto,
				CompraCiudad	=	@CompraCiudad,
				CompraDir	=	@CompraDir,
				SitArancelariaId	=	@SitArancelariaId,
				CompraObsCompra	=	@CompraObsCompra,
				CompraObsPagos	=	@CompraObsPagos,
				CompraObs	=	@CompraObs,
				FechaNac	=	@FechaNac,
				LugarNac	=	@LugarNac,
				OcupacionId	=	@OcupacionId,
				CargoId	=	@CargoId,
				EmpresaTrabajo	=	@EmpresaTrabajo,
				SexoId	=	@SexoId,
				EstadoCivilId	=	@EstadoCivilId,
				FechaAniversario	=	@FechaAniversario,
				ObservacionContRef	=	@ObservacionContRef,
				ObservacionRefAnt	=	@ObservacionRefAnt,
				CoorX	=	@CoorX,
				CoorY	=	@CoorY,
				CodigoAnt	=	@CodigoAnt,
				EstadoId	=	@EstadoId				
			WHERE  PersonaId = @PersonaId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Persona No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.parPersonaDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.parPersonaDelete 
END 
GO
CREATE PROC dbo.parPersonaDelete 
			@DeleteFilter smallint,
			@PersonaId int
AS 
BEGIN
	IF EXISTS (	SELECT	PersonaId 
				FROM	parPersona 
				WHERE	PersonaId = @PersonaId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.parPersona
			WHERE  PersonaId = @PersonaId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Persona No Encontrado', 16, 1)
		RETURN
    END 
END
GO

