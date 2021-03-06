
CREATE TABLE [strimm].[UserChannelTubeView](
	[UserId] [int] NOT NULL,
	[ChannelTubeId] [int] NOT NULL,
	[ViewStartTime] [datetime] NOT NULL,
	[ViewEndTime] [datetime] NULL
) ON [PRIMARY]

GO
ALTER TABLE [strimm].[UserChannelTubeView]  WITH CHECK ADD  CONSTRAINT [FK_UserChannelTubeView_ChannelTube] FOREIGN KEY([ChannelTubeId])
REFERENCES [strimm].[ChannelTube] ([ChannelTubeId])
GO
ALTER TABLE [strimm].[UserChannelTubeView] CHECK CONSTRAINT [FK_UserChannelTubeView_ChannelTube]
GO
ALTER TABLE [strimm].[UserChannelTubeView]  WITH CHECK ADD  CONSTRAINT [FK_UserChannelTubeView_User] FOREIGN KEY([UserId])
REFERENCES [strimm].[User] ([UserId])
GO
ALTER TABLE [strimm].[UserChannelTubeView] CHECK CONSTRAINT [FK_UserChannelTubeView_User]
GO
