
CREATE PROCEDURE [strimm].[GetAllFeaturedChannelTubes]
AS
BEGIN
	SELECT
		c.ChannelTubeId,
		c.CategoryId,
		c.Name,
		c.Description,
		c.IsFeatured,
		c.IsLocked,
		c.IsPrivate,
		c.PictureUrl,
		c.Rating,
		c.Url,
		c.UserId
	FROM strimm.ChannelTube c
	WHERE
		c.IsFeatured = 1
END
