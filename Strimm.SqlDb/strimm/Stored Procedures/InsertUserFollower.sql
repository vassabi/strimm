
CREATE PROCEDURE  [strimm].[InsertUserFollower]
(
	@UserId int,
	@FollowerUserId int,
	@StartedFollowDate datetime
)
AS
BEGIN
	INSERT INTO strimm.UserFollower
	(
		UserId,
		FollowerUserId,
		StartedFollowDate
	)
	VALUES
	(
		@UserId,
		@FollowerUserId,
		@StartedFollowDate
	) 
END
