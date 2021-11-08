CREATE TABLE [dbo].[ListaAmigos]
(
	[CorreoDueno]		NVARCHAR (50)	NOT NULL,
	[CorreoAmigo]		NVARCHAR (50)	NOT NULL,
	PRIMARY KEY ([CorreoDueno], [CorreoAmigo] ASC),
	CONSTRAINT [FK_dbo.ListaAmigos_dbo.Cliente] FOREIGN KEY ([CorreoDueno])
		REFERENCES [dbo].[Cliente] (Correo),
	CONSTRAINT [FK_dbo.ListaAmigos_dbo.Registrado] FOREIGN KEY ([CorreoAmigo])
		REFERENCES [dbo].[Registrado] (Correo)
)