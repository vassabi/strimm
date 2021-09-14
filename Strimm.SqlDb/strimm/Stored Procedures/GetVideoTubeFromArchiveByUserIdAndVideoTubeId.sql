
CREATE PROCEDURE [strimm].[GetVideoTubeFromArchiveByUserIdAndVideoTubeId]
(
	@VideoTubeId int,
	@UserId int
)
AS
BEGIN
	SELECT 
		v.VideoTubeId,
		v.Title,
		v.[Description],
		v.CategoryId,
		v.VideoProviderId,
		v.CreatedDate,
		v.ProviderVideoId,
		v.IsRRated,
		v.IsRemovedByProvider,
		v.IsRestrictedByProvider,
		v.IsInPublicLibrary,
		v.IsPrivate
	FROM strimm.VideoTube v
		INNER JOIN strimm.UserVideoArchive ua on v.VideoTubeId = ua.VideoTubeId
		INNER JOIN strimm.[User] u on ua.UserId = u.UserId AND u.IsDeleted = 0
	WHERE
		ua.VideoTubeId = @VideoTubeId AND
		ua.UserId = @UserId
END
