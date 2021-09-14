CREATE TABLE [dbo].[NewsCategoryLocalization] (
    [Id]             UNIQUEIDENTIFIER CONSTRAINT [DF_NewsCategoriesLocalization_Id] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [NewsCategoryId] UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]     UNIQUEIDENTIFIER NOT NULL,
    [Title]          NVARCHAR (150)   NOT NULL,
    [Name]           NVARCHAR (150)   NOT NULL,
    [Description]    NVARCHAR (MAX)   NULL,
    [Published]      BIT              CONSTRAINT [DF_NewsCategoriesLocalization_Published] DEFAULT ((1)) NOT NULL,
    [DateEntered]    DATETIME         CONSTRAINT [DF_NewsCategoriesLocalization_DateEntered] DEFAULT (getdate()) NULL,
    [DateModified]   DATETIME         CONSTRAINT [DF_NewsCategoriesLocalization_DateModified] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_NewsCategoriesLocalization] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NewsCategoriesLocalization_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NewsCategoriesLocalization_NewsCategories] FOREIGN KEY ([NewsCategoryId]) REFERENCES [dbo].[NewsCategory] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[NewsCategoryLocalization]([NewsCategoryId] ASC, [LanguageId] ASC, [Published] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Name]
    ON [dbo].[NewsCategoryLocalization]([LanguageId] ASC, [Name] ASC);

