CREATE PROCEDURE [dbo].[spVirtual_Delete]
	@ProductoID int = 0
AS
BEGIN
	DELETE FROM Virtual
	WHERE ProductoID = @ProductoID
END
