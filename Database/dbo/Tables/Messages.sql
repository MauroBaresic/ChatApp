CREATE TABLE [dbo].[Messages]
(
	[MessageId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Content] NVARCHAR(MAX) NOT NULL, 
    [ChannelId] INT NULL, 
    [UserId] INT NULL, 
    [TimeSent] DATETIME NOT NULL
)
