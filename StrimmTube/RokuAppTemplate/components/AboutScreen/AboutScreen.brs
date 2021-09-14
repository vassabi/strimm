sub Init()
  Dbg("[AboutScreen] Init")
  mBind(["version", "appTitle", "ppLabel", "aboutText"])
  m.top.focusable = true
  m.version.text = "v." + CreateObject("roAppInfo").GetVersion()
  m.appTitle.text = CreateObject("roAppInfo").GetTitle()
  m.aboutText.text = m.global.config.about
  if isnonemptystr(m.global.config.privacyPolicy) then m.ppLabel.text = "Privacy Policy: " + m.global.config.privacyPolicy
  onLayoutChange(theme().AboutScreen)
  m.top.observeField("focusedChild", "onFocusedChildChange")
end sub


sub onFocusedChildChange()
  if m.top.hasFocus()
    Dbg("onFocusedChildChange", m.top.hasFocus())
  end if
  m.top.visible = m.top.isInFocusChain()
end sub


function onKeyEvent(key as String, press as Boolean) as Boolean
  handled = false
  if press
    Dbg(">>> SearchScreen >> OnkeyEvent key: " + key + " >> pressed down: ", press)
    if key = "back"
      handled = goBack()
    end if
  end if
  return handled 
end function
