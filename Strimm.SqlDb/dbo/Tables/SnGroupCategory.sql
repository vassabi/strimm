CREATE TABLE [dbo].[SnGroupCategory] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ApplicationId] UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]    UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (200)   NOT NULL,
    [Slug]          NVARCHAR (255)   NULL,
    CONSTRAINT [PK_SnGroupType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnGroupCategory_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]),
    CONSTRAINT [FK_SnGroupCategory_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]),
    CONSTRAINT [IX_SnGroupCategory] UNIQUE NONCLUSTERED ([ApplicationId] ASC, [LanguageId] ASC, [Name] ASC)
);

