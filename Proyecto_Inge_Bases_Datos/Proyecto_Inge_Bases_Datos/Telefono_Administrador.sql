CREATE TABLE [dbo].[Telefono_Administrador]
(
	[Correo] NVARCHAR (50) NOT NULL,
	[Telefono] NVARCHAR (14) NOT NULL PRIMARY KEY,
	CONSTRAINT [FK_dbo.Telefono_Administrador_dbo.Administrador_Correo] 
	FOREIGN KEY ([Correo]) REFERENCES [dbo].[Administrador] ([Correo]) ON DELETE CASCADE
)
