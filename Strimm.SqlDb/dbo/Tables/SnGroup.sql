CREATE TABLE [dbo].[SnGroup] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [Name]            NVARCHAR (500)   NOT NULL,
    [DateCreated]     DATETIME         NOT NULL,
    [MemberCount]     INT              NOT NULL,
    [Description]     NVARCHAR (MAX)   NULL,
    [IsPublic]        BIT              NOT NULL,
    [UserId]          UNIQUEIDENTIFIER NULL,
    [ImageUrl]        NVARCHAR (500)   NULL,
    [GroupCategoryId] UNIQUEIDENTIFIER NULL,
    [Slug]            NVARCHAR (500)   NULL,
    CONSTRAINT [PK_SnGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SnGroup_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_SnGroup_SnGroupCategory] FOREIGN KEY ([GroupCategoryId]) REFERENCES [dbo].[SnGroupCategory] ([Id])
);

