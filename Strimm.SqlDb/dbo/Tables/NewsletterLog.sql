CREATE TABLE [dbo].[NewsletterLog] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [NewsletterId] UNIQUEIDENTIFIER NOT NULL,
    [Email]        NVARCHAR (200)   NOT NULL,
    [SentOn]       DATETIME         NOT NULL,
    [Success]      BIT              NOT NULL,
    [Message]      NVARCHAR (500)   NULL,
    CONSTRAINT [PK_NewsletterLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NewsletterLog_Newsletter] FOREIGN KEY ([NewsletterId]) REFERENCES [dbo].[Newsletter] ([Id]) ON DELETE CASCADE
);

