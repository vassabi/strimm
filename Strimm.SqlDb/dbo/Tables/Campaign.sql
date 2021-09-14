CREATE TABLE [dbo].[Campaign] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [ApplicationId] UNIQUEIDENTIFIER NOT NULL,
    [Name]          NVARCHAR (250)   NOT NULL,
    [Start]         DATETIME         NULL,
    [End]           DATETIME         NULL,
    [Active]        BIT              NOT NULL,
    [DateEntered]   DATETIME         NOT NULL,
    [DateModified]  DATETIME         NOT NULL,
    CONSTRAINT [PK_Campaign] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Campaign_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Active]
    ON [dbo].[Campaign]([Active] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Period]
    ON [dbo].[Campaign]([Start] ASC, [End] ASC);

