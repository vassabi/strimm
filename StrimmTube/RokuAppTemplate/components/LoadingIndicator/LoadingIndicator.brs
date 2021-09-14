sub init()
  m.busySpinner = m.top.findNode("busySpinner")
  m.fadeAnimation = m.top.findNode("fadeAnimation")
  m.loadingIndicatorGroup = m.top.findNode("loadingIndicatorGroup")
  m.loadingGroup = m.top.findNode("loadingGroup")
  m.background = m.top.findNode("background")

  startAnimation()
end sub


sub updateLayout()
  m.background.width = m.top.width
  m.background.height = m.top.height
  m.background.translation = [0 - m.top.width / 2, 0 - m.top.height / 2]
end sub


sub onImageWidthChange()
  if m.top.imageWidth > 0 then m.busySpinner.poster.width = m.top.imageWidth
end sub


sub onImageHeightChange()
  if m.top.imageHeight > 0 then m.busySpinner.poster.height = m.top.imageHeight
end sub


sub onControlChange(evt)
  if evt.getData()
    startAnimation()
  else
    ' if there is fadeInterval set, fully dispose component before stopping spinning animation
    if m.top.fadeInterval > 0
      m.fadeAnimation.duration = m.top.fadeInterval
      m.fadeAnimation.observeField("state", "onFadeAnimationStateChange")
      m.fadeAnimation.control = "start"
    else
      stopAnimation()
    end if
  end if
end sub


sub startAnimation()
  m.loadingIndicatorGroup.opacity = 1
  m.busySpinner.control = "start"
end sub


sub stopAnimation()
  m.loadingIndicatorGroup.opacity = 0
  m.busySpinner.control = "stop"
end sub


sub onFadeAnimationStateChange()
  if m.fadeAnimation.state = "stopped" then stopAnimation()
end sub
