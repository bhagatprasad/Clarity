CREATE TABLE [dbo].[NotificationType]
(
	[NotificationTypeId] BIGINT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] VARCHAR(MAX)NULL,
	[Description] VARCHAR(MAX)NULL,
	[CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null
)
