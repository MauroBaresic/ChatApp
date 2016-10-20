CREATE PROCEDURE [dbo].[UpdateMessage]
	@username NVARCHAR(50),
	@messageId BIGINT,
	@content NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @userId BIGINT;

	SET @userId = (SELECT TOP (1) [UserId] FROM [dbo].[Users] WHERE [UserName] = @username);

	IF @userId is not null
	BEGIN
		UPDATE [dbo].[Messages]
		SET [dbo].[Messages].[Content] = @content
		WHERE [dbo].[Messages].[MessageId] = @messageId AND [dbo].[Messages].[SenderUserId] = @userId
	END
RETURN 0
END
