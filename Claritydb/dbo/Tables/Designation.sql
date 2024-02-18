CREATE TABLE [dbo].[Designation]
(
	[DesignationId] Bigint NOT NULL PRIMARY KEY default newId(),
	[Name] VARCHAR(MAX)NULL,
	[Code] VARCHAR(MAX)NULL,
	[CreatedBy]   Bigint   NULL,
    [CreatedOn]   DATETIMEOFFSET (7) NULL,
	[ModifiedBy]  Bigint NULL,
	[ModifiedOn]  DATETIMEOFFSET NULL,
    [IsActive]    BIT   NULL
)
