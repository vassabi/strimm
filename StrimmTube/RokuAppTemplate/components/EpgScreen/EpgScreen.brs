sub Init()
  Dbg("EpgScreen Init")
  mBind(["timeGrid", "channelHint", "aboutHint", "channelTitle", "networkTitle",
          "channelAbout", "infoGroup", "programPanel", "programInfo", "fullscreenHint",
          "currentProgramInfo", "buttonGroup", "channelPanel", "hintsGroupFrameAnimation"])

  m.timeGrid.ObserveField("channelInfoFocused", "onChannelInfoFocused")
  m.timeGrid.ObserveField("channelUnfocused", "onChannelInfoUnFocused")
  m.timeGrid.ObserveField("channelFocused", "onChannelFocused")
  m.timeGrid.ObserveField("channelSelected", "onChannelSelected")
  m.timeGrid.ObserveField("programFocused", "onProgramFocused")
  m.timeGrid.ObserveField("programSelected", "onProgramSelected")
  m.timeGrid.ObserveField("channelInfoSelected", "onChannelInfoSelected")
  m.timeGrid.ObserveField("isEscaping", "onEscaping")
  m.programPanel.ObserveField("visible", "onProgramPanelClose")
  m.channelPanel.ObserveField("visible", "onProgramPanelClose")
  m.buttonGroup.ObserveField("itemSelected", "onButtonSelected")

  m.fullScreenTimer = CreateObject("roSGNode","Timer")
  m.fullScreenTimer.duration = 20
  m.fullScreenTimer.repeat = false
  m.fullScreenTimer.ObserveField("fire", "onFullScreenTimer")

  m.goTopHintCounter = 3
  m.networkTitle.text = CreateObject("roAppInfo").GetTitle()

  m.currentControl = m.timeGrid

  onLayoutChange(theme().EpgScreen)
  m.timeGrid.timeLabelFont = createFont(theme().timeLabelFont.size, theme().timeLabelFont.weight)
  m.timeGrid.programTitleFont = createFont(theme().programTitleFont.size, theme().programTitleFont.weight)

  m.top.observeField("focusedChild", "onFocusedChildChange")
end sub


sub onFocusedChildChange()
  if m.top.hasFocus()
    Dbg("onFocusedChildChange", m.top.hasFocus())
    setVideo()
    m.currentControl.setFocus(true)
    if m.timeGrid.content = invalid
      runTask("epgTask", {timestamp: utcToLocal(nowTimestamp())}, {responseNode: "onEpgTaskResponse"})
    else
      m.hintsGroup2.visible = true
      restartFullScreenTimer()
    end if
  end if
end sub


sub onAdPlaying(evt)
  if evt.getData()
    Dbg("onAdPlaying")
    m.fullScreenTimer.control = "stop"
  else
    setVideo()
    m.currentControl.setFocus(true)
    m.hintsGroup2.visible = true
    restartFullScreenTimer()
  end if
end sub


Function restartFullScreenTimer()
  m.fullScreenTimer.control = "stop"
  if m.timeGrid.isInFocusChain()
    m.fullScreenTimer.duration = 30
    m.fullScreenTimer.control = "start"
  end if
End Function


Function onFullScreenTimer()
  setVideo(true)
End Function


sub setButtonsContent()
  buttons = ["Search", "About"]
  m.buttonGroup.numColumns = buttons.Count()
  lb = createObject("RoSGNode", "Label")
  lb.font = createFont(theme().ButtonIconMarkup.buttonLabel.font.size, theme().ButtonIconMarkup.buttonLabel.font.weight)
  columnWidths = []
  content = CreateObject("roSGNode", "ContentNode")
  for each button in buttons
    btn = content.CreateChild("ContentNode")
    btn.title = button
    btn.id = button
    lb.text = button
    width = int(lb.boundingRect().width) + 20
    if button = "Search"
      btn.HDPosterUrl = "pkg:/images/icon_search_$$RES$$.png"
      width += 26 + 4
    end if
    columnWidths.push(width)
    m.defaultButtonHeight = int(lb.boundingRect().height) + 12
  end for
  m.buttonGroup.columnWidths = columnWidths
  m.buttonGroup.content = content
end sub


