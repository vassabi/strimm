CREATE TABLE [dbo].[NewsMetaIgnoredWord] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF_IgnoredWords_Id] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [LanguageId]   UNIQUEIDENTIFIER NOT NULL,
    [Word]         NVARCHAR (255)   NOT NULL,
    [DateEntered]  DATETIME         CONSTRAINT [DF_NewsMetaIgnoredWords_DateEntered] DEFAULT (getdate()) NULL,
    [DateModified] DATETIME         CONSTRAINT [DF_NewsMetaIgnoredWords_DateModified] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_IgnoredWords] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NewsMetaIgnoredWords_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Language]
    ON [dbo].[NewsMetaIgnoredWord]([LanguageId] ASC);

