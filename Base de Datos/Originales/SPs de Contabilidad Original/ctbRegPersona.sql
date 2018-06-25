/********************************************************************/
/*  STORE PROCEDURE	: ctbRegPersona							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbRegPersonaSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbRegPersonaSelect 
END 
GO
CREATE PROC ctbRegPersonaSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@RegPersonaId int,
			@PersonaId int,
			@PlanGrupoId int,
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 1)   --PrimaryKey
	BEGIN
		SELECT	ctbRegPersona.RegPersonaId, 
				ctbRegPersona.PersonaId, 
				ctbRegPersona.PlanGrupoId, 
				ctbRegPersona.MonedaId, 
				ctbRegPersona.FechaReg, 
				ctbRegPersona.RegPersonaObs, 
				ctbRegPersona.SaldoAnterior, 
				ctbRegPersona.FechaAnterior, 
				ctbRegPersona.SaldoActual, 
				ctbRegPersona.MontoMaximo, 
				ctbRegPersona.FechaExpiracion, 
				ctbRegPersona.CreditoId, 
				ctbRegPersona.MaxCuotaVenc, 
				ctbRegPersona.EstadoId 
		FROM	ctbRegPersona 
		WHERE	RegPersonaId = @RegPersonaId 
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 3)		--Grid
	AND (@OrderByFilter = 3)	--Grid
	BEGIN
		SELECT	ctbRegPersona.RegPersonaId, 
				parPersona.PersonaId, 
				parPersona.RazonSocial, 
				ctbPlanGrupo.PlanGrupoId, 
				ctbPlanGrupo.PlanGrupoDes, 
				parMoneda.MonedaId, 
				parMoneda.MonedaDes, 
				ctbRegPersona.FechaReg, 
				ctbRegPersona.RegPersonaObs, 
				ctbRegPersona.SaldoAnterior, 
				ctbRegPersona.FechaAnterior, 
				ctbRegPersona.SaldoActual, 
				ctbRegPersona.MontoMaximo, 
				ctbRegPersona.FechaExpiracion, 
				ctbRegPersona.CreditoId, 
				ctbRegPersona.MaxCuotaVenc, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	ctbRegPersona  
		LEFT JOIN	parPersona		ON ctbRegPersona.PersonaId = parPersona.PersonaId				 
		LEFT JOIN	ctbPlanGrupo	ON ctbRegPersona.PlanGrupoId = ctbPlanGrupo.PlanGrupoId				 
		LEFT JOIN	parMoneda		ON ctbRegPersona.MonedaId = parMoneda.MonedaId 		
		LEFT JOIN	parEstado		ON ctbRegPersona.EstadoId = parEstado.EstadoId 
		ORDER BY parPersona.RazonSocial, ctbPlanGrupo.PlanGrupoDes 
	END

	ELSE IF (@SelectFilter = 2) --ListBox
	AND (@OrderByFilter = 2)	--PlanGrupoId
	BEGIN
		SELECT	ctbRegPersona.RegPersonaId, 
				ctbRegPersona.RegPersonaObs 
		FROM	ctbRegPersona 
		ORDER BY ctbRegPersona.PlanGrupoId
	END

	ELSE IF  (@SelectFilter = 0 )	--All
	AND (@WhereFilter = 5)			--PersonaId
	BEGIN
		SELECT	ctbRegPersona.RegPersonaId, 
				ctbRegPersona.PersonaId, 
				ctbRegPersona.PlanGrupoId, 
				ctbRegPersona.MonedaId, 
				ctbRegPersona.FechaReg, 
				ctbRegPersona.RegPersonaObs, 
				ctbRegPersona.SaldoAnterior, 
				ctbRegPersona.FechaAnterior, 
				ctbRegPersona.SaldoActual, 
				ctbRegPersona.MontoMaximo, 
				ctbRegPersona.FechaExpiracion, 
				ctbRegPersona.CreditoId, 
				ctbRegPersona.MaxCuotaVenc, 
				ctbRegPersona.EstadoId 
		FROM	ctbRegPersona 
		WHERE	PersonaId = @PersonaId 
	END
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('ctbRegPersonaInsert') IS NOT NULL
BEGIN 
    DROP PROC ctbRegPersonaInsert	 
END 
GO
CREATE PROC ctbRegPersonaInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@PersonaId int,
			@PlanGrupoId int,
			@MonedaId int,
			@FechaReg datetime,
			@RegPersonaObs varchar(255),
			@SaldoAnterior decimal(18,5),
			@FechaAnterior datetime,
			@SaldoActual decimal(18,5),
			@MontoMaximo decimal(18,5),
			@FechaExpiracion datetime,
			@CreditoId int,
			@MaxCuotaVenc int,
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	PersonaId 
					FROM	ctbRegPersona 
					WHERE	PersonaId = @PersonaId 
					AND		PlanGrupoId = @PlanGrupoId) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbRegPersona(PersonaId, 
								PlanGrupoId,
								MonedaId,
								FechaReg,
								RegPersonaObs,
								SaldoAnterior,
								FechaAnterior,
								SaldoActual,
								MontoMaximo,
								FechaExpiracion,
								CreditoId,
								MaxCuotaVenc,
								EstadoId)
						VALUES (@PersonaId, 
								@PlanGrupoId,
								@MonedaId,
								@FechaReg,
								@RegPersonaObs,
								@SaldoAnterior,
								@FechaAnterior,
								@SaldoActual,
								@MontoMaximo,
								@FechaExpiracion,
								@CreditoId,
								@MaxCuotaVenc,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Registro de Persona a Grupo de Cuentas Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbRegPersonaUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbRegPersonaUpdate 
END 
GO
CREATE PROC dbo.ctbRegPersonaUpdate 
			@UpdateFilter smallint,
			@RegPersonaId int,
			@PersonaId int,
			@PlanGrupoId int,
			@MonedaId int,
			@FechaReg datetime,
			@RegPersonaObs varchar(255),
			@SaldoAnterior decimal(18,5),
			@FechaAnterior datetime,
			@SaldoActual decimal(18,5),
			@MontoMaximo decimal(18,5),
			@FechaExpiracion datetime,
			@CreditoId int,
			@MaxCuotaVenc int,
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	RegPersonaId 
				FROM	ctbRegPersona 
				WHERE	RegPersonaId = @RegPersonaId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	PersonaId 
							FROM	ctbRegPersona 
							WHERE	PersonaId = @PersonaId 
							AND		PlanGrupoId = @PlanGrupoId 
							AND		RegPersonaId <> @RegPersonaId)
			BEGIN	
				UPDATE dbo.ctbRegPersona
				SET		PersonaId = @PersonaId, 
						PlanGrupoId = @PlanGrupoId, 
						MonedaId = @MonedaId, 
						FechaReg = @FechaReg, 
						RegPersonaObs = @RegPersonaObs, 
						SaldoAnterior = @SaldoAnterior, 
						FechaAnterior = @FechaAnterior, 
						SaldoActual = @SaldoActual, 
						MontoMaximo = @MontoMaximo, 
						FechaExpiracion = @FechaExpiracion, 
						CreditoId = @CreditoId, 
						MaxCuotaVenc = @MaxCuotaVenc, 
						EstadoId = @EstadoId
				WHERE  RegPersonaId = @RegPersonaId
			END
			ELSE
			BEGIN
				RAISERROR('Registro de Persona a Grupo de Cuentas Duplicado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID Registro de Persona a Grupo de Cuentas No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbRegPersonaDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbRegPersonaDelete 
END 
GO
CREATE PROC dbo.ctbRegPersonaDelete 
			@DeleteFilter smallint,
			@RegPersonaId int
AS 
BEGIN
	IF EXISTS (	SELECT	RegPersonaId 
				FROM	ctbRegPersona 
				WHERE	RegPersonaId = @RegPersonaId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.ctbRegPersona
			WHERE  RegPersonaId = @RegPersonaId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID Registro de Persona a Grupo de Cuentas No Encontrado', 16, 1)
		RETURN
    END 
END
GO

