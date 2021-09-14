CREATE TABLE [dbo].[UserAvatar] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Avatar]       IMAGE            NOT NULL,
    [DateEntered]  DATETIME         NULL,
    [DateModified] DATETIME         NULL,
    CONSTRAINT [PK_UserAvatar] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserAvatar_aspnet_Users] FOREIGN KEY ([Id]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE CASCADE
);

