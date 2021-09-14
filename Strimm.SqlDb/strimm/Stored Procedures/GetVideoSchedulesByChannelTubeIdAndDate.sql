
CREATE PROCEDURE [strimm].[GetVideoSchedulesByChannelTubeIdAndDate]
(
	@ChannelTubeId int,
	@ScheduleDate datetime
)
AS
BEGIN
	SELECT
		vs.ChannelScheduleId,
		vs.ChannelTubeId,
		vs.VideoTubeId,
		vs.VideoTubeTitle,
		vs.Duration,
		vs.PlaybackStartTime,
		vs.PlaybackEndTime,
		vs.PlaybackOrderNumber,
		vs.[Description],
		vs.ProviderVideoId,
		vs.CategoryId,
		vs.CategoryName,
		vs.VideoProviderId,
		vs.VideoProviderName,
		vs.IsRRated,
		vs.IsRemovedByProvider,
		vs.IsInPublicLibrary,
		vs.IsPrivate
	FROM strimm.vw_VideoSchedules vs
		INNER JOIN strimm.ChannelSchedule cs on vs.ChannelScheduleId = cs.ChannelScheduleId
	WHERE
		vs.ChannelTubeId = @ChannelTubeId AND
		cs.StartTime = @ScheduleDate
END
