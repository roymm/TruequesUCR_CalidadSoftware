CREATE TABLE [dbo].[Suspension] (
    [Correo]       NVARCHAR (50) NOT NULL,
    [FechaInicio]  DATETIME      NOT NULL,
    [FechaFin]     DATETIME      NOT NULL,
    [TipoDenuncia] TINYINT       NOT NULL,
    PRIMARY KEY CLUSTERED ([Correo] ASC),
    CONSTRAINT [FK_dbo.Suspension_dbo.Registrado] FOREIGN KEY ([Correo]) REFERENCES [dbo].[Registrado] ([Correo])
);
