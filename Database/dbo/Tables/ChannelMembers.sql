CREATE TABLE [dbo].[ChannelMembers]
(
	[ChannelId] BIGINT NOT NULL,
	[UserId] BIGINT NOT NULL,
	CONSTRAINT [PK_ChannelMembers] PRIMARY KEY CLUSTERED ([ChannelId] ASC, [UserId] ASC),
	CONSTRAINT [FK_ChannelMembers_Channels] FOREIGN KEY ([ChannelId]) REFERENCES [dbo].[Channels]([ChannelId]),
	CONSTRAINT [FK_ChannelMembers_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([UserId])
)