sub onEpgTaskResponse(event)
  Dbg("onEpgTaskResponse")
  if m.timeGrid.content = invalid then m.contentStartTime = getHourStart(nowTimestamp())
  content = event.getData()
  if content <> invalid
    m.timeGrid.content = content.clone(true)
    if m.contentStartTime <> invalid
      m.timeGrid.visible = true
      if 1800 < nowTimestamp() - m.contentStartTime then m.contentStartTime += 1800
      m.timeGrid.contentStartTime = m.contentStartTime
      m.timeGrid.leftEdgeTargetTime = m.contentStartTime
      m.timeGrid.duration = m.timeGrid.content.timeGridDuration
      m.contentStartTime = invalid
    end if
    m.timeGrid.indexItemFocused = evalInteger(content.channelIndex)
    Dbg("m.timeGrid.indexItemFocused", m.timeGrid.indexItemFocused)
    m.timeGrid.jumpToChannel = m.timeGrid.indexItemFocused
    if m.top.getScene().deepLinking <> invalid
      m.top.openChannelID = m.top.getScene().deepLinking.contentId
      m.top.getScene().deepLinking = invalid
    else
      channel = m.timeGrid.content.getChild(m.timeGrid.indexItemFocused)
      m.fullscreenHint.text = "to go fullscreen"
      m.hintsGroup2.visible = true
      if channel <> invalid then m.top.openChannelID = channel.id
    end if
    m.infoGroup.visible = true
    m.hintsGroup2.visible = true
    restartFullScreenTimer()
    setButtonsContent()
  end if
  m.top.getScene().loadingIndicatorOn = false
end sub


sub onGetEpgTaskResponse(event)
  Dbg("onGetEpgTaskResponse")
'  m.timeGridProgramFocused = m.timeGrid.programFocused
'  m.timeGrid.content = event.getData()
'  m.timeGrid.jumpToProgram = m.timeGridProgramFocused
end sub


function onChannelInfoFocused(event as Object)
'  m.timeGrid.focusBitmapUri = ""
  m.programInfo.visible = false
  m.hintsGroup2.visible = true
'  if m.timeGrid.content.getChild(event.getData()).isFavorite
'    m.fullscreenHint.text = "to remove from favorites"
'  else
'    m.fullscreenHint.text = "to add to favorites"
'  end if
  m.fullscreenHint.text = "to see channel options"
end function


sub onChannelInfoUnFocused(event as Object)
'  m.timeGrid.focusBitmapUri = "pkg:/images/list_focus.9.png"
  m.programInfo.visible = false
  m.hintsGroup2.visible = true
end sub


sub onEscaping(event as Object)
  isEscaping = event.getData()
  Dbg("onEscaping", isEscaping)
  if isEscaping then setFocusOnButtons()
end sub


sub setFocusOnButtons()
  m.buttonGroup.setFocus(true)
  m.fullScreenTimer.control = "stop"
  m.programInfo.visible = false
  m.hintsGroup2.visible = false
  m.currentControl = m.buttonGroup
end sub


sub setFocusOnGrid()
  Dbg("setFocusOnGrid")
  m.timeGrid.setFocus(true)
  restartFullScreenTimer()
  m.programInfo.visible = false
  m.hintsGroup2.visible = true
  m.currentControl = m.timeGrid
  m.buttonGroup.jumpToItem = 0
  onProgramFocused()
end sub


sub onChannelFocused(event as Object)
  restartFullScreenTimer()
  index = event.getData()
  Dbg("onChannelFocused", index)
  m.timeGrid.indexItemFocused = index
  onProgramFocused()
  if m.goTopHintCounter >= 0 and index > 2
    m.hintsGroupFrameAnimation.duration = 5
    m.hintsGroupFrameAnimation.control = "start"
    m.goTopHintCounter--
  end if
end sub


sub requestNextDayIfRequired(playstart=invalid)
  if m.timeGrid.content <> invalid
    Dbg("requestNextDayIfRequired m.timeGrid.content.lastStartTime", m.timeGrid.content.lastStartTime)
    if playstart = invalid then playstart = utcToLocal(nowTimestamp())
    if m.timeGrid.content.timestamp < m.timeGrid.content.lastStartTime + 10800 and playstart >= m.timeGrid.content.lastStartTime - 18000
      m.timeGrid.content.timestamp += 24 * 60 * 60
      Dbg("m.timeGrid.content.timestamp", m.timeGrid.content.timestamp)
      runTask("getEpgTask", {content: m.timeGrid.content, timestamp: m.timeGrid.content.timestamp}, {responseNode: "onGetEpgTaskResponse"})
    end if
  end if
