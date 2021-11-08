CREATE PROCEDURE spProducto_SelectUltimosDiez
AS

BEGIN
SELECT p.Nombre, p.PrecioEstimado, p.Condicion, Registrado.Nombre, Registrado.Apellido1 
From Producto p
INNER JOIN Registrado ON p.CorreoCliente = Registrado.Correo ORDER BY p.FechaPublicado ASC
END
