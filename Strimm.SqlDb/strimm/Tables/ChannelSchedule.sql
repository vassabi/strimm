CREATE TABLE [strimm].[ChannelSchedule] (
    [ChannelScheduleId] INT      IDENTITY (1, 1) NOT NULL,
    [ChannelTubeId]     INT      NOT NULL,
    [StartTime]         DATETIME NOT NULL,
    [IsActive]          BIT      CONSTRAINT [DF_ChannelSchedule_IsActive] DEFAULT ((0)) NOT NULL,
    [CreatedDate]       DATETIME NOT NULL,
    CONSTRAINT [PK_ChannelSchedule] PRIMARY KEY CLUSTERED ([ChannelScheduleId] ASC),
    CONSTRAINT [FK_ChannelSchedule_ChannelTube] FOREIGN KEY ([ChannelTubeId]) REFERENCES [strimm].[ChannelTube] ([ChannelTubeId])
);

