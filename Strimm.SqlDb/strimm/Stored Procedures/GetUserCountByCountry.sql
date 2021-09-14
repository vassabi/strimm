CREATE PROCEDURE  [strimm].[GetUserCountByCountry]
(
	@CountryName nvarchar(250),
	@Count int output
)
AS
BEGIN
	SELECT
		@Count = count(up.UserId)
	FROM strimm.[UserProfile] up (nolock)
	WHERE
		up.Country = @CountryName 
END
