CREATE TABLE [dbo].[Denuncia]
(
	[Fecha] DATETIME NOT NULL,
	 PRIMARY KEY ([Fecha] ASC),
	[Comentarios] NVARCHAR (200) NULL,
	[Denunciante] NVARCHAR (50) NOT NULL,
	[Denunciado] NVARCHAR (50) NOT NULL,
	[Tipo] TINYINT NOT NULL,
	[ProductoID] INT NULL,
	CONSTRAINT [FK_dbo.Producto_dbo.Cliente_Denunciante] FOREIGN KEY ([Denunciante])
		REFERENCES [dbo].[Cliente] ([Correo]),
	CONSTRAINT [FK_dbo.Producto_dbo.Cliente_Denunciado] FOREIGN KEY ([Denunciado])
		REFERENCES [dbo].[Cliente] ([Correo])
	
)