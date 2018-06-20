/********************************************************************/
/*  STORE PROCEDURE	: ctbPlanGrupo							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbPlanGrupoSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanGrupoSelect 
END 
GO
CREATE PROC ctbPlanGrupoSelect 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@PlanGrupoId int,
			@PlanGrupoCod varchar(50),
			@EstadoId int 
AS
BEGIN
	IF  (@SelectFilter = 0 ) --All
	AND (@WhereFilter = 1)   --PrimaryKey
	BEGIN
		SELECT	ctbPlanGrupo.PlanGrupoId, 
				ctbPlanGrupo.PlanGrupoCod, 
				ctbPlanGrupo.PlanGrupoDes, 
				ctbPlanGrupo.PlanGrupoEsp, 
				ctbPlanGrupo.PlanGrupoTipoId, 
				ctbPlanGrupo.PlanGrupoTipoDetId, 
				ctbPlanGrupo.NroCuentas, 
				ctbPlanGrupo.MonedaId, 
				ctbPlanGrupo.VerificaMto, 
				ctbPlanGrupo.EstadoId 
		FROM	ctbPlanGrupo 
		WHERE	PlanGrupoId = @PlanGrupoId 
	END

	ELSE IF (@SelectFilter = 3) --Grid
	AND (@WhereFilter = 3)		--Grid
	AND (@OrderByFilter = 3)	--Grid
	BEGIN
		SELECT	ctbPlanGrupo.PlanGrupoId, 
				ctbPlanGrupo.PlanGrupoCod, 
				ctbPlanGrupo.PlanGrupoDes, 
				ctbPlanGrupo.PlanGrupoEsp, 
				ctbPlanGrupoTipo.PlanGrupoTipoId, 
				ctbPlanGrupoTipo.PlanGrupoTipoDes, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetId, 
				ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes, 
				ctbPlanGrupo.NroCuentas, 
				parMoneda.MonedaId, 
				parMoneda.MonedaDes, 
				ctbPlanGrupo.VerificaMto, 
				parEstado.EstadoId, 
				parEstado.EstadoDes 
		FROM	ctbPlanGrupo  
		LEFT JOIN	ctbPlanGrupoTipo	ON ctbPlanGrupo.PlanGrupoTipoId = ctbPlanGrupoTipo.PlanGrupoTipoId				 
		LEFT JOIN	ctbPlanGrupoTipoDet	ON ctbPlanGrupo.PlanGrupoTipoDetId = ctbPlanGrupoTipoDet.PlanGrupoTipoDetId				 
		LEFT JOIN	parMoneda			ON ctbPlanGrupo.MonedaId = parMoneda.MonedaId 		
		LEFT JOIN	parEstado			ON ctbPlanGrupo.EstadoId = parEstado.EstadoId 
		ORDER BY ctbPlanGrupoTipo.PlanGrupoTipoDes, ctbPlanGrupoTipoDet.PlanGrupoTipoDetDes 
	END

	ELSE IF (@SelectFilter = 2) --ListBox
	AND (@OrderByFilter = 2)	--PlanGrupoDes
	BEGIN
		SELECT	ctbPlanGrupo.PlanGrupoId, 
				ctbPlanGrupo.PlanGrupoCod, 
				ctbPlanGrupo.PlanGrupoDes 
		FROM	ctbPlanGrupo 
		ORDER BY ctbPlanGrupo.PlanGrupoDes
	END

	ELSE IF  (@SelectFilter = 0 )	--All
	AND (@WhereFilter = 5)			--PlanGrupoCod
	BEGIN
		SELECT	ctbPlanGrupo.PlanGrupoId, 
				ctbPlanGrupo.PlanGrupoCod, 
				ctbPlanGrupo.PlanGrupoDes, 
				ctbPlanGrupo.PlanGrupoEsp, 
				ctbPlanGrupo.PlanGrupoTipoId, 
				ctbPlanGrupo.PlanGrupoTipoDetId, 
				ctbPlanGrupo.NroCuentas, 
				ctbPlanGrupo.MonedaId, 
				ctbPlanGrupo.VerificaMto, 
				ctbPlanGrupo.EstadoId 
		FROM	ctbPlanGrupo 
		WHERE	PlanGrupoCod = @PlanGrupoCod 
	END
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('ctbPlanGrupoInsert') IS NOT NULL
BEGIN 
    DROP PROC ctbPlanGrupoInsert	 
END 
GO
CREATE PROC ctbPlanGrupoInsert
			@InsertFilter smallint,
			@Id int OUT, 
			@PlanGrupoCod varchar(50),
			@PlanGrupoDes varchar(255),
			@PlanGrupoEsp varchar(255),
			@PlanGrupoTipoId int,
			@PlanGrupoTipoDetId int,
			@NroCuentas int,
			@MonedaId int,
			@VerificaMto int,
			@EstadoId int
AS
BEGIN
	IF NOT EXISTS (	SELECT	PlanGrupoCod 
					FROM	ctbPlanGrupo 
					WHERE	PlanGrupoCod = @PlanGrupoCod) 
	BEGIN
		IF @InsertFilter = 0 --All
		BEGIN
			INSERT INTO ctbPlanGrupo(PlanGrupoCod, 
								PlanGrupoDes,
								PlanGrupoEsp,
								PlanGrupoTipoId,
								PlanGrupoTipoDetId,
								NroCuentas,
								MonedaId,
								VerificaMto,
								EstadoId)
						VALUES (@PlanGrupoCod, 
								@PlanGrupoDes,
								@PlanGrupoEsp,
								@PlanGrupoTipoId,
								@PlanGrupoTipoDetId,
								@NroCuentas,
								@MonedaId,
								@VerificaMto,
								@EstadoId)
		
			SET @Id = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Código de Grupo de Cuentas Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbPlanGrupoUpdate') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbPlanGrupoUpdate 
END 
GO
CREATE PROC dbo.ctbPlanGrupoUpdate 
			@UpdateFilter smallint,
			@PlanGrupoId int,
			@PlanGrupoCod varchar(50),
			@PlanGrupoDes varchar(255),
			@PlanGrupoEsp varchar(255),
			@PlanGrupoTipoId int,
			@PlanGrupoTipoDetId int,
			@NroCuentas int,
			@MonedaId int,
			@VerificaMto int,
			@EstadoId int
AS 
BEGIN
	IF EXISTS (	SELECT	PlanGrupoId 
				FROM	ctbPlanGrupo 
				WHERE	PlanGrupoId = @PlanGrupoId) 
	BEGIN
		IF @UpdateFilter = 0 --All
		BEGIN
			IF NOT EXISTS (	SELECT	PlanGrupoCod 
							FROM	ctbPlanGrupo 
							WHERE	PlanGrupoCod = @PlanGrupoCod 
							AND		PlanGrupoId <> @PlanGrupoId)
			BEGIN	
				UPDATE dbo.ctbPlanGrupo
				SET		PlanGrupoCod = @PlanGrupoCod, 
						PlanGrupoDes = @PlanGrupoDes, 
						PlanGrupoEsp = @PlanGrupoEsp, 
						PlanGrupoTipoId = @PlanGrupoTipoId, 
						PlanGrupoTipoDetId = @PlanGrupoTipoDetId, 
						NroCuentas = @NroCuentas, 
						MonedaId = @MonedaId, 
						VerificaMto = @VerificaMto, 
						EstadoId = @EstadoId
				WHERE  PlanGrupoId = @PlanGrupoId
			END
			ELSE
			BEGIN
				RAISERROR('Código de Grupo de Cuentas Duplicado', 16, 1)
				RETURN
			END 
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Grupo de Cuentas No Encontrado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('dbo.ctbPlanGrupoDelete') IS NOT NULL
BEGIN 
    DROP PROC dbo.ctbPlanGrupoDelete 
END 
GO
CREATE PROC dbo.ctbPlanGrupoDelete 
			@DeleteFilter smallint,
			@PlanGrupoId int
AS 
BEGIN
	IF EXISTS (	SELECT	PlanGrupoId 
				FROM	ctbPlanGrupo 
				WHERE	PlanGrupoId = @PlanGrupoId) 
	BEGIN
		IF @DeleteFilter = 0 --All
		BEGIN
			DELETE
			FROM   dbo.ctbPlanGrupo
			WHERE  PlanGrupoId = @PlanGrupoId
		END
	END
	ELSE
	BEGIN
		RAISERROR('ID de Grupo de Cuentas No Encontrado', 16, 1)
		RETURN
    END 
END
GO

