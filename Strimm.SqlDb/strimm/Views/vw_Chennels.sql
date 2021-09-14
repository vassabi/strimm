

CREATE VIEW [strimm].[vw_Chennels]
AS
	SELECT 
		vt.VideoTubeId,
		vt.Title,
		vt.VideoProviderId,
		vp.Name as VideoProviderName,
		vt.ProviderVideoId,
		vt.IsRRated,
		vt.IsRestrictedByProvider,
		vt.IsRemovedByProvider,
		vt.IsPrivate,
		vt.IsInPublicLibrary,
		vt.CategoryId,
		c.Name as CategoryName,
		vt.Description,
		vt.Duration,
		vt.CreatedDate
	FROM strimm.ChannelTubeVideoTube cv
		INNER JOIN strimm.ChannelTube	ct ON cv.ChannelTubeId = ct.ChannelTubeId
		INNER JOIN strimm.VideoTube		vt ON cv.VideoTubeId = vt.VideoTubeId
		INNER JOIN strimm.VideoProvider vp ON vt.VideoProviderId = vp.VideoProviderId
		INNER JOIN strimm.Category		c  ON vt.CategoryId = c.CategoryId

