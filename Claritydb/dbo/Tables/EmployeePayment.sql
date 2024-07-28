CREATE TABLE [dbo].[EmployeePayment]
(
	[Id] bigint NOT NULL PRIMARY KEY identity(1,1) ,
	[EmployeeId] bigint null,
	[TutionFeeId] bigint null,
	[PaymentMethodId] bigint null,
	[PaymentTypeId] bigint null,
	[Amount] decimal(22,11) null,
	[PaymentMessage] nvarchar(max) null,
	[CreatedBy] bigint null,
	[CreatedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[IsActive] bit  null
)
