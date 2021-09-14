
CREATE PROCEDURE [strimm].[GetNumberOfBoardVisitorsByUserId]
(
	@UserId int
)
AS
BEGIN
	SELECT
		count(v.VisitorId)
	FROM strimm.[Visitor] v
	WHERE
		v.UserId = @UserId AND v.Destination='Board'
END
