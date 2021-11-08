CREATE TABLE [dbo].[CategoriaContieneProductos]
(
	[IDCategoria] INT NOT NULL,
	[ProductoID] INT NOT NULL,
	[CorreoClienteProducto]  NVARCHAR(50) NOT NULL,
	CONSTRAINT [FK_dbo.CategoriaContieneProductos_dbo.Categoria] FOREIGN KEY ([IDCategoria])
	 REFERENCES [dbo].[Categoria] ([CategoriaID]) ON DELETE CASCADE,
	 CONSTRAINT [FK_dbo.CategoriaContieneProductos_dbo.Producto] FOREIGN KEY ([ProductoID],[CorreoClienteProducto])
	 REFERENCES [dbo].[Producto] ([ProductoID],[CorreoCliente]) ON DELETE CASCADE,
	 PRIMARY KEY ([IDCategoria],[ProductoID],[CorreoClienteProducto] ASC)
)
