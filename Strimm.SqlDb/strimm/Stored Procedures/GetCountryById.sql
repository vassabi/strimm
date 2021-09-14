
CREATE PROCEDURE [strimm].[GetCountryById]
(
	@CountryId int
)
AS
BEGIN
	SELECT
		c.CountryId,
		c.Name,
		c.Code_3,
		c.Code_2
	FROM strimm.Country c
	WHERE
		c.CountryId = @CountryId
END
