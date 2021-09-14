CREATE TABLE [strimm].[AuthenticationLog] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [Date]                 DATETIME        NOT NULL,
    [ApplicationComponent] NVARCHAR (50)   NOT NULL,
    [UserName]             NVARCHAR (50)   NOT NULL,
    [Thread]               VARCHAR (255)   NOT NULL,
    [Level]                VARCHAR (50)    NOT NULL,
    [Ndc]                  NVARCHAR (2000) NOT NULL,
    [Logger]               VARCHAR (255)   NOT NULL,
    [Message]              VARCHAR (4000)  NOT NULL,
    [Exception]            VARCHAR (2000)  NULL,
    CONSTRAINT [PK_AuthenticationLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

