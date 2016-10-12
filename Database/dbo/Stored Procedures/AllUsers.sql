CREATE PROCEDURE [dbo].[AllUsers]
AS
BEGIN
	SET NOCOUNT ON
	SELECT [UserName], [FirstName], [LastName] FROM [dbo].[Users]
END