
CREATE PROCEDURE [strimm].[UpdateVisitor]
( 
	@VisitorId int,
	@UserId int,
	@IpAddress nvarchar(45),
	@VisitDate datetime,
	@VisitDuration int,
	@Destination nvarchar(50),
	@ChannelTubeId int
)
AS
BEGIN
	BEGIN TRY
		UPDATE strimm.Visitor
		SET
			UserId = @UserId,
			IpAddress = @IpAddress,
			VisitDate = @VisitDate,
			VisitDuration = @VisitDuration,
			Destination = @Destination,
			ChannelTubeId = @ChannelTubeId
		WHERE
			VisitorId = @VisitorId
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
