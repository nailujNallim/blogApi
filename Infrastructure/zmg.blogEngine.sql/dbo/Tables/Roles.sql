CREATE TABLE [dbo].[Roles] (
    [Usuario_id]      UNIQUEIDENTIFIER NOT NULL,
    [EnumValueColumn] INT              NULL,
    CONSTRAINT [FK372639D42167E45] FOREIGN KEY ([Usuario_id]) REFERENCES [dbo].[Usuarios] ([Id])
);

