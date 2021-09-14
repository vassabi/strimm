CREATE TABLE [dbo].[List] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Title]         NVARCHAR (200)   NOT NULL,
    [DateModified]  DATETIME         NOT NULL,
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    [ApplicationId] UNIQUEIDENTIFIER NOT NULL,
    [ListType]      INT              CONSTRAINT [DF_List_ListType] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_List] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_List_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]) ON DELETE CASCADE,
    CONSTRAINT [FK_List_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Filter]
    ON [dbo].[List]([UserId] ASC, [ApplicationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Title]
    ON [dbo].[List]([Title] ASC);

