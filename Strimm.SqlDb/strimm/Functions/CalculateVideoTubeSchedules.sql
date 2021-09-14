
CREATE FUNCTION [strimm].[CalculateVideoTubeSchedules]()
RETURNS @ttable TABLE
(
	ChannelScheduleId int not null,
	ChannelTubeId int not null,
	VideoTubeId int not null,
	VideoTubeTitle nvarchar(255) not null,
	Duration float not null,
	PlaybackStartTime datetime not null,
	PlaybackEndTime datetime not null,
	PlaybackOrderNumber int not null,
	[Description] nvarchar(max),
	ProviderVideoId nvarchar(max),
	CategoryId int not null,
	CategoryName nvarchar(255) not null,
	VideoProviderId int not null,
	VideoProviderName nvarchar(255) not null,
	IsRRated bit not null,
	IsRemovedByProvider bit not null,
	IsInPublicLibrary bit not null,
	IsPrivate bit not null
)
AS
BEGIN
	DECLARE @ChannelScheduleId int
	DECLARE @ChannelTubeId int
	DECLARE @VideoTubeId int
	DECLARE @VideoTubeTitle nvarchar(255)
	DECLARE @Duration float
	DECLARE @PlaybackStartTime datetime
	DECLARE @PlaybackEndTime datetime
	DECLARE @PlaybackOrderNumber int
	DECLARE @Description nvarchar(max)
	DECLARE @ProviderVideoId nvarchar(max)
	DECLARE @CategoryId int
	DECLARE @CategoryName nvarchar(255)
	DECLARE @VideoProviderId int
	DECLARE @VideoProviderName nvarchar(255)
	DECLARE @IsRRated bit
	DECLARE @IsRemoveByProvider bit
	DECLARE @IsInPublicLibrary bit
	DECLARE @IsPrivate bit
	DECLARE @ActiveScheduleId int
	DECLARE @StartTime datetime

	DECLARE @crs CURSOR

	SET @crs=CURSOR FOR
		SELECT
			csh.ChannelScheduleId,
			csh.ChannelTubeId,
			csh.StartTime,
			vt.VideoTubeId,
			vt.Title,
			vt.Duration,
			vt.Description,
			cshvt.PlaybackOrderNumber,
			cat.CategoryId,
			cat.Name as CategoryName,
			vt.ProviderVideoId,
			vt.VideoProviderId,
			vp.Name as VideoProviderName,
			vt.IsRRated,
			vt.IsRemovedByProvider,
			vt.IsInPublicLibrary,
			vt.IsPrivate
		FROM strimm.ChannelSchedule csh
			INNER JOIN strimm.ChannelTube ct on csh.ChannelTubeId = ct.ChannelTubeId
			INNER JOIN strimm.Category cat on ct.CategoryId = cat.CategoryId
			INNER JOIN strimm.ChannelScheduleVideoTube cshvt on csh.ChannelScheduleId = cshvt.ChannelScheduleId
			INNER JOIN strimm.VideoTube vt on cshvt.VideoTubeId = vt.VideoTubeId
			INNER JOIN strimm.VideoProvider vp on vt.VideoProviderId = vp.VideoProviderId
		ORDER BY
			csh.ChannelScheduleId asc, cshvt.PlaybackOrderNumber

	SET @PlaybackStartTime = NULL
	SET @ActiveScheduleId = -1
		
	OPEN @crs

	FETCH NEXT FROM @crs INTO 
		@ChannelScheduleId, 
		@ChannelTubeId, 
		@StartTime, 
		@VideoTubeId, 
		@VideoTubeTitle, 
		@Duration,
		@Description,
		@PlaybackOrderNumber,
		@CategoryId,
		@CategoryName,
		@ProviderVideoId,
		@VideoProviderId,
		@VideoProviderName,
		@IsRRated,
		@IsRemoveByProvider,
		@IsInPublicLibrary,
		@IsPrivate

	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF @ActiveScheduleId = -1
			BEGIN
				SET @ActiveScheduleId = @ChannelScheduleId
			END
		ELSE IF @ActiveScheduleId <> @ChannelScheduleId
			BEGIN
				SET @ActiveScheduleId = @ChannelScheduleId
				SET @PlaybackStartTime = null
				SET @PlaybackEndTime = null
			END

		IF @PlaybackStartTime is NULL
			BEGIN
				SET @PlaybackStartTime = @StartTime
			END
		ELSE
			BEGIN
				SET @PlaybackStartTime = @PlaybackEndTime;
			END

		SET @PlaybackEndTime = dateadd(mi, @duration, @PlaybackStartTime);

		INSERT INTO @ttable VALUES
		(
			@ChannelScheduleId,
			@ChannelTubeId,
			@VideoTubeId,
			@VideoTubeTitle,
			@Duration,
			@PlaybackStartTime,
			@PlaybackEndTime,
			@PlaybackOrderNumber,
			@Description,
			@ProviderVideoId,
			@CategoryId,
			@CategoryName,
			@VideoProviderId,
			@VideoProviderName,
			@IsRRated,
			@IsRemoveByProvider,
			@IsInPublicLibrary,
			@IsPrivate		
		)
		FETCH NEXT FROM @crs INTO 
			@ChannelScheduleId, 
			@ChannelTubeId, 
			@StartTime, 
			@VideoTubeId, 
			@VideoTubeTitle, 
			@Duration,
			@Description,
			@PlaybackOrderNumber,
			@CategoryId,
			@CategoryName,
			@ProviderVideoId,
			@VideoProviderId,
			@VideoProviderName,
			@IsRRated,
			@IsRemoveByProvider,
			@IsInPublicLibrary,
			@IsPrivate
	END
	CLOSE @crs
	DEALLOCATE @crs

	RETURN
END
