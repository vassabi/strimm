CREATE TABLE [dbo].[SnGroupRequest] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [GroupId]      UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]  DATETIME         NOT NULL,
    [DateAccepted] DATETIME         NULL,
    [RequestType]  INT              CONSTRAINT [DF_SnGroupRequest_RequestType] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SnGroupRequest] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnGroupRequest_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_SnGroupRequest_SnGroup] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[SnGroup] ([Id]) ON DELETE CASCADE
);

