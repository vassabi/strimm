Library "Roku_Ads.brs"

sub init()
  m.top.functionName = "playContentWithAds"
  m.top.id = "playerTask"
end sub


sub storeBookmark(position=invalid)
  m.top.bookmark = position
  if m.top.content <> invalid and isnonemptystr(m.top.content.id) and m.top.bookmark <> invalid
    bookmarks = ParseJson(RegRead("bookmarks", m.global.config.regSection, "{}"))
    if m.top.bookmark > 0
      bookmarkAA = {}
      bookmarkAA[m.top.content.id] = m.top.bookmark
      bookmarks.append(bookmarkAA)
    else
      bookmarks.delete(m.top.content.id)
    end if
    RegWrite("bookmarks", FormatJson(bookmarks), m.global.config.regSection)
  end if
end sub


sub playVideoContent()
  video = m.top.video
  content = m.top.content
  vs = m.top.getScene().videoscreen
  port = CreateObject("roMessagePort")

  video.unobserveFieldScoped("position")
  video.unobserveFieldScoped("state")
  if video.state = "playing" or video.state = "buffering" then video.control = "stop"
  video.observeFieldScoped("state", port)
  video.control = "play"

  while true
    msg = wait(0, port)
    if type(msg) = "roSGNodeEvent"
      if msg.GetField() = "state"
        curState = msg.GetData()
'        Dbg("PlayerTask: state = ", curState)
        vs.isActive = curState = "playing" or curState = "paused" or curState = "buffering"
        if curState = "stopped" or curState = "finished" or curState = "error"
          video.visible = false
          vs.visible = false
          exit while
        else if curState = "playing"
          video.visible = true
          vs.visible = true
        else
          video.visible = false
          vs.visible = false
        end if
      end if
    end if
  end while
'  Dbg("end while")
  vs.isActive = false
end sub


sub findAndSetVideoContent()
  video = m.top.getScene().video
  content = m.top.content
  if content <> invalid
    i = findCurrentProgramIndex(content)
    program = content.getChild(i)
    m.top.programIndex = i
    setVideoContent(video, program)
    if program <> invalid then m.top.bookmark = nowTimestamp() - program.PLAYSTART
  end if
end sub


sub setVideoContent(video, program)
  if video <> invalid and program <> invalid
    if program.providerName = "vimeo"
      video.content = getVimeoItem(program)
    else if program.providerName = "custom"
      video.content = getCustomItem(program)
    end if
  end if
end sub


sub playContentWithAds()
  video = m.top.video
  video.loadingIndicatorOn = true
  video.visible = true
  if m.top.content <> invalid then findAndSetVideoContent()

  keepPlaying = video.content <> invalid and isnonemptystr(video.content.url)
  port = CreateObject("roMessagePort")

  contentRuntime = 0
  if isnonemptystr(m.global.config.vastTag)
    adPods = setupAds()
    if adPods <> invalid and m.adIface <> invalid
      if video.state = "playing" or video.state = "buffering" then video.control = "stop"
      m.top.adPlaying = true
      keepPlaying = m.adIface.showAds(adPods, invalid, video.getParent())
      m.top.adPlaying = false
    end if
  end if

  if keepPlaying
'    video.unobserveFieldScoped("position")
    video.unobserveFieldScoped("state")
'    video.observeFieldScoped("position", port)
    video.observeFieldScoped("state", port)
    if m.top.bookmark > 0 then video.seek = m.top.bookmark
'    video.visible = true
'    video.loadingIndicatorOn = false
    video.control = "play"
'    video.setFocus(true) 'so we can handle a Back key interruption
  end if

  curPos% = 0
  playbackSecondsCounter = 0
  lastReportedPosision = 0
  minBookmarkVideoLength = m.global.config.minBookmarkVideoLength
  adPods = invalid
  isPlayingPostroll = false
  m.top.adType = "midroll"
  while keepPlaying
    msg = wait(0, port)
    if type(msg) = "roSGNodeEvent"
      if msg.GetField() = "position"
        playbackSecondsCounter++
        ' keep track of where we reached in content
        curPos% = msg.GetData()
