CREATE VIEW [dbo].[VistaInteresesCliente]
	AS SELECT        cliente.CorreoCliente AS CatCliente, cliente.IDCategoria AS idCliente, amigo.CorreoCliente AS CatAmigo, amigo.IDCategoria AS idAmigo
FROM            dbo.Relacion_ClienteTieneInteresCategoria AS cliente INNER JOIN
                         dbo.Relacion_ClienteTieneInteresCategoria AS amigo ON cliente.CorreoCliente = cliente.CorreoCliente AND cliente.IDCategoria = amigo.IDCategoria AND cliente.CorreoCliente <> amigo.CorreoCliente
