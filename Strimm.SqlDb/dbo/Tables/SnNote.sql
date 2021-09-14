CREATE TABLE [dbo].[SnNote] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ApplicationId]  UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]     UNIQUEIDENTIFIER NOT NULL,
    [UserId]         UNIQUEIDENTIFIER NOT NULL,
    [GroupId]        UNIQUEIDENTIFIER NULL,
    [NoteContent]    NVARCHAR (MAX)   NULL,
    [DateCreated]    DATETIME         NOT NULL,
    [Title]          NVARCHAR (500)   NULL,
    [Rating]         FLOAT (53)       CONSTRAINT [DF_SnNote_Rating] DEFAULT ((0)) NOT NULL,
    [PostToUserId]   UNIQUEIDENTIFIER NULL,
    [PrivacyLevelId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_SnNote] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnNote_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SnNote_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnNote_aspnet_Users1] FOREIGN KEY ([PostToUserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnNote_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]),
    CONSTRAINT [FK_SnNote_PrivacyLevel] FOREIGN KEY ([PrivacyLevelId]) REFERENCES [dbo].[PrivacyLevel] ([Id]),
    CONSTRAINT [FK_SnNote_SnGroup] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[SnGroup] ([Id]) ON DELETE CASCADE
);

