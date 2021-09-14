
CREATE PROCEDURE [strimm].[GetChannelSubscriptionByChannelTubeIdAndUserId]
(
	@ChannelTubeId int,
	@UserId int
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
		WHERE cs.ChannelTubeId = @ChannelTubeId AND
		      cs.UserId = @UserId

END
