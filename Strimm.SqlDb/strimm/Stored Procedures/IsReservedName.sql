
CREATE PROCEDURE [strimm].[IsReservedName]
(
	@Name nvarchar(255),
	@IsReserved bit output
)
AS
BEGIN

	DECLARE @ReservedNameId int;

	SELECT
		@ReservedNameId = ReservedNameId
	FROM strimm.ReservedName
	WHERE
		Name = @Name

	IF @ReservedNameId > 0
		BEGIN
			SET @IsReserved = 1
		END
	ELSE
		BEGIN
			SET @IsReserved = 0
		END
END
