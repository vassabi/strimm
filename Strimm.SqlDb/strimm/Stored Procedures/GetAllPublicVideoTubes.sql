CREATE PROCEDURE  [strimm].[GetAllPublicVideoTubes]
AS
BEGIN
	SELECT
		vt.VideoTubeId,
		vt.VideoProviderId,
		vt.VideoProviderName,
		vt.Title,
		vt.CategoryId,
		vt.CategoryName,
		vt.Description,
		vt.Duration,
		vt.ProviderVideoId,
		vt.IsInPublicLibrary,
		vt.IsPrivate,
		vt.IsRemovedByProvider,
		vt.IsRestrictedByProvider,
		vt.IsRRated,
		vt.CreatedDate
	FROM strimm.vw_VideoTubes vt 
END
