
CREATE PROCEDURE [strimm].[InsertChannelSubscription]
(
	@ChannelTubeId int,
	@UserId int,
	@SubscriptionStartDate datetime
)
AS
BEGIN
		INSERT INTO strimm.ChannelSubscription
		(
			ChannelTubeId,
			SubscriptionStartDate,
			UserId
		)
		VALUES
		(
			@ChannelTubeId,
			@SubscriptionStartDate,
			@UserId
		)
END
