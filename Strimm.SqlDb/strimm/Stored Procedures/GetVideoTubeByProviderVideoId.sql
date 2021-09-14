CREATE PROCEDURE  [strimm].[GetVideoTubeByProviderVideoId]
(
	@ProviderVideoId nvarchar(max)
)
AS
BEGIN
	SELECT
		vt.VideoTubeId,
		vt.VideoProviderId,
		vt.Title,
		vt.CategoryId,
		vt.Description,
		vt.Duration,
		vt.IsInPublicLibrary,
		vt.IsPrivate,
		vt.ProviderVideoId,
		vt.IsRemovedByProvider,
		vt.IsRestrictedByProvider,
		vt.IsRRated,
		vt.CreatedDate
	FROM strimm.VideoTube vt 
	WHERE
		ProviderVideoId = @ProviderVideoId
END
