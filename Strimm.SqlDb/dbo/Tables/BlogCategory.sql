CREATE TABLE [dbo].[BlogCategory] (
    [Id]     UNIQUEIDENTIFIER NOT NULL,
    [BlogId] UNIQUEIDENTIFIER NOT NULL,
    [Name]   NVARCHAR (255)   NOT NULL,
    [Slug]   NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_BlogCategory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BlogCategory_Blog] FOREIGN KEY ([BlogId]) REFERENCES [dbo].[Blog] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[BlogCategory]([BlogId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[BlogCategory]([Name] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Slug]
    ON [dbo].[BlogCategory]([Slug] ASC);

