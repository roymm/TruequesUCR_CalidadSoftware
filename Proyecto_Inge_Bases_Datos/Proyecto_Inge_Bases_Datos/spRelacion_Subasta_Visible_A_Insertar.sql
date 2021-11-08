CREATE PROCEDURE spRelacion_Subasta_Visible_A_Insertar
				(@CorreoDuenoLista			NVARCHAR(50) ,
				 @SublistaID				INT			 ,
				 @ProductoID				INT			 ,
				 @CorreoDuenoSubasta		NVARCHAR(50) ,
				 @FechaPublicado			DATETIME)
AS

INSERT INTO Relacion_Subasta_Visible_A(CorreoDuenoSublista, SublistaID, ProductoID, CorreoDuenoSubasta, FechaPublicado)
VALUES (@CorreoDuenoLista, @SublistaID, @ProductoID, @CorreoDuenoSubasta, @FechaPublicado);
