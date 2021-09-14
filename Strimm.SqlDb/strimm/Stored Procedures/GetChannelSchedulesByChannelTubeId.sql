
CREATE PROCEDURE [strimm].[GetChannelSchedulesByChannelTubeId]
(
	@ChannelTubeId int
)
AS
BEGIN
	SELECT
		c.ChannelScheduleId,
		c.ChannelTubeId,
		c.IsActive,
		c.StartTime,
		c.CreatedDate
	FROM strimm.ChannelSchedule c with (nolock)
	WHERE c.ChannelTubeId = @ChannelTubeId
END