end sub


sub onProgramFocused()
  restartFullScreenTimer()
  Dbg("onProgramFocused", m.timeGrid.programFocused)
'  m.timeGrid.focusBitmapUri = "pkg:/images/list_focus.9.png"
  m.currentProgramInfo.visible = false
  m.hintsGroup2.visible = false
  if m.timeGrid.content <> invalid
    channel = m.timeGrid.content.getChild(m.timeGrid.channelFocused)
    if channel <> invalid
      program = channel.getChild(m.timeGrid.programFocused)
      if program <> invalid
'        m.programInfo.program = nodeToAA(program)
        m.currentProgramInfo.program = nodeToAA(program)
        requestNextDayIfRequired(program.PLAYSTART)
        now = nowTimestamp()
        if program.PLAYSTART <= now and now < program.PLAYEND
          m.channelTitle.text = "Playing now on " + channel.TITLE
          if m.videoPlayer <> invalid
            if m.videoPlayer.channel = invalid or m.videoPlayer.channel.id <> channel.id
              m.fullscreenHint.text = "to open channel"
              m.hintsGroup2.visible = true
            else
              m.fullscreenHint.text = "to go fullscreen"
              m.hintsGroup2.visible = true
            end if
          end if
        else if m.videoPlayer <> invalid
'          if m.videoPlayer.channel = invalid or m.videoPlayer.channel.id <> channel.id
'            m.fullscreenHint.text = "to open channel"
'            m.hintsGroup2.visible = false
'          else
'            m.fullscreenHint.text = "to go fullscreen"
'            m.hintsGroup2.visible = true
'          end if
          if now < program.PLAYSTART
            m.channelTitle.text = "Coming up later on " + channel.TITLE
          else
            m.channelTitle.text = "Past broadcast on " + channel.TITLE
          end if
        end if
      else
        m.channelTitle.text = channel.TITLE
      end if
    else
      m.channelTitle.text = channel.TITLE
    end if
  end if
end sub


function onProgramSelected(event as Object)
  restartFullScreenTimer()
  if m.timeGrid.content <> invalid
    channel = m.timeGrid.content.getChild(m.timeGrid.channelFocused)
    if channel <> invalid
      program = channel.getChild(m.timeGrid.programFocused)
      if program <> invalid and m.hintsGroup2.visible
'        if nowTimestamp() < program.PLAYSTART
'          m.fullScreenTimer.control = "stop"
'          m.programPanel.program = nodeToAA(program)
'        else 
        if m.videoPlayer.channel = invalid or m.videoPlayer.channel.id <> channel.id
          playChannel(channel)
          m.fullscreenHint.text = "to go fullscreen"
        else
          setVideo(true)
        end if
      end if
    end if
  end if
end function


function onChannelSelected(event as Object)
  restartFullScreenTimer()
end function


function onChannelInfoSelected(event as Object)
  restartFullScreenTimer()
  if m.timeGrid.content <> invalid
    m.channelInfoSelected = event.getData()
    channel = m.timeGrid.content.getChild(m.timeGrid.channelInfoSelected)
    if channel <> invalid
      m.fullScreenTimer.control = "stop"
      channelAA = nodeToAA(channel)
      channelAA.isPlayable = channel.getChildCount() > 0 and findCurrentProgramIndex(channel) >= 0
      m.channelPanel.channel = channelAA
    end if
  end if
end function


function toggleChannelFav()
  Dbg("toggleChannelFav")
  restartFullScreenTimer()
  if m.timeGrid.content <> invalid and m.channelPanel.channel <> invalid
    id = m.channelPanel.channel.id
    if m.timeGrid.content <> invalid and m.timeGrid.content.getChildCount() > 0
      for i = 0 to m.timeGrid.content.getChildCount() - 1
        if m.timeGrid.content.getChild(i).id = id
          channel = m.timeGrid.content.getChild(i)
          m.channelInfoSelected = i
          if channel <> invalid
            favs = m.global.favs
            if channel.isFavorite
              favs.Delete(channel.id)
            else
              favs[channel.id] = 0
            end if
            channel.isFavorite = not channel.isFavorite
            m.global.favs = favs
            regWrite("favs", FormatJSON(favs), constants().regSection)
          end if
          Dbg("m.channelInfoSelected", m.channelInfoSelected)
          m.timeGrid.jumpToChannel = m.channelInfoSelected
          m.channelInfoSelected = invalid
          exit for
        end if
      end for
    end if
  end if
