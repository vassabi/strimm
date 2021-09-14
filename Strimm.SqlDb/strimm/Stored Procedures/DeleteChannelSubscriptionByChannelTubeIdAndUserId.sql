
CREATE PROCEDURE [strimm].[DeleteChannelSubscriptionByChannelTubeIdAndUserId]
(
	@ChannelTubeId int,
	@UserId int
)
AS
BEGIN
	BEGIN TRY
		DELETE 
		FROM strimm.ChannelSubscription
		WHERE
			ChannelTubeId = @ChannelTubeId AND
			UserId = @UserId
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
