USE [DB_STubeQA]
GO
/****** Object:  StoredProcedure [strimm].[DeleteVideoTubeFromChannelTubeById]    Script Date: 8/27/2021 5:25:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [strimm].[DeleteVideoTubeFromChannelTubeById]
(
	@ChannelTubeId int,
	@VideoTubeId int
)
AS
BEGIN
	DELETE FROM strimm.ChannelTubeVideoTube 
	WHERE ChannelTubeId = @ChannelTubeId AND
		  VideoTubeId = @VideoTubeId;
	

	DELETE FROM strimm.ChannelScheduleVideoTube
	WHERE VideoTubeId = @VideoTubeId;
END