end function


function onProgramIndex(event as Object)
  restartFullScreenTimer()
  program = event.getData()
  channel = event.getRoSGNode().content
  if channel <> invalid
    if program <> invalid
      currentTime = utcToLocal(nowTimestamp())
      currentTime = nowTimestamp()
      contentStartTime = getHourStart(currentTime)
      if 1800 < currentTime - contentStartTime then contentStartTime += 1800
      m.timeGrid.jumpToTime = currentTime
      m.timeGrid.contentStartTime = contentStartTime
'      m.currentProgramInfo.program = nodeToAA(program)
    end if
  end if
  requestNextDayIfRequired()
end function


Sub onProgramPanelClose(event)
  if not event.getData()
    setFocusOnGrid()
    panel = event.getRoSGNode()
    if panel.selectedButtonId = "OPEN CHANNEL"
      channel = m.timeGrid.content.getChild(m.timeGrid.channelFocused)
      if channel <> invalid
        if m.videoPlayer.channel = invalid or m.videoPlayer.channel.id <> channel.id then playChannel(channel)
      end if
    else if panel.selectedButtonId = "GO FULLSCREEN"
      setVideo(true)
    else if panel.selectedButtonId = "ADD TO FAVORITES" or panel.selectedButtonId = "REMOVE FROM FAVORITES"
      toggleChannelFav()
    end if
  end if
End Sub


Sub onButtonSelected(event)
  index = event.getData()
  if index = 0
    mainGroup = m.top.getScene().findNode("mainGroup")
    m.searchScreen = mainGroup.createChild("SearchScreen")
    m.searchScreen.setFields({
          id: "searchScreen"
          backNode: m.currentControl
          content: m.timeGrid.content
        })
    m.searchScreen.setFocus(true)
  else if index = 1
    mainGroup = m.top.getScene().findNode("mainGroup")
    aboutScreen = mainGroup.createChild("AboutScreen")
    aboutScreen.setFields({
          id: "aboutScreen"
          backNode: m.currentControl
        })
    aboutScreen.setFocus(true)
  end if
End Sub


Sub onOpenChannelIDChange(event)
  id = event.getData()
  Dbg("onOpenChannelIDChange", id)
  if m.timeGrid.content <> invalid and m.timeGrid.content.getChildCount() > 0
    for i = 0 to m.timeGrid.content.getChildCount() - 1
      if m.timeGrid.content.getChild(i).id = id
        channel = m.timeGrid.content.getChild(i).clone(true)
        playChannel(channel)
        Dbg("m.timeGrid.jumpToChannel", i)
        m.timeGrid.jumpToChannel = i
        index = findCurrentProgramIndex(channel)
        program = channel.getChild(index)
'        if program <> invalid then m.currentProgramInfo.program = nodeToAA(program)
        exit for
      end if
    end for
  end if
End Sub


Sub setCurrentChannel(id)
  if m.timeGrid.content <> invalid and m.timeGrid.content.getChildCount() > 0 and isnonemptystr(id)
    for i = 0 to m.timeGrid.content.getChildCount() - 1
      appendAaToNode({isCurrent : m.timeGrid.content.getChild(i).id = id}, m.timeGrid.content.getChild(i))
    end for
  end if
End Sub


function onKeyEvent(key as String, press as Boolean) as Boolean
  handled = false
  Dbg(">>> EpgScreen >> OnkeyEvent key: " + key + " >> pressed down: ", press)
  if press
    if key = "back"
      if m.top.getScene().video.width = 0
        setVideo()
        setFocusOnGrid()
        requestNextDayIfRequired()
        handled = true
      else if m.timeGrid.isInFocusChain()
        setFocusOnButtons()
        handled = true
      end if
    else if key = "OK"
      if m.top.getScene().video.width = 0
        handled = true
      end if
    else if key = "play"
      if m.top.getScene().video.width > 0
        setFocusOnGrid()
        setVideo(true)
        handled = true
      else if m.top.getScene().video.width = 0
        setVideo()
        setFocusOnGrid()
        handled = true
      end if
    else if key = "down"
      if m.buttonGroup.isInFocusChain()
        setFocusOnGrid()
        handled = true
      end if
    end if
  end if
  return handled 
end function
