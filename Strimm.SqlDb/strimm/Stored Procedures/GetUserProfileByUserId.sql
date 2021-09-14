
CREATE PROCEDURE [strimm].[GetUserProfileByUserId]
(
	@UserId int
)
AS
BEGIN
	SELECT 
		up.UserProfileId,
		up.UserId,
		up.FirstName,
		up.LastName,
		up.BoardName,
		up.[Address],
		up.City,
		up.StateOrProvince,
		up.ZipCode,
		up.Country,
		up.Gender,
		up.UserStory,
		up.Company,
		up.TermsAndConditionsAcceptanceDate,
		up.ProfileImageUrl,
		up.UserIp,
		up.PhoneNumber,
		up.BoardName,
		up.CreatedDate
	FROM strimm.[UserProfile] up (nolock)
	WHERE up.UserId=@UserId
END

