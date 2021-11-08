CREATE TABLE [dbo].[Relacion_Producto_Visible_A]
(
	[CorreoDuenoSublista]	NVARCHAR (50)	NOT NULL,
	[SublistaID]			INT				NOT NULL,
	[ProductoID]			INT				NOT NULL , 
	[CorreoDuenoProducto]	NVARCHAR(50)	NOT NULL ,	
	PRIMARY KEY ([CorreoDuenoSublista],[SublistaID],[ProductoID] ,[CorreoDuenoProducto]),
	
	CONSTRAINT [FK_dbo.ProductoVisible_dbo.Producto] FOREIGN KEY (ProductoID,CorreoDuenoProducto)
		REFERENCES [dbo].[Producto] (ProductoID,CorreoCliente),

	CONSTRAINT [FK_dbo.ProductoVisible_dbo.Sublista] FOREIGN KEY (SublistaID, CorreoDuenoSublista)
		REFERENCES [dbo].[Sublista] (Id,CorreoDueno)

	

)
