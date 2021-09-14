CREATE TABLE [dbo].[SnFriendRequest] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [FriendUserId] UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]  DATETIME         NOT NULL,
    [DateAccepted] DATETIME         NULL,
    CONSTRAINT [PK_SnFriendRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnFriendRequest_aspnet_Users] FOREIGN KEY ([FriendUserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnFriendRequest_SnFriendRequest] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);

