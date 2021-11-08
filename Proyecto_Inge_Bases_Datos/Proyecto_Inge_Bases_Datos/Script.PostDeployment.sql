/*
Post-Deployment Script Template                         
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.       
 Use SQLCMD syntax to include a file in the post-deployment script.         
 Example:      :r .\myfile.sql                              
 Use SQLCMD syntax to reference a variable in the post-deployment script.       
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                 
--------------------------------------------------------------------------------------
*/

DELETE FROM ListaAmigos;
--DBCC CHECKIDENT ('ListaAmigos', RESEED, 0);

DELETE FROM Sublista;
DBCC CHECKIDENT ('Sublista', RESEED, 0);

DELETE FROM Producto;

DBCC CHECKIDENT ('Producto', RESEED, 0);

DELETE FROM Cliente;
--DBCC CHECKIDENT ('Cliente', RESEED, 0);

DELETE FROM Categoria;
DBCC CHECKIDENT ('Categoria', RESEED, 0);

DELETE FROM Registrado;
--DBCC CHECKIDENT ('Registrado', RESEED, 0);

DELETE FROM Pais;
--DBCC CHECKIDENT ('Pais', RESEED, 0);

DELETE FROM Provincia;
--DBCC CHECKIDENT ('Provincia', RESEED, 0);

DELETE FROM Canton;
DBCC CHECKIDENT ('Canton', RESEED, 0);

/*
DELETE FROM Subasta
DBCC CHECKIDENT ('Subasta', RESEED, 0);
*/
DELETE FROM Trueque;
--DBCC CHECKIDENT ('Trueque', RESEED, 0);

DELETE FROM Relacion_ClienteTieneInteresCategoria;
--DBCC CHECKIDENT ('Relacion_ClienteTieneInteresCategoria', RESEED, 0);


DELETE FROM CategoriaContieneProductos;
--DBCC CHECKIDENT ('CategoriaContieneProductos', RESEED, 0);

DELETE FROM Administrador;

DELETE FROM Telefono_Administrador;

DELETE FROM Resena;

DELETE FROM Denuncia;

DELETE FROM Apelacion;

DELETE FROM Suspension;

MERGE INTO Registrado AS Target
USING (VALUES
    ('joalva18@gmail.com', '$s2$16384$8$1$gFVlIFJIC6oGp4KDVIXVk6YNClhLRwDaITu0QGgIQVs=$h+Xk91CYk35cZrdHujfJKGOS+iKtH9pDQiUeJrFxw3s=', 'Johan', 'Murillo', 'Alvarado', 1,0),
    ('mariovargasc97@gmail.com', '$s2$16384$8$1$gFVlIFJIC6oGp4KDVIXVk6YNClhLRwDaITu0QGgIQVs=$h+Xk91CYk35cZrdHujfJKGOS+iKtH9pDQiUeJrFxw3s=', 'Mario', 'Vargas', 'Campos', 1,0),
    ('richixalfaro@gmail.com', '$s2$16384$8$1$gFVlIFJIC6oGp4KDVIXVk6YNClhLRwDaITu0QGgIQVs=$h+Xk91CYk35cZrdHujfJKGOS+iKtH9pDQiUeJrFxw3s=', 'Richix', 'Alfaro', 'Viquez', 1,0),
    ('manuel@gmail.com','$s2$16384$8$1$gFVlIFJIC6oGp4KDVIXVk6YNClhLRwDaITu0QGgIQVs=$h+Xk91CYk35cZrdHujfJKGOS+iKtH9pDQiUeJrFxw3s=', 'Manuel', 'Fernandez', 'Carrillo', 1,0),
    ('juangarro05@gmail.com','$s2$16384$8$1$gFVlIFJIC6oGp4KDVIXVk6YNClhLRwDaITu0QGgIQVs=$h+Xk91CYk35cZrdHujfJKGOS+iKtH9pDQiUeJrFxw3s=', 'Juan', 'Garro', 'Nunez', 1,0),
    ('isaac@gmail.com', '$s2$16384$8$1$gFVlIFJIC6oGp4KDVIXVk6YNClhLRwDaITu0QGgIQVs=$h+Xk91CYk35cZrdHujfJKGOS+iKtH9pDQiUeJrFxw3s=', 'Isaac', 'Rojas', 'Sanchez', 1,0),
    ('community.org.cr@gmail.com', '$s2$16384$8$1$gFVlIFJIC6oGp4KDVIXVk6YNClhLRwDaITu0QGgIQVs=$h+Xk91CYk35cZrdHujfJKGOS+iKtH9pDQiUeJrFxw3s=', 'Javier', 'Cascante', 'Sanchez', 0,0),
    ('tt6033406@gmail.com', '$s2$16384$8$1$evdzPMvfFv/ANYP3ZkLnyRziT8hb/lTcxBxzDCs5X14=$TErWz9J4gzPdpOqqq4WcmuUhxqbiZF+fc/NnJxWzxbg=', 'Tester', 'Test', 'Testing', 1,0),
	('testerAd1234@gmail.com', '$s2$16384$8$1$31p+8bEfDo6hokdS6+lz2kYqgtePEX71YWtTljN9wlQ=$VkBt9KNER2iWEhdo5QO0vUzJCOpsDqNnLGoVmvecEbI=', 'TesterAdmin', 'TestAdmin', 'TestingAdmin', 1,0)
)
AS Source ([Correo], Contrasena, Nombre, Apellido1, Apellido2, EstadoCuenta,Bloqueo)
ON Target.Correo = Source.Correo
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Correo, Contrasena, Nombre, Apellido1, Apellido2, EstadoCuenta,Bloqueo)
VALUES (Correo, Contrasena, Nombre, Apellido1, Apellido2, EstadoCuenta,Bloqueo);

MERGE INTO Pais AS Target
USING (VALUES
        ('Costa Rica')
)
AS Source ([Nombre])
ON Target.Nombre = Source.Nombre
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Nombre)
VALUES (Nombre);

MERGE INTO Provincia AS Target
USING (VALUES
        ('San José','Costa Rica'),
        ('Alajuela','Costa Rica'),
        ('Heredia','Costa Rica'),
        ('Cartago','Costa Rica'),
        ('Puntarenas','Costa Rica'),
        ('Guanacaste','Costa Rica'),
        ('Limón','Costa Rica')
)
AS Source ([Nombre], NombrePais)
ON Target.Nombre = Source.Nombre
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Nombre, NombrePais)
VALUES (Nombre, NombrePais);

MERGE INTO Canton AS Target
USING (VALUES
         ('San Jose','San José'),
         ('Escazú','San José'),
         ('Desamparados','San José'),
         ('Puriscal','San José'),
         ('Tarrazú','San José'),
         ('Aserrí','San José'),
         ('Mora','San José'),
         ('Goicoechea','San José'),
         ('Santa Ana','San José'),
         ('Alajuelita','San José'),
         ('Vásquez de Coronado','San José'),
         ('Acosta','San José'),
         ('Tibás','San José'),
         ('Moravia','San José'),
         ('Montes de Oca','San José'),
         ('Turrubares','San José'),
         ('Dota','San José'),
         ('Curridabat','San José'),
         ('Pérez Zeledón','San José'),
         ('León Cortés Castro','San José'),
         ('Alajuela','Alajuela'),
         ('San Ramón','Alajuela'),
         ('Grecia','Alajuela'),
         ('San Mateo','Alajuela'),
         ('Cartago','Cartago'),
         ('Guácimo','Limón'),
         ('Matina','Limón'),
         ('Talamanca','Limón'),
         ('Siquirres','Limón'),
         ('Pococí','Limón'),
         ('Limón','Limón'),
         ('Garabito','Puntarenas'),
         ('Corredores','Puntarenas'),
         ('Parrita','Puntarenas'),
         ('Coto Brus','Puntarenas'),
         ('Golfito','Puntarenas'),
         ('Aguirre','Puntarenas'),
         ('Osa','Puntarenas'),
         ('Montes de Oro','Puntarenas'),
         ('Buenos Aires','Puntarenas'),
         ('Esparza','Puntarenas'),
         ('Puntarenas','Puntarenas'),
         ('Hojancha','Guanacaste'),
         ('La Cruz','Guanacaste'),
         ('Nandayure','Guanacaste'),
         ('Tilarán','Guanacaste'),
         ('Abangares','Guanacaste'),
         ('Cañas','Guanacaste'),
         ('Carrillo','Guanacaste'),
         ('Bagaces','Guanacaste'),
         ('Santa Cruz','Guanacaste'),
         ('Nicoya','Guanacaste'),
         ('Liberia','Guanacaste'),
         ('Guatuso','Alajuela'),
         ('Los Chiles','Alajuela'),
         ('Upala','Alajuela'),
         ('Valverde Vega','Alajuela'),
         ('Zarcero','Alajuela'),
         ('San Carlos','Alajuela'),
         ('Orotina','Alajuela'),
         ('Poás','Alajuela'),
         ('Palmares','Alajuela'),
         ('Naranjo','Alajuela'),
         ('Atenas','Alajuela'),
         ('Sarapiquí','Heredia'),
         ('San Pablo','Heredia'),
         ('Flores','Heredia'),
         ('Belén','Heredia'),
         ('San Isidro','Heredia'),
         ('San Rafael','Heredia'),
         ('Santa Bárbara','Heredia'),
         ('Santo Domingo','Heredia'),
         ('Barva','Heredia'),
         ('Heredia','Heredia'),
         ('El Guarco','Cartago'),
         ('Oreamuno','Cartago'),
         ('Alvarado','Cartago'),
         ('Turrialba','Cartago'),
         ('Jiménez','Cartago'),
         ('La Unión','Cartago'),
         ('Paraíso','Cartago')
)
AS Source ([Nombre], NombreProvincia)
ON Target.Nombre = Source.Nombre
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Nombre, NombreProvincia)
VALUES (Nombre, NombreProvincia);


