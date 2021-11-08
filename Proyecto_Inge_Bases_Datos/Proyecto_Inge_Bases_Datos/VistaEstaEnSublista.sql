CREATE VIEW [dbo].[VistaEstaEnSublista]
	AS SELECT   Sub.CorreoDueno, Sub.CorreoAmigo, Reg.Nombre, Reg.Apellido1, Reg.Apellido2, Sub.SublistaID
FROM         dbo.Relacion_Sublista_Amigos AS Sub INNER JOIN
                         dbo.Registrado AS Reg ON Sub.CorreoAmigo = Reg.Correo
