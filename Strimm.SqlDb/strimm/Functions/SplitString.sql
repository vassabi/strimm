CREATE Function strimm.[SplitString] 
(
    @keywords Varchar(max),
	@delimiter char
)
RETURNS @Table TABLE (ColumnData VARCHAR(100))
AS
BEGIN
    IF RIGHT(@keywords, 1) <> @delimiter
    SELECT @keywords = @keywords + @delimiter

    DECLARE @Pos    BIGINT,
            @OldPos BIGINT
    SELECT  @Pos    = 1,
            @OldPos = 1

    WHILE   @Pos < LEN(@keywords)
        BEGIN
            SELECT  @Pos = CHARINDEX(@delimiter, @keywords, @OldPos)
            INSERT INTO @Table
            SELECT  LTRIM(RTRIM(SUBSTRING(@keywords, @OldPos, @Pos - @OldPos))) Col001

            SELECT  @OldPos = @Pos + 1
        END

    RETURN
END
