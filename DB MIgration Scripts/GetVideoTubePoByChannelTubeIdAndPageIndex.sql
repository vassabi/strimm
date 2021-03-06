USE [DB_STubeQA]
GO
/****** Object:  StoredProcedure [strimm].[GetVideoTubePoByChannelTubeIdAndPageIndex]    Script Date: 9/21/2021 6:01:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE  [strimm].[GetVideoTubePoByChannelTubeIdAndPageIndex]
(
	@ChannelTubeId INT,
	@PageIndex INT = 1,
	@PageSize INT = 10,
	@Keywords NVARCHAR(MAX) = NULL,
	@PageCount INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @delimiter CHAR;	
	SET @delimiter = ','

	DECLARE @QueryFTX NVARCHAR(2000)
	SELECT @QueryFTX =  [strimm].[GET_FTX_QUERY] (@Keywords,@delimiter)

	--CREATE TABLE #keywords (keyword VARCHAR(100))		
	CREATE TABLE #results 
	(
		RowNumber INT,
		VideoTubeId INT,
		VideoProviderId INT,
		VideoProviderName NVARCHAR(50),
		Title NVARCHAR(255),
		CategoryId INT,
		CategoryName NVARCHAR(50),
		Description NTEXT,
		Duration FLOAT,
		ProviderVideoId NVARCHAR(255),
		IsInPublicLibrary BIT,
		IsPrivate BIT,
		IsRemovedByProvider BIT,
		IsRestrictedByProvider BIT,
		IsRRated BIT,
		CreatedDate DATETIME,
		LiveStartTime DATETIME,
		LiveEndTime DATETIME,
		IsLive bit,
		Thumbnail NVARCHAR(255),
		IsScheduled BIT,
		UseCounter INT NULL,
		ViewCounter INT NULL,
		VideoPreviewKey NVARCHAR(255) NULL,
		Keywords NVARCHAR(MAX) NULL,
		VideoKey NVARCHAR(MAX) NULL,
		VideoStatus VARCHAR(10) NULL,
		VideoStatusMessage NVARCHAR(255) NULL,
		OwnerUserId INT NULL,
		OwnerUserName NVARCHAR(50) NULL,
		DateAdded DATETIME NULL,
		OwnerPublicUrl NVARCHAR(50) NULL,
		PrivateVideoModeEnabled bit	
	)

	--IF @keywords <> '' AND @keywords IS NOT NULL 
	--BEGIN 
	--	INSERT INTO #keywords (keyword) SELECT * FROM strimm.SplitString(@keywords,@delimiter)
	--END

	IF @keywords <> '' AND @keywords IS NOT NULL
	BEGIN 
		INSERT INTO #Results (
			RowNumber,
			VideoTubeId,
			VideoProviderId,
			VideoProviderName,
			Title,
			CategoryId,
			CategoryName,
			Description,
			Duration,
			ProviderVideoId,
			IsInPublicLibrary,
			IsPrivate,
			IsRemovedByProvider,
			IsRestrictedByProvider,
			IsRRated,
			CreatedDate,
			LiveStartTime,
			LiveEndTime,
			IsLive,
			Thumbnail,
			IsScheduled,
			UseCounter,
			ViewCounter,
			VideoPreviewKey,
			Keywords,
			VideoKey,
			VideoStatus,
			VideoStatusMessage,
			OwnerUserId,
			OwnerUserName,
			DateAdded,
			OwnerPublicUrl,
			PrivateVideoModeEnabled	
		) 
		SELECT ROW_NUMBER() OVER
			(
				ORDER BY ctvt.CreatedDate DESC
			)
			AS RowNumber,
			vt.VideoTubeId,
			vt.VideoProviderId,
			vt.VideoProviderName,
			vt.Title,
			vt.CategoryId,
			vt.CategoryName,
			vt.Description,
			vt.Duration,
			vt.ProviderVideoId,
			vt.IsInPublicLibrary,
			vt.IsPrivate,
			vt.IsRemovedByProvider,
			vt.IsRestrictedByProvider,
			vt.IsRRated,
			vt.CreatedDate,
			null,
			null,
			0,
			vt.Thumbnail,
			vt.IsScheduled,
			vt.UseCounter,
			vt.ViewCounter,
			vt.PreviewClipKey,
			vt.Keywords,
			vt.VideoKey,
			vt.VideoStatus,
			vt.VideoStatusMessage,
			vt.OwnerUserId,
			vt.OwnerUserName,
			ctvt.CreatedDate AS DateAdded,
			u.PublicUrl as OwnerPublicUrl,
			vt.PrivateVideoModeEnabled
		FROM strimm.vw_VideoTubes vt with (nolock)
			INNER JOIN strimm.ChannelTubeVideoTube ctvt ON vt.VideoTubeId = ctvt.VideoTubeId
			INNER JOIN strimm.ChannelTube ct ON ctvt.ChannelTubeId = ct.ChannelTubeId AND ct.IsDeleted = 0
			INNER JOIN strimm.[User] u ON ct.UserId = u.UserId AND u.IsDeleted = 0
			INNER JOIN containstable(strimm.VideoTube  , Title, @QueryFTX) k on vt.VideoTubeID = k.[Key]
			--INNER JOIN #keywords kn ON  vt.Title LIKE '%' + kn.keyword + '%'--OR vt.Description LIKE '%' + kn.keyword + '%' 
		WHERE 
			ctvt.ChannelTubeId = @ChannelTubeId	
		ORDER BY 
			ctvt.CreatedDate DESC
			
		-- Add live videos
		INSERT INTO #Results (
			RowNumber,
			VideoTubeId,
			VideoProviderId,
			VideoProviderName,
			Title,
			CategoryId,
			CategoryName,
			Description,
			Duration,
			ProviderVideoId,
			IsInPublicLibrary,
			IsPrivate,
			IsRemovedByProvider,
			IsRestrictedByProvider,
			IsRRated,
			CreatedDate,
			LiveStartTime,
			LiveEndTime,
			IsLive,
			Thumbnail,
			IsScheduled,
			UseCounter,
			ViewCounter,
			VideoPreviewKey,
			Keywords,
			VideoKey,
			VideoStatus,
			VideoStatusMessage,
			OwnerUserId,
			OwnerUserName,
			DateAdded,
			OwnerPublicUrl,
			PrivateVideoModeEnabled
		) 
		SELECT ROW_NUMBER() OVER
			(
				ORDER BY ctvt.AddedDate DESC
			)
			AS RowNumber,
			vt.VideoLiveTubeId as VideoTubeId,
			vt.VideoProviderId,
			vt.VideoProviderName,
			vt.Title,
			vt.CategoryId,
			vt.CategoryName,
			vt.Description,
			0.0,
			vt.ProviderVideoId,
			0,
			vt.IsPrivate,
			vt.IsRemovedByProvider,
			vt.IsRestrictedByProvider,
			vt.IsRRated,
			vt.CreatedDate,
			vt.StartTime as LiveStartTime,
			vt.EndTime as LiveEndTime,
			1,
			vt.Thumbnail,
			0,
			0,
			vt.ViewCounter,
			null,
			null,
			null,
			null,
			null,
			vt.OwnerUserId,
			vt.OwnerUserName,
			ctvt.AddedDate AS DateAdded,
			u.PublicUrl	as OwnerPublicUrl,
			0
		FROM strimm.vw_VideoLiveTubes vt with (nolock)
			INNER JOIN strimm.ChannelTubeVideoLiveTube ctvt ON vt.VideoLiveTubeId = ctvt.VideoLiveTubeId
			INNER JOIN strimm.ChannelTube ct ON ctvt.ChannelTubeId = ct.ChannelTubeId AND ct.IsDeleted = 0
			INNER JOIN strimm.[User] u ON ct.UserId = u.UserId AND u.IsDeleted = 0
			--INNER JOIN containstable(strimm.VideoTube  , Title, @QueryFTX) k on vt.VideoTubeID = k.[Key]
			--INNER JOIN #keywords kn ON  vt.Title LIKE '%' + kn.keyword + '%'--OR vt.Description LIKE '%' + kn.keyword + '%' 
		WHERE 
			ctvt.ChannelTubeId = @ChannelTubeId	
		ORDER BY 
			ctvt.AddedDate DESC
			
		END
	ELSE
	BEGIN 	
		INSERT INTO #Results (
			RowNumber,
			VideoTubeId,
			VideoProviderId,
			VideoProviderName,
			Title,
			CategoryId,
			CategoryName,
			Description,
			Duration,
			ProviderVideoId,
			IsInPublicLibrary,
			IsPrivate,
			IsRemovedByProvider,
			IsRestrictedByProvider,
			IsRRated,
			CreatedDate,
			LiveStartTime,
			LiveEndTime,
			IsLive,
			Thumbnail,
			IsScheduled,
			UseCounter,
			ViewCounter,
			VideoPreviewKey,
			Keywords,
			VideoKey,
			VideoStatus,
			VideoStatusMessage,
			OwnerUserId,
			OwnerUserName,
			DateAdded,
			OwnerPublicUrl,
			PrivateVideoModeEnabled	
		) 
		SELECT ROW_NUMBER() OVER
			(
				ORDER BY ctvt.CreatedDate DESC
			)
			AS RowNumber,
			vt.VideoTubeId,
			vt.VideoProviderId,
			vt.VideoProviderName,
			vt.Title,
			vt.CategoryId,
			vt.CategoryName,
			vt.Description,
			vt.Duration,
			vt.ProviderVideoId,
			vt.IsInPublicLibrary,
			vt.IsPrivate,
			vt.IsRemovedByProvider,
			vt.IsRestrictedByProvider,
			vt.IsRRated,
			vt.CreatedDate,
			null,
			null,
			0,
			vt.Thumbnail,
			vt.IsScheduled,
			vt.UseCounter,
			vt.ViewCounter,
			vt.PreviewClipKey,
			vt.Keywords,
			vt.VideoKey,
			vt.VideoStatus,
			vt.VideoStatusMessage,
			vt.OwnerUserId,
			vt.OwnerUserName,
			ctvt.CreatedDate AS DateAdded,
			u.PublicUrl,
			vt.PrivateVideoModeEnabled		
		FROM strimm.vw_VideoTubes vt with (nolock)
			INNER JOIN strimm.ChannelTubeVideoTube ctvt ON vt.VideoTubeId = ctvt.VideoTubeId
			INNER JOIN strimm.ChannelTube ct ON ctvt.ChannelTubeId = ct.ChannelTubeId AND ct.IsDeleted = 0
			INNER JOIN strimm.[User] u ON ct.UserId = u.UserId AND u.IsDeleted = 0
		WHERE 
			ctvt.ChannelTubeId = @ChannelTubeId
		ORDER BY 
			ctvt.CreatedDate DESC
			
		-- Add live videos
		INSERT INTO #Results (
			RowNumber,
			VideoTubeId,
			VideoProviderId,
			VideoProviderName,
			Title,
			CategoryId,
			CategoryName,
			Description,
			Duration,
			ProviderVideoId,
			IsInPublicLibrary,
			IsPrivate,
			IsRemovedByProvider,
			IsRestrictedByProvider,
			IsRRated,
			CreatedDate,
			LiveStartTime,
			LiveEndTime,
			IsLive,
			Thumbnail,
			IsScheduled,
			UseCounter,
			ViewCounter,
			VideoPreviewKey,
			Keywords,
			VideoKey,
			VideoStatus,
			VideoStatusMessage,
			OwnerUserId,
			OwnerUserName,
			DateAdded,
			OwnerPublicUrl,
			PrivateVideoModeEnabled	
		) 
		SELECT ROW_NUMBER() OVER
			(
				ORDER BY ctvt.AddedDate DESC
			)
			AS RowNumber,
			vt.VideoLiveTubeId as VideoTubeId,
			vt.VideoProviderId,
			vt.VideoProviderName,
			vt.Title,
			vt.CategoryId,
			vt.CategoryName,
			vt.Description,
			0.0,
			vt.ProviderVideoId,
			0,
			vt.IsPrivate,
			vt.IsRemovedByProvider,
			vt.IsRestrictedByProvider,
			vt.IsRRated,
			vt.CreatedDate,
			vt.StartTime as LiveStartTime,
			vt.EndTime as LiveEndTime,
			1,
			vt.Thumbnail,
			0,
			0,
			vt.ViewCounter,
			null,
			null,
			null,
			null,
			null,
			vt.OwnerUserId,
			vt.OwnerUserName,
			ctvt.AddedDate AS DateAdded,
			u.PublicUrl	as OwnerPublicUrl,
			0
		FROM strimm.vw_VideoLiveTubes vt with (nolock)
			INNER JOIN strimm.ChannelTubeVideoLiveTube ctvt ON vt.VideoLiveTubeId = ctvt.VideoLiveTubeId
			INNER JOIN strimm.ChannelTube ct ON ctvt.ChannelTubeId = ct.ChannelTubeId AND ct.IsDeleted = 0
			INNER JOIN strimm.[User] u ON ct.UserId = u.UserId AND u.IsDeleted = 0
		WHERE 
			ctvt.ChannelTubeId = @ChannelTubeId	
		ORDER BY 
			ctvt.AddedDate DESC
			
	END
     
    DECLARE @RecordCount INT
    SELECT @RecordCount = COUNT(*) FROM #Results
 
    SET @PageCount = CEILING(CAST(@RecordCount AS DECIMAL(10, 2)) / CAST(@PageSize AS DECIMAL(10, 2)))
           
    SELECT * FROM #Results
    WHERE RowNumber BETWEEN(@PageIndex -1) * @PageSize + 1 AND(((@PageIndex -1) * @PageSize + 1) + @PageSize) - 1
     
    DROP TABLE #Results
   -- DROP TABLE #keywords
END

  


  --select * from [strimm].[vw_VideoTubes] where VideoProviderId=4
  --select * from strimm.ChannelTubeVideoTube where ChannelTubeId=1
  --exec [strimm].[GetVideoTubePoByChannelTubeIdAndPageIndex] 1,1,50,'',0
  --insert into strimm.ChannelTubeVideoTube (ChannelTubeId, VideoTubeId,CreatedDate,LastScheduleDateTime) values (1253,3276,getdate(),dateadd(day, -1, getdate()))