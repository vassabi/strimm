CREATE TABLE [dbo].[NavigationRole] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [NavigationId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_NavigationRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NavigationRole_aspnet_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[aspnet_Roles] ([RoleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_NavigationRole_Navigation] FOREIGN KEY ([NavigationId]) REFERENCES [dbo].[Navigation] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[NavigationRole]([NavigationId] ASC, [RoleId] ASC);

