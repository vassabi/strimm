CREATE TABLE [dbo].[SnFriendList] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (200)   NOT NULL,
    [DateCreated] DATETIME         NOT NULL,
    CONSTRAINT [PK_SnFriendList] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnFriendList_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);

