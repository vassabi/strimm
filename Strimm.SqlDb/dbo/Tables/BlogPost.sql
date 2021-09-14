CREATE TABLE [dbo].[BlogPost] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [IdentityId]       INT              IDENTITY (1, 1) NOT NULL,
    [BlogId]           UNIQUEIDENTIFIER NOT NULL,
    [Title]            NVARCHAR (450)   NOT NULL,
    [Description]      NVARCHAR (MAX)   NULL,
    [PostContent]      NVARCHAR (MAX)   NOT NULL,
    [DateCreated]      DATETIME         NULL,
    [DateModified]     DATETIME         NULL,
    [DatePublished]    DATETIME         NULL,
    [IsCommentEnabled] BIT              CONSTRAINT [DF_BlogPost_IsCommentEnabled] DEFAULT ((1)) NOT NULL,
    [Raters]           INT              CONSTRAINT [DF_BlogPost_Raters] DEFAULT ((0)) NOT NULL,
    [Rating]           FLOAT (53)       CONSTRAINT [DF_BlogPost_Rating] DEFAULT ((0)) NOT NULL,
    [Slug]             NVARCHAR (450)   NOT NULL,
    [UserId]           UNIQUEIDENTIFIER NOT NULL,
    [IsPublished]      BIT              CONSTRAINT [DF_BlogPost_IsPublished] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_BlogPost] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Blog_BlogPost] FOREIGN KEY ([BlogId]) REFERENCES [dbo].[Blog] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BlogPost_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_BlogId]
    ON [dbo].[BlogPost]([BlogId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BlogPost_Id_Opt1]
    ON [dbo].[BlogPost]([Id] ASC, [BlogId] ASC, [UserId] ASC, [IsPublished] ASC, [DatePublished] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BlogPost_User_Opt1]
    ON [dbo].[BlogPost]([BlogId] ASC, [DatePublished] ASC, [UserId] ASC, [IsPublished] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[BlogPost]([DatePublished] ASC, [UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Identity]
    ON [dbo].[BlogPost]([IdentityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Slug]
    ON [dbo].[BlogPost]([Slug] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TextSearch]
    ON [dbo].[BlogPost]([Title] ASC);

