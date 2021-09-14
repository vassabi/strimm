CREATE TABLE [dbo].[SnGroupMember] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    [GroupId]     UNIQUEIDENTIFIER NOT NULL,
    [DateCreated] DATETIME         NOT NULL,
    [IsAdmin]     BIT              NOT NULL,
    [IsApproved]  BIT              NOT NULL,
    CONSTRAINT [PK_SnGroupMember] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnGroupMember_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SnGroupMember_SnGroup] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[SnGroup] ([Id]) ON DELETE CASCADE
);

