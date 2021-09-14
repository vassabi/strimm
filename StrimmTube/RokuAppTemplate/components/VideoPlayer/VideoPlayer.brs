sub Init()
  m.top.visible = false
  m.top.notificationInterval = 1
  m.top.enableUI = false
  m.top.enableTrickplay = false
  mBind(["loadingIndicator", "onScreenInfo", "playerTask"])
  onLayoutChange(theme().videoPlayer)
  m.playerTask.video = m.top
'  m.channelIcon.ObserveField("loadStatus", "onPosterLoadStatusChange")
  m.playerTask.observeField("programIndex", "onProgramIndex")
  onSizeChange()
end sub


function onProgramIndex(event as Object)
  programIndex = event.getData()
  channel = event.getRoSGNode().content
  if channel <> invalid
    m.top.program = channel.getChild(programIndex)
  end if
end function


sub onChannelChange()
  if m.top.channel <> invalid
    m.playerTask.control = "STOP"
    m.playerTask.content = m.top.channel
    m.top.control = "stop"
    if m.playerTask.state <> "run"
      m.playerTask.functionName = "playContentWithAds"
      m.playerTask.control = "RUN"
    end if
    showScreenInfo(m.onScreenInfo.hideDelay)
  end if
end sub


Function onProgramChange()
  if m.top.program <> invalid
  end if
end Function


sub onSizeChange()
  Dbg("onSizeChange", m.top.isFullscreen)
  if m.top.isFullscreen
    m.top.width = 0
    m.top.height = 0
    m.onScreenInfo.width = 1280
    m.onScreenInfo.height = 720
    m.onScreenInfo.programInfoGroup = true
    showScreenInfo(3)
  else
    m.top.width = 710
    m.top.height = 400
    m.onScreenInfo.width = m.top.width
    m.onScreenInfo.height = m.top.height
    m.onScreenInfo.programInfoGroup = false
    showScreenInfo(0)
  end if
  m.loadingIndicator.width = m.onScreenInfo.width
  m.loadingIndicator.height = m.onScreenInfo.height
  m.loadingIndicator.translation = [m.onScreenInfo.width / 2, m.onScreenInfo.height / 2]
  if m.top.state = "playing" then m.top.loadingIndicatorOn = false
end sub


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


sub showScreenInfo(delay)
  m.onScreenInfo.visible = true
  m.onScreenInfo.hideDelay = delay
end sub


function onKeyEvent(key as String, press as Boolean) as Boolean
  handled = false
  Dbg(">>> VIDEO >> OnkeyEvent key: " + key + " >> pressed down: ", press)
  if press
    if key = "back"
      if m.top.isFullscreen and m.top.state <> "paused"
        setVideo()
        if m.top.backNode <> invalid then m.top.backNode.setFocus(true)
        handled = true
      end if
    else if key = "OK"
      if m.top.isFullscreen
        showScreenInfo(3)
        handled = true
      end if
    else if key = "play"
      Dbg(m.top.state, m.top.keepPlaying)
      if m.top.isFullscreen and m.top.keepPlaying and m.top.state <> "paused"
        m.top.control = "pause"
        showScreenInfo(0)
        handled = true
      else if m.top.state = "paused"
        m.top.control = "resume"
        showScreenInfo(3)
        handled = true
      end if
    end if
  end if
  return handled 
end function
