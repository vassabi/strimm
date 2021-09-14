
CREATE PROCEDURE [strimm].[GetUserBoardNameByUserId]
(
	@UserId int
)
AS
BEGIN
	SELECT
		up.BoardName
	FROM strimm.[UserProfile] up
	WHERE
		up.UserId = @UserId
END
