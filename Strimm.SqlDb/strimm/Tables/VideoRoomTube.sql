CREATE TABLE [strimm].[VideoRoomTube] (
    [VideoRoomTubeId] INT      IDENTITY (1, 1) NOT NULL,
    [UserId]          INT      NOT NULL,
    [CreatedDate]     DATETIME NOT NULL,
    [IsPrivate]       BIT      CONSTRAINT [DF_VideoRoomTube_IsPrivate] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_VideoRoomTube] PRIMARY KEY CLUSTERED ([VideoRoomTubeId] ASC),
    CONSTRAINT [FK_VideoRoomTube_User] FOREIGN KEY ([UserId]) REFERENCES [strimm].[User] ([UserId])
);

