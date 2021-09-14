
CREATE PROCEDURE [strimm].[GetVideoScheduleByChannelScheduleIdAndVideoTubeId]
(
	@ChannelScheduleId int,
	@VideoTubeId int
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
	WHERE vs.VideoTubeId = @VideoTubeId AND
		  vs.ChannelScheduleId = @ChannelScheduleId
END
