CREATE PROCEDURE spRelacion_Producto_Visible_A_Insertar
				(@CorreoDuenoLista			NVARCHAR(50) ,
				 @SublistaID				INT			 ,
				 @ProductoID				INT			 ,
				 @CorreoDuenoProducto		NVARCHAR(50) )
AS

INSERT INTO Relacion_Producto_Visible_A(CorreoDuenoSublista, SublistaID, ProductoID, CorreoDuenoProducto)
VALUES (@CorreoDuenoLista, @SublistaID, @ProductoID, @CorreoDuenoProducto);
