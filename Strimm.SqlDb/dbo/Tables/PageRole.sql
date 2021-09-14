CREATE TABLE [dbo].[PageRole] (
    [Id]     UNIQUEIDENTIFIER NOT NULL,
    [PageId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_PageRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PageRole_aspnet_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[aspnet_Roles] ([RoleId]),
    CONSTRAINT [FK_PageRole_Page] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[PageRole]([PageId] ASC, [RoleId] ASC);

