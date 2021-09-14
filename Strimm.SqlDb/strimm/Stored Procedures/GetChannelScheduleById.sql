
CREATE PROCEDURE [strimm].[GetChannelScheduleById]
(
	@ChannelScheduleId int
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
	WHERE c.ChannelScheduleId = @ChannelScheduleId
END
