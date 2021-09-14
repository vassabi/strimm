CREATE TABLE [dbo].[SnDiscussionBoardInRole] (
    [BoardId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK__SnDiscussionBoar__5011CCEA] PRIMARY KEY CLUSTERED ([BoardId] ASC, [RoleId] ASC),
    CONSTRAINT [FK__SnDiscussionBoardInRole__Board] FOREIGN KEY ([BoardId]) REFERENCES [dbo].[SnDiscussionBoard] ([Id]),
    CONSTRAINT [FK__SnDiscussionBoardInRole__Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[aspnet_Roles] ([RoleId])
);


GO
CREATE NONCLUSTERED INDEX [SnDiscussionBoardInRole_index]
    ON [dbo].[SnDiscussionBoardInRole]([RoleId] ASC);

