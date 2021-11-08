CREATE TABLE [dbo].[Contrasenas_Anteriores]
(
	[Correo] NVARCHAR(50) NOT NULL,
    [Contrasena] NVARCHAR(16) NOT NULL
	PRIMARY KEY ([Correo] ASC, [Contrasena] ASC),
	[Fecha_Ingreso] DATETIME NOT NULL, 
    CONSTRAINT [FK_dbo.Contrasenas_Anteriores_dbo.Registrado] FOREIGN KEY ([Correo])
		REFERENCES [dbo].Registrado ([Correo])
)

