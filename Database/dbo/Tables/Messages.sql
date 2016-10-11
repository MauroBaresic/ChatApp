CREATE TABLE [dbo].[Messages]
(
	[MessageId] BIGINT IDENTITY(1,1) NOT NULL, 
    [Content] NVARCHAR(MAX) NOT NULL,
    [UserId] BIGINT NULL, 
    [TimeSent] DATETIME NOT NULL,
	CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([MessageId] ASC),
	CONSTRAINT [FK_Messages_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([UserId])
)
