Function apiConstants()
  apiBase = "http://iframe-api.strimm.com:8081/api/v1/services/roku"
  apiBaseUsers = apiBase + "/users/"
  return  {
            settings: apiBase + "/settings/" + m.global.config.api_key
            channels: apiBaseUsers + evalString(m.global.config.user) + "/channels?limit=100"
            channel:  apiBaseUsers + evalString(m.global.config.user) + "/channels/"
            videos:   apiBaseUsers + evalString(m.global.config.user) + "/channels/{{channel}}/video"
            video:    apiBaseUsers + evalString(m.global.config.user) + "/channels/video"
          }
end Function


function apiRequest(url as String, params = {} as Object, method = "GET" as String) as Object
  http = NewHttp(url)
  http.Params = params
  http.Method = method
  response = http.Request()
  Dbg(method, http.http.getUrl())
  Dbg("Request params", params)
  Dbg("Request body", http.body)

  if response = "" then return invalid

  responseJson = ParseJson(response)
  Dbg("Response", responseJson)
  return responseJson
end function


function getRemoteSettings()
  resp = apiRequest(apiConstants().settings)
  if isnonemptyAA(resp)
    config = m.global.config
    if resp.UserId <> invalid then config.user = evalString(resp.UserId)
    if resp.AdLink <> invalid then config.vastTag = evalString(resp.AdLink)
    if resp.PrivacyPolicyLink <> invalid then config.privacyPolicy = evalString(resp.PrivacyPolicyLink)
    if resp.About <> invalid then config.About = evalString(resp.About)
    if resp.AppName <> invalid then config.AppName = evalString(resp.AppName)
    m.global.config = config
  end if
end function


sub epgTask()
  responseNode = invalid
  resp = apiRequest(apiConstants().channels)
  if isnonemptyAA(resp) and isnonemptyArray(resp.data)
    data = resp.data
    while resp.pagination <> invalid and resp.pagination.page < resp.pagination.lastPage
      resp = apiRequest(apiConstants().channels + "&page=" + (resp.pagination.page + 1).ToStr())
      if isnonemptyAA(resp) and isnonemptyArray(resp.data)
        data.Append(resp.data)
      else
        exit while
      end if
    end while
    favs = ParseJSON(regRead("favs", constants().regSection, "{}"))
    appendAAToNode({favs: favs}, m.global)
    favChannels = []
    channels = []
    for each item in data
      ch = channelParser(item)
      if favs[ch.id] <> invalid
        ch.isFavorite = true
        favChannels.unshift(ch)
      else
        channels.push(ch)
      end if
    end for
    channels.SortBy("numb")
    favChannels.Append(channels)
    responseNode = list2ContentNode(favChannels)
    params = m.top.params
    params.content = responseNode
    m.top.params = params
    getEpgTask()
  end if
  m.top.control = "DONE"
end sub


sub getEpgTask()
  Dbg("getEpgTask")
  if m.top.params <> invalid and m.top.params.content <> invalid
    content = m.top.params.content  '.clone(true)
    channelId = regRead("channelId", constants().regSection, content.getChild(0).id)
    channelIndex = 0
    channels = []
    channelsAA = {}
    for i = 0 to content.getChildCount() - 1
      id = content.getChild(i).id
      channels.push(id)
      channelsAA[id] = []
      if channelId = id then channelIndex = i
    end for
    resp = apiRequest(apiConstants().video, {channel_ids: channels.Join(","), dates: timestampToDates(m.top.params.timestamp)})
    if isnonemptyArray(resp)
      programsCount = resp.count()
      resp.sortBy("startTime")
      for each item in resp
        channelsAA[evalString(item.channelId)].Push(item)
      end for
      lastStartTime = isoDateToTimestamp(resp[resp.count() - 1].startDate)
      m.sumMinutes = 0
      for i = 0 to content.getChildCount() - 1
        channelId = channels[i]
        list = channelsAA[channelId]
        if list.count() > 0
          for each video in list
            program = programItemParser(video)
            content.getChild(i).appendChild(aAToNode(program))
            if lastStartTime < program.PLAYSTART then lastStartTime = program.PLAYSTART
          end for
        end if
      end for
      averageProgramDuration = int(m.sumMinutes / programsCount * 60) * 2
      timeGridDuration = 14400
      while averageProgramDuration < timeGridDuration / 2 and timeGridDuration > theme().minTimeGridDuration
        timeGridDuration -= theme().minTimeGridDuration
      end while
      appendAAToNode({  timestamp: m.top.params.timestamp
                        timeGridDuration: timeGridDuration
                        lastStartTime: utcToLocal(lastStartTime)
                        channelIndex: channelIndex
        }, content)
      m.top.responseNode = content
      Dbg("getEpgTask responseNode")
    end if
  end if
  Dbg("getEpgTask")
  m.top.control = "DONE"
end sub


Function channelParser(itemAA as Object) as Object
  itemAA.id = evalString(itemAA.id)
  itemAA.title = evalString(itemAA.name)
  itemAA.HDSMALLICONURL = evalString(itemAA.customLogo)
  itemAA.isFavorite = false
  itemAA.numb = 1000
  if itemAA.description <> invalid
    description = itemAA.description.trim()
    if description.left(1) = "["
      description = description.split("]")[0]
      itemAA.numb = description.right(description.len() - 1).trim().toInt()
    else if description.right(1) = "]"
      descriptionArray = description.split("[")
      if descriptionArray.count() > 0
        description = descriptionArray[descriptionArray.Count() - 1]
        itemAA.numb = description.left(description.len() - 1).trim().toInt()
      end if
    end if
  end if
  return itemAA
End Function


Function programItemParser(itemAA as Object) as Object
  m.sumMinutes += int(itemAA.duration / 60)
  playstart = isoDateToTimestamp(itemAA.startDate)
  playend = isoDateToTimestamp(itemAA.endDate)
  return {
    id              : evalString(itemAA.id)
    channelId       : evalString(itemAA.channelId)
    providerName    : LCase(evalString(itemAA.providerName))
    providerVideoId : evalString(itemAA.providerVideoId)
    videoTubeId     : evalString(itemAA.videoTubeId)
    description     : evalString(itemAA.description)
    title           : itemAA.title
    PLAYSTART       : playstart
    PLAYDURATION    : itemAA.duration
    PLAYEND         : playend
  }
End Function


function getVimeoItem(i)
  item = createObject("roSGNode", "ContentNode")
  item.id = i.providerVideoId
  item.title = i.title
  item.length = i.PLAYDURATION
  vresp = apiRequest("https://player.vimeo.com/video/" + i.providerVideoId + "/config")
  if vresp <> invalid and vresp.request <> invalid and vresp.request.files <> invalid and vresp.request.files.hls <> invalid
    hlsFiles = vresp.request.files.hls
    if hlsFiles.default_cdn <> invalid and hlsFiles.cdns <> invalid and hlsFiles.cdns[hlsFiles.default_cdn] <> invalid
      item.url = hlsFiles.cdns[hlsFiles.default_cdn].url
    else if hlsFiles.cdns <> invalid and hlsFiles.cdns.fastly_skyfire <> invalid
      item.url = hlsFiles.cdns.fastly_skyfire.url
    else if hlsFiles.cdns <> invalid and hlsFiles.cdns.level3 <> invalid
      item.url = hlsFiles.cdns.level3.url
    else if hlsFiles.cdns <> invalid and hlsFiles.cdns.akfire_interconnect_quic <> invalid
      item.url = hlsFiles.cdns.akfire_interconnect_quic.url
    end if
  end if
  item.streamformat = "hls"
  return item
end function


function getCustomItem(i)
  item = createObject("roSGNode", "ContentNode")
  item.id = i.id
  item.title = i.title
  item.url = i.providerVideoId
  if item.url.left(2) = "//" then item.url = "http:" + item.url
  item.streamformat = getStreamFormat(item.url)
  return item
end function


Function getCommonParams()
  di = CreateObject("roDeviceInfo")
  return  { client_tracking_id: getClientTrackingId(),
            app_name: CreateObject("roAppInfo").GetTitle(),
            model: di.GetModel(),
            device_display_name: di.GetModelDisplayName()
          }
end Function
