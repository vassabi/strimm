CREATE TABLE [strimm].[UserVideoArchive] (
    [UserId]      INT      NOT NULL,
    [VideoTubeId] INT      NOT NULL,
    [CreatedDate] DATETIME NOT NULL,
    CONSTRAINT [PK_UserVideoArchive] PRIMARY KEY CLUSTERED ([UserId] ASC, [VideoTubeId] ASC),
    CONSTRAINT [FK_UserVideoArchive_User] FOREIGN KEY ([UserId]) REFERENCES [strimm].[User] ([UserId]),
    CONSTRAINT [FK_UserVideoArchive_VideoTube] FOREIGN KEY ([VideoTubeId]) REFERENCES [strimm].[VideoTube] ([VideoTubeId])
);

