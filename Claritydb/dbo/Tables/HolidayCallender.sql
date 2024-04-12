CREATE TABLE [dbo].[HolidayCallender]
(
	[Id] bigint NOT NULL PRIMARY KEY identity(1000,1) ,
	[FestivalName] varchar(max) null,
	[HolidayDate] datetimeoffset null,
	[Year] int null,
	[CreatedBy] bigint null,
	[CreatedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[IsActive] bit  null
)
