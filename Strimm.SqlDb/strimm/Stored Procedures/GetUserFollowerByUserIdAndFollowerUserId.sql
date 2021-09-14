
CREATE PROCEDURE  [strimm].[GetUserFollowerByUserIdAndFollowerUserId]
(
	@UserId int,
	@FollowerUserId int
)
AS
BEGIN
	SELECT 
		uf.UserFollowerId,
		uf.UserId,
		uf.FollowerUserId,
		uf.StartedFollowDate,
		uf.StoppedFollowDate
	FROM strimm.[UserFollower] uf (nolock)
	WHERE uf.UserId = @UserId 
			AND uf.FollowerUserId = @FollowerUserId 
END
