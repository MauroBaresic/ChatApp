CREATE TABLE [dbo].[UserMessages]
(	
	[UserId] BIGINT NOT NULL,
	[OtherUserId] BIGINT NOT NULL,
	[MessageId] BIGINT NOT NULL,
	CONSTRAINT [PK_UserMessages] PRIMARY KEY CLUSTERED ([UserId] ASC, [OtherUserId] ASC, [MessageId] ASC),
	CONSTRAINT [FK_UserMessages_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([UserId]),
	CONSTRAINT [FK_UserMessages_Users_Other] FOREIGN KEY ([OtherUserId]) REFERENCES [dbo].[Users]([UserId]),
	CONSTRAINT [FK_UserMessages_Messages] FOREIGN KEY ([MessageId]) REFERENCES [dbo].[Messages]([MessageId])
)
