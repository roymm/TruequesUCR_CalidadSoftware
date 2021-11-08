CREATE TABLE [dbo].[Relacion_Subasta_Visible_A]
(
	[CorreoDuenoSublista] NVARCHAR(50) NOT NULL,
	[SublistaID] INT NOT NULL,
	[ProductoID] INT NOT NULL,
	[CorreoDuenoSubasta] NVARCHAR(50) NOT NULL,
	[FechaPublicado] DATETIME
	
	
	PRIMARY KEY CLUSTERED ([CorreoDuenoSublista], [SublistaID], [ProductoID], [CorreoDuenoSubasta], [FechaPublicado]),
	
	CONSTRAINT [FK_dbo.Relacion_Subasta_Visible_A_dbo.Subasta] FOREIGN KEY (ProductoID, CorreoDuenoSubasta, FechaPublicado) 
		REFERENCES [dbo].[Subasta] (ProductoIDSubastado ,CorreoSubastador, FechaPublicado),

	CONSTRAINT [FK_dbo.Relacion_Subasta_Visible_A_dbo.Sublista] FOREIGN KEY (SublistaID, CorreoDuenoSublista) 
		REFERENCES [dbo].[Sublista] (Id ,CorreoDueno)
)
