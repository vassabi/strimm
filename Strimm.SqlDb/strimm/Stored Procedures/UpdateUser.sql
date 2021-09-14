CREATE PROCEDURE  [strimm].[UpdateUser]
(
	@UserId int,
	@UserName nvarchar(50),
	@ExternalUserId uniqueidentifier = NULL,
	@AccountNumber nvarchar(8),
	@IsDeleted bit,
	@CreatedDate datetime
)
AS
BEGIN
	DECLARE @LastUpdatedDate datetime;

	SET @LastUpdatedDate = GETDATE()

	INSERT INTO strimm.[User]
	(
		UserName,
		CreatedDate,
		LastUpdateDate,
		ExternalUserId,
		AccountNumber,
		IsDeleted
	)
	VALUES
	(
		@UserName,
		@CreatedDate,
		@LastUpdatedDate,
		@ExternalUserId,
		@AccountNumber,
		@IsDeleted
	)
END
