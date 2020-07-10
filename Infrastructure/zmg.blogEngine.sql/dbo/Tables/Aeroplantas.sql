CREATE TABLE [dbo].[Aeroplantas] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Nombre]           NVARCHAR (255)   NOT NULL,
    [CodigoAeroplanta] NVARCHAR (255)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

