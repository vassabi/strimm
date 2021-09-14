
CREATE PROCEDURE [strimm].[GetVideoSchedulesByUserId]
(
	@UserId int
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
		INNER JOIN strimm.ChannelTube ct on vs.ChannelTubeId = ct.ChannelTubeId
		INNER JOIN strimm.[User] u on ct.UserId = u.UserId AND u.IsDeleted = 0
	WHERE
		u.UserId = @UserId
END
