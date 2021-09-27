<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="channelPageNew.aspx.cs" Inherits="StrimmTube.channelPageNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">

    <meta name="description" content="Watch and enjoy Strimm TV channel" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server" ClientIDMode="Static">
    <%= new StrimmTube.CorsUpload { }.ToString()%>
    <%--<%: System.Web.Optimization.Styles.Render("~/bundles/channelpageNew/css") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/channelPage/js") %>--%>
    <style>
</style>
    <%-- <script src="/jquery/jquery-1.11.1.min.js"></script>
<script src="/jquery/jquery-migrate-1.2.1.min.js"></script>--%>
    <script src="/JS/Controls.js"></script>
    <script src="/Plugins/Vis/dist/vis.js"></script>
    <link href="/css/timeLine.css" rel="stylesheet" />
    <script src="/Plugins/Spin/spin.js"></script>
    <link href="/Plugins/Vis/dist/vis.css" rel="stylesheet" />
    <link href="/reactplayer/css/main.317ac729.chunk.css" rel="stylesheet">

    <%-- <script src="/JS/Froogaloop.js"></script>--%>
  
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <%-- <script src="https://api.dmcdn.net/all.js"></script>--%>
    <script src="/Plugins/Scroller/nanoscroller.min.js"></script>
    <link href="/Plugins/Scroller/scroller.css" rel="stylesheet" />
    <%-- <script src="/JS/Dmotion.js"></script>--%>
    <script src="/JS/jquery.prettySocial.min.js"></script>
    <!--<script src="https://f.vimeocdn.com/js/froogaloop2.min.js"></script>-->
   
    

    <script type="text/javascript">
        /* share42.com | 28.05.2014 | (c) Dimox */
        window.onload = function () { var e = document.getElementsByTagName('div'); for (var k = 0; k < e.length; k++) { if (e[k].className.indexOf('share42init') != -1) { if (e[k].getAttribute('data-url') != -1) var u = e[k].getAttribute('data-url'); if (e[k].getAttribute('data-title') != -1) var t = e[k].getAttribute('data-title'); if (e[k].getAttribute('data-image') != -1) var i = e[k].getAttribute('data-image'); if (e[k].getAttribute('data-description') != -1) var d = e[k].getAttribute('data-description'); if (e[k].getAttribute('data-path') != -1) var f = e[k].getAttribute('data-path'); if (e[k].getAttribute('data-icons-file') != -1) var fn = e[k].getAttribute('data-icons-file'); if (!u) u = location.href; if (!t) t = document.title; if (!fn) fn = 'icons.png'; function desc() { var meta = document.getElementsByTagName('meta'); for (var m = 0; m < meta.length; m++) { if (meta[m].name.toLowerCase() == 'description') { return meta[m].content; } } return ''; } if (!d) d = desc(); u = encodeURIComponent(u); t = encodeURIComponent(t); t = t.replace(/\'/g, '%27'); i = encodeURIComponent(i); d = encodeURIComponent(d); d = d.replace(/\'/g, '%27'); var fbQuery = 'u=' + u; if (i != 'null' && i != '') fbQuery = 's=100&p[url]=' + u + '&p[title]=' + t + '&p[summary]=' + d + '&p[images][0]=' + i; var s = new Array('"#" data-count="fb" onclick="window.open(\'https://www.facebook.com/sharer.php?m2w&' + fbQuery + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=550, height=440, toolbar=0, status=0\');return false" title="Share on Facebook"', '"#" data-count="gplus" onclick="window.open(\'https://plus.google.com/share?url=' + u + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=550, height=440, toolbar=0, status=0\');return false" title="Share on Google+"', '"#" data-count="pin" onclick="window.open(\'https://pinterest.com/pin/create/button/?url=' + u + '&media=' + i + '&description=' + t + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=600, height=300, toolbar=0, status=0\');return false" title="Pin It"', '"#" data-count="twi" onclick="window.open(\'https://twitter.com/intent/tweet?text=' + t + '&url=' + u + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=550, height=440, toolbar=0, status=0\');return false" title="Share on Twitter"'); var l = ''; for (j = 0; j < s.length; j++) l += '<a rel="nofollow" style="display:inline-block;vertical-align:bottom;width:32px;height:32px;margin:0 6px 6px 0;padding:0;outline:none;background:url(image.php?i=facebook,google-plus,pinterest,twitter&size=32&) -' + 32 * j + 'px 0 no-repeat" href=' + s[j] + ' target="_blank"></a>'; e[k].innerHTML = '<span id="share42">' + l + '</span>'; } }; };
        var channelTubeId = "<%=channelTubeId%>";
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
        var hideFooter = "<%=HideOldFooter%>";
        var iLikeThisChannel = JSON.parse("<%=iLikeThisCahnnel%>".toLowerCase());
        var categoryName = '<%=categoryName%>';
        var width = $(window).width();
        var channelCreatorUserId = '<%=channelCreatorUserId%>';
        var userInfo = null;
        var userip = '';
        var durationCount = 0;
        var interval = null;
        var visitorId = 0;
        var absoluteEmbeddedChannelUrl = '<%=absoluteEmbeddedChannelUrl%>';
        var domainName = '<%=DomainName%>';
        var accountNumber = '<%=accountNumber%>';
        var defaultFemaleAvatar = '<%=defaultFemaleAvatar%>';
        var defaultMaleAvatar = '<%=defaultMaleAvatar%>';
        var fbAppId = '<%=fbAppId%>';
        var isChannelPasswordProtected = JSON.parse("<%=isChannelPasswordProtected%>".toLowerCase());
        var channelUrl = "<%=channelUrl%>";
        var isFlowplayerEnabled = JSON.parse("<%=isFlowplayerEnabeled%>".toLowerCase());
        var categoryId = '<%=activeCategoryId%>';
        if (userId != null) {
            userIdCheked = userId;
        }
        facebookUrl = window.location.href;
        $(".rateit").bind('rated', function (event, value) { alert(value) });
       
        $(document).ready(function () {
            var windowWidth;
            var mobile;
            windowWidth = window.screen.width < window.outerWidth ?
            window.screen.width : window.ridth;
            mobile = windowWidth < 500;
            if (mobile) {
                console.log("mobile res")
                switchToFullVideo();
            }
            if (!isFlowplayerEnabled) {
                $("#controls").hide();
            }
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
                //console.log(typeof(visitorUserId));
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
            if (iLikeThisChannel) {
                $("#ancLike").addClass("likeActive");
            }
            if (userId == 0) {
                $(".rateit").rateit('readonly', true);
                $(".rateit").css('cursor', "pointer");
                $(".rateit").click(function () {
                    loginModal('sameLocation');
                })
            }
            if (hideFooter == "True") {
                $('#divFooter').hide();
            }
            var viewCountSetTimeOut = setTimeout(function () {
                if (userId == "" || userId == undefined || userId == null) {
                    userId = 0;
                }
                var clientTime = getClientTime();
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
            if (categoryId > 0) {
                if (categoryId == 9999) {
                    loadAllChannels();
                }
                else {
                    loadChannelsByCategoryId(categoryId);
                }
            }
            else {
                if (executeSchedulePolling == 1) {
                    (function poll() {
                        SchedulePolling()
                    })();
                }
            }
            if (userId == 0) {
                $(".watchlater").removeAttr("onclick").attr("onclick", "loginModal('sameLocation')");
            }
            $(".nano").nanoScroller({ alwaysVisible: false });
            //if (width <= 1499) {
            // $(".nano").nanoScroller({ alwaysVisible: false });
            // setTimeout(function () {
            // HideInfoBar();
            // HideSchedule();
            // }, 3000);
            //}
            //else {
            // ShowInfoBar();
            // ShowScheduleBlock();
            //}
        });
        //$(window).resize(function () {
        // if ($(this).width() != width) {
        // width = $(this).width();
        // }
        // if (width <= 1499) {
        // $(".nano").nanoScroller({ alwaysVisible: false });
        // setTimeout(function () {
        // HideInfoBar();
        // HideSchedule();
        // }, 3000);
        // }
        // else {
        // ShowInfoBar();
        // ShowScheduleBlock();
        // }
        //});
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        //window.fbAsyncInit = function () {
        // FB.init({
        // appId: '576305899083877',
        // status: true,
        // cookie: true,
        // xfbml: true,
        // version: 'v2.0'
        // });
        //};
        //(function () {
        // var e = document.createElement('script'); e.async = true;
        // e.src = document.location.protocol +
        // '//connect.facebook.net/en_US/all.js#xfbml=1&status=0&appId=576305899083877"';
        // $("#fb-root").append(e);
        // // document.getElementById('fb-root').appendChild(e);
        //}());
        //if (!isIE8) {
        // (function (d, s, id) {
        // var js, fjs = d.getElementsByTagName(s)[0];
        // if (d.getElementById(id)) return;
        // js = d.createElement(s); js.id = id;
        // js.src = "HTTPS://connect.facebook.net/en_US/all.js#xfbml=1&status=0&appId=576305899083877&version=v2.0";
        // fjs.parentNode.insertBefore(js, fjs);
        // }(document, 'script', 'facebook-jssdk'));
        //}
    </script>
    <div class="outerWrapperClose" onclick="ToggleOuterWrapper()"></div>
    <div id="switchModeNav" class="switchModeNav" onclick="switchToFullVideo()"></div>
    <div class="channelNavigationHolder">
        <div class="channelNavigationGuide" onclick="tabShowAllChannels()">
            <%--<div class="guideLetters guideLettersTV">TV</div>
            <div class="guideLetters">g</div>
            <div class="guideLetters">u</div>
            <div class="guideLetters">i</div>
            <div class="guideLetters">d</div>
            <div class="guideLetters">e</div>--%>
        </div>
         <div class="channelNavigationLive" onclick="ShowScheduleBlock('live')">LIVE</div>
        <div class="channelNavigationSchedule" onclick="ShowScheduleBlock('schedule')"></div>
        <div class="channelNavigationChInfo" onclick="ShowInfoBar()"></div>
        <div class="channelNavigationVideoInfo" onclick="ToggleOuterWrapper()"></div>
        <div class="channelNavigationChat" onclick="ShowScheduleBlock('chat')">Chat</div>
        <div class="channelNavigationSwitchMode" onclick="switchToFullVideo()"></div>
    </div>
    <div class="TLinfoTopPannelFull">
        <div class="TLinfoTopPannel">
            <div class="TLpannelIconsHolder">
                <span class="TVguideTitle TVguideTitleActive" onclick="tabShowAllChannels()" style="cursor: pointer;">Guide</span>
                <span class="TLchannlesBy" onclick="tabShowCreatorChannels()" style="cursor: pointer;" active>Channels by Robert</span>
                <div id="showTopChannels" class="TLtopChannels" title="Show Top Channels" onclick="tabShowTopChannels();">Top Channels</div>
                <div id="showMyChannels" class="TLmyChannels" title="Show My Channels" onclick="tabShowMyChannels();">My Channels</div>
                <div id="showFavoriteChannels" class="TLfavChannels" title="Show Favorite Channels" onclick="tabShowFavoriteChannels()">Favorite Channels</div>
            </div>
            <div>
                <span class="TLcreateChannel" style="cursor: pointer;" onclick="TriggerCreateChannel()">Create Channel</span>
            </div>
        </div>
    </div>
    <div class="mainHolder">
        <div id="mainWrapper" class="block" onclick="closeTvGuide();">
            <div class="webHolderRightWrapperOverlay"></div>
            <div class="showInfoBar" onclick="ShowInfoBar()">
                <span class="infoShowIcon"></span>
                <span class="infoShowIconInfo"></span>
            </div>
            <div class="hideInfoBlock">
                <a id="btnHide" onclick="HideInfoBar()"></a>
                <div class="webHolderLeftChanel">
                    <div id="ChannelPageInfoHolder">
<%--                        <span class="iconDescription">Video Info</span>
                        <div class="hideIcon" onclick="HideSideBar()"></div>--%>
                        <div id="channelPageInfoWrapper">
                            <div class="videoTitle videoTitleMain">
                                <h1></h1>
                                <span class="Rrated"></span>
                            </div>
                            <div class="channelInfoRightHolder">
                                <asp:Image ClientIDMode="Static" itemprop="image" ID="imgChannelAvatarChannel" class="imgChannelAvatarChannel" runat="server" />
                                <asp:Image runat="server" ID="imgChannel" CssClass="channelChannelImg" />
                                <div id="titleHolder">
                                    <asp:Label ID="ancName" runat="server" ClientIDMode="Static"></asp:Label>
                                    <div id="categoryHolder">
                                        <span class="channelInfoCategory">category</span>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblCategory"></asp:Label>
                                    </div>
                                </div>
                                <div class="descriptionHolder">
                                    <%--<asp:Image ID="imgChannel" runat="server" CssClass="channelChannelImg"></asp:Image> --%>
                                    <div class="channelWatchig"><%-- <a runat="server" id="hrefToChannel" class="hrefToChannel"><%=channelOnlandingPageName%></a>--%></div>
                                    <div class="channelWatchigDescription">
                                        <asp:Label runat="server" ID="chDescription" ClientIDMode="Static" CssClass="describtionP"></asp:Label>
                                        <asp:Label CssClass="moreInfo" runat="server" ID="chDescriptionMoreLabel" onclick="showChannelDescription()">More</asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="channelInfoLeftHolder">
                                <div id="ChannelPageSocialHolder">
                                    <div id="ratingHolder">
                                        <span id="spnAVGRating"><%=channelAVGRating%></span>
                                        <div class="ratingContainer">
                                            <span class="yourRating">Your Channel Rating</span>
                                            <input type="range" min="0" max="5" step="0.5" id="backing2" />
                                            <div class="rateit" id="rateit1" data-rateit-backingfld="#backing2" data-rateit-resetable="false"></div>
                                        </div>
                                        <script type="text/javascript">
                                            var webMethodSetChannelRating = "/WebServices/ChannelWebService.asmx/SetChannelRatingByUserIdAndChannelTubeId";
                                            var webMethodGetRating = "/WebServices/ChannelWebService.asmx/GetChannelRatingByChannelId"
                                            $("#rateit1").bind('rated', function (event, value) {
                                                //console.log(userId);
                                                if (userId != 0) {
                                                    var enteredDate = getClientTime();
                                                    var params = '{"userId":' + userId + ',"channelTubeId":' + channelTubeId + ',"ratingValue":' + value + ',"enteredDate":' + "'" + enteredDate + "'" + '}';
                                                    $.ajax({
                                                        type: 'POST',
                                                        url: webMethodSetChannelRating,
                                                        dataType: 'json',
                                                        data: params,
                                                        contentType: "application/json; charset=utf-8",
                                                        success: function (response) {
                                                            alertify.success("Channel rating was successfully updated.");
                                                            if (response.d == true) {
                                                                $.ajax({
                                                                    type: 'POST',
                                                                    url: webMethodGetRating,
                                                                    dataType: 'json',
                                                                    data: '{"channelTubeId":' + channelTubeId + '}',
                                                                    contentType: "application/json; charset=utf-8",
                                                                    success: function (response) {
                                                                        $("#spnAVGRating").text("").text(response.d);
                                                                    }
                                                                });
                                                            }
                                                        }
                                                    });
                                                }
                                                else {
                                                    loginModal('sameLocation');
                                                }
                                            });
                                            function OpenInfoPopup() {
                                                $("#embeddedLinkInfo").lightbox_me({
                                                    centered: true,
                                                    onLoad: function () {
                                                    },
                                                    onClose: function () {
                                                        RemoveOverlay();
                                                    },
                                                    closeSelector: "close_xx"
                                                });
                                            }
                                            function CloseEmbeddedPopup() {
                                                $('#embeddedLinkInfo').trigger('close');
                                                $('#embeddedLinkInfo').hide();
                                            }
                                        </script>
                                    </div>
                                    <div class="infoDevider deviderMargin" style="display: none;"></div>
                                    <div class="options">
                                        <a class="like" runat="server" clientidmode="Static" id="ancLike"></a>
                                        <a class="addtofavorite" runat="server" id="lnkAddToFave" clientidmode="Static"></a>
                                        <a class="watchlater" onclick="WatchItLater()" title="Save video to watch it later"></a>
                                        <a class="abusereport" onclick="ShowAbuseModal()" title="Report inappropriate content"></a>
                                        <%-- <a class="embeddedCode" onclick="OpenEmbededArea()" title="Embed"></a>--%>
                                        <a class="embeddedCode" onclick="OpenInfoPopup()" title="Embed"></a>
                                        <%-- <a class="shareInfo" title="Share" onclick="ShowSocioal()"></a>--%>
                                    </div>
                                    <div id="embeddedCodeHolder" style="display: none;">
                                        <textarea cols="35" rows="5" readonly="readonly" id="embededCodeCopyArea" class="embeddedTextarea"></textarea>
                                    </div>
                                    <!--<a class="shareInfo" title="Share" onclick="ShowSocioal()"></a> -->
                                    <div id="channelInfo">
                                        <asp:Label runat="server" ID="lblSubscribers" ClientIDMode="Static"></asp:Label>
                                        <asp:Label runat="server" ID="lblViews" ClientIDMode="Static"></asp:Label>
                                        <asp:Label runat="server" ID="lblLikes" ClientIDMode="Static"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="channelMenuHolder"></div>
                    </div>
                </div>
            </div>
            <%--<div id="loadingDiv">
<div id="loadingDivHolder">
<img src="/images/ajax-loader(3).gif" />
</div>
</div>--%>
            <div id="sideBarChannel">
                <div class="showScheduleWrapper">
                    <a id="expandPanel" class="expandPanel" onclick="ShowScheduleBlock()"></a>
                    <a id="expandAndShowSchedule" class="showSchedule" title="Channel Schedule" onclick="ShowScheduleBlock('schedule')"></a>
                    <%--<a id="expandAndShowFavorite" class="showFavorite" title="Favorite Channels" onclick="ShowScheduleBlock('favorite')"></a>
<a id="expandAndShowTopChannels" class="showTopChannels" title="Top Channels" onclick="ShowScheduleBlock('topchannels')"></a>
<a id="expandAndShowMyChannels" class="showMyChannels" title="My Channels" onclick="ShowScheduleBlock('mychannels')"></a>--%>
                    <a id="expandAndShowChat" class="showChat" title="Chat" onclick="ShowScheduleBlock('chat')"></a>
                </div>
                <div class="hideInfoScheduleBlock">
                    <a class="hideSchedule" onclick="HideSchedule()">Hide</a>
                    <div class="sideBarOptions">
                        <a class="myChat " title="Chat" onclick="GetFBComments()"></a>
                        <%--<a class="mychannels " title="My Channels" onclick="ShowMyChannels()"></a>
<a class="toprated" title="Top Channels" onclick="ShowTopChannels()"></a>
<a class="favoriteChannels" title="Favorite Channels" onclick="ShowFavorites()"></a>--%>
                        <a class="schedule scheduleactive " title="Channel Schedule" onclick="ShowSchedule()"></a>
                        <a class="liveVideos" title="LiveVideos" onclick="ShowSchedule()"></a>
                    </div>
                    <div id="hideIcon" onclick="HideSideBar()"></div>
                    <span class="iconDescription">channel schedule</span>
                    <div style="min-height: 735px; max-height: 2000px;">
                        <div class="nano scheduleScrollHomePage">
                            <div class="content">
                                <div class="sideContentHolder">
                                    <%-- <div class="pane">
<div class="slider" style="height: 327px; top: 0px;"></div>
</div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- <div class="leftWrapperRS">--%>
            <div class="wedHolderChannel">
                <div id="STRIMM_PLAYER_ROOT">
                </div>
            </div>
        </div>
        <div class="outerWrapper">
            <%--     <div id="controls">
                <div id="volume">
                    <a id="mute-toggle" class="material-icons">volume_up</a>
                </div>
                <div id="playBtn">
                    <a id="btnPlayPause" class="material-icons">pause</a>
                </div>
                <div id="startBtn">
                    <a id="btnstart" class="material-icons">replay</a>
                </div>
                <div id="prevBtn">
                    <a id="btnPrev" class="material-icons">skip_previous</a>
                </div>
                <div id="nextBtn">
                    <a id="btnNext" class="material-icons">skip_next</a>
                </div>
                <div id="currTime">
                    <span style="display: block;" id="current-time"></span>
                    <div class="hp_slide">
                        <div class="hp_range"></div>
                    </div>
                    <%-- <progress value="0" max="100" id="progress-bar"></progress>--%>
            <%--<input type="range" value="0" id="progress-bar"/>--%>
            <%-- <span style="display: block;" id="duration"></span>--%>
            <%--</div>
                <div id="fullScreenBtn">
                    <a id="btnFullscreen" class="material-icons">settings_overscan</a>
                </div>
            </div>--%>
            <div class="innerWrapper">
                <div class="videoInfoHolderPlayer">
                    <div class="channelInfoPlayer">
                        <div class="channelTitlePlayer"></div>
                        <div class="creatorInfoPlayer">
                            <span class="byCreatorName">by</span> <a class="creatorInfoPlayer" id="channelCreatorName" onclick="openCreatorInfoPopup();"></a>
                            <div class="creatorInfoPopUP" style="display: none">
                                <div class="ddHolder ddlHOlderCreator">
                                    <span class="TLtrcreator"></span>
                                </div>
                                <div class="TLvideoClose" onclick="closeCreatorInfoPopup()"></div>
                                <div class="channelCreator">
                                    <div class="creatorLeft">
                                        <a runat="server" id="ancCreatorLink" class="ancCreatorLink">
                                            <asp:Image ID="imgCreator" runat="server" CssClass="channelCreatorImg" /></a>
                                    </div>
                                    <div class="creatorRight">
                                        <span class="spnChannelCreator">creator</span>
                                        <asp:Label runat="server" ID="spnCreatorName" CssClass="spnChannelCreatorName"></asp:Label>
                                        <a id="ancUserName" runat="server" clientidmode="static"></a>
                                    </div>
                                    <div class="channelCreatorBIO">
                                        <asp:Label runat="server" ID="chCreatorBio" CssClass="creatorBioP" ClientIDMode="Static"></asp:Label>
                                        <asp:Label CssClass="moreInfo" runat="server" ID="chCreatorBioMore" onclick="showCreatorBio()">More</asp:Label>
                                    </div>
                                    <div class="visitMyPage">
                                        <a class="spnVisitMyPage">visit my page</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="TLcreatorImg">
                        </div>
                    </div>
                    <div class="videoinfoPlayer">
                        <div class="videoDurationPlayer"></div>
                        <div class="videoTitlePlayer"></div>
                    </div>
                </div>
                <div id="socialHolder">
                    <div class="likeItshareIt">Like it? Share it!</div>
                    <div class="social-container">
                        <div class="links">

                            <div id="newSocilaHolder">
                                <a href="#" data-type="twitter" data-url="" data-description="" data-via="strimm" class="prettySocial fa fa-twitter"></a>
                                <a href="#" data-type="facebook" data-url="" data-title="" data-description="" data-media="" class="prettySocial fa fa-facebook"></a>
                                <a href="#" data-type="googleplus" data-url="" data-description="" class="prettySocial fa fa-google-plus"></a>
                                <a href="#" data-type="pinterest" data-url="" data-description="" data-media="" class="prettySocial fa fa-pinterest"></a>
                            </div>
                            <div id="emailBtn" class="emailBtnSocial" onclick="emailCurrentPage();"></div>
                            <%--<a href="#" data-type="linkedin" data-url="https://sonnyt.com/prettySocial" data-title="prettySocial - custom social share buttons." data-description="Custom share buttons for Pinterest, Twitter, Facebook and Google Plus." data-via="sonnyt" data-media="https://sonnyt.com/assets/imgs/prettySocial.png" class="prettySocial fa fa-linkedin"></a>--%>
                        </div>
                    </div>
                    <%--<div class="share42init" style="float: right;"></div>--%>
                    <%--<script type="text/javascript" src="/Plugins/Share42/share42.js"></script>--%>
                </div>
            </div>
        </div>
        <%-- <div class='flowplayer fp-player flowplayerControls'> </div>--%>
    </div>
    <div class="browseChannelsView">
        <div id="socialHolderMobile">
            <div id="socialHolder">
                <div class="social-container">
                    <div class="links">
                        <div id="emailBtn" class="emailBtnSocial" onclick="emailCurrentPage();"></div>
                        <div id="newSocilaHolder">
                            <a href="#" data-type="twitter" data-url="" data-description="" data-via="strimm" class="prettySocial fa fa-twitter"></a>
                            <a href="#" data-type="facebook" data-url="" data-title="" data-description="" data-media="" class="prettySocial fa fa-facebook"></a>
                            <a href="#" data-type="googleplus" data-url="" data-description="" class="prettySocial fa fa-google-plus"></a>
                            <a href="#" data-type="pinterest" data-url="" data-description="" data-media="" class="prettySocial fa fa-pinterest"></a>
                        </div>
                        <%--<a href="#" data-type="linkedin" data-url="https://sonnyt.com/prettySocial" data-title="prettySocial - custom social share buttons." data-description="Custom share buttons for Pinterest, Twitter, Facebook and Google Plus." data-via="sonnyt" data-media="https://sonnyt.com/assets/imgs/prettySocial.png" class="prettySocial fa fa-linkedin"></a>--%>
                    </div>
                </div>
                <%--<div class="share42init" style="float: right;"></div>--%>
                <%--<script type="text/javascript" src="/Plugins/Share42/share42.js"></script>--%>
            </div>
        </div>
        <div class="VideoInfoCL">
            <div id="PlayerHolderPreview" class="videoCL">
                <div id="STRIMM_PLAYER_ROOT">
                </div>
            </div>
        </div>
        <div class="ChannelInfoCL">
            <div class="ChannelInfoCLGradient"></div>
            <div class="ChannelInfoCLCloseMobile">
                <%--  <div class="material-icons muteMobile">volume_up</div>--%>
                <div class="openAbuseReport" onclick="ShowAbuseModal()"></div>
                <div class="openSaveVideo">
                    <div class="TLrecordLed"></div>
                    <span class="TLrecordRec TLrecordRecCL"></span>
                </div>
                <div class="openChannelInfo" onclick="ShowInfoBarMobile()"><i class="material-icons">expand_more</i></div>
            </div>
            <div class="openChannelInfoGuide">Open Info</div>
            <div class="closeChannelInfo" onclick="HideInfoBarMobile()"></div>
            <div class="channelNameCL">
                <div class="titleCLFixed">channel</div>
                <span id="lineupChannelName"></span>
            </div>
            <div class="creatorMobileHolder">
                <div class="creatorMobileImg"></div>
                <div class="creatorMobileName" onclick="openCreatorInfoPopup()"></div>
            </div>
            <div class="nowPlayingCL">
                <div class="titleCLFixed">now playing </div>
                <div class="lineupVideoTitle"><span id="lineupVideoTitle"></span></div>
            </div>
            <div class="runtimeCL">
                <div class="titleCLFixed">runtime</div>
                <div class="durationTimelCL">
                    <span id="lineupVideoPlaytime"></span>
                </div>
            </div>
            <div class="channelDescriptionCL">
                <div class="titleCLFixed"><span class="descrCL">description</span></div>
                <span id="lineupVideoDescription"></span>
            </div>
            <div class="actionsCLHolder">
                <div class="durationFullCL"><span id="lineupVideoDuration"></span></div>
                <div class="watchCL" onclick="switchToFullVideo()"><span>watch channel</span></div>
                <%--   <div class="muteCL" ></div>--%>
                <%--muteON-white--%>
                <div class="TLabuseReportHolder">
                    <div class="abuseCL" onclick="ShowAbuseModal()"></div>
                    <span class="spnAbuseRep">Abuse Report</span>
                </div>
                <div class="recHolder">
                    <div class="TLrecordLed" id="lineupRecordVideo"></div>
                    <span class="TLrecordRec TLrecordRecCL">Watch Later</span>
                </div>
            </div>
            <%-- <span class="moreDescription"> more</span>--%>
        </div>
    </div>
    <div class="browseChannelsDublicate"></div>
    <!--- TIMELINE CODE -->
    <br />
    <div id="remoteControl" class="TLnavigationHolder">
        <div class="TLnavPrior" onclick="moveBackInTime()"></div>
        <div class="TLnavUP" onclick="loadPrevChannel()"></div>
        <div id="switchMode" class="switchMode" <%--onclick="switchToFullVideo()"--%>></div>
        <div class="TLnavNext" onclick="moveForwardInTime()"></div>
        <div class="TLnavDOWN" onclick="loadNextChannel()"></div>
    </div>
    <div id="tvGuideControl" class="TVGuideBottom" style="display: none;">
        <div class="TLinfoTopPannelBrCH">
            <div class="TLinfoTopPannel">
                <span class="TVguideTitle TVguideTitleActive" onclick="tabShowAllChannels()" style="cursor: pointer;">Guide</span>
               
                <span class="TLchannlesBy" onclick="tabShowCreatorChannels()" style="cursor: pointer;" active>Channels by Robert</span>
                <div class="TLpannelIconsHolder">
                    <div id="showTopChannels" class="TLtopChannels" title="Show Top Channels" onclick="tabShowTopChannels();">Top Channels</div>
                    <div id="showMyChannels" class="TLmyChannels" title="Show My Channels" onclick="tabShowMyChannels();">My Channels</div>
                    <div id="showFavoriteChannels" class="TLfavChannels" title="Show Favorite Channels" onclick="tabShowFavoriteChannels()">Favorite Channels</div>
                     <div class="TLLive" onclick="ShowLiveVideos(this)">Live</div>
                </div>
                <div id="liveScheduleSmall" style="display:none; width:100%; height:500px;">

                </div>
                <%-- <div>
<span class="TLcreateChannel" style="cursor:pointer;" onclick="TriggerCreateChannel()">Create Channel</span>
</div>--%>
            </div>
        </div>
        <div class="TLactionsTopHolder">
            <div class="closeGuide" onclick="closeTvGuide()"></div>
            <div class="TLpannelDDLHOlder">
                <div id="categorySearch" class="leftSortingHolder styled-selectOpt">
                    <select id='ddlCategory' class='TLenglishChannels' onchange="showChannelsByCategory()"></select>
                </div>
                <div id="languageSearch" class="leftSortingHolder styled-selectOpt">
                    <select id="ddlLang" class="TLenglishChannels" onchange="showChannelsByLanguage()"></select>
                </div>
                <div class="keywordHolder">
                    <input id="txtKeywords" class="TLserchInput TLserchInputIcon" type="text" placeholder="Search for channels" onkeyup="channelSearchTvGuideInputKeyUp()" />
                    <div id="searchByKeywords" class="TLsearch" onclick="showChannelsByKeywords()"></div>
                    <div class="searchIconClose" onclick="closeSearch();"></div>
                </div>
                <div class="keywordHolderIcon">
                    <input id="txtKeywordsIcon" class="TLserchInput" type="text" onkeyup="channelSearchTvGuideInputKeyUp()" onmousedown="expandSearchControl();" />
                    <div id="searchByKeywordsIcon" class="TLsearch" onclick="showChannelsByKeywords()"></div>
                </div>
                <div class="shareMobile">share</div>
            </div>
            <div class="TLinfoTopPannelMobile">
                <div class="TLinfoTopPannel">
                    <div id="showTopChannelsMobile" class="TLtopChannels" title="Show Top Channels" onclick="tabShowTopChannels();">Top Channels</div>
                    <div id="showMyChannelsMobile" class="TLmyChannels" title="Show My Channels" onclick="tabShowMyChannels();">My Channels</div>
                    <div id="showFavoriteChannelsMobile" class="TLfavChannels" title="Show Favorite Channels" onclick="tabShowFavoriteChannels()">Favorite Channels</div>
                </div>
            </div>
        </div>
        <div id="popupChannel" class="TLchannelInfoHolder" style="display: none;">
            <div id="popupChannelContainer" class="popupContainer">
                <div class="ddHolder">
                    <span class="tr TLtr"></span>
                </div>
                <div class="TLvideoClose" onclick="closeChannelPopup();"></div>
                <div class="TLchannelInfoLeft">
                    <div id="channelAvatar" class="TLchannelBoxAvatar"></div>
                </div>
                <div class="TLchannelInfoRight">
                    <div id="channelName" class="TLchannelBoxTitle">Fashion TV</div>
                    <div id="channelDesc" class="TLchannelBoxDescription">Dive into the world of fashion and beauty. Imagine yourself sitting front row at the most exclusive Fashion TV </div>
                </div>
                <div class="TLchannelInfoBottom">
                    <div class="TLchannelBoxCreatorInfo">
                        <div id="userImage" class="TLcreatorImg">
                        </div>
                        <div class="TLcreator">creator</div>
                        <a id="creatorName" class="TLcreatorName"></a>
                    </div>
                    <%-- <div id="channelWatchNow" class="TLbtnWhatchNow">watch channel</div>--%>
                </div>
            </div>
        </div>
        <div id="popupVideo" class="TLVideoInfoHolder" style="display: none;">
            <div id="popupVideoContainer" class="popupContainer">
                <div class="ddHolder ddlHOlderCreator">
                    <span class="tr TLtr"></span>
                </div>
                <div class="TLvideoClose" onclick="closeVideoPopup();"></div>
                <div class="TLleft">
                    <div class="TLimage">
                        <img id="videoImage" src="/images/comingSoon.jpg" />
                    </div>
                    <div class="TLactionHolder">
                        <div id="videoRecord" class="TLrec">
                            <div class="TLrecordLed"></div>
                            <span class="TLrecordRec">Watch Later</span>
                        </div>
                        <div id="videoWatchNow" class="TLbtnWhatchNow">watch channel</div>
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
        <div id="mytimeline">
            <div class="noChannels" style="display: none;"><span>No channels found</span></div>
        </div>
        <div class="paginationHolder">
            <div id="divPagination" style="display: none;">
                <div id="firstPage" onclick="loadFirstPage();"></div>
                <div id="priorPage" onclick="loadPrevPage();"></div>
                <div id="status">1 of 1</div>
                <div id="nextPage" onclick="loadNextPage();"></div>
                <div id="lastPage" onclick="loadLastPage();"></div>
            </div>
        </div>
    </div>
    <%-- <div class="moreChannelsHolder">
<div class="seeMoreCategoryHolder">
<span class="spnMoreChannels"> More channels from this category</span>
<a href="/browse-channel?category=<%=categoryName%>" class="seeMoreChannels"> See More channels </a>
</div>
<div class="moreChannelsContent"> </div>
</div> --%>
    <%-- <div id="divAddSense">
<script type="text/javascript">
var ad_idzone = "1777962",
ad_width = "728",
ad_height = "90";
</script>
<script type="text/javascript" src="https://ads.exoclick.com/ads.js"></script>
<noscript>
<a href="https://main.exoclick.com/img-click.php?idzone=1777962" target="_blank">
<img src="https://syndication.exoclick.com/ads-iframe-display.php?idzone=1777962&output=img&type=728x90" width="728" height="90">
</a>
</noscript>
</div>--%>
    <div id="abuseModal" style="display: none;">
        <h1 class="popupHeader">Report an Issue</h1>
        <a id="close_x" class="close close_x closeAbuseReport" href="#"><span>&times;</span></a>
        <div class="typeAbuse abuse">
            <h3 class="H3abuseReport">Select type of issue</h3>
            <asp:DropDownList runat="server" ID="ddlCategoryAbuse" class="ddlCategoryAbuse" ClientIDMode="Static">
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
    <div class="homeBlocksHolder homeBlocksHolderFooterChP ">
        <div id="divFooterNH" class="default regular">
            <h1 class="paragraphSloganBlackFoooterBasic plainNH">Continuous broadcast from your own channel</h1>
            <div class="bottomButtonsHolderFoooterBasic">
                <a class="bottomSignUpFoooterBasic" href="/sign-up">sign up </a>
                <a class="learnMoreHomeFoooterBasic" href="/learn-more">Learn More</a>
            </div>
            <div class="holderNH">
                <div class="column columnHN ">
                    <a href="Default.aspx" class="logoFooterLInk">
                        <div class="logoFooter"></div>
                    </a>
                    <div class="column columnFooterSocial">
                        <ul class="footerSocialHolder">
                            <li class="footerSocial footerSocialFacebook"><a href="https://www.facebook.com/strimmTV" target="_blank"></a></li>
                            <li class="footerSocial footerSocialTwitter"><a href="https://twitter.com/strimmtv" target="_blank"></a></li>
                            <li class="footerSocial footerSocialGoogle"><a href="https://plus.google.com/+StrimmTV/posts" target="_blank"></a></li>
                            <li class="footerSocial footerSocialPinterest"><a href="https://pinterest.com/strimmTV" target="_blank"></a></li>
                        </ul>
                    </div>
                </div>
                <div class="column columnHN">
                    <h3>About Us</h3>
                    <ul>
                        <li><a href="/company">Company</a></li>
                        <li><a href="/press">Press </a></li>
                        <li><a href="/contact-us">Contact</a></li>
                    </ul>
                </div>
                <div class="column columnHN">
                    <h3>How It Works</h3>
                    <ul>
                        <li><a href="/learn-more">Become a Producer</a></li>
                        <li><a href="/faq">FAQ</a></li>
                        <li><a href="/guides">How To</a></li>
                    </ul>
                </div>
                <%-- <div class="column columnHN columnFooterSocial">
<h3>Social</h3>
<ul class="footerSocialHolder">
<li class="footerSocial footerSocialFacebook"><a href="https://www.facebook.com/strimmTV" target="_blank"></a></li>
<li class="footerSocial footerSocialTwitter"><a href="https://twitter.com/strimmtv" target="_blank"></a></li>
<li class="footerSocial footerSocialGoogle"><a href="https://plus.google.com/+StrimmTV/posts" target="_blank"></a></li>
<li class="footerSocial footerSocialPinterest"><a href="https://plus.google.com/+StrimmTV/posts" target="_blank"></a></li>
</ul>
</div>--%>
                <div class="column columnHN">
                    <h3>Legal</h3>
                    <ul>
                        <li><a href="/copyright">Copyright Policy</a></li>
                        <li><a href="/privacy-policy">Privacy Policy</a></li>
                        <li><a href="/terms-of-use">Terms of Use</a></li>
                    </ul>
                </div>
                <div id="divAllRights">
                    <span>&#169;2015-2016 Strimm, Inc. | All Rights reserved </span>
                    <%-- <a href="Copyrights.aspx" class="copyrights"> / &nbsp; copyright policy</a>--%>
                </div>
            </div>
        </div>
    </div>
    <!-- AddToAny BEGIN -->
    <%--<div class="a2a_kit a2a_kit_size_32 a2a_default_style">
<a class="a2a_dd" href="https://www.addtoany.com/share_save"></a>
<a class="a2a_button_facebook"></a>
<a class="a2a_button_twitter"></a>
<a class="a2a_button_google_plus"></a>
</div>
<script type="text/javascript" src="//static.addtoany.com/menu/page.js"></script>--%>
    <!-- AddToAny END -->
    <%-- <div id="channelPasswordModal" style="display:none; background-color:#fff;">
<a id="" class="close close_x" href="#"><span> &times;</span></a>
<h1 class="popupHeader">Please enter a password to access this channel</h1>
<input type="password" id="channelPasswordInput"/>
<a onclick="ValidateChannelPassword()">ok</a>
<a href="/<%=userName%>">contact channel owner</a>
<span id="passMessage"></span>
</div>--%>
    <div id="embeddedLinkInfo">
        <a id="close_xx" class="close close_x closeInterestsPopup" href="#" onclick="CloseEmbeddedPopup()">close</a>
        <div id="popupTitle">
            Embedded Link
        </div>
        <div id="popupBody">
            <div id="popupContent">
                Want to embed this channel on a website?</br> Please visit <a href="/create-tv/pricing" class="gotoBusinessSolutions">Plans and Pricing</a> for details 
            </div>
            <%-- <div id="popupActions">
<a id="buttonSubmit" href="/business_solutions#pricingBlock">
<span class="ancBoxText">Go</span>
</a>
</div>--%>
        </div>
    </div>
    <div id="ageVerificationPopup">
        <%-- <h2>Your watching experience is important to us!</h2>--%>
        <div id="pContent">
            <p>The site content is user-generated. We cannot guarantee that it is appropriate for all ages. Adult supervision is recommended for all minors.</p>

            <p>By clicking "I Confirm and Agree" below, you acknowledge that you have read and accepted our<a href="/terms-of-use" target="_blank"> Terms of Use</a> and <a href="/privacy-policy" target="_blank">Privacy Policy</a></p>

        </div>
        <div id="pActions">
            <div id="disagreeButton" onclick="UserCancelsAgeVerification();">I Don’t Agree</div>
            <div id="confirmButton" onclick="UserConfirmsAndAgrees();">I confirm and agree</div>
        </div>
        <div id="pSignup">
            Please <a href="/sign-up">Sign Up</a> to prevent this message from re-appearing next time.
        </div>
    </div>
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"/>
      <script async="async" src='https://cdnjs.cloudflare.com/ajax/libs/jstimezonedetect/1.0.4/jstz.min.js'></script>
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

            var n = {}, o = { 1: 0 }, u = [];

            function i(r) {
                if (n[r]) return n[r].exports;
                var t = n[r] = { i: r, l: !1, exports: {} };
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
                        return i.p + "static/js/" + ({}[e] || e) + "." + { 3: "c5fd260e" }[e] + ".chunk.js"
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
                        u({ type: "timeout", target: a })
                    }), 12e4);
                    a.onerror = a.onload = u, document.head.appendChild(a)
                }
                return Promise.all(r)
            }, i.m = e, i.c = n, i.d = function (e, r, t) {
                i.o(e, r) || Object.defineProperty(e, r, { enumerable: !0, get: t })
            }, i.r = function (e) {
                "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e, Symbol.toStringTag, { value: "Module" }), Object.defineProperty(e, "__esModule", { value: !0 })
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
    <script src="/reactplayer/js/2.cd6796f3.chunk.js"></script>
    <script src="/reactplayer/js/main.4514192c.chunk.js"></script>
</asp:Content>
