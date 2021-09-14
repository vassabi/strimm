
CREATE PROCEDURE [strimm].[GetAllVideoTubeByChannelTubeId]
(
	@ChannelTubeId int
)
AS
BEGIN
	SELECT 
		vw.VideoTubeId,
		vw.Title,
		vw.[Description],
		vw.CategoryId,
		vw.CategoryName,
		vw.VideoProviderId,
		vw.VideoProviderName,
		vw.CreatedDate,
		vw.ProviderVideoId,
		vw.IsRRated,
		vw.IsRemovedByProvider,
		vw.IsRestrictedByProvider,
		vw.IsInPublicLibrary,
		vw.IsPrivate
	FROM strimm.vw_VideoTubes vw
		INNER JOIN strimm.ChannelTubeVideoTube cv on vw.VideoTubeId = cv.VideoTubeId
		INNER JOIN strimm.ChannelTube ct on cv.ChannelTubeId = ct.ChannelTubeId
		INNER JOIN strimm.[User] u on ct.UserId = u.UserId AND u.IsDeleted = 0
	WHERE
		ct.ChannelTubeId = @ChannelTubeId
END
