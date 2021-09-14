Function Init()
  mBind(["infoLabels", "channelAbout", "frame", "channelTitle", "buttonGroup"])
  m.buttonGroup.ObserveField("itemSelected", "onItemSelected")
  m.defaultButtonHeight = 40
  m.closeButtonId = "CLOSE"
End Function


sub onItemSelected()
  if m.buttonGroup.content.getChild(m.buttonGroup.itemSelected) <> invalid
    m.top.selectedButtonId = m.buttonGroup.content.getChild(m.buttonGroup.itemSelected).id
    m.top.visible = false
  end if
end sub

 
Function onChannelChange()
  if m.top.channel <> invalid
    setButtonsContent()
    m.channelTitle.text = m.top.channel.title
    m.channelAbout.text = m.top.channel.description
    m.top.visible = true
    m.buttonGroup.SetFocus(true)
  else
    m.top.visible = false
  end if
end Function


sub setButtonsContent()
  if m.top.channel.isCurrent = true
    buttons = ["GO FULLSCREEN"]
  else if m.top.channel.isPlayable = true
    buttons = ["OPEN CHANNEL"]
  else
    buttons = []
  end if
  if m.top.channel.isFavorite
    buttons.push("REMOVE FROM FAVORITES")
  else
    buttons.push("ADD TO FAVORITES")
  end if
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
  m.buttonGroup.jumpToItem = 0  'm.buttonGroup.content.getChildCount() - 1
  updateLayout()
end sub


sub onButtonWidthChanged(message as Object)
  m.buttonGroup.itemSize = [ message.GetData(), m.defaultButtonHeight ]
end sub


Function updateLayout()
  m.buttonGroup.translation = [40, (m.top.height - m.defaultButtonHeight - 20)]
  m.infoLabels.translation = [m.top.width / 2, 20]
  m.channelAbout.width = m.top.width - 40
end Function


function OnKeyEvent(key as String, press as Boolean) as Boolean
  if press and key = "back"
    m.top.selectedButtonId = m.closeButtonId
    m.top.visible = false
  end if
  return true
end function
