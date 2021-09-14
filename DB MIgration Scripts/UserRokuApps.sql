USE [DB_STubeQA]
GO

/****** Object:  Table [strimm].[UserRokuApps]    Script Date: 4/19/2021 8:10:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [strimm].[UserRokuApps](
	[UserId] [int] NOT NULL,
	[AppName] [nvarchar](50) NOT NULL,
	[AdLink] [nvarchar](500) NULL,
	[PrivacyPolicyLink] [nvarchar](500) NULL,
	[ImageHD] [varbinary](max) NOT NULL,
	[ImageSD] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_UserRokuApps] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


