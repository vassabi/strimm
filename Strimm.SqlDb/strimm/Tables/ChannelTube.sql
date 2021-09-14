CREATE TABLE [strimm].[ChannelTube] (
    [ChannelTubeId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (255) NOT NULL,
    [Description]   NVARCHAR (MAX) NULL,
    [CategoryId]    INT            NOT NULL,
    [PictureUrl]    NVARCHAR (255) NULL,
    [Url]           NVARCHAR (255) NULL,
    [Rating]        REAL           NOT NULL,
    [IsFeatured]    BIT            CONSTRAINT [DF_ChannelTube_IsFeatured] DEFAULT ((0)) NOT NULL,
    [CreatedDate]   DATETIME       NOT NULL,
    [IsPrivate]     BIT            CONSTRAINT [DF_ChannelTube_IsPrivate] DEFAULT ((0)) NOT NULL,
    [IsLocked]      BIT            NOT NULL,
    [UserId]        INT            NOT NULL,
    CONSTRAINT [PK_ChannelTube] PRIMARY KEY CLUSTERED ([ChannelTubeId] ASC),
    CONSTRAINT [FK_ChannelTube_Category] FOREIGN KEY ([CategoryId]) REFERENCES [strimm].[Category] ([CategoryId]),
    CONSTRAINT [FK_ChannelTube_User] FOREIGN KEY ([UserId]) REFERENCES [strimm].[User] ([UserId])
);

