/********************************************************************/
/*  STORE PROCEDURE	: segAutorizaItemSelect							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('segAutorizaItemSelect') IS NOT NULL
BEGIN 
    DROP PROC segAutorizaItemSelect 
END 
GO
CREATE PROC segAutorizaItemSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('segAutorizaItemInsert') IS NOT NULL
BEGIN 
    DROP PROC segAutorizaItemInsert	 
END 
GO
CREATE PROC segAutorizaItemInsert
			@InsertFilter smallint,
			@AutorizaItemId int OUT, 
			@AutorizaItemDes varchar(50),
			@ModuloId int
AS
BEGIN	
	IF @InsertFilter = 0		--All
	BEGIN
		IF NOT EXISTS (	SELECT	AutorizaItemId 
					FROM	segAutorizaItem 
					WHERE	ModuloId = @ModuloId
						AND AutorizaItemDes = @AutorizaItemDes ) 	
		BEGIN
			INSERT INTO segAutorizaItem( AutorizaItemDes, ModuloId)
								VALUES (@AutorizaItemDes, @ModuloId)
		
			SET @AutorizaItemId = SCOPE_IDENTITY()
		END
	END
	ELSE
	BEGIN
		RAISERROR('Item Autorización Duplicado', 16, 1)
		RETURN
    END 
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('segAutorizaItemUpdate') IS NOT NULL
BEGIN 
    DROP PROC segAutorizaItemUpdate 
END 
GO
CREATE PROC segAutorizaItemUpdate 
			@UpdateFilter smallint,
			@AutorizaItemId int OUT, 
			@AutorizaItemDes varchar(50), 
			@ModuloId int

AS 
BEGIN
	
	IF @UpdateFilter = 0		--All
	BEGIN
		IF NOT EXISTS (	SELECT	AutorizaItemId 
							FROM	segAutorizaItem 
							WHERE	AutorizaItemId = @AutorizaItemId)	
		BEGIN			
			UPDATE	segAutorizaItem
			SET		ModuloId	=	@ModuloId, 
					AutorizaItemDes = @AutorizaItemDes
			WHERE	AutorizaItemId = @AutorizaItemId
		END	
		ELSE
		BEGIN
			RAISERROR('ID de AutorizaItem No Encontrado', 16, 1)
			RETURN
		END 			 
	END	
END
GO

----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

IF OBJECT_ID('segAutorizaItemDelete') IS NOT NULL
BEGIN 
    DROP PROC segAutorizaItemDelete 
END 
GO
CREATE PROC segAutorizaItemDelete 
			@DeleteFilter smallint,
			@AutorizaItemId int
AS 
BEGIN
	IF @DeleteFilter = 0 --All
	BEGIN
		IF EXISTS (	SELECT	AutorizaItemId 
				FROM	segAutorizaItem 
				WHERE	AutorizaItemId = @AutorizaItemId) 
		BEGIN			
			DELETE
			FROM   segAutorizaItem
			WHERE  AutorizaItemId = @AutorizaItemId		
		END
		ELSE
		BEGIN
			RAISERROR('ID de AutorizaItem No Encontrado', 16, 1)
			RETURN
		END 		
	END
	
END
GO


EXEC segAutorizaItemInsert	0, 0, 'segAplicacion5',	1
EXEC segAutorizaItemInsert	0, 0, 'parPrefijo2',	1
EXEC segAutorizaItemInsert	0, 0, 'parDocumento2',	1
EXEC segAutorizaItemInsert	0, 0, 'parTipoPersona2',	1
EXEC segAutorizaItemInsert	0, 0, 'ctbCenCos2',	2
EXEC segAutorizaItemInsert	0, 0, 'ctbSucursal2',	2
EXEC segAutorizaItemInsert	0, 0, 'ctbPlanGrupo2',	2
EXEC segAutorizaItemInsert	0, 0, 'invAlmacen2',	3
EXEC segAutorizaItemInsert	0, 0, 'invListaPrecios2',	3
EXEC segAutorizaItemInsert	0, 0, 'venVendedores2',	3


------------AUXILIAR-----------
IF OBJECT_ID('segAutorizaItemDet') IS NOT NULL
BEGIN 
    DROP PROC segAutorizaItemDet 
END 
GO
CREATE PROC segAutorizaItemDet 
			@SelectFilter smallint,
			@WhereFilter smallint,
			@OrderByFilter smallint,
			@Nombre varchar(50)			
AS
BEGIN
	IF (@Nombre = 'segAplicacion')
	BEGIN
		EXEC segAplicacionSelect 2,0,1
	END
	ELSE IF (@Nombre = 'segparPrefijo')
	BEGIN
		EXEC segPrefijoSelect 2,0,
	END
	ELSE IF (@Nombre = 'parDocumento')
	BEGIN
		EXEC segDocumentoSelect 2,0,
	END
	ELSE IF (@Nombre = 'segTipoPersona')
	BEGIN
		EXEC segTipoPersonaSelect 2,0,
	END
	
END
GO




--EXEC segAutorizaItemSelect 3, 6, 0, 2, 0

--EXEC segAutorizaItemInsert 0, 0, 1, 1, 'a11234', 'yo', '', '23232', '2323','',  1, 'mmercado', '01/01/2017', 1

--EXEC segAutorizaItemUpdate 0, 2321, 2, 1, 'a4455', 'yoel', '', '23232', '2323','',  1, 'jmercado', '01/01/2017', 1