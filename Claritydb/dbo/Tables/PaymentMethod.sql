CREATE TABLE [dbo].[PaymentMethod]
(
	[Id] bigint NOT NULL PRIMARY KEY identity(1000,1) ,
	[Name] varchar(max) null,
	[Code] varchar(max) null,
	[CreatedBy] bigint null,
	[CreatedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[IsActive] bit  null
)
