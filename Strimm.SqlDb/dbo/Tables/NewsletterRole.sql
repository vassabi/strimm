CREATE TABLE [dbo].[NewsletterRole] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [NewsletterId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_NewsletterRole] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NewsletterRole_aspnet_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[aspnet_Roles] ([RoleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_NewsletterRole_Newsletter] FOREIGN KEY ([NewsletterId]) REFERENCES [dbo].[Newsletter] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[NewsletterRole]([NewsletterId] ASC, [RoleId] ASC);

