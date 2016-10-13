CREATE PROCEDURE [dbo].[DirectMessages]	
	@username nvarchar(50),
	@usernameOther nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @userId INT;
	DECLARE @otherUserId INT;

	SET @userId = (SELECT TOP 1 [UserId] FROM [dbo].[Users] WHERE [UserName] = @username);
	SET @otherUserId = (SELECT TOP 1 [UserId] FROM [dbo].[Users] WHERE [UserName] = @usernameOther);

	IF @userId is not null AND @otherUserId is not null
	BEGIN
		SELECT [dbo].[Users].[UserName], [dbo].[Messages].[Content], [dbo].[Messages].[TimeSent] 
		FROM [dbo].[UserMessages]
		JOIN [dbo].[Messages] ON [dbo].[UserMessages].[MessageId] = [dbo].[Messages].[MessageId]
		JOIN [dbo].[Users] ON [dbo].[Messages].[SenderUserId] = [dbo].[Users].[UserId]
		WHERE ([dbo].[UserMessages].[UserId] = @userId AND [dbo].[UserMessages].[OtherUserId] = @otherUserId) 
			OR ([dbo].[UserMessages].[UserId] = @otherUserId AND [dbo].[UserMessages].[OtherUserId] = @userId)
	END
END
