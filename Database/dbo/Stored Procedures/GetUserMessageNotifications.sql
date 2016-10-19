CREATE PROCEDURE [dbo].[GetUserMessageNotifications]
	@username NVARCHAR(50),
    @lastReceived DATETIME
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @userId BIGINT;
	SET @userId = (SELECT TOP 1 [UserId] FROM [dbo].[Users] WHERE [UserName] = @username);

	IF @userId is not null
	BEGIN
		SELECT DISTINCT ([dbo].[Users].[UserName]) 
		FROM [dbo].[UserMessages]
		JOIN [dbo].[Messages] ON [dbo].[UserMessages].[MessageId] = [dbo].[Messages].[MessageId]
		JOIN [dbo].[Users] ON [dbo].[UserMessages].[UserId] = [dbo].[Users].[UserId]
		WHERE [dbo].[UserMessages].[OtherUserId] = @userId AND [dbo].[Messages].[TimeSent] > @lastReceived
	END
END
