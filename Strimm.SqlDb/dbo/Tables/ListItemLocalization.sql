CREATE TABLE [dbo].[ListItemLocalization] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ListItemId]  UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]  UNIQUEIDENTIFIER NOT NULL,
    [ItemTitle]   NVARCHAR (MAX)   NOT NULL,
    [ItemContent] NVARCHAR (MAX)   NULL,
    [ItemUrl]     NVARCHAR (500)   NULL,
    CONSTRAINT [PK_ListItemLocalization] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ListItemLocalization_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ListItemLocalization_ListItem] FOREIGN KEY ([ListItemId]) REFERENCES [dbo].[ListItem] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_LanguageId]
    ON [dbo].[ListItemLocalization]([LanguageId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ListItemId]
    ON [dbo].[ListItemLocalization]([ListItemId] ASC);

