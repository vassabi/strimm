
CREATE PROCEDURE [strimm].[GetUserByEmailAndIp]
(
	@Email nvarchar(255),
	@Ip nvarchar(45)
)
AS
BEGIN
	SELECT
		u.UserId,
		u.UserName,
		u.ExternalUserId,
		u.LastUpdateDate,
		u.CreatedDate
	FROM strimm.[User] u (nolock)
		INNER JOIN strimm.[UserMembership] um ON u.UserId = um.UserId
		INNER JOIN strimm.[UserProfile] up ON u.UserId = up.UserId
	WHERE
		um.Email = @Email AND up.UserIp = @Ip
END

