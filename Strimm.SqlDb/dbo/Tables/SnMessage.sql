CREATE TABLE [dbo].[SnMessage] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [UserId]             UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]        DATETIME         NOT NULL,
    [Subject]            NVARCHAR (255)   NULL,
    [Body]               NVARCHAR (MAX)   NULL,
    [InReplyToMessageId] UNIQUEIDENTIFIER NOT NULL,
    [ToList]             NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_SnMessage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnMessage_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnMessage_SnMessage] FOREIGN KEY ([InReplyToMessageId]) REFERENCES [dbo].[SnMessage] ([Id])
);

