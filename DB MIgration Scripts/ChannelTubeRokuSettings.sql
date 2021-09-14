USE [DB_STubeQA]
GO

/****** Object:  Table [strimm].[ChannelTubeRokuSettings]    Script Date: 4/19/2021 8:11:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [strimm].[ChannelTubeRokuSettings](
	[ChannelTubeId] [int] NOT NULL,
	[AddedToRoku] [bit] NOT NULL,
	[LastUpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ChannelTubeRokuSettings] PRIMARY KEY CLUSTERED 
(
	[ChannelTubeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [strimm].[ChannelTubeRokuSettings] ADD  CONSTRAINT [DF_ChannelTubeRokuSettings_LastUpdateDate]  DEFAULT (getdate()) FOR [LastUpdateDate]
GO


