


CREATE VIEW [strimm].[vw_VideoTubes]
AS
	WITH CTE
	AS
	(
		SELECT 
			vt.VideoTubeId,
			COUNT(vs.ChannelScheduleId) as UseCounter,
			COUNT(vw.UserId) as ViewCounter
		FROM strimm.VideoTube vt
			LEFT OUTER JOIN strimm.vw_VideoSchedules vs ON vt.VideoTubeId = vs.VideoTubeId
			LEFT OUTER JOIN strimm.UserVideoTubeView vw ON vt.VideoTubeId = vw.VideoTubeId
		GROUP BY
			vt.VideoTubeId
	)
	SELECT
		vt.VideoTubeId,
		vt.VideoProviderId,
		vp.Name as VideoProviderName,
		vt.Title,
		vt.CategoryId,
		c.Name as CategoryName,
		vt.[Description],
		vt.Duration,
		vt.ProviderVideoId,
		vt.IsInPublicLibrary,
		vt.IsPrivate,
		vt.IsRemovedByProvider,
		vt.IsRestrictedByProvider,
		vt.IsRRated,
		vt.CreatedDate,
		vt.Thumbnail,
		CASE	
			WHEN cte.UseCounter > 0  THEN 1
			ELSE 0
		END as IsScheduled,
		cte.UseCounter,
		cte.ViewCounter
	FROM strimm.VideoTube vt
		INNER JOIN strimm.VideoProvider vp on vt.VideoProviderId = vp.VideoProviderId
		INNER JOIN strimm.Category c on vt.CategoryId = c.CategoryId
		INNER JOIN CTE cte on vt.VideoTubeId = cte.VideoTubeId
GO