CREATE TABLE [dbo].[Ad] (
    [Id]            UNIQUEIDENTIFIER CONSTRAINT [DF_Ads_Id] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [CampaignId]    UNIQUEIDENTIFIER NOT NULL,
    [ImageUrl]      NVARCHAR (255)   NULL,
    [AdContent]     NVARCHAR (MAX)   NULL,
    [NavigateUrl]   NVARCHAR (255)   NOT NULL,
    [Keyword]       NVARCHAR (255)   NULL,
    [AlternateText] NVARCHAR (255)   NULL,
    [Caption]       NVARCHAR (255)   NULL,
    [Weight]        INT              CONSTRAINT [DF_Ads_Weight] DEFAULT ((0)) NOT NULL,
    [ValidFrom]     DATETIME         NULL,
    [ValidTo]       DATETIME         NULL,
    [DateEntered]   DATETIME         NULL,
    [DateModified]  DATETIME         NULL,
    CONSTRAINT [PK_Ad] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Ad_Campaign] FOREIGN KEY ([CampaignId]) REFERENCES [dbo].[Campaign] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Keyword]
    ON [dbo].[Ad]([Keyword] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Valid]
    ON [dbo].[Ad]([ValidFrom] ASC, [ValidTo] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Weight]
    ON [dbo].[Ad]([Weight] ASC);

