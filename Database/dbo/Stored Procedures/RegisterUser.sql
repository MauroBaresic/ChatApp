CREATE PROCEDURE [dbo].[RegisterUser]
	@username nvarchar(50),
	@firstname nvarchar(50),
	@lastname nvarchar(50),
	@password nvarchar(50),
	@registrationDate datetime
AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO [dbo].[Users] ([UserName], [Password], [FirstName], [LastName], [RegistrationDate]) VALUES (@username, @password, @firstname, @lastname, @registrationDate)
RETURN 0
END
