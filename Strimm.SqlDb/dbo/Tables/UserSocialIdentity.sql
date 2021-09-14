CREATE TABLE [dbo].[UserSocialIdentity] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [UserId]         UNIQUEIDENTIFIER NOT NULL,
    [ProviderName]   NVARCHAR (50)    NOT NULL,
    [ProviderUserId] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_UserSocialIdentity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserSocialIdentity_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);

