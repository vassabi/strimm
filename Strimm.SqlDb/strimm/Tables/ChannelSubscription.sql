CREATE TABLE [strimm].[ChannelSubscription] (
    [ChannelSubscriptionId] INT      IDENTITY (1, 1) NOT NULL,
    [UserId]                INT      NOT NULL,
    [ChannelTubeId]         INT      NOT NULL,
    [SubscriptionStartDate] DATETIME NOT NULL,
    [SubscriptionEndDate]   DATETIME NULL,
    CONSTRAINT [PK_ChannelSubscription] PRIMARY KEY CLUSTERED ([ChannelSubscriptionId] ASC),
    CONSTRAINT [FK_ChannelSubscription_ChannelTube] FOREIGN KEY ([ChannelTubeId]) REFERENCES [strimm].[ChannelTube] ([ChannelTubeId]),
    CONSTRAINT [FK_ChannelSubscription_User] FOREIGN KEY ([UserId]) REFERENCES [strimm].[User] ([UserId])
);

