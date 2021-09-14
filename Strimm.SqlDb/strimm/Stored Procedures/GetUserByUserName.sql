CREATE PROCEDURE [strimm].[GetUserByUserName]
(
	@UserName nvarchar(50)
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
	WHERE u.UserName=@UserName
END

