CREATE TABLE [dbo].[TutionFee]
(
	[Id] bigint NOT NULL PRIMARY KEY identity(1000,1) ,
	[EmployeeId] bigint null,
	[ActualFee] decimal(22,11) null,
	[FinalFee] decimal(22,11) null,
	[RemaingFee] decimal(22,11) null,
	[PaidFee] decimal(22,11) null,
	[CreatedBy] bigint null,
	[CreatedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[IsActive] bit  null
)
