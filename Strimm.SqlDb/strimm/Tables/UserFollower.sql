CREATE TABLE [strimm].[UserFollower] (
    [UserFollowerId]    INT      IDENTITY (1, 1) NOT NULL,
    [UserId]            INT      NULL,
    [FollowerUserId]    INT      NULL,
    [StartedFollowDate] DATETIME NOT NULL,
    [StoppedFollowDate] DATETIME NULL,
    CONSTRAINT [PK_UserFollowers] PRIMARY KEY CLUSTERED ([UserFollowerId] ASC),
    CONSTRAINT [FK_UserFollower_User] FOREIGN KEY ([UserId]) REFERENCES [strimm].[User] ([UserId]),
    CONSTRAINT [FK_UserFollower_User1] FOREIGN KEY ([FollowerUserId]) REFERENCES [strimm].[User] ([UserId])
);

