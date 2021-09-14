CREATE TABLE [dbo].[SnRelationship] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [NoteId]              UNIQUEIDENTIFIER NULL,
    [FileId]              UNIQUEIDENTIFIER NULL,
    [AlbumId]             UNIQUEIDENTIFIER NULL,
    [BlogPostId]          UNIQUEIDENTIFIER NULL,
    [MessageId]           UNIQUEIDENTIFIER NULL,
    [DiscussionMessageId] UNIQUEIDENTIFIER NULL,
    [CustomId1]           UNIQUEIDENTIFIER NULL,
    [CustomId2]           UNIQUEIDENTIFIER NULL,
    [CustomId3]           UNIQUEIDENTIFIER NULL,
    [DiscussionBoardId]   UNIQUEIDENTIFIER NULL,
    [DiscussionTopicId]   UNIQUEIDENTIFIER NULL,
    [DocumentId]          UNIQUEIDENTIFIER NULL,
    [NewsItemId]          UNIQUEIDENTIFIER NULL,
    [ListItemId]          UNIQUEIDENTIFIER NULL,
    [BlogId]              UNIQUEIDENTIFIER NULL,
    [ApplicationId]       UNIQUEIDENTIFIER NULL,
    [CalendarEventId]     UNIQUEIDENTIFIER NULL,
    [GroupId]             UNIQUEIDENTIFIER NULL,
    [UserId]              UNIQUEIDENTIFIER NULL,
    [CampaignId]          UNIQUEIDENTIFIER NULL,
    [NewsCategoryId]      UNIQUEIDENTIFIER NULL,
    [NewsletterId]        UNIQUEIDENTIFIER NULL,
    [PageId]              UNIQUEIDENTIFIER NULL,
    [PollId]              UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_SnRelationship] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnRelationship_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]),
    CONSTRAINT [FK_SnRelationship_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnRelationship_Blog] FOREIGN KEY ([BlogId]) REFERENCES [dbo].[Blog] ([Id]),
    CONSTRAINT [FK_SnRelationship_BlogPost] FOREIGN KEY ([BlogPostId]) REFERENCES [dbo].[BlogPost] ([Id]),
    CONSTRAINT [FK_SnRelationship_CalendarEvent] FOREIGN KEY ([CalendarEventId]) REFERENCES [dbo].[CalendarEvent] ([Id]),
    CONSTRAINT [FK_SnRelationship_Campaign] FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Campaign] ([Id]),
    CONSTRAINT [FK_SnRelationship_Document] FOREIGN KEY ([DocumentId]) REFERENCES [dbo].[Document] ([Id]),
    CONSTRAINT [FK_SnRelationship_ListItem] FOREIGN KEY ([ListItemId]) REFERENCES [dbo].[ListItem] ([Id]),
    CONSTRAINT [FK_SnRelationship_NewsCategory] FOREIGN KEY ([NewsCategoryId]) REFERENCES [dbo].[NewsCategory] ([Id]),
    CONSTRAINT [FK_SnRelationship_NewsItem] FOREIGN KEY ([NewsItemId]) REFERENCES [dbo].[NewsItem] ([Id]),
    CONSTRAINT [FK_SnRelationship_Newsletter] FOREIGN KEY ([NewsletterId]) REFERENCES [dbo].[Newsletter] ([Id]),
    CONSTRAINT [FK_SnRelationship_Page] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([Id]),
    CONSTRAINT [FK_SnRelationship_Poll] FOREIGN KEY ([PollId]) REFERENCES [dbo].[Poll] ([Id]),
    CONSTRAINT [FK_SnRelationship_SnAlbum] FOREIGN KEY ([AlbumId]) REFERENCES [dbo].[SnAlbum] ([Id]),
    CONSTRAINT [FK_SnRelationship_SnDiscussionBoard] FOREIGN KEY ([DiscussionBoardId]) REFERENCES [dbo].[SnDiscussionBoard] ([Id]),
    CONSTRAINT [FK_SnRelationship_SnDiscussionMessage] FOREIGN KEY ([DiscussionMessageId]) REFERENCES [dbo].[SnDiscussionMessage] ([Id]),
    CONSTRAINT [FK_SnRelationship_SnDiscussionTopic] FOREIGN KEY ([DiscussionTopicId]) REFERENCES [dbo].[SnDiscussionTopic] ([Id]),
    CONSTRAINT [FK_SnRelationship_SnFile] FOREIGN KEY ([FileId]) REFERENCES [dbo].[SnFile] ([Id]),
    CONSTRAINT [FK_SnRelationship_SnGroup] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[SnGroup] ([Id]),
    CONSTRAINT [FK_SnRelationship_SnMessage] FOREIGN KEY ([MessageId]) REFERENCES [dbo].[SnMessage] ([Id]),
    CONSTRAINT [FK_SnRelationship_SnNote] FOREIGN KEY ([NoteId]) REFERENCES [dbo].[SnNote] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_SnRelationship_BlogPost_Opt1]
    ON [dbo].[SnRelationship]([BlogPostId] ASC)
    INCLUDE([Id], [NoteId], [FileId], [AlbumId], [MessageId], [DiscussionMessageId], [CustomId1], [CustomId2], [CustomId3], [DiscussionBoardId], [DiscussionTopicId], [DocumentId], [NewsItemId], [ListItemId], [BlogId], [ApplicationId], [CalendarEventId], [GroupId], [UserId], [CampaignId], [NewsCategoryId], [NewsletterId], [PageId], [PollId]);


