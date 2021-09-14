CREATE TABLE [dbo].[BlogEditor] (
    [Id]     UNIQUEIDENTIFIER NOT NULL,
    [BlogId] UNIQUEIDENTIFIER NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_BlogEditor] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BlogEditor_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_BlogEditor_Blog] FOREIGN KEY ([BlogId]) REFERENCES [dbo].[Blog] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[BlogEditor]([BlogId] ASC, [UserId] ASC);

