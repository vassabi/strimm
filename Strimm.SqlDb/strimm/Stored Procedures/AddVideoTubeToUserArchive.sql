
CREATE PROCEDURE [strimm].[AddVideoTubeToUserArchive]
(
	@VideoTubeId int,
	@UserId int
)
AS
BEGIN
	DECLARE @CreatedDate datetime;
	SET @CreatedDate = GETDATE();

	INSERT INTO strimm.UserVideoArchive
	(
		UserId,
		VideoTubeId,
		CreatedDate
	)
	VALUES
	(
		@UserId,
		@VideoTubeId,
		@CreatedDate
	)
END
