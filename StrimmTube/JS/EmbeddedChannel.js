var webMethodGetChannelPreviewDataJson = "/WebServices/ChannelWebService.asmx/GetCurrentlyPlayingChannelData";
var webMethodGetTopChannelsOnTheAirJson = "/WebServices/ChannelWebService.asmx/GetTopChannelsOnTheAir";
var webMethodGetFavoriteChannelsOnTheAirForUserJson = "/WebServices/ChannelWebService.asmx/GetFavoriteChannelsOnTheAirForUser";
var WebMethodGetCurrentlyPlayingChannelsByCategoryNameForChannel = "/WebServices/ChannelWebService.asmx/GetCurrentlyPlayingChannelsByCategoryNameForChannel";
var webMethodAddVideoView = "/WebServices/ChannelWebService.asmx/AddVideoViewsOnChangeForUser";
var webMethodAddVideoViewEnd = "/WebServices/ChannelWebService.asmx/AddVideoViewEndForUser";
var webMethodAddChannelCount = "/WebServices/ChannelWebService.asmx/AddChannelViewForUser";
var webMethodAddVideoViewStartForUser = "/WebServices/ChannelWebService.asmx/AddVideoViewStartForUser";
var webMethodAddToArchive = "/WebServices/ChannelWebService.asmx/AddToArchive";
var webMethodPollRings = "/WebServices/ChannelWebService.asmx/PollRings";
var webMethodIsNewRing = "/WebServices/ChannelWebService.asmx/IsNewRing";
var webMethodSunscribeToChannel = "/WebServices/ChannelWebService.asmx/SubscribeUserForChannel";
var webMethodUnsuscribeFromChannel = "/WebServices/ChannelWebService.asmx/UnsubscribeUserFromChannel";
var webMethodAddVideoToArchive = "/WebServices/ChannelWebService.asmx/AddVideoToUserArchive";
var webMethodSendAbuseReport = "/WebServices/ChannelWebService.asmx/SendAbuseReport";
var webMethodGetFavoriteChannels = "/WebServices/ChannelWebService.asmx/GetAllFavoriteChannelsForUserByUserIdAndClientTime";
var webMethodGetMyChannels = "/WebServices/ChannelWebService.asmx/GetAllChannelsByUserId";
var webMethodAddUserChannelTubeView = "/WebServices/ChannelWebService.asmx/AddChannelViewForUser";
var webMethodAddLike = "/WebServices/ChannelWebService.asmx/AddLike";
var webMethodUnLike = "/WebServices/ChannelWebService.asmx/UnLike";
var webMethodInsertVisitor = "/WebServices/ChannelWebService.asmx/InsertVisitor";
var webMethodUpdateVisitor = "/WebServices/ChannelWebService.asmx/UpdateVisitor";
var webMethodUpdateVisitDurationByVisitorId = "/WebServices/ChannelWebService.asmx/UpdateVisitDurationByVisitorId";

var player;
var done = false;
var channelScheduleId = 0;
var playlistArr = new Array();
var playlist;
var playingVideoId = 0;
var currVideoTitle;
var currChannelSchedule;
var playBackOrderNum;
var executeSchedulePolling = 1;
var timeoutInMilliseconds;
var SchedulePollingTimer;
var ajaxTopChannels;
var favoriteChannelSetTimeOut;
var topChannelSetTimeOut;
var executeTopChannels = 0;
var currentlyPlayingVideo;
var playlistStr;
var isMatch = false;

var startTime = 0;
var autoplay = 1;
var isPlayerOnMute = true;

// Youtube=1, Vimeo=2, DM=3
var providerId = 1;

var tag = document.createElement('script');

tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

$('.ytp-button-fullscreen-enter').on("click", (function () {
    //console.log("fullscfreen youtube up");
}));


function isMobile() {
    return (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino|android|ipad|playbook|silk/i.test(navigator.userAgent || navigator.vendor || window.opera) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test((navigator.userAgent || navigator.vendor || window.opera).substr(0, 4)))
};

function setAutoplay() {
    if (isMobile()) autoplay = 0;
}

setAutoplay();

//GetScheduleData();

function SchedulePolling() {
    GetScheduleData();
};

function Unmute() {
    isPlayerOnMute = false;
  
    if (player != undefined) {
        if (providerId == 1) {
            player.unMute(100);
        }
        else if (providerId == 3) {
            dplayer.setMuted(false);
        }
        else {
            player.api('setVolume',1);
        }
    }

    $("#btnSound").removeClass("mute").addClass("muteOFF").removeAttr('onclick').attr("onclick", "Mute()");

};

function Mute() {
    isPlayerOnMute = true;
    if (providerId == 1) {
        player.mute();
    }
    else if (providerId == 3) {
        dplayer.setMuted(true);
    }
    else {
        player.api('setVolume', 0);
    }

    $("#btnSound").removeClass("muteOFF").addClass("mute").removeAttr('onclick').attr("onclick", "Unmute()");;
}

// 3. This function creates an <iframe> (and YouTube player)
//    after the API code downloads.
function onYouTubeIframeAPIReady() {
    ResetUIForProperPlayer(true, false);

    player = new YT.Player('player', {
        height: '390',
        width: '640',
        playerVars: {
            autohide: 1,
            showinfo: 0,
            autoplay: autoplay,
            html5: 1,
            allowFullScreen: 1
            //  controls: 0
        },
        events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange,
            'onError': onError
        }
    });
}

