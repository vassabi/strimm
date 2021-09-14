

CREATE VIEW [strimm].[vw_VideoSchedules]
AS
	SELECT 
		ChannelScheduleId,
		ChannelTubeId,
		VideoTubeId,
		VideoTubeTitle,
		Duration,
		PlaybackStartTime,
		PlaybackEndTime,
		PlaybackOrderNumber,
		[Description],
		ProviderVideoId,
		CategoryId,
		CategoryName,
		VideoProviderId,
		VideoProviderName,
		IsRRated,
		IsRemovedByProvider,
		IsInPublicLibrary,
		IsPrivate
	FROM strimm.CalculateVideoTubeSchedules()

