CREATE TABLE [dbo].[City]
(
	Id bigint not null primary key identity(1,1),
Name varchar(max) null,
Code varchar(max) null,
ContryId bigint null ,
StateId bigint null,
[CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null
)
