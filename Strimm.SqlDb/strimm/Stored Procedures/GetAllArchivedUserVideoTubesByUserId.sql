
CREATE PROCEDURE [strimm].[GetAllArchivedUserVideoTubesByUserId]
(
	@UserId int
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
		INNER JOIN strimm.VideoTube v ON vw.VideoTubeId = v.VideoTubeId
		INNER JOIN strimm.UserVideoArchive ua on v.VideoTubeId = ua.VideoTubeId
		INNER JOIN strimm.[User] u on ua.UserId = u.UserId AND u.IsDeleted = 0
	WHERE
		ua.UserId = @UserId
END
