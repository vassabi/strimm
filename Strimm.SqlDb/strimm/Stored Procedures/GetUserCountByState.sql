CREATE PROCEDURE  [strimm].[GetUserCountByState]
(
	@StateName nvarchar(250),
	@UserCount int OUTPUT
)
AS
BEGIN
	SELECT
		@UserCount = count(up.UserId)
	FROM strimm.[UserProfile] up (nolock)
	WHERE
		up.StateOrProvince = @StateName 
END
