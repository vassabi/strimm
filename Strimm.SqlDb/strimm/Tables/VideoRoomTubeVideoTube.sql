CREATE TABLE [strimm].[VideoRoomTubeVideoTube] (
    [VideoRoomTubeId] INT      NOT NULL,
    [VideoTubeId]     INT      NOT NULL,
    [CreatedDate]     DATETIME NOT NULL,
    CONSTRAINT [PK_VideoRoomTubeVideoTube] PRIMARY KEY CLUSTERED ([VideoRoomTubeId] ASC, [VideoTubeId] ASC),
    CONSTRAINT [FK_VideoRoomTubeVideoTube_VideoRoomTube] FOREIGN KEY ([VideoRoomTubeId]) REFERENCES [strimm].[VideoRoomTube] ([VideoRoomTubeId]),
    CONSTRAINT [FK_VideoRoomTubeVideoTube_VideoTube] FOREIGN KEY ([VideoTubeId]) REFERENCES [strimm].[VideoTube] ([VideoTubeId])
);

