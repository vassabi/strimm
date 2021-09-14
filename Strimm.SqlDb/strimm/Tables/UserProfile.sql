CREATE TABLE [strimm].[UserProfile] (
    [UserProfileId]                    INT            IDENTITY (1, 1) NOT NULL,
    [UserId]                           INT            NOT NULL,
    [FirstName]                        NVARCHAR (250) NULL,
    [LastName]                         NVARCHAR (250) NULL,
    [BirthDate]                        DATETIME       NULL,
    [Address]                          NVARCHAR (250) NULL,
    [City]                             NVARCHAR (250) NULL,
    [StateOrProvince]                  NVARCHAR (250) NULL,
    [ZipCode]                          NVARCHAR (250) NULL,
    [Country]                          NVARCHAR (250) NULL,
    [Gender]                           NVARCHAR (50)  NULL,
    [UserStory]                        NVARCHAR (MAX) NULL,
    [Company]                          NVARCHAR (250) NULL,
    [TermsAndConditionsAcceptanceDate] DATETIME       NULL,
    [ProfileImageUrl]                  NVARCHAR (MAX) NULL,
    [UserIp]                           NVARCHAR (45)  NULL,
    [PhoneNumber]                      NVARCHAR (20)  NULL,
    [CreatedDate]                      DATETIME       NOT NULL,
    [BoardName]                        NVARCHAR (255) NULL,
    CONSTRAINT [PK_UserProfile_1] PRIMARY KEY CLUSTERED ([UserProfileId] ASC),
    CONSTRAINT [FK_UserProfile_User] FOREIGN KEY ([UserId]) REFERENCES [strimm].[User] ([UserId])
);

