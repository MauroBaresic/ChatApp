CREATE TABLE [dbo].[ChannelMessages]
(
	[ChannelId] BIGINT NOT NULL,
	[MessageId] BIGINT NOT NULL,
	CONSTRAINT [PK_ChannelMessages] PRIMARY KEY CLUSTERED ([ChannelId] ASC, [MessageId] ASC),
	CONSTRAINT [FK_ChannelMessages_Channels] FOREIGN KEY ([ChannelId]) REFERENCES [dbo].[Channels]([ChannelId]),
	CONSTRAINT [FK_ChannelMessages_Messages] FOREIGN KEY ([MessageId]) REFERENCES [dbo].[Messages]([MessageId])
)
