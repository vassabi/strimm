CREATE TABLE [dbo].[Language] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [LanguageName]    NVARCHAR (100)   NOT NULL,
    [CultureName]     NVARCHAR (10)    NOT NULL,
    [DefaultLanguage] BIT              CONSTRAINT [DF_Language_DefaultLanguage] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_DefaultLanguage]
    ON [dbo].[Language]([DefaultLanguage] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Language]
    ON [dbo].[Language]([CultureName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_LanguageName]
    ON [dbo].[Language]([LanguageName] ASC);