function onYouTubeIframeAPIReadyIE() {
    ResetUIForProperPlayer(true, false);

    if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
        $("iframe.playerbox").attr("allowfullscreen");
        player = new YT.Player('player', {
            height: '546',
            width: '728',
            playerVars: {
                autohide: 1,
                controls: 0,
                showinfo: 0,
                autoplay: autoplay,
                html5: 0
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

// This method will be used to play an individual video using YouTube player
// and not the whole playlist.
function InitYoutubePlayer(playList, videoPlaylistIndex, startTime) {
    ResetUIForProperPlayer(true, false);

    var currentVideo = playList[videoPlaylistIndex];

    if (currentVideo) {
        startTime = parseInt(parseFloat(currentVideo.PlayerPlaybackStartTimeInSec).toFixed());
    }
    else {
        startTime = 0;
    }

    providerId = 1;

    function onYouTubeIframeAPIReady() {
        player = new YT.Player('player', {
            height: '546',
            width: '728',
            playerVars: {
                controls: 1,
                showinfo: 0,
                autoplay: autoplay,
                wmode: 'transparent',
                html5: 1
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

    function OnPlayerStateChange(eventStateChanged) {
        //console.log("OnPlayerStateChange");

        if (currentVideo) {
            playingVideoId = currentVideo.VideoTubeId;

            if (videoPlaylistIndex >= playList.length) {
                videoPlaylistIndex = playList.length - 1;
            }

            var orderNumber = currentVideo.PlaybackOrderNumber;

            UpdateUIForNewVideo(videoPlaylistIndex);

            //onFinish

            if (eventStateChanged.data == 0) {
                videoPlaylistIndex++;

                if (videoPlaylistIndex != 0) {
                    startTime = 0;
                }

                player.destroy();

                mutePlayerOnLoad = player.isMuted();

                ProcessNextVideo(videoPlaylistIndex, startTime);
            }
        }
    }

    function OnPlayerReady(event) {
        void 0;
        if (currentVideo) {
            event.target.loadVideoById({ videoId: currentVideo.ProviderVideoId, startSeconds: startTime, suggestedQuality: "large" })
            event.target.setLoop(false);
            event.target.playVideo();
            void 0
            if (mutePlayerOnLoad) {
              
                event.target.mute();
            }
            else
            {
                Unmute();
            }
            


            $("#ChannelPageInfoHolder .videoTitle h1").text("").text(currentVideo.VideoTubeTitle)
        }
    };

    function onYouTubeIframeAPIReadyIE() {
        if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
            $("iframe.playerbox").attr("allowfullscreen");
            player = new YT.Player('player', {
                height: '546',
                width: '728',
                playerVars: {
                    controls: 1,
                    showinfo: 0,
                    autoplay: autoplay,
                    html5: 1
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

// 4. The API will call this function when the video player is ready.
function onPlayerReady(event) {
    if (startTime == undefined) {
        startTime = 0;
    }

    event.target.loadPlaylist(playlistArr, 0, startTime, "large");
    event.target.setLoop(false);

    if (mutePlayerOnLoad) {
        event.target.mute();
    }

    event.target.playVideo();
}

// 5. The API calls this function when the player's state changes.
//    The function indicates that when playing a video (state=1),
//    the player should play for six seconds and then stop.
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
                // "http://www.youtube.com/watch?v=gzDS-Kfd5XQ&feature=..."
                var match = url.match(/[?&]v=([^&]+)/);
                // ["?v=gzDS-Kfd5XQ", "gzDS-Kfd5XQ"]
                if (match != undefined) {
                    var videoId = match[1];
                    unstartedVideo = GetCurrentVideoByProviderVideoId(videoId);
                }
            }

            var prevVideoId;
            var playingNext;
            var r_rated;

            if (currentlyPlayingVideo == undefined || unstartedVideo.VideoTubeId != currentlyPlayingVideo.VideoTubeId) {
                if (currentlyPlayingVideo == undefined) {
                    prevVideoId = -1;
                    playBackOrderNum = 0;
                }
                else {
                    prevVideoId = currentlyPlayingVideo.VideoTubeId;
                    playBackOrderNum = currentlyPlayingVideo.PlaybackOrderNumber;
                }

                mutePlayerOnLoad = event.target.isMuted();

                UpdateUIForNewVideo(playBackOrderNum)

                IncrementVideoCountForVideo(prevVideoId, unstartedVideo.VideoTubeId, event);
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
}
/////
function ReCreatePlayerBox() {
    var PlayerHolderHtml = "<div id='dMotionPlayer'>" +
                          "</div>" +
                          "<div class='playerbox' id='player'>" +
                          "<iframe class='playerbox video-tracking' style='display:none;' id='myVideo'  width='853' height='100%' data-frameborder='0' data-webkitallowfullscreen='true' data-mozallowfullscreen='true' data-allowfullscreen='true'>" +
                          "</iframe>" +
                          "</div>";
    $("#PlayerHolder").html("").html(PlayerHolderHtml);
}

function InitVimeoPlayer(playList, videoPlaylistIndex, startTime) {
    ResetUIForProperPlayer(true, false);

    var currentVideo = playlist[videoPlaylistIndex];
    var iframe;
   
    if (currentVideo) {
        playingVideoId = currentVideo.VideoTubeId;

        window.$('.video-tracking').each(function () {
            // get the vimeo player(s)
            iframe = window.$(this);
            iframe.attr("src", "https://player.vimeo.com/video/" + currentVideo.ProviderVideoId + "?api=1&player_id=myVideo;autoplay=1");

            player = window.$f(iframe[0]);

                //console.log(player);
                // When the player is ready, add listeners for pause, finish, and playProgress
            player.addEvent('ready', function () {
                if (mutePlayerOnLoad) {
                    player.api('setVolume', 0);
                }
                    //console.log('Vimeo player \'' + iframe.attr('id') + '\': ready');

                   //these are the three standard events Vimeo's API offers
                player.addEvent('play', onPlay);
                player.addEvent('pause', onPause);
                player.addEvent('finish', onFinish);
                player.api('seekTo', startTime + 1);
                        });
                    });
                    }

                        //define the custom events to push to Optimizely
    //appending the id of the specific video (dynamically) is recommended

    //to make this script extensible to all possible videos on your site
    function onPause(id) {
    //console.log('Vimeo player \'' + id + '\': pause');
        window['optimizely'] = window['optimizely'] || [];
        window.optimizely.push(["trackEvent", id + "Pause"]);
        }


    function onFinish(id) {

        var player = window.$f(iframe[0]);
       
        player.api('getVolume', function (v) {
            mutePlayerOnLoad = (v == 0);
        });

        videoPlaylistIndex++;

        if (videoPlaylistIndex != 0) {
            startTime = 0;
        }

        var nextVideo = playList[videoPlaylistIndex];

        if (nextVideo) {
            if (playList[videoPlaylistIndex].VideoProviderName == "vimeo") {
                providerId = 2;
                iframe.removeAttr("src").attr("src", "https://player.vimeo.com/video/" + playList[videoPlaylistIndex].ProviderVideoId + "?api=1&player_id=myVideo;autoplay=1");
            }
            else if (playList[videoPlaylistIndex].VideoProviderName == "youtube") {
                providerId = 1;
                if (videoPlaylistIndex != 0) {
                    startTime = 0;
                }
                iframe.removeAttr("src").css("display", "none");
                InitYoutubePlayer(playList, videoPlaylistIndex, startTime);
            }
            else {
                providerId = 3;
                if (videoPlaylistIndex != 0) {
                    startTime = 0;
                }
                iframe.removeAttr("src").css("display", "none");
                InitDMotionPlayer(playList, videoPlaylistIndex, startTime);
            }
        }
        else {
            $("#PlayerHolder").toggle();
            $("#playerImage").removeAttr("src").attr("src", "/images/thankYouChannelIL.jpg");
            $("#playerImage").toggle();
        }

        //console.log('Vimeo player \'' + id + '\': finish');
        window['optimizely'] = window['optimizely'] || [];
        window.optimizely.push(["trackEvent", id + "Finish"]);
    }

    function onPlay(id) {
        UpdateUIForNewVideo(videoPlaylistIndex);

        window['optimizely'] = window['optimizely'] || [];
        window.optimizely.push(["trackEvent", id + "Play"]);
    }
    
    function onPlay(id) {
        UpdateUIForNewVideo(videoPlaylistIndex);

        window['optimizely'] = window['optimizely'] || [];
        window.optimizely.push(["trackEvent", id + "Play"]);
    }
}
var dplayer;
function InitDMotionPlayer(playList, playBackOrderNumber, startTime) {

    ResetUIForProperPlayer(true, true);
    //$("#dMotionPlayer").css("display", "block");

    dplayer = DM.player("dMotionPlayer", { video: playList[playBackOrderNumber].ProviderVideoId, width: "100%", height: "100%", params: { autoplay: autoplay, html: 1, allowfullscreen: 1, start: startTime, mute: isPlayerOnMute } });

    providerId = 3;

    // 4. We can attach some events on the player (using standard DOM events)
    DmotionPlayerPlay(dplayer, playList, playBackOrderNumber, startTime);
    DmotionPlayerFinishPlay(dplayer, playList, playBackOrderNumber, startTime);

    dplayer.addEventListener("volumechange", function (e) {
        mutePlayerOnLoad = !mutePlayerOnLoad;
    });

};

function DmotionPlayerFinishPlay(dplayer, playList, playBackOrderNumber, startTime) {
    dplayer.addEventListener("ended", function (e) {

        playBackOrderNumber++;

        if (playBackOrderNumber != 0) {
            startTime = 0;
        }

        ProcessNextVideo(playBackOrderNumber, startTime);
    });
}

function DmotionPlayerPlay(dplayer, playList, playBackOrderNumber, startTime) {
    $("#player.playerbox").hide();

    dplayer.addEventListener("apiready", function (e) {

        $("#ChannelPageInfoHolder .videoTitle h1").text("").text(playList[playBackOrderNumber].VideoTubeTitle);

        UpdateUIForNewVideo(playBackOrderNumber);

        if (mutePlayerOnLoad) {
            dplayer.setMuted("1");
        }

        e.target.play();
    });
}

function InitFlowPlayer(playList, videoPlaylistIndex, seekTime) {
    ResetUIForProperPlayer(false, false);
    fpKey = $('#complete').data('key');

    if (seekTime == undefined) {
        seekTime = 0;
    }

    var newPlaylist = [];

    if (videoPlaylistIndex == -1) {
        if (playList && playList.length > 0) {
            $.each(playList, function (i, d) {
                var playlistItem = [{ mp4: d.VideoKey }];
                newPlaylist.push(playlistItem);
            });
        }
    }
    else {
        var currentVideo = playList[videoPlaylistIndex];
        var playlistItem = [{ mp4: currentVideo.VideoKey }];
        newPlaylist.push(playlistItem);
    }

    PlayVideoWithHtml5FlowPlayer(newPlaylist, videoPlaylistIndex, seekTime, fpKey);
}

function PlayVideoWithHtml5FlowPlayer(newPlaylist, globalVideoPlaylistIndex, seekTime, apiKey, callback) {
    var nextVideoPlaylistIndex = 0;
    var currentVideoPlaylistIndex = 0;

    // This should be done when list of videos passed to this method
    // is a subset of videos that are part in the main playlist retrieved for this channel
    // (case when playlist contains videos from multiple video providers)
    if (globalVideoPlaylistIndex >= 0) {
        currentVideoPlaylistIndex = globalVideoPlaylistIndex;
        nextVideoPlaylistIndex = globalVideoPlaylistIndex;
    }

    $("#complete").flowplayer({
        key: apiKey,
        playlist: newPlaylist,
        play: { opecity: 0 },
        //ratio: 9 / 16,
        autoplay: (autoplay == 1)
    }).one('ready', function (ev, api) {
        // exclude devices which do not allow autoplay
        // because this will have no effect anyway
        if (flowplayer.support.firstframe) {
            api.seek(seekTime).play(0);

            if (playlist) {
                currentVideo = playlist[currentVideoPlaylistIndex];
                playingVideoId = currentVideo.VideoTubeId;
            }

            if (currentVideo) {
                AddVideoViewStartForUser(currentVideo.VideoTubeId);
            }

            UpdateUIForNewVideo(currentVideoPlaylistIndex);
        }
    }).on('finish', function (ev, api) {
        var currentlyPlayingVideo = api.video;

        if (currentlyPlayingVideo) {
            var currentVideo;
            var nextVideo;

            var currentVideoTubeId = 0;
            var nextVideoTubeId = 0;

            nextVideoPlaylistIndex = currentVideoPlaylistIndex + 1;

            if (playlist) {
                nextVideo = playlist[nextVideoPlaylistIndex];
                currentVideo = playlist[currentVideoPlaylistIndex];
            }

            if (nextVideo) {
                if (nextVideo.VideoProviderName != "vimeo" && nextVideo.VideoProviderName != "youtube" && nextVideo.VideoProviderName != "dailymotion") {
                    nextVideoTubeId = nextVideo.VideoTubeId;
                }
            }
            else {
                $("#PlayerHolder").toggle();
                $("#playerImage").removeAttr("src").attr("src", "/images/thankYouChannelIL.jpg");
                $("#playerImage").toggle();
            }

            if (currentVideo) {
                currentVideoTubeId = currentVideo.VideoTubeId;
            }

            if (currentVideo) {
                IncrementVideoCountForVideo(currentVideoTubeId, nextVideoTubeId, null);
                playBackOrderNumber = currentVideo.PlaybackOrderNumber;

                if (nextVideoTubeId > 0) {
                    currentVideoPlaylistIndex = nextVideoPlaylistIndex;
                    UpdateUIForNewVideo(currentVideoPlaylistIndex);
                    playingVideoId = nextVideoTubeId;
                }
                else {
                    if (nextVideo) {
                        ProcessNextVideo((currentVideoPlaylistIndex + 1), 0);
                    }
                }
            }
        }
    });
}

function GetScheduleData() {
    var timeOfRequest = getClientTime();
    void 0;
    $.ajax({
        type: 'POST',
        url: webMethodGetChannelPreviewDataJson,
        dataType: 'json',
        data: '{"clientTime":' + "'" + timeOfRequest + "'" + ',"channelId":' + channelTubeId + ',"userId":' + userIdCheked + '}',
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d.ActiveSchedule != null) {
                $("#playerbox").toggle();

                AddChannelViewCount();

                isMatch = true;

                var data = response.d;
                var channel = data.Channel;
                var schedule = data.Schedule;

                playlist = data.Playlist;

                for (var i = 0; i < playlist.length; i++) {
                    playlistArr[i] = playlist[i].ProviderVideoId;
                }

                currChannelSchedule = data.ActiveSchedule.ChannelScheduleId;
                var SchedulesHtml = Controls.BuildVideoScheduleControlForChannel(data.Playlist);
                var firstVideo = playlist[0];

                if (firstVideo) {
                    startTime = parseInt(parseFloat(firstVideo.PlayerPlaybackStartTimeInSec).toFixed());
                }
                else {
                    startTime = 0;
                }

                if (HasMultipleProviders(data.Playlist)) {
                    ProcessNextVideo(0, startTime);
                }
                else {
                    if (firstVideo.VideoProviderName == "vimeo") {
                        providerId = 2;
                        InitVimeoPlayer(playlist, 0, startTime);
                    }
                    else if (firstVideo.VideoProviderName == "youtube") {
                        providerId = 1
                        if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                            onYouTubeIframeAPIReadyIE();
                        }
                        else {
                            onYouTubeIframeAPIReady();
                        }
                    }
                    else if (firstVideo.VideoProviderName == "dailymotion") {
                        providerId = 3;
                        InitDMotionPlayer(playlist, 0, startTime);
                    }
                    else {
                        providerId = 4;
                        InitFlowPlayer(playlist, -1, startTime);
                    }
                }

                $("#sideBarChannel .sideContentHolder").html("").html(SchedulesHtml);

                if (muteOnStartup) {
                    Mute();
                }
            }
            else {
                $("#PlayerHolder").toggle();
                $("#playerImage").removeAttr("src").attr("src", "/images/sorryChannelIL1200px.jpg");
                $("#playerImage").toggle();

                $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>" + response.d.Message + "</>");

                var nextPlayTimeStr = response.d.NextScheduleStartDateTime.substr(6, 13);
                var nextPlayTime = new Date(parseInt(nextPlayTimeStr));


                if (parseInt(nextPlayTimeStr) > 0) {
                    var today = new Date();
                    var nextMillis = nextPlayTime.getTime();
                    var nowMillis = today.getTime();
                    timeoutInMilliseconds = (nextMillis - nowMillis);

                    if (timeoutInMilliseconds < 0) {
                        timeoutInMilliseconds = 0;
                    }
                }
                else {
                    isMatch = true;
                    clearTimeout(SchedulePollingTimer);
                }
            }
        },
        complete: function () {
            if (isMatch == false) {
                SchedulePollingTimer = setTimeout(GetScheduleData, timeoutInMilliseconds);
            }
            else {
                clearTimeout(SchedulePollingTimer);
            }
            $(".nano").nanoScroller({ alwaysVisible: false });
        }
    });
};

function GetNextVideo(currentPlaybackOrderNumber) {
    var nextVideoPlaylistIndex = -1;

    if (playlist) {
        $.each(playlist, function (i, d) {
            if (d.PlaybackOrderNumber == currentPlaybackOrderNumber) {
                nextVideoPlaylistIndex = (i + 1);
            }
        });
    }

    var nextVideo = playlist[nextVideoPlaylistIndex];

    return nextVideo;
}

    function ResetUIForProperPlayer(isExternal, isDailyMotion) {

    var dailyMotionPlayerHolderHtml = "<div id='dMotionPlayer'>" +
                      "</div>" +
                      "<div class='playerbox' id='player'>" +
                      "<iframe class='playerbox video-tracking' style='display:none;' id='myVideo'  width='853' height='100%' data-frameborder='0' data-webkitallowfullscreen='true' data-mozallowfullscreen='true' data-allowfullscreen='true'>" +
                      "</iframe>" +
                      "</div>";

    var flowPlayerHolderContentHtml = "<div class='playerbox' id='strimmVideoBox'>" +
                                  "    <div id='complete' class='flowplayer functional' data-ratio='0.4167' data-key='$437712314481272' >" +
                                  "</div>";

    var externalPlayerHolderContentHtml = "<div class='playerbox' id='player'>" +
                          "<iframe class='playerbox video-tracking' id='myVideo'  width='853' height='510' data-frameborder='0' data-webkitallowfullscreen='true' data-mozallowfullscreen='true' data-allowfullscreen='true'>" +
                          "</iframe>" +
                          "</div>";

    var playerHolder = $("#PlayerHolder");

    playerHolder.html("");

    if (isExternal) {
        if (isDailyMotion) {
            playerHolder.html(dailyMotionPlayerHolderHtml);
        }
        else {
            playerHolder.html(externalPlayerHolderContentHtml);
        }
    }
    else {
        playerHolder.html(flowPlayerHolderContentHtml);
    }
}

function HasMultipleProviders(playlist) {
    var hasMultipleProviders = false;
    var currentProviderId = 0;

    if (playlist && playlist.length > 0) {
        $.each(playlist, function (i, d) {
            var providerVideoId = d.ProviderVideoId;

            if (currentProviderId == 0) {
                currentProviderId = providerVideoId;
            }
            else {
                if (currentProviderId != providerVideoId) {
                    hasMultipleProviders = true;
                }
            }
        });
    }

    return hasMultipleProviders;
}

function ProcessNextVideo(nextVideoPlaylistIndex, startTime) {
    var currentVideo = playlist[nextVideoPlaylistIndex];
    void 0;
    if (currentVideo) {
        var videoProvider = currentVideo.VideoProviderName;

        if (videoProvider == "vimeo") {
            providerId = 2;
            InitVimeoPlayer(playlist, nextVideoPlaylistIndex, startTime);
        }
        else if (videoProvider == "youtube") {
            providerId = 1;
            InitYoutubePlayer(playlist, nextVideoPlaylistIndex, startTime);
        }
        else if (videoProvider == "dailymotion") {
            providerId = 3;
            InitDMotionPlayer(playlist, nextVideoPlaylistIndex, startTime);
        }
        else {
            providerId = 4;
            InitFlowPlayer(playlist, nextVideoPlaylistIndex, startTime);
        }
    }
    else {
        $("#PlayerHolder").toggle();
        $("#playerImage").removeAttr("src").attr("src", "/images/thankYouChannelIL.jpg");
        $("#playerImage").toggle();
    }
}

function UpdateUIForNewVideo(videoPlaylistIndex) {
    var currentVideo = playlist[videoPlaylistIndex];
    var orderNumber = currentVideo.PlaybackOrderNumber;
    var title = currentVideo.VideoTubeTitle;
    var r_rated = currentVideo.IsRRated;

    $("#ChannelPageInfoHolder .videoTitle h1").text("").text(title);

    $(".sideContentHolder a.sideSchedule").removeClass("playingNow");
    $(".sideContentHolder a.sideSchedule .playingNowShow").hide();
    $(".sideContentHolder a.sideSchedule .playingNextShow").hide();
    $(".sideContentHolder a#order_" + orderNumber).addClass("playingNow");
    $(".sideContentHolder a#order_" + orderNumber + " .playingNowShow").show();
    $(".sideContentHolder a#order_" + (orderNumber + 1) + " .playingNextShow").show();

    if (r_rated == true) {
        $(".Rrated").text("").text("r-rated");
    }
    else {
        $(".Rrated").text("");
    }
}

function GetVideoByProviderVideoId(providerVideoId) {
    var video = null;
    if (playlist && playlist.length > 0) {
        for (var i = 0; i < playlist.length; i++) {
            if (playlist[i].ProviderVideoId == providerVideoId) {
                video = playlist[i];
            }
        }
    }

    return video;
}

function AddVideoViewStartForUser(videoTubeId) {
    var currentTime = getClientTime();
    $.ajax({
        type: "POST",
        url: webMethodAddVideoViewStartForUser,
        data: '{"videoTubeId":' + videoTubeId + ',"viewStartTime":' + "'" + currentTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d && response.d.IsSuccess) {
                //console.log("Successfully added a new video view");
            }
        },
        complete: function () {
        }
    });
}

function IncrementVideoCountForVideo(prevVideoTubeId, currentVideoTubeId, event) {
    var currentTime = getClientTime();
    $.ajax({
        type: "POST",
        url: webMethodAddVideoView,
        data: '{"prevVideoTubeId":' + prevVideoTubeId + ',"currentVideoTubeId":' + currentVideoTubeId + ',"clientTime":' + "'" + currentTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d && response.d.IsSuccess) {
                //console.log("Successfully added a new video view");
            }
        },
        complete: function () {
        }
    });
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
}

function GetChannelPreviewDataJson() {
    $.ajax({
        type: 'POST',
        url: webMethodGetChannelPreviewDataJson,
        dataType: 'json',
        data: params,
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d.ActiveSchedule != null) {
                isMatch = true;
                var data = response.d;
                var channel = data.Channel;
                var schedule = data.Schedule;
                var SchedulesHtml = Controls.BuildVideoScheduleControlForChannel(data.Playlist);
                $("#sideBarChannel .sideContentHolder").html("").html(SchedulesHtml);
            }
        }
    });
}

function AddChannelViewCount() {
    var addChannelViewTimer = setTimeout(function () {
        var clientTime = getClientTime();
        $.ajax({
            type: "POST",
            url: webMethodAddChannelCount,
            data: '{"userId":' + userId + ',"channelTubeId":' + channelTubeId + ',"viewTime":' + "'" + clientTime + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                void 0;
                window.clearTimeout(addChannelViewTimer);
            }
        });
    }, 180000)
}
//////
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
}

function GetVideoByPlaybackOrderNumber(videoPlaybackOrderNumber) {
    var video = null;

    if (playlist) {
        $.each(playlist, function (i, d) {
            if (d.PlaybackOrderNumber == videoPlaybackOrderNumber) {
                video = d;
            }
        });
    }

    return d;
}

function GetFavoriteChannels() {
    var favoriteChannel = 0;
    var clientTime = getClientTime();
    $.ajax({
        type: "POST",
        url: webMethodGetFavoriteChannels,
        data: '{"userId":' + "'" + userIdCheked + "'" + ',"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            favoriteChannel = Controls.BuildChannelControlForChannelPage(response, true);
            if (favoriteChannel.length > 0) {
                $(".sideContentHolder").html("").html(favoriteChannel);
            }
            else {
                if (userIdCheked && userIdCheked > 0) {
                    $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>You have no favorite channels yet</>");
                }
                else {
                    $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>You have no favorite channels yet.  Sign up or login to save your favorites</>");
                }
            }
        },
        complete: function () {
            $(".nano").nanoScroller({ alwaysVisible: false });
        },
        error: function () {
        }
    });
};

