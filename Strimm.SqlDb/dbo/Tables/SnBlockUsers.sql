CREATE TABLE [dbo].[SnBlockUsers] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    [BlockedUserId] UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]   DATETIME         NOT NULL,
    CONSTRAINT [PK_dbo.SnBlockUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.SnBlockUsers_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.SnBlockUsers_aspnet_Users1] FOREIGN KEY ([BlockedUserId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);

