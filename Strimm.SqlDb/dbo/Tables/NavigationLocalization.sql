CREATE TABLE [dbo].[NavigationLocalization] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]   UNIQUEIDENTIFIER NOT NULL,
    [NavigationId] UNIQUEIDENTIFIER NOT NULL,
    [Title]        NVARCHAR (255)   NOT NULL,
    [ToolTip]      NVARCHAR (500)   NULL,
    CONSTRAINT [PK_NavigationLocalization] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NavigationLocalization_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NavigationLocalization_Navigation] FOREIGN KEY ([NavigationId]) REFERENCES [dbo].[Navigation] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[NavigationLocalization]([LanguageId] ASC, [NavigationId] ASC);

