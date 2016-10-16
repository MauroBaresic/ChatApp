CREATE PROCEDURE [dbo].[DirectMessages]	
	@username nvarchar(50),
	@usernameOther nvarchar(50),
	@topN int = 100
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @userId BIGINT;
	DECLARE @otherUserId BIGINT;

	SET @userId = (SELECT TOP 1 [UserId] FROM [dbo].[Users] WHERE [UserName] = @username);
	SET @otherUserId = (SELECT TOP 1 [UserId] FROM [dbo].[Users] WHERE [UserName] = @usernameOther);

	IF @userId is not null AND @otherUserId is not null
	BEGIN
		SELECT TOP (@topN) ISNULL([dbo].[Users].[UserName], '') as [UserName], [dbo].[Messages].[MessageId], [dbo].[Messages].[Content], [dbo].[Messages].[TimeSent] 
		FROM [dbo].[UserMessages]
		JOIN [dbo].[Messages] ON [dbo].[UserMessages].[MessageId] = [dbo].[Messages].[MessageId]
		LEFT OUTER JOIN [dbo].[Users] ON [dbo].[Messages].[SenderUserId] = [dbo].[Users].[UserId]
		WHERE ([dbo].[UserMessages].[UserId] = @userId AND [dbo].[UserMessages].[OtherUserId] = @otherUserId) 
			OR ([dbo].[UserMessages].[UserId] = @otherUserId AND [dbo].[UserMessages].[OtherUserId] = @userId)
		ORDER BY [dbo].[Messages].[TimeSent] DESC
	END
END
