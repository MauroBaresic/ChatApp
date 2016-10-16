CREATE PROCEDURE [dbo].[DeleteMessage]
	@username NVARCHAR(50),
	@messageId BIGINT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @userId BIGINT;

	SET @userId = (SELECT TOP (1) [UserId] FROM [dbo].[Users] WHERE [UserName] = @username);

	IF @userId is not null
	BEGIN
		DELETE FROM [dbo].[Messages]
		WHERE [dbo].[Messages].[MessageId] = (
		SELECT TOP(1) [dbo].[Messages].[MessageId]
		FROM [dbo].[Messages]
		WHERE [dbo].[Messages].[MessageId] = @messageId AND [dbo].[Messages].[SenderUserId] = @userId )
	END
RETURN 0
END
