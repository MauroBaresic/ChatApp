CREATE TABLE [dbo].[Channels]
(
	[ChannelId] BIGINT IDENTITY(1,1) NOT NULL, 
    [ChannelName] NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_Channels] PRIMARY KEY CLUSTERED ([ChannelId] ASC)
)
