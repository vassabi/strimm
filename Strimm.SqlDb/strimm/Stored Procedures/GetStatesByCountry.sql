
CREATE PROCEDURE [strimm].[GetStatesByCountry]
(
	@CountryName nchar(64)
)
AS
BEGIN
	SELECT
		s.StateId,
		s.CountryId,
		s.Name,
		s.Code_2,
		s.Code_3
	FROM strimm.State s
		INNER JOIN strimm.Country c ON s.CountryId = c.CountryId
	WHERE
		s.Name = @CountryName
END