function GetPlayingChannelScheduleId() {
    var scheduledDate;
    var DateTimeNow = new Date();
    var month = DateTimeNow.getMonth() + 1;
    var day = DateTimeNow.getDate();
    var year = DateTimeNow.getFullYear();
    var hour = DateTimeNow.getHours();
    var minute = DateTimeNow.getMinutes();

    scheduledDate = month + "/" + day + "/" + year + " " + hour + ":" + minute;

    $.ajax({
        type: "POST",
        url: webMethodGetChannelSchedules,
        data: '{"channelTubeId":' + "'" + channelTubeId + "'" + ',"scheduledDate":' + "'" + scheduledDate + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            return response.d.ChannelScheduleId
        },
        complete: function () {
        },
        error: function () {
        }
    });
};

function PollFavoriteChannels() {
    var now = new Date();
    var favoritematch
    var userIdCheked = 0;

    if (userId != null) {
        userIdCheked = userId;
    }

    favoriteChannelSetTimeOut = setTimeout(function () {
        var favoriteChannel = 0;
        var clientTime = getClientTime();
        $.ajax({
            type: "POST",
            url: webMethodGetFavoriteChannels,
            data: '{"userId":' + "'" + userIdCheked + "'" + ',"clientTime":' + "'" + clientTime + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                favoriteChannel = Controls.BuildChannelControlForChannelPage(response, true);

                if (favoriteChannel.length > 0) {
                    $(".sideContentHolder").html("").html(favoriteChannel);
                }
                else {
                    if (userIdCheked && userIdCheked > 0) {
                        $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>You have no favorite channels yet</>");
                    }
                    else {
                        $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>You have no favorite channels yet.  Sign up or login to save your favorites</>");
                    }
                }
            },
            complete: function () {
                PollFavoriteChannels();
            },
            error: function () {
            }
        });
    }, 30000);
};