/* AS 10/06/2020 - Occidente - Historia: OC-2.5 - Devs: Ricardo Alfaro B70257 Y Mario Vargas B67454 */
MERGE INTO Cliente AS Target
USING (VALUES
    ('joalva18@gmail.com', '1997/12/18', 0, 'Liceo', NULL, NULL, 5, 0),
    ('mariovargasc97@gmail.com', '1997-11-06', 1, 'Bomba', NULL, NULL, 4, 0),
    ('juangarro05@gmail.com','2000-05-24', 1, 'Casa', NULL, NULL, 1, 0),
    ('richixalfaro@gmail.com', '1998-12-07', 2, 'Plaza', NULL, NULL, 3, 0),
    ('community.org.cr@gmail.com', '1998-12-09', 2, 'Plaza', NULL,'2002-02-01', 3, 0),
    ('tt6033406@gmail.com', '1991/01/01', 0, 'Liceo', NULL, NULL, 5, 0)
)
AS Source ([Correo], FechaNacimiento, Canton, DireccionExacta, FotoPerfil, FechaCierre, CalificacionPromedio, BloquearNotificaciones)
ON Target.Correo = Source.Correo
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Correo, FechaNacimiento, Canton, DireccionExacta, FotoPerfil, FechaCierre, CalificacionPromedio, BloquearNotificaciones)
VALUES (Correo, FechaNacimiento, Canton, DireccionExacta, FotoPerfil, FechaCierre, CalificacionPromedio, BloquearNotificaciones);


MERGE INTO ListaAmigos AS Target
USING (VALUES
('joalva18@gmail.com', 'mariovargasc97@gmail.com'),
('joalva18@gmail.com', 'richixalfaro@gmail.com'),
('mariovargasc97@gmail.com', 'richixalfaro@gmail.com')
)
AS Source ([CorreoDueno], [CorreoAmigo])
ON Target.CorreoDueno = Source.CorreoDueno
WHEN NOT MATCHED BY TARGET THEN
INSERT (CorreoDueno, CorreoAmigo)
VALUES (CorreoDueno, CorreoAmigo);

MERGE INTO Sublista AS Target
USING (VALUES
('joalva18@gmail.com', 'Mejengas'),
('joalva18@gmail.com', 'Carros'),
('mariovargasc97@gmail.com', 'Carretas Tipicas')
)
AS Source ([CorreoDueno], [NombreSublista])
ON Target.CorreoDueno = Source.CorreoDueno
WHEN NOT MATCHED BY TARGET THEN
INSERT (CorreoDueno, NombreSublista)
VALUES (CorreoDueno, NombreSublista);


