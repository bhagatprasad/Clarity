CREATE TABLE [dbo].[EmployeeEducation]
(
	[EmployeeEducationId] bigint not null primary key identity(1,1),
	[EmployeeId] bigint null,
	[Degree] varchar(max) null,
	[FeildOfStudy] varchar(max) null,
	[Institution] varchar(max) null,
	[YearOfCompletion] datetimeoffset null,
	[PercentageMarks] varchar(max) null,
	[CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null,
	FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId)
)
