CREATE PROCEDURE [dbo].[StoreUserMessage]
	@content NVARCHAR(MAX),
	@username NVARCHAR(50),
	@usernameOther NVARCHAR(50),
    @timeSent DATETIME
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @userId BIGINT;
	DECLARE @otherUserId BIGINT;

	SET @userId = (SELECT TOP 1 [UserId] FROM [dbo].[Users] WHERE [UserName] = @username);
	SET @otherUserId = (SELECT TOP 1 [UserId] FROM [dbo].[Users] WHERE [UserName] = @usernameOther);

	IF  @userId is not null AND @otherUserId is not null
	BEGIN
		INSERT INTO [dbo].[Messages] ([Content], [SenderUserId], [TimeSent]) VALUES (@content, @userId, @timeSent);
		DECLARE @messageId BIGINT;
		SET @messageId = (SELECT CAST(scope_identity() AS BIGINT));
		INSERT INTO [dbo].[UserMessages] ([UserId], [OtherUserId], [MessageId]) VALUES (@userId, @otherUserId, @messageId);
		SELECT @messageId AS 'MessageId';
	END
END