MERGE INTO Producto AS Target
USING (VALUES
    (1, 'Play Station 4 Como Nuevo!', 200000, 'Usado', 'Perfecto para el aparta. Tiene 2 juegos y el control original.  No manden ningun Xbox de cambio porfa.  Pueden verlo sin compromiso. ', NULL, NULL,  NULL,1, '2020-05-30T13:10:20', NULL, NULL, 'joalva18@gmail.com', 1),
    (2, 'Botas de vestir cafe de cuero', 25000, 'Nuevo', 'Cuero cubano, 100% real.  Ajuste ergonomico en el pie para caminar largas distancias.   Talla: 36', NULL, NULL, NULL, 1, '2020-05-30T13:10:20', NULL, NULL, 'mariovargasc97@gmail.com', 1),
    (3, 'Elmasri- Base De Datos Lo Cambio por otro',31000 , 'Usado', 'Muy bueno para aprender de basses de datos. Lo tengo en fisico, solo libros fisicos para el cambio. Estoy interesado mas que todo en ciencia y matematicas.  El libro esta sin subrayar y sin anotaciones. Como nuevo.', NULL, NULL, NULL, 1, '2020-05-30T13:10:20', NULL, NULL, 'joalva18@gmail.com', 1),
    (4, 'Audifonos Beats', 140000 , 'Nuevo', 'AUDÍFONOS ORIGINALES BEATS SOLO PRO WIRELESS NEGROS NUEVOS CON UNA AUTONOMÍA DE 22HRS D MÚSICA, BLUETOOTH,CONTESTADOR D LLAMADAS, SONIDO IMPECABLE LO MEJOR DEL MERCADO. en foto esta la página donde encuentra la información completa del artículo y precio real en el mercado.', NULL, NULL, NULL, 1, '2020-05-30T13:10:20', NULL, NULL, 'richixalfaro@gmail.com', 1),
    (5, 'El Diario de Greg 1 en pdf', 140000 , 'Nuevo', 'Un buen libro', NULL, NULL, NULL, 1, '2020-05-30T13:10:20', NULL, NULL, 'richixalfaro@gmail.com', 1)
    
    /* (5, 'Folleto de Quimica 1 Segundo Semestre 2019!', 5000 , 'Nuevo', 'Folleto para pasar quimica! Ya no lo ocupo entonces me gustaria intercambiarlo :)', NULL, NULL, 0, NULL, NULL, NULL),
    (6, '007 Quantum of Solace Digital', 6000, 'Nuevo' , 'Blu-Ray, cambio por otras peliculas preferiblemente de accion. ', NULL, NULL, 0, NULL, NULL, NULL),   
    (7, 'Pink Floyd The Dark Side of The Moon Vinilo', 30000 , 'Usado', 'Buenisimo para recuerdo.', NULL, NULL, 0, NULL, NULL, NULL),
    (8, 'Bufanda negra para hombre', 10000 , 'Nuevo', 'Tela 100% algodon. Se lava facil con agua fria. Muy util para estos frios! Pueden verla y probarsela sin compromiso.', NULL, NULL, 0, NULL, NULL, NULL),
    (9,'Televisor Sony 35 pulgadas', 50000 , 'Usado', 'Tiene un golpe en la parte de atras pero aparte funciona bien.', NULL, NULL, 0, NULL, NULL, NULL),
    (10,'Cuna azul para bebe', 115000 , 'Nuevo', 'Cunita para bebe pequeña. Si requieres una opción para salir y que tu bebé duerma como en casa. Estructura reforzada está fabricada en acero, fácil plegado.', NULL, NULL, 0, NULL, NULL, NULL),
    (11,'Estuche iPhone X, cambio por otro color', 6000 , 'Usado', 'Esta como nuevo, solo quiero cambiar el look de mi celular, que sean colores distintos porfa.', NULL, NULL, 0, NULL, NULL, NULL),
    (12,'DELL Inspiron Gaming 15 1700',400000 , 'Usado', 'Poco uso, muy buena.  Se puede usar para gaming.  Abierto a cambios, por favor ninguna Apple.  Comprada hace 3 meses pero por mi trabajo no la uso mucho.', NULL, NULL, 0, NULL, NULL, NULL),
    (13, 'The Power Of Now Eckhart Tolle', 3000 , 'Usado' , '-- Cambio por libro digital . El clasico que consagro a Eckhart Tolle como uno de los gurus mas importantes del mundo.', NULL, NULL, 0, NULL, NULL, NULL) */

)

