
CREATE PROCEDURE [strimm].[GetVideoTubesByVideoRoomTubeIdAndCategoryId]
(
	@VideoRoomTubeId int,
	@CategoryId int
)
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
		vt.IsInPublicLibrary,
		vt.ProviderVideoId,
		vt.IsPrivate,
		vt.IsRemovedByProvider,
		vt.IsRestrictedByProvider,
		vt.IsRRated,
		vt.CreatedDate
	FROM strimm.vw_VideoTubes vt
		INNER JOIN strimm.VideoRoomTubeVideoTube vrvt on vt.VideoTubeId = vrvt.VideoTubeId
	WHERE
		vrvt.VideoRoomTubeId = @VideoRoomTubeId AND
		vt.CategoryId = @CategoryId
END
