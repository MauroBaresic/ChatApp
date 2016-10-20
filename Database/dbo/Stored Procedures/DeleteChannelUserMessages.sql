CREATE PROCEDURE [dbo].[DeleteChannelUserMessages]
	@channelId BIGINT,
	@username NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @userId BIGINT;

	SET @userId = (SELECT TOP (1) [UserId] FROM [dbo].[Users] WHERE [UserName] = @username);

	IF @userId is not null
	BEGIN
		DELETE FROM [dbo].[ChannelMessages]
		WHERE [dbo].[ChannelMessages].[ChannelId] = @channelId AND [dbo].[ChannelMessages].[MessageId] IN (
			SELECT [dbo].[Messages].[MessageId]
		FROM [dbo].[Channels]
		JOIN [dbo].[ChannelMessages] ON [dbo].[Channels].[ChannelId] = [dbo].[ChannelMessages].[ChannelId]
		JOIN [dbo].[Messages] ON [dbo].[ChannelMessages].[MessageId] = [dbo].[Messages].[MessageId]		
		WHERE [dbo].[Channels].[ChannelId] = @channelId AND [dbo].[Messages].[SenderUserId] = @userId );

		DELETE FROM [dbo].[Messages]
		WHERE [dbo].[Messages].[MessageId] IN (
		SELECT [dbo].[Messages].[MessageId]
		FROM [dbo].[Channels]
		JOIN [dbo].[ChannelMessages] ON [dbo].[Channels].[ChannelId] = [dbo].[ChannelMessages].[ChannelId]
		JOIN [dbo].[Messages] ON [dbo].[ChannelMessages].[MessageId] = [dbo].[Messages].[MessageId]		
		WHERE [dbo].[Channels].[ChannelId] = @channelId AND [dbo].[Messages].[SenderUserId] = @userId );
	END
RETURN 0
END
