CREATE TABLE [dbo].[Agencia] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Nome]   NVARCHAR (255) NOT NULL,
    [Status] BIT            DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

