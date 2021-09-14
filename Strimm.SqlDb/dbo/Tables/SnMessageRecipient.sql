CREATE TABLE [dbo].[SnMessageRecipient] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [MessageId]     UNIQUEIDENTIFIER NOT NULL,
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    [MessageStatus] INT              NOT NULL,
    [DateRead]      DATETIME         NULL,
    CONSTRAINT [PK_SnMessageRecipient] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnMessageRecipient_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SnMessageRecipient_SnMessage] FOREIGN KEY ([MessageId]) REFERENCES [dbo].[SnMessage] ([Id]) ON DELETE CASCADE
);

