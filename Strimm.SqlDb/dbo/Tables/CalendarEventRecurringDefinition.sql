CREATE TABLE [dbo].[CalendarEventRecurringDefinition] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [RecurringTypeId] UNIQUEIDENTIFIER NOT NULL,
    [RepeatFactor]    INT              NOT NULL,
    [Sunday]          BIT              CONSTRAINT [DF_CalendarEventRecurringDefinition_Sunday] DEFAULT ((0)) NOT NULL,
    [Monday]          BIT              CONSTRAINT [DF_CalendarEventRecurringDefinition_Monday] DEFAULT ((0)) NOT NULL,
    [Tuesday]         BIT              CONSTRAINT [DF_CalendarEventRecurringDefinition_Tuesday] DEFAULT ((0)) NOT NULL,
    [Wednesday]       BIT              CONSTRAINT [DF_CalendarEventRecurringDefinition_Wednesday] DEFAULT ((0)) NOT NULL,
    [Thursday]        BIT              CONSTRAINT [DF_CalendarEventRecurringDefinition_Thursday] DEFAULT ((0)) NOT NULL,
    [Friday]          BIT              CONSTRAINT [DF_CalendarEventRecurringDefinition_Friday] DEFAULT ((0)) NOT NULL,
    [Saturday]        BIT              CONSTRAINT [DF_CalendarEventRecurringDefinition_Saturday] DEFAULT ((0)) NOT NULL,
    [DayOfMonth]      INT              CONSTRAINT [DF_CalendarEventRecurringDefinition_DayOfMonth] DEFAULT ((0)) NULL,
    [MonthOfYear]     INT              NULL,
    [StartsOn]        DATETIME         NOT NULL,
    [EndsOn]          DATETIME         NOT NULL,
    CONSTRAINT [PK_CalendarEventRecurringDefinition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CalendarEventRecurringDefinition_CalendarEventRecurringDefinitionType] FOREIGN KEY ([RecurringTypeId]) REFERENCES [dbo].[CalendarEventRecurringDefinitionType] ([Id])
);

