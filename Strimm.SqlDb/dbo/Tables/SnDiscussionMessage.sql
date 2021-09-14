CREATE TABLE [dbo].[SnDiscussionMessage] (
    [Id]                             UNIQUEIDENTIFIER NOT NULL,
    [SnDiscussionTopicId]            UNIQUEIDENTIFIER NOT NULL,
    [UserId]                         UNIQUEIDENTIFIER NOT NULL,
    [Message]                        NVARCHAR (MAX)   NULL,
    [DateCreated]                    DATETIME         NOT NULL,
    [LastModifiedDate]               DATETIME         NULL,
    [Ip]                             NVARCHAR (50)    NULL,
    [Referrer]                       NVARCHAR (500)   NULL,
    [UserAgent]                      NVARCHAR (255)   NULL,
    [IsApproved]                     BIT              NOT NULL,
    [IsSpam]                         BIT              NOT NULL,
    [Spaminess]                      DECIMAL (18)     NOT NULL,
    [Signature]                      NVARCHAR (MAX)   NULL,
    [IsAnswer]                       BIT              NULL,
    [AdminAttentionRequired]         BIT              NULL,
    [AdminAttentionReportedByUserId] UNIQUEIDENTIFIER NULL,
    [AdminAttentionReportedOn]       DATETIME         NULL,
    [AdminAttentionApproved]         BIT              NULL,
    [AdminAttentionReason]           NVARCHAR (300)   NULL,
    [IsDeleteRequested]              BIT              CONSTRAINT [DF_SnDiscussionMessage_IsDeleteRequested] DEFAULT ((0)) NOT NULL,
    [DeleteRequestedOn]              DATETIME         NULL,
    [DeleteRequestedByUserId]        UNIQUEIDENTIFIER NULL,
    [IsDeleteApproved]               BIT              NULL,
    [DeleteDisapprovedReason]        NVARCHAR (300)   NULL,
    [IsPinned]                       BIT              CONSTRAINT [DF_SnDiscussionMessage_IsPinned] DEFAULT ((0)) NOT NULL,
    [PinnedOn]                       DATETIME         NULL,
    [PinnedByUserId]                 UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_SnDiscussionMessage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__SnDiscussionMessage_DeleteRequest_AspnetUsers] FOREIGN KEY ([DeleteRequestedByUserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK__SnDiscussionMessage_PinnedBy_AspnetUsers] FOREIGN KEY ([PinnedByUserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnDiscussionMessage_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnDiscussionMessage_aspnet_Users_AdminAttentionReportedByUserId] FOREIGN KEY ([AdminAttentionReportedByUserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnDiscussionMessage_SnDiscussionTopic] FOREIGN KEY ([SnDiscussionTopicId]) REFERENCES [dbo].[SnDiscussionTopic] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_AdminAttention]
    ON [dbo].[SnDiscussionMessage]([SnDiscussionTopicId] ASC, [AdminAttentionRequired] ASC, [AdminAttentionApproved] ASC, [IsSpam] ASC, [IsApproved] ASC, [UserId] ASC, [Id] ASC)
    INCLUDE([Message]);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Answer]
    ON [dbo].[SnDiscussionMessage]([SnDiscussionTopicId] ASC, [IsApproved] ASC, [IsAnswer] ASC, [Id] ASC)
    INCLUDE([Message], [DateCreated], [IsSpam], [AdminAttentionRequired], [AdminAttentionApproved], [IsDeleteRequested], [IsDeleteApproved]);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Answer_Opt1]
    ON [dbo].[SnDiscussionMessage]([SnDiscussionTopicId] ASC, [IsApproved] ASC, [IsAnswer] ASC)
    INCLUDE([Id]);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Approval_Opt1]
    ON [dbo].[SnDiscussionMessage]([IsApproved] ASC, [IsSpam] ASC, [SnDiscussionTopicId] ASC)
    INCLUDE([Message]);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Approve]
    ON [dbo].[SnDiscussionMessage]([DateCreated] ASC, [SnDiscussionTopicId] ASC, [IsApproved] ASC, [IsSpam] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Id_Opt1]
    ON [dbo].[SnDiscussionMessage]([Id] ASC)
    INCLUDE([Message]);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Modified]
    ON [dbo].[SnDiscussionMessage]([DateCreated] ASC, [IsSpam] ASC, [IsApproved] ASC, [SnDiscussionTopicId] ASC, [Id] ASC, [UserId] ASC, [LastModifiedDate] ASC, [Ip] ASC)
    INCLUDE([Message], [Referrer], [UserAgent], [Spaminess], [Signature], [IsAnswer], [AdminAttentionRequired], [AdminAttentionReportedByUserId], [AdminAttentionReportedOn], [AdminAttentionApproved], [AdminAttentionReason], [IsDeleteRequested], [DeleteRequestedOn], [DeleteRequestedByUserId], [IsDeleteApproved], [DeleteDisapprovedReason]);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Opt1]
    ON [dbo].[SnDiscussionMessage]([DateCreated] ASC, [IsSpam] ASC, [IsApproved] ASC, [SnDiscussionTopicId] ASC, [Id] ASC)
    INCLUDE([UserId], [Message], [LastModifiedDate], [Ip], [Referrer], [UserAgent], [Spaminess], [Signature], [IsAnswer], [AdminAttentionRequired], [AdminAttentionReportedByUserId], [AdminAttentionReportedOn], [AdminAttentionApproved], [AdminAttentionReason], [IsDeleteRequested], [DeleteRequestedOn], [DeleteRequestedByUserId], [IsDeleteApproved], [DeleteDisapprovedReason], [IsPinned], [PinnedOn], [PinnedByUserId]);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_TopicIdDateCreated]
    ON [dbo].[SnDiscussionMessage]([SnDiscussionTopicId] ASC, [DateCreated] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_User_Opt1]
    ON [dbo].[SnDiscussionMessage]([UserId] ASC);

