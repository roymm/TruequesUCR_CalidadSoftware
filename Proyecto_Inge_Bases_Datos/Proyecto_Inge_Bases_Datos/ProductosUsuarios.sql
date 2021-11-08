CREATE VIEW [dbo].[ProductosUsuarios]
	AS SELECT TOP 10 r.Nombre,r.Correo, r.Apellido1, r.Apellido2, count(p.ProductoID) as CantidadProductos 
FROM [dbo].[Producto] p
INNER JOIN [dbo].[Registrado] r
ON p.CorreoCliente=r.Correo
WHERE p.Publicado = 1
GROUP BY r.Nombre,r.Correo, r.Apellido1, r.Apellido2, p.CorreoCliente
ORDER BY CantidadProductos DESC;

	
