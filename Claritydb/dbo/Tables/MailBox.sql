CREATE TABLE [dbo].[MailBox]
(
	[MailBoxId]           BIGINT              NOT NULL      PRIMARY KEY  Identity(1,1),
	[MessageTypeId]       BIGINT              NULL,
	[Title]			      NVARCHAR(MAX)       NULL,
	[Subject]             NVARCHAR(MAX)       NULL,
	[Description]         NVARCHAR(MAX)       NULL,
	[Message]             NVARCHAR(MAX)       NULL,
	[HTMLMessage]         NVARCHAR(MAX)       NULL,
	[IsForAll]            BIT                 NULL,
	[FromUser]            varchar(max)        NULL,
	[ToUser]              varchar(max)        NULL,
	[CreatedBy]           BIGINT              NULL,
	[CreatedOn]           DateTimeOffSet      NULL,
	[ModifiedBy]          BIGINT              NULL,
	[ModifiedOn]          DateTimeOffSet      NULL,
	[IsActive]            BIT                 NULL,
	FOREIGN KEY (MessageTypeId) REFERENCES MessageType(Id)
)
