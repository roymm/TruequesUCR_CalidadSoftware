CREATE TABLE [dbo].[Producto]
(
	[ProductoID]		INT				IDENTITY (1, 1) NOT NULL , 
	[CorreoCliente]		NVARCHAR(50)	NOT NULL ,
	[Nombre]			NVARCHAR(50)	NOT NULL,
	[PrecioEstimado]	FLOAT			NOT NULL, 
	[Condicion]			NCHAR(50)		NOT NULL,
	[Descripcion]		TEXT			NULL,
	[ProductoImagen1]	IMAGE			NULL,	
	[ProductoImagen2]	IMAGE			NULL,	
	[ProductoImagen3]	IMAGE			NULL,	
	[Publicado]			BIT				NOT NULL	DEFAULT		0, 
	[FechaRegistrado]	DATETIME		NOT NULL, 
	[FechaPublicado]	DATETIME		NULL		DEFAULT NULL , 
	[Calificacion]		SMALLINT		NULL		DEFAULT NULL,
	[Estado]			BIT				NULL		DEFAULT 1, --Variable para el borrado logico
	[Seleccionado]		BIT				NOT NULL	DEFAULT 0,
	[PublicoEspecial]	SMALLINT		NOT NULL	DEFAULT 0,
	
	
	PRIMARY KEY CLUSTERED (ProductoID, CorreoCliente),

	CONSTRAINT [FK_dbo.Producto_dbo.Cliente] FOREIGN KEY ([CorreoCliente])
		REFERENCES [dbo].[Cliente] ([Correo]) 
		--ON DELETE CASCADE	
		--Ahora esta se maneja mediante triggers.

)
