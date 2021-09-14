CREATE TABLE [strimm].[VideoProvider] (
    [VideoProviderId]   INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50)  NOT NULL,
    [Description]       NVARCHAR (MAX) NULL,
    [IsActive]          BIT            NOT NULL,
    [CreatedDate]       DATETIME       NOT NULL,
    [ProdEffectiveDate] DATETIME       NULL,
    [QaEffectiveDate]   DATETIME       NULL,
    CONSTRAINT [PK_VideoProvider] PRIMARY KEY CLUSTERED ([VideoProviderId] ASC)
);

