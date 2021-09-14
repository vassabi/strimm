CREATE TABLE [dbo].[SnTag] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [RelationshipId] UNIQUEIDENTIFIER NOT NULL,
    [Tag]            NVARCHAR (200)   NOT NULL,
    [Slug]           NVARCHAR (200)   NOT NULL,
    [SortOrder]      INT              CONSTRAINT [DF_SnTag_SortOrder] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SnTag] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnTag_SnRelationship] FOREIGN KEY ([RelationshipId]) REFERENCES [dbo].[SnRelationship] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SnTag_RelationShip]
    ON [dbo].[SnTag]([RelationshipId] ASC)
    INCLUDE([Id], [Tag], [Slug], [SortOrder]);


GO
CREATE NONCLUSTERED INDEX [IX_SnTag_RelationshipSortOrder]
    ON [dbo].[SnTag]([RelationshipId] ASC, [SortOrder] ASC)
    INCLUDE([Id], [Tag], [Slug]);


GO
CREATE NONCLUSTERED INDEX [IX_SnTag_Slug_Opt1]
    ON [dbo].[SnTag]([Slug] ASC, [RelationshipId] ASC);

