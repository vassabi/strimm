
CREATE PROCEDURE [strimm].[GetUserByAccountNumber]
(
	@AccountNumber nvarchar(8)
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
	WHERE
		u.AccountNumber = @AccountNumber
END
