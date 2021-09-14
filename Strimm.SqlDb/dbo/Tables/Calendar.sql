CREATE TABLE [dbo].[Calendar] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [OwnerId]      UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]  DATETIME         NOT NULL,
    [DateModified] DATETIME         NULL,
    [Name]         NVARCHAR (255)   NULL,
    [Slug]         NVARCHAR (255)   NULL,
    CONSTRAINT [PK_Calendar] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Calendar_aspnet_Users] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);

