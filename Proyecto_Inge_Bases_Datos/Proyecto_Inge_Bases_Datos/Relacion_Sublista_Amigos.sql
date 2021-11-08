CREATE TABLE [dbo].[Relacion_Sublista_Amigos]
(
	[CorreoDueno]			NVARCHAR(50)	NOT NULL, 
	[CorreoAmigo]			NVARCHAR(50)	NOT NULL,
	[CorreoDuenoSublista]	NVARCHAR(50)	NOT NULL,
	[SublistaID]			INT				NOT NULL,
	[Estado]				BIT				NULL		Default NULL,

	PRIMARY KEY CLUSTERED (CorreoDueno, CorreoAmigo, CorreoDuenoSublista, SublistaID),

	CONSTRAINT [FK_dbo.Sublista_dbo.CorreoId] FOREIGN KEY (SublistaID, CorreoDuenoSublista)
		REFERENCES [dbo].[Sublista] (Id, CorreoDueno),

	CONSTRAINT [FK_dbo.Sublista_dbo.CorreosListaAmigo] FOREIGN KEY (CorreoDueno, CorreoAmigo)
		REFERENCES [dbo].[ListaAmigos] (CorreoDueno, CorreoAmigo) ON DELETE CASCADE
)
