CREATE TABLE [dbo].[AdPage] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF_AdPage_Id] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [AdId]         UNIQUEIDENTIFIER NOT NULL,
    [PageId]       UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]   UNIQUEIDENTIFIER NOT NULL,
    [DateEntered]  DATETIME         NULL,
    [DateModified] DATETIME         NULL,
    CONSTRAINT [PK_AdPage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdPage_Ad] FOREIGN KEY ([AdId]) REFERENCES [dbo].[Ad] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AdPage_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AdPage_Page] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PageLanguage]
    ON [dbo].[AdPage]([PageId] ASC, [LanguageId] ASC);

