
CREATE VIEW [dbo].[FriendSuggestionTypedViewEntry]
AS
SELECT dbo.aspnet_Users.UserId, dbo.aspnet_Users.UserName, dbo.UserProfile.FirstName, dbo.UserProfile.LastName, 1 AS ConnectionLevel, 1 AS RecordCount
FROM  dbo.aspnet_Users LEFT OUTER JOIN
               dbo.UserProfile ON dbo.UserProfile.Id = dbo.aspnet_Users.UserId


