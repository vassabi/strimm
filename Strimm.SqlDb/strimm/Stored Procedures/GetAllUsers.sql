
CREATE PROCEDURE [strimm].[GetAllUsers]
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
END
