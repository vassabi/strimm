sub Init()
  m.title = m.top.findNode("title")
  m.poster = m.top.findNode("poster")
  m.selector = m.top.findNode("selector")
  m.favStar = m.top.findNode("favStar")
  m.frame = m.top.findNode("frame")
  m.top.findNode("background").color = theme().backgroundColor
  m.separator = m.top.findNode("separator")
  m.poster.ObserveField("loadStatus", "onPosterLoadStatusChange")
  m.title.opacity = 1
  m.poster.opacity = 0

  onLayoutChange(theme().channelInfo)

  m.isChannelIconReady = false
  m.horizLeftMargin = 60
  m.starRightMargin = 12
  m.horizRightMargin = 2
  m.verticalMargin = 4
end sub


sub onContentChange()
  if m.top.content <> invalid and m.top.width > 0 and m.top.height > 0
    m.separator.width = m.top.width
    renderingHeight = m.top.height - m.verticalMargin * 2
    m.favStar.height = (m.top.height - m.verticalMargin * 2) * 0.4
    m.favStar.width = m.favStar.height
    m.favStar.translation = [m.horizLeftMargin, (m.top.height - m.favStar.height) / 2]
    m.title.translation = [m.favStar.width + m.horizLeftMargin + m.starRightMargin, m.verticalMargin]
    m.poster.translation = m.title.translation

    if m.top.content.isCurrent = true
      m.title.color = theme().channelInfoFocusedTextColor
      m.selector.color = theme().channelInfoFocusedBackgroundColor
    else
      m.selector.color = theme().channelInfoBackgroundColor
      m.title.color = theme().channelInfoTextColor
    end if

    renderingWidth = m.top.width - m.horizLeftMargin - m.horizRightMargin - m.starRightMargin - m.favStar.width

    m.poster.loadWidth = renderingWidth
    m.poster.loadheight = renderingHeight

    m.title.text = m.top.content.title
    posterUrl = m.top.content.HDSMALLICONURL
    if posterUrl = invalid or posterUrl = "" then posterUrl = m.top.content.HDPOSTERURL
    if m.poster.uri <> posterUrl
      m.isChannelIconReady = false
      m.poster.uri = posterUrl
    end if
    if m.top.content.isFavorite = true
      m.favStar.uri = "pkg:/images/star.png"
    else
      m.favStar.uri = "pkg:/images/starEmpty.png"
    end if

    m.poster.width = renderingWidth
    m.poster.height = renderingHeight
    m.selector.width = renderingWidth
    m.selector.height = m.top.height - 4
    m.selector.translation = [m.title.translation[0], (m.top.height - m.selector.height) / 2]
'    m.frame.width = m.horizLeftMargin + m.starRightMargin + m.favStar.width
    m.frame.width = m.top.width
    m.frame.height = m.top.height
    m.title.width = renderingWidth
    m.title.height = renderingHeight
    onFocusChange()
  end if
end sub


sub onFocusPercentChange()
  m.frame.opacity = m.top.focusPercent
end sub


sub onFocusChange()
'  if m.top.hasFocus
'    m.title.color = theme().channelInfoFocusedTextColor
'    m.selector.color = theme().channelInfoFocusedBackgroundColor
'  else
'    m.selector.color = theme().channelInfoBackgroundColor
'    m.title.color = theme().channelInfoTextColor
'  end if
'  if m.top.hasFocus and m.isChannelIconReady = true
'    m.title.opacity = 0
'    m.poster.opacity = 1
'  else
'    m.title.opacity = 1
'    m.poster.opacity = 0
'  end if
'  m.frame.visible = m.top.hasFocus
end sub


sub onPosterLoadStatusChange(event as Object)
  loadStatus = event.getData()
  if loadStatus = "ready"
'    m.title.visible = false
    m.isChannelIconReady = true
    onFocusChange()
  end if
end sub
