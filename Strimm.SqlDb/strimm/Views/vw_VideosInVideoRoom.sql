
CREATE VIEW [strimm].[vw_VideosInVideoRoom]
AS
	SELECT 
		vr.VideoRoomTubeId,
		vr.IsPrivate as IsPrivateVideoRoomTube,
		vt.VideoTubeId,
		vt.Title,
		vt.VideoProviderId,
		vp.Name as VideoProviderName,
		vt.ProviderVideoId,
		vt.IsRRated,
		vt.IsRestrictedByProvider,
		vt.IsRemovedByProvider,
		vt.IsPrivate as IsPrivateVideo,
		vt.IsInPublicLibrary,
		vt.CategoryId,
		c.Name as CategoryName,
		c.Description as CategoryDescription,
		vt.Description as VideoDescription,
		vt.Duration,
		vt.CreatedDate as VideoAddedDate,
		vrvt.CreatedDate as VideoAddedToVideoRoomDate
	FROM strimm.VideoRoomTubeVideoTube vrvt
		INNER JOIN strimm.VideoRoomTube vr ON vrvt.VideoRoomTubeId = vr.VideoRoomTubeId
		INNER JOIN strimm.VideoTube		vt ON vrvt.VideoTubeId = vt.VideoTubeId
		INNER JOIN strimm.VideoProvider vp ON vt.VideoProviderId = vp.VideoProviderId
		INNER JOIN strimm.Category		c  ON vt.CategoryId = c.CategoryId

