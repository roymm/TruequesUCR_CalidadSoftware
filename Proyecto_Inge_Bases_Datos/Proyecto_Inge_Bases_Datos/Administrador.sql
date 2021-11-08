CREATE TABLE [dbo].[Administrador]
(
	[Correo] NVARCHAR (50) NOT NULL PRIMARY KEY,
	CONSTRAINT [FK_dbo.Administrador_dbo.Registrado_Correo] FOREIGN KEY ([Correo]) 
	REFERENCES [dbo].[Registrado] ([Correo]) ON DELETE CASCADE
)

