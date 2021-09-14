CREATE PROCEDURE  [strimm].[InsertUser]
(
	@UserName nvarchar(50),
	@ExternalUserId uniqueidentifier = NULL,
	@AccountNumber nvarchar(8)
)
AS
BEGIN
	DECLARE @CreatedDate datetime;

	SET @CreatedDate = GETDATE()

	INSERT INTO strimm.[User]
	(
		UserName,
		CreatedDate,
		LastUpdateDate,
		ExternalUserId,
		AccountNumber
	)
	VALUES
	(
		@UserName,
		@CreatedDate,
		@CreatedDate,
		@ExternalUserId,
		@AccountNumber
	)
END
