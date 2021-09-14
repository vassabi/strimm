CREATE TABLE [dbo].[LocalizationBinary] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Data]         IMAGE            NOT NULL,
    [DateEntered]  DATETIME         NOT NULL,
    [DateModified] DATETIME         NOT NULL,
    CONSTRAINT [PK_LocalizationBinary] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LocalizationBinary_LocalizationSource] FOREIGN KEY ([Id]) REFERENCES [dbo].[LocalizationSource] ([Id]) ON DELETE CASCADE
);

