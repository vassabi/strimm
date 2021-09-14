CREATE TABLE [strimm].[ReservedName] (
    [ReservedNameId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (255) NOT NULL,
    [Description]    NVARCHAR (MAX) NULL,
    [OverrideCodeId] INT            NULL,
    CONSTRAINT [PK_ReservedName] PRIMARY KEY CLUSTERED ([ReservedNameId] ASC),
    CONSTRAINT [FK_ReservedName_OverrideCode] FOREIGN KEY ([OverrideCodeId]) REFERENCES [strimm].[OverrideCode] ([OverrideCodeId])
);

