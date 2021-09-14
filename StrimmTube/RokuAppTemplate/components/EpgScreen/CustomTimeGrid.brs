function onKeyEvent(key as String, press as Boolean) as Boolean
  handled = false
  if press
    if key = "up"
      handled = m.top.channelFocused = 0
      if handled
        m.top.isEscaping = true
'      else
'        m.top.indexItemFocused--
      end if
'    else if key = "down"
'      m.top.indexItemFocused++
    end if

    if not handled and m.top.isEscaping then m.top.isEscaping = false
  end if
  return handled
end function
