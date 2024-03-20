CREATE TABLE [dbo].[RepotingManager]
(
	[RepotingManagerId] BIGINT NOT NULL PRIMARY KEY Identity(1,1),
	[EmployeeId] BIGINT Not NULL unique,
	[ManagerId] BIGINT  Null,
	[CreatedBy]   Bigint   NULL,
    [CreatedOn]   DATETIMEOFFSET (7) NULL,
	[ModifiedBy]  Bigint NULL,
	[ModifiedOn]  DATETIMEOFFSET NULL,
    [IsActive]    BIT                NULL,
	Foreign Key (EmployeeId) References Employee(EmployeeId),
	Foreign Key (ManagerId) References Employee(EmployeeId),

)
