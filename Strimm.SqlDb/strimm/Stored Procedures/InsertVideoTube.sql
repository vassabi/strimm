CREATE PROCEDURE  [strimm].[InsertVideoTube]
(
	@Title nvarchar(255),
	@Description ntext,
	@ProviderVideoId nvarchar(max),
	@Duration float,
	@CategoryId int,
	@VideoProviderId int,
	@IsRRated bit,
	@IsInPublicLibrary bit,
	@IsPrivate bit
)
AS
BEGIN
	DECLARE @CreatedDate datetime;
	SET @CreatedDate = GETDATE()

	INSERT INTO strimm.VideoTube
	(
		Title,
		[Description],
		ProviderVideoId,
		Duration,
		CategoryId,
		VideoProviderId,
		CreatedDate,
		IsRRated,
		IsRemovedByProvider,
		IsRestrictedByProvider,
		IsInPublicLibrary,
		IsPrivate
	)
	VALUES
	(
		@Title,
		@Description,
		@ProviderVideoId,
		@Duration,
		@CategoryId,
		@VideoProviderId,
		@CreatedDate,
		@IsRRated,
		0,
		0,
		@IsInPublicLibrary,
		@IsPrivate
	)
END
