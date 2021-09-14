CREATE PROCEDURE  [strimm].[DeleteUserById]
(
	@UserId int
)
AS
BEGIN
	UPDATE [strimm].[User]
	SET IsDeleted = 1
	WHERE 
		UserId = @UserId 
END
