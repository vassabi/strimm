sub Init()
  mBind(["keyboard", "results"])
  onLayoutChange(theme().searchScreen)
  m.keyboard.observeField("text", "searchText")
  m.results.ObserveField("itemSelected", "onResultsSelected")
  m.top.observeField("focusedChild", "onFocusedChildChange")
end sub


sub onFocusedChildChange()
  if m.top.hasFocus()
    Dbg("onFocusedChildChange", m.top.hasFocus())
    m.keyboard.setFocus(true)
  end if
  m.top.visible = m.top.isInFocusChain()
end sub


sub searchText()
  if m.keyboard.text.Len() > 0 and m.top.content <> invalid
    content = createObject("roSGNode", "ContentNode")
    query = LCase(m.keyboard.text.trim())
    Dbg("query", query)
    for i = 0 to m.top.content.getChildCount() - 1
      channel = m.top.content.getChild(i).clone(true)
      appendAAToNode({highlightChannel: false, highlightProgram: false}, channel)
      index = findCurrentProgramIndex(channel)
      program = channel.getChild(index)
      if LCase(channel.title).instr(0, query) >= 0
        channel.highlightChannel = true
      end if
      if program <> invalid and LCase(program.title).instr(0, query) >= 0
        channel.highlightProgram = true
      end if
      if channel.highlightChannel or channel.highlightProgram
        if program <> invalid then channel.Description = program.title
        content.appendChild(channel)
      end if
    end for
    m.results.content = content
  else
    m.results.content = invalid
  end if
end sub


Sub onResultsSelected(event)
  index = event.getData()
  if m.top.content <> invalid and m.results.content.getChild(index) <> invalid
    epgScreen = m.top.getScene().findNode("mainGroup").findNode("epgScreen")
    if epgScreen <> invalid then epgScreen.openChannelID = m.results.content.getChild(index).id
  end if
  goBack()
End Sub


function onKeyEvent(key as String, press as Boolean) as Boolean
  handled = false
  if press
    Dbg(">>> SearchScreen >> OnkeyEvent key: " + key + " >> pressed down: ", press)
    if key = "back"
      handled = goBack()
    else if key = "right"
      if m.results.content <> invalid and m.results.content.getChildCount() > 0
        m.results.setFocus(true)
        handled = true
      end if
    else if key = "left"
      m.keyboard.setFocus(true)
      handled = true
    end if
  end if
  return handled 
end function
