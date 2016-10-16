CREATE PROCEDURE [dbo].[UpdateMessage]
	@messageId BIGINT,
	@content NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON
	UPDATE [dbo].[Messages]
	SET [dbo].[Messages].[Content] = @content
	WHERE [dbo].[Messages].[MessageId] = @messageId
RETURN 0
END
