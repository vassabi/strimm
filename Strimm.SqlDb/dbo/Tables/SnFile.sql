CREATE TABLE [dbo].[SnFile] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [UserId]         UNIQUEIDENTIFIER NOT NULL,
    [RelationshipId] UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (500)   NULL,
    [Url]            NVARCHAR (2000)  NOT NULL,
    [Description]    NVARCHAR (MAX)   NULL,
    [PrivacyLevelId] UNIQUEIDENTIFIER NULL,
    [ThumbnailUrl]   NVARCHAR (2000)  NULL,
    [DateCreated]    DATETIME         NOT NULL,
    [AlbumId]        UNIQUEIDENTIFIER NULL,
    [IsAlbumCover]   BIT              NULL,
    [SortOrder]      INT              NULL,
    [Rating]         FLOAT (53)       CONSTRAINT [DF_SnFile_Rating] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SnPhoto] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnFile_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnFile_PrivacyLevel] FOREIGN KEY ([PrivacyLevelId]) REFERENCES [dbo].[PrivacyLevel] ([Id]),
    CONSTRAINT [FK_SnFile_SnAlbum] FOREIGN KEY ([AlbumId]) REFERENCES [dbo].[SnAlbum] ([Id]),
    CONSTRAINT [FK_SnFile_SnRelationship] FOREIGN KEY ([RelationshipId]) REFERENCES [dbo].[SnRelationship] ([Id])
);

