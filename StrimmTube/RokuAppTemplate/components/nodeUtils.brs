Function constants()
  return  {
            regSection: "strimm"
            screenW: 1280
            screenH: 720
            vertFixPadding: 24
            horizFixPadding: 66
          }
end Function


Function onLayoutChange(layoutSettings)
  if isnonemptyAA(layoutSettings)
    for each key in layoutSettings
      if key = "top"
        element = m.top
      else
        if m[key] = invalid then m[key] = m.top.findNode(key)
        element = m[key]
      end if
      if element <> invalid
        if isnonemptyAA(layoutSettings[key].font)
          font = layoutSettings[key].font
          layoutSettings[key].Delete("font")
          appendAAToNode(layoutSettings[key], element)
          element.font = createFont(font.size, font.weight)
        else
          appendAAToNode(layoutSettings[key], element)
        end if
      end if
    end for
  end if
end Function


Sub mBind(bindIds)
  if isnonemptyArray(bindIds)
    for each id in bindIds
      m[id] = m.top.FindNode(id)
    end for
  end if
end Sub


Function findFocusedChild(item = m.top, maxDepth=-1)
  if item.isInFocusChain() and not item.hasFocus() and maxDepth <> 0 and item.getChildCount() > 0
    for i=0 to item.getChildCount() - 1
      if item.getChild(i).isInFocusChain()
        item = findFocusedChild(item.getChild(i), maxDepth - 1)
        exit for
      end if
    end for
  end if
  return item
end function


Function runTask(taskFunc, params=invalid, observersAA=invalid)
  taskNode = CreateObject("roSGNode", "BaseTask")
  if taskNode <> invalid
    taskNode.setFields({
      id: taskFunc
      functionName: taskFunc
    })
    if isNonEmptyAA(observersAA)
      for each key in observersAA.keys()
        taskNode.observeField(key, observersAA[key])
      end for
    end if
    if isNonEmptyAA(params) then taskNode.params = params
    taskNode.control = "RUN"
  end if
  m[taskFunc] = taskNode
  return taskNode
end Function


function createFont(fontSize, fontWeight="Regular") as object
  font = CreateObject("roSGNode", "Font")
  font.uri = "pkg:/fonts/Roboto-" + fontWeight + ".ttf"
  font.size = fontSize
  return font
end function


sub setupGlobals()
  if not m.global.hasField("appRandomUUID")
    m.global.addFields({  appRandomUUID: CreateObject("roDeviceInfo").GetRandomUUID()
                          timeShift: getTimeshift()
                          animationDisabled: not isOpenGl()
                      })
  end if
end sub


Function theme()
  if m.theme = invalid then m.theme = m.global.config.theme
  return m.theme
end Function


'///////////////////////////////////////////'
' Helper function convert AA array to Row Node
Function list2ContentNode(contentList as Object, parseFunc=stubFunc as Function, nodeType="ContentNode" as String, listLimit=contentList.count()) as Object
  result = createObject("roSGNode", nodeType)
  if result = invalid then result = createObject("roSGNode", "ContentNode")
  if isnonemptyArray(contentList)
    if listLimit > contentList.count() then listLimit = contentList.count()
    for i = 0 to listLimit - 1
      item = AAToNode(parseFunc(contentList[i]), nodeType)
      result.appendChild(item)
    end for
  end if
  return result
End Function


'converts AA to ContentNode
Function aAToNode(inputAA = {} as Object, nodeType = "ContentNode" as String)
  item = createObject("roSGNode", nodeType)
  if item = invalid then item = createObject("roSGNode", "ContentNode")
  return appendAAToNode(inputAA, item)
End Function


Function nodeFieldsFilterAA()
  return  { focusedChild  : "focusedChild"
            change        : "change"
            metadata      : "metadata"
            nextPanelName : "nextPanelName"
            children      : "children"
          }
End Function


Function appendAAToNode(inputAA = {} as Object, item = invalid as Object)
  if item = invalid then item = createObject("roSGNode", "Node")
  existingFields = {}
  newFields = {}
    'AA of node read-only fields for filtering'
  fieldsFilterAA = nodeFieldsFilterAA()
  for each field in inputAA
    if item.hasField(field)
      if NOT fieldsFilterAA.doesExist(field) then existingFields[field] = inputAA[field]
    else
      newFields[field] = inputAA[field]
    end if
  end for
  item.setFields(existingFields)
  item.addFields(newFields)
  return item
End Function


