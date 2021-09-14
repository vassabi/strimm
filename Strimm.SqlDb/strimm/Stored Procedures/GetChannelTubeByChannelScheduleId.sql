
CREATE PROCEDURE [strimm].[GetChannelTubeByChannelScheduleId]
(
	@ChannelScheduleId int
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
		INNER JOIN strimm.ChannelSchedule sc on c.ChannelTubeId = sc.ChannelTubeId
	WHERE 
		sc.ChannelScheduleId = @ChannelScheduleId
END