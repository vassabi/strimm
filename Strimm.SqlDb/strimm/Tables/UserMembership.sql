CREATE TABLE [strimm].[UserMembership] (
    [UserMembershipId]           INT            IDENTITY (1, 1) NOT NULL,
    [UserId]                     INT            NOT NULL,
    [Password]                   NVARCHAR (50)  NOT NULL,
    [Email]                      NVARCHAR (250) NOT NULL,
    [RecoveryEmail]              NVARCHAR (250) NULL,
    [IsLockedOut]                BIT            NOT NULL,
    [ActivationEmailSendDate]    DATETIME       NOT NULL,
    [ActivationEmailRetryCount]  INT            NOT NULL,
    [OptOutFromEmailActivation]  BIT            NOT NULL,
    [EmailActivationOptOutDate]  DATETIME       NULL,
    [LastLoginDate]              DATETIME       NOT NULL,
    [LastPasswordChangeDate]     DATETIME       NULL,
    [FailedPasswordAttemptCount] INT            NOT NULL,
    [EmailVerified]              BIT            NOT NULL,
    [IsTempUser]                 BIT            NOT NULL,
    [CreatedDate]                DATETIME       NOT NULL,
    CONSTRAINT [PK_UserMembership] PRIMARY KEY CLUSTERED ([UserMembershipId] ASC),
    CONSTRAINT [FK_UserMembership_User] FOREIGN KEY ([UserId]) REFERENCES [strimm].[User] ([UserId])
);

