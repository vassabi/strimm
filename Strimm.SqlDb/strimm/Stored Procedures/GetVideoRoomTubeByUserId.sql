CREATE PROCEDURE  [strimm].GetVideoRoomTubeByUserId
(
	@UserId int
)
AS
BEGIN
	SELECT
		vr.VideoRoomTubeId,
		vr.UserId,
		vr.IsPrivate,
		vr.CreatedDate
	FROM strimm.VideoRoomTube vr (nolock)
		INNER JOIN strimm.[User] u on vr.UserId = u.UserId and u.IsDeleted = 0
	WHERE vr.UserId = @UserId
END
