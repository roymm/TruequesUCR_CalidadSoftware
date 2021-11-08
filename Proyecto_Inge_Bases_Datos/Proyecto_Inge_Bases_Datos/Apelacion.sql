CREATE TABLE [dbo].[Apelacion] (
    [Correo]         NVARCHAR (50)  NOT NULL,
    [FechaApelacion] DATETIME       NOT NULL,
    [FechaDenuncia]  DATETIME       NOT NULL,
    [Comentario]     NVARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Correo] ASC),
    CONSTRAINT [FK_dbo.Apelacion_dbo.Denuncia] FOREIGN KEY ([FechaDenuncia]) REFERENCES [dbo].[Denuncia] ([Fecha])
);
