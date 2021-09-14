
CREATE PROCEDURE [strimm].[AddVideoTubeToChannelSchedule]
(
	@ChannelScheduleId int,
	@VideoTubeId int,
	@PlaybackOrderNumber int
)
AS
BEGIN
	INSERT INTO strimm.ChannelScheduleVideoTube
	(
		ChannelScheduleId,
		VideoTubeId,
		PlaybackOrderNumber
	)
	VALUES
	(
		@ChannelScheduleId,
		@VideoTubeId,
		@PlaybackOrderNumber
	)
END
