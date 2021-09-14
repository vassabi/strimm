CREATE PROCEDURE  [strimm].[GetVideoTubesByTitleKeywords]
(
	@Keywords nvarchar(max)
)
AS
BEGIN

	DECLARE @delimiter char
	SET @delimiter = ','

	CREATE TABLE #keywords (keyword VARCHAR(100))
	
	INSERT INTO #keywords (keyword) SELECT * FROM strimm.SplitString(@Keywords,@delimiter)

	SELECT
		vt.VideoTubeId,
		vt.VideoProviderId,
		vt.VideoProviderName,
		vt.Title,
		vt.CategoryId,
		vt.CategoryName,
		vt.[Description],
		vt.Duration,
		vt.ProviderVideoId,
		vt.IsInPublicLibrary,
		vt.IsPrivate,
		vt.IsRemovedByProvider,
		vt.IsRestrictedByProvider,
		vt.IsRRated,
		vt.CreatedDate
	FROM strimm.vw_VideoTubes vt 
		INNER JOIN #keywords k 
			ON vt.Title LIKE '''%' + k.keyword + '%'''
END
