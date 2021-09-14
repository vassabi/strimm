CREATE TABLE [strimm].[State] (
    [StateId]   INT        IDENTITY (1, 1) NOT NULL,
    [Name]      NCHAR (64) NOT NULL,
    [Code_3]    NCHAR (10) NULL,
    [Code_2]    NCHAR (2)  NULL,
    [CountryId] INT        NOT NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([StateId] ASC),
    CONSTRAINT [FK_State_Country] FOREIGN KEY ([CountryId]) REFERENCES [strimm].[Country] ([CountryId])
);

