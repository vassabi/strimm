
CREATE PROCEDURE [strimm].[GetChannelTubeById]
(
	@ChannelTubeId int
)
AS
BEGIN

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
	WHERE 
		c.ChannelTubeId = @ChannelTubeId
END