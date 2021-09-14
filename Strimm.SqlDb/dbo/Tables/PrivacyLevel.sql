CREATE TABLE [dbo].[PrivacyLevel] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [PrivacyLevel] NVARCHAR (100)   NOT NULL,
    [Sort]         INT              CONSTRAINT [DF_PrivacyLevel_Sort] DEFAULT ((0)) NOT NULL,
    [DateEntered]  DATETIME         CONSTRAINT [DF_PrivacyLevel_DateEntered] DEFAULT (getdate()) NULL,
    [DateModified] DATETIME         CONSTRAINT [DF_PrivacyLevel_DateModified] DEFAULT (getdate()) NULL,
    [Abrv]         NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_PrivacyLevel] PRIMARY KEY CLUSTERED ([Id] ASC)
);

