CREATE TABLE [strimm].[ChannelScheduleVideoTube] (
    [ChannelScheduleId]   INT IDENTITY (1, 1) NOT NULL,
    [VideoTubeId]         INT NOT NULL,
    [PlaybackOrderNumber] INT NOT NULL,
    CONSTRAINT [PK_ChannelScheduleVideoTube] PRIMARY KEY NONCLUSTERED ([ChannelScheduleId] ASC, [VideoTubeId] ASC, [PlaybackOrderNumber] ASC),
    CONSTRAINT [FK_ChannelScheduleVideoTube_VideoTube] FOREIGN KEY ([VideoTubeId]) REFERENCES [strimm].[VideoTube] ([VideoTubeId]),
    CONSTRAINT [UIX_ChannelScheduleVideoTube] UNIQUE CLUSTERED ([ChannelScheduleId] ASC, [VideoTubeId] ASC, [PlaybackOrderNumber] ASC)
);

