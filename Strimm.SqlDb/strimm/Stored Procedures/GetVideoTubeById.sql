CREATE PROCEDURE  [strimm].[GetVideoTubeById]
(
	@VideoTubeId int
)
AS
BEGIN
	SELECT
		v.VideoTubeId,
		v.VideoProviderId,
		v.Title,
		v.Description,
		v.ProviderVideoId,
		v.Duration,
		v.CategoryId,
		v.CreatedDate,
		v.IsRRated,
		v.IsRemovedByProvider,
		v.IsRestrictedByProvider,
		v.IsInPublicLibrary,
		v.IsPrivate
	FROM strimm.VideoTube v (nolock)
	WHERE v.VideoTubeId = @VideoTubeId
END
