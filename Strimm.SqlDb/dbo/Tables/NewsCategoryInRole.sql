CREATE TABLE [dbo].[NewsCategoryInRole] (
    [Id]              UNIQUEIDENTIFIER CONSTRAINT [DF_NewsCategories_InRoles_Id] DEFAULT (newid()) NOT NULL,
    [NewsCategoryId]  UNIQUEIDENTIFIER NOT NULL,
    [RoleId]          UNIQUEIDENTIFIER NOT NULL,
    [ViewPermissions] BIT              CONSTRAINT [DF_NewsCategoryInRole_ViewPermissions] DEFAULT ((0)) NOT NULL,
    [EditPermissions] BIT              CONSTRAINT [DF_NewsCategoryInRole_EditPermissions] DEFAULT ((0)) NOT NULL,
    [DateEntered]     DATETIME         NULL,
    [DateModified]    DATETIME         NULL,
    CONSTRAINT [PK_NewsCategories_InRoles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NewsCategories_InRoles_aspnet_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[aspnet_Roles] ([RoleId]),
    CONSTRAINT [FK_NewsCategories_InRoles_NewsCategories] FOREIGN KEY ([NewsCategoryId]) REFERENCES [dbo].[NewsCategory] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[NewsCategoryInRole]([NewsCategoryId] ASC, [RoleId] ASC);