Function nodeToAA(node)
  result = {}
  if node <> invalid
    nodeFields = node.getFields()
    'AA of node read-only fields for filtering'
    fieldsFilterAA = nodeFieldsFilterAA()
    for each field in nodeFields
      if NOT fieldsFilterAA.doesExist(field) then result[field] = nodeFields[field]
    end for
  end if
  return result
End Function


Function stubFunc(itemAA as Object) as Object
  return itemAA
End Function


Function findChildNode(node, value, field="id", recursive=false)
  for i = 0 to node.getChildCount() - 1
    if node.getChild(i)[field] = value then return node.getChild(i)
    if recursive and node.getChild(i).getChildCount() > 0
      item = findChildNode(node.getChild(i), value, field, recursive)
      if item <> invalid then return item
    end if
  end for
  return invalid
End Function


Function clearNodeAndCreateChild(topNode, nodeType, itemId=invalid)
  topNode.removeChildren(topNode.getChildren(-1, 0))
  item = topNode.createChild(nodeType)
  if itemId = invalid then itemId = nodeType
  item.id = itemId
  return item
End Function


sub setNodeFields(node, itemAA)
  appendAAToNode(itemAA, node)
end sub


sub setNodeField(node, key, value)
  if node.hasField(key)
    node[key] = value
  else
    fields = {}
    fields[key] = value
    node.addFields(fields)
  end if
end sub


sub setPosterMaxSize(poster, uri)
  if poster <> invalid and isnonemptystr(uri)
    poster.loadDisplayMode = "limitSize"
    di = CreateObject("roDeviceInfo")
    poster.loadWidth = di.GetUIResolution().width
    poster.loadHeight = di.GetUIResolution().height
    poster.uri = uri
    poster.width = di.GetUIResolution().width
    poster.height = di.GetUIResolution().height
  end if
end sub







'============ App utils =================='



Sub setVideo(isFullScreen=false)
  videoPlayer = m.top.getScene().video
  Dbg("videoPlayer.state", videoPlayer.state)
  if videoPlayer <> invalid and videoPlayer.state = "playing"
    videoPlayer.isFullscreen = isFullscreen
    videoPlayer.setFocus(true)
  end if
End Sub


Sub playChannel(channel)
  if m.videoPlayer = invalid
    m.videoPlayer = m.top.getScene().video
    m.videoPlayer.observeField("program", "onProgramIndex")
    m.videoPlayer.observeField("adPlaying", "onAdPlaying")
  end if
'  m.channelTitle.text = channel.TITLE
'  m.channelAbout.text = channel.description
  m.videoPlayer.channel = channel
  setCurrentChannel(channel.id)
End Sub


Function findCurrentProgramIndex(content)
  if content <> invalid and content.getChildCount() > 0
    if nowTimestamp() < content.getChild(0).PLAYSTART then return -1
    for i = 0 to content.getChildCount() - 1
      program = content.getChild(i)
      now = nowTimestamp()
      if program.PLAYSTART <= now and now < program.PLAYSTART + program.PLAYDURATION then return i
    end for
  end if
  return 0
end Function


function goBack()
  m.top.getParent().removeChild(m.top)
  if m.top.backNode <> invalid
    m.top.backNode.setFocus(true)
    return true
  end if
  return false
End function










'============ Dialogs =================='

Function AskPINDialog(observFunctionName, title="Entrar PIN:")
    m.askPinDialog = createObject("RoSGNode","PinDialog")
    'm.buttons =  [ "Enviar", "Cerrar" ]
    'm.pinDialog.message = m.optionsChannel.title
    m.askPinDialog.buttons = [ "Enviar", "Cerrar" ]
    'm.pinDialog.backgroundUri = "pkg:/images/dialog.png"
    m.askPinDialog.title = title
    'm.pinDialog.optionsDialog = true
    'm.pinDialog.messageFont = "font:LargeBoldSystemFont"
    m.askPinDialog.observeField("buttonSelected", observFunctionName)
    getScene().dialog = m.askPinDialog
End Function


function showMessage(fields = {title: "Error", message: "Network problem detected. Please check your connection", buttons: ["OK"]}, functionToObserve = "onDefaultCloseDialog" as String, subType="Dialog")
  dialog = createObject("roSGNode", subType)
  dialog.id = subType
  dialog.setFields(fields)
  dialog.observeField("buttonSelected", functionToObserve)
  dialog.observeField("wasClosed", functionToObserve)
  m.top.getScene().dialog = dialog
end function


sub onDefaultCloseDialog(event as Object)
  dialog = event.GetRoSGNode()
  dialog.close = true
end sub








'============ Roku =================='

