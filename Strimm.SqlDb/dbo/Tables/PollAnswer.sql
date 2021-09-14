CREATE TABLE [dbo].[PollAnswer] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [PollId]      UNIQUEIDENTIFIER NOT NULL,
    [Votes]       INT              CONSTRAINT [DF_PollAnswer_Votes] DEFAULT ((0)) NOT NULL,
    [Color]       NVARCHAR (200)   NOT NULL,
    [AnswerOrder] INT              CONSTRAINT [DF_PollAnswer_AnswerOrder] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_PollAnswer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PollAnswer_Poll] FOREIGN KEY ([PollId]) REFERENCES [dbo].[Poll] ([Id]) ON DELETE CASCADE
);

