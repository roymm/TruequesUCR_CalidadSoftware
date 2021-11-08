CREATE PROCEDURE [dbo].[spSubasta_Contar]
	@correoSublista nvarchar(50),
	@SublistaId int,
	@ProductoID int,
	@correoSubasta nvarchar(50),
	@Fecha datetime,
	@PublicoEspecial smallint

AS
	if @PublicoEspecial = 1
		SELECT count(*) From ListaAmigos
		where ListaAmigos.CorreoDueno = @correoSublista
	else 
		SELECT count (*) From Relacion_Subasta_Visible_A
			inner join Relacion_Sublista_Amigos 
			on Relacion_Subasta_Visible_A.CorreoDuenoSublista = Relacion_Sublista_Amigos.CorreoDueno and
			Relacion_Subasta_Visible_A.SublistaID = Relacion_Sublista_Amigos.SublistaID
		where Relacion_Subasta_Visible_A.CorreoDuenoSubasta = @correoSubasta and
		Relacion_Subasta_Visible_A.CorreoDuenoSublista = @correoSublista and
		Relacion_Subasta_Visible_A.FechaPublicado = @Fecha and
		Relacion_Subasta_Visible_A.ProductoID = @ProductoID and
		Relacion_Subasta_Visible_A.SublistaID = @SublistaId
RETURN 0
