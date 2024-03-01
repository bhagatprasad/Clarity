CREATE TABLE [dbo].[EmployeeEmployment]
(
	[EmployeeEmploymentId] bigint not null primary key identity(1,1),
	[EmployeeId] bigint null,
	[CompanyName] varchar(max) null,
	[Address] varchar(max) null,
	[Designation] varchar(max) null,
	[StartedOn] datetimeoffset null,
	[EndedOn] datetimeoffset null,
	[Reason] varchar(max) null,
	[ReportingManager] varchar(max) null,
	[HREmail] varchar(max) null,
	[Referance] varchar(max) null,
	[CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null,
	FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId)
)
