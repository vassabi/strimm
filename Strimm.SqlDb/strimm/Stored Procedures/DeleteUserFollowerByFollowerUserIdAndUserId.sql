CREATE PROCEDURE  [strimm].[DeleteUserFollowerByFollowerUserIdAndUserId]
(
	@FollowerUserId int,
	@UserId int
)
AS
BEGIN
	BEGIN TRY
		DELETE FROM strimm.UserFollower 
		WHERE FollowerUserId = @FollowerUserId AND 
			  UserId = @UserId 
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
