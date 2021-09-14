
CREATE PROCEDURE strimm.GetAllOverrideCodes
AS
BEGIN
	SELECT
		o.OverrideCodeId,
		o.UserId,
		o.Code,
		o.Comments,
		o.CreatedDate
	FROM strimm.OverrideCode o
END
