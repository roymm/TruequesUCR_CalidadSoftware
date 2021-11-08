CREATE TABLE [dbo].[Sublista]
(
	[Id]				INT IDENTITY (1,1)	NOT NULL,
	[CorreoDueno]		NVARCHAR (50)	NOT NULL,
	[NombreSublista]	NVARCHAR (100)	 NOT NULL,
	PRIMARY KEY ([Id],[CorreoDueno] ASC),
	CONSTRAINT [FK_dbo.Sublista_dbo.Cliente] FOREIGN KEY ([CorreoDueno])
		REFERENCES [dbo].[Cliente] ([Correo])
)