function GetPlayingChannelScheduleId() {
    var scheduledDate;
    var DateTimeNow = new Date();
    var month = DateTimeNow.getMonth() + 1;
    var day = DateTimeNow.getDate();
    var year = DateTimeNow.getFullYear();
    var hour = DateTimeNow.getHours();
    var minute = DateTimeNow.getMinutes();

    scheduledDate = month + "/" + day + "/" + year + " " + hour + ":" + minute;

    $.ajax({
        type: "POST",
        url: webMethodGetChannelSchedules,
        data: '{"channelTubeId":' + "'" + channelTubeId + "'" + ',"scheduledDate":' + "'" + scheduledDate + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            return response.d.ChannelScheduleId
        },
        complete: function () {
        },
        error: function () {
        }
    });
}

function GetTopChannels() {
    var clientTime = getClientTime();
    ajaxTopChannels = $.ajax({
        type: "POST",
        url: webMethodGetTopChannelsOnTheAirJson,
        data: '{"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            var topChannels = Controls.BuildChannelControlForChannelPage(response, true);
            if (topChannels.length > 0) {
                $(".sideContentHolder").html("").html(topChannels);
            }
            else {
                $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>The top channels are not currently broadcasting</>");
            }
        },
        complete: function () {
            $(".nano").nanoScroller({ alwaysVisible: false });
            PollTopChannels();
        },
        error: function () {
        }
    });

};

