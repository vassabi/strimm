CREATE TABLE [dbo].[PollAnswerLocalization] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [PollAnswerId] UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]   UNIQUEIDENTIFIER NOT NULL,
    [Answer]       NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_PollAnswerLocalization] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PollAnswerLocalization_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PollAnswerLocalization_PollAnswer] FOREIGN KEY ([PollAnswerId]) REFERENCES [dbo].[PollAnswer] ([Id]) ON DELETE CASCADE
);

