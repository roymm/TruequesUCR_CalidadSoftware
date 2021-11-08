CREATE PROCEDURE [dbo].[spFisico_Delete]
	@ProductoID int = 0
AS
BEGIN
	DELETE FROM Fisico
	WHERE ProductoID = @ProductoID
END
