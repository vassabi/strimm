
CREATE TABLE [strimm].[UserVideoTubeView](
	[UserId] [int] NOT NULL,
	[VideoTubeId] [int] NOT NULL,
	[ViewStartTime] [datetime] NOT NULL,
	[ViewEndTime] [datetime] NULL
) ON [PRIMARY]

GO
ALTER TABLE [strimm].[UserVideoTubeView]  WITH CHECK ADD  CONSTRAINT [FK_UserVideoTubeView_User] FOREIGN KEY([UserId])
REFERENCES [strimm].[User] ([UserId])
GO
ALTER TABLE [strimm].[UserVideoTubeView] CHECK CONSTRAINT [FK_UserVideoTubeView_User]
GO
ALTER TABLE [strimm].[UserVideoTubeView]  WITH CHECK ADD  CONSTRAINT [FK_UserVideoTubeView_VideoTube] FOREIGN KEY([VideoTubeId])
REFERENCES [strimm].[VideoTube] ([VideoTubeId])
GO
ALTER TABLE [strimm].[UserVideoTubeView] CHECK CONSTRAINT [FK_UserVideoTubeView_VideoTube]
GO
