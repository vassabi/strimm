CREATE TABLE [dbo].[PrivacyLevelDefinition] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [UserId]         UNIQUEIDENTIFIER NOT NULL,
    [ObjectName]     NVARCHAR (150)   NOT NULL,
    [FieldName]      NVARCHAR (150)   NOT NULL,
    [PrivacyLevelId] UNIQUEIDENTIFIER NOT NULL,
    [DateEntered]    DATETIME         CONSTRAINT [DF_PrivacyLevelDefinition_DateEntered] DEFAULT (getdate()) NULL,
    [DateModified]   DATETIME         CONSTRAINT [DF_PrivacyLevelDefinition_DateModified] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_PrivacyLevelDefinition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrivacyLevelDefinition_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrivacyLevelDefinition_PrivacyLevel] FOREIGN KEY ([PrivacyLevelId]) REFERENCES [dbo].[PrivacyLevel] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PrivacyLevelDefinition]
    ON [dbo].[PrivacyLevelDefinition]([UserId] ASC, [ObjectName] ASC, [FieldName] ASC, [PrivacyLevelId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PrivacyLevelDefinition_2]
    ON [dbo].[PrivacyLevelDefinition]([ObjectName] ASC, [UserId] ASC)
    INCLUDE([Id], [FieldName], [PrivacyLevelId], [DateEntered], [DateModified]);

