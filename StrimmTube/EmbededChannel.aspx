<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmbededChannel.aspx.cs" Inherits="StrimmTube.EmbededChannel" %>

<!DOCTYPE html>

<html>

<head runat="server">
   <!-- Google Tag Manager -->

<script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':

new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],

j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=

'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);

})(window,document,'script','dataLayer','GTM-WZX2SKG');</script>

<!-- End Google Tag Manager -->
       <meta http-equiv="content-type" content="text/html; charset=utf-8" />
       <meta http-equiv="Access-Control-Allow-Origin" content="*" />
       <%--<link href="/Plugins/slider/css/jquery.bxslider.css" rel="stylesheet" />--%>
       <script src="/Plugins/Vis/dist/vis.js"></script>
       <link href="/Plugins/Vis/dist/vis.min.css" rel="stylesheet" />
       <link href="/css/timeLine.css" rel="stylesheet" />
       <%--<script src="/Plugins/slider/js/jquery.bxslider.min.js"></script>--%>
       <%--<script src="/jquery/jquery.timepicker.js"></script>--%>
       <script src="/Plugins/Spin/spin.js"></script>

   <script src="/Flowplayer7/flowplayer.js"></script>
   <script src="/Flowplayer7/cc-button-7.2.5.js"></script>
   <script src="/Flowplayer7/settingsmenu-7.2.5.js"></script>
   <script src="/Flowplayer7/embed.min.js"></script>
   <script src="/Flowplayer7/vimeo-7.0.0.js"></script>
   <script src="/Flowplayer7/youtube-7.2.5.js"></script>
   <script src="/Flowplayer7/dailymotion-7.0.0.js"></script>

   <style>
.closeMenuLive {
display: block;
float: left;
background-image: url(/images/close_pop-white.png);
background-position: right;
background-repeat: no-repeat;
background-size: 30px;
height: 12px;
width: 100%;
background: ;
background-size: cover;
height: 20px;
width: 20px;
float: right;
}


.menuChannelLive,
.infoChannelLive {
   display: block;
   float: left;
   width: 100%;
}


/*.btnPlayLiveVideo {
   float: left;
   line-height: 40px;
   border-bottom: 1px solid #555;
}*/

.btnPlayLiveVideo.material-icons {
   float: left;
   line-height: 40px;
   border-bottom: 1px solid #555;
   color: #32bae6;
}
.titleLive.active {
background-color:#2a99bd;
}
.btnPlayLiveVideo.material-icons.active {
       color: red;
   }

.submenuChannelLive,
.subinfoChannelLive,
.liveEventInfo {
   display: block;
   float: left;
   width: 30%;
   text-align: center;
   line-height: 40px;
   color: #ddd;
   text-transform: none;
   border-bottom: 1px solid #555;
   text-overflow: ellipsis;
   overflow: hidden;
   white-space: nowrap;
}

   .submenuChannelLive.submenuChannelLiveTitle {
       width: 38%;
   }

.submenuChannelLive {
   color: #fc0;
}

.liveEventInfo {
   width: 3%;
   text-transform: lowercase;
   background-image: url(/images/infoIconSlider.png);
   background-position: center;
   background-repeat: no-repeat;
   background-size: 20px;
   height: 40px;
   opacity: 0.9;
   cursor: pointer;
}

.subinfoChannelLive.titleLive {
   width: 34%;
}

.liveEventInfoPopUp {
       display: none;
       position: relative;
       /* left: 30%; */
       width: 98%;
       /* max-width: 290px; */
       height: auto;
       background-color: #555;
       /* padding: 1%; */
       /* height: 271px; */
       /* overflow: hidden; */
       z-index: 999;
       /* padding-top: 0.5%; */
}

.liveEventInfoPopUpTitle, .liveEventInfoPopUpDescription {
       display: block;
       float: left;
       color: #ccc;
       line-height: 20px;
       text-align: left;
       width: 100%;
       background: #555;
}

