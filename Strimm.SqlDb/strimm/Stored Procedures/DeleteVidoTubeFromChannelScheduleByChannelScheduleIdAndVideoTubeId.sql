
CREATE PROCEDURE [strimm].[DeleteVidoTubeFromChannelScheduleByChannelScheduleIdAndVideoTubeId]
(
	@ChannelScheduleId int,
	@VideoTubeId int
)
AS
BEGIN
	DELETE FROM strimm.ChannelScheduleVideoTube
	WHERE ChannelScheduleId = @ChannelScheduleId AND
		  VideoTubeId = @VideoTubeId
END
