
CREATE PROCEDURE [strimm].[GetAllChannelSubscriptionsByChannelTubeId]
(
	@ChannelTubeId int
)
AS
BEGIN
	
		SELECT
			cs.ChannelSubscriptionId,
			cs.ChannelTubeId,
			cs.SubscriptionEndDate,
			cs.SubscriptionStartDate,
			cs.UserId
		FROM strimm.ChannelSubscription cs
		WHERE cs.ChannelTubeId = @ChannelTubeId

END
