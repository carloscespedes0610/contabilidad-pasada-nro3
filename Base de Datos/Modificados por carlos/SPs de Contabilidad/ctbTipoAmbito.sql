
IF OBJECT_ID('ctbTipoAmbitoSelect') IS NOT NULL
BEGIN 
    DROP PROC ctbTipoAmbitoSelect 
END 
GO
CREATE PROC ctbTipoAmbitoSelect 
	@SQL varchar(MAX) 
AS
BEGIN
	EXEC(@SQL)
END
GO