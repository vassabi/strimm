CREATE TABLE [dbo].[SnDiscussionTopic] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [SnDiscussionBoardId] UNIQUEIDENTIFIER NOT NULL,
    [Title]               NVARCHAR (500)   NOT NULL,
    [UserId]              UNIQUEIDENTIFIER NULL,
    [DateCreated]         DATETIME         NOT NULL,
    [Ip]                  NVARCHAR (50)    NULL,
    [Referrer]            NVARCHAR (500)   NULL,
    [UserAgent]           NVARCHAR (255)   NULL,
    [IsApproved]          BIT              NOT NULL,
    [IsSpam]              BIT              NOT NULL,
    [Spaminess]           DECIMAL (18)     NOT NULL,
    [Signature]           NVARCHAR (MAX)   NULL,
    [TimesViewed]         INT              NULL,
    [IsClosed]            BIT              CONSTRAINT [DF_SnDiscussionTopic_IsClosed] DEFAULT ((0)) NOT NULL,
    [IsPinned]            BIT              CONSTRAINT [DF_SnDiscussionTopic_IsPinned] DEFAULT ((0)) NOT NULL,
    [PinnedOn]            DATETIME         NULL,
    [PinnedByUserId]      UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_SnDiscussionTopic] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__SnDiscussionTopic_PinnedBy_AspnetUsers] FOREIGN KEY ([PinnedByUserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnDiscussionTopic_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnDiscussionTopic_SnDiscussionBoard] FOREIGN KEY ([SnDiscussionBoardId]) REFERENCES [dbo].[SnDiscussionBoard] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_Approved]
    ON [dbo].[SnDiscussionTopic]([DateCreated] DESC, [IsSpam] ASC, [IsApproved] ASC, [UserId] ASC, [Id] ASC, [SnDiscussionBoardId] ASC)
    INCLUDE([Title]);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_ApproveSpam_Opt1]
    ON [dbo].[SnDiscussionTopic]([IsApproved] ASC, [SnDiscussionBoardId] ASC, [IsSpam] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_Board_Opt1]
    ON [dbo].[SnDiscussionTopic]([SnDiscussionBoardId] ASC)
    INCLUDE([Id], [Title], [UserId], [DateCreated], [Ip], [Referrer], [UserAgent], [IsApproved], [IsSpam], [Spaminess], [Signature], [TimesViewed], [IsClosed], [IsPinned], [PinnedOn], [PinnedByUserId]);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_Board2_Opt1]
    ON [dbo].[SnDiscussionTopic]([SnDiscussionBoardId] ASC)
    INCLUDE([Id], [Title], [UserId], [IsApproved], [IsSpam]);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_DateCreated_Opt1]
    ON [dbo].[SnDiscussionTopic]([DateCreated] DESC)
    INCLUDE([Id], [SnDiscussionBoardId], [Title], [UserId], [Ip], [Referrer], [UserAgent], [IsApproved], [IsSpam], [Spaminess], [Signature], [TimesViewed], [IsClosed], [IsPinned], [PinnedOn], [PinnedByUserId]);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_Pinned_Opt1]
    ON [dbo].[SnDiscussionTopic]([SnDiscussionBoardId] ASC, [IsApproved] ASC, [IsPinned] ASC)
    INCLUDE([Id], [IsSpam]);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_Spam_Opt1]
    ON [dbo].[SnDiscussionTopic]([SnDiscussionBoardId] ASC, [IsSpam] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_User_Opt1]
    ON [dbo].[SnDiscussionTopic]([UserId] ASC);

