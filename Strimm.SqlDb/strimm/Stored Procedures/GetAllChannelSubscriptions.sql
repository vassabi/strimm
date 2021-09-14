
CREATE PROCEDURE strimm.GetAllChannelSubscriptions
AS
BEGIN
	SELECT
		c.ChannelSubscriptionId,
		c.ChannelTubeId,
		c.SubscriptionEndDate,
		c.SubscriptionStartDate,
		c.UserId
	FROM strimm.ChannelSubscription c
END
