CREATE TABLE [dbo].[NewsPublishQueue] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF_NewsPublishQueue_Id] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [NewsItemId]   UNIQUEIDENTIFIER NOT NULL,
    [UserId]       UNIQUEIDENTIFIER NULL,
    [DateEntered]  DATETIME         CONSTRAINT [DF_NewsPublishQueue_DateEntered] DEFAULT (getdate()) NOT NULL,
    [DateModified] DATETIME         CONSTRAINT [DF_NewsPublishQueue_DateModified] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_NewsPublishQueue] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NewsPublishQueue_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_NewsPublishQueue_NewsItem] FOREIGN KEY ([NewsItemId]) REFERENCES [dbo].[NewsItem] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_User]
    ON [dbo].[NewsPublishQueue]([UserId] ASC);

