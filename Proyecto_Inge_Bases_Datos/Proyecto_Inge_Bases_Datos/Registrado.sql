CREATE TABLE [dbo].[Registrado]
(
	[Correo]			NVARCHAR (50) 	NOT NULL,
	[Contrasena]		NVARCHAR (300)	NOT NULL,
	[Nombre]			NVARCHAR (20)	NULL,
	[Apellido1]			NVARCHAR (20)	NULL,
	[Apellido2]			NVARCHAR (20)	NULL,
	[EstadoCuenta]		BIT				NOT NULL DEFAULT 1,
	[CodigoCambioContrasena] NVARCHAR(100) NULL,
	[Bloqueo]			BIT				NOT NULL DEFAULT 0,
    PRIMARY KEY ([Correo] ASC)
)
