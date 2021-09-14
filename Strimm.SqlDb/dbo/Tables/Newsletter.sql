CREATE TABLE [dbo].[Newsletter] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Title]         NVARCHAR (200)   NOT NULL,
    [TextContent]   NVARCHAR (MAX)   NULL,
    [HtmlContent]   NVARCHAR (MAX)   NULL,
    [DateModified]  DATETIME         NOT NULL,
    [SentOn]        DATETIME         NULL,
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    [ApplicationId] UNIQUEIDENTIFIER NOT NULL,
    [Status]        NVARCHAR (200)   NULL,
    [MailFrom]      NVARCHAR (200)   NULL,
    [TestAddress]   NVARCHAR (200)   NULL,
    [Result]        NVARCHAR (200)   NULL,
    [TextOnly]      BIT              CONSTRAINT [DF_Newsletter_TextOnly] DEFAULT ((0)) NOT NULL,
    [LanguageId]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Newsletter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Newsletter_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]),
    CONSTRAINT [FK_Newsletter_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_Newsletter_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Language]
    ON [dbo].[Newsletter]([LanguageId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Title]
    ON [dbo].[Newsletter]([Title] ASC);

