CREATE PROCEDURE [dbo].[sp_Subasta_Select]
	@Correo nvarchar (50)
AS
	Select * from Subasta
		where Subasta.PublicoEspecial = 0 
		or @Correo =Subasta.CorreoSubastador

		or
			(Subasta.PublicoEspecial = 1 and exists(Select * from ListaAmigos
			where ListaAmigos.CorreoDueno = Subasta.CorreoSubastador and ListaAmigos.CorreoAmigo = @Correo)) 

		or
			(Subasta.PublicoEspecial = 2 and exists (Select * 
			from Relacion_Sublista_Amigos
			inner Join Relacion_Subasta_Visible_A
			on  Relacion_Sublista_Amigos.SublistaID = Relacion_Subasta_Visible_A.SublistaID and 
				Relacion_Sublista_Amigos.CorreoDuenoSublista = Relacion_Subasta_Visible_A.CorreoDuenoSublista
			where Relacion_Subasta_Visible_A.CorreoDuenoSublista = Subasta.CorreoSubastador and
				  Relacion_Subasta_Visible_A.ProductoID = Subasta.ProductoIDSubastado and
				  Relacion_Subasta_Visible_A.FechaPublicado = Subasta.FechaPublicado and
				  Relacion_Sublista_Amigos.CorreoAmigo = @Correo)) 
RETURN 0
