CREATE TABLE [dbo].[NewsItemLocalization] (
    [Id]              UNIQUEIDENTIFIER CONSTRAINT [DF_NewsItemsLocalization_Id] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [NewsId]          UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]      UNIQUEIDENTIFIER NOT NULL,
    [Title]           NVARCHAR (300)   NULL,
    [ShortContent]    NVARCHAR (MAX)   NULL,
    [Content]         NVARCHAR (MAX)   NULL,
    [Published]       BIT              CONSTRAINT [DF_NewsItemsLocalization_Published] DEFAULT ((1)) NOT NULL,
    [PublishStart]    DATETIME         NOT NULL,
    [PublishEnd]      DATETIME         NULL,
    [MetaDescription] NVARCHAR (255)   NULL,
    [MetaKeywords]    NVARCHAR (255)   NULL,
    [DateEntered]     DATETIME         NULL,
    [DateModified]    DATETIME         NULL,
    [VisibleDate]     DATETIME         NULL,
    CONSTRAINT [PK_NewsItemsLocalization] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NewsItemLocalization_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NewsItemsLocalization_NewsItems] FOREIGN KEY ([NewsId]) REFERENCES [dbo].[NewsItem] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_FilterPeriod]
    ON [dbo].[NewsItemLocalization]([PublishStart] ASC, [PublishEnd] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Language]
    ON [dbo].[NewsItemLocalization]([LanguageId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Published]
    ON [dbo].[NewsItemLocalization]([Published] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TextSearch]
    ON [dbo].[NewsItemLocalization]([Title] ASC);

