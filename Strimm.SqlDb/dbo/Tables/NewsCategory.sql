CREATE TABLE [dbo].[NewsCategory] (
    [Id]             UNIQUEIDENTIFIER CONSTRAINT [DF_NewsCategories_Id] DEFAULT (newid()) NOT NULL,
    [ApplicationId]  UNIQUEIDENTIFIER NOT NULL,
    [NewsCategoryId] UNIQUEIDENTIFIER NULL,
    [Image]          IMAGE            NULL,
    [Order]          INT              NOT NULL,
    [DateEntered]    DATETIME         NULL,
    [DateModified]   DATETIME         NULL,
    CONSTRAINT [PK_NewsCategories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NewsCategories_NewsCategories] FOREIGN KEY ([NewsCategoryId]) REFERENCES [dbo].[NewsCategory] ([Id]),
    CONSTRAINT [FK_NewsCategory_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
);


GO
CREATE NONCLUSTERED INDEX [IX_AppId]
    ON [dbo].[NewsCategory]([ApplicationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NewsCategoryId]
    ON [dbo].[NewsCategory]([NewsCategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Order]
    ON [dbo].[NewsCategory]([Order] ASC);

