CREATE PROCEDURE  [strimm].[UpdateVideoTube]
(
	@VideoTubeId int,
	@Title nvarchar(255),
	@Description ntext,
	@ProviderVideoId nvarchar(max),
	@Duration float,
	@CategoryId int,
	@VideoProviderId int,
	@IsRRated bit,
	@IsInPublicLibrary bit,
	@IsPrivate bit,
	@IsRestrictedByProvider bit,
	@IsRemovedByProvider bit,
	@CreatedDate datetime
)
AS
BEGIN
	UPDATE strimm.VideoTube
	SET
		Title = @Title,
		[Description] = @Description,
		ProviderVideoId = @ProviderVideoId,
		Duration = @Duration,
		CategoryId = @CategoryId,
		VideoProviderId = @VideoProviderId,
		IsRRated = @IsRRated,
		IsRemovedByProvider = @IsRemovedByProvider,
		IsRestrictedByProvider = @IsRestrictedByProvider,
		IsInPublicLibrary = @IsInPublicLibrary,
		IsPrivate = @IsPrivate,
		CreatedDate = @CreatedDate
	WHERE
		VideoTubeId = @VideoTubeId
END