.liveEventInfoPopUpDescription
{
height: 116px;
overflow: hidden;
padding-bottom: 1%;
text-overflow: ellipsis;
width: 100%;
display: block;
display: -webkit-box;

-webkit-line-clamp: 3;
-webkit-box-orient: vertical;
}
.showMoreLessLive {
       position: absolute;
       bottom: 0px;
       right: 10px;
       display: block;
       color: #2db9e7;
   cursor:pointer;
}
.nano .content.liveInfo {
       bottom: 0;
       left: 0;
       overflow-x: hidden;
       overflow-y: scroll;
       padding-left: 5px;
       padding-top: 5px;
       position: absolute;
       right: 0;
       top: 0;
       padding-left: 1%;
       padding-right: 1%;
}
.customLabel
{
   padding-left:30px !important;
}

.liveEventInfoPopUpTitle {
   color: #fff;
}
.closeLivePanel {
       display: block;
       background-image: url(/images/close_pop-white.png);
       background-position: right;
       background-repeat: no-repeat;
       background-size: 30px;
       height: 12px;
       width: 100%;
       background: ;
       background-size: cover;
       height: 30px;
       width: 30px;
       float: right;
}
.customLogo {
   height: 30px;
 
   max-width: 100px;
   background-repeat: no-repeat;
   margin-left: 30px;
   
}

.liveEventInfoPopUpTitle {
   border-bottom: 1px solid #999;
   width:99%;
   padding-left:1%;
}

       @media (max-width: 1100px) {
           .submenuChannelLive, .subinfoChannelLive {
               font-size: 12px;
               width: 31%;
           }

           .liveEventInfo {
               display: none;
           }
       }

       @media (max-width: 520px) {

           .submenuChannelLive, .subinfoChannelLive {
               font-size: 10px;
               width: 30%;
           }
       }

   </style>
       <title></title>



</head>

<body>
   <!-- Google Tag Manager (noscript) -->

<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-WZX2SKG"

height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>

