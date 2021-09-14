CREATE TABLE [dbo].[CalendarModerator] (
    [CalendarId] UNIQUEIDENTIFIER NOT NULL,
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_CalendarModerator] PRIMARY KEY CLUSTERED ([CalendarId] ASC, [UserId] ASC),
    CONSTRAINT [FK_CalendarModerator_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_CalendarModerator_Calendar] FOREIGN KEY ([CalendarId]) REFERENCES [dbo].[Calendar] ([Id])
);

