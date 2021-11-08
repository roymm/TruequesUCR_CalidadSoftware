CREATE VIEW [dbo].[Productos_Info_Clientes]
	AS SELECT  P.FechaPublicado, P.Nombre, P.CorreoCliente, P.ProductoID, P.ProductoImagen1, P.Condicion, P.PrecioEstimado,C.CalificacionPromedio, R.Nombre as nombre_cliente
	FROM Producto P, Cliente C, Registrado R
	WHERE P.CorreoCliente = C.Correo AND C.Correo = R.Correo
