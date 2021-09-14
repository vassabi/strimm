CREATE TABLE [dbo].[CalendarEditRole] (
    [CalendarId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_CalendarEditRole] PRIMARY KEY CLUSTERED ([CalendarId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_CalendarEditRole_aspnet_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[aspnet_Roles] ([RoleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CalendarEditRole_Calendar] FOREIGN KEY ([CalendarId]) REFERENCES [dbo].[Calendar] ([Id]) ON DELETE CASCADE
);

