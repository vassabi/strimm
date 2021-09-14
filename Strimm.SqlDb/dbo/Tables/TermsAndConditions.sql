CREATE TABLE [dbo].[TermsAndConditions] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Terms]        NVARCHAR (MAX)   NOT NULL,
    [DateEntered]  DATETIME         NULL,
    [DateModified] DATETIME         NULL,
    CONSTRAINT [PK_TermsAndConditions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

