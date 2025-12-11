CREATE TABLE [dbo].[TimesheetTask]
(
	[Id]					bigint				NOT NULL PRIMARY KEY identity(1,1),
	[TimesheetId]			bigint				NULL,
	[TaskItemId]			bigint				NULL,
	[TaskCodeId]			bigint				NULL,
	[MondayHours]			bigint				NULL,
	[TuesdayHours]			bigint				NULL,
	[WednesdayHours]		bigint				NULL,
	[ThursdayHours]			bigint				NULL,
	[FridayHours]			bigint				NULL,
	[SaturdayHours]			bigint				NULL,
	[SundayHours]			bigint				NULL,
	[CreatedBy]				bigint				NULL,
    [CreatedOn]				datetimeoffset		NULL,
	[ModifiedBy]			bigint				NULL,
	[ModifiedOn]			datetimeoffset		NULL,
    [IsActive]				bit                 NULL,
	FOREIGN KEY (TimesheetId) REFERENCES Timesheet(Id)
)
