
CREATE PROCEDURE [strimm].[DeleteChannelSubscriptionById]
(
	@ChannelSubscriptionId int
)
AS
BEGIN
	BEGIN TRY
		DELETE 
		FROM strimm.ChannelSubscription
		WHERE
			ChannelSubscriptionId = @ChannelSubscriptionId
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
