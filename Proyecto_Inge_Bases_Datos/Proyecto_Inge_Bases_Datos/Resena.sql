CREATE TABLE [dbo].[Resena]
(
	[Correo_Cliente_Calificado]  NVARCHAR(50) NOT NULL,
	[Correo_Cliente_Calificador] NVARCHAR(50) NOT NULL,
	[Valor]	TINYINT NULL,
	[Fecha] DATETIME2 NOT NULL PRIMARY KEY,
	[AtributoNuevo] NCHAR(10) NULL DEFAULT NULL, 
    [AtributoNuevo2] NCHAR(10) NULL DEFAULT NULL, 
    [AtributoNuevo3] NCHAR(10) NULL DEFAULT NULL, 
    CONSTRAINT [FK_dbo.Resena_dbo.Cliente_Correo] FOREIGN KEY (Correo_Cliente_Calificado)
	REFERENCES [dbo].[Cliente] (Correo),

	CONSTRAINT [FK_dbo.Resena_dbo.Cliente_Correo_2] FOREIGN KEY (Correo_Cliente_Calificador)
	REFERENCES [dbo].[Cliente] (Correo)
)