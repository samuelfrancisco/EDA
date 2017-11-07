CREATE TABLE [dbo].[AgenciasEnviadas] (
    [Id]          INT            NOT NULL,
    [IdDaAgencia] INT            NOT NULL,
    [Nome]        NVARCHAR (255) NOT NULL,
    [Status]      BIT            NOT NULL,
    [CriadoEm]    DATETIME       NOT NULL,
    [EnviadoEm]   DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

