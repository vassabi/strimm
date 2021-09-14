CREATE TABLE [dbo].[CalendarEventRecurringDefinitionType] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR (250)   NOT NULL,
    [Abrv] NVARCHAR (250)   NOT NULL,
    CONSTRAINT [PK_CalendarEventRecurringDefinitionType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

