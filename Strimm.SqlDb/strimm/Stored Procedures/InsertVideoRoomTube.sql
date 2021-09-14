CREATE PROCEDURE  [strimm].InsertVideoRoomTube
(
	@UserId int,
	@IsPrivate bit
)
AS
BEGIN
	DECLARE @CreatedDate datetime;

	SET @CreatedDate = GETDATE();

	INSERT INTO strimm.VideoRoomTube
	(
		UserId,
		IsPrivate,
		CreatedDate
	)
	VALUES
	(
		@UserId,
		@IsPrivate,
		@CreatedDate
	)
END
