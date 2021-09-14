<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="TestReactPlayer.aspx.cs" Inherits="StrimmTube.TestReactPlayer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="canonicalHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    <link href="/css/dragula.css" rel="stylesheet" />
    <link href="/reactplayer/css/2.679831fc.chunk.css" rel="stylesheet">
    <link href="/reactplayer/css/main.317ac729.chunk.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="playerBoxReactplayer" id="reactPlayerBox">
        <div id="STRIMM_PLAYER_ROOT">
        </div>
        <a id="close_x" class="close close_x closePlayerBox" href="#"></a>
    </div>
    <br />
    <a onclick="play();">PLAY VIDEO</a>
    <br />
    <script>
        $("#reactPlayerBox").hide();
        function play() {

            $('.playerBoxReactplayer').lightbox_me({
                centered: true,
                onLoad: function () {
                    window.STRIMM_PLAYER = {
                        url: '//bitmovin-a.akamaihd.net/content/playhouse-vr/m3u8s/105560.m3u8',
                        startDate: new Date(),
                    }
                },
                overlayCSS: {
                    background: 'black',
                    opacity: .8
                },
                onClose: function () {
                    $("#STRIMM_PLAYER_ROOT").remove();
                    $(".playerBoxReactplayer").html("<div id='STRIMM_PLAYER_ROOT'></div> <a id='close_x' class='close close_x closePlayerBox' href='#'></a>");
                    RemoveOverlay();
                }
            });
        }
    </script>

    <script>!function (e) {
  function r(r) {
    for (var n, i, a = r[0], c = r[1], l = r[2], s = 0, p = []; s < a.length; s++) i = a[s], Object.prototype.hasOwnProperty.call(o, i) && o[i] && p.push(o[i][0]), o[i] = 0;
    for (n in c) Object.prototype.hasOwnProperty.call(c, n) && (e[n] = c[n]);
    for (f && f(r); p.length;) p.shift()();
    return u.push.apply(u, l || []), t()
  }

  function t() {
    for (var e, r = 0; r < u.length; r++) {
      for (var t = u[r], n = !0, a = 1; a < t.length; a++) {
        var c = t[a];
        0 !== o[c] && (n = !1)
      }
      n && (u.splice(r--, 1), e = i(i.s = t[0]))
    }
    return e
  }

  var n = {}, o = {1: 0}, u = [];

  function i(r) {
    if (n[r]) return n[r].exports;
    var t = n[r] = {i: r, l: !1, exports: {}};
    return e[r].call(t.exports, t, t.exports, i), t.l = !0, t.exports
  }

  i.e = function (e) {
    var r = [], t = o[e];
    if (0 !== t) if (t) r.push(t[2]); else {
      var n = new Promise((function (r, n) {
        t = o[e] = [r, n]
      }));
      r.push(t[2] = n);
      var u, a = document.createElement("script");
      a.charset = "utf-8", a.timeout = 120, i.nc && a.setAttribute("nonce", i.nc), a.src = function (e) {
        return i.p + "static/js/" + ({}[e] || e) + "." + {3: "06356254"}[e] + ".chunk.js"
      }(e);
      var c = new Error;
      u = function (r) {
        a.onerror = a.onload = null, clearTimeout(l);
        var t = o[e];
        if (0 !== t) {
          if (t) {
            var n = r && ("load" === r.type ? "missing" : r.type), u = r && r.target && r.target.src;
            c.message = "Loading chunk " + e + " failed.\n(" + n + ": " + u + ")", c.name = "ChunkLoadError", c.type = n, c.request = u, t[1](c)
          }
          o[e] = void 0
        }
      };
      var l = setTimeout((function () {
        u({type: "timeout", target: a})
      }), 12e4);
      a.onerror = a.onload = u, document.head.appendChild(a)
    }
    return Promise.all(r)
  }, i.m = e, i.c = n, i.d = function (e, r, t) {
    i.o(e, r) || Object.defineProperty(e, r, {enumerable: !0, get: t})
  }, i.r = function (e) {
    "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e, Symbol.toStringTag, {value: "Module"}), Object.defineProperty(e, "__esModule", {value: !0})
  }, i.t = function (e, r) {
    if (1 & r && (e = i(e)), 8 & r) return e;
    if (4 & r && "object" == typeof e && e && e.__esModule) return e;
    var t = Object.create(null);
    if (i.r(t), Object.defineProperty(t, "default", {
      enumerable: !0,
      value: e
    }), 2 & r && "string" != typeof e) for (var n in e) i.d(t, n, function (r) {
      return e[r]
    }.bind(null, n));
    return t
  }, i.n = function (e) {
    var r = e && e.__esModule ? function () {
      return e.default
    } : function () {
      return e
    };
    return i.d(r, "a", r), r
  }, i.o = function (e, r) {
    return Object.prototype.hasOwnProperty.call(e, r)
  }, i.p = "/", i.oe = function (e) {
    throw console.error(e), e
  };
  var a = this["webpackJsonpstrimm.player"] = this["webpackJsonpstrimm.player"] || [], c = a.push.bind(a);
  a.push = r, a = a.slice();
  for (var l = 0; l < a.length; l++) r(a[l]);
  var f = c;
  t()
}([])</script>
    <script src="/reactplayer/js/2.581b0486.chunk.js"></script>
    <script src="/reactplayer/js/main.1983326b.chunk.js"></script>
</asp:Content>
