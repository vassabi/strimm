
CREATE PROCEDURE [strimm].[UpdateChannelTube]
(
	@ChannelTubeId int,
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
		UPDATE strimm.ChannelTube
		SET
			CategoryId = @CategoryId,
			Name = @Name,
			[Description] = @Description,
			IsFeatured = @IsFeatured,
			IsLocked = @IsLocked,
			IsPrivate = @IsPrivate,
			PictureUrl = @PictureUrl,
			Rating = @Rating,
			Url = @Url,
			UserId = @UserId
		WHERE
			ChannelTubeId = @ChannelTubeId
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END