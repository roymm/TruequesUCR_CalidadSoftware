/****** Object:  Trigger [dbo].[Borrar_Trueques_En_Los_Que_Participa_Producto]    Script Date: 5/29/2020 8:21:54 PM ******/
-- =============================================
-- Author:		<Yulian Loaiza>
-- Create date: <29 May 2020>
-- Description:	<No podíamos eliminar productos si estaban en trueques, este trigger se encarga de borrar los trueques en los que esté un producto antes de eliminar dicho producto. Esto nos salva de la limitación que tenía el motor de SQL Server por multiple cascading paths>
-- =============================================
CREATE TRIGGER [dbo].[Borrar_Trueques_En_Los_Que_Participa_Producto] --Antes de borrar un producto, se deben borrar todos los trueques en los que el participa
   ON  [dbo].[Producto]
   INSTEAD OF DELETE
   AS
   BEGIN
   SET NOCOUNT ON;
   DELETE FROM	Subasta WHERE ProductoIDSubastado IN (SELECT ProductoID FROM DELETED)
   DELETE FROM	Trueque WHERE ProductoIDPublicador IN (SELECT ProductoID FROM DELETED) --Hace un IN osea, que en la que los valores sean iguales. Quito los trueques en donde a este producto le hagan ofertas.
   DELETE FROM	Trueque WHERE ProductoIDOfertante IN (SELECT ProductoID FROM DELETED) --Quito los trueques en donde este producto esté siendo ofrecido.
   DELETE FROM Producto WHERE ProductoID IN (SELECT ProductoID FROM DELETED) --Remuevo el producto de la tabla Producto
   END 

