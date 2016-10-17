CREATE PROCEDURE [dbo].[StoreChannelMessage]
	@content NVARCHAR(MAX),
	@username NVARCHAR(50),
    @channelId BIGINT, 
    @timeSent DATETIME
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @userId BIGINT;

	SET @userId = (SELECT TOP 1 [UserId] FROM [dbo].[Users] WHERE [UserName] = @username);

	INSERT INTO [dbo].[Messages] ([Content], [SenderUserId], [TimeSent]) VALUES (@content, @userId, @timeSent);
	DECLARE @messageId BIGINT;
	SET @messageId = (SELECT CAST(scope_identity() AS BIGINT));
	INSERT INTO [dbo].[ChannelMessages] ([ChannelId], [MessageId]) VALUES (@channelId, @messageId);
	SELECT @messageId;
RETURN 0
END
