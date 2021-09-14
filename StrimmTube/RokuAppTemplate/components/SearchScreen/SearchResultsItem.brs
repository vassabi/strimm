sub init()
  m.channelName = m.top.findNode("channelName")
  m.programName = m.top.findNode("programName")
end sub

sub showContent()
  if m.top.itemContent <> invalid and m.top.width > 0 and m.top.height > 0
    m.channelName.height = m.top.height
    m.channelName.width = m.top.width / 2 - 10
    m.programName.height = m.top.height
    m.programName.width = m.top.width / 2
    m.programName.translation = [m.top.width - m.programName.width, 0]
    m.channelName.text = m.top.itemContent.title
    m.programName.text = m.top.itemContent.description
    if m.top.itemContent.highlightChannel
      m.channelName.color = theme().searchResultHighlightColor
    else
      m.channelName.color = theme().searchResultColor
    end if
    if m.top.itemContent.highlightProgram
      m.programName.color = theme().searchResultHighlightColor
    else
      m.programName.color = theme().searchResultColor
    end if
  end if
end sub
