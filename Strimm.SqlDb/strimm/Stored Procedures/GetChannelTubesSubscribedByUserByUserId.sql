
CREATE PROCEDURE [strimm].[GetChannelTubesSubscribedByUserByUserId]
(
	@UserId int
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
		INNER JOIN strimm.ChannelSubscription sc on c.ChannelTubeId = sc.ChannelTubeId and sc.SubscriptionEndDate is NULL
	WHERE 
		sc.UserId = @UserId
END