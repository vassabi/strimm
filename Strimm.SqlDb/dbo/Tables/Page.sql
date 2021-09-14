CREATE TABLE [dbo].[Page] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [ApplicationId]         UNIQUEIDENTIFIER NOT NULL,
    [Url]                   NVARCHAR (450)   NULL,
    [UserId]                UNIQUEIDENTIFIER NULL,
    [DateCreated]           DATETIME         CONSTRAINT [DF_Page_DateCreated] DEFAULT (getdate()) NOT NULL,
    [TemplateName]          NVARCHAR (100)   NULL,
    [CacheDuration]         INT              CONSTRAINT [DF_Page_CacheDuration] DEFAULT ((0)) NOT NULL,
    [VaryByCustom]          NVARCHAR (100)   NULL,
    [VaryByParam]           NVARCHAR (100)   NULL,
    [VaryByContentEncoding] NVARCHAR (100)   NULL,
    [VaryByHeader]          NVARCHAR (100)   NULL,
    [VaryByControl]         NVARCHAR (100)   NULL,
    [CacheDependencyKeys]   NVARCHAR (100)   NULL,
    [CacheLocation]         INT              CONSTRAINT [DF_Page_CacheLocation] DEFAULT ((-1)) NOT NULL,
    [SlidingExpiration]     INT              CONSTRAINT [DF_Page_SlidingExpiration] DEFAULT ((-1)) NOT NULL,
    [Theme]                 NVARCHAR (50)    NULL,
    [MasterPage]            NVARCHAR (50)    NULL,
    CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Page_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Page_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Application]
    ON [dbo].[Page]([ApplicationId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Page_1]
    ON [dbo].[Page]([Url] ASC);