<!-- End Google Tag Manager (noscript) -->
       <%= new StrimmTube.CorsUpload { }.ToString()%>

       <script>

               /* share42.com | 28.05.2014 | (c) Dimox */
               window.onload = function () { var e = document.getElementsByTagName('div'); for (var k = 0; k < e.length; k++) { if (e[k].className.indexOf('share42init') != -1) { if (e[k].getAttribute('data-url') != -1) var u = e[k].getAttribute('data-url'); if (e[k].getAttribute('data-title') != -1) var t = e[k].getAttribute('data-title'); if (e[k].getAttribute('data-image') != -1) var i = e[k].getAttribute('data-image'); if (e[k].getAttribute('data-description') != -1) var d = e[k].getAttribute('data-description'); if (e[k].getAttribute('data-path') != -1) var f = e[k].getAttribute('data-path'); if (e[k].getAttribute('data-icons-file') != -1) var fn = e[k].getAttribute('data-icons-file'); if (!u) u = location.href; if (!t) t = document.title; if (!fn) fn = 'icons.png'; function desc() { var meta = document.getElementsByTagName('meta'); for (var m = 0; m < meta.length; m++) { if (meta[m].name.toLowerCase() == 'description') { return meta[m].content; } } return ''; } if (!d) d = desc(); u = encodeURIComponent(u); t = encodeURIComponent(t); t = t.replace(/\'/g, '%27'); i = encodeURIComponent(i); d = encodeURIComponent(d); d = d.replace(/\'/g, '%27'); var fbQuery = 'u=' + u; if (i != 'null' && i != '') fbQuery = 's=100&p[url]=' + u + '&p[title]=' + t + '&p[summary]=' + d + '&p[images][0]=' + i; var s = new Array('"#" data-count="fb" onclick="window.open(\'https://www.facebook.com/sharer.php?m2w&' + fbQuery + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=550, height=440, toolbar=0, status=0\');return false" title="Share on Facebook"', '"#" data-count="gplus" onclick="window.open(\'https://plus.google.com/share?url=' + u + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=550, height=440, toolbar=0, status=0\');return false" title="Share on Google+"', '"#" data-count="pin" onclick="window.open(\'https://pinterest.com/pin/create/button/?url=' + u + '&media=' + i + '&description=' + t + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=600, height=300, toolbar=0, status=0\');return false" title="Pin It"', '"#" data-count="twi" onclick="window.open(\'https://twitter.com/intent/tweet?text=' + t + '&url=' + u + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=550, height=440, toolbar=0, status=0\');return false" title="Share on Twitter"'); var l = ''; for (j = 0; j < s.length; j++) l += '<a rel="nofollow" style="display:inline-block;vertical-align:bottom;width:32px;height:32px;margin:0 6px 6px 0;padding:0;outline:none;background:url(image.php?i=facebook,google-plus,pinterest,twitter&size=32&) -' + 32 * j + 'px 0 no-repeat" href=' + s[j] + ' target="_blank"></a>'; e[k].innerHTML = '<span id="share42">' + l + '</span>'; } }; };

               var channelTubeId = "<%=channelTubeId%>";
               var activeChannels;
               var activeChannelId = channelTubeId;
               var userId = "<%=userId%>";
               var isMyChannel = "<%=isMyChannel%>";
               var isIE8 = false;
               var facebookUrl = "";
               var userName = "<%=userName%>";
               var channelName = '<%=channelName%>';
               var rating = '<%=userRating%>';
               var isYoutubeActive = JSON.parse("<%=IsYoutubeActive%>".toLowerCase());
               var isVimeoActive = JSON.parse("<%=IsVimeoActive%>".toLowerCase());
               var userIdCheked = 0;
               var realChannelName = '<%=realChannelName%>';
               //subscribtion vars
           var LogoModeActive = JSON.parse('<%=LogoModeActive%>'.toLowerCase());
           var customLogoUrl = '<%=CustomLogo%>';
               var IsWhiteLabeled = JSON.parse('<%=IsWhiteLabeled%>'.toLowerCase());
           var muteOnStartup = true;
               var mutePlayerOnLoad = true;
               //var muteOnStartup = mute;
               var embedEnabled = JSON.parse("<%=embedEnabled%>".toLowerCase());
       var isCustomLabelEnabled = JSON.parse("<%=isCustomLabelEnabled%>".toLowerCase());
       var customLabel = '<%=customLabel%>';
               var subscribtionName = "<%=subscribtionDomainName%>";
               var allowControls = JSON.parse("<%=showPlayerControls%>".toLowerCase());
           var channelCreatorUserId = "<%=channelCreatorUserId%>";
       var playLiveFirst = JSON.parse("<%=PlayLiveFirst%>".toLowerCase());
       var keepGuideOpen=JSON.parse("<%=keepGuideOpen%>".toLowerCase());
       //end subscribtion vars

<%--        var defaultFemaleAvatar = '<%=defaultFemaleAvatar%>';
       var defaultMaleAvatar = '<%=defaultMaleAvatar%>';--%>

               var iLikeThisChannel = JSON.parse("<%=iLikeThisCahnnel%>".toLowerCase());
               var categoryName = '<%=categoryName%>';
               var width = $(window).width();
               var channelCreatorUserId = '<%=channelCreatorUserId%>';
               var userInfo = null;
               var userip = '';
               var durationCount = 0;
               var interval = null;
               var visitorId = 0;
               var embeddedChannelHostLoadId = 0;
               var isEmbededbyOwner = JSON.parse('<%=isEmbededbyOwner%>'.toLowerCase());
               var url;
               if (userId != null) {
                       userIdCheked = userId;
               }

               facebookUrl = window.location.href;

               //$(".rateit").bind('rated', function (event, value) { alert(value) });
               var isIframe;
               var iframe;
               var url;
               var accountNumber;
               var isSingleChannel;
               var isSingleChannelView = false;
               var IsSubscribedDomain = false;
               var embeddedDomainName ="<%=subscribtionDomainName%>";
               var embedUrl;

               function getParameterByName(name, url) {

                       name = name.replace(/[\[\]]/g, "\\$&");
                       var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)", "i"),
                               results = regex.exec(url);
                       if (!results) return null;
                       if (!results[2]) return '';
                       return decodeURIComponent(results[2].replace(/\+/g, " "));
               }
               function ExtractDomain(url) {
                       var domain;
                       //find & remove protocol (http, ftp, etc.) and get domain
                       if (url.indexOf("://") > -1) {
                               domain = url.split('/')[2];
                       }
                       else {
                               domain = url.split('/')[0];
                       }

                       //find & remove port number
                       domain = domain.split(':')[0];

                       return domain;
               }
               function notMouseMove() {

                       setTimeout(function () {
                               $(".controlsHolder").hide();
                       }, 3000);

               }
               function showControlsOnMouseMove() {
                       $(".controlsHolder").show();
               }
               var timer = setTimeout(notMouseMove, 3000);
       
               flowplayer.conf = {
                       fullscreen: true,
                       // iOS allows only native fullscreen from within iframes
                       native_fullscreen: true
               };
       $(document).ready(function () {
           //if (keepGuideOpen) {
           //    $("#embeddedViewHolder, #tvGuideControl").addClass("keepGuideOpen");
           //    $("#ancShowTvGuide").hide();
           //    TriggerTVGuide();
           //}
           console.log(customLabel)
                       if (!embedEnabled) {
                               $("#embeddedViewHolder").html("");
                               $("#embeddedViewHolder").html("<span style='width:853px; height: 480px;' class='embeddErrorMessage'>The channel '" + realChannelName + "' not embedded</span>");
                               return false;
                       }

                       setTimeout(function () {
                               HideSchedule();
                       }, 7000)
                       var now = new Date;
                       window.___gcfg = {
                               lang: 'en-US',
                               parsetags: 'explicit'
                       };
                       var globalClientTime = moment(now).format("MM/DD/YYYY HH:mm");
                       var clientTime = setClientTime();

                       url = this.referrer;
                       embedUrl = this.documentURI;

                       if (url == '') {
                               url = embedUrl;
                       }
                       var parentUrl;
                       //  console.log("url for Max: " + url)
                       // embeddedDomainName = ExtractDomain(url);
                       // console.log("channelTubeId: " + channelTubeId);
                       //check if subscribed domain is equal to embedded domain
                       //if (!embeddedDomainName.includes(subscribtionDomainName)) {
                       //    $("#sideBarChannel").hide();
                       //    $(".muteOnOff").hide();
                       //    console.log("embeddedDomainName: " + embeddedDomainName)
                       //    console.log("subscribtionDomainName: " + subscribtionDomainName)
                       //    return false;
                       //}
                       
                       if (isCustomLabelEnabled) {
                           if (LogoModeActive == true)
                           {
                               $("#logo").html("").html("<img class='customLogo' src='"+customLogoUrl+"' />").show();
                           }
                           else
                           {
                               $("#logo").html("").html("<a class='customLabel' style='color: #ddd !important;'>   " + customLabel + "<a>").show();
                           }
                               


                       }
                       else {
                               $("#logo").show();
                       }

                       if (url != undefined && embedUrl != undefined) {
                               var webMethodInsertEmbeddedHostChannelLoad = "/WebServices/ChannelWebService.asmx/InsertEmbeddedHostChannelLoad";

                               accountNumber = getParameterByName("accountNumber", embedUrl);
                               isSingleChannel = getParameterByName("showChannels", embedUrl);
                               if (isSingleChannel != null) {
                                       isSingleChannelView = isSingleChannel;
                               }
                               else {
                                       isSingleChannelView = false;
                               }

                               var EmbeddedPageLoadCriteria = {
                                       channelTubeId: channelTubeId,
                                       clientTime: clientTime,
                                       embeddedHostUrl: url,
                                       accountNumber: accountNumber,
                                       isSingleChannelView: isSingleChannelView,
                                       isSubscribedDomain: IsSubscribedDomain
                               };

                               $.ajax({
                                       type: 'POST',
                                       url: webMethodInsertEmbeddedHostChannelLoad,
                                       data: '{"model":' + JSON.stringify(EmbeddedPageLoadCriteria) + "}",
                                       dataType: "json",
                                       contentType: "application/json; charset=utf-8",
                                       success: function (response) {
                                               embeddedChannelHostLoadId = response.d;
                                       }
                               });
                       }

         
                       if ((IsWhiteLabeled == true) && (!isCustomLabelEnabled)) {
                               $('#logo').hide();
                       }
                       // console.log("IsWhiteLabeled: " + IsWhiteLabeled)


                       setClientTime();

                       window.setInterval(function () {
                               setClientTime();
                       }, 1000);

                       $(window).keydown(function (event) {
                               if (event.keyCode == 13) {
                                       event.preventDefault();
                                       return false;
                               }
                       });

                       interval = setInterval(function () {
                               ++durationCount;
                       }, 1000);

                       setTimeout(function () {
                               var visitorUserId = 0;
                               app = window.getApp();

                               if (app != null && app != undefined) {
                                       var user = app.getUserInfo();

                                       if (user != null && user != undefined) {
                                               userInfo = JSON.parse(user);
                                               userip = userInfo != null ? userInfo.UserIp : '';
                                       }
                               }

                               if (userId.trim()) {
                                       visitorUserId = userId;
                               }

                               var destination = "Channel";

                               if (userip == undefined || userip == null) {
                                       userip = '::1';
                               }

                               var params = '{"visitorUserId":' + visitorUserId + ',"channelTubeId":' + channelTubeId + ',"clientTime":' + "'" + clientTime + "'" + ',"visitorIp":' + "'" + userip + "'" + ',"destination":' + "'" + destination + "'" + ',"uri":' + "'" + window.location + "'" + '}';

                               $.ajax({
                                       type: 'POST',
                                       url: webMethodInsertVisitor,
                                       dataType: 'json',
                                       data: params,
                                       contentType: "application/json; charset=utf-8",
                                       success: function (response) {
                                               visitorId = response.d;
                                       }
                               });
                       }, 200);

                       //if (iLikeThisChannel) {
                       //    $("#ancLike").addClass("likeActive");
                       //}

                       //if (userId == 0) {
                       //    $(".rateit").rateit('readonly', true);
                       //    $(".rateit").css('cursor', "pointer");
                       //    $(".rateit").click(function () {
                       //        loginModal('sameLocation');
                       //    })
                       //}

                       var pinterest = '<div itemscope itemtype="https://schema.org/Movie">' +
                               '<h1 itemprop="name">' + '<%=channelName%>' + '\n' + '<%=channelDescription%>' + '</h1>' +
                               '<span itemprop="description">' + '<%=channelDescription%>' + '</span>' +
                               '<meta itemprop="url" content="' + window.location.href + '" />' +
                               '</div>';

                       $('head').append(pinterest);

                       $('.share42init').attr("data-url", window.location.href)
                               .attr("data-title", '<%=channelName%>')
                               .attr("data-image", '<%=channelImgAvatarUrl%>')
                               .attr("data-description", '<%=channelDescription%>');

                       var viewCountSetTimeOut = setTimeout(function () {

                               if (isMyChannel != "True") {
                                       var params = '{"userId":' + userId + ',"channelTubeId":' + channelTubeId + ',"viewTime":' + "'" + clientTime + "'" + '}';
                                       $.ajax({
                                               type: 'POST',
                                               url: webMethodAddUserChannelTubeView,
                                               dataType: 'json',
                                               data: params,
                                               contentType: "application/json; charset=utf-8",
                                               success: function (response) {
                                                       //console.log(response.d);
                                               }
                                       });
                               }
                       }, 120000);// set for 2 min from the page load, check last count for current user for this channel if it was today already skip the add

                       if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                               var ieversion = new Number(RegExp.$1) // capture x.x portion and store as a number
                               if (ieversion && ieversion >= 8) {
                                       isIE8 = true;
                                       $("#tweetUpdates").empty().html("<span id=spnMsg style='display:block; color:#555; font-size:13px;'>Your current version of Internet Explorer does not support Facebook communication on our site. Please use a latest version of Internet Explorer or another browser, like Firefox or Chrome</span>");
                               }
                       }

                       //if (executeSchedulePolling == 1) {
                       //    (function poll() {
                       SchedulePolling();
                       //    })();
                       //}

                       //if (userId == 0) {
                       //    $(".watchlater").removeAttr("onclick").attr("onclick", "loginModal('sameLocation')");
                       //}

                       //$(".nano").nanoScroller({ alwaysVisible: false });




               });

               $(window).bind('beforeunload', function () {

                       var webMethodUpdateUpdateEmbeddedHostChannelLoadById = "/WebServices/ChannelWebService.asmx/UpdateEmbeddedHostChannelLoadById";
                       var clientTime = getClientTime();
                       var params = '{"embeddedChannelHostLoadId":' + embeddedChannelHostLoadId + ',"visitTime":' + durationCount + ',"loadEndTime":' + "'" + clientTime + "'" + '}';
                       //  console.log(visitorId);

                       $.ajax({
                               async: false,
                               type: 'POST',
                               url: webMethodUpdateUpdateEmbeddedHostChannelLoadById,
                               dataType: 'json',
                               data: params,
                               contentType: "application/json; charset=utf-8",
                               success: function (response) {

                               }
                       });
                       var params = '{"visitorId":' + visitorId + ',"durationCount":' + durationCount + '}';
                       //console.log(visitorId);
                       //console.log(durationCount);
                       $.ajax({
                               async: false,
                               type: 'POST',

                               url: webMethodUpdateVisitDurationByVisitorId,
                               dataType: 'json',
                               data: params,
                               contentType: "application/json; charset=utf-8",
                               success: function (response) {

                               }
           });

           
               });

       </script>

       <form id="form1" runat="server">
               <div id="fb-root"></div>
       



               <div id="embeddedViewHolder" style="position: relative; padding-bottom: 50%; height: 0;">

                       <%--THE LOGO CODE BELOW IS DEPENDING ON JAVASCRIPT FOR CUSTOM LABEL BRANDING, PLEASE DO NOT CHANGE THE HTML and CSS CODE WITHOUT CLIENT SIDE DEVELOPER--%>
                       <a id="logo" runat="server" style="position: absolute; top: 3px; left: 0px; width: 100%; height: 35px; color: #ddd !important; font-size: 12px; font-family: Arial; background-color: #222; border-bottom: 1px solid #333; z-index: 9; line-height: 37px; display: none;">   Powered by  
                               <div onclick="RedirectToStrimm()" style="background-image: url(/images/527AB7E6.png); background-repeat: no-repeat; background-size: 100px; width: 100px; height: 33px; position: absolute; top: 2px; z-index: 9; left: 75px;"></div>
                       </a>
                       <%--END LOGO CODE--%>



                       <a style="position: absolute; top: 2px; right: 10px; width: 75px; z-index: 99; background-image: url(/images/TV-Guide.png); background-repeat: no-repeat; background-size: 30px; background-position: 2px; padding: 0; height: 30px; background-color: #ccc; color: #000; text-align: right; padding-right: 10px; line-height: 30px;"
                               id="ancShowTvGuide" onclick="TriggerTVGuide()">Guide</a>
           <a onclick="LoadLIveVideos()" class="embedLive" style="display: none;
   position: absolute;
   right: 108px;
   z-index: 99;
   color: #fff;
   background-color: red;
   width: 50px;
   height: 20px;
   text-align: center;
   line-height: 20px;
   margin-top: 9px;
   font-weight: bold;
   font-style: italic;">Live</a>
                       <div id="mainWrapper" class="block" onmousemove="showControlsOnMouseMove()" onmouseout="notMouseMove()" onmouseenter="showControlsOnMouseMove()">
                               <div class="webHolderRightWrapperOverlay"></div>


                               <div id="PlayerHolder">
                                       <div class="playerbox" id="player">
                                       </div>
                               </div>

                       </div>


                       <div class="moreChannelsHolder" runat="server" id="morechannelsholder">
                               <div class="moreChannelsContent" runat="server" id="moreChannelsContent">
                               </div>
                       </div>
                       <%--LIVE CONTROL PANEL--%>
             <div class="sideContentHolder">


               </div>


                       <div id="tvGuideControl" class="TVGuideBottom" style="display: none;">

                               <div class="TLactionsTopHolder">
                                       <ul class="TLNavUL">
                                               <li class="TLNavli onDemand"></li>
                                               <%--    <li class="TLNavli">Live</li>--%>
                                       </ul>

                                       <div class="closeGuide" onclick="closeTvGuide()"></div>



                                       <div class="TLinfoTopPannelMobile">
                                               <div class="TLinfoTopPannel">
                                                       <%-- <div id="showTopChannelsMobile" class="TLtopChannels" title="Show Top Channels" onclick="tabShowTopChannels();">Top Channels</div>--%>
                                                       <div id="showMyChannelsMobile" class="TLmyChannels" title="Show My Channels" onclick="tabShowMyChannels();">My Channels</div>
                                                       <%-- <div id="showFavoriteChannelsMobile" class="TLfavChannels" title="Show Favorite Channels" onclick="tabShowFavoriteChannels()">Favorite Channels</div>--%>
                                               </div>



                                       </div>


                               </div>
                               <div id="popupChannel" class="TLchannelInfoHolder" style="display: none;">
                                       <div id="popupChannelContainer" class="popupContainer">
                                               <div class="ddHolder">
                                                       <span class="tr TLtr"></span>
                                               </div>

                                               <div class="TLvideoClose" onclick="closeChannelPopup();">Hide Info</div>
                                               <div class="TLchannelInfoLeft">
                                                       <div id="channelAvatar" class="TLchannelBoxAvatar"></div>
                                               </div>
                                               <div class="TLchannelInfoRight">
                                                       <div id="channelName" class="TLchannelBoxTitle">Fashion TV</div>
                                                       <div id="channelDesc" class="TLchannelBoxDescription">Dive into the world of fashion and beauty. Imagine yourself sitting front row at the most exclusive Fashion TV </div>
                                               </div>


                                       </div>
                               </div>

                               <div id="tvguide">
                                       <div id="mytimeline">
                                               <div class="noChannels" style="display: none;"><span>No channels found</span></div>
                                       </div>

                               </div>
                               <div id="popupVideo" class="TLVideoInfoHolderEmbedded">
                                       <div id="popupVideoContainer" class="popupContainer">
                                               <div class="ddHolder ddlHOlderCreator">
                                                       <span class="tr TLtr"></span>
                                               </div>
                                               <div class="TLvideoClose" onclick="closeVideoPopup();">Hide Info</div>

                                               <div class="TLleft">
                                                       <div class="TLimage">
                                                               <img id="videoImage" src="/images/comingSoon.jpg" />
                                                       </div>

                                               </div>
                                               <div class="TLRight">
                                                       <div id="videoTitle" class="TLTitle">fashion TV</div>
                                                       <div id="videoDescription" class="TLDescription">Dive into the world of fashion and beauty. Imagine yourself sitting front row at the most exclusive Fashion </div>
                                                       <div id="videoStartTime" class="TLstartTime">1:30pm - 2:45pm</div>
                                                       <div id="videoDuration" class="TLDuration">1h 30min</div>
                                               </div>
                                       </div>
                               </div>
                               <div class="OpenPopupVideoInfo" onclick="OpenVideoInfo()">Open Info</div>
                               <div class="paginationHolder">
                                       <div id="divPagination">
                                               <div id="firstPage" onclick="loadFirstPage();"></div>
                                               <div id="priorPage" onclick="loadPrevPage();"></div>
                                               <div id="status">1 of 1</div>
                                               <div id="nextPage" onclick="loadNextPage();"></div>
                                               <div id="lastPage" onclick="loadLastPage();"></div>
                                       </div>
                               </div>

                       </div>
               </div>

               

               <div id="abuseModal" style="display: none;">
                       <h1 class="popupHeader">Report an Issue</h1>
                       <a id="close_x" class="close close_x closeAbuseReport" href="#"><span>×</span></a>
                       <div class="typeAbuse abuse">
                               <h3 class="H3abuseReport">Select type of issue</h3>
                               <asp:DropDownList runat="server" ID="ddlCategory" class="ddlCategoryAbuse" ClientIDMode="Static">
                                       <asp:ListItem>Copyright infringement</asp:ListItem>
                                       <asp:ListItem>Adult content</asp:ListItem>
                                       <asp:ListItem>Drug content</asp:ListItem>
                                       <asp:ListItem>Inappropriate content</asp:ListItem>
                                       <asp:ListItem>Other</asp:ListItem>
                               </asp:DropDownList>
                       </div>
                       <div class="videoAbuse abuse">
                               <h3 class="H3abuseReport">Video Title</h3>
                               <asp:TextBox ID="txtVideoTitle" class="ddlCategoryAbuse" runat="server" ClientIDMode="Static"></asp:TextBox>
                       </div>
                       <div class="commentAbuse abuse">
                               <h3 class="H3abuseReport">Comments</h3>
                               <asp:TextBox ID="txtComments" CssClass="fontFamily" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="6" Columns="84"></asp:TextBox>
                       </div>
                       <div class="submitAbuse">
                               <asp:Label runat="server" ID="lblMsg" ClientIDMode="Static"></asp:Label>
                               <a onclick="SendAbuseReport()">report</a>
                       </div>
               </div>
       </form>
<link async="async" href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"/>
    <script async="async" src='https://cdnjs.cloudflare.com/ajax/libs/jstimezonedetect/1.0.4/jstz.min.js'></script>
</body>
   
</html>