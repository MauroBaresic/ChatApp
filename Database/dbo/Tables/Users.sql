﻿CREATE TABLE [dbo].[Users]
(
	[UserId] BIGINT IDENTITY(1,1) NOT NULL, 
    [UserName] NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(40) NOT NULL,
	[FirstName] NVARCHAR(50) CONSTRAINT [DF_Users_FirstName] DEFAULT '' NOT NULL,
	[LastName] NVARCHAR(50) CONSTRAINT [DF_Users_LastName] DEFAULT '' NOT NULL,
    [RegistrationDate] DATETIME NOT NULL,
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC),
	CONSTRAINT [UC_Users_UserName] UNIQUE ([UserName])
)
