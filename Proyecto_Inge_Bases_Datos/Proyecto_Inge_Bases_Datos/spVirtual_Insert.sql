CREATE PROCEDURE [dbo].[spVirtual_Insert]


    @Nombre NVARCHAR(50),
    @CorreoCliente NVARCHAR(50),
    @PrecioEstimado FLOAT,
    @Condicion NCHAR(50),
    @Descripcion TEXT,
    @ProductoImagen1 IMAGE,
    @ProductoImagen2 IMAGE,
    @ProductoImagen3 IMAGE,
    @Publicado BIT,
    @FechaRegistrado DATETIME,
    @ListaCategorias NVARCHAR(100),


    --  Caracteristicas del virtual
    @TipoDeArchivo NVARCHAR(50),
    @DerechosDeProducto NVARCHAR(50) NULL,
    @Fuentes NVARCHAR(50) NULL,
    @FechaExpiracion DATETIME NULL

AS
BEGIN
        DECLARE @ProductoVirtualID INT

        INSERT INTO [Producto] ([Nombre], [CorreoCliente], [PrecioEstimado], [Condicion], [Descripcion], [ProductoImagen1], [ProductoImagen2], [ProductoImagen3], [Publicado], [FechaRegistrado])
            VALUES(@Nombre, @CorreoCliente, @PrecioEstimado, @Condicion, @Descripcion, @ProductoImagen1, @ProductoImagen2, @ProductoImagen3, @Publicado, @FechaRegistrado)

        SET @ProductoVirtualID = (
            SELECT MAX(ProductoID)
            FROM Producto
        )

         CREATE TABLE #CategoriaContieneProductoTemp
      (
	        [IDCategoria] INT  NULL,
	        [ProductoID] INT  NULL,
	        [CorreoClienteProducto]  NVARCHAR(50)  NULL,
      )

    INSERT INTO #CategoriaContieneProductoTemp (IDCategoria)
        SELECT value FROM STRING_SPLIT(@Listacategorias, ',');

    UPDATE #CategoriaContieneProductoTemp
    SET ProductoID = @ProductoVirtualID, CorreoClienteProducto = @CorreoCliente;

    INSERT INTO [CategoriaContieneProductos] ([IDCategoria], [ProductoID], [CorreoClienteProducto])
    SELECT [IDCategoria], [ProductoID], [CorreoClienteProducto]
    FROM  #CategoriaContieneProductoTemp


    INSERT INTO [Virtual] ([ProductoID], [CorreoCliente], [TipoDeArchivo], [DerechosDeProducto], [Fuentes], [FechaExpiracion])
        VALUES(@ProductoVirtualID, @CorreoCliente, @TipoDeArchivo, @DerechosDeProducto, @Fuentes, @FechaExpiracion)
END