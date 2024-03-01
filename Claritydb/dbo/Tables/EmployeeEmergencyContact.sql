CREATE TABLE [dbo].[EmployeeEmergencyContact]
(
	[EmployeeEmergencyContactId] bigint not null primary key identity(1,1),
	[EmployeeId] bigint null,
	[Name] varchar(max) null,
	[Relation] varchar(max) null,
	[Phone] varchar(max) null,
	[Email] varchar(max) null,
	[Address] varchar(max) null,
	[CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null,
	FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId)
)
