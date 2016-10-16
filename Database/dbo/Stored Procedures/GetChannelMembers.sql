CREATE PROCEDURE [dbo].[GetChannelMembers]
	@channelId BIGINT
AS
BEGIN
	SET NOCOUNT ON
	SELECT [UserName], [FirstName], [LastName]
	FROM [dbo].[Channels]
	JOIN [dbo].[ChannelMembers] ON [dbo].[ChannelMembers].[ChannelId] = [dbo].[Channels].[ChannelId]
	JOIN [dbo].[Users] ON [dbo].[ChannelMembers].[UserId] = [dbo].[Users].[UserId]
	WHERE [dbo].[Channels].[ChannelId] = @channelId
END
