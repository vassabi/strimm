CREATE TABLE [dbo].[LocalizationSource] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF_LocalizationSource_Id] DEFAULT (newid()) NOT NULL,
    [LanguageId]   UNIQUEIDENTIFIER NOT NULL,
    [Source]       NVARCHAR (256)   NOT NULL,
    [ResourceKey]  NVARCHAR (128)   NOT NULL,
    [Type]         NVARCHAR (256)   NOT NULL,
    [DateEntered]  DATETIME         NOT NULL,
    [DateModified] DATETIME         NOT NULL,
    CONSTRAINT [PK_LocalizationSource] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LocalizationSource_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LocalizationSource]
    ON [dbo].[LocalizationSource]([LanguageId] ASC, [Source] ASC, [ResourceKey] ASC);

