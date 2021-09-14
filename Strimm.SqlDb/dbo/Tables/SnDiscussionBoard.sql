CREATE TABLE [dbo].[SnDiscussionBoard] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [ApplicationId]     UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]        UNIQUEIDENTIFIER NOT NULL,
    [SnGroupId]         UNIQUEIDENTIFIER NULL,
    [Name]              NVARCHAR (500)   NOT NULL,
    [UserId]            UNIQUEIDENTIFIER NULL,
    [DateCreated]       DATETIME         NOT NULL,
    [Description]       NVARCHAR (MAX)   NULL,
    [DailyReportSentOn] DATETIME         NULL,
    [IsClosed]          BIT              CONSTRAINT [DF_SnDiscussionBoard_IsClosed] DEFAULT ((0)) NOT NULL,
    [IsPinned]          BIT              CONSTRAINT [DF_SnDiscussionBoard_IsPinned] DEFAULT ((0)) NOT NULL,
    [PinnedOn]          DATETIME         NULL,
    [PinnedByUserId]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_SnDiscussionBoard] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__SnDiscussionBoard_PinnedBy_AspnetUsers] FOREIGN KEY ([PinnedByUserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnDiscussionBoard_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]),
    CONSTRAINT [FK_SnDiscussionBoard_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnDiscussionBoard_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]),
    CONSTRAINT [FK_SnDiscussionBoard_SnGroup] FOREIGN KEY ([SnGroupId]) REFERENCES [dbo].[SnGroup] ([Id])
);

