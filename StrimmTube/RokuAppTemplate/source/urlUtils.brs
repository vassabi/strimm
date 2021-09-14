REM ******************************************************
REM Constucts a URL Transfer object
REM ******************************************************
Function createURLTransferObject(url As String, contentHeader="application/x-www-form-urlencoded" As String) as Object
  obj = CreateObject("roUrlTransfer")
  obj.SetPort(CreateObject("roMessagePort"))
  obj.SetUrl(url)
  obj.EnableEncodings(true)
  obj.AddHeader("Content-Type", contentHeader)
  if m.global.config <> invalid and isnonemptystr(m.global.config.authorizationBearer) then obj.AddHeader("Authorization", "Bearer " + m.global.config.authorizationBearer)
  if m.global.user <> invalid and m.global.user.isLoggedIn then obj.AddHeader("X-Auth-Token", m.global.user.token)
  if LCase(Left(url, 8)) = "https://"
    obj.SetCertificatesFile("common:/certs/ca-bundle.crt")
'    obj.EnablePeerVerification(false)
    obj.AddHeader("X-Roku-Reserved-Dev-Id", "")
    obj.InitClientCertificates()
  end if
  return obj
End Function


REM ******************************************************
REM Url Query builder
REM ******************************************************
Function NewHttp(url As String, contentHeader="application/json" As String) as Object
  obj = CreateObject("roAssociativeArray")
  obj.contentHeader               = contentHeader
  obj.http                        = createURLTransferObject(url, obj.contentHeader)
  obj.method                      = "GET"
  obj.HTTP_TIMEOUT                = 60
  obj.useCookies                  = false
  obj.saveCookies                 = false
  obj.retainBodyOnError           = false
'  obj.FirstParam                  = true
  obj.params                      = {}
  obj.addParamsToRequest          = add_params_to_request
  obj.addParam                    = http_add_param
'  obj.AddRawQuery                 = http_add_raw_query
  obj.getToStringWithRetry        = http_get_to_string_with_retry
'  obj.PrepareUrlForQuery          = http_prepare_url_for_query
  obj.getToStringWithTimeout      = http_get_to_string_with_timeout
  obj.postFromStringWithTimeout   = http_post_from_string_with_timeout
  obj.handleResponseEvent         = function(event as Object) as String
                                      m.responseCode = event.GetResponseCode()
                                      m.isSuccess = (m.responseCode >= 200 and m.responseCode < 300)
                                      return event.GetString()
                                    end function

  obj.request                     = function()
                                      m.http.retainBodyOnError(m.retainBodyOnError)
                                      m.addParamsToRequest()
                                      if m.useCookies
                                        cookie = restoreCookies()
                                        if cookie <> invalid then m.http.AddHeader("Cookie", cookie)
                                      end if
                                      if m.method = "GET"
                                        return m.getToStringWithTimeout(m.HTTP_TIMEOUT)
                                      else
                                        m.http.setRequest(UCase(m.method))
                                        return m.postFromStringWithTimeout(m.body, m.HTTP_TIMEOUT)
                                      end if
                                      return invalid
                                    end function

'  if Instr(1, url, "?") > 0 then obj.FirstParam = false

  return obj
End Function


REM ******************************************************
REM HttpEncode - just encode a string
REM ******************************************************
Function add_params_to_request()
  m.body = ""
  if isNonEmptyAA(m.params)
    if m.method = "GET" or (m.method = "POST" and m.contentHeader = "application/x-www-form-urlencoded")
      bodyArray = []
      for each key in m.params.keys()
        if isArray(m.params[key])
          for each item in m.params[key]
            bodyArray.push(key.Escape() + "=" + evalString(item).Escape())
          end for
        else
          bodyArray.push(key.Escape() + "=" + evalString(m.params[key]).Escape())
        end if
      end for
      m.body = bodyArray.join("&")
      if m.body <> "" 'and m.method = "GET"
        url = m.http.GetUrl()
        if url.Instr("?") > 0
          url += "&" + m.body
        else
          url += "?" + m.body
        end if
        m.http.SetUrl(url)
      end if
      if m.method = "POST" then m.body = ""
    else if m.method = "POST" and m.contentHeader = "application/json"
      m.body = FormatJson(m.params)
    end if
  end if
End Function


REM ******************************************************
REM Percent encode a name/value parameter pair and add the
REM the query portion of the current url
REM Automatically add a '?' or '&' as necessary
REM Prevent duplicate parameters
REM ******************************************************
Function http_add_param(name As String, val As String) as Void
  m.params[name] = val
End Function


REM ******************************************************
REM Performs Http.AsyncGetToString() in a retry loop
REM with exponential backoff. To the outside
REM world this appears as a synchronous API.
REM ******************************************************
Function http_get_to_string_with_retry() as String
  timeout%         = m.HTTP_TIMEOUT * 1000
  num_retries%     = 5

  str = ""
  while num_retries% > 0
    if m.http.AsyncGetToString()
      event = wait(timeout%, m.http.GetPort())
      if type(event) = "roUrlEvent"
        str = m.handleResponseEvent(event)
        if m.saveCookies then saveCookies(getCookies(event))
        exit while        
      else if event = invalid
        m.http.AsyncCancel()
        REM reset the connection on timeouts
        m.http = createURLTransferObject(m.http.GetUrl(), m.contentHeader)
        timeout% = 2 * timeout%
      else
        print "roUrlTransfer::AsyncGetToString(): unknown event"
      endif
    endif
    num_retries% = num_retries% - 1
  end while

  return str
End Function


REM ******************************************************
REM Performs Http.AsyncGetToString() with a single timeout in seconds
REM To the outside world this appears as a synchronous API.
REM ******************************************************
Function http_get_to_string_with_timeout(seconds as Integer) as String
  timeout% = 1000 * seconds

  timer = CreateObject("roTimespan")
  timer.Mark()
  str = ""
  m.http.EnableFreshConnection(true) 'Don't reuse existing connections
  if m.http.AsyncGetToString()
    event = wait(timeout%, m.http.GetPort())
    if type(event) = "roUrlEvent"
      Dbg("roUrlEvent received", timer.TotalMilliseconds())
      str = m.handleResponseEvent(event)
      if m.saveCookies then saveCookies(getCookies(event))
    else if event = invalid
      Dbg("AsyncGetToString timeout")
      m.http.AsyncCancel()
    else
      Dbg("AsyncGetToString unknown event", event)
    endif
  endif

  return str
End Function


REM ******************************************************
REM Performs Http.AsyncPostFromString() with a single timeout in seconds
REM To the outside world this appears as a synchronous API.
REM ******************************************************
Function http_post_from_string_with_timeout(val As String, seconds as Integer) as String
  timeout% = 1000 * seconds

  timer = CreateObject("roTimespan")
  timer.Mark()
  str = ""
'    m.http.EnableFreshConnection(true) 'Don't reuse existing connections
  if m.http.AsyncPostFromString(val)
    event = wait(timeout%, m.http.GetPort())
    if type(event) = "roUrlEvent"
      Dbg("roUrlEvent received", timer.TotalMilliseconds())
      str = m.handleResponseEvent(event)
      if m.saveCookies then saveCookies(getCookies(event))
    else if event = invalid
      Dbg("AsyncPostFromString timeout")
      m.http.AsyncCancel()
    else
      Dbg("AsyncPostFromString unknown event", event)
    endif
  endif

  return str
End Function

'return an array of cookies, from a response. These can be added to a roUrlTransfer using AddCookies()
'creating them as an roArray of roAssociativeArrays - as expected by AddCookies()
function getCookies(msg)
  cookies = []
  if type(msg) = "roUrlEvent"

    'search for any Set-Cookie headers
    responseHeaders = msg.GetResponseHeadersArray()
    for each responseHeader in responseHeaders
      if responseHeader["Set-Cookie"] <> invalid

        'responseHeader["Set-Cookie"] will be a string of the format "CookieName=CookieValue; Version=1; Domain="... etc
        'key value pairs separated by ;
        'uses regex to split into substrings of "Key=Value"
        cookie = responseHeader["Set-Cookie"].split(";")[0].trim()

        cookies.Push(cookie)
      end if
    end for
  end if
  return cookies
end function


function restoreCookies() as Object
  cookies = restoreFile("tmp:/cookies/cookies")
  if cookies <> invalid then return cookies.join("; ")
  return invalid
end function


function saveCookies(cookies) as Object
  if not isInvalid(cookies) then saveFile("cookies/cookies", cookies)
end function


function clearCookies() as Object
  fileSystem = CreateObject("roFileSystem")
  fileSystem.CreateDirectory("tmp:/cookies")
  if fileSystem.Exists("tmp:/cookies/cookies") then fileSystem.Delete("tmp:/cookies/cookies")
end function
