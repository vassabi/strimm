
CREATE PROCEDURE [strimm].[AddVideoTubeToChannelTube]
(
	@ChannelTubeId int,
	@VideoTubeId int
)
AS
BEGIN
	DECLARE @CreatedDate datetime;
	SET @CreatedDate = GETDATE();

	INSERT INTO strimm.ChannelTubeVideoTube
	(
		ChannelTubeId,
		VideoTubeId,
		CreatedDate
	)
	VALUES
	(
		@ChannelTubeId,
		@VideoTubeId,
		@CreatedDate
	)
END
