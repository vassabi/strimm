CREATE TABLE [dbo].[SnDiscussionBoardModerator] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [SnDiscussionBoardId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]              UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SnDiscussionBoardModerator] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnDiscussionBoardModerator_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnDiscussionBoardModerator_SnDiscussionBoard] FOREIGN KEY ([SnDiscussionBoardId]) REFERENCES [dbo].[SnDiscussionBoard] ([Id]) ON DELETE CASCADE
);

