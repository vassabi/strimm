CREATE TABLE [strimm].[Visitor] (
    [VisitorId]     INT           IDENTITY (1, 1) NOT NULL,
    [UserId]        INT           NOT NULL,
    [IpAddress]     NVARCHAR (45) NOT NULL,
    [VisitDate]     DATETIME      NOT NULL,
    [VisitDuration] INT           NOT NULL,
    [Destination]   NVARCHAR (50) NOT NULL,
    [ChannelTubeId] INT           NULL,
    [VisitorUserId] INT           NULL,
    CONSTRAINT [PK_ChannelVisitor] PRIMARY KEY CLUSTERED ([VisitorId] ASC),
    CONSTRAINT [FK_Visitor_ChannelTube] FOREIGN KEY ([ChannelTubeId]) REFERENCES [strimm].[ChannelTube] ([ChannelTubeId]),
    CONSTRAINT [FK_Visitor_User] FOREIGN KEY ([UserId]) REFERENCES [strimm].[User] ([UserId])
);

