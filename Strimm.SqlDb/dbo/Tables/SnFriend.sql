CREATE TABLE [dbo].[SnFriend] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [FriendUserId] UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]  DATETIME         NOT NULL,
    CONSTRAINT [PK_SnFriend] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnFriend_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SnFriend_aspnet_Users1] FOREIGN KEY ([FriendUserId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);

