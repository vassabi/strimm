
CREATE PROCEDURE strimm.GetAllReservedNames
AS
BEGIN
	SELECT
		r.ReservedNameId,
		r.OverrideCodeId,
		r.Name,
		r.Description
	FROM strimm.ReservedName r
END