function PollTopChannels() {
    var timeoutInSec = 30000;
    var now = new Date();

    topChannelSetTimeOut = setTimeout(function () {
        if (executeTopChannels > 0) {
            GetTopChannels();
        }
        else {
            return;
        }
    }, timeoutInSec);
};

function AddToFavorite() {
    var clientTime = getClientTime();
    $.ajax({
        type: "POST",
        url: webMethodSunscribeToChannel,
        data: '{"userId":' + "'" + userIdCheked + "'" + ',"channelTubeId":' + "'" + channelTubeId + "'" + ',"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $(".options .addtofavorite").addClass('addtofavoriteActive');
            $(".options .addtofavorite").removeAttr("onclick").attr("onclick", "RemoveFromFavorite()").text("").removeAttr("title").attr("title", "Remove from Favorites");
            var pattern = /[0-9]+/g;

            var funText = $("#lblSubscribers").text();
            var funNumberStr = funText.match(pattern);
            var funNumber = parseInt(funNumberStr[0]);
            var funsAfterAdd = funNumber + 1;
            $("#lblSubscribers").text("").text("fans: " + funsAfterAdd);

            alertify.success("You have successfully subscribed to this channel.");
        },
        complete: function () {
        },
        error: function () {
        }
    });
};

function RemoveFromFavorite() {
    var clientTime = getClientTime();
    $.ajax({
        type: "POST",
        url: webMethodUnsuscribeFromChannel,
        data: '{"userId":' + "'" + userIdCheked + "'" + ',"channelTubeId":' + "'" + channelTubeId + "'" + ',"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $(".options .addtofavorite").removeClass('addtofavoriteActive');
            $(".options .addtofavorite").removeAttr("onclick").attr("onclick", "AddToFavorite()").text("").removeAttr("title").attr("title", "Add to Favorites");
            var pattern = /[0-9]+/g;

            var funText = $("#lblSubscribers").text();
            var funNumberStr = funText.match(pattern);
            var funNumber = parseInt(funNumberStr[0]);
            var funsAfterRemove = funNumber - 1;
            $("#lblSubscribers").text("").text("fans: " + funsAfterRemove)
        },
        complete: function () {
        },
        error: function () {
        }
    });
};

function WatchitLater() {
    if (playingVideoId != 0) {
        var clientTime = getClientTime();
        $.ajax({
            type: "POST",
            url: webMethodAddVideoToArchive,
            data: '{"videoId":' + playingVideoId + ',"clientTime":' + "'" + clientTime + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $(".watchlater").text("");
                $(".watchlater").addClass('watchlaterActive');
                alertify.success("Video was successfully added to your 'Watch Later' list.");
            },
            complete: function () {
            },
            error: function () {
            }
        });
    };
};

function ShowAbuseModal() {
    $("#txtVideoTitle").val("");
    $("#txtComments").val("");
    //console.log(currVideoTitle);
    $("#txtVideoTitle").val(currVideoTitle);
    $('#abuseModal').lightbox_me({
        centered: true,
        onLoad: function () {
        },
        onClose: function () {
            RemoveOverlay();
        }
    });
}

function SendAbuseReport() {
    var selectedOption = $('#ddlCategory option:selected').text();

    var videoTitle = $("#txtVideoTitle").val();
    var comments = $("#txtComments").val();
    var videoTubeId = playingVideoId;
    $('#abuseModal').ajaxStart(function () {
        $("#lblMsg").text("please wait");
    })

    var clientTime = getClientTime();
    var params = '{"selectedOption":' + "'" + selectedOption + "'" +
                 ',"videoTitle":' + "'" + videoTitle + "'" +
                 ',"comments":' + "'" + comments + "'" +
                 ',"senderDateTime":' + "'" + clientTime + "'" +
                 ',"channelScheduleId":' + currChannelSchedule +
                 ',"videoTubeId":' + videoTubeId +
                 ',"senderUserId":' + userIdCheked +
                 ',"channelId":' + channelTubeId + '}';

    $.ajax({
        type: "POST",
        url: webMethodSendAbuseReport,
        data: params,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            closeAbuse();
            alertify.success(response.d);
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
};

function closeAbuse() {
    $('#txtVideoTitle, #txtComments').val("");
    $("#lblMsg").text("");
    $('#abuseModal .close').trigger("click");
}

function ShowSchedule() {
    executeSchedulePolling = 1;
    executeTopChannels = 0;
    $(".iconDescription").html("channel schedule");
    $(".sideContentHolder").html("");
    $(".sideBarOptions .schedule").addClass("scheduleactive");
    $(".sideBarOptions .favoriteChannels").removeClass("favoriteChannelsActive");
    $(".sideBarOptions .toprated").removeClass("topRatedActive");
    $(".sideBarOptions .mychannels").removeClass("mychannelsactive");
    $(".sideBarOptions .myChat").removeClass("myChatActive");
    window.clearTimeout(favoriteChannelSetTimeOut);
    window.clearTimeout(topChannelSetTimeOut);

    var clientTime = getClientTime();
    var params = '{"clientTime":' + "'" + clientTime + "'" + ',"channelId":' + channelTubeId + ',"userId":' + userIdCheked + '}';

    $.ajax({
        type: 'POST',
        url: webMethodGetChannelPreviewDataJson,
        dataType: 'json',
        data: params,
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d.ActiveSchedule != null) {
                var data = response.d;
                var SchedulesHtml = Controls.BuildVideoScheduleControlForChannel(data.Playlist);
                if (executeSchedulePolling != 0) {
                    $("#sideBarChannel .sideContentHolder").html("").html(SchedulesHtml);

                    // Since we are regetting the channel schedule, the first video in the schedule will
                    // always be playing first.
                    UpdateUIForNewVideo(0);
                }

            }
            else {
                $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>" + response.d.Message + "</>");
            }
        },
        complete: function (res) {
            $(".nano").nanoScroller({ alwaysVisible: false });
        }

    });
};

function GetMyChannels() {
    window.clearTimeout(favoriteChannelSetTimeOut);
    window.clearTimeout(SchedulePollingTimer);
    window.clearTimeout(topChannelSetTimeOut);

    var clientTime = getClientTime();
    $.ajax({
        type: "POST",
        url: webMethodGetMyChannels,
        data: '{"userId":' + "'" + userIdCheked + "'" + ',"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            myChannels = Controls.BuildChannelControlForChannelPage(response, false);
            if (myChannels.length > 0) {
                $(".sideContentHolder").html("").html(myChannels);
            }
            else {
                if (userIdCheked && userIdCheked > 0) {
                    $(".sideContentHolder").html("").html("<span class='msg'>You have no channels of your own yet.</>");
                }
                else {
                    $(".sideContentHolder").html("").html("<span class='msg'>You have no channels of your own yet. Sign up or login to create yours now!</>");
                }
            }
        },
        complete: function () {
            $(".nano").nanoScroller({ alwaysVisible: false });

        },
        error: function () {
        }
    });
}
function GetFBComments() {
    executeSchedulePolling = 0;
    executeTopChannels = 0;
    window.clearTimeout(favoriteChannelSetTimeOut);
    window.clearTimeout(SchedulePollingTimer);
    window.clearTimeout(topChannelSetTimeOut);
    $(".sideBarOptions .schedule").removeClass("scheduleactive");
    $(".sideBarOptions .favoriteChannels").removeClass("favoriteChannelsActive");
    $(".sideBarOptions .toprated").removeClass("topRatedActive");
    $(".sideBarOptions .mychannels").removeClass("mychannelsactive");
    $(".sideBarOptions .myChat").addClass("myChatActive");
    $(".iconDescription").text("").text("chat");
    if ('undefined' === typeof FB) {
        return;
    }
    else {
        $(".sideContentHolder").html("");
        var $comments_div = $('<div/>');
        $comments_div.addClass('fb-comments');
        $comments_div.attr('data-href', document.location);
        $comments_div.appendTo($('.sideContentHolder'));

        FB.XFBML.parse();

    }


    $(".nano").nanoScroller({ alwaysVisible: false });
}
function ShowFavorites() {
    executeSchedulePolling = 0;
    executeTopChannels = 0;
    window.clearTimeout(SchedulePollingTimer);
    window.clearTimeout(topChannelSetTimeOut);
    $(".iconDescription").html("favorite channels");
    $(".sideContentHolder").html("");

    $(".sideBarOptions .schedule").removeClass("scheduleactive");
    $(".sideBarOptions .favoriteChannels").addClass("favoriteChannelsActive");
    $(".sideBarOptions .toprated").removeClass("topRatedActive");
    $(".sideBarOptions .mychannels").removeClass("mychannelsactive");
    $(".sideBarOptions .myChat").removeClass("myChatActive");

    GetFavoriteChannels();
    (function poll() {
        PollFavoriteChannels();
    })();
};

function ShowTopChannels() {
    executeSchedulePolling = 0;
    executeTopChannels = 1;
    $(".iconDescription").html("top channels");
    $(".sideContentHolder").html("");
    $(".sideBarOptions .schedule").removeClass("scheduleactive");
    $(".sideBarOptions .favoriteChannels").removeClass("favoriteChannelsActive");
    $(".sideBarOptions .toprated").addClass("topRatedActive");
    $(".sideBarOptions .mychannels").removeClass("mychannelsactive");
    $(".sideBarOptions .myChat").removeClass("myChatActive");

    window.clearTimeout(favoriteChannelSetTimeOut);
    window.clearTimeout(SchedulePollingTimer);

    GetTopChannels();
    (function poll() {
        PollTopChannels();

    })();


};

function ShowMyChannels() {
    executeSchedulePolling = 0;
    executeTopChannels = 0;
    $(".iconDescription").html("my channels");
    $(".sideContentHolder").html("");
    $(".sideBarOptions .schedule").removeClass("scheduleactive");
    $(".sideBarOptions .favoriteChannels").removeClass("favoriteChannelsActive");
    $(".sideBarOptions .toprated").removeClass("topRatedActive");
    $(".sideBarOptions .mychannels").addClass("mychannelsactive");
    $(".sideBarOptions .myChat").removeClass("myChatActive");
    window.clearTimeout(topChannelSetTimeOut);
    window.clearTimeout(favoriteChannelSetTimeOut);
    window.clearTimeout(SchedulePollingTimer);

    GetMyChannels();
};

function Like() {
    var currentTime = getClientTime();
    var params = '{"clientTime":' + "'" + currentTime + "'" + ',"channelTubeId":' + channelTubeId + ',"userId":' + userIdCheked + '}';

    $.ajax({
        type: "POST",
        url: webMethodAddLike,
        data: params,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            //console.log(response.d);
            if (response.d != null) {
                $("#ancLike").addClass("likeActive");
                $("#ancLike").removeAttr("onclick").attr("onclick", "UnLike()");
                $("#ancLike").removeAttr("title").attr("title", "You like this channel. Click again, if you want to remove it.");
                $("#lblLikes").text("").text("likes: " + response.d);
                alertify.success("Thank you for liking this channel!");

            }
        },
        complete: function () {

        },
        error: function () {
        }
    });
}

function UnLike() {
    var currentTime = getClientTime();
    var params = '{"clientTime":' + "'" + currentTime + "'" + ',"channelTubeId":' + channelTubeId + ',"userId":' + userIdCheked + '}';

    $.ajax({
        type: "POST",
        url: webMethodUnLike,
        data: params,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            // console.log(response.d);
            if (response.d != null) {
                $("#ancLike").removeClass("likeActive");
                $("#ancLike").removeAttr("onclick").attr("onclick", "Like()");
                $("#ancLike").removeAttr("title").attr("title", "Like this channel");
                $("#lblLikes").text("").text("likes: " + response.d);
                alertify.success("You successfully removed your Like from this channel");

            }
        },
        complete: function () {

        },
        error: function () {
        }
    });
};


function ShowInfoBar() {

    $(".showInfoBar").hide();
    $(".hideInfoBlock").show('slide', { direction: 'left' }, 1000);
};

function HideInfoBar() {

    $(".hideInfoBlock").hide('slide', { direction: 'left' }, 1000);
    $(".showInfoBar").show();
};

function ShowScheduleBlock(activePanelName) {

    $(".showScheduleWrapper").hide();
    $(".hideInfoScheduleBlock").show('slide', { direction: 'right' }, 500);
    $("#sideBarChannel").css("width", "320px");
    $("#sideBarChannel").css("height", "600px");
    if (activePanelName != undefined) {
        if (activePanelName == 'schedule') {
            ShowSchedule();
        }
        else if (activePanelName == 'mychannels') {
            ShowMyChannels();
        }
        else if (activePanelName == 'favorite') {
            ShowFavorites();
        }
        else if (activePanelName == 'topchannels') {
            ShowTopChannels();
        }
        else if (activePanelName == 'chat') {
            GetFBComments();
        }
    }
    $(".nano").nanoScroller({ alwaysVisible: false });
};

function HideSchedule() {

    $(".hideInfoScheduleBlock").hide('slide', { direction: 'right' }, 500);
    $(".showScheduleWrapper").show();
    $("#sideBarChannel").css("width", "40px");
    $("#sideBarChannel").css("height", "40px");
};

function showChannelDescription() {
    $("#innerRightWrapper, #ChannelPageSocialHolder, .channelCreator").hide();
    $(".channelWatchigDescription").css("height", "560px");
    $(".channelWatchigDescription p").css("height", "auto")
    $("#chDescription").removeClass("descriptionP").addClass('descriptionPFull');
    $(".channelWatchigDescription .moreInfo").text("").text("less").removeAttr("onclick").attr("onclick", "HideChannelDescription()");
};

function HideChannelDescription() {
    $("#innerRightWrapper, #ChannelPageSocialHolder, .channelCreator").show();
    $(".channelWatchigDescription").css("height", "auto");
    $(".channelWatchigDescription p").css("height", "80px")
    $("#chDescription").removeClass("descriptionPFull").addClass('descriptionP');
    $(".channelWatchigDescription .moreInfo").text("").text("more").removeAttr("onclick").attr("onclick", "showChannelDescription()");
};

function showCreatorBio() {
    $("#innerLeftWrapperChannel, #titleHolder, .descriptionHolder, #innerRightWrapper, #ChannelPageSocialHolder, .infoDevider").hide();
    $(".channelCreatorBIO").css("height", "560px");
    $(".channelCreatorBIO p").css("height", "auto")
    $("#chCreatorBio").removeClass("creatorBioP").addClass('descriptionPFull');
    $(".channelCreatorBIO .moreInfo").text("").text("less").removeAttr("onclick").attr("onclick", "HideCreatorBio()");
};

function HideCreatorBio() {
    $("#innerLeftWrapperChannel, #titleHolder, .descriptionHolder, #innerRightWrapper, #ChannelPageSocialHolder, .infoDevider").show();
    $(".channelCreatorBIO").css("height", "auto");
    $(".channelCreatorBIO p").css("height", "80")
    $("#chCreatorBio").removeClass("descriptionPFull").addClass('creatorBioP');
    $(".channelCreatorBIO .moreInfo").text("").text("more").removeAttr("onclick").attr("onclick", "showCreatorBio()");
};

function ShowSocioal() {
    $("#socialHolder").show();
    $(".shareInfo").removeAttr("onclick").attr("onclick", "CloseSocial()").addClass("socialActive");
};

function CloseSocial() {
    $("#socialHolder").hide();
    $(".shareInfo").removeAttr("onclick").attr("onclick", "ShowSocioal()").removeClass("socialActive");
};





function GetRandomPlayingChannelsByCategory() {

    //  var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('=');
    //var categoryName = hashes[1].replace(/%20/g, " ");
    $("select option[value='0']").attr('selected', 'selected');

    var clientTime = getClientTime();
    var params = '{"clientTime":' + "'" + clientTime + "'" + ',"currentChannelId":' + channelTubeId + ',"categoryName":' + "'" + categoryName + "'" + '}';
    $.ajax({
        type: "POST",
        url: WebMethodGetCurrentlyPlayingChannelsByCategoryNameForChannel,
        data: params,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {



            if ($.isEmptyObject(response.d)) {
                $(".moreChannelsContent").show().html("<span id='spnMessage' class='noChannelsMessage'>There are no other channels in this category</span>");

            }
            else {
                var currentlyPlayingChannels = Controls.BuildChannelControlForBrowseResultsPage(response.d, true);
                $(".moreChannelsContent").show().html(currentlyPlayingChannels);
                //console.log(response.d.channelTubeId)
            }


        }
    });
};

function FormatUrl(originDomain, key) {
    var link = key;
    var pattern = /^((http|https|ftp|):\/\/)/;
    var pattern2 = /^(\/\/)/;
    var domain = 'https://';

    if (isVideo) {
        domain = '//';
    }

    if (!pattern.test(link) && !pattern2.test(link)) {
        link = domain + originDomain + '/' + link;
    }

    return link;
};

function RedirectToStrimm()
{
    window.top.location.replace('https://www.strimm.com/');
}