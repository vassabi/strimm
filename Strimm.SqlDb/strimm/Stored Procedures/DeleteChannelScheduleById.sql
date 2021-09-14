
CREATE PROCEDURE [strimm].[DeleteChannelScheduleById]
(
	@ChannelScheduleId int
)
AS
BEGIN
	
	BEGIN TRAN
	BEGIN TRY

		DELETE FROM strimm.ChannelScheduleVideoTube
		WHERE ChannelScheduleId = @ChannelScheduleId

		DELETE FROM strimm.ChannelSchedule
		WHERE ChannelScheduleId = @ChannelScheduleId

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		BEGIN
			ROLLBACK TRAN;
		END;
		THROW
	END CATCH
END
