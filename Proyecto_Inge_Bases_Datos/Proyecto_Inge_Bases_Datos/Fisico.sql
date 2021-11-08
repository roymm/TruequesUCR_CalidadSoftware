CREATE TABLE [dbo].[Fisico]
(
	[ProductoID] INT NOT NULL,
	[CorreoCliente] NVARCHAR(50)  NOT NULL 
	PRIMARY KEY CLUSTERED (ProductoID,CorreoCliente),
	
	CONSTRAINT [FK_dbo.Fisico_dbo.Producto_ProductoID_CorreoCliente] FOREIGN KEY (ProductoID, CorreoCliente)
		REFERENCES [dbo].[Producto] (ProductoID,CorreoCliente) ON DELETE CASCADE
)