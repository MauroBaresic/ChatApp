CREATE PROCEDURE [dbo].[GetChannelMessageNotifications]
	@username NVARCHAR(50),
    @lastReceived DATETIME
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @userId BIGINT;
	SET @userId = (SELECT TOP 1 [UserId] FROM [dbo].[Users] WHERE [UserName] = @username);

	IF @userId is not null
	BEGIN
		SELECT DISTINCT ([dbo].[ChannelMessages].[ChannelId])
		FROM [dbo].[Users]
		JOIN [dbo].[ChannelMembers] ON [dbo].[Users].[UserId] = [dbo].[ChannelMembers].UserId
		JOIN [dbo].[ChannelMessages] ON [dbo].[ChannelMessages].[ChannelId] = [dbo].[ChannelMembers].[ChannelId]
		JOIN [dbo].[Messages] ON [dbo].[ChannelMessages].[MessageId] = [dbo].[Messages].[MessageId]
		WHERE [dbo].[Users].[UserId] = @userId AND [dbo].[Messages].[TimeSent] > @lastReceived
	END
END
