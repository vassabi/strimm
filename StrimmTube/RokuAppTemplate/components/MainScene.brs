function init()
  m.top.backgroundColor = CreateObject("roAppInfo").GetValue("splash_color")
  m.top.backgroundURI = ""

  m.top.focusable = true

  runTask("initTask", invalid, {responseAA: "onInitTaskResponse"})
end function


sub onInitTaskResponse(event)
  if isnonemptystr(theme().backgroundColor) then m.top.backgroundColor = theme().backgroundColor
  mainGroup = m.top.findNode("mainGroup")
  m.epgScreen = mainGroup.createChild("EpgScreen")
  m.epgScreen.id = "epgScreen"
  m.top.video = mainGroup.createChild("VideoPlayer")
  m.top.video.id = "video"
  m.top.video.backNode = m.epgScreen

  m.epgScreen.setFocus(true)
  m.top.observeField("deepLinking", "onDeepLinkingChanged")
end sub 


sub onAppLaunch(event)
  Dbg("onAppLaunch", event.getData())
end sub 


sub onDeepLinkingChanged(event)
  deepLinking = event.getData()
  Dbg("onDeepLinkingChanged", deepLinking)
  if deepLinking <> invalid and m.epgScreen <> invalid
    m.epgScreen.openChannelID = deepLinking.contentId
    m.top.deepLinking = invalid
'    m.top.loadingIndicatorOn = true
  end if
end sub 


function onKeyEvent(key as String, press as Boolean) as Boolean
  result = false
  Dbg(">>> AppScene >> OnkeyEvent key: " + key + " >> pressed down: ", press)
  if press
    if key = "back"
    end if
  end if
  return result 
end function
