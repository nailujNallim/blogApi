CREATE TABLE [dbo].[Usuarios] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Estado]           NVARCHAR (255)   NOT NULL,
    [UserName]         NVARCHAR (255)   NULL,
    [NombresCompletos] NVARCHAR (255)   NULL,
    [DNI]              NVARCHAR (255)   NULL,
    [ZonaHoraria]      NVARCHAR (255)   NULL,
    [Pathfirma]        NVARCHAR (255)   NULL,
    [contactoId]       UNIQUEIDENTIFIER NULL,
    [aeroplantaId]     UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK65BAD0E869DBF620] FOREIGN KEY ([contactoId]) REFERENCES [dbo].[Contactos] ([Id]),
    CONSTRAINT [FK65BAD0E88F19EE4F] FOREIGN KEY ([aeroplantaId]) REFERENCES [dbo].[Aeroplantas] ([Id]),
    CONSTRAINT [FK7AFBD09C681095D4] FOREIGN KEY ([aeroplantaId]) REFERENCES [dbo].[Aeroplantas] ([Id]),
    CONSTRAINT [FK7AFBD09CEAD6F4AB] FOREIGN KEY ([contactoId]) REFERENCES [dbo].[Contactos] ([Id])
);





