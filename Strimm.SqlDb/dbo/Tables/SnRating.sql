CREATE TABLE [dbo].[SnRating] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [UserId]         UNIQUEIDENTIFIER NULL,
    [RelationshipId] UNIQUEIDENTIFIER NOT NULL,
    [DateEntered]    DATETIME         NOT NULL,
    [Rating]         INT              NOT NULL,
    CONSTRAINT [PK_SnRating] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnRating_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnRating_SnRelationship] FOREIGN KEY ([RelationshipId]) REFERENCES [dbo].[SnRelationship] ([Id])
);

