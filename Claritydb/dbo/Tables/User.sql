CREATE TABLE [dbo].[User]
(
	[Id] bigint NOT NULL PRIMARY KEY identity(1,1),
	[FirstName] varchar(max) null,
	[LastName] varchar(max) null,
	[Email] varchar(max) not null,
	[Phone] varchar(max) not null,
	[RoleId] bigint not null,
	[DepartmentId] bigint not null,
	[PasswordHash] varchar(max),
	[PasswordSalt] varchar(max),
	[PasswordlastChangedOn] datetimeoffset null,
	[PasswordLastChangedBY] bigint null,
	[UserWorngPasswordCount] int null,
	[UserLastWrongPasswordOn] datetimeoffset null,
	[IsBlocked] bit null,
	[CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null,
	FOREIGN KEY (RoleId) REFERENCES Roles(Id),
	FOREIGN KEY (DepartmentId) REFERENCES Department(DepartmentId)
)
