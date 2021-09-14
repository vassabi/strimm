CREATE TABLE [dbo].[CalendarViewRole] (
    [CalendarId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_CalendarInRole] PRIMARY KEY CLUSTERED ([CalendarId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_CalendarInRole_aspnet_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[aspnet_Roles] ([RoleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CalendarInRole_Calendar] FOREIGN KEY ([CalendarId]) REFERENCES [dbo].[Calendar] ([Id]) ON DELETE CASCADE
);

