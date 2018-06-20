
/********************************************************************/
/*  STORE PROCEDURE	: ctbSucursal							   	    */
/*  AUTOR			: Joel Mercado									*/
/*  MODIFICACION	: Carlos Cespedes (15/06/2018)					*/
/*  FECHA			: 28/03/2018									*/
/*  DESCRIPCION		:									            */
/********************************************************************/

IF OBJECT_ID('ctbTipoPlanSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbTipoPlanSelect 
END 
GO
CREATE PROC ctbTipoPlanSelect 
			@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO
