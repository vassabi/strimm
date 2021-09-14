CREATE TABLE [dbo].[Document] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [PageId]          UNIQUEIDENTIFIER NULL,
    [ControlId]       NVARCHAR (200)   NULL,
    [ContentId]       NVARCHAR (200)   NULL,
    [Title]           NVARCHAR (MAX)   NULL,
    [TextContent]     NVARCHAR (MAX)   NULL,
    [LanguageId]      UNIQUEIDENTIFIER NOT NULL,
    [UserId]          UNIQUEIDENTIFIER NULL,
    [DateModified]    DATETIME         CONSTRAINT [DF_Document_DateModified] DEFAULT (getdate()) NOT NULL,
    [RevisionVersion] BIT              CONSTRAINT [DF_Document_Revision] DEFAULT ((0)) NOT NULL,
    [BackupVersion]   BIT              CONSTRAINT [DF_Document_BackupVersion] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Document_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_Document_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Document_Page] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_BackupVersion]
    ON [dbo].[Document]([BackupVersion] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ContentId]
    ON [dbo].[Document]([ContentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ControlId]
    ON [dbo].[Document]([ControlId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Document_41]
    ON [dbo].[Document]([PageId] ASC, [RevisionVersion] ASC)
    INCLUDE([Id], [ControlId], [ContentId], [Title], [TextContent], [LanguageId], [UserId], [DateModified], [BackupVersion]);


GO
CREATE NONCLUSTERED INDEX [IX_Document_Page_Opt1]
    ON [dbo].[Document]([LanguageId] ASC, [BackupVersion] ASC, [RevisionVersion] ASC, [PageId] ASC)
    INCLUDE([Id], [ControlId], [ContentId], [Title], [TextContent], [UserId], [DateModified]);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[Document]([LanguageId] ASC, [UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_MainFilter]
    ON [dbo].[Document]([PageId] ASC, [ControlId] ASC, [ContentId] ASC, [LanguageId] ASC, [RevisionVersion] ASC, [BackupVersion] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Page]
    ON [dbo].[Document]([PageId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RevisionVersionBackupVersion]
    ON [dbo].[Document]([RevisionVersion] ASC, [BackupVersion] ASC);

