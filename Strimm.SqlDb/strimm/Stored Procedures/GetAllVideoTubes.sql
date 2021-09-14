
CREATE PROCEDURE [strimm].[GetAllVideoTubes]
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
		vt.ProviderVideoId,
		vt.IsPrivate,
		vt.IsRemovedByProvider,
		vt.IsRestrictedByProvider,
		vt.IsRRated,
		vt.CreatedDate
	FROM strimm.VideoTube vt
END
