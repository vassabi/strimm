CREATE TABLE [dbo].[SnEvent] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [UserId]              UNIQUEIDENTIFIER NOT NULL,
    [EventTypeId]         UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]         DATETIME         NOT NULL,
    [FriendId]            UNIQUEIDENTIFIER NULL,
    [BlogPostId]          UNIQUEIDENTIFIER NULL,
    [SnGroupId]           UNIQUEIDENTIFIER NULL,
    [SnAlbumId]           UNIQUEIDENTIFIER NULL,
    [SnFileId]            UNIQUEIDENTIFIER NULL,
    [SnDiscussionBoardId] UNIQUEIDENTIFIER NULL,
    [SnDiscussionTopicId] UNIQUEIDENTIFIER NULL,
    [OaConsumerId]        UNIQUEIDENTIFIER NULL,
    [CustomId1]           UNIQUEIDENTIFIER NULL,
    [CustomId2]           UNIQUEIDENTIFIER NULL,
    [CustomId3]           UNIQUEIDENTIFIER NULL,
    [EventContent]        NVARCHAR (500)   NULL,
    [PlainEventTitle]     NVARCHAR (150)   NULL,
    [PlainEventUrl]       NVARCHAR (500)   NULL,
    [SnNoteId]            UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_SnEvent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnEvent_aspnet_Users_FriendId] FOREIGN KEY ([FriendId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnEvent_aspnet_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SnEvent_BlogPost] FOREIGN KEY ([BlogPostId]) REFERENCES [dbo].[BlogPost] ([Id]),
    CONSTRAINT [FK_SnEvent_oaConsumer] FOREIGN KEY ([OaConsumerId]) REFERENCES [dbo].[oaConsumer] ([Id]),
    CONSTRAINT [FK_SnEvent_SnAlbum] FOREIGN KEY ([SnAlbumId]) REFERENCES [dbo].[SnAlbum] ([Id]),
    CONSTRAINT [FK_SnEvent_SnDiscussionBoard] FOREIGN KEY ([SnDiscussionBoardId]) REFERENCES [dbo].[SnDiscussionBoard] ([Id]),
    CONSTRAINT [FK_SnEvent_SnDiscussionTopic] FOREIGN KEY ([SnDiscussionTopicId]) REFERENCES [dbo].[SnDiscussionTopic] ([Id]),
    CONSTRAINT [FK_SnEvent_SnEventType] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[SnEventType] ([Id]),
    CONSTRAINT [FK_SnEvent_SnFile] FOREIGN KEY ([SnFileId]) REFERENCES [dbo].[SnFile] ([Id]),
    CONSTRAINT [FK_SnEvent_SnGroup] FOREIGN KEY ([SnGroupId]) REFERENCES [dbo].[SnGroup] ([Id]),
    CONSTRAINT [FK_SnEvent_SnNote] FOREIGN KEY ([SnNoteId]) REFERENCES [dbo].[SnNote] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SnEvent_EventTypeId]
    ON [dbo].[SnEvent]([EventTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SnEvent_UserId]
    ON [dbo].[SnEvent]([UserId] ASC);

