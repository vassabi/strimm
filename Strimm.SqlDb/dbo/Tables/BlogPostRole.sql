CREATE TABLE [dbo].[BlogPostRole] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [BlogPostId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_BlogPostRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BlogPostRole_aspnet_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[aspnet_Roles] ([RoleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BlogPostRole_BlogPost] FOREIGN KEY ([BlogPostId]) REFERENCES [dbo].[BlogPost] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[BlogPostRole]([BlogPostId] ASC, [RoleId] ASC);