'        duration = video.duration
        if isnonemptystr(m.global.config.vastTag) and m.adIface <> invalid then adPods = m.adIface.getAds(msg)
        ' check for mid-roll ads
        if adPods <> invalid
          'ask the video to stop - the rest is handled in the state=stopped event below
          video.control = "stop"
        end if
      else if msg.GetField() = "state"
        curState = msg.GetData()
        Dbg("curState", curState)
        if curState = "stopped"
          Dbg("video.visible", video.visible)
          if not video.visible then exit while
          bookmark = invalid
          if adPods <> invalid and m.adIface <> invalid
            m.top.adPlaying = true
            keepPlaying = m.adIface.showAds(adPods, invalid, video.getParent())
            bookmark = 0
            m.top.adPlaying = false
            m.top.bookmark = curPos%
          else
            findAndSetVideoContent()
            keepPlaying = video.content <> invalid and isnonemptystr(video.content.url)
          end if
          Dbg("keepPlaying", keepPlaying)
          if m.top.bookmark > 0 then video.seek = m.top.bookmark
          if keepPlaying then video.control = "play"
          bookmark = invalid
        else if curState = "finished"
          m.top.programIndex++
          program = m.top.content.getChild(m.top.programIndex)
          setVideoContent(video, program)
          m.top.bookmark = 0
          if isnonemptystr(m.global.config.vastTag)
            adPods = setupAds()
            if adPods <> invalid and m.adIface <> invalid
              m.top.adPlaying = true
              keepPlaying = m.adIface.showAds(adPods, invalid, video.getParent())
              m.top.adPlaying = false
            end if
          end if
          keepPlaying = video.content <> invalid and isnonemptystr(video.content.url)
          if keepPlaying then video.control = "play"
        else if curState = "buffering"
          video.loadingIndicatorOn = true
        else if curState = "playing"
          video.visible = true
          video.loadingIndicatorOn = false
          m.top.keepPlaying = true
          channelId = regRead("channelId", constants().regSection)
          if channelId = invalid or channelId <> m.top.content.id then regWrite("channelId", m.top.content.id, constants().regSection)
        else if curState = "error"
'          showMessage({ title: "The broadcast should start on [date] at [time]", buttons: ["OK"]})
          showMessage({ title: "The broadcast was not started.", buttons: ["OK"]})
          exit while
        end if
      end if
    end if
  end while
  m.top.content = invalid
  video.visible = false
  m.top.keepPlaying = false
end sub


Sub initAds()
  m.adIface = Roku_Ads()
  m.adIface.setDebugOutput(false)
  m.adIface.enableNielsenDAR(true)
  m.adIface.setNielsenAppId("P2871BBFF-1A28-44AA-AF68-C7DE4B148C32")
  m.adIface.setNielsenGenre("GV")
  m.adIface.enableAdMeasurements(true)
  m.adIface.setAdExit(false)
end sub


Sub setupAdsContent()
  Dbg("setupAdsContent")
  initAds()
  video = m.top.video.content
  if video <> invalid
    m.adIface.setNielsenProgramId(video.Title)
    m.adIface.setContentId(video.Title)
    m.adIface.setContentGenre(video.categories)
    m.adIface.setContentLength(int(video.length))
  
    vTitle = video.Title.left(30)
    vTitle = vTitle.Replace(Chr(34),"").Replace("  "," ")
    Dbg(vTitle)
  end if
  Dbg("m.global.config.vastTag", m.global.config.vastTag)
  vastTag = m.global.config.vastTag.Replace("RokuDevAccount", CreateObject("roAppInfo").GetTitle().escape())
  if CreateObject("roDeviceInfo").IsRIDADisabled()
    vastTag = m.global.config.vastTag.Replace("ROKU_ADS_LIMIT_TRACKING", "1").Replace("ROKU_ADS_TRACKING_ID", m.global.appRandomUUID)
  else
    vastTag = m.global.config.vastTag
  end if
  m.adIface.setAdUrl(vastTag)
end sub


Function setupAds()
  setupAdsContent()
  return m.adIface.getAds()
End Function
