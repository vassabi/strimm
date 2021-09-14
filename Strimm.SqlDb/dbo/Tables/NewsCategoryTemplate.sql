CREATE TABLE [dbo].[NewsCategoryTemplate] (
    [Id]                      UNIQUEIDENTIFIER CONSTRAINT [DF_NewsCategoryTemplate_Id] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [TemplateName]            NVARCHAR (100)   NOT NULL,
    [TemplateNameFullContent] NVARCHAR (100)   NOT NULL,
    [DateEntered]             DATETIME         NULL,
    [DateModified]            DATETIME         NULL,
    CONSTRAINT [PK_NewsCategoryTemplate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NewsCategoryTemplate_NewsCategory1] FOREIGN KEY ([Id]) REFERENCES [dbo].[NewsCategory] ([Id]) ON DELETE CASCADE
);

