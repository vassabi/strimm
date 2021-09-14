CREATE PROCEDURE  [strimm].[GetAllFollowersByUserId]
(
	@UserId int
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
	WHERE uf.UserId = @UserId AND 
		  uf.StoppedFollowDate is NOT NULL
END
