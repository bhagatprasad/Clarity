CREATE TABLE [dbo].[EmployeeDocument]
(
	[Id]                  bigint NOT			NULL PRIMARY KEY identity(1,1),
	[EmployeeId]          bigint NOT			NULL,
	[DocumentTypeId]      bigint NOT			NULL,
	[DocumentName]        varchar(max)			NULL,
	[DocumentPath]        varchar(max)			NULL,
	[DocumentExtension]   varchar(max)			NULL,
	[CreatedBy]           bigint				NULL,
	[CreatedOn]           datetimeoffset		NULL,
	[ModifiedBy]		  bigint				NULL,
	[ModifiedOn]          datetimeoffset		NULL,
	[IsActive]            bit                   NULL
)
