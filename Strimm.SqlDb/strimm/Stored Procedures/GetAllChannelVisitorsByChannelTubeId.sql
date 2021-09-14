
CREATE PROCEDURE [strimm].[GetAllChannelVisitorsByChannelTubeId]
(
	@ChannelTubeId int
)
AS
BEGIN
	SELECT
		v.VisitorId,
		v.ChannelTubeId,
		v.IpAddress,
		v.UserId,
		v.VisitorUserId,
		v.VisitDate,
		v.VisitDuration,
		v.Destination
	FROM strimm.Visitor v
	WHERE v.ChannelTubeId = @ChannelTubeId AND
		  v.Destination = 'Channel'
END
