CREATE TABLE [dbo].[MonthlySalary]
(
	[MonthlySalaryId] bigint NOT NULL PRIMARY KEY identity(1,1),
	[Title] varchar(max) null,
	[SalaryMonth] varchar(max) null,
	[SalaryYear] varchar(max) null,
	[LOCATION] varchar(max) null,
	[STDDAYS] INT NULL,
	[WRKDAYS] INT NULL,
	[LOPDAYS] INT NULL,
	[CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null,
)
