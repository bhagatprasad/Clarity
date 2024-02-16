CREATE TABLE [dbo].[State]
(
	[StateId] bigint NOT NULL PRIMARY KEY identity(1,1),
	[CountryId] bigint not null,
	[Name] varchar(max),
	[Description] varchar(max),
	[SateCode] varchar(max),
	[CountryCode] varchar(max),
	[CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null
)
