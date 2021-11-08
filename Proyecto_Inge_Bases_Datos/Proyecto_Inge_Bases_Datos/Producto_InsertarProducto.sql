CREATE PROCEDURE [dbo].[Producto_InsertarProducto]
	@CorreoCliente NVARCHAR(50) ,
	@Nombre			NVARCHAR(50)	,
	@PrecioEstimado	FLOAT			,
	@Condicion		NCHAR(50)		,
	@Descripcion		TEXT		,
	@PathImagen1		NVARCHAR(MAX),
	@PathImagen2		NVARCHAR(MAX),
	@PathImagen3		NVARCHAR(MAX),
	@Publicado			BIT	= 0, 
	@FechaRegistrado DATETIME2
AS
	BEGIN
		INSERT INTO Producto (CorreoCliente, Nombre, PrecioEstimado, Condicion, Descripcion, PathImagen1, PathImagen2, PathImagen3, Publicado, FechaRegistrado)
		VALUES (@CorreoCliente, @Nombre, @PrecioEstimado, @Condicion, @Descripcion, @PathImagen1, @PathImagen2, @PathImagen3, @Publicado, @FechaRegistrado)
	END
	--SELECT @CorreoCliente, @Nombre, @PrecioEstimado, @Condicion, @Descripcion, @PathImagen1, @PathImagen2, @PathImagen3, @Publicado, @FechaRegistrado
RETURN 0
