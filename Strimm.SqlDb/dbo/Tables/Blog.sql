CREATE TABLE [dbo].[Blog] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ApplicationId] UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]    UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (450)   NOT NULL,
    [Slug]          NVARCHAR (450)   NOT NULL,
    [Description]   NVARCHAR (MAX)   NULL,
    [DateCreated]   DATETIME         NOT NULL,
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Blog_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]),
    CONSTRAINT [FK_Blog_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_Blog_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_DefaultFilter]
    ON [dbo].[Blog]([ApplicationId] ASC, [LanguageId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[Blog]([Name] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Slug]
    ON [dbo].[Blog]([Slug] ASC);

