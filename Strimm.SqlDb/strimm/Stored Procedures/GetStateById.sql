
CREATE PROCEDURE [strimm].[GetStateById]
(
	@StateId int
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
	WHERE
		s.StateId = @StateId
END