/*  Preguntar si el insert es necesario que tenga  todos los atributos en el post deploymente script ???  */
AS Source ([ProductoID], Nombre, PrecioEstimado, Condicion, Descripcion, ProductoImagen1, ProductoImagen2, ProductoImagen3, Publicado, FechaRegistrado, FechaPublicado, Calificacion, CorreoCliente, Estado)
ON Target.ProductoID = Source.ProductoID AND Target.CorreoCliente = Source.CorreoCliente
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Nombre, PrecioEstimado, Condicion, Descripcion, ProductoImagen1, ProductoImagen2, ProductoImagen3, Publicado, FechaRegistrado, FechaPublicado, Calificacion, CorreoCliente, Estado)
VALUES (Nombre, PrecioEstimado, Condicion, Descripcion, ProductoImagen1, ProductoImagen2, ProductoImagen3, Publicado, FechaRegistrado, FechaPublicado, Calificacion, CorreoCliente, Estado);
/*
MERGE INTO Subasta AS Target
USING (VALUES
        (1, NULL, NULL, NULL)
)
AS Source ([Id], FechaInicio, FechaFin, Calificacion)
ON Target.Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN 
INSERT (FechaInicio, FechaFin, Calificacion)
VALUES (FechaInicio, FechaFin, Calificacion);
*/
/*
MERGE INTO Trueque AS Target
USING (VALUES
        (1, 'joalva18@gmail.com', 2, 'mariovargasc97@gmail.com', '2020-05-30T13:10:20', NULL, NULL, NULL),
        (3, 'joalva18@gmail.com', 4, 'richixalfaro@gmail.com', '2020-05-30T13:10:20', NULL, NULL, NULL)
)
AS Source ([ProductoIDPublicador], [CorreoPublicador], [ProductoIDOfertante], [CorreoOfertante], FechaInicio, Mensaje, Finalizado, Calificacion)
ON Target.ProductoIDPublicador = Source.ProductoIDPublicador AND Target.ProductoIDOfertante = Source.ProductoIDOfertante AND Target.CorreoPublicador = Source.CorreoPublicador AND Target.CorreoOfertante = Source.CorreoOfertante
WHEN NOT MATCHED BY TARGET THEN 
INSERT (ProductoIDPublicador, CorreoPublicador, ProductoIDOfertante, CorreoOfertante, FechaInicio, Mensaje, Finalizado, Calificacion)
VALUES (ProductoIDPublicador, CorreoPublicador, ProductoIDOfertante, CorreoOfertante, FechaInicio, Mensaje, Finalizado, Calificacion);
*/

MERGE INTO Categoria AS Target
USING (VALUES
       ('Categoría', 'isaac@gmail.com', NULL, 1),
       ('Entretenimiento', 'isaac@gmail.com',0 , 1),
       ('Educación', 'isaac@gmail.com',0, 1),
       ('Salud y Belleza', 'isaac@gmail.com', 0, 1),
       ('Deportes', 'isaac@gmail.com', 0, 1),
       ('Ferreteria', 'isaac@gmail.com', 0, 1),
       ('Hogar', 'isaac@gmail.com',0, 1),
       ('Peliculas', 'isaac@gmail.com',1 , 1),
       ('Juegos', 'isaac@gmail.com',1 , 1),
       ('Libros', 'isaac@gmail.com',1 , 1),
       ('Tecnología', 'isaac@gmail.com',0 , 1),
       ('Material educativo', 'isaac@gmail.com',2 , 1),
       ('Articulos educativos', 'isaac@gmail.com',2, 1),
       ('Acción', 'isaac@gmail.com',7 , 1),
       ('Prendas de vestir', 'isaac@gmail.com',0 , 1),
       ('Calzado', 'isaac@gmail.com',14 , 1),
       ('Camisas', 'isaac@gmail.com',14 , 1),
       ('Pantalones', 'isaac@gmail.com',14 , 1)
) AS Source ([Nombre],CorreoUsuarioRegistrado,CategoriaPadreID,EsCategoriaFisica)
ON Target.Nombre = Source.Nombre
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Nombre, CorreoUsuarioRegistrado, CategoriaPadreID, EsCategoriaFisica)
VALUES (Nombre, CorreoUsuarioRegistrado, CategoriaPadreID, EsCategoriaFisica);


