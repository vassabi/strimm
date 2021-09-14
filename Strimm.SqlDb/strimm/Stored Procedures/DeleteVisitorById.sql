
CREATE PROCEDURE strimm.DeleteVisitorById
( 
	@VisitorId int
)
AS
BEGIN
	BEGIN TRY
		DELETE 
		FROM strimm.Visitor
		WHERE
			VisitorId = @VisitorId
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
