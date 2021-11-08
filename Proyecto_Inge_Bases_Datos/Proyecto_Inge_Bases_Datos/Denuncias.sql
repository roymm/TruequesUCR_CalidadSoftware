CREATE TABLE [dbo].[Denuncias]
(
	[Fecha] DATETIME2 NOT NULL PRIMARY KEY,
	[Comentarios] NVARCHAR (50) NULL,
	[Denunciante] NVARCHAR (50) NOT NULL,
	[Denunciado] NVARCHAR (50) NOT NULL,
	[Tipo] TINYINT NOT NULL,
	CONSTRAINT [FK_dbo.Producto_dbo.Cliente_Denunciante] FOREIGN KEY ([Denunciante])
		REFERENCES [dbo].[Cliente] ([Correo]),
	CONSTRAINT [FK_dbo.Producto_dbo.Cliente_Denunciado] FOREIGN KEY ([Denunciado])
		REFERENCES [dbo].[Cliente] ([Correo]) 
)