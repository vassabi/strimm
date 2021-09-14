CREATE TABLE [dbo].[Poll] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Title]         NVARCHAR (200)   NOT NULL,
    [DateModified]  DATETIME         CONSTRAINT [DF_Poll_DateCreated] DEFAULT (getdate()) NOT NULL,
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    [ApplicationId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Poll] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Poll_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]),
    CONSTRAINT [FK_Poll_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Application]
    ON [dbo].[Poll]([ApplicationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Title]
    ON [dbo].[Poll]([Title] ASC);