GO
CREATE NONCLUSTERED INDEX [IX_SnRelationship_BlogPost2_Opt1]
    ON [dbo].[SnRelationship]([BlogPostId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SnRelationship_DiscussionBoard]
    ON [dbo].[SnRelationship]([Id] ASC, [DiscussionBoardId] ASC)
    INCLUDE([DiscussionTopicId]);


GO
CREATE NONCLUSTERED INDEX [IX_SnRelationship_DiscussionMessage]
    ON [dbo].[SnRelationship]([DiscussionMessageId] ASC, [Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SnRelationship_DiscussionMessage_Opt1]
    ON [dbo].[SnRelationship]([DiscussionMessageId] ASC)
    INCLUDE([Id], [NoteId], [FileId], [AlbumId], [BlogPostId], [MessageId], [CustomId1], [CustomId2], [CustomId3], [DiscussionBoardId], [DiscussionTopicId], [DocumentId], [NewsItemId], [ListItemId], [BlogId], [ApplicationId], [CalendarEventId], [GroupId], [UserId], [CampaignId], [NewsCategoryId], [NewsletterId], [PageId], [PollId]);


GO
CREATE NONCLUSTERED INDEX [IX_SnRelationship_DiscussionTopic]
    ON [dbo].[SnRelationship]([DiscussionTopicId] ASC)
    INCLUDE([Id], [NoteId], [FileId], [AlbumId], [BlogPostId], [MessageId], [DiscussionMessageId], [CustomId1], [CustomId2], [CustomId3], [DiscussionBoardId], [DocumentId], [NewsItemId], [ListItemId], [BlogId], [ApplicationId], [CalendarEventId]);


GO
CREATE NONCLUSTERED INDEX [IX_SnRelationship_DiscussionTopic_Opt1]
    ON [dbo].[SnRelationship]([DiscussionTopicId] ASC, [Id] ASC)
    INCLUDE([NoteId], [FileId], [AlbumId], [BlogPostId], [MessageId], [DiscussionMessageId], [CustomId1], [CustomId2], [CustomId3], [DiscussionBoardId], [DocumentId], [NewsItemId], [ListItemId], [BlogId], [ApplicationId], [CalendarEventId], [GroupId], [UserId], [CampaignId], [NewsCategoryId], [NewsletterId], [PageId], [PollId]);

