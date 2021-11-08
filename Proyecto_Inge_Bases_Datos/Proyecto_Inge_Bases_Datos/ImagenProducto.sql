CREATE TABLE [dbo].[ImagenProducto]
(
	[ProductoID]		INT				NOT NULL , 
	[Imagen1]			IMAGE			NULL,
	[Imagen2]			IMAGE			NULL,
	[Imagen3]			IMAGE			NULL

	PRIMARY KEY CLUSTERED (ProductoID),

	CONSTRAINT [FK_dbo.Producto_dbo.ImagenProducto] FOREIGN KEY ([ProductoID])
		REFERENCES [dbo].[Producto] ([ProductoID]) ON DELETE CASCADE	
)
