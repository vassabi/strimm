CREATE TABLE [dbo].[SnAlbum] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [UserId]         UNIQUEIDENTIFIER NULL,
    [SnGroupId]      UNIQUEIDENTIFIER NULL,
    [Name]           NVARCHAR (1000)  NOT NULL,
    [Description]    NVARCHAR (MAX)   NULL,
    [PrivacyLevelId] UNIQUEIDENTIFIER NULL,
    [DateCreated]    DATETIME         NOT NULL,
    [ApplicationId]  UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SnAlbum] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnAlbum_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]),
    CONSTRAINT [FK_SnAlbum_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnAlbum_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]),
    CONSTRAINT [FK_SnAlbum_PrivacyLevel] FOREIGN KEY ([PrivacyLevelId]) REFERENCES [dbo].[PrivacyLevel] ([Id]),
    CONSTRAINT [FK_SnAlbum_SnGroup] FOREIGN KEY ([SnGroupId]) REFERENCES [dbo].[SnGroup] ([Id])
);

