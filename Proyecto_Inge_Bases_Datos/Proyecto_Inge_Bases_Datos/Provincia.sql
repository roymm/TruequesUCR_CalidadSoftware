CREATE TABLE [dbo].[Provincia]
(
	[Nombre]		NVARCHAR (50) NOT NULL PRIMARY KEY,
	[NombrePais]	NVARCHAR (50) NOT NULL,
	CONSTRAINT [FK_dbo.Provincia_dbo.Pais] FOREIGN KEY ([NombrePais])
		REFERENCES [dbo].Pais ([Nombre]) ON DELETE CASCADE
)