--Dev Dayana Marín Mayorga Inicio Actividad Supervisada--
--Dev José Chavés Hurtado Inicio Actividad Supervisada--
--REB22 / Subtarea: Modificar Scrit.PostDeployment--
MERGE INTO CategoriaContieneProductos AS Target
USING (VALUES
 ( 8 , 1 ,  'joalva18@gmail.com'),
 ( 15 , 2 ,  'mariovargasc97@gmail.com'),
 ( 11 , 3 ,  'joalva18@gmail.com'),
 ( 10 , 4 ,  'richixalfaro@gmail.com'),
 ( 9 , 5 ,  'richixalfaro@gmail.com')
 )
AS Source (IDCategoria,ProductoID,CorreoClienteProducto)
ON Target.IDCategoria = Source.IDCategoria
WHEN NOT MATCHED BY TARGET THEN 
INSERT (IDCategoria,ProductoID,CorreoClienteProducto)
VALUES (IDCategoria,ProductoID,CorreoClienteProducto);

MERGE INTO Administrador AS Target
USING (VALUES
       ('manuel@gmail.com'),
       ('isaac@gmail.com'),
       ('testerAd1234@gmail.com')
) AS Source ([Correo])
ON Target.Correo = Source.Correo
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Correo)
VALUES (Correo);

MERGE INTO Telefono_Administrador AS Target
USING (VALUES
       ('manuel@gmail.com','88888888'),
       ('isaac@gmail.com', '77777777'),
       ('isaac@gmail.com', '66666666'),
       ('testerAd1234@gmail.com','99999999')
) AS Source ([Correo], Telefono)
ON Target.Correo = Source.Correo
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Correo, Telefono)
VALUES (Correo, Telefono);


MERGE INTO Resena AS Target
USING (VALUES
       ('joalva18@gmail.com', 'mariovargasc97@gmail.com',5, '12:10:16.1234567'),
       ('joalva18@gmail.com', 'mariovargasc97@gmail.com',3, '11:10:16.1234567'),
       ('joalva18@gmail.com', 'mariovargasc97@gmail.com',2, '14:10:16.1234567')
) 
AS Source ([Correo_Cliente_Calificado], [Correo_Cliente_Calificador], Valor, Fecha)
ON Target.Fecha = Source.Fecha
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Correo_Cliente_Calificado, Correo_Cliente_Calificador, Valor, Fecha )
VALUES (Correo_Cliente_Calificado, Correo_Cliente_Calificador, Valor, Fecha );

MERGE INTO Relacion_ClienteTieneInteresCategoria AS Target
USING (VALUES
        ('joalva18@gmail.com', 1)
)
AS Source ([CorreoCliente], [IDCategoria])
ON Target.CorreoCliente = Source.CorreoCliente
WHEN NOT MATCHED BY TARGET THEN 
INSERT (CorreoCliente, IDCategoria)
VALUES (CorreoCliente, IDCategoria);

MERGE INTO Denuncia AS Target
USING (VALUES
        ('12:10:16', NULL, 'joalva18@gmail.com','mariovargasc97@gmail.com',2,1),
        ('12:10:16', NULL, 'joalva18@gmail.com','richixalfaro@gmail.com',4,1)
)
AS Source ([Fecha], Comentarios, Denunciante, Denunciado,ProductoID, Tipo)
ON Target.Fecha = Source.Fecha
WHEN NOT MATCHED BY TARGET THEN
INSERT (Fecha, Comentarios, Denunciante, Denunciado, ProductoID, Tipo)
VALUES (Fecha, Comentarios, Denunciante, Denunciado, ProductoID, Tipo);