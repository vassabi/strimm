
CREATE PROCEDURE [strimm].[GetUserByChannelName]
(
	@ChannelName nvarchar(255)
)
AS
BEGIN
	SELECT
		u.UserId,
		u.UserName,
		u.ExternalUserId,
		u.LastUpdateDate,
		u.CreatedDate,
		u.AccountNumber,
		u.IsDeleted
	FROM strimm.[User] u (nolock)
		INNER JOIN strimm.[ChannelTube] um ON u.UserId = um.UserId
	WHERE
		um.Name = @ChannelName

END

