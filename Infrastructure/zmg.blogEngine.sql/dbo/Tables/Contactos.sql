CREATE TABLE [dbo].[Contactos] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [NombreCompleto] NVARCHAR (255)   NULL,
    [Telefono]       NVARCHAR (255)   NULL,
    [Email]          NVARCHAR (255)   NULL,
    [Cargo]          NVARCHAR (255)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



