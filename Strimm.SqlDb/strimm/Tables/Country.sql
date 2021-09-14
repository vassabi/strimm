CREATE TABLE [strimm].[Country] (
    [CountryId] INT        IDENTITY (1, 1) NOT NULL,
    [Name]      NCHAR (64) NOT NULL,
    [Code_3]    NCHAR (3)  NULL,
    [Code_2]    NCHAR (2)  NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([CountryId] ASC)
);

