CREATE TABLE [dbo].[Canton]
(
	[id]				INT IDENTITY (1,1)	NOT NULL PRIMARY KEY,
	[Nombre]			NVARCHAR (50)		NOT NULL,
	[NombreProvincia]	NVARCHAR (50)		NOT NULL,
	CONSTRAINT [FK_dbo.Canton_dbo.Provincia] FOREIGN KEY ([NombreProvincia])
		REFERENCES [dbo].Provincia ([Nombre])
)
