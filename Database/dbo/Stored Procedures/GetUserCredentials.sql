CREATE PROCEDURE [dbo].[GetUserCredentials]
	@username nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON
	SELECT [dbo].[Users].[UserName], [dbo].[Users].[Password]
	FROM [dbo].[Users]
	WHERE [dbo].[Users].[UserName] = @username
END
