
var webMethodGetChannelPreviewDataJson = "/WebServices/ChannelWebService.asmx/GetCurrentlyPlayingChannelData";
var webMethodGetTopChannelsOnTheAirJson = "/WebServices/ChannelWebService.asmx/GetTopChannelsOnTheAir";
var webMethodGetFavoriteChannelsOnTheAirForUserJson = "/WebServices/ChannelWebService.asmx/GetFavoriteChannelsOnTheAirForUser";

var webMethodAddVideoView = "/WebServices/ChannelWebService.asmx/AddVideoViewsOnChangeForUser";
var webMethodAddChannelCount = "/WebServices/ChannelWebService.asmx/AddChannelViewForUser";
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

var channelScheduleId = 0;
var playlistArr = new Array();
var playList;
var playingVideoId = 0;
var currVideoTitle;
var currChannelSchedule;
var player;
var playBackOrderNum;

//$(document).ajaxStart(function () {
//    $("#loadingDiv").show();
//});
//$(document).ajaxStop(function () {
//    $("#loadingDiv").hide();
//});
function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}

function DeleteCookie(name) {
    document.cookie = name + '=; expires=Thu, 01-Jan-70 00:00:01 GMT;';
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

var executeSchedulePolling = 1;
var timeoutInMilliseconds;

var SchedulePollingTimer;

function SchedulePolling() {
    GetScheduleData();
};

function GetScheduleData() {
    isMatch = false;
    var timeOfRequest = (new Date()).toISOString();

    $.ajax({
        type: 'POST',
        url: webMethodGetChannelPreviewDataJson,
        dataType: 'json',
        data: '{"clientTime":' + "'" + timeOfRequest + "'" + ',"channelId":' + channelTubeId + ',"userId":' + userIdCheked + '}',
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d.ActiveSchedule != null) {
                isMatch = true;

                var data = response.d;
                var channel = data.Channel;
                var schedule = data.Schedule;
                
                playList = data.Playlist;
              
                $.each(playList, function (i, d) {
                    void 0;
                });
                currChannelSchedule = data.ActiveSchedule.ChannelScheduleId;
                var SchedulesHtml = Controls.BuildVideoScheduleControlForChannel(data.Playlist);
                var firstVideo = playList[0];
                startTime = (playList[0].PlayerPlaybackStartTimeInSec).toFixed(1);
                if (firstVideo.VideoProviderName == "vimeo")
                {
                    InitVimeoPlayer(playList,0);
                }
                else {
                    var tag = document.createElement('script');

                    tag.src = "https://www.youtube.com/iframe_api";
                    var firstScriptTag = document.getElementsByTagName('script')[0];
                    firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
                    InitYoutubePlayer(playList,0);
                }
                $("#sideBarChannel .sideContentHolder").html("").html(SchedulesHtml);

                for (var i = 0; i < playList.length; i++) {
                    playlistArr[i] = playList[i].ProviderVideoId;
                }
               
                // //console.log(response.d);
                // //console.log(data);
                //var tag = document.createElement('script');

                //tag.src = "https://www.youtube.com/iframe_api";
                //var firstScriptTag = document.getElementsByTagName('script')[0];
                //firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

                //function onYouTubeIframeAPIReady() {

                //    //if ($.browser.device = (/android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase()))) {
                //    //    player = new YT.Player('player', {
                //    //        height: '546',
                //    //        width: '728',

                //    //        //videoId: 'M7lc1UVf-VE',
                //    //        playerVars: {
                //    //            //controls: 0,
                //    //            showinfo: 0,
                //    //            autoplay: 1
                //    //             //html5: 0

                //    //        },
                //    //        events: {
                //    //            'onReady': OnPlayerReady,
                //    //            'onStateChange': OnPlayerStateChange
                //    //        }
                //    //    });
                //    //}

                //    //else {
                //    player = new YT.Player('player', {
                //        height: '546',
                //        width: '728',

                //        //videoId: 'M7lc1UVf-VE',
                //        playerVars: {
                //            //controls: 0,
                //            showinfo: 0,
                //            autoplay: 1,
                //            wmode: 'transparent'
                //            // html5: 0

                //        },
                //        events: {
                //            'onReady': OnPlayerReady,
                //            'onStateChange': OnPlayerStateChange
                //        }
                //    });
                //    //  }

                //    $('iframe').each(function () {
                //        var src = $(this).attr('src');
                //        $(this).attr('src', src + '&wmode=transparent');
                //    });
                //};
                //function OnPlayerStateChange(eventStateChanged) {

                //    playBackOrderNum = (playList[eventStateChanged.target.getPlaylistIndex()].PlaybackOrderNumber);
                //    $(".sideContentHolder a.sideSchedule").removeClass("playingNow");
                //    $(".sideContentHolder a.sideSchedule .playingNowShow").hide();
                //    $(".sideContentHolder a.sideSchedule .playingNextShow").hide();
                //    $(".sideContentHolder a#order_" + playBackOrderNum).addClass("playingNow");
                //    $(".sideContentHolder a#order_" + playBackOrderNum + " .playingNowShow").show();
                //    var playingNext = playBackOrderNum + 1;
                //    $(".sideContentHolder a#order_" + playingNext + " .playingNextShow").show();


                //    $("#watchlater").text("").text("watch later");
                //    //if (eventStateChanged.data == 2)
                //    //{
                //    //    return;
                //    //}
                //    $("#ChannelPageInfoHolder .videoTitle h1").text("").text(playList[eventStateChanged.target.getPlaylistIndex()].VideoTubeTitle);
                //    currVideoTitle = playList[eventStateChanged.target.getPlaylistIndex()].VideoTubeTitle;
                //    //$("#ChannelPageInfoHolder .videoTitle h1").text(playList[event.target.getPlaylistIndex()].views);
                //    var r_rated = playList[eventStateChanged.target.getPlaylistIndex() + 1].IsRRated;
                //    if (r_rated == true) {
                //        $(".Rrated").text("").text("r-rated");
                //    }
                //    else {
                //        $(".Rrated").text("");
                //    }
                //    playingVideoId = playList[eventStateChanged.target.getPlaylistIndex()].VideoTubeId;
                //    var prevVideoId;

                //    if (eventStateChanged.data == -1) {
                //        if (playingVideoId != 0) {
                //            console.log(eventStateChanged.target.getPlaylistIndex());
                //            if (eventStateChanged.target.getPlaylistIndex() != 0) {
                //                prevVideoId = playList[eventStateChanged.target.getPlaylistIndex() - 1].VideoTubeId;
                //            }
                //            else {
                //                prevVideoId = playList[eventStateChanged.target.getPlaylistIndex()].VideoTubeId;
                //            }

                //        }
                //        $("#ChannelPageInfoHolder .videoTitle h1").text(playList[eventStateChanged.target.getPlaylistIndex()].VideoTubeTitle);
                //        playingVideoId = playList[eventStateChanged.target.getPlaylistIndex()].VideoTubeId;
                //        currVideoTitle = playList[eventStateChanged.target.getPlaylistIndex()].VideoTubeTitle;
                //        // var r_rated = playList[eventStateChanged.target.getPlaylistIndex() + 1].IsRRated;

                //        if (r_rated == true) {
                //            $(".Rrated").text("").text("r-rated");
                //        }
                //        else {
                //            $(".Rrated").text("");
                //        }
                //        //$("#spnViews").text(" ").text(playList[event.target.getPlaylistIndex()].views);
                //        $.ajax({
                //            type: "POST",
                //            url: webMethodAddVideoView,
                //            data: '{"prevVideoTubeId":' + prevVideoId + ',"currentVideoTubeId":' + playingVideoId + ',"clientTime":' + "'" + globalClientTime + "'" + '}',
                //            dataType: "json",
                //            contentType: "application/json; charset=utf-8",
                //            success: function (response) {
                //                console.log("Successfully added a new video view");
                //            },
                //            complete: function () {
                //                if ((eventStateChanged.target.getPlaylistIndex() == playlistArr.length - 1) && (eventStateChanged.data == -1)) {
                //                    if (eventStateChanged.data == 0) {
                //                        document.location.reload(true);
                //                    }
                //                    //$.ajax({
                //                    //    type: "POST",
                //                    //    url: webMethodAddChannelCount,
                //                    //    data: '{"userId":' + userId + ',"channelTubeId":' + channelTubeId + ',"clientTime":' + "'" + clientTime.format('m/d/Y H:i') + "'" + '}',
                //                    //    dataType: "json",
                //                    //    contentType: "application/json; charset=utf-8",
                //                    //    success: function (response) {
                //                    //        console.log("Successfully added a new channel view");
                //                    //    },
                //                    //    complete: function () {
                //                    //        if (eventStateChanged.data == 0) {
                //                    //            document.location.reload(true);
                //                    //        }
                //                    //    }
                //                    //});
                //                }
                //            }
                //        });

                //    }
                //    // //console.log(event.target.k.playlistIndex);
                //}
                //function OnPlayerReady(event) {
                //    player.cuePlaylist(playlistArr, 0, startTime, "large");
                //    player.setLoop(false);
                //    event.target.playVideo();
                //    console.log(event.target);
                //    $("#ChannelPageInfoHolder .videoTitle h1").text("").text(playList[0].VideoTubeTitle)

                //};
                //function onYouTubeIframeAPIReadyIE() {
                //    if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                //        //  //console.log("IE");
                //        $("iframe.playerbox").attr("allowfullscreen");
                //        player = new YT.Player('player', {
                //            height: '546',
                //            width: '728',

                //            //videoId: 'M7lc1UVf-VE',
                //            playerVars: {
                //                //controls: 0,
                //                showinfo: 0,
                //                autoplay: 1
                //                //  html5: 0




                //            },
                //            events: {
                //                'onReady': OnPlayerReady,
                //                'onStateChange': OnPlayerStateChange
                //            }
                //        });
                //    }
                //    $('iframe').each(function () {
                //        var src = $(this).attr('src');
                //        $(this).attr('src', src + '&wmode=transparent');
                //    });
                //}

                //if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                //    OnYouTubeIframeAPIReadyIE();
                //}
                //else {
                //    onYouTubeIframeAPIReady();
                //}

                var done = false;



            }
            else {
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
        }
    });
};
function InitVimeoPlayer(playList, playBackOrderNumb) {
   // console.log("init vimeo player")
   //var src = "//player.vimeo.com/video/15321875?api=1&player_id=myVideo;autoplay=1"
    //console.log("here");
    var iframe;
    window.$('.video-tracking').each(function () {

        // get the vimeo player(s)
        iframe = window.$(this);
        iframe.attr("src", "//player.vimeo.com/video/" + playList[playBackOrderNumb].ProviderVideoId + "?api=1&player_id=myVideo;autoplay=1")
        player = window.$f(iframe[0]);
        void 0;
        // When the player is ready, add listeners for pause, finish, and playProgress
        player.addEvent('ready', function () {
            void 0;

            //these are the three standard events Vimeo's API offers
            player.addEvent('play', onPlay);
            player.addEvent('pause', onPause);
            player.addEvent('finish', onFinish);
            // player.api('seekTo', 375);
        });
    });

    //define the custom events to push to Optimizely
    //appending the id of the specific video (dynamically) is recommended

    //to make this script extensible to all possible videos on your site
    function onPause(id) {

        void 0;
        window['optimizely'] = window['optimizely'] || [];
        window.optimizely.push(["trackEvent", id + "Pause"]);
    }

    function onFinish(id) {
        playBackOrderNumb++;
        //console.log(playBackOrderNumb);
        if (playList[playBackOrderNumb].VideoProviderName == "vimeo")
        {
            iframe.removeAttr("src").attr("src", "//player.vimeo.com/video/" + playList[playBackOrderNumb].ProviderVideoId + "?api=1&player_id=myVideo;autoplay=1");
        }
        else
        {
            iframe.removeAttr("src");
            InitYoutubePlayer(playList, playBackOrderNumb);
        }

        void 0;
        window['optimizely'] = window['optimizely'] || [];
        window.optimizely.push(["trackEvent", id + "Finish"]);
    }

    function onPlay(id) {
        void 0;
        window['optimizely'] = window['optimizely'] || [];
        window.optimizely.push(["trackEvent", id + "Play"]);
    }



}
function InitYoutubePlayer(playList, playbackOrderNumber)
{

   
    void 0;
    
    startTime = (playList[playbackOrderNumber].PlayerPlaybackStartTimeInSec).toFixed(1);
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
            height: '546',
            width: '728',

           // videoId: playList[playbackOrderNumber+1].VideoTubeId,
            playerVars: {
                //controls: 0,
                showinfo: 0,
                autoplay: 1,
                wmode: 'transparent'
                // html5: 0

            },
            events: {
                'onReady': OnPlayerReady,
                'onStateChange': OnPlayerStateChange
               

            }
        });
        //  }

        $('iframe').each(function () {
            var src = $(this).attr('src');
            $(this).attr('src', src + '&wmode=transparent');
        });
    };
    function OnPlaybackQualityChange(eventQualityChanged) {
        void 0;
    };
    function OnApiChange(eventApiChanged) {
        void 0;
    };

    function OnPlayerStateChange(eventStateChanged) {
        
        void 0;
        //onFinish
        if(eventStateChanged.data==0)
        {
            playbackOrderNumber++;
            if(playList[playbackOrderNumber].VideoProviderName=="vimeo")
            {
                player.destroy();
                InitVimeoPlayer(playList, playbackOrderNumber);
            }
            else
            {
                InitYoutubePlayer(playList, playbackOrderNumber)
            }
        }
       
    }
    function OnPlayerReady(event) {
        //player.cueVideoById(playList[playbackOrderNumber], 0, startTime, "large");
        player.loadVideoById({ videoId: playList[playbackOrderNumber].ProviderVideoId, startSeconds: 0, suggestedQuality: "large" })
        player.setLoop(false);
        event.target.playVideo();
        void 0;
        $("#ChannelPageInfoHolder .videoTitle h1").text("").text(playList[playbackOrderNumber].VideoTubeTitle)

    };
    function onYouTubeIframeAPIReadyIE() {
        if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
            //  //console.log("IE");
            $("iframe.playerbox").attr("allowfullscreen");
            player = new YT.Player('player', {
                height: '546',
                width: '728',

                //videoId: 'M7lc1UVf-VE',
                playerVars: {
                    //controls: 0,
                    showinfo: 0,
                    autoplay: 1
                    //  html5: 0




                },
                events: {
                    'onReady': OnPlayerReady,
                    'onStateChange': OnPlayerStateChange
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

$('.ytp-button-fullscreen-enter').on("click",(function () {

    void 0;
})
);


function GetFavoriteChannels() {
    var favoriteChannel = 0;
    $.ajax({
        type: "POST",
        url: webMethodGetFavoriteChannels,
        data: '{"userId":' + "'" + userIdCheked + "'" + ',"clientTime":' + "'" + globalClientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            favoriteChannel = Controls.BuildChannelControlForChannelPage(response, true);
            if (favoriteChannel.length > 0) {
                $(".sideContentHolder").html("").html(favoriteChannel);
            }
            else {
                $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>You have no favorite channels yet</>");
            }

        },
        complete: function () {
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
            void 0
            return response.d.ChannelScheduleId
        },
        complete: function () {
        },
        error: function () {
        }
    });
};

var favoriteChannelSetTimeOut;
function PollFavoriteChannels() {

    var now = new Date();
    var favoritematch
    var userIdCheked = 0;
    if (userId != null) {
        userIdCheked = userId;
    }

    favoriteChannelSetTimeOut = setTimeout(function () {
        var favoriteChannel = 0;
        $.ajax({
            type: "POST",
            url: webMethodGetFavoriteChannels,
            data: '{"userId":' + "'" + userIdCheked + "'" + ',"clientTime":' + "'" + globalClientTime + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                favoriteChannel = Controls.BuildChannelControlForChannelPage(response, true);


                if (favoriteChannel.length > 0) {
                    $(".sideContentHolder").html("").html(favoriteChannel);
                }
                else {
                    $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>You have no favorite channels yet</>");
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
            void 0
            return response.d.ChannelScheduleId
        },
        complete: function () {
        },
        error: function () {
        }
    });
}
var ajaxTopChannels;
function GetTopChannels() {
    ajaxTopChannels = $.ajax({
        type: "POST",
        url: webMethodGetTopChannelsOnTheAirJson,
        data: '{"clientTime":' + "'" + globalClientTime + "'" + '}',
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
            PollTopChannels();
        },
        error: function () {
        }
    });

};
var topChannelSetTimeOut;
var executeTopChannels = 0;
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
        //$.ajax({
        //    type: "POST",
        //    url: webMethodGetTopChannelsOnTheAirJson,          
        //    data: '{"clientTime":' + "'" + now.format('m/d/Y H:i')  + "'" + '}',
        //    dataType: "json",
        //    contentType: "application/json; charset=utf-8",
        //    success: function (response) {

        //        var  topChannels = Controls.BuildChannelControlForChannelPage(response, true);
        //        if(topChannels.length>0)
        //       {
        //      $(".sideContentHolder").html("").html(topChannels);
        //        }
        //        else
        //        {
        //            $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>The top channels are not currently broadcasting</>");
        //        }

        //    },
        //    complete: function () {
        //        PollTopChannels();
        //    },
        //    error: function () {
        //    }
        //});
    }, timeoutInSec);
};

function StopVideo() {
    player.stopVideo();
}

function PlayVideo() {
    player.playVideo();
}

function AddToFavorite() {
    $.ajax({
        type: "POST",
        url: webMethodSunscribeToChannel,
        data: '{"userId":' + "'" + userIdCheked + "'" + ',"channelTubeId":' + "'" + channelTubeId + "'" + ',"clientTime":' + "'" + globalClientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $(".options .addtofavorite").removeAttr("onclick").attr("onclick", "RemoveFromFavorite()").text("").text("Remove from Favorites").removeAttr("title").attr("title", "Remove from Favorites");
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
    $.ajax({
        type: "POST",
        url: webMethodUnsuscribeFromChannel,
        data: '{"userId":' + "'" + userIdCheked + "'" + ',"channelTubeId":' + "'" + channelTubeId + "'" + ',"clientTime":' + "'" + globalClientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $(".options .addtofavorite").removeAttr("onclick").attr("onclick", "AddToFavorite()").text("").text("Add to Favorites").removeAttr("title").attr("title", "Add to Favorites");
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
    void 0;
    if (playingVideoId != 0) {
        $.ajax({
            type: "POST",
            url: webMethodAddVideoToArchive,
            data: '{"videoId":' + playingVideoId + ',"clientTime":' + "'" + globalClientTime + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $(".watchlater").text("").text("added");
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
    void 0;
    $("#txtVideoTitle").val(currVideoTitle);
    $('#abuseModal').lightbox_me({
        centered: true,
        onLoad: function () {

        }
    });

}

function SendAbuseReport() {
    //string selectedOption, string videoTitle, string comments,  string senderDateTime, int channelScheduleId=0, int videoTubeId=0, int senderUserId=0,int channelId=0)
    var selectedOption = $('#ddlCategory option:selected').text();

    var videoTitle = $("#txtVideoTitle").val();
    var comments = $("#txtComments").val();
    var videoTubeId = playingVideoId;
    $('#abuseModal').ajaxStart(function () {
        $("#lblMsg").text("please wait");
    })
    var params = '{"selectedOption":' + "'" + selectedOption + "'" +
                 ',"videoTitle":' + "'" + videoTitle + "'" +
                 ',"comments":' + "'" + comments + "'" +
                 ',"senderDateTime":' + "'" + globalClientTime + "'" +
                 ',"channelScheduleId":' + currChannelSchedule +
                 ',"videoTubeId":' + videoTubeId +
                 ',"senderUserId":' + userIdCheked +
                 ',"channelId":' + channelTubeId + '}';
    void 0;
    $.ajax({
        type: "POST",
        url: webMethodSendAbuseReport,
        data: params,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            //$("#lblMsg").text(response.d);
            //setTimeout(function () { closeAbuse() }, 5000);
            closeAbuse();
            alertify.success(response.d);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            void 0;
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
    $(".sideBarOptions .addtofavorite").removeClass("addFavoritesActive");
    $(".sideBarOptions .toprated").removeClass("topRatedActive");
    $(".sideBarOptions .rings").removeClass("myChannelsActive");
    window.clearTimeout(favoriteChannelSetTimeOut);
    window.clearTimeout(topChannelSetTimeOut);
    var params = '{"clientTime":' + "'" + globalClientTime + "'" + ',"channelId":' + channelTubeId + ',"userId":' + userIdCheked + '}';
    //if (player != null)
    //{
    //    player.destroy();
    //}
    //(function poll() {
    //        SchedulePolling();

    //    })();
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
                $("#sideBarChannel .sideContentHolder").html("").html(SchedulesHtml);

                $(".sideContentHolder a#order_" + playBackOrderNum).addClass("playingNow");
                $(".sideContentHolder a#order_" + playBackOrderNum + " .playingNowShow").show();
                var playingNext = playBackOrderNum + 1;
                $(".sideContentHolder a#order_" + playingNext + " .playingNextShow").show();
            }
            else {
                $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>" + response.d.Message + "</>");
            }
        }
    });



};

function GetMyChannels() {
    window.clearTimeout(favoriteChannelSetTimeOut);
    window.clearTimeout(SchedulePollingTimer);
    window.clearTimeout(topChannelSetTimeOut);
    $.ajax({
        type: "POST",
        url: webMethodGetMyChannels,
        data: '{"userId":' + "'" + userIdCheked + "'" + ',"clientTime":' + "'" + globalClientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            myChannels = Controls.BuildChannelControlForChannelPage(response, false);
            if (myChannels.length > 0) {
                $(".sideContentHolder").html("").html(myChannels);
            }
            else {
                $(".sideContentHolder").html("").html("You have no channels of your own yet");
            }

        },
        complete: function () {

        },
        error: function () {
        }
    });
}

function ShowFavorites() {
    executeSchedulePolling = 0;
    executeTopChannels = 0;
    window.clearTimeout(SchedulePollingTimer);
    window.clearTimeout(topChannelSetTimeOut);
    $(".iconDescription").html("favorite channels");
    $(".sideContentHolder").html("");
    $(".sideBarOptions .schedule").removeClass("scheduleactive");
    $(".sideBarOptions .addtofavorite").addClass("addFavoritesActive");
    $(".sideBarOptions .toprated").removeClass("topRatedActive");
    $(".sideBarOptions .rings").removeClass("myChannelsActive");

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
    $(".sideBarOptions .addtofavorite").removeClass("addFavoritesActive");
    $(".sideBarOptions .toprated").addClass("topRatedActive");
    $(".sideBarOptions .rings").removeClass("myChannelsActive");
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
    $(".sideBarOptions .addtofavorite").removeClass("addFavoritesActive");
    $(".sideBarOptions .toprated").removeClass("topRatedActive");
    $(".sideBarOptions .rings").addClass("myChannelsActive");
    window.clearTimeout(topChannelSetTimeOut);
    window.clearTimeout(favoriteChannelSetTimeOut);
    window.clearTimeout(SchedulePollingTimer);

    GetMyChannels();
};


