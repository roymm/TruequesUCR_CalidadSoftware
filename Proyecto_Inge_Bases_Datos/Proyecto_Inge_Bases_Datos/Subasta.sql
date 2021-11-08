CREATE TABLE [dbo].[Subasta]
(
	
    [ProductoIDSubastado]	INT		        NOT NULL,
    [CorreoSubastador]		NVARCHAR(50)	NOT NULL,
    [FechaPublicado]        DATETIME        NOT NULL,
    [PrecioMinimo]          FLOAT           NOT NULL,
    [FechaInicio]           DATETIME        NOT NULL    DEFAULT NULL, 
    [FechaFin]              DATETIME        NOT NULL    DEFAULT NULL, 
    [Calificacion]          SMALLINT        NULL        DEFAULT NULL,
    [Estado]                BIT             NULL        DEFAULT 1,
    [PublicoEspecial]       SMALLINT        NOT NULL    DEFAULT 0,
    [Mensaje] NVARCHAR(50) NULL DEFAULT NULL, 
    [EstadoEnvio] BIT NULL DEFAULT NULL, 

    PRIMARY KEY CLUSTERED ([ProductoIDSubastado],[CorreoSubastador], [FechaPublicado] ),
    CONSTRAINT [FK_dbo.Subasta_dbo.Producto] FOREIGN KEY (ProductoIDSubastado, CorreoSubastador) 
    REFERENCES [dbo].[Producto] (ProductoID,CorreoCliente) --ON DELETE CASCADE --Se agregó el delete cascade,
    --Poner trigger para que una subasta no se pueda borrar si hay algún producto subastado
)

