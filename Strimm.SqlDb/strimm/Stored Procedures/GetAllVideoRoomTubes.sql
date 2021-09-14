
CREATE PROCEDURE strimm.GetAllVideoRoomTubes
AS
BEGIN
	SELECT
		vrt.VideoRoomTubeId,
		vrt.UserId,
		vrt.IsPrivate,
		vrt.CreatedDate
	FROM strimm.VideoRoomTube vrt
END
