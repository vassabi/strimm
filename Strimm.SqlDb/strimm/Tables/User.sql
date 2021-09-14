CREATE TABLE [strimm].[User] (
    [UserId]         INT              IDENTITY (1, 1) NOT NULL,
    [UserName]       NVARCHAR (50)    NOT NULL,
    [CreatedDate]    DATETIME         NOT NULL,
    [LastUpdateDate] DATETIME         NOT NULL,
    [ExternalUserId] UNIQUEIDENTIFIER NULL,
    [AccountNumber]  NVARCHAR (8)     NOT NULL,
    [IsDeleted]      BIT              CONSTRAINT [DF_User_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_User_aspnet_Users] FOREIGN KEY ([ExternalUserId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);

