CREATE TABLE [dbo].[PageLocalization] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]  UNIQUEIDENTIFIER NOT NULL,
    [PageId]      UNIQUEIDENTIFIER NOT NULL,
    [Title]       NVARCHAR (255)   NOT NULL,
    [Description] NVARCHAR (MAX)   NULL,
    [Keywords]    NVARCHAR (1000)  NULL,
    CONSTRAINT [PK_PageLocalization_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PageLocalization_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PageLocalization_Page] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PageLocalization]
    ON [dbo].[PageLocalization]([PageId] ASC, [LanguageId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PageLocalization_41]
    ON [dbo].[PageLocalization]([PageId] ASC, [LanguageId] ASC, [Id] ASC)
    INCLUDE([Title], [Description], [Keywords]);

