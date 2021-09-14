
CREATE PROCEDURE [strimm].[GetChannelTubesByTitleKeywords]
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
		c.ChannelTubeId,
		c.CategoryId,
		c.Name,
		c.[Description],
		c.IsFeatured,
		c.IsLocked,
		c.IsPrivate,
		c.PictureUrl,
		c.Rating,
		c.Url,
		c.UserId
	FROM strimm.ChannelTube c
		INNER JOIN #keywords k 
			ON c.Name LIKE '''%' + k.keyword + '%''' OR 
			   c.[Description] LIKE '''%' + k.keyword + '%'''
END