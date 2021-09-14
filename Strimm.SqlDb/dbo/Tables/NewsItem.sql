CREATE TABLE [dbo].[NewsItem] (
    [Id]                      UNIQUEIDENTIFIER CONSTRAINT [DF_NewsItems_Id] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [NewsCategoryId]          UNIQUEIDENTIFIER NOT NULL,
    [UserId]                  UNIQUEIDENTIFIER NOT NULL,
    [ShowOnHomePage]          BIT              CONSTRAINT [DF_NewsItems_ShowOnHomePage] DEFAULT ((0)) NOT NULL,
    [Revision]                INT              CONSTRAINT [DF_NewsItems_Revision] DEFAULT ((0)) NOT NULL,
    [ShowTitle]               BIT              CONSTRAINT [DF_NewsItems_ShowTitle] DEFAULT ((1)) NOT NULL,
    [ShowShortContent]        BIT              CONSTRAINT [DF_NewsItems_ShowShortContent] DEFAULT ((1)) NOT NULL,
    [ShowCategoryTitle]       BIT              CONSTRAINT [DF_NewsItems_ShowCategoryTitle] DEFAULT ((0)) NOT NULL,
    [ShowCategoryTitleAsLink] BIT              CONSTRAINT [DF_NewsItems_ShowCategoryTitleAsLink] DEFAULT ((0)) NOT NULL,
    [ShowFullCategoryPath]    BIT              CONSTRAINT [DF_NewsItems_ShowFullCategoryPath] DEFAULT ((1)) NOT NULL,
    [ShowUserName]            BIT              CONSTRAINT [DF_NewsItems_ShowUserName] DEFAULT ((1)) NOT NULL,
    [ShowDateEntered]         BIT              CONSTRAINT [DF_NewsItems_ShowDateEntered] DEFAULT ((1)) NOT NULL,
    [ShowDateModified]        BIT              CONSTRAINT [DF_NewsItems_ShowDateModified] DEFAULT ((1)) NOT NULL,
    [ViewCount]               INT              NULL,
    [DateEntered]             DATETIME         NULL,
    [DateModified]            DATETIME         NULL,
    [VisibleDate]             DATETIME         NULL,
    CONSTRAINT [PK_NewsItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NewsItems_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_NewsItems_NewsCategories] FOREIGN KEY ([NewsCategoryId]) REFERENCES [dbo].[NewsCategory] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_NewsCategoryId]
    ON [dbo].[NewsItem]([NewsCategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sorter]
    ON [dbo].[NewsItem]([DateModified] ASC);

