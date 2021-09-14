
CREATE PROCEDURE [strimm].[GetAllVisitors]
AS
BEGIN
	SELECT
		c.VisitorId,
		c.ChannelTubeId,
		c.IpAddress,
		c.UserId,
		c.VisitorUserId,
		c.VisitDate,
		c.VisitDuration,
		c.Destination
	FROM strimm.Visitor c
END
