
CREATE PROCEDURE [strimm].[InsertChannelTube]
(
	@CategoryId int,
	@Name nvarchar(255),
	@Description nvarchar(max),
	@PictureUrl nvarchar(255),
	@Url nvarchar(255),
	@Rating real,
	@IsFeatured bit = 0,
	@IsPrivate bit = 0,
	@IsLocked bit = 0,
	@UserId int
)
AS
BEGIN
	BEGIN TRY
		INSERT INTO strimm.ChannelTube
		(
			CategoryId,
			Name,
			[Description],
			IsFeatured,
			IsLocked,
			IsPrivate,
			PictureUrl,
			Rating,
			Url,
			UserId
		)
		VALUES
		(
			@CategoryId,
			@Name,
			@Description,
			@IsFeatured,
			@IsLocked,
			@IsPrivate,
			@PictureUrl,
			@Rating,
			@Url,
			@UserId
		)
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END