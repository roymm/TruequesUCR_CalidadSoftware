CREATE TABLE [dbo].[Relacion_ClienteTieneInteresCategoria]
(
	[id]				INT IDENTITY (1,1)	NOT NULL,
	[CorreoCliente]		NVARCHAR (50) NOT NULL,
	[IDCategoria]		INT			  NOT NULL,
	PRIMARY KEY CLUSTERED([CorreoCliente],[IDCategoria] ASC),
	CONSTRAINT [FK_dbo.Cliente_dbo.Relacion_ClienteTieneInteresCategoria] FOREIGN KEY ([CorreoCliente])
		REFERENCES [dbo].[Cliente] (Correo),
	CONSTRAINT [FK_dbo.Categoria_dbo.Relacion_ClienteTieneInteresCategoria] FOREIGN KEY ([IDCategoria])
		REFERENCES [dbo].[Categoria] (CategoriaID)
)