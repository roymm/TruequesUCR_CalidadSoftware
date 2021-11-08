CREATE PROCEDURE [dbo].[ExisteInteres]
@interes int,
@amigo nvarchar(50)
AS
BEGIN
declare @existe bit;
IF EXISTS (
	Select IDCategoria from Relacion_ClienteTieneInteresCategoria as RE
	Where  RE.CorreoCliente = @amigo AND RE.IDCategoria=@interes)
	SET @existe = 1;
ElSE
	SET @existe = 0;
RETURN @existe
END
