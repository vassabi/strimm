(this["webpackJsonpstrimm.player"]=this["webpackJsonpstrimm.player"]||[]).push([[0],{54:function(n,e,t){},55:function(n,e,t){"use strict";t.r(e);var o=t(0),r=t.n(o),a=t(19),i=t.n(a),c=t(7),s=t(6),l=t(20),u=t.n(l),b=t(13),p=t.n(b),d=t(9),g=t.n(d),x=t(24),f=t(1);function m(n){var e=new Date(1e3*n),t=e.getUTCHours(),o=e.getUTCMinutes(),r=h(e.getUTCSeconds());return t?"".concat(t,":").concat(h(o),":").concat(r):"".concat(o,":").concat(r)}function h(n){return"0".concat(n).slice(-2)}function j(n){var e=n.seconds;return Object(f.jsx)("time",{dateTime:"P".concat(Math.round(e),"S"),children:m(e)})}var y,v,k,w,O,z,M,S,A,E,P,F,R,T,C,I,D=Object(o.memo)(j),L=t(2),Y=t(3),_=Y.b.div(y||(y=Object(L.a)(["\n  position: absolute;\n  z-index: 1;\n  top: 0;\n  left: 0;\n  width: 100%;\n  height: 100%;\n  background-image: none;\n  text-align: center;\n  cursor: pointer;\n  font-size: 16px;\n  color: #fff;\n\n  ",";\n"])),(function(n){return n.isPaused||n.isActive?"\n    background-image: linear-gradient(to top, rgba(0, 0, 0, 0.25), transparent 15%);\n\n    ".concat(B," {\n      opacity: 1;\n    }\n  "):null})),U=Y.b.div(v||(v=Object(L.a)(["\n  position: absolute;\n  top: 0;\n  left: 0;\n  width: 100%;\n  height: 100%;\n  z-index: 1;\n\n  &::before {\n    font-family: flowplayer;\n    position: absolute;\n    top: 50%;\n    left: 50%;\n    transform: translate(-50%, -50%);\n    font-size: 14vw;\n    transition: opacity 0.1s;\n    opacity: 0;\n\n    @media (min-width: 768px) {\n      font-size: 107px;\n    }\n  }\n\n  ","\n"])),(function(n){return n.playing?"\n        &::before {\n          content: '\\e008';\n        }\n      ":"\n        &::before {\n          content: '\\e007';\n        }\n      "})),B=Y.b.div(k||(k=Object(L.a)(["\n  position: absolute;\n  bottom: 0;\n  width: 100%;\n  display: flex;\n  justify-content: center;\n  align-items: center;\n  height: 3em;\n  z-index: 2;\n  padding-left: 0.3em;\n  padding-right: 2.5em;\n  transition: background-image 0.1s, opacity 0.1s;\n  opacity: 0;\n"]))),H=Y.b.a(w||(w=Object(L.a)(["\n  margin: 0 0.5em;\n  flex: 0 0 auto;\n  display: inline-block;\n\n  ","\n"])),(function(n){return n.playing?"\n        &::before {\n          font-family: flowplayer;\n          font-size: 1.7em;\n          content: '\\e008';\n        }\n       ":"\n        &::before {\n        font-family: flowplayer;\n        font-size: 1.7em;\n        content: '\\e007';\n      }\n      "})),J=Y.b.span(O||(O=Object(L.a)(["\n  margin: 0 0.5em;\n  display: inline-block;\n  flex: 0 0 auto;\n  cursor: default;\n"]))),N=Y.b.div(z||(z=Object(L.a)(["\n  position: relative;\n  margin: 0 0.5em;\n  background-color: rgba(255, 255, 255, 0.5);\n  height: 0.9em;\n  border-radius: 0.24em;\n  flex: 1 1 auto;\n  transition: height 0.2s;\n"]))),q=Y.b.input(M||(M=Object(L.a)(["\n  position: absolute;\n  z-index: 1;\n  top: 0;\n  left: 0;\n  background-color: transparent;\n\n  &[type='range'] {\n    -webkit-appearance: none;\n    width: 100%;\n    margin: 0;\n  }\n  &[type='range']:focus {\n    outline: none;\n  }\n  &[type='range']::-webkit-slider-runnable-track {\n    width: 100%;\n    height: 15px;\n    box-shadow: 0px 0px 0.6px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n    background: rgba(255, 255, 255, 0);\n    border-radius: 0px;\n    border: 0px solid rgba(255, 255, 255, 0);\n    cursor: col-resize;\n  }\n  &[type='range']::-webkit-slider-thumb {\n    box-shadow: 0px 0px 0px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n    border: 0px solid rgba(255, 255, 255, 0);\n    height: 20px;\n    width: 20px;\n    border-radius: 20px;\n    background: rgba(255, 255, 255, 0);\n    cursor: col-resize;\n    -webkit-appearance: none;\n    margin-top: -2.5px;\n  }\n  &[type='range']:focus::-webkit-slider-runnable-track {\n    background: rgba(255, 255, 255, 0);\n  }\n  &[type='range']::-moz-range-track {\n    width: 100%;\n    height: 15px;\n    box-shadow: 0px 0px 0.6px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n    background: rgba(255, 255, 255, 0);\n    border-radius: 0px;\n    border: 0px solid rgba(255, 255, 255, 0);\n    cursor: col-resize;\n  }\n  &[type='range']::-moz-range-thumb {\n    box-shadow: 0px 0px 0px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n    border: 0px solid rgba(255, 255, 255, 0);\n    height: 20px;\n    width: 20px;\n    border-radius: 20px;\n    background: rgba(255, 255, 255, 0);\n    cursor: col-resize;\n  }\n  &[type='range']::-ms-track {\n    width: 100%;\n    height: 15px;\n    cursor: col-resize;\n    background: transparent;\n    border-color: transparent;\n    color: transparent;\n  }\n  &[type='range']::-ms-fill-lower {\n    background: rgba(242, 242, 242, 0);\n    border: 0px solid rgba(255, 255, 255, 0);\n    border-radius: 0px;\n    box-shadow: 0px 0px 0.6px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n  }\n  &[type='range']::-ms-fill-upper {\n    background: rgba(255, 255, 255, 0);\n    border: 0px solid rgba(255, 255, 255, 0);\n    border-radius: 0px;\n    box-shadow: 0px 0px 0.6px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n  }\n  &[type='range']::-ms-thumb {\n    box-shadow: 0px 0px 0px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n    border: 0px solid rgba(255, 255, 255, 0);\n    width: 20px;\n    height: 15px;\n    border-radius: 20px;\n    background: rgba(255, 255, 255, 0);\n    cursor: col-resize;\n  }\n  &[type='range']:focus::-ms-fill-lower {\n    background: rgba(255, 255, 255, 0);\n  }\n  &[type='range']:focus::-ms-fill-upper {\n    background: rgba(255, 255, 255, 0);\n  }\n"]))),G=Y.b.div(S||(S=Object(L.a)(["\n  position: absolute;\n  left: 0;\n  height: 100%;\n  max-width: 100%;\n  background-color: rgba(255, 255, 255, 0.6);\n  border-radius: 0.24em;\n  transition: width 0.25s linear;\n"]))),K=Y.b.span(A||(A=Object(L.a)(["\n  position: absolute;\n  background-color: rgba(0, 0, 0, 0.65);\n  display: none;\n  border-radius: 0.2em;\n  padding: 0.1em 0.3em;\n  font-size: 90%;\n  bottom: 1.4em;\n  height: auto;\n"]))),Q=Y.b.div(E||(E=Object(L.a)(["\n  position: absolute;\n  height: 100%;\n  max-width: 100%;\n  border-radius: 0.24em;\n  background-color: #00abcd;\n  fill: rgba(0, 0, 0, 0.2);\n  transition: width 0.25s linear;\n"]))),V=Y.b.a(P||(P=Object(L.a)(["\n  margin: 0 0.5em;\n  display: inline-block;\n  flex: 0 0 auto;\n\n  ","\n"])),(function(n){return n.muted?"\n  &::before {\n    font-family: flowplayer;\n    font-size: 1.7em;\n    content: '\\e00d';\n  }\n   ":"\n    &::before {\n    font-family: flowplayer;\n    font-size: 1.7em;\n    content: '\\e00b';\n  }\n  "})),W=Y.b.div(F||(F=Object(L.a)(["\n  margin: 0 0.5em;\n  flex: 0 0 auto;\n  position: relative;\n"]))),X=Y.b.input(R||(R=Object(L.a)(["\n  position: absolute;\n  z-index: 1;\n  top: 0;\n  left: 0;\n  background-color: transparent;\n\n  &[type='range'] {\n    -webkit-appearance: none;\n    width: 100%;\n    margin: 0;\n  }\n  &[type='range']:focus {\n    outline: none;\n  }\n  &[type='range']::-webkit-slider-runnable-track {\n    width: 100%;\n    height: 15px;\n    box-shadow: 0px 0px 0.6px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n    background: rgba(255, 255, 255, 0);\n    border-radius: 0px;\n    border: 0px solid rgba(255, 255, 255, 0);\n    cursor: col-resize;\n  }\n  &[type='range']::-webkit-slider-thumb {\n    box-shadow: 0px 0px 0px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n    border: 0px solid rgba(255, 255, 255, 0);\n    height: 1px;\n    width: 1px;\n    border-radius: 20px;\n    background: rgba(255, 255, 255, 0);\n    cursor: col-resize;\n    -webkit-appearance: none;\n    margin-top: -2.5px;\n  }\n  &[type='range']:focus::-webkit-slider-runnable-track {\n    background: rgba(255, 255, 255, 0);\n  }\n  &[type='range']::-moz-range-track {\n    width: 100%;\n    height: 15px;\n    box-shadow: 0px 0px 0.6px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n    background: rgba(255, 255, 255, 0);\n    border-radius: 0px;\n    border: 0px solid rgba(255, 255, 255, 0);\n    cursor: col-resize;\n  }\n  &[type='range']::-moz-range-thumb {\n    box-shadow: 0px 0px 0px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n    border: 0px solid rgba(255, 255, 255, 0);\n    height: 1px;\n    width: 1px;\n    border-radius: 20px;\n    background: rgba(255, 255, 255, 0);\n    cursor: col-resize;\n  }\n  &[type='range']::-ms-track {\n    width: 100%;\n    height: 15px;\n    cursor: col-resize;\n    background: transparent;\n    border-color: transparent;\n    color: transparent;\n  }\n  &[type='range']::-ms-fill-lower {\n    background: rgba(242, 242, 242, 0);\n    border: 0px solid rgba(255, 255, 255, 0);\n    border-radius: 0px;\n    box-shadow: 0px 0px 0.6px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n  }\n  &[type='range']::-ms-fill-upper {\n    background: rgba(255, 255, 255, 0);\n    border: 0px solid rgba(255, 255, 255, 0);\n    border-radius: 0px;\n    box-shadow: 0px 0px 0.6px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n  }\n  &[type='range']::-ms-thumb {\n    box-shadow: 0px 0px 0px rgba(255, 255, 255, 0),\n      0px 0px 0px rgba(255, 255, 255, 0);\n    border: 0px solid rgba(255, 255, 255, 0);\n    width: 1px;\n    height: 1px;\n    border-radius: 20px;\n    background: rgba(255, 255, 255, 0);\n    cursor: col-resize;\n  }\n  &[type='range']:focus::-ms-fill-lower {\n    background: rgba(255, 255, 255, 0);\n  }\n  &[type='range']:focus::-ms-fill-upper {\n    background: rgba(255, 255, 255, 0);\n  }\n"]))),Z=Y.b.div(T||(T=Object(L.a)(["\n  flex: 1;\n  position: relative;\n  cursor: col-resize;\n  height: 0.9em;\n  border-radius: 0.24em;\n  background-color: transparent;\n  user-select: none;\n  transition: height 0.2s;\n  white-space: nowrap;\n"]))),$=Y.b.em(C||(C=Object(L.a)(["\n  border-radius: 2px;\n  display: inline-block;\n  width: 5px;\n  height: 100%;\n  position: relative;\n  vertical-align: top;\n  transition: transform 0.4s;\n  transform-origin: bottom;\n  user-select: none;\n  transform: scale(1.1);\n  ","\n\n  & + & {\n    margin-left: 3px;\n  }\n\n  &:hover {\n    transform: scaleY(1.35);\n    transition: transform 0.2s;\n  }\n"])),(function(n){return n.isActive?"\n    background-color: #00abcd;\n    fill: rgba(0, 0, 0, 0.2);\n    ":"\n    background-color: rgba(255, 255, 255, 0.75);\n    "})),nn=Y.b.a(I||(I=Object(L.a)(["\n  position: absolute;\n  z-index: 1;\n  top: 50%;\n  transform: translateY(-50%);\n  right: 0;\n  padding: 0 0.7em 0 0.3em;\n\n  &::before {\n    font-family: flowplayer;\n    font-size: 1.45em;\n    content: '\\e002';\n  }\n"],["\n  position: absolute;\n  z-index: 1;\n  top: 50%;\n  transform: translateY(-50%);\n  right: 0;\n  padding: 0 0.7em 0 0.3em;\n\n  &::before {\n    font-family: flowplayer;\n    font-size: 1.45em;\n    content: '\\\\e002';\n  }\n"]))),en={duration:0,played:0,loaded:0,volume:0,playing:!1,controlsEnabled:!0,muted:!1,onFullscreen:function(){},onPlayPause:function(){},seekMouseDown:function(){},seekChange:function(){},seekMouseUp:function(){},toggleMute:function(){},volumeChange:function(){}},tn=[{id:0,isActive:!1},{id:1,isActive:!1},{id:2,isActive:!1},{id:3,isActive:!1},{id:4,isActive:!1},{id:5,isActive:!1},{id:6,isActive:!1}],on=100/tn.length;function rn(n){var e=n.duration,t=n.played,r=n.loaded,a=n.volume,i=n.playing,l=n.controlsEnabled,u=n.muted,b=n.onFullscreen,p=n.onPlayPause,d=n.seekMouseDown,g=n.seekChange,m=n.seekMouseUp,h=n.toggleMute,j=n.volumeChange,y=Object(o.useState)(tn),v=Object(c.a)(y,2),k=v[0],w=v[1],O=Object(o.useState)(!0),z=Object(c.a)(O,2),M=z[0],S=z[1];Object(o.useEffect)((function(){var n=Object(x.a)(k);n=n.map((function(n,e){return Object(s.a)(Object(s.a)({},n),{},{isActive:on*e<100*a})})),w(n)}),[a]),Object(o.useEffect)((function(){var n;return M&&(n=setTimeout((function(){S(!1)}),5e3)),function(){return clearTimeout(n)}}),[M]);return Object(f.jsxs)(_,{isPaused:!i,isActive:M,onMouseMove:function(){S(!0)},onMouseLeave:function(){S(!1)},children:[Object(f.jsx)(U,{playing:i,onClick:function(){p()}}),Object(f.jsxs)(B,{children:[l?Object(f.jsx)(H,{playing:i,onClick:p}):null,l?Object(f.jsx)(J,{children:Object(f.jsx)(D,{seconds:e*t})}):null,l?Object(f.jsxs)(N,{children:[Object(f.jsx)(q,{type:"range",min:0,max:1,step:"any",value:t,onMouseDown:d,onChange:g,onMouseUp:m}),Object(f.jsx)(G,{style:{width:"".concat(100*r,"%")}}),Object(f.jsx)(K,{}),Object(f.jsx)(Q,{style:{width:"".concat(100*t,"%")}})]}):null,Object(f.jsx)(V,{muted:u,onClick:h}),Object(f.jsxs)(W,{children:[Object(f.jsx)(X,{type:"range",min:0,max:1,step:"any",value:a,onChange:j}),Object(f.jsx)(Z,{children:k&&k.map((function(n){return Object(f.jsx)($,{isActive:n.isActive},n.id)}))})]}),Object(f.jsx)(nn,{onClick:function(){b()}})]})]})}rn.defaultProps=en;var an,cn=Object(o.memo)(rn),sn=Y.b.div(an||(an=Object(L.a)(["\n  position: relative;\n  background-color: black;\n  padding-bottom: ",";\n\n  ","\n\n  ","\n\n  & > .react-player {\n    position: absolute;\n    z-index: 0;\n    top: 0;\n    left: 0;\n  }\n"])),"".concat(56.25,"%"),(function(n){return n.screenFullActive?"\n    position: static;\n    padding-bottom: 0;\n  ":null}),(function(n){return n.fullscreenFallbackEnabled?"\n    position: fixed;\n    padding-bottom: 0;\n    top: 0%;\n    left: 0;\n    width: 100vw;\n    height: 100vh;\n    z-index: 13;\n    background-color: black;\n  ":null})),ln=(t(54),{url:"",pip:!0,playing:!0,controls:!1,light:!1,volume:0,muted:!0,played:0,loaded:0,loop:!1,duration:0,playbackRate:1,playsinline:!0}),un={source:null,startDate:null};function bn(n){var e=n.source,t=n.startDate,r=Object(o.useState)(ln),a=Object(c.a)(r,2),i=a[0],l=a[1],b=Object(o.useState)(),d=Object(c.a)(b,2),x=d[0],m=d[1],h=Object(o.useState)(!1),j=Object(c.a)(h,2),y=j[0],v=j[1],k=Object(o.useRef)(null);Object(o.useEffect)((function(){x&&w()}),[x]),Object(o.useEffect)((function(){O({url:e&&e.length>0?e:void 0})}),[e]);var w=function(){var n=localStorage.getItem("volume"),e=Object(s.a)(Object(s.a)({},i),{},{volume:null!==n&&0!==parseFloat(n)?parseFloat(n):.7});l(e)},O=function(n){var e=Object(s.a)(Object(s.a)({},i),n);l(e)},z=function(){var n=localStorage.getItem("volume"),e=null!==n&&0!==parseFloat(n)?parseFloat(n):.7;localStorage.setItem("volume",e.toString()),O({muted:!i.muted,volume:i.muted?e:0})};return Object(f.jsxs)(sn,{screenFullActive:"isFullscreen"in g.a&&g.a.isFullscreen,fullscreenFallbackEnabled:y,ref:k,children:[Object(f.jsx)(u.a,Object(s.a)({ref:function(n){m(n)},className:"react-player",width:"100%",height:"100%",onReady:function(){},onStart:function(){if(t&&x){var n=p()(),e=p()(t),o=n.diff(e,"seconds");x.seekTo(o,"seconds")}},onPlay:function(){O({playing:!0})},onPause:function(){O({playing:!1})},onBuffer:function(){return console.log("onBuffer")},onBufferEnd:function(){return console.log("onBufferEnd")},onSeek:function(){},onEnded:function(){},onError:function(n){return console.log("onError",n)},onProgress:function(n){i.seeking||O(Object(s.a)({loaded:n.loaded,loadedSeconds:n.loadedSeconds,played:n.played,playedSeconds:n.playedSeconds},{}))},onDuration:function(n){O({duration:n})}},i)),i.controls?null:Object(f.jsx)(cn,{duration:i.duration,playing:i.playing,played:i.played,loaded:i.loaded,muted:i.muted,volume:i.volume,onFullscreen:function(){k&&k.current&&(g.a.isEnabled?g.a.toggle(k.current):v(!y))},onPlayPause:function(){O({playing:!i.playing})},seekMouseDown:function(){O({seeking:!0})},seekChange:function(n){O({played:parseFloat(n.target.value)})},seekMouseUp:function(n){var e=n.target;O({seeking:!1}),x&&x.seekTo(parseFloat(e.value))},toggleMute:z,volumeChange:function(n){var e=parseFloat(n.target.value);localStorage.setItem("volume",n.target.value),O({muted:!e,volume:e})}})]})}bn.defaultProps=un;var pn,dn=bn,gn=Object(Y.a)(pn||(pn=Object(L.a)(["\n  #STRIMM_PLAYER_ROOT {\n    font-family: Roboto-Regular, 'Helvetica Neue', Helvetica, Arial, sans-serif;\n\t  -webkit-overflow-scrolling: touch;\n    -webkit-font-smoothing: antialiased;\n    -moz-osx-font-smoothing: grayscale;\n    box-sizing: border-box;\n    \n    & *, & *::before, & *::after {\n      box-sizing: border-box;\n    }\n  }\n"])));var xn=function(n){return null!=n&&"object"===typeof n};var fn=function n(e,t){if(!e||!t)return!1;var o=Object.keys(e),r=Object.keys(t);if(o.length!==r.length)return!1;for(var a=0,i=o;a<i.length;a++){var c=i[a],s=e[c],l=t[c],u=xn(s)&&xn(l);if(u&&!n(s,l)||!u&&s!==l)return!1}return!0},mn={url:null,startDate:null};function hn(){var n=Object(o.useState)(mn),e=Object(c.a)(n,2),t=e[0],r=e[1];return Object(o.useEffect)((function(){var n=setInterval((function(){"STRIMM_PLAYER"in window&&"url"in window.STRIMM_PLAYER&&"startDate"in window.STRIMM_PLAYER&&!fn(t,window.STRIMM_PLAYER)&&r(window.STRIMM_PLAYER)}),1e3);return function(){clearInterval(n)}}),[t]),Object(f.jsxs)(f.Fragment,{children:[Object(f.jsx)(dn,{source:t.url,startDate:t.startDate}),Object(f.jsx)(gn,{})]})}var jn=Object(o.memo)(hn),yn=function(n){n&&n instanceof Function&&t.e(3).then(t.bind(null,56)).then((function(e){var t=e.getCLS,o=e.getFID,r=e.getFCP,a=e.getLCP,i=e.getTTFB;t(n),o(n),r(n),a(n),i(n)}))};i.a.render(Object(f.jsx)(r.a.StrictMode,{children:Object(f.jsx)(jn,{})}),document.getElementById("STRIMM_PLAYER_ROOT")),yn()}},[[55,1,2]]]);
//# sourceMappingURL=main.4514192c.chunk.js.map