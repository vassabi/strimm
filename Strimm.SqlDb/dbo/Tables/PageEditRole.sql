CREATE TABLE [dbo].[PageEditRole] (
    [Id]     UNIQUEIDENTIFIER NOT NULL,
    [PageId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_PageEditRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PageEditRole_aspnet_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[aspnet_Roles] ([RoleId]),
    CONSTRAINT [FK_PageEditRole_Page] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([Id]) ON DELETE CASCADE
);

