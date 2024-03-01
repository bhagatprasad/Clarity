CREATE TABLE [dbo].[TaskCode]
(
	[TaskCodeId] bigint NOT NULL PRIMARY KEY identity(1,1) ,
	[Name] varchar(max) null,
	[Code] varchar(max) null,
	[TaskItemId]  bigint null,
	[CreatedBy] bigint null,
	[CreatedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[IsActive] bit  null,
	FOREIGN KEY (TaskItemId) REFERENCES TaskItem(TaskItemId),
)
