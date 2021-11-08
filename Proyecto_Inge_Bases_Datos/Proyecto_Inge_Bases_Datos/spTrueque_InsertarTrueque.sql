CREATE PROCEDURE spTrueque_InsertarTrueque
				(@ProductoIDPublicador		INT			 ,
				 @CorreoPublicador			NVARCHAR(50) ,
				 @ProductoIDOfertante		INT			 ,
				 @CorreoOfertante			NVARCHAR(50) ,
				 @FechaInicio				DATETIME	 ,
				 @Mensaje					NVARCHAR(max) = NULL,
				 @Finalizado				DATETIME = NULL ,
				 @Calificacion				SMALLINT = NULL)
AS

INSERT INTO Trueque(ProductoIDPublicador, CorreoPublicador, ProductoIDOfertante, CorreoOfertante, FechaInicio, Mensaje, Finalizado, Calificacion)
VALUES (@ProductoIDPublicador, @CorreoPublicador, @ProductoIDOfertante, @CorreoOfertante, @FechaInicio, @Mensaje, @Finalizado, @Calificacion)
