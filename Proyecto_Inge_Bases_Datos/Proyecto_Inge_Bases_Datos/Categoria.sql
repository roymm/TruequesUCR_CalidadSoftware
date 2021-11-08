CREATE TABLE [dbo].[Categoria]
(
	[CategoriaID]    INT				IDENTITY (1, 1) NOT NULL , 
	[Nombre] NVARCHAR(50) NOT NULL,
	[CorreoUsuarioRegistrado] NVARCHAR(50) NOT NULL,
	[CategoriaPadreID] INT NULL,
	[EsCategoriaFisica] BIT NULL, --si no, es virtual
	PRIMARY KEY CLUSTERED (CategoriaID),
	CONSTRAINT [FK_dbo.Categoria_dbo.Registrado] FOREIGN KEY ([CorreoUsuarioRegistrado])
	 REFERENCES [dbo].[Registrado] ([Correo]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Categoria_dbo.Categoria] FOREIGN KEY ([CategoriaPadreID])
	 REFERENCES [dbo].[Categoria] ([CategoriaID]) --ON DELETE CASCADE,
)
