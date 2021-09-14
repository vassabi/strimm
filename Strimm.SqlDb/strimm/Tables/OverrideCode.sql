CREATE TABLE [strimm].[OverrideCode] (
    [OverrideCodeId] INT              NOT NULL,
    [Code]           NCHAR (10)       NOT NULL,
    [UserId]         UNIQUEIDENTIFIER NOT NULL,
    [Comments]       NVARCHAR (MAX)   NULL,
    [CreatedDate]    DATETIME         NOT NULL,
    CONSTRAINT [PK_OverrideCode] PRIMARY KEY CLUSTERED ([OverrideCodeId] ASC)
);

