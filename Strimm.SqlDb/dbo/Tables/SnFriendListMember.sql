CREATE TABLE [dbo].[SnFriendListMember] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [FriendListId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SnFriendListMember] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnFriendListMember_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SnFriendListMember_SnFriendList] FOREIGN KEY ([FriendListId]) REFERENCES [dbo].[SnFriendList] ([Id]) ON DELETE CASCADE
);

