CREATE PROCEDURE [dbo].[StoreMessage]
	@content NVARCHAR(MAX),
    @senderUserId BIGINT, 
    @timeSent DATETIME
AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO [dbo].[Messages] ([Content], [SenderUserId], [TimeSent]) VALUES (@content, @senderUserId, @timeSent)
RETURN 0
END
