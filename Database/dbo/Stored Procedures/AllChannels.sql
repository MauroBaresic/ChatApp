CREATE PROCEDURE [dbo].[AllChannels]
AS
BEGIN
	SET NOCOUNT ON
	SELECT [ChannelId], [ChannelName] FROM [dbo].[Channels]
END
