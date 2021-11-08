
/*Esta pasando un error con las constraints. Es una limitación del motor de datos. Por ello, vamos a tener que hacer
lo que teníamos planeado con triggers probablemente*/

CREATE TABLE [dbo].[Trueque]
(
	[ProductoIDPublicador]	INT				NOT NULL,
	[CorreoPublicador]		NVARCHAR(50)	NOT NULL,
	[ProductoIDOfertante]	INT				NOT NULL,
	[CorreoOfertante]		NVARCHAR(50)	NOT NULL,
	[FechaInicio]			DATETIME		NOT NULL,
	[Mensaje]				NVARCHAR(MAX)	NULL,
	[Finalizado]			DATETIME		NULL,
	[Calificacion]			SMALLINT		NULL DEFAULT NULL,
	[FechaEntrega]			DATETIME		NULL DEFAULT NULL, 
	--[UbicacionEntrega]		NVARCHAR(50)	NULL DEFAULT NULL, 
	--[TelefonoEntrega]		NCHAR(50)		NULL DEFAULT NULL, 

	PRIMARY KEY CLUSTERED(ProductoIDPublicador, CorreoPublicador, ProductoIDOfertante, CorreoOfertante, FechaInicio),

	/*CorreoPublicante, ID publicante*/
	CONSTRAINT [FK_dbo.Trueque_dbo.Producto_ConjuntoPublicador] FOREIGN KEY (ProductoIDPublicador, CorreoPublicador)
	REFERENCES [dbo].[Producto] (ProductoID,CorreoCliente),
	--ON DELETE CASCADE,


	/*CorreoOfertante, ID Ofertante*/
	CONSTRAINT [FK_dbo.Trueque_dbo.Producto_ConjuntoOfertante] FOREIGN KEY (ProductoIDOfertante, CorreoOfertante)
	REFERENCES [dbo].[Producto] (ProductoID,CorreoCliente) 
	--ON DELETE CASCADE, 


	--ON DELETE manejadas por trigger.
)
