CREATE TABLE [dbo].[Virtual]
(   [ProductoID] INT NOT NULL,	
    [CorreoCliente] NVARCHAR(50)  NOT NULL,
    [TipoDeArchivo] NVARCHAR(50) NULL, 
    [DerechosDeProducto] NVARCHAR(50) NULL, 
    [Fuentes] NVARCHAR(50) NULL, 
    [FechaExpiracion] DATETIME NULL,
	PRIMARY KEY CLUSTERED (ProductoID,CorreoCliente),

	CONSTRAINT [FK_dbo.Virtual_dbo.Producto_ProductoID_CorreoCliente] FOREIGN KEY (ProductoID, CorreoCliente)
		REFERENCES [dbo].[Producto] (ProductoID,CorreoCliente) ON DELETE CASCADE
        
)