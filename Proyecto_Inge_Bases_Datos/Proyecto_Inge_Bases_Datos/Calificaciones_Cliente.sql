CREATE VIEW [dbo].[Calificaciones_Cliente]
AS SELECT  c.Correo, r.Nombre,	SUM (re.Valor) as SumaCalificaciones,  COUNT(re.Correo_Cliente_Calificado) as CantidadResenas, YEAR(c.FechaNacimiento) as fechaNacimiento,r.Bloqueo
FROM Cliente c
LEFT OUTER JOIN  Registrado r 
ON c.Correo = r.Correo
LEFT OUTER JOIN Resena re 
ON r.Correo = re.Correo_Cliente_Calificado
GROUP BY c.Correo, r.Nombre, c.CalificacionPromedio, c.FechaNacimiento,r.Bloqueo;