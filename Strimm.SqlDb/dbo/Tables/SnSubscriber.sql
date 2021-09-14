CREATE TABLE [dbo].[SnSubscriber] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [RelationshipId]    UNIQUEIDENTIFIER NOT NULL,
    [UserId]            UNIQUEIDENTIFIER NULL,
    [Email]             NVARCHAR (250)   NULL,
    [SubscriptionDate]  DATETIME         NOT NULL,
    [Active]            BIT              CONSTRAINT [DF_SnSubscriber_Active] DEFAULT ((1)) NOT NULL,
    [SubscriptionOrder] INT              IDENTITY (1, 1) NOT NULL,
    [DateModified]      DATETIME         NULL,
    CONSTRAINT [PK_SnSubscriber] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnSubscriber_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnSubscriber_SnRelationship] FOREIGN KEY ([RelationshipId]) REFERENCES [dbo].[SnRelationship] ([Id])
);

