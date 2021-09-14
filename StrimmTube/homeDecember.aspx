    <%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="homeDecember.aspx.cs" Inherits="StrimmTube.homeDecember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Strimm – Free Online Video Platform to Create Your Own TV Network
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Strimm is a free online video platform to create your own TV network or watch channels online." />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://www.strimm.com" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <%: System.Web.Optimization.Styles.Render("~/bundles/home/css") %>

  


<%--    <link href="css/DefaultCSS.css" rel="stylesheet" />--%>
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="//www.google.com/jsapi" type="text/javascript"></script>
    <script src="https://www.youtube.com/iframe_api"></script>
    <script src="/JS/Main.js"></script>
      <script src="/JS/Froogaloop.js"></script>
    <script src="/JS/Dmotion.js" type="text/javascript"></script> 
   
     <script src="https://api.dmcdn.net/all.js"></script>
    <script type="text/javascript">
        google.load("swfobject", "2.1");
    </script>



    <script type="text/javascript">
        var username = "<%=UserName%>";
        var hideFooter = "<%=HideOldFooter%>";
        var playingChannelId = "<%=playingChannelId%>";
        var webMethodGetChannelPreviewDataJson = "/WebServices/ChannelWebService.asmx/GetCurrentlyPlayingChannelData";
        <%-- 
        var webMethodGetCurrentlyPlayingChannelsForLandingPage = "/../WebServices/ChannelWebService.asmx/GetCurrentlyPlayingChannelsForLandingPage";
        function GetFeaturedChannels() {
            ajaxTopChannels = $.ajax({
                type: "POST",
                url: webMethodGetCurrentlyPlayingChannelsForLandingPage,
                data: '{"clientTime":' + "'" + globalClientTime + "'" + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (response) {
                    if (response.d) {
                        var data = response.d;
                        if (data) {
                            var maxCount = 11;

                            if (data.ChannelGroup) {
                                var echannels = Controls.BuildChannelsForLandingPageControl(data.ChannelGroup, maxCount, data.GroupName);
                                $("#channelGroup").html("").html(echannels);
                            }
                        }
                    }
                },
                error: function () {
                }
            });
        };--%>
        $(document).ready(function () {

            GetPlayingChannelScheduleData();
        });
        window.onload = function () {
            //GetFeaturedChannels();
            if (username) {
                $(".bottomSignUpNH").hide();
                $(".learnMoreHomeNH").removeClass('learnMoreHomeNH').addClass('learnMoreHomeAL');
            }

            if (hideFooter == "True") {
                $('#divFooter').hide();
            }
        };
        var player;
        var done = false;
        var channelScheduleId = 0;
        var playlistArr = new Array();
        var playlist;
        var playingVideoId = 0;
        var currVideoTitle;
        var currChannelSchedule;
        var playBackOrderNum;   
        var currentlyPlayingVideo;
        var playlistStr;
        var userIdCheked = 0;
        var isYoutubeActive = true;
        var isVimeoActive = false;
        
        function GetPlayingChannelScheduleData() {
            var timeOfRequest = getClientTime();

            $.ajax({
                type: 'POST',
                url: webMethodGetChannelPreviewDataJson,
                dataType: 'json',
                data: '{"clientTime":' + "'" + timeOfRequest + "'" + ',"channelId":' + playingChannelId + ',"userId":' + userIdCheked + '}',
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d.ActiveSchedule != null) {
                        $("#playerbox").toggle();
                        var data = response.d;
                        var channel = data.Channel;
                        var schedule = data.Schedule;

                        playlist = data.Playlist;

                        for (var i = 0; i < playlist.length; i++) {
                            playlistArr[i] = playlist[i].ProviderVideoId;
                        }

                        currChannelSchedule = data.ActiveSchedule.ChannelScheduleId;
                        var SchedulesHtml = Controls.BuildScheduleControlForLandingPage(data.Playlist);
                        var firstVideo = playlist[0];

                        startTime = parseInt(parseFloat(firstVideo.PlayerPlaybackStartTimeInSec).toFixed());

                        if (isYoutubeActive && isVimeoActive) {
                            if (firstVideo.VideoProviderName == "vimeo") {
                                InitVimeoPlayer(playlist, 0, startTime);
                            }
                            else if (firstVideo.VideoProviderName == "dailymotion") {
                                InitDMotionPlayer(playlist, 0, startTime);
                            }
                            else {
                                InitYoutubePlayer(playlist, 0, startTime);
                            }
                        }
                        else if (isVimeoActive && !isYoutubeActive) {
                            InitVimeoPlayer(playlist, 0, startTime);
                        }
                        else {
                            if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                                onYouTubeIframeAPIReadyIE();
                            }
                            else {
                                onYouTubeIframeAPIReady();
                            }
                        }
//////ADD
                        $(".webHolderRight").html("").html(SchedulesHtml);
                    }

                }

            });
        };

        function onYouTubeIframeAPIReady() {
            player = new YT.Player('player', {
                height: '560',
                width: '100%',
                playerVars: {
                    autoplay: 1,
                    html5: 1,
                    rel: 0,
                    controls: 0,
                    modestbranding: 1
                },
                events: {
                    'onReady': onPlayerReady,
                    'onStateChange': onPlayerStateChange,
                    'onError': onError
                }
            });
        }

        function onYouTubeIframeAPIReadyIE() {
            if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                $("iframe.playerbox").attr("allowfullscreen");
                player = new YT.Player('player', {

                    height: '560',
                    width: '100%',
                    playerVars: {
                        autoplay: 1,
                        html5: 1,
                        rel: 0,
                        controls: 0,
                        modestbranding: 1
                    },
                    events: {
                        'onReady': onPlayerReady,
                        'onStateChange': onPlayerStateChange,
                        'onError': onError
                    }
                });
            }

            $('iframe').each(function () {
                var src = $(this).attr('src');
                $(this).attr('src', src + '&wmode=transparent');
            });
        }

        // 4. The API will call this function when the video player is ready.
        function onPlayerReady(event) {
            event.target.mute();
            event.target.loadPlaylist(playlistArr, 0, startTime, "large");
            event.target.setLoop(false);
            event.target.playVideo();
        }

        function GetVideoByProviderVideoId(videoId) {
            var video = null;
            if (playlist && playlist.length > 0) {
                for (var i = 0; i < playlist.length; i++) {
                    if (playlist[i].ProviderVideoId == videoId) {
                        video = playlist[i];
                    }
                }
            }

            return video;
        };
        function Unmute() {
            player.unMute();
            $("#btnSound").removeClass("mute").addClass("muteOFF").removeAttr('onclick').attr("onclick","Mute()");

        };

        function Mute() {
            player.mute();
            $("#btnSound").removeClass("muteOFF").addClass("mute").removeAttr('onclick').attr("onclick", "Unmute()");;
        }
        function CheckForEndOfPlaylist(currentVideoId, event) {
            if ((event.target.getPlaylistIndex() == playlistArr.length - 1)) {
                var currentTime = getClientTime();
                $.ajax({
                    type: "POST",
                    url: webMethodAddVideoViewEnd,
                    data: '{"videoTubeId":' + currentVideoId + ',"viewEndTime":' + "'" + currentTime + "'" + '}',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response.d && response.d.IsSuccess) {
                            //console.log("Successfully added a new video view");
                        }
                    },
                    complete: function () {
                        if (!done) {
                            if (event.data == 0) {
                                done = true;
                                document.location.reload(true);
                            }
                        }
                    }
                });
            }
        };

        function onPlayerStateChange(event) {

            if (event.data == YT.PlayerState.PLAYING) {
                var req = event.target.requestFullScreen;
                req.call(event.target);
            }
            else if (event.data == YT.PlayerState.BUFFERING) {
                void 0;
            }
            else if (event.data == YT.PlayerState.CUED) {
                void 0;
            }
            else if (event.data == YT.PlayerState.PAUSED) {
                void 0;
            }
            else if (event.data == YT.PlayerState.ENDED) {
                void 0;
                CheckForEndOfPlaylist(currentlyPlayingVideo.VideoTubeId, event);
            }
            else if (event.data == YT.PlayerState.UNSTARTED) {
                void 0;

                var unstartedVideoPlaylistIndex = event.target.getPlaylistIndex();

                if (unstartedVideoPlaylistIndex >= 0) {

                    var unstartedVideo = GetCurrentlyPlayingVideo(unstartedVideoPlaylistIndex);

                    if (unstartedVideo == undefined) {
                        var url = event.target.getVideoUrl();
                        // "https://www.youtube.com/watch?v=gzDS-Kfd5XQ&feature=..."
                        var match = url.match(/[?&]v=([^&]+)/);
                        // ["?v=gzDS-Kfd5XQ", "gzDS-Kfd5XQ"]
                        var videoId = match[1];
                        unstartedVideo = GetCurrentVideoByProviderVideoId(videoId);
                    }

                    var prevVideoId;
                    var playingNext;
                    var r_rated;

                    if (currentlyPlayingVideo == undefined || unstartedVideo.VideoTubeId != currentlyPlayingVideo.VideoTubeId) {
                        if (currentlyPlayingVideo == undefined) {
                            prevVideoId = -1;
                        }
                        else {
                            prevVideoId = currentlyPlayingVideo.VideoTubeId;
                        }

                        currentlyPlayingVideo = unstartedVideo;
                        playBackOrderNum = currentlyPlayingVideo.PlaybackOrderNumber;
                        currVideoTitle = currentlyPlayingVideo.VideoTubeTitle;
                        playingVideoId = currentlyPlayingVideo.VideoTubeId;
                        playingNext = playBackOrderNum + 1;
                        r_rated = currentlyPlayingVideo.IsRRated;

                       
                        $(".webHolderRight .playingNowHome").hide();
                        $(".webHolderRight  .playingNextHome").hide();
                       
                        $(".webHolderRight  #order_" + playBackOrderNum + " .playingNowHome").show();
                        $(".webHolderRight  #order_" + playingNext + " .playingNextHome").show();

                        console.log(playBackOrderNum+", "+ playingNext)

                      
                        

                      
                    }
                }
            }
        }

        function onError(event) {
            var e = event;
        }

        function stopVideo() {
            player.stopVideo();
        }

        function startVideo() {
            player.startVideo();
        };

        function ReCreatePlayerBox() {
            var PlayerHolderHtml = "<div id='dMotionPlayer'>" +
                                  "</div>" +
                                  "<div class='playerbox' id='player'>" +
                                  "<iframe class='playerbox video-tracking' style='display:none;' id='myVideo'  width='100%' height='560' data-frameborder='0' data-webkitallowfullscreen='true' data-mozallowfullscreen='true' data-allowfullscreen='true'>" +
                                  "</iframe>" +
                                  "</div>";
            $("#PlayerHolder").html("").html(PlayerHolderHtml);
        }
        function InitVimeoPlayer(playList, playBackOrderNumber, startTime) {
            ReCreatePlayerBox()
            $('.video-tracking').css("display", "block");
            // $("#dMotionPlayer").css("display", "none");
            console.log(playList[playBackOrderNumber].ProviderVideoId);
            playingVideoId = playlist[playBackOrderNumber].VideoTubeId;
            // //console.log("init vimeo player")
            //var src = "//player.vimeo.com/video/15321875?api=1&player_id=myVideo;autoplay=1"
            ////console.log("here");
            var iframe;
            window.$('.video-tracking').each(function () {


                // get the vimeo player(s)
                iframe = window.$(this);
                iframe.attr("src", "//player.vimeo.com/video/" + playlist[playBackOrderNumber].ProviderVideoId + "?api=1&player_id=myVideo;autoplay=1");

                player = window.$f(iframe[0]);
                //console.log(player);
                // When the player is ready, add listeners for pause, finish, and playProgress
                player.addEvent('ready', function () {
                    //console.log('Vimeo player \'' + iframe.attr('id') + '\': ready');

                    //these are the three standard events Vimeo's API offers
                    player.addEvent('play', onPlay);
                    player.addEvent('pause', onPause);
                    player.addEvent('finish', onFinish);
                    player.api('seekTo', startTime + 1);
                });
            });

            //define the custom events to push to Optimizely
            //appending the id of the specific video (dynamically) is recommended

            //to make this script extensible to all possible videos on your site
            function onPause(id) {

                //console.log('Vimeo player \'' + id + '\': pause');
                window['optimizely'] = window['optimizely'] || [];
                window.optimizely.push(["trackEvent", id + "Pause"]);
            }

            function onFinish(id) {
                playBackOrderNumber++;
                if (playBackOrderNumber != 0) {
                    startTime = 0;
                }

                if (playList.length - 1 < playBackOrderNumber) {
                    $("#PlayerHolder").toggle();
                    $("#playerImage").removeAttr("src").attr("src", "/images/thankYouChannelIL.jpg");
                    $("#playerImage").toggle();
                }
                else {

                    ////console.log(playBackOrderNumb);
                    if (playList[playBackOrderNumber].VideoProviderName == "vimeo") {

                        iframe.removeAttr("src").attr("src", "//player.vimeo.com/video/" + playList[playBackOrderNumber].ProviderVideoId + "?api=1&player_id=myVideo;autoplay=1");
                    }
                    else if (playList[playBackOrderNumber].VideoProviderName == "youtube") {
                        if (playBackOrderNumber != 0) {
                            startTime = 0;
                        }
                        iframe.removeAttr("src").css("display", "none");
                        InitYoutubePlayer(playList, playBackOrderNumber, startTime);
                    }
                    else {
                        if (playBackOrderNumber != 0) {
                            startTime = 0;
                        }
                        iframe.removeAttr("src").css("display", "none");
                        InitDMotionPlayer(playList, playBackOrderNumber, startTime);
                    }
                }


                //console.log('Vimeo player \'' + id + '\': finish');
                window['optimizely'] = window['optimizely'] || [];
                window.optimizely.push(["trackEvent", id + "Finish"]);
            }

            function onPlay(id) {
                $("#ChannelPageInfoHolder .videoTitle h1").text("").text(playList[playBackOrderNumber].VideoTubeTitle);
                var orderNumber = playList[playBackOrderNumber].PlaybackOrderNumber;
                $(".sideContentHolder a.sideSchedule").removeClass("playingNow");
                $(".sideContentHolder a.sideSchedule .playingNowShow").hide();
                $(".sideContentHolder a.sideSchedule .playingNextShow").hide();
                $(".sideContentHolder a#order_" + orderNumber).addClass("playingNow");
                $(".sideContentHolder a#order_" + orderNumber + " .playingNowShow").show();
                var playingNext = orderNumber + 1;
                void 0;
                $(".sideContentHolder a#order_" + playingNext + " .playingNextShow").show();

                window['optimizely'] = window['optimizely'] || [];
                window.optimizely.push(["trackEvent", id + "Play"]);
            }
        }

        function InitDMotionPlayer(playList, playBackOrderNumber, startTime) {

            ReCreatePlayerBox();
            //$("#dMotionPlayer").css("display", "block");

            var dplayer = DM.player("dMotionPlayer", { video: playList[playBackOrderNumber].ProviderVideoId, width: "100%", height: "560", params: { autoplay: 1, html: 1, allowfullscreen: 1, start: startTime } });

            // 4. We can attach some events on the player (using standard DOM events)
            DmotionPlayerPlay(dplayer, playList, playBackOrderNumber, startTime);
            DmotionPlayerFinishPlay(dplayer, playList, playBackOrderNumber, startTime);

        };

        function DmotionPlayerFinishPlay(dplayer, playList, playBackOrderNumber, startTime) {


            dplayer.addEventListener("ended", function (e) {
                playBackOrderNumber++;
                if (playBackOrderNumber != 0) {
                    startTime = 0;
                }

                if (playList.length - 1 < playBackOrderNumber) {
                    $("#PlayerHolder").toggle();
                    $("#playerImage").removeAttr("src").attr("src", "/images/thankYouChannelIL.jpg");
                    $("#playerImage").toggle();
                }
                else {
                    if (playList[playBackOrderNumber].VideoProviderName == "vimeo") {


                        InitVimeoPlayer(playList, playBackOrderNumber, startTime);
                    }
                    else if (playList[playBackOrderNumber].VideoProviderName == "youtube") {
                        if (playBackOrderNumber != 0) {
                            startTime = 0;
                        }

                        InitYoutubePlayer(playList, playBackOrderNumber, startTime);
                    }
                    else {
                        InitDMotionPlayer(playList, playBackOrderNumber, startTime)
                    }
                }
            });
        }

        function DmotionPlayerPlay(dplayer, playList, playBackOrderNumber, startTime) {
            $("#player.playerbox").hide();
            dplayer.addEventListener("apiready", function (e) {

                $("#ChannelPageInfoHolder .videoTitle h1").text("").text(playList[playBackOrderNumber].VideoTubeTitle);
                var orderNumber = playList[playBackOrderNumber].PlaybackOrderNumber;
                $(".sideContentHolder a.sideSchedule").removeClass("playingNow");
                $(".sideContentHolder a.sideSchedule .playingNowShow").hide();
                $(".sideContentHolder a.sideSchedule .playingNextShow").hide();
                $(".sideContentHolder a#order_" + orderNumber).addClass("playingNow");
                $(".sideContentHolder a#order_" + orderNumber + " .playingNowShow").show();
                var playingNext = orderNumber + 1;

                $(".sideContentHolder a#order_" + playingNext + " .playingNextShow").show();
                e.target.play();

            });
        };

        function InitYoutubePlayer(playList, playbackOrderNumber, startTime) {
            ReCreatePlayerBox();
            startTime = parseInt(parseFloat(playList[playbackOrderNumber].PlayerPlaybackStartTimeInSec).toFixed());

            function onYouTubeIframeAPIReady() {

                //if ($.browser.device = (/android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase()))) {
                //    player = new YT.Player('player', {
                //        height: '546',
                //        width: '728',

                //        //videoId: 'M7lc1UVf-VE',
                //        playerVars: {
                //            //controls: 0,
                //            showinfo: 0,
                //            autoplay: 1
                //             //html5: 0

                //        },
                //        events: {
                //            'onReady': OnPlayerReady,
                //            'onStateChange': OnPlayerStateChange
                //        }
                //    });
                //}

                //else {
                player = new YT.Player('player', {
                    height: '560',
                    width: '100%',
                    playerVars: {
                        autoplay: 1,
                        html5: 1,
                        rel: 0,
                        controls: 0,
                        modestbranding: 1

                    },
                    events: {
                        'onReady': OnPlayerReady,
                        'onStateChange': OnPlayerStateChange
                    }
                });

                $('iframe').each(function () {
                    var src = $(this).attr('src');
                    $(this).attr('src', src + '&wmode=transparent');
                });
            };

            function OnPlaybackQualityChange(eventQualityChanged) {
                //console.log(eventQualityChanged);
            };

            function OnApiChange(eventApiChanged) {
                //console.log(eventApiChanged);
            };

            function OnPlayerStateChange(eventStateChanged) {
                console.log("OnPlayerStateChange");
                playingVideoId = playList[playbackOrderNumber].VideoTubeId;

                if (playbackOrderNumber >= playList.length) {
                    playBackOrderNumber = playList.length - 1;
                }

                var orderNumber = playList[playbackOrderNumber].PlaybackOrderNumber;

                $(".sideContentHolder a.sideSchedule").removeClass("playingNow");
                $(".sideContentHolder a.sideSchedule .playingNowShow").hide();
                $(".sideContentHolder a.sideSchedule .playingNextShow").hide();
                $(".sideContentHolder a#order_" + orderNumber).addClass("playingNow");
                $(".sideContentHolder a#order_" + orderNumber + " .playingNowShow").show();
                $(".sideContentHolder a#order_" + playingNext + " .playingNextShow").show();
                var playingNext = orderNumber + 1;
                $(".sideContentHolder a#order_" + playingNext + " .playingNextShow").show();

                //onFinish

                if (eventStateChanged.data == 0) {
                    playbackOrderNumber++;

                    if (playBackOrderNum != 0) {
                        startTime = 0;
                    }

                    if (playList.length - 1 < playbackOrderNumber) {
                        $("#PlayerHolder").toggle();
                        $("#playerImage").removeAttr("src").attr("src", "/images/thankYouChannelIL.jpg");
                        $("#playerImage").toggle();
                    }
                    else {
                        if (playList[playbackOrderNumber].VideoProviderName == "vimeo") {
                            player.destroy();
                            InitVimeoPlayer(playList, playbackOrderNumber, startTime);
                        }
                        else if (playList[playbackOrderNumber].VideoProviderName == "youtube") {
                            if (playBackOrderNum != 0) {
                                startTime = 0;
                            }
                            InitYoutubePlayer(playList, playbackOrderNumber, startTime)
                        }
                        else {
                            console.log("on else of Dmotion init")
                            InitDMotionPlayer(playList, playbackOrderNumber, startTime)
                        }
                    }
                }
            }

            function OnPlayerReady(event) {
                void 0;
                event.target.loadVideoById({ videoId: playList[playbackOrderNumber].ProviderVideoId, startSeconds: startTime, suggestedQuality: "large" })
                event.target.setLoop(false);
                event.target.playVideo();

                $("#ChannelPageInfoHolder .videoTitle h1").text("").text(playList[playbackOrderNumber].VideoTubeTitle)
            };

            function onYouTubeIframeAPIReadyIE() {
                if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                    $("iframe.playerbox").attr("allowfullscreen");
                    player = new YT.Player('player', {
                        height: '560',
                        width: '100%',
                        playerVars: {
                            autoplay: 1,
                            html5: 1,
                            rel: 0,
                            controls: 0,
                            modestbranding: 1
                        },
                        events: {
                            'onReady': OnPlayerReady,
                            'onStateChange': onPlayerStateChange
                        }
                    });
                }

                $('iframe').each(function () {
                    var src = $(this).attr('src');
                    $(this).attr('src', src + '&wmode=transparent');
                });
            }

            if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                OnYouTubeIframeAPIReadyIE();
            }
            else {
                onYouTubeIframeAPIReady();
            }

            var done = false;
        }

        function GetCurrentlyPlayingVideo(currentPlaylistIndex) {
            if (playlist && playlist.length > 0) {
                if (currentPlaylistIndex < 0) {
                    currentPlaylistIndex = 0;
                }
                else if (currentPlaylistIndex > (playlist.length - 1)) {
                    currentPlaylistIndex = playlist.length - 1;
                }
            }
            else {
                return null;
            }

            return playlist[currentPlaylistIndex];
        }

        function GetPreviouslyPlayedVideo(currentPlaylistIndex) {
            if (playlist && playlist.length > 0) {
                if (currentPlaylistIndex < 0) {
                    currentPlaylistIndex = 0;
                    return null;
                }
                else if (currentPlaylistIndex > (playlist.length - 1)) {
                    currentPlaylistIndex = playlist.length - 1;
                }
            }
            else {
                return null;
            }

            return playlist[currentPlaylistIndex - 1];
        }

        function GetNextVideoToPlay(currentPlaylistIndex) {
            if (playlist && playlist.length > 0) {
                if (currentPlaylistIndex < 0) {
                    currentPlaylistIndex = 0;
                }
                else if (currentPlaylistIndex > (playlist.length - 1)) {
                    currentPlaylistIndex = playlist.length - 1;
                    return null;
                }
            }
            else {
                return null;
            }

            return playlist[currentPlaylistIndex + 1];
        };
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="/JS/Controls.js" type="text/javascript">


    </script>


    <div id="homesliderHN">

        <div class="webHolderLeft">
            <div class="channelWatchig"> Fashion Tv</div>
            <div class="channelWatchigDescription">
                <p>Lorem ipsum dolor sit amet, est ut falli quaestio, magna timeam vidisse et vel, cu ius civibus prodesset. Modo laoreet nominavi vis eu, eos cu enim graecis, nam at saepe perfecto nominati. Eu vis rebum persequeris, sonet nominati an pri. Delenit philosophia ea vis, his officiis scribentur te, in diam quidam nec. Dicit interesset an usu, at eirmod dissentiet usu.</p>

