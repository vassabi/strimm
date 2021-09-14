<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Default_sideView.aspx.cs" Inherits="StrimmTube.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Strimm TV – Free Online Video Platform to Create Your Own TV Network
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Strimm TV is a free online video platform to create your own TV network or watch channels online." />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://www.strimm.com" />

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <%: System.Web.Optimization.Styles.Render("~/bundles/home/css") %>


 
<%--use HomeNew.css--%>

<%--    <link href="css/DefaultCSS.css" rel="stylesheet" />--%>
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="//www.google.com/jsapi" type="text/javascript"></script>
    <script src="https://www.youtube.com/iframe_api"></script>
    <script src="/JS/Main.js"></script>
      <script src="/JS/Froogaloop.js"></script> 
    <script type="text/javascript">
        google.load("swfobject", "2.1");
    </script>
    <script src="Plugins/Scroller/nanoscroller.min.js"></script>
    <link href="Plugins/Scroller/scroller.css" rel="stylesheet" />


    <script type="text/javascript">
        var username = "<%=UserName%>";
        var hideFooter = "<%=HideOldFooter%>";
        var playingChannelId = "<%=playingChannelId%>";
        var webMethodGetChannelPreviewDataJson = "/WebServices/ChannelWebService.asmx/GetCurrentlyPlayingChannelData";
        var isPlayerOnMute = true;
        var width = $(window).width();
        var autoplay = 1;
        var startTime = 0;

        function isMobile() {
            return (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino|android|ipad|playbook|silk/i.test(navigator.userAgent || navigator.vendor || window.opera) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test((navigator.userAgent || navigator.vendor || window.opera).substr(0, 4)))
        };

        $(document).ready(function () {

            if (isMobile()) autoplay = 0;

            GetPlayingChannelScheduleData();
            var timeOfRequest = getClientTime();
            $("#hiddenTime").val(timeOfRequest);
            //console.log(width);
            if (width <= 1499) {
                setTimeout(function () {
                    HideInfoBar();
                    HideSchedule();
                }, 9000);

            }
            else {
                ShowInfoBar();
                ShowSchedule();
            }



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



        $(window).resize(function () {
            if ($(this).width() != width) {
                width = $(this).width();
                //console.log("width: " + width);
            }
            if (width <= 1499) {
                setTimeout(function () {
                    HideInfoBar();
                    HideSchedule();
                }, 3000);

            }
            else {
                ShowInfoBar();
                ShowSchedule();
            }

        });


        var playerLandingPage;
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
            var now = new Date();
            var timeOfRequest = now.format("MM-dd-yyyy-H-mm");

            $("#homesliderHN #loadingDiv").show();

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
                        //console.log(playlist);
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

                },
                complete: function () {
                    $("#homesliderHN #loadingDiv").hide();
                    $(".nano").nanoScroller({ alwaysVisible: false });
                }

            });
        };

        function onYouTubeIframeAPIReady() {
            if (isMobile())
            {
                //console.log("isMobile")
                playerLandingPage = new YT.Player('player', {
                    height: '560',
                    width: '100%',
                    playerVars: {
                        modestbranding: 1,
                        html5: 1,
                        rel: 0,
                        controls: 0,
                        showinfo: 0
                    },
                    events: {
                        'onReady': onPlayerReadyMobile,
                        'onStateChange': onPlayerStateChange,
                        'onError': onError
                    }
                });
            }
            else
            {
                playerLandingPage = new YT.Player('player', {
                    height: '560',
                    width: '100%',
                    playerVars: {
                        modestbranding: 1,
                        html5: 1,
                        autoplay:1,
                        rel: 0,
                        controls: 0,
                        showinfo: 0
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

        function onYouTubeIframeAPIReadyIE() {
            if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                $("iframe.playerbox").attr("allowfullscreen");
                playerLandingPage = new YT.Player('player', {

                    height: '560',
                    width: '100%',
                    playerVars: {
                        modestbranding: 1,
                      //  autoplay: 0,
                        html5: 1,
                        rel: 0,
                        controls: 0,
                        showinfo: 0
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
            //console.log(playlistArr)
            event.target.mute();
            event.target.loadPlaylist(playlistArr, 0, startTime, "large");
            event.target.setLoop(false);
            event.target.playVideo();
            
        }

      function onPlayerReadyMobile(event) {
            //console.log(playlistArr)
            event.target.mute();
            event.target.cuePlaylist(playlistArr, 0, startTime, "large");
            event.target.setLoop(false);
            //event.target.playVideo();

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
            isPlayerOnMute = false;
            playerLandingPage.unMute(100);
            $("#btnSound").removeClass("mute").addClass("muteOFF").removeAttr('onclick').attr("onclick", "Mute()");

        };

        function Mute() {
            isPlayerOnMute = true;
            playerLandingPage.mute();
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
                // req.call(event.target);
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
                        var prevPlayed = 0;
                        if (playBackOrderNum != 0) {
                            prevPlayed = playBackOrderNum - 1
                        }





                        $(".webHolderRight  #order_" + playBackOrderNum + " .playingNowHome").show();
                        $(".webHolderRight  #order_" + playingNext + " .playingNextHome").show();
                        $(".webHolderRight  #order_" + prevPlayed).hide();

                        //console.log(playBackOrderNum + ", " + playingNext)





                    }
                }
            }
        }

        function onError(event) {
            var e = event;
        }

        function stopVideo() {
            playerLandingPage.stopVideo();
        }

        function startVideo() {
            playerLandingPage.startVideo();
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
            //console.log(playList[playBackOrderNumber].ProviderVideoId);
            playingVideoId = playlist[playBackOrderNumber].VideoTubeId;
            // //console.log("init vimeo player")
            //var src = "//player.vimeo.com/video/15321875?api=1&player_id=myVideo;autoplay=1"
            ////console.log("here");
            var iframe;
            window.$('.video-tracking').each(function () {


                // get the vimeo player(s)
                iframe = window.$(this);
                iframe.attr("src", "//player.vimeo.com/video/" + playlist[playBackOrderNumber].ProviderVideoId + "?api=1&player_id=myVideo;autoplay=1");

                playerLandingPage = window.$f(iframe[0]);
                //console.log(player);
                // When the player is ready, add listeners for pause, finish, and playProgress
                playerLandingPage.addEvent('ready', function () {
                    //console.log('Vimeo player \'' + iframe.attr('id') + '\': ready');

                    //these are the three standard events Vimeo's API offers
                    playerLandingPage.addEvent('play', onPlay);
                    playerLandingPage.addEvent('pause', onPause);
                    playerLandingPage.addEvent('finish', onFinish);
                    playerLandingPage.api('seekTo', startTime + 1);
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

            var dplayer = DM.player("dMotionPlayer", { video: playList[playBackOrderNumber].ProviderVideoId, width: "100%", height: "560", params: { autoplay: 0, html: 1, allowfullscreen: 1, start: startTime } });

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

                playerLandingPage = new YT.Player('player', {
                    height: '560',
                    width: '100%',
                    playerVars: {
                        modestbranding: 1,
                      //  autoplay: 0,
                        html5: 1,
                        rel: 0,
                        controls: 0,
                        showinfo: 0

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
                //console.log("OnPlayerStateChange");
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
                            playerLandingPage.destroy();
                            InitVimeoPlayer(playList, playbackOrderNumber, startTime);
                        }
                        else if (playList[playbackOrderNumber].VideoProviderName == "youtube") {
                            if (playBackOrderNum != 0) {
                                startTime = 0;
                            }
                            InitYoutubePlayer(playList, playbackOrderNumber, startTime)
                        }
                        else {
                            //console.log("on else of Dmotion init")
                            InitDMotionPlayer(playList, playbackOrderNumber, startTime)
                        }
                            }
                        }
                    }

            //function OnPlayerReady(event) {
            //    void 0;
            //    event.target.loadVideoById({ videoId: playList[playbackOrderNumber].ProviderVideoId, startSeconds: startTime, suggestedQuality: "large" })
            //    event.target.setLoop(false);
            //  //  event.target.playVideo();

            //    $("#ChannelPageInfoHolder .videoTitle h1").text("").text(playList[playbackOrderNumber].VideoTubeTitle)
            //};

            function onYouTubeIframeAPIReadyIE() {
                if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                    $("iframe.playerbox").attr("allowfullscreen");
                    playerLandingPage = new YT.Player('player', {
                        height: '560',
                        width: '100%',
                        playerVars: {
                            modestbranding: 1,
                            autoplay: 0,
                            html5: 1,
                            rel: 0,
                            controls: 0,
                            showinfo: 0
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

        function showChannelDescription() {
            $(".descriptionHolder").addClass("descriptionFull");
            $(".channelCreator").removeClass("aboutFull").hide();
            $(".descriptionHolder .moreInfo").text("").text("Less").removeAttr("onclick").attr("onclick", "hideChannelDescription()");
        };

        function showCreatorBio() {
            $(".descriptionHolder").removeClass("descriptionFull").hide();
            $(".channelCreator").addClass("aboutFull");
            $(".channelCreator .moreInfo").text("").text("Less").removeAttr("onclick").attr("onclick", "hideCreatorBio()");
        };

        function hideChannelDescription() {
            $(".descriptionHolder").removeClass("descriptionFull").show();
            $(".channelCreator").removeClass("aboutFull").show();
            $(".descriptionHolder .moreInfo").text("").text("More").removeAttr("onclick").attr("onclick", "showChannelDescription()");
        };

        function hideCreatorBio() {
            $(".descriptionHolder").removeClass("descriptionFull").show();
            $(".channelCreator").removeClass("aboutFull").show();
            $(".channelCreator .moreInfo").text("").text("More").removeAttr("onclick").attr("onclick", "showCreatorBio()");
        };


        function ShowInfoBar() {

            $(".showInfoBar").hide();
            $(".hideInfoBlock").show('slide', { direction: 'left' }, 1000);
        };

        function HideInfoBar() {

            $(".hideInfoBlock").hide('slide', { direction: 'left' }, 1000);
            $(".showInfoBar").show();
        };

        function ShowSchedule() {

            $(".showScheduleWrapper").hide();
            $(".scheduleScrollHomePage").show('slide', { direction: 'right' }, 1000);
        };

        function HideSchedule() {

            $(".scheduleScrollHomePage").hide('slide', { direction: 'right' }, 1000);
            $(".showScheduleWrapper").show();
        }

    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="/JS/Controls.js" type="text/javascript">


    </script>

    <asp:HiddenField ID="hiddenTime" runat="server" ClientIDMode="Static" />
    <div id="homesliderHN">
   <div class="webHolderRightWrapperOverlay"></div>
        
<%--        <div id="loadingDiv">
            <div id="loadingDivHolder">
                <img src="/images/ajax-loader(3).gif" />
            </div>
        </div>--%>
         

            <div class="showInfoBar" onclick="ShowInfoBar()">
                <span class="infoShowIcon"></span>
                <span class="infoShowIconInfo"></span>
            </div>
        <div class="webHolderHidden">

  <div class="hideInfoBlock" >
      <a id="btnHide" onclick="HideInfoBar()">Hide</a>
        <div class="webHolderLeft">


            <div class="descriptionHolder">

                <%--<asp:Image ID="imgChannel" runat="server" CssClass="channelChannelImg"></asp:Image>  --%>
            <div class="channelWatchig"> <a runat="server" id="hrefToChannel" class="hrefToChannel"><%=channelOnlandingPageName%></a></div>
            <div class="channelWatchigDescription">

                <p>    <asp:Image runat="server" ID="imgChannel" CssClass="channelChannelImg" />
                   <%=channelDescription%>  </p>
                <div class="moreInfo" onclick="showChannelDescription()">More</div>
                </div>

               </div>



            <div class="channelCreator">
<a  runat="server" id="ancCreatorLink"  class="ancCreatorLink">

                <span class="spnChannelCreator"> creator:</span>
                 <asp:Label runat="server" ID="spnCreatorName" CssClass="spnChannelCreatorName"></asp:Label>
                    </a>
                            <div class="channelCreatorBIO">

                <p>
                       <asp:Image ID="imgCreator" runat="server" CssClass="channelCreatorImg"/>
                   <%=chCreatorBio %> </p>
                                <div class="moreInfo" onclick="showCreatorBio()">More</div>
                                    </div>
                                


                                </div>
            <%--<div class="mute" onclick="Unmute()" id="btnSound"></div>--%>
                            </div>
                    </div>

            </div>


         <div class="webHolder  block"> 

             <div id="PlayerHolderHome" class="PlayerHolderHome">
                     <div class="mute" onclick="Unmute()" id="btnSound"></div> 
                <div class="playerbox" id="player">

                    <iframe class="playerbox video-tracking" id="myVideo"  width="100%" height="560" data-frameborder="0" data-webkitallowfullscreen="true" data-mozallowfullscreen="true" data-allowfullscreen="true"></iframe>
                    <asp:Image ID="Image1" runat="server" />
                                </div>
                                </div>
                            </div>

     <div class="webHolderRightWrapper">
     <div class="showScheduleWrapper">

     <a class="showSchedule" onclick="ShowSchedule()"></a>
     <a class="showScheduleIcon" onclick="ShowSchedule()"></a>

                            </div>

        
     <div class="nano scheduleScrollHomePage">

     <div class="content">
   <div class="hideInfoScheduleBlock">

     <a class="hideSchedule" onclick="HideSchedule()">Hide</a>
     <div class="webHolderRight">
                                </div>
                                </div>
                            </div>

                    </div>
                </div>

            </div>

    <div class="channelsBG">
        <div class="televisionByPeople">
            <h2 class="televisionByPeopleH2">Television Created By People Like You</h2>
            <div class="homeActionsHolder" >
                 <div class="btnCreate" onclick="TriggerCreateChannel()"> create  your channel</div>
                 <a class="watchVideoIcon200" onclick="ShowTutorialPlayer('<%=TutorialVideoId%>', true)"></a>
    </div>
            <a class="whatsPlayingNowIconHN" href="home#entertainmentGroup"></a>
        </div>




        <a name="entertainmentGroup"></a>
       
        <div class="whatIskWrapper">
        <div class="homeBlocksHolder" id="homeBlocksHolder" runat="server">
            <a href="browse-channel?category=Most%20Popular" class="seeAllChannels"> see all channels</a>
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
                    <div class="whatIsStep" > 1. Create Your Channel</div>
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
                        <li><a href="/press">Press</a></li>
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

