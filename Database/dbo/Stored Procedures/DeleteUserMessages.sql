CREATE PROCEDURE [dbo].[DeleteUserMessages]
	@username nvarchar(50),
	@usernameOther nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @userId BIGINT;
	DECLARE @otherUserId BIGINT;

	SET @userId = (SELECT TOP 1 [UserId] FROM [dbo].[Users] WHERE [UserName] = @username);
	SET @otherUserId = (SELECT TOP 1 [UserId] FROM [dbo].[Users] WHERE [UserName] = @usernameOther);

	IF @userId is not null AND @otherUserId is not null
	BEGIN
		DELETE FROM [dbo].[UserMessages]
		WHERE [dbo].[UserMessages].[UserId] = @userId AND [dbo].[UserMessages].[OtherUserId] = @otherUserId;

		DELETE FROM [dbo].[Messages]
		WHERE [dbo].[Messages].[MessageId] IN (
		SELECT [dbo].[Messages].[MessageId] 
		FROM [dbo].[UserMessages]
		JOIN [dbo].[Messages] ON [dbo].[UserMessages].[MessageId] = [dbo].[Messages].[MessageId]
		WHERE [dbo].[Messages].[SenderUserId] = @userId AND ([dbo].[UserMessages].[UserId] = @userId AND [dbo].[UserMessages].[OtherUserId] = @otherUserId) 
			OR ([dbo].[UserMessages].[UserId] = @otherUserId AND [dbo].[UserMessages].[OtherUserId] = @userId));
	END
RETURN 0
END