</div>
            <div class="channelCreator">
                <a  runat="server" id="ancCreatorLink">
                <asp:Image ID="imgCreator" runat="server" CssClass="channelCreatorImg"></asp:Image> 
                <span class="spnChannelCreator"> creator</span>
                 <asp:Label runat="server" ID="spnCreatorName" CssClass="spnChannelCreatorName"></asp:Label>
                    </a>
            </div>
            <div class="mute" onclick="Unmute()" id="btnSound"></div>
        </div>
         <div class="webHolder  block">
             <div id="PlayerHolderHome">
                <div class="playerbox" id="player">
                    <iframe class="playerbox video-tracking" id="myVideo"  width="100%" height="560" data-frameborder="0" data-webkitallowfullscreen="true" data-mozallowfullscreen="true" data-allowfullscreen="true"></iframe>
                    <asp:Image ID="Image1" runat="server" />
                </div>
            </div>
         </div>

        <div class="webHolderRightWrapper">
         <div class="webHolderRight">
             

         </div>
            </div>
    </div>
    <div class="channelsBG">
        <div class="televisionByPeople">
            <h2 class="televisionByPeopleH2">Television Created By People Like You</h2>
            <div class="homeActionsHolder">
                 <div class="btnCreate"> create  your channel</div>
                 <a class="watchVideoIcon200" onclick="ShowTutorialPlayer('kVopERCG2j4')"></a>
            </div>
            <a class="whatsPlayingNowIconHN" href="home#entertainmentGroup"></a>
        </div>




        <a name="entertainmentGroup"></a>
       
        <div class="whatIskWrapper">
        <div class="homeBlocksHolder" id="homeBlocksHolder" runat="server">

          <%--  <div id="channelGroupTop" class="entertainment">
                <h1 id="channelGroupTopTitle" class="video-box-h1">TOP Channels</h1>
                <div id="channelHolderTop" runat="server">
                </div>
            </div>--%>
          <%--  REPLACED BY UC CHANNELSHOLDERCATEGORYUC--%>
             
            </div>
            <div class="whatIs">
                <div class="whatIsTitle">How it Works</div>
                <div class="whatIsBlock">
                    <div class="whatIsIcon">
                    <img src="images/whatIsIcons-create.png" />
                    </div>
                    <div class="whatIsStep"> 1. Create Your Channel</div>
                     <div class="whatIsDescription">Sign up & create you own network of channels. Its free!</div>
                </div>
                <div class="whatIsBlock">
                    <div class="whatIsIcon">
                        <img src="images/whatIsIcons-add.png" />
                         
                    </div>
                     <div class="whatIsStep"> 2. Add Videos</div>
                    <div class="whatIsDescription"> Search for videos directly on Strimm<br/> and add them to your channel.</div>
                </div>
                <div class="whatIsBlock">
                    <div class="whatIsIcon">
                        

                        <img src="images/whatIsIcons-broadcast.png" />
                    </div>
                     <div class="whatIsStep"> 3. Broadcast</div>
                    <div class="whatIsDescription"> Schedule selected videos on specific days and times.<br/> Then share it! </div>
                </div>
                <div class="whatIslearnMore">
                <a href="LearnMore.aspx" class="btnWhatIslearnMore">Learn More</a>
            </div>

            </div>
            <div class="spacer" style="height: 30px;"></div>
        </div>
            

            
            </div>
  






    <div class="homeBlocksHolder footerHomeBlock">
        <div id="divFooterNH" class="default regular">

            <h1 class="paragraphSloganBlackNH plainNH">Continuous broadcast from your own channel</h1>
            <div class="bottomButtonsHolderNH">
                <a class="bottomSignUpNH" href="/sign-up">Sign Up. It's Free </a>
                <a class="learnMoreHomeNH" href="/learn-more">Learn More</a>
            </div>

            <div class="holderNH">

                <div class="column columnHN">
                    <a href="Default.aspx" class="logoFooterLInk">
                        <div class="logoFooter"></div>
                    </a>

                    <div class="column columnHN columnFooterSocial">

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
                        <li><a href="/press-release">Press</a></li>
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









                <div class="column columnHN">
                    <h3>Legal</h3>
                    <ul>
                        <li><a href="/copyright">Copyright Policy</a></li>
                        <li><a href="/privacy-policy">Privacy Policy</a></li>
                        <li><a href="/terms-of-use">Terms of Use</a></li>

                    </ul>
                </div>

                <div id="divAllRights">
                    <span>&#169;2015-2016 Strimm, Inc. |  All Rights reserved </span>
                    <%--  <a href="Copyrights.aspx" class="copyrights"> / &nbsp; copyright policy</a>--%>
                </div>
            </div>
        </div>

    </div>



</asp:Content>
