Function Init()
  m.programInfo = m.top.findNode("programInfo")
  m.channelLogo = m.top.findNode("channelLogo")
  m.panelBackground = m.top.findNode("panelBackground")
  m.frame = m.top.findNode("frame")
  m.buttonGroup = m.top.findNode("buttonGroup")
  m.buttonGroup.ObserveField("itemSelected", "OnItemSelected")
  m.defaultButtonHeight = 40
  m.closeButtonId = "CLOSE"
End Function


sub onItemSelected()
  if m.buttonGroup.content.getChild(m.buttonGroup.itemSelected) <> invalid
    m.top.selectedButtonId = m.buttonGroup.content.getChild(m.buttonGroup.itemSelected).id
    m.top.visible = false
  end if
end sub

 
Function onProgramChange()
  if m.top.program <> invalid
    setButtonsContent()
    if isnonemptystr(m.top.program.channelLogo) then m.channelLogo.uri = m.top.program.channelLogo
    m.top.visible = true
    m.buttonGroup.SetFocus(true)
  else
    m.top.visible = false
  end if
end Function


sub setButtonsContent()
  buttons = []
  buttons.push(m.closeButtonId)
  m.buttonGroup.numColumns = buttons.Count()
  lb = createObject("RoSGNode", "Label")
  lb.font = "font:SmallestBoldSystemFont"
  columnWidths = []
  content = CreateObject("roSGNode", "ContentNode")
  for each button in buttons
    btn = content.CreateChild("ContentNode")
    btn.title = button
    btn.id = button
    lb.text = button
    columnWidths.push(int(lb.boundingRect().width) + 20)
    m.defaultButtonHeight = int(lb.boundingRect().height) + 12
  end for
  m.buttonGroup.columnWidths = columnWidths
  m.buttonGroup.translation = [int(m.top.width * 0.1), int(m.top.height * 0.9 - m.defaultButtonHeight)]
  m.buttonGroup.content = content
  m.buttonGroup.jumpToItem = m.buttonGroup.content.getChildCount() - 1
end sub


sub onButtonWidthChanged(message as Object)
  m.buttonGroup.itemSize = [ message.GetData(), m.defaultButtonHeight ]
end sub

 
Function updateLayout()
  m.programInfo.width = int(m.top.width * 0.8)
  m.programInfo.translation = [int(m.top.width * 0.1), int(m.top.height * 0.1)]
  m.buttonGroup.translation = [int(m.top.width * 0.1), int(m.top.height * 0.9 - m.defaultButtonHeight)]
  m.channelLogo.translation = [m.top.width - int(m.top.width * 0.1) - m.channelLogo.width, int(m.top.height * 0.1)]
  m.frame.width = m.top.width
  m.frame.height = m.top.height
end Function


function OnKeyEvent(key as String, press as Boolean) as Boolean
  if press and key = "back"
    m.top.selectedButtonId = m.closeButtonId
    m.top.visible = false
  end if
  return true
end function
