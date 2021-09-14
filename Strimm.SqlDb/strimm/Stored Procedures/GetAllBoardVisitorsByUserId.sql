
CREATE PROCEDURE [strimm].[GetAllBoardVisitorsByUserId]
(
	@UserId int
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
	WHERE v.UserId = @UserId AND
		  v.Destination = 'Board'
END 