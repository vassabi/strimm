
CREATE PROCEDURE [strimm].[InsertChannelSchedule]
(
	@ChannelTubeId int,
	@StartTime datetime
)
AS
BEGIN
	
		DECLARE @CreatedDate datetime;
		SET @CreatedDate = GETDATE();

		INSERT INTO strimm.ChannelSchedule
		(
			ChannelTubeId,
			StartTime,
			IsActive,
			CreatedDate
		)
		VALUES
		(
			@ChannelTubeId,
			@StartTime,
			1,
			@CreatedDate
		)

END
