CREATE TABLE [dbo].[MessageType]
(
[Id] bigint NOT NULL PRIMARY KEY identity(1,1),
[Name] varchar(max) null,
[CreatedBy] bigint null,
[CreatedOn] datetimeoffset null,
[ModifiedBy] bigint null,
[ModifiedOn] datetimeoffset null,
[IsActive] bit  null
)
