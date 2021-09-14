CREATE TABLE [dbo].[Version] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Version]     NVARCHAR (50)    NOT NULL,
    [Comment]     NVARCHAR (500)   NULL,
    [UpgradeLog]  NVARCHAR (MAX)   NULL,
    [DateEntered] DATETIME         CONSTRAINT [DF_Version_DateEntered] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED ([Id] ASC)
);

