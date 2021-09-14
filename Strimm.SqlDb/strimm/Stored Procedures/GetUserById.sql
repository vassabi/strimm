
CREATE PROCEDURE [strimm].[GetUserById]
(
	@UserId int
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
	FROM strimm.[User] u
	WHERE u.UserId = @UserId
END
