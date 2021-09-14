
CREATE PROCEDURE [strimm].[GetUsersByBoardTitleKeywords]
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
		INNER JOIN strimm.UserProfile up ON u.UserId = up.UserId AND u.IsDeleted = 0
		INNER JOIN #keywords k ON up.BoardName LIKE '''%' + k.keyword + '%'''
END