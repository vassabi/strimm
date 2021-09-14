
CREATE PROCEDURE [strimm].[GetCategoryById]
(
	@CategoryId int
)
AS
BEGIN
	SELECT
		c.CategoryId,
		c.Description,
		c.Name
	FROM strimm.Category c
	WHERE
		c.CategoryId = @CategoryId
END
