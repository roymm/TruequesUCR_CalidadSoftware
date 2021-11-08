CREATE TABLE [dbo].[Cliente]
(
	[Correo]					NVARCHAR (50)	NOT NULL,
	[FechaNacimiento]			DATETIME2 		NULL,
	[Canton]					INT				NULL,
	[DireccionExacta]			NVARCHAR (100)	NULL,
	[FotoPerfil]				IMAGE			NULL,
	[FechaCierre]				DATETIME		NULL,
	[CalificacionPromedio]		TINYINT			NULL,
	[BloquearNotificaciones] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY ([Correo] ASC),
	CONSTRAINT [FK_dbo.Cliente_dbo.Registrado] FOREIGN KEY ([Correo])
		REFERENCES [dbo].Registrado ([Correo]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Cliente_dbo.Canton] FOREIGN KEY ([Canton])
		REFERENCES [dbo].Canton ([id])
)
