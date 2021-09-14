CREATE TABLE [dbo].[Navigation] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [PageId]          UNIQUEIDENTIFIER NULL,
    [ApplicationId]   UNIQUEIDENTIFIER NOT NULL,
    [ExternalUrl]     NVARCHAR (500)   NULL,
    [IsContainerPage] BIT              CONSTRAINT [DF_Navigation_IsContainerPage] DEFAULT ((0)) NOT NULL,
    [ParentId]        UNIQUEIDENTIFIER NULL,
    [PageOrder]       INT              CONSTRAINT [DF_Navigation_PageOrder] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Navigation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Navigation_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]),
    CONSTRAINT [FK_Navigation_Navigation] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Navigation] ([Id]),
    CONSTRAINT [FK_Navigation_Page] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApplicationId]
    ON [dbo].[Navigation]([ApplicationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PageId]
    ON [dbo].[Navigation]([PageId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PageOrder]
    ON [dbo].[Navigation]([PageOrder] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ParentId]
    ON [dbo].[Navigation]([ParentId] ASC);


GO
CREATE TRIGGER [dbo].[DeleteNavigationItem] ON [dbo].[Navigation] FOR DELETE
AS

IF @@rowcount = 0 RETURN

DELETE FROM T
FROM Navigation AS T JOIN deleted AS D
  ON T.ParentId = D.Id
COMMIT



/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION

