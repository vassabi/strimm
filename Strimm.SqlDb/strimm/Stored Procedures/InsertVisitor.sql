
CREATE PROCEDURE strimm.InsertVisitor
( 
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
		INSERT INTO strimm.Visitor
		(
			UserId,
			IpAddress,
			VisitDate,
			VisitDuration,
			Destination,
			ChannelTubeId
		)
		VALUES
		(
			@UserId,
			@IpAddress,
			@VisitDate,
			@VisitDuration,
			@Destination,
			@ChannelTubeId
		)
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
