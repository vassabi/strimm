CREATE TABLE [dbo].[PollLocalization] (
    [Id]                     UNIQUEIDENTIFIER NOT NULL,
    [PollId]                 UNIQUEIDENTIFIER NOT NULL,
    [LanguageId]             UNIQUEIDENTIFIER NOT NULL,
    [Question]               NVARCHAR (MAX)   NOT NULL,
    [VoteButtonText]         NVARCHAR (100)   NULL,
    [TotalVotesTextTemplate] NVARCHAR (200)   NULL,
    CONSTRAINT [PK_PollLocalization] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PollLocalization_Language] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PollLocalization_Poll] FOREIGN KEY ([PollId]) REFERENCES [dbo].[Poll] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PollLocalization]
    ON [dbo].[PollLocalization]([LanguageId] ASC, [PollId] ASC);

