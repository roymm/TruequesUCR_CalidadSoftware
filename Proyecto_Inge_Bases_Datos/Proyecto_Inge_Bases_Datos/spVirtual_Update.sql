CREATE PROCEDURE [dbo].[spVirtual_Update]
	
	-- Para updatear el correo del dueño, nada mas, el id no se puede editar ya cuando se crea
	@ProductoID int = 0,
	@CorreoCliente NVARCHAR (50),
	@TipoDeArchivo NVARCHAR(50),
	@DerechosDeProducto NVARCHAR(50) NULL,
	@Fuentes NVARCHAR(50) NULL,
	@FechaExpiracion DATETIME NULL
	
AS
BEGIN
	UPDATE Virtual
	 SET CorreoCliente = @CorreoCliente,
		TipoDeArchivo = @TipoDeArchivo,
		DerechosDeProducto = @DerechosDeProducto,
		Fuentes = @Fuentes,
		FechaExpiracion = @FechaExpiracion

	 WHERE ProductoID = @ProductoID
END
