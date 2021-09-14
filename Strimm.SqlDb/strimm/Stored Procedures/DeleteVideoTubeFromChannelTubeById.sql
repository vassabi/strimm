
CREATE PROCEDURE [strimm].[DeleteVideoTubeFromChannelTubeById]
(
	@ChannelTubeId int,
	@VideoTubeId int
)
AS
BEGIN
	DELETE FROM strimm.ChannelTubeVideoTube 
	WHERE ChannelTubeId = @ChannelTubeId AND
		  VideoTubeId = @VideoTubeId
END
