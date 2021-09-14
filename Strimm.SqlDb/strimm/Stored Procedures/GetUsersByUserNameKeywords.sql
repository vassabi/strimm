
CREATE PROCEDURE [strimm].[GetUsersByUserNameKeywords]
(
	@Keywords nvarchar(max)
)
AS
BEGIN
	
	DECLARE @delimiter char
	SET @delimiter = ','

	CREATE TABLE #keywords (keyword VARCHAR(100))
	
	INSERT INTO #keywords (keyword) SELECT * FROM strimm.SplitString(@Keywords,@delimiter)

	SELECT
		u.UserId,
		u.UserName,
		u.AccountNumber,
		u.IsDeleted,
		u.LastUpdateDate,
		u.CreatedDate,
		u.ExternalUserId
	FROM strimm.[User] u (nolock)
		INNER JOIN #keywords k 
			ON u.UserName LIKE '''%' + k.keyword + '%'''
END