CREATE VIEW [dbo].[VistaMisProductos]
AS	SELECT        CAT.CorreoCliente, CL.IDCategoria AS CatInteresCliente, CAT.IDCategoria AS CatIdProducto, CL.ProductoID
FROM            dbo.Relacion_ClienteTieneInteresCategoria AS CAT INNER JOIN
                         dbo.CategoriaContieneProductos AS CL ON CAT.IDCategoria = CL.IDCategoria AND CAT.IDCategoria = CL.IDCategoria
