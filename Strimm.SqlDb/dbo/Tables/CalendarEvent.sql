CREATE TABLE [dbo].[CalendarEvent] (
    [Id]                    UNIQUEIDENTIFIER NOT NULL,
    [AuthorId]              UNIQUEIDENTIFIER NOT NULL,
    [OwnerCalendarId]       UNIQUEIDENTIFIER NOT NULL,
    [DateCreated]           DATETIME         NOT NULL,
    [DateModified]          DATETIME         NULL,
    [StartTime]             DATETIME         NOT NULL,
    [EndTime]               DATETIME         NOT NULL,
    [AllDay]                BIT              CONSTRAINT [DF_CalendarEvent_AllDay] DEFAULT ((0)) NOT NULL,
    [Title]                 NVARCHAR (150)   NULL,
    [Description]           NVARCHAR (500)   NULL,
    [Place]                 NVARCHAR (250)   NULL,
    [RecurringDefinitionId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_CalendarEvent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_CalendarEvent] CHECK ([StartTime]<=[EndTime]),
    CONSTRAINT [FK_CalendarEvent_aspnet_Users] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[aspnet_Users] ([UserId]),
    CONSTRAINT [FK_CalendarEvent_Calendar] FOREIGN KEY ([OwnerCalendarId]) REFERENCES [dbo].[Calendar] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CalendarEvent_CalendarEventRecurringDefinition] FOREIGN KEY ([RecurringDefinitionId]) REFERENCES [dbo].[CalendarEventRecurringDefinition] ([Id]) ON DELETE CASCADE
);

