
CREATE PROCEDURE strimm.GetAllChannelSchedules
AS
BEGIN
	SELECT
		c.ChannelScheduleId,
		c.ChannelTubeId,
		c.IsActive,
		c.StartTime,
		c.CreatedDate
	FROM strimm.ChannelSchedule c
END
