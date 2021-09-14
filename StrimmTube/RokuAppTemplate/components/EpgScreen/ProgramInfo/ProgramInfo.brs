Function Init()
'  m.channellogo = m.top.findNode("channellogo")
  m.programTitle = m.top.findNode("programTitle")
  m.programSubtitle = m.top.findNode("programSubtitle")
  m.programDesc = m.top.findNode("programDesc")

  onLayoutChange(theme().ProgramInfo)
End Function


Function onProgramChange()
'    m.channellogo.uri = channel.HDPOSTERURL
  m.programTitle.width = m.top.width - evalInteger(m.top.margins[0]) * 2
  m.programDesc.width = m.programTitle.width

  if m.top.program <> invalid
    m.programTitle.text = m.top.program.title
    if m.top.maxDescLines > 0
      m.programDesc.text = m.top.program.description
    else
      m.programDesc.text = ""
    end if
    if isnonemptystr(m.top.program.channelNumber) and isnonemptystr(m.top.program.callsign)
      m.programSubtitle.text = m.top.program.channelNumber + "   " + m.top.program.callsign + "   |   "
    else
      m.programSubtitle.text = ""
    end if
    m.programSubtitle.text += secondsToTime(utcToLocal(m.top.program.PLAYSTART), true, true) + " - " + secondsToTime(utcToLocal(m.top.program.PLAYSTART + m.top.program.PLAYDURATION), true, true)
    m.top.visible = true
  end if
end Function
