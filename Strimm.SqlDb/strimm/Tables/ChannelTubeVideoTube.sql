CREATE TABLE [strimm].[ChannelTubeVideoTube] (
    [ChannelTubeId] INT      NOT NULL,
    [VideoTubeId]   INT      NOT NULL,
    [CreatedDate]   DATETIME NOT NULL,
    CONSTRAINT [PK_ChannelTubeVideoTube] PRIMARY KEY CLUSTERED ([ChannelTubeId] ASC, [VideoTubeId] ASC, [CreatedDate] ASC),
    CONSTRAINT [FK_ChannelTubeVideoTube_ChannelTube] FOREIGN KEY ([ChannelTubeId]) REFERENCES [strimm].[ChannelTube] ([ChannelTubeId]),
    CONSTRAINT [FK_ChannelTubeVideoTube_VideoTube] FOREIGN KEY ([VideoTubeId]) REFERENCES [strimm].[VideoTube] ([VideoTubeId])
);

