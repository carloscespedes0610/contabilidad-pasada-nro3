
/********************************************************************/
/*  STORE PROCEDURE	: parMonedaSelect							   	    */
/*  AUTOR			: Carlos Cespedes									*/
/*  MODIFICACION	: Carlos Cespedes (15/06/2018)					*/
/*  MODIFICACION	: Carlos Cespedes (15/06/2018)					*/
/*  FECHA			: 15/06/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('parMonedaSelect') IS NOT NULL
BEGIN 
    DROP PROC parMonedaSelect 
END 
GO
CREATE PROC parMonedaSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO