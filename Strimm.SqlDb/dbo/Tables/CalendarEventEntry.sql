CREATE TABLE [dbo].[CalendarEventEntry] (
    [CalendarId]      UNIQUEIDENTIFIER NOT NULL,
    [CalendarEventId] UNIQUEIDENTIFIER NOT NULL,
    [IsBusy]          BIT              CONSTRAINT [DF_CalendarEventEntry_IsBusy] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CalendarEventEntry] PRIMARY KEY CLUSTERED ([CalendarId] ASC, [CalendarEventId] ASC),
    CONSTRAINT [FK_CalendarEventEntry_Calendar] FOREIGN KEY ([CalendarId]) REFERENCES [dbo].[Calendar] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CalendarEventEntry_CalendarEvent] FOREIGN KEY ([CalendarEventId]) REFERENCES [dbo].[CalendarEvent] ([Id])
);

