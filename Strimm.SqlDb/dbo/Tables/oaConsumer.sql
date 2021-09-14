CREATE TABLE [dbo].[oaConsumer] (
    [Id]                          UNIQUEIDENTIFIER NOT NULL,
    [Name]                        NVARCHAR (50)    NOT NULL,
    [Secret]                      NVARCHAR (50)    NOT NULL,
    [CallbackUrl]                 NVARCHAR (MAX)   NULL,
    [CertificateRaw]              VARBINARY (MAX)  NULL,
    [VerificationCodeFormatValue] INT              CONSTRAINT [DF_oaConsumer_VerificationCodeTypeValue] DEFAULT ((0)) NOT NULL,
    [VerificationCodeLength]      INT              CONSTRAINT [DF_oaConsumer_VerificationCodeLength] DEFAULT ((0)) NOT NULL,
    [Version]                     NVARCHAR (50)    NULL,
    CONSTRAINT [PK_oaConsumer] PRIMARY KEY CLUSTERED ([Id] ASC)
);

