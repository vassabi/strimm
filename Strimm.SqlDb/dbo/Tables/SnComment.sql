CREATE TABLE [dbo].[SnComment] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [UserId]         UNIQUEIDENTIFIER NULL,
    [RelationshipId] UNIQUEIDENTIFIER NOT NULL,
    [Comment]        NVARCHAR (MAX)   NULL,
    [DateCreated]    DATETIME         NOT NULL,
    [Author]         NVARCHAR (255)   NULL,
    [Email]          NVARCHAR (255)   NULL,
    [Website]        NVARCHAR (500)   NULL,
    [Ip]             NVARCHAR (50)    NULL,
    [Referrer]       NVARCHAR (500)   NULL,
    [UserAgent]      NVARCHAR (255)   NULL,
    [IsApproved]     BIT              NOT NULL,
    [IsSpam]         BIT              NOT NULL,
    [Spaminess]      DECIMAL (18)     CONSTRAINT [DF_SnComment_Spaminess] DEFAULT ((0)) NOT NULL,
    [Signature]      NVARCHAR (MAX)   NULL,
    [Rating]         FLOAT (53)       CONSTRAINT [DF_SnComment_Rating] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SnComment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnComment_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SnComment_SnRelationship] FOREIGN KEY ([RelationshipId]) REFERENCES [dbo].[SnRelationship] ([Id])
);

