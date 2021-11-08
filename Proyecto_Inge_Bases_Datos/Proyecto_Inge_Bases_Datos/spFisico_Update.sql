CREATE PROCEDURE [dbo].[spFisico_Update]
	
	-- Para updatear el correo del dueño, nada mas, el id no se puede editar ya cuando se crea
	@ProductoID int = 0,
	@CorreoCliente NVARCHAR (50)
AS
BEGIN
	UPDATE Fisico
	 SET CorreoCliente = @CorreoCliente
	 WHERE ProductoID = @ProductoID
END
