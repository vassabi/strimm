CREATE TABLE [dbo].[BlogPostCategory] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [BlogPostId]     UNIQUEIDENTIFIER NOT NULL,
    [BlogCategoryId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_BlogPostCategory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BlogPostCategory_BlogCategory] FOREIGN KEY ([BlogCategoryId]) REFERENCES [dbo].[BlogCategory] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BlogPostCategory_BlogPost] FOREIGN KEY ([BlogPostId]) REFERENCES [dbo].[BlogPost] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[BlogPostCategory]([BlogPostId] ASC, [BlogCategoryId] ASC);

