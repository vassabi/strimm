
CREATE PROCEDURE [strimm].[GetUserByEmail]
(
	@Email nvarchar(250)
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
		INNER JOIN strimm.[UserMembership] um ON u.UserId = um.UserId
	WHERE
		um.Email = @Email
END
