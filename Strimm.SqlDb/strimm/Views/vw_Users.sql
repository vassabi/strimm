




CREATE VIEW [strimm].[vw_Users]
AS
	SELECT     
		u.UserId, 
		u.UserName, 
		u.CreatedDate, 
		u.LastUpdateDate, 
		u.ExternalUserId,
		u.AccountNumber,
		u.IsDeleted,
		um.UserMembershipId, 
		um.Password, 
		um.Email, 
		um.RecoveryEmail,
		um.IsLockedOut, 
		um.ActivationEmailSendDate, 
		um.ActivationEmailRetryCount,
		um.OptOutFromEmailActivation, 
		um.LastLoginDate, 
		um.EmailActivationOptOutDate,
		um.LastPasswordChangeDate, 
		um.FailedPasswordAttemptCount, 
		um.EmailVerified,
		um.IsTempUser, 
		up.UserProfileId, 
		up.FirstName, 
		up.LastName, 
		up.BirthDate,
		up.Address, 
		up.City, 
		up.StateOrProvince, 
		up.Country, 
		up.ZipCode,
		up.Gender, 
		up.UserStory, 
		up.Company, 
		up.TermsAndConditionsAcceptanceDate,
		up.ProfileImageUrl, 
		up.UserIp, 
		up.PhoneNumber,
		um.CreatedDate AS UserMembershipCreatedDate, 
		up.CreatedDate AS UserProfileCreatedDate
	FROM         
		strimm.[User] as u (nolock)
		INNER JOIN strimm.UserMembership as um ON u.UserId = um.UserId 
		INNER JOIN strimm.UserProfile as up ON u.UserId = up.UserId




