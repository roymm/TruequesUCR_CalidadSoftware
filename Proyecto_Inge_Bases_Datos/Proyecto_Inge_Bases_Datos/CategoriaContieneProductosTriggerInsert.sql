CREATE TRIGGER [dbo].[Insertar_En_Categoria_Contiene_Productos] --Este Trigger es para que cuando se agrega un producto a una subcategoria, tambien se agregue el mismo producto al a categoria padre, y repite el proceso hasta llegar a la categoria raiz
ON [dbo].[CategoriaContieneProductos]
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON


	DECLARE @categoria_actual INT, @producto INT, @correo NVARCHAR(50), @padre INT;

	Select Top 1 @categoria_actual = i.IDCategoria, @producto = i.ProductoID, @correo = i.CorreoClienteProducto
    FROM inserted i

	SELECT Top 1 @padre = c.CategoriaPadreID
	FROM Categoria c
	WHERE c.CategoriaID = @categoria_actual

	WHILE @padre != 0 --repetir hasta llegar a la raiz
	BEGIN
		 INSERT INTO [dbo].[CategoriaContieneProductos] (IDCategoria, ProductoID, CorreoClienteProducto)
	     VALUES (@padre, @producto, @correo);

		 SET @categoria_actual = @padre
		 SELECT Top 1 @padre = c.CategoriaPadreID
		 FROM Categoria c
		 WHERE c.CategoriaID = @categoria_actual
	END


	--IF @padre != 0 -- Si la categoria padre no es la raiz, entonces vuelve a ejecutarse el insert
	--	INSERT INTO [dbo].[CategoriaContieneProductos] (IDCategoria, ProductoID, CorreoClienteProducto)
	--	VALUES (@padre, (SELECT ProductoID FROM inserted), (SELECT CorreoClienteProducto FROM inserted));


	
 --   SELECT @categoria_actual = i.IDCategoria, @producto = i.ProductoID, @correo = i.CorreoClienteProducto
 --   FROM inserted i

	--SELECT @padre = c.CategoriaPadreID
	--FROM Categoria c
	--WHERE c.CategoriaID = @categoria_actual
 

	--INSERT INTO CategoriaContieneProductos (IDCategoria, ProductoID, CorreoClienteProducto) -- En la relacion se crea la tupla entre el producto y la categoria seleccionada
	----@categoria_actual, @producto, @correo
	--SELECT IDCategoria, ProductoID, CorreoClienteProducto From inserted

	--IF @padre > 0 -- Si la categoria padre no es la raiz, entonces vuelve a ejecutarse el insert
	--	INSERT INTO [dbo].[CategoriaContieneProductos] (IDCategoria, ProductoID, CorreoClienteProducto)
	--	VALUES (@padre, (SELECT ProductoID FROM inserted), (SELECT CorreoClienteProducto FROM inserted));
	
END
