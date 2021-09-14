sub Main(args)
  screen = CreateObject("roSGScreen")
  port = CreateObject("roMessagePort")
  screen.setMessagePort(port)
  input = CreateObject("roInput")
  input.SetMessagePort(port)
  scene = screen.CreateScene("MainScene")
  scene.deepLinking = getDeepLinking(args)
  screen.show()

  scene.apprunning = true
  scene.observeField("apprunning", port)
  scene.signalBeacon("AppLaunchComplete")

  while scene.apprunning
    msg = wait(0, port)
    msgType = type(msg)
    if msgType = "roSGScreenEvent"
      if msg.isScreenClosed() then exit while
    else if msgType = "roSGNodeEvent"
      if msg.GetField() = "apprunning" then exit while
    else if msgType = "roDeviceInfoEvent"
      ?"roDeviceInfoEvent " msg.GetInfo()
    else if msgType = "roInputEvent"
      if msg.IsInput()
        info = msg.GetInfo()
        scene.deepLinking = getDeepLinking(info)
      end if
    end if
  end while
  ?"EXIT"
end sub


Function getDeepLinking(args)
  if isnonemptystr(args.contentID) and isnonemptystr(args.mediaType)
    isApplicableMediaType = args.mediaType = "live"
    isNotApplicableMediaType = args.mediaType = "movie" or args.mediaType = "episode" or args.mediaType = "series" or args.mediaType = "season"
    isNotApplicableMediaType = isNotApplicableMediaType or args.mediaType = "shortFormVideo" or args.mediaType = "special"
    if isApplicableMediaType
      ? "DeepLinking Content Id: " args.contentID " mediaType: " args.mediaType
      return args
    else if isNotApplicableMediaType
      ? "DeepLinking non-supported mediaType: " args.mediaType
    else
      ? "DeepLinking wrong mediaType: " args.mediaType
    end if
  end if
  return invalid
end Function
