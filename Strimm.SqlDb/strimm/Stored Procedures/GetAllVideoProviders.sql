
CREATE PROCEDURE strimm.GetAllVideoProviders
AS
BEGIN
	SELECT
		vp.VideoProviderId,
		vp.Name,
		vp.Description,
		vp.IsActive,
		vp.ProdEffectiveDate,
		vp.QaEffectiveDate,
		vp.CreatedDate
	FROM strimm.VideoProvider vp
END
