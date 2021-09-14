CREATE TABLE [dbo].[oaToken] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Token]            NVARCHAR (MAX)   NOT NULL,
    [TokenSecret]      NVARCHAR (50)    NOT NULL,
    [CreatedOnUtc]     DATETIME         NOT NULL,
    [CallbackUrl]      NVARCHAR (MAX)   NULL,
    [Scope]            NVARCHAR (MAX)   NULL,
    [ConsumerId]       UNIQUEIDENTIFIER NOT NULL,
    [UserId]           UNIQUEIDENTIFIER NULL,
    [VerificationCode] NVARCHAR (50)    NULL,
    [ExpirationDate]   DATETIME         NULL,
    [IsAccess]         BIT              CONSTRAINT [DF_oaToken_IsAccess] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_oaToken] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_oaToken_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_oaToken_oaConsumer] FOREIGN KEY ([ConsumerId]) REFERENCES [dbo].[oaConsumer] ([Id])
);

