CREATE TABLE [dbo].[LocalizationString] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Data]         NVARCHAR (2048)  NOT NULL,
    [DateEntered]  DATETIME         NOT NULL,
    [DateModified] DATETIME         NOT NULL,
    CONSTRAINT [PK_LocalizationStrings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LocalizationStrings_LocalizationSource] FOREIGN KEY ([Id]) REFERENCES [dbo].[LocalizationSource] ([Id]) ON DELETE CASCADE
);

