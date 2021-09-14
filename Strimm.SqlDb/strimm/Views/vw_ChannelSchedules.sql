

CREATE VIEW [strimm].[vw_ChannelSchedules]
AS
	SELECT
		cs.ChannelScheduleId,
		cs.ChannelTubeId,
		ct.Name as ChannelTubeName,
		cs.StartTime,
		cs.IsActive,
		cs.CreatedDate,
		dateadd(mi,sum(vt.Duration),cs.StartTime) as EndTime
	FROM strimm.ChannelSchedule cs
		INNER JOIN strimm.ChannelScheduleVideoTube pl on cs.ChannelScheduleId = pl.ChannelScheduleId
		INNER JOIN strimm.VideoTube vt on pl.VideoTubeId = vt.VideoTubeId
		INNER JOIN strimm.ChannelTube ct on cs.ChannelTubeId = ct.ChannelTubeId
	GROUP BY
		cs.ChannelScheduleId,
		cs.ChannelTubeId,
		ct.Name,
		cs.StartTime,
		cs.IsActive,
		cs.CreatedDate

		--	select 
		--	s.ScheduleId,
		--	s.StartTime,
		--	dateadd(mi,sum(mf.Duration),s.StartTime) as EndTime
		--from #schedule s
		--	inner join #PlayList pl on pl.ScheduleId = s.ScheduleId
		--	inner join #MusicFile mf on pl.MusicFileId = mf.MusicFileId 
		--group by
		--	s.ScheduleId, s.StartTime
