CREATE TABLE [strimm].[VideoTube] (
    [VideoTubeId]            INT            IDENTITY (1, 1) NOT NULL,
    [Title]                  NVARCHAR (255) NOT NULL,
    [Description]            NTEXT          NULL,
    [ProviderVideoId]        NVARCHAR (MAX) NULL,
    [Duration]               FLOAT (53)     NOT NULL,
    [CategoryId]             INT            NOT NULL,
    [VideoProviderId]        INT            NOT NULL,
    [CreatedDate]            DATETIME       NOT NULL,
    [IsRRated]               BIT            NOT NULL,
    [IsRemovedByProvider]    BIT            NOT NULL,
    [IsRestrictedByProvider] BIT            NOT NULL,
    [IsInPublicLibrary]      BIT            CONSTRAINT [DF_VideoTube_IsInPublicLib] DEFAULT ((0)) NOT NULL,
    [IsPrivate]              BIT            CONSTRAINT [DF_VideoTube_IsPrivate] DEFAULT ((0)) NOT NULL,
    [Thumbnail] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_VideoTube] PRIMARY KEY CLUSTERED ([VideoTubeId] ASC),
    CONSTRAINT [FK_VideoTube_Category] FOREIGN KEY ([CategoryId]) REFERENCES [strimm].[Category] ([CategoryId]),
    CONSTRAINT [FK_VideoTube_Category1] FOREIGN KEY ([CategoryId]) REFERENCES [strimm].[Category] ([CategoryId]),
    CONSTRAINT [FK_VideoTube_VideoProvider] FOREIGN KEY ([VideoProviderId]) REFERENCES [strimm].[VideoProvider] ([VideoProviderId])
);

