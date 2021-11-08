CREATE VIEW [dbo].[subastasPorCategoria]
AS	SELECT DISTINCT s.ProductoIDSubastado,ccp.IDCategoria FROM CategoriaContieneProductos ccp
	INNER JOIN Subasta s ON (s.CorreoSubastador = ccp.CorreoClienteProducto AND s.ProductoIDSubastado = ccp.ProductoID)
	WHERE S.FechaInicio < GETDATE()
	AND S.FechaFin > GETDATE();
