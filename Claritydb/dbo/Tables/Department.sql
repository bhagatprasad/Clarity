CREATE TABLE [dbo].[Department]
(
	[DepartmentId] Bigint NOT NULL PRIMARY KEY identity(1000,1),
	[Name]        VARCHAR(MAX)NULL,
    [Description] VARCHAR(MAX)NULL,
    [Code]        VARCHAR(MAX)NULL,
    [CreatedBy]   Bigint   NULL,
    [CreatedOn]   DATETIMEOFFSET (7) NULL,
	[ModifiedBy]  Bigint NULL,
	[ModifiedOn]  DATETIMEOFFSET NULL,
    [IsActive]    BIT                NULL
)
