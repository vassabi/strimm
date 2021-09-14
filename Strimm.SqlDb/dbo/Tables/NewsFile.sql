CREATE TABLE [dbo].[NewsFile] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_NewsFiles_Id] DEFAULT (newid()) NOT NULL,
    [NewsItemId]  UNIQUEIDENTIFIER NOT NULL,
    [ContentType] NVARCHAR (50)    NOT NULL,
    [Name]        NVARCHAR (50)    NULL,
    [Content]     VARBINARY (MAX)  NOT NULL,
    CONSTRAINT [PK_NewsFiles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NewsFiles_NewsItems] FOREIGN KEY ([NewsItemId]) REFERENCES [dbo].[NewsItem] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_NewsItemId]
    ON [dbo].[NewsFile]([NewsItemId] ASC);

