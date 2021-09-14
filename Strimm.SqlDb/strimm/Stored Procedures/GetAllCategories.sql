
CREATE PROCEDURE strimm.GetAllCategories
AS
BEGIN
	SELECT
		c.CategoryId,
		c.Description,
		c.Name
	FROM strimm.Category c
END
