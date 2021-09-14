sub Init()
  mBind(["channelName", "channelIcon", "playerOverlayGradient", "channelGroup", "programTitle", "programSubtitle"])
  m.channelIcon.ObserveField("loadStatus", "onPosterLoadStatusChange")
  m.hideTimer = CreateObject("roSGNode", "Timer")
  m.hideTimer.duration = 3
  m.hideTimer.repeat = false
  m.hideTimer.ObserveField("fire", "hideInfo")
end sub


Function hideInfo()
  m.top.visible = false
End Function


Function restartHideTimer()
  m.hideTimer.control = "stop"
  if m.top.hideDelay > 0
    m.hideTimer.duration = m.top.hideDelay
    m.hideTimer.control = "start"
  end if
End Function


sub onSizeChange()
  if m.top.width > 0 and m.top.height > 0
    onLayoutChange(theme().onScreenInfo)
    m.playerOverlayGradient.width = m.top.width
    m.playerOverlayGradient.height = m.top.height * 0.4
    m.playerOverlayGradient.translation = [0, m.top.height - m.playerOverlayGradient.height]
    m.channelGroup.translation = [50, m.top.height - m.playerOverlayGradient.height / 4]
'    m.favStar.height = (m.top.height - m.verticalMargin * 2) * 0.4
'    m.favStar.width = m.favStar.height
    m.channelIcon.width = m.playerOverlayGradient.width / 4 - m.channelGroup.translation[0] - m.channelGroup.itemSpacings[0]  ' - m.favStar.width
    m.channelIcon.height = m.playerOverlayGradient.height / 4
    m.channelName.width = m.channelIcon.width
    m.channelName.height = m.channelIcon.height
    m.programTitle.width = m.top.width - m.channelGroup.translation[0] - m.channelGroup.itemSpacings[0] - m.channelGroup.itemSpacings[1] - m.channelIcon.width  ' - m.favStar.width
  end if
end sub


sub onContentChange()
  if m.top.channel <> invalid
    m.channelName.text = m.top.channel.title
    m.channelName.visible = true
    m.channelIcon.visible = false
    channelIconUrl = m.top.channel.HDSMALLICONURL
    if channelIconUrl = invalid or channelIconUrl = "" then channelIconUrl = m.top.channel.HDPOSTERURL
    m.channelIcon.uri = channelIconUrl
  end if
end sub


Function onProgramChange()
  if m.top.program <> invalid
    m.programTitle.text = m.top.program.title
    m.programSubtitle.text = secondsToTime(utcToLocal(m.top.program.PLAYSTART), true, true) + " - " + secondsToTime(utcToLocal(m.top.program.PLAYSTART + m.top.program.PLAYDURATION), true, true)
  end if
end Function


sub onFocusChange()
  if m.top.hasFocus
    m.channelName.color = theme().channelInfoFocusedTextColor
    m.playerOverlayGradient.color = theme().channelInfoFocusedBackgroundColor
  else
    m.playerOverlayGradient.color = theme().channelInfoBackgroundColor
    m.channelName.color = theme().channelInfoTextColor
  end if
  if m.top.hasFocus and m.isChannelIconReady = true
    m.channelName.opacity = 0
    m.channelIcon.opacity = 1
  else
    m.channelName.opacity = 1
    m.channelIcon.opacity = 0
  end if
end sub


sub onPosterLoadStatusChange(event as Object)
  loadStatus = event.getData()
  if loadStatus = "ready"
    m.channelName.visible = false
    m.channelIcon.visible = true
  end if
end sub
