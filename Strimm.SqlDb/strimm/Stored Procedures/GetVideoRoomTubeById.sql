CREATE PROCEDURE  [strimm].[GetVideoRoomTubeById]
(
	@VideoRoomTubeId int
)
AS
BEGIN
	SELECT
		vr.VideoRoomTubeId,
		vr.UserId,
		vr.IsPrivate,
		vr.CreatedDate
	FROM strimm.VideoRoomTube vr (nolock)
	WHERE vr.VideoRoomTubeId = @VideoRoomTubeId
END
