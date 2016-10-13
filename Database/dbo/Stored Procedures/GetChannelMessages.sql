CREATE PROCEDURE [dbo].[GetChannelMessages]
	@channelId BIGINT,
	@topN int = 100
AS
BEGIN
	SET NOCOUNT ON
	SELECT TOP (@topN) ISNULL([dbo].[Users].[UserName], '') as [UserName], [dbo].[Messages].[Content], [dbo].[Messages].[TimeSent] 
	FROM [dbo].[ChannelMessages]
	JOIN [dbo].[Messages] ON [dbo].[ChannelMessages].[MessageId] = [dbo].[Messages].[MessageId]
	LEFT OUTER JOIN [dbo].[Users] ON [dbo].[Messages].[SenderUserId] = [dbo].[Users].[UserId]
	WHERE [dbo].[ChannelMessages].[ChannelId] = @channelId
	ORDER BY [dbo].[Messages].[TimeSent] DESC
END
