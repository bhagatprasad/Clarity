CREATE TABLE [dbo].[UserMailBox]
(
	[UserMailBoxId]	    bigint			NOT NULL PRIMARY KEY identity(1,1),
	[UserId]			bigint			NULL,
	[MailBoxId]			bigint			NULL,
	[IsRead]			bit				NULL,
	[ReadOn]			bigint			NULL,
	[CreatedBy]			bigint			NULL,
    [CreatedOn]			datetimeoffset	NULL,
	[ModifiedBy]		bigint			NULL,
	[ModifiedOn]		datetimeoffset	NULL,
    [IsActive]			bit             NULL,
	FOREIGN KEY (UserId) REFERENCES [User](Id),
	FOREIGN KEY (MailBoxId) REFERENCES MailBox(MailBoxId),
)
