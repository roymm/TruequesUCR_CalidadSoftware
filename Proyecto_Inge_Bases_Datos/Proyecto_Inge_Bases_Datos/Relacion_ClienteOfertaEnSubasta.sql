CREATE TABLE [dbo].[Relacion_ClienteOfertaEnSubasta]
(
	[CorreoOfertador] NVARCHAR (50) NOT NULL,
	[ProductoIDSubastado] INT		NOT NULL ,
	[CorreoSubastador] NVARCHAR (50) NOT NULL,
	[FechaPublicado]    DATETIME NOT NULL,
	[PrecioOfrecido] FLOAT NOT NULL,
	
	PRIMARY KEY CLUSTERED ( CorreoOfertador, CorreoSubastador, ProductoIDSubastado, FechaPublicado),
	CONSTRAINT [FK_dbo.Relacion_ClienteOfertaEnSubasta_dbo.Subasta] FOREIGN KEY (  ProductoIDSubastado,CorreoSubastador, FechaPublicado)
	REFERENCES [dbo].[Subasta] (  ProductoIDSubastado,CorreoSubastador, FechaPublicado)
	ON DELETE CASCADE, -- ver esta vara

	CONSTRAINT [FK.dbo_Relacion_ClienteOfertaEnSubasta_dbo.Cliente]  FOREIGN KEY (CorreoOfertador)
	REFERENCES [dbo].[Cliente] (Correo)
)
