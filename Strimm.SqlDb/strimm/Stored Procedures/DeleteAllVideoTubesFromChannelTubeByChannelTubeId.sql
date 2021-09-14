
CREATE PROCEDURE [strimm].[DeleteAllVideoTubesFromChannelTubeByChannelTubeId]
(
	@ChannelTubeId int
)
AS
BEGIN
	DELETE FROM strimm.ChannelTubeVideoTube 
	WHERE ChannelTubeId = @ChannelTubeId
END
