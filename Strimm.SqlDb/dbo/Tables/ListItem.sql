CREATE TABLE [dbo].[ListItem] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ListId]      UNIQUEIDENTIFIER NOT NULL,
    [ItemOrder]   INT              CONSTRAINT [DF_ListItem_ItemOrder] DEFAULT ((1)) NOT NULL,
    [DateCreated] DATETIME         NOT NULL,
    CONSTRAINT [PK_ListItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ListItem_List] FOREIGN KEY ([ListId]) REFERENCES [dbo].[List] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ItemOrder]
    ON [dbo].[ListItem]([ItemOrder] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ListId]
    ON [dbo].[ListItem]([ListId] ASC);

