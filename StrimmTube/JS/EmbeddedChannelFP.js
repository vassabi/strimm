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
var webMethodTvGuideUserChannelsById = "/WebServices/ChannelWebService.asmx/GetTvGuideUserChannelsById";
var webMethodGetEmbeddedTvGuideByUserIdAndPageIndex = "/WebServices/ChannelWebService.asmx/GetEmbeddedTvGuideByUserIdAndPageIndex";
var webMethodGetLiveVideos = "/WebServices/ChannelWebService.asmx/GetAllVideoLiveTubePosByChannelIdAndDate";
var webMethodTvGuideAllChannels = "/WebServices/ChannelWebService.asmx/GetTvGuideAllChannels";

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
var timeline;
var startTime = 0;
var autoplay = 0;
var isPlayerOnMute = true;
var isBrowseViewActive = false;
var pageIndex = 1;
var prevPageIndex = 1;
var nextPageIndex = 1;
var pageCount = 0;
// Youtube=1, Vimeo=2, DM=3
var providerId = 1;

var isAllChannelMode = false;
var isTopChannelMode = false;
var isMyChannelMode = false;
var isFavoriteChannelMode = false;
var isChannelByLanguageMode = false;
var isChannelByKeywordMode = false;
var isChannelByCategoryMode = false;
var isCreatorChannelsMode = false;
var isBrowseViewActive = false;

var loadFirstChannel = true;
var activeTabChangedOnTvGuide = false;



var pageSize = 4;





var mX, mY, isOn = false;
$(document).mousemove(function (e) {
    mX = e.pageX;
    mY = e.pageY;
}).mouseover();
var livePlaylist = null;
$(document).ready(function () {
    if (!String.prototype.startsWith) {
        String.prototype.startsWith = function (searchString, position) {
            position = position || 0;
            return this.indexOf(searchString, position) === position;
        };
    }

    //check if live videos are scheduled
    var clientTime = new Date();
    var clientTimeInUTC = moment.utc(clientTime).format('MM/DD/YYYY HH:mm');
    var params = '{"channelTubeId":' + channelTubeId + ',"targetDate":' + "'" + clientTimeInUTC + "'" + '}';
    livePlaylist = $.ajax({
        type: 'POST',
        url: webMethodGetLiveVideos,
        dataType: 'json',
        data: params,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            var data = response.hasOwnProperty("d") ? response.d : response;
            if (data.length != 0) {
                if (executeSchedulePolling != 0) {
                    $(".embedLive").show();
                }

            }
            else {

                $(".embedLive").hide();

            }
        },
        complete: function (res) {
            //  $(".nano").nanoScroller({ alwaysVisible: false });
        }

    });
    //END


    checkScreenSize();

    var container = document.getElementById('mytimeline');
    var start = new Date();
    var end = new Date();

    end.setHours(start.getHours() + 1);
    var options = {
        stack: false,
        start: start,
        end: end,
        editable: false,
        margin: {
            item: 10, // minimal margin between items
            axis: 10   // minimal margin between items and the axis
        },
        orientation: 'top',
        clickToUse: true,
        timeAxis: { scale: 'minute', step: 10 },
        showCurrentTime: true
    };

    timeline = new vis.Timeline(container, null, null, options);
    //showMyChannels();

});

function isMobile() {
    return (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino|android|ipad|playbook|silk/i.test(navigator.userAgent || navigator.vendor || window.opera) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test((navigator.userAgent || navigator.vendor || window.opera).substr(0, 4)))
};

function setAutoplay() {
    if (isMobile()) autoplay = 0;
}

var checkScreenSize = function () {
    if (screen.width < 768) {
        pageSize = 4;
    }
};

setAutoplay();

//GetScheduleData();

function SchedulePolling() {
    GetScheduleData();
};

function Unmute() {
    isPlayerOnMute = false;

    if (player !== undefined) {
        if (providerId == 1) {
            player.unMute(100);
        }
        else if (providerId == 3) {
            dplayer.setMuted(false);
        }
        else {
            player.api('setVolume', 1);
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


function InitFlowPlayer(playList, videoPlaylistIndex, seekTime) {
    //  console.log(playList);
    ResetUIForProperPlayer(false, false);
    fpKey = $('#complete').data('key');

    if (seekTime == undefined) {
        seekTime = 0;
    }

    var newPlaylist = [];
    //   console.log(playlist);
    // var identifier = "'" + schedule.ChannelTubeId + '-' + schedule.VideoTubeId + '-' + schedule.PlaybackOrderNumber + "'";

    if (playList && playList.length > 0) {
        $.each(playList, function (i, d) {
            //var playlistItem = [{ mp4: d.VideoKey }];
            var typeValue = "video/" + d.VideoProviderName;
            var srcValue = "";
            var startTime = (d.PlayerPlaybackStartTimeInSec).toFixed()
            if (d.VideoProviderName == "youtube") {
                if (d.ProviderVideoId.startsWith("http")) {
                    srcValue = d.ProviderVideoId
                }
                else {
                    srcValue = "https://youtube.com/watch?v=" + d.ProviderVideoId;
                }

            }
            if (d.VideoProviderName == "vimeo") {
                if (d.ProviderVideoId.indexOf("http") === 0) {
                    srcValue = d.ProviderVideoId;
                }
                else {
                    srcValue = "https://vimeo.com/" + d.ProviderVideoId
                }
            }
            if (d.VideoProviderName == "dailymotion") {
                typeValue = "video/dailymotion";
                if (d.ProviderVideoId.indexOf("http") === 0) {
                    srcValue = d.ProviderVideoId;
                }
                else {
                    srcValue = "https://www.dailymotion.com/video/" + d.ProviderVideoId
                }

            }
            if (d.VideoProviderName == "custom") {
                var ext = d.ProviderVideoId.substr((d.ProviderVideoId.lastIndexOf('.') + 1));
                typeValue = "video/" + ext;
                srcValue = d.ProviderVideoId;
            }
            var playlistItem = { sources: [{ type: typeValue, src: srcValue, start: startTime }] };

            newPlaylist.push(playlistItem);


        });

    }

    void 0;
    //    console.log(JSON.stringify(newPlaylist));
    if (playLiveFirst && livePlaylist.responseJSON.d.length > 0) {
        $(".embedLive").trigger("click");
        $(".btnPlayLiveVideo:first").trigger("click");
        setTimeout(function () {
            $(".closeLivePanel").trigger("click");

        }, 4000)
    }
    else {
        PlayVideoWithFlowPlayer(newPlaylist, videoPlaylistIndex, seekTime);
    }

    //  PlayVideoWithHtml5FlowPlayer(newPlaylist, videoPlaylistIndex, seekTime, fpKey);
}
function updateProgressBar(duration, time) {
    // Update the value of our progress bar accordingly.
    //  $('#progress-bar').val((time / duration) * 100);
    //var percent = Math.floor((100 / duration) * time);
    $('.hp_range').stop(true, true).animate({ 'width': (time + .25) / duration * 100 + '%' }, 250, 'linear');
    // $('#progress-bar').val(percent);

}
function UpdateTimerDisplay(startTime, videoDuration) {


    $("#current-time").text("").text(GetDurationString(startTime));
    $("#duration").text("").text(GetDurationString(videoDuration));
}
function GetDurationString(seconds) {

    var hours = Math.floor(seconds / (60 * 60));
    seconds -= hours * (60 * 60);

    var mins = Math.floor(seconds / (60));
    seconds -= mins * (60);

    var duration = '';

    if (hours > 0) {
        duration += (hours + 'hr ');
    }

    if (duration != '' || mins > 0) {
        duration += (mins + 'min ');
    }

    duration += (seconds + 'sec');

    // console.log(duration);
    return duration;
};

function isIphone() {
    return (
        (navigator.platform.indexOf("iPhone") != -1) || (navigator.platform.indexOf("iPod") != -1)
    );
}

function PlayVideoWithFlowPlayer(newPlaylist, globalVideoPlaylistIndex, seekTime) {
    // console.log(newPlaylist)
    var nextVideoPlaylistIndex = 0;
    var currentVideoPlaylistIndex = 0;
    if (globalVideoPlaylistIndex >= 0) {
        currentVideoPlaylistIndex = globalVideoPlaylistIndex;
        nextVideoPlaylistIndex = globalVideoPlaylistIndex;
    }
    var fplayer;
    void 0
    !function () {


        fplayer = flowplayer("#complete", {

            youtube: {
                hasAds: false,
                defaultQuality: "low",
                autoplay: autoplay,
                html5: 1,
                allowFullScreen: 1
            },
            vimeo: {
                //advance: true,
                //apidata: true



            },
            autoplay: false,
            splash: false,
            playlist: newPlaylist




        });
        fplayer.on("medialevels", function (e, api, levels) {

        }).one("ready", function (e, api, data) {

            //if (muteOnStartup) {
            //    api.mute(true);
            //    //  $('#mute-toggle').text("").text('volume_off');
            //}
            //else {
            //    api.mute(false);
            //    api.volume(1);
            //    // $('#mute-toggle').text("").text('volume_on');
            //}
            //  remove flowplayer logo
            $("#complete").find("a").css("display", "none");

            $(".fp-ui").next().remove();

            void 0;
            $(".fp-ui").css("display", "none");

            if (api.video.type == "video/vimeo") {
                $(".flowplayer .fp-controls").css("display", "none");

            }
            else {
                $(".flowplayer .fp-controls").css("display", "flex");
            }

            $(".ytp-title-channel-logo").css("display", "none");
            $(".ytp-title-channel-logo").remove();

            var currentlyPlayingVideo = fplayer.video;
            var videoType = fplayer.video.type;

            // **********************************************************************************
            // WORK AROUND FOR LATEST CHROME RELEASE ISSUE WITH FROSEN PLAY BUTTON
            $(".fp-playlist").css("display", "none");
            // **********************************************************************************

            if (allowControls == false) {
                $(".fp-icon.fp-playbtn, .fp-elapsed, .fp-timeline.fp-bar, .fp-duration, .fp-remaining").remove();

                //  $(".fp-ui>.fp-play, .fp-ui>.fp-pause").remove();

            }
            if (!allowControls) {
                $("#startBtn,#prevBtn,#nextBtn,#current-time").hide();
            }
            if (fplayer.video.index == 0) {


                if (!isIphone()) {
                    api.seek(seekTime);
                }


                $("#PlayerHolder").css("background-image", "none");

                if (playlist) {
                    currentVideo = playlist[fplayer.video.index];
                    playingVideoId = currentVideo.VideoTubeId;
                }

                if (currentVideo) {
                    AddVideoViewStartForUser(currentVideo.VideoTubeId);
                }

            }

            var index = api.video.index;
            UpdateCurrentlyPlayingVideoInfoOnUI(playlist[index])
            UpdateUIForNewVideo(index);
            void 0;
            api.play();

            //}
        }).one("resume", function (e, api) {
            // **********************************************************************************
            // WORK AROUND FOR LATEST CHROME RELEASE ISSUE WITH FROSEN PLAY BUTTON
            $(".fp-ui").css("display", "");
            $(".ytp-title-channel-logo").css("display", "none");
            $(".ytp-title-channel-logo").remove();
            $("#complete").find("a").css("display", "none");

          // **********************************************************************************
            if (fplayer.video.index == 0) {
                if (isIphone()) {
                    api.seek(seekTime)
                }

            }

        }
        ).on("ready", function (e, api) {
            api.play();
            $(".fp-ui").css("display", "");
            $(".ytp-title-channel-logo").css("display", "none");
            $(".ytp-title-channel-logo").remove();
            $("#complete").find("a").css("display", "none");
           //if (muteOnStartup) {
            //    api.mute(true);
            //    //  $('#mute-toggle').text("").text('volume_off');
            //}
            //else {
            //    api.mute(false);
            //    api.volume(1);
            //    // $('#mute-toggle').text("").text('volume_on');
            //}
            //$(".fp-player").next().remove();

            //var currentlyPlayingVideo = fplayer.video;
            //var videoType = fplayer.video.type;
            //$(".fp-playlist").css("display", "none");


            //if (fplayer.video.index == 0) {

            //    api.seek(seekTime);

            //    $("#PlayerHolder").css("background-image", "none");

            //    if (playlist) {
            //        currentVideo = playlist[fplayer.video.index];
            //        playingVideoId = currentVideo.VideoTubeId;
            //    }

            //    if (currentVideo) {
            //        AddVideoViewStartForUser(currentVideo.VideoTubeId);
            //    }
            //}
            //$('.actionsCLHolder .muteCL').on('click', function () {
            //    //  var mute_toggle = $(this);

            //    if (api.muted) {
            //        api.mute(false);
            //        $('.actionsCLHolder .muteCL').css("background-image", "url('/images/muteOFF-white.png')")
            //    }
            //    else {
            //        api.mute(true);
            //        $('.actionsCLHolder .muteCL').css("background-image", "url('/images/muteON-white.png')")
            //    }
            //});
            //$("#btnNext").on("click", function () {

            //    var index = api.video.index;

            //    if (index != undefined) {
            //        api.play(index + 1);
            //    }

            //    UpdateCurrentlyPlayingVideoInfoOnUI(playlist[index])
            //    UpdateUIForNewVideo(index);
            //});
            //if (api.video.index == 0) {
            //    $("#btnPrev").css("opacity", "0.3");
            //}
            //else {
            //    $("#btnPrev").css("opacity", "1");
            //    $("#btnPrev").on("click", function () {
            //        var index = api.video.index;

            //        if (index != undefined && index != 0) {
            //            api.play(index - 1);
            //            UpdateCurrentlyPlayingVideoInfoOnUI(playlist[index - 1])
            //            UpdateUIForNewVideo(index - 1);
            //        }

            //    });
            //}


        }).on("finish", function (e, api, data) {

            void 0;
            var currentlyPlayingVideo = api.video;
            var isLastVideo = currentlyPlayingVideo.is_last;

            if (currentlyPlayingVideo) {
                if (isLastVideo) {
                    GetScheduleData(activeChannelId, true);

                }
                //else {
                //    var index = api.video.index;

                //    if (index != undefined) {
                //        api.play(index + 1);
                //    }

                //ProcessNextVideo(index + 1, 0);
                var index = api.video.index;

                UpdateCurrentlyPlayingVideoInfoOnUI(playlist[index + 1])
                UpdateUIForNewVideo(index + 1);

                //}



            }
        });



    }();
    //!function () {

    //    fplayer = flowplayer("#complete", {

    //        youtube: {
    //            hasAds: false
    //        },
    //        vimeo: {
    //            advance: true,
    //            apidata: true
    //        },
    //        autoplay: true,
    //        splash: false,
    //        playlist: newPlaylist,
    //        volume: '1'

    //    });
    //    fplayer.on("medialevels", function (e, api, levels) {

    //    }).on("ready", function (e, api, data) {
    //        if (muteOnStartup) {
    //            api.mute(true);
    //          //  $('#mute-toggle').text("").text('volume_off');
    //        }
    //        else {
    //            api.mute(false);
    //            api.volume(1);
    //           // $('#mute-toggle').text("").text('volume_on');
    //        }
    //        //   console.log("ready");
    //        //$("#btnNext").on("click", function () {
    //        //    //  console.log("next");
    //        //    var index = api.video.index;

    //        //    if (index != undefined) {
    //        //        api.play(index + 1);
    //        //    }

    //        //    UpdateCurrentlyPlayingVideoInfoOnUI(playlist[index])
    //        //    UpdateUIForNewVideo(index + 1);
    //        //});
    //        if (api.video.index == 0) {
    //           // $("#btnPrev").css("opacity", "0.3");
    //        }
    //        else {
    //            //$("#btnPrev").css("opacity", "1");
    //            //$("#btnPrev").on("click", function () {
    //            //    var index = api.video.index;
    //            //    //   console.log(index);
    //            //    if (index != undefined && index != 0) {
    //            //        api.play(index - 1);
    //            //        UpdateCurrentlyPlayingVideoInfoOnUI(playlist[index - 1])
    //            //        UpdateUIForNewVideo(index - 1);
    //            //    }

    //            //});
    //        }

    //    }).one("ready", function (e, api, data) {
    //        $(".fp-player").next().remove();
    //        //if (allowControls == false) {
    //        //    $(".fp-icon.fp-playbtn, .fp-elapsed, .fp-timeline.fp-bar, .fp-duration, .fp-remaining").remove();
    //        //    $("a.fp-fullscreen").css("display","none")
    //        //    $(".fp-ui>.fp-play, .fp-ui>.fp-pause").remove();

    //        //}
    //        var index = api.video.index;
    //        console.log(api.video)
    //        UpdateCurrentlyPlayingVideoInfoOnUI(playlist[index])
    //        //if (!allowControls) {
    //        //    $("#startBtn,#prevBtn,#nextBtn,#current-time").hide();
    //        //}
    //        console.log(api.volume())
    //        console.log(muteOnStartup);
    //        if (muteOnStartup) {
    //            api.mute(true);
    //          //  $('#mute-toggle').text("").text('volume_off');
    //        }
    //        else
    //        {
    //            api.mute(false);
    //            api.volume(1);
    //           // $('#mute-toggle').text("").text('volume_on');
    //        }
    //        //  console.log("ready");
    //        var currentlyPlayingVideo = fplayer.video;
    //        var videoType = fplayer.video.type;
    //        $(".fp-playlist").css("display", "none");
    //        if (fplayer.video.index == 0) {

    //            api.seek(seekTime);

    //            $("#PlayerHolder").css("background-image", "none");

    //            if (playlist) {
    //                currentVideo = playlist[fplayer.video.index];
    //                playingVideoId = currentVideo.VideoTubeId;
    //            }

    //            if (currentVideo) {
    //                AddVideoViewStartForUser(currentVideo.VideoTubeId);
    //            }

    //            //timeUpdateInterVal = setInterval(function () {
    //            //    var time = api.video.time;
    //            //    var duration = api.video.duration;
    //            //    if (time == undefined) {
    //            //        time = "00";
    //            //    }
    //            //    if (duration == undefined) {
    //            //        duration = "00";
    //            //    }
    //            //    UpdateTimerDisplay(time, duration);
    //            //    updateProgressBar(duration, time);
    //            //}, 1000)
    //            //$('#mute-toggle').on('click', function () {
    //            //    //  var mute_toggle = $(this);

    //            //    if (api.muted) {
    //            //        api.mute(false);
    //            //        $('#mute-toggle').text("").text('volume_up');
    //            //    }
    //            //    else {
    //            //        api.mute(true);
    //            //        $('#mute-toggle').text("").text('volume_off');
    //            //    }
    //            //});
    //            //$("#btnPlayPause").on("click", function () {

    //            //    if (api.playing) {
    //            //        $("#btnPlayPause").text("").text("play_arrow")
    //            //        api.pause();
    //            //    }
    //            //    else {
    //            //        $("#btnPlayPause").text("").text("pause")
    //            //        api.play();
    //            //    }
    //            //});
    //            //$("#btnPause").on("click", function () {

    //            //    $("#btnPause").removeAttr("id").attr("id", "btnPlay").text("").text("play");
    //            //    api.pause();


    //            //});
    //            //$("#btnstart").on("click", function () {
    //            //    api.seek(0);
    //            //});
    //            //$("#btnFullscreen").on("click", function () {
    //            //    api.fullscreen();
    //            //});
    //            //$("#btnNext").on("click", function () {

    //            //    var index = api.video.index;

    //            //    if (index != undefined) {
    //            //        api.play(index + 1);
    //            //    }

    //            //    UpdateCurrentlyPlayingVideoInfoOnUI(playlist[index + 1])
    //            //    UpdateUIForNewVideo(index + 1);
    //            //});
    //            if (api.video.index == 0) {
    //               // $("#btnPrev").css("opacity", "0.3");
    //            }
    //            else {
    //                //$("#btnPrev").css("opacity", "1");
    //                //$("#btnPrev").on("click", function () {


    //                //    var index = api.video.index;

    //                //    if (index != undefined) {
    //                //        api.play(index - 1);
    //                //        UpdateCurrentlyPlayingVideoInfoOnUI(playlist[index - 1])
    //                //        UpdateUIForNewVideo(index - 1);
    //                //    }

    //                //});


    //                UpdateCurrentlyPlayingVideoInfoOnUI(playlist[index + 1])
    //                UpdateUIForNewVideo(index + 1);
    //            }


    //            UpdateUIForNewVideo(api.video.index + 1);
    //        }
    //    }).on("finish", function (e, api, data) {

    //        var currentlyPlayingVideo = api.video;
    //        var isLastVideo = currentlyPlayingVideo.is_last;

    //        if (currentlyPlayingVideo) {
    //            if (isLastVideo) {
    //                GetScheduleData(activeChannelId, true);

    //            }
    //            else {
    //                var index = api.video.index;

    //                if (index != undefined) {
    //                    api.play(index + 1);
    //                }

    //                UpdateCurrentlyPlayingVideoInfoOnUI(playlist[index + 1])
    //                UpdateUIForNewVideo(index + 1);
    //            }



    //        }
    //    });



    //}();


};

function UpdateCurrentlyPlayingVideoInfoOnUI(video) {
    void 0;
    if (video != null && video != undefined) {
        var popup = $("#popupVideo");

        if (popup) {

            var jsonDateStartDate = video.PlaybackStartTime;
            var jsonDateEndDate = video.PlaybackEndTime;

            var start = new Date(video.PlaybackStartTimeString); //new Date(jsonDateStartDate.match(/\d+/)[0] * 1);
            var end = new Date(video.PlaybackEndTimeString); //new Date(jsonDateEndDate.match(/\d+/)[0] * 1);

            var now = new Date();

            //if (start <= now && now < end) {
            //    LoadChannel(channelTubeId);
            //    return;
            //}

            var startTime = formatTime(start);
            var endTime = formatTime(end);

            var image = $("#videoImage");
            var record = $(".TLrecordLed");
            var title = $("#videoTitle");
            var description = $("#videoDescription");
            var duration = $("#videoDuration");
            var playtime = $("#videoStartTime");
            var watch = $("#videoWatchNow");

            if (video.Thumbnail != null) {
                image.attr('src', video.Thumbnail);
            }

            record.removeAttr('onclick').attr('onclick', 'WatchItLater(' + video.VideoTubeId + ')');

            if (video.VideoTubeTitle != null && video.VideoTubeTitle != '') {
                title.text(video.VideoTubeTitle);
            }
            else {
                title.text('No title specified by provider.');
            }

            duration.text(GetDurationString(video.Duration));

            playtime.text(startTime + '-' + endTime);

            var command = "LoadChannelAndCloseTvGuide(" + video.ChannelTubeId + ")";

            watch.removeAttr('onclick').attr('onclick', command);

            if (video.Description != null && video.Description != '') {
                description.text(video.Description);
            }
            else {
                description.text('Description was not specified by provider.');
            }


            //popup.css('left', mX);
            //popup.css('top', 150); //mY - (isBrowseViewActive ? 670 : 650));
            popup.css('z-index', 300);


            //var sidebarWidth = $('#tvguide').width();


            //if (state) {
            //    var sidebarWidthnew = sidebarWidth + popup.width();
            //    popup.width(sidebarWidthnew / 4 + 'px').show();

            //}
            //else {
            //    popup.show();
            //    $('#popupVideo').width("0%");
            //    $('#popupVideo').animate({ width: '+=' + sidebarWidth / 4 + 'px' }, 500);
            //    console.log($('#popupVideo').width())
            //    $('#tvguide').animate({ width: '-=' + sidebarWidth / 4 + 'px' }, 500);

            //    state = true;
            //}
            //$("#tvguide").width("100%").animate({ width: '-=22%' }, function () {
            //    popup.show()
            //})




        }
    }
}


function UpdateTimerDisplay(startTime, videoDuration) {


    $("#current-time").text("").text(GetDurationString(startTime));
    $("#duration").text("").text(GetDurationString(videoDuration));
}
function GetDurationString(seconds) {

    // multiply by 1000 because Date() requires miliseconds
    var date = new Date(seconds * 1000);
    var hh = date.getUTCHours();
    var mm = date.getUTCMinutes();
    var ss = date.getSeconds();
    // If you were building a timestamp instead of a duration, you would uncomment the following line to get 12-hour (not 24) time
    // if (hh > 12) {hh = hh % 12;}
    // These lines ensure you have two-digits
    if (hh < 10) { hh = "0" + hh; }
    if (mm < 10) { mm = "0" + mm; }
    if (ss < 10) { ss = "0" + ss; }
    // This formats your string to HH:MM:SS
    return hh + ":" + mm + ":" + ss;
};

function GetScheduleData() {
    var timeOfRequest = getClientTime();

    $.ajax({
        type: 'POST',
        url: webMethodGetChannelPreviewDataJson,
        dataType: 'json',
        data: '{"clientTime":' + "'" + timeOfRequest + "'" + ',"channelId":' + channelTubeId + ',"isEmbeddedChannel":' + true + ',"userId":' + userIdCheked + '}',
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
                //var SchedulesHtml = Controls.BuildVideoScheduleControlForChannel(data.Playlist);
                var firstVideo = playlist[0];

                if (firstVideo) {
                    startTime = parseInt(parseFloat(firstVideo.PlayerPlaybackStartTimeInSec).toFixed());
                }
                else {
                    startTime = 0;
                }
                InitFlowPlayer(playlist, -1, startTime);

                showMyChannels();
                //<- from here to "controls"
                //if (HasMultipleProviders(data.Playlist)) {
                //    ProcessNextVideo(0, startTime);
                //}
                //else {
                //    if (firstVideo.VideoProviderName == "vimeo") {
                //        providerId = 2;
                //        InitVimeoPlayer(playlist, 0, startTime);
                //    }
                //    else if (firstVideo.VideoProviderName == "youtube") {
                //        providerId = 1
                //        if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                //            onYouTubeIframeAPIReadyIE();
                //        }
                //        else {
                //            onYouTubeIframeAPIReady();
                //        }
                //    }
                //    else if (firstVideo.VideoProviderName == "dailymotion") {
                //        providerId = 3;
                //        InitDMotionPlayer(playlist, 0, startTime);
                //    }
                //    else {
                //        providerId = 4;

                //}
                //}

                //$("#sideBarChannel .sideContentHolder").html("").html(SchedulesHtml);
                $("#controls").show();


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
        "    <div id='complete' class='flowplayer fp-mute fp-slim'  data-aspect-ratio='12:5' data-embed='false' data-volume='1' >" +
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

function moveBackInTime() {
    var windowTimeRange = timeline.getWindow();
    var startWindowTime = windowTimeRange.start;
    var endWindowTime = windowTimeRange.end;

    startWindowTime.setTime(startWindowTime.getTime() - 15 * 60000);
    endWindowTime.setTime(endWindowTime.getTime() - 15 * 60000);

    timeline.setWindow(startWindowTime, endWindowTime);
}

function moveForwardInTime() {
    var windowTimeRange = timeline.getWindow();
    var startWindowTime = windowTimeRange.start;
    var endWindowTime = windowTimeRange.end;

    startWindowTime.setTime(startWindowTime.getTime() + 15 * 60000);
    endWindowTime.setTime(endWindowTime.getTime() + 15 * 60000);

    timeline.setWindow(startWindowTime, endWindowTime);
}

function identifyActiveItems() {
    var currentTime = timeline.getCurrentTime();
    if (activeChannels) {
        $.each(activeChannels, function (i, model) {
            var schedules = model.VideoSchedules;

            $.each(schedules, function (k, schedule) {
                var identifier = schedule.ChannelTubeId + '-' + schedule.VideoTubeId + '-' + schedule.PlaybackOrderNumber;

                var jsonDateStartDate = schedule.PlaybackStartTime;
                var jsonDateEndDate = schedule.PlaybackEndTime;

                var start = new Date(schedule.PlaybackStartTimeString); //new Date(jsonDateStartDate.match(/\d+/)[0] * 1);
                var end = new Date(schedule.PlaybackEndTimeString); //new Date(jsonDateEndDate.match(/\d+/)[0] * 1);

                if (start <= currentTime && currentTime < end) {
                    $('#' + identifier).css('color', 'eee');
                }
                else {
                    $('#' + identifier).css('color', '#999');
                }
            });
        });
    }
}

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

//function ShowMyChannels() {
//    executeSchedulePolling = 0;
//    executeTopChannels = 0;
//    $(".iconDescription").html("my channels");
//    $(".sideContentHolder").html("");
//    $(".sideBarOptions .schedule").removeClass("scheduleactive");
//    $(".sideBarOptions .favoriteChannels").removeClass("favoriteChannelsActive");
//    $(".sideBarOptions .toprated").removeClass("topRatedActive");
//    $(".sideBarOptions .mychannels").addClass("mychannelsactive");
//    $(".sideBarOptions .myChat").removeClass("myChatActive");
//    window.clearTimeout(topChannelSetTimeOut);
//    window.clearTimeout(favoriteChannelSetTimeOut);
//    window.clearTimeout(SchedulePollingTimer);

//    GetMyChannels();
//};

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

function showMyChannels(index) {

    var date = new Date();
    var timeOfRequest = (date.getMonth() + 1) + '-' + date.getDate() + '-' + date.getFullYear() + '-' + date.getHours() + '-' + date.getMinutes();

    pageIndex = (index == undefined) ? 1 : index;

    $('.TLmyChannels').addClass('TLmyChannelsActive');
    $(".popupVideo").hide();
    $.ajax({
        type: 'POST',
        url: webMethodGetEmbeddedTvGuideByUserIdAndPageIndex, //webMethodTvGuideUserChannelsById,
        dataType: 'json',
        data: '{"clientTime":' + "'" + timeOfRequest + "'" + ', "userId":' + channelCreatorUserId + ', "domain":' + "'" + embeddedDomainName + "'" + ', "pageIndex":' + pageIndex + ', "pageSize":' + 4 + '}',
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            displayedChannels = response.d;
            void 0
            if (displayedChannels) {
                displayTvGuide(displayedChannels);
                highlightActiveChannel();

                if (displayedChannels.ActiveChannels != null && displayedChannels.ActiveChannels.length > 0) {
                    setPaginationControls(displayedChannels);
                }
            }
            else {
                resetPaginationControls();
            }

            $("#loadingDiv").hide();
        },
        complete: function () {
        }
    });
}

function displayTvGuide(data) {

    var groups = [];
    var items = [];

    timeline.setGroups(null);
    timeline.setItems(null);

    if (data && data.ActiveChannels != null && data.ActiveChannels.length > 0) {
        $('.noChannels').hide();

        var date = new Date();

        activeChannels = data.ActiveChannels;

        var channelOrder = 2;
        var activeChannelSet = false;

        $.each(data.ActiveChannels, function (i, channelModel) {
            var schedules = channelModel.VideoSchedules;
            var channel = channelModel.Channel;
            var user = channelModel.User;
            var newChannelOrder = channelOrder + i;

            if (schedules) {
                var group;
                if (activeChannelId == channel.ChannelTubeId) {
                    group = {
                        id: channel.ChannelTubeId,
                        content: '<div id="chGroup' + channel.ChannelTubeId + '" class="visChannelsInfoHolder"> <div class="visChannelName" style="cursor:pointer;" onclick="LoadChannelAndCloseTvGuide(' + channel.ChannelTubeId + ', this)">' + channel.Name + '</div><div class="info" style="float:right;cursor:pointer;" onclick="displayChannelInfoPopup(' + channel.ChannelTubeId + ')">i</div></div>',
                        value: 1
                    };
                    channelOrder = channelOrder - 1;
                    activeChannelSet = true;
                }
                else {
                    group = {
                        id: channel.ChannelTubeId,
                        content: '<div id="chGroup' + channel.ChannelTubeId + '" class="visChannelsInfoHolder"> <div class="visChannelName" style="cursor:pointer;" onclick="LoadChannelAndCloseTvGuide(' + channel.ChannelTubeId + ', this)">' + channel.Name + '</div><div class="info" style="float:right;cursor:pointer;" onclick="displayChannelInfoPopup(' + channel.ChannelTubeId + ')">i</div></div>',
                        value: newChannelOrder
                    };
                }


                $.each(schedules, function (j, schedule) {
                    var jsonDateStartDate = schedule.PlaybackStartTime;
                    var jsonDateEndDate = schedule.PlaybackEndTime;

                    var start = new Date(schedule.PlaybackStartTimeString); //new Date(jsonDateStartDate.match(/\d+/)[0] * 1);
                    var end = new Date(schedule.PlaybackEndTimeString); //new Date(jsonDateEndDate.match(/\d+/)[0] * 1);

                    var identifier = "'" + schedule.ChannelTubeId + '-' + schedule.VideoTubeId + '-' + schedule.PlaybackOrderNumber + "'";
                    items.push({
                        id: schedule.ChannelTubeId + '-' + schedule.VideoTubeId + '-' + schedule.PlaybackOrderNumber,
                        group: channel.ChannelTubeId,
                        start: start,
                        end: end,
                        content: '<div onclick="displayVideoInfoPopup(' + '' + identifier + '' + ')" ><snap id="' + schedule.ChannelTubeId + '-' + schedule.VideoTubeId + '-' + schedule.PlaybackOrderNumber + '" style="text-overflow:ellipsis;max-width:100px;cursor:pointer;" >' + schedule.VideoTubeTitle + '</span></div>'
                    });

                });

                groups.push(group);

            }
        });

        if (activeChannelSet == false) {
            $.each(groups, function (i, group) {
                group.value = 1 + i;
            });
            activeTabChangedOnTvGuide = true;
        }

        var date2 = new Date();

        date2.setHours(date.getHours() + 1);

        var options = {
            stack: false,
            start: new Date(),
            end: date2,
            editable: false,
            margin: {
                item: 10, // minimal margin between items
                axis: 10   // minimal margin between items and the axis
            },
            orientation: 'top',
            clickToUse: false,
            timeAxis: { scale: 'minute', step: 15 },
            showCurrentTime: true,
            format: {
                majorLabels: {
                    millisecond: 'HH:mm:ss',
                    second: 'D MMMM HH:mm',
                    minute: 'MMMM D, YYYY',
                    hour: 'ddd D MMMM',
                    weekday: 'MMMM YYYY',
                    day: 'MMMM YYYY',
                    month: 'YYYY',
                    year: 'YYYY'
                }, minorLabels: {
                    millisecond: 'SSS',
                    second: 's',
                    minute: 'h:mm a',
                    hour: 'h:mm a',
                    weekday: 'ddd D',
                    day: 'D',
                    month: 'MMM',
                    year: 'YYYY'
                },

            },
            //height: 500,
            //moveable: true,
            groupOrder: function (a, b) {
                return a.value - b.value;
            },
            groupEditable: true,
            showMajorLabels: true,
            zoomable: false
        };

        timeline.setOptions(options);
        timeline.setGroups(groups);
        timeline.setItems(items);

        timeline.on('currentTimeTick', function (properties) {
            var currentTime = timeline.getCurrentTime();
            var nextDayTime = new Date();

            identifyActiveItems();

            nextDayTime.setHours(23, 55, 0, 0);

            if (currentTime > nextDayTime) {
                //console.log("retrieve next day schedule");
            }
        });

    }
    else {
        resetPaginationControls();
        $('.noChannels').show();
    }
};
function resetPaginationControls(index) {
    pageIndex = 1;
    pageCount = 1;
    nextPageIndex = 1;
    prevPageIndex = 1;

    $("#status").html('Page 1 of 1');

    $("#divPagination").hide();
    $('.noChannels').show();
}

function setPaginationControls(data) {
    if (data) {
        pageIndex = data.PageIndex;
        pageCount = data.PageCount;

        nextPageIndex = (pageIndex == pageCount) ? pageIndex : pageIndex + 1;
        prevPageIndex = (pageIndex == 1) ? 1 : pageIndex - 1;

        $("#status").html('Page ' + pageIndex + ' of ' + pageCount);

        if (pageIndex != pageCount || pageCount > 1) {
            $("#divPagination").show();
        }
        else {
            $("#divPagination").hide();
        }
    }
    else {
        resetPaginationControls();
    }
}
function TriggerTVGuide() {
    closeVideoPopup();
    if (!$(".TVGuideBottom").is(":visible")) {
        showMyChannels();
        openTvGuide();
        CloseLivePanel();
        // $("#ancShowTvGuide").text("Guide")
    }
    else {

        closeTvGuide();
        //$("#ancShowTvGuide").text("Guide")
    }

}
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

function RedirectToStrimm() {
    window.top.location.replace('https://www.strimm.com/');
}



function closeChannelPopup() {
    $("#popupChannel").hide();
    $("#popupVideo").hide();
    $(".creatorInfoPopUP").hide();
}

function closeCreatorInfoPopup() {
    $("#popupChannel").hide();
    $("#popupVideo").hide();
    $(".creatorInfoPopUP").hide();
    HideCreatorBio();
}

function openCreatorInfoPopup() {
    HideCreatorBio();
    $(".creatorInfoPopUP").show();
}

function openVideoPopup() {
    closeChannelPopup();
    //closeVideoPopup();

    $("#popupVideo").show();
}

function openChannelPopup() {
    closeChannelPopup();
    //closeVideoPopup();

    $("#popupChannel").show();
}

function displayChannelInfoPopup(channelIdentifier) {
    closeChannelPopup();
    // closeVideoPopup();

    var targetChannel = null;
    var user = null;

    if (!$('#popupChannel').is(':visible')) {
        if (activeChannels) {
            $.each(activeChannels, function (i, model) {
                var channel = model.Channel;
                user = model.User;

                if (channel.ChannelTubeId == channelIdentifier) {
                    targetChannel = channel;
                }
            });

            if (targetChannel) {
                var popup = $("#popupChannel");

                if (popup) {
                    var channelAvatar = $("#channelAvatar");
                    var userAvatar = $(".TLcreatorImg");
                    var channelName = $("#channelName");
                    var channelDesc = $("#channelDesc");
                    var channelCreator = $("#creatorName");
                    var watch = $("#channelWatchNow");

                    if (targetChannel.PictureUrl != '' && targetChannel.PictureUrl != null && targetChannel.PictureUrl != '') {
                        channelAvatar.css('background-image', "url('" + targetChannel.PictureUrl + "')");
                    }
                    else {
                        channelAvatar.css('background-image', "url('/images/comingSoon.jpg')");
                    }
                    channelName.text(targetChannel.Name);

                    if (targetChannel.Description != '' && targetChannel.Description != null) {
                        channelDesc.text(targetChannel.Description);
                    }
                    else {
                        channelDesc.text('Thank you for visiting my channel. Watch it and enjoy!');
                    }
                    channelCreator.text(targetChannel.ChannelOwnerUserName);
                    channelCreator.prop("href", "/" + targetChannel.ChannelOwnerUrl);

                    //if (user.ProfileImageUrl != '' && user.ProfileImageUrl != null) {
                    //    userAvatar.css('background-image', "url('" + user.ProfileImageUrl + "')");
                    //}
                    //else {
                    //    userAvatar.css('background-image', "url('" + (user.Gender == 'Male') ? defaultMaleAvatar : defaultFemaleAvatar + "')");
                    //}

                    var command = "LoadChannelAndCloseTvGuide(" + targetChannel.ChannelTubeId + ")";

                    watch.removeAttr('onclick').attr('onclick', command);

                    //popup.css('left', mX);
                    //popup.css('top', 150); //mY - (isBrowseViewActive ? 680 : 525));
                    popup.css('z-index', 300);

                    popup.show();
                }
            }
        }
    }
    else {
        $("#popupChannel").hide();
    }
}
var state = false;
function OpenVideoInfo() {
    $(".OpenPopupVideoInfo").width("0%").hide();
    var sidebarWidth = $('#tvguide').width();
    var popup = $("#popupVideo");

    if (state) {
        var sidebarWidthnew = sidebarWidth + popup.width();
        popup.width(sidebarWidthnew / 4 + 'px').show();

    }
    else {
        popup.show();
        $('#popupVideo').width("0%");
        $('#popupVideo').animate({ width: '+=' + sidebarWidth / 4 + 'px' }, 500);

        $('#tvguide').animate({ width: '-=' + popup.width() + 'px' }, 500);
        void 0
        state = true;
    }

}
function closeVideoPopup() {

    $(".TLrecordLed").removeClass('TLrecordLedActive');
    var popupWidth = $("#popupVideo").width();
    var tvguideWidth = $('#tvguide').width();
    void 0;
    $('#popupVideo').animate({ width: '-=0%' }, 500);

    $('#tvguide').animate({ width: "100%" }, 500);
    $("#popupVideo").width("0%").hide();
    state = false;
    $(".OpenPopupVideoInfo").width("25px").show();

}

function displayVideoInfoPopup(itemIdentifier) {
    closeChannelPopup();
    // closeVideoPopup();

    var videoSchedule;
    var targetChannel;
    var channelTubeId;
    var videoTubeId;
    var playbackOrder;

    if (!$('#popupVideo').is(':visible')) {
        if (activeChannels) {
            var parts = itemIdentifier.split("-");
            channelTubeId = parts[0];
            videoTubeId = parts[1];
            playbackOrder = parts[2];

            $.each(activeChannels, function (i, model) {
                if (model.Channel.ChannelTubeId == channelTubeId) {
                    targetChannel = model.Channel;
                    var schedules = model.VideoSchedules;

                    $.each(schedules, function (k, schedule) {
                        if (schedule.VideoTubeId == videoTubeId && schedule.PlaybackOrderNumber == playbackOrder) {
                            videoSchedule = schedule;
                        }
                    });
                }
            });
        }

        if (videoSchedule) {
            var popup = $("#popupVideo");

            if (popup) {

                var jsonDateStartDate = videoSchedule.PlaybackStartTime;
                var jsonDateEndDate = videoSchedule.PlaybackEndTime;

                var start = new Date(videoSchedule.PlaybackStartTimeString); //new Date(jsonDateStartDate.match(/\d+/)[0] * 1);
                var end = new Date(videoSchedule.PlaybackEndTimeString); //new Date(jsonDateEndDate.match(/\d+/)[0] * 1);

                var now = new Date();

                //if (start <= now && now < end) {
                //    LoadChannel(channelTubeId);
                //    return;
                //}

                var startTime = formatTime(start);
                var endTime = formatTime(end);

                var image = $("#videoImage");
                var record = $(".TLrecordLed");
                var title = $("#videoTitle");
                var description = $("#videoDescription");
                var duration = $("#videoDuration");
                var playtime = $("#videoStartTime");
                var watch = $("#videoWatchNow");

                if (videoSchedule.Thumbnail != null) {
                    image.attr('src', videoSchedule.Thumbnail);
                }

                record.removeAttr('onclick').attr('onclick', 'WatchItLater(' + videoSchedule.VideoTubeId + ')');

                if (videoSchedule.VideoTubeTitle != null && videoSchedule.VideoTubeTitle != '') {
                    title.text(videoSchedule.VideoTubeTitle);
                }
                else {
                    title.text('No title specified by provider.');
                }

                duration.text(GetDurationString(videoSchedule.Duration));

                playtime.text(startTime + '-' + endTime);

                var command = "LoadChannelAndCloseTvGuide(" + targetChannel.ChannelTubeId + ")";

                watch.removeAttr('onclick').attr('onclick', command);

                if (videoSchedule.Description != null && videoSchedule.Description != '') {
                    description.text(videoSchedule.Description);
                }
                else {
                    description.text('Description was not specified by provider.');
                }


                //popup.css('left', mX);
                //popup.css('top', 150); //mY - (isBrowseViewActive ? 670 : 650));
                popup.css('z-index', 300);
                OpenVideoInfo();

                //$(".OpenPopupVideoInfo").width("0%").hide();
                //var sidebarWidth = $('#tvguide').width();
                //var popup = $("#popupVideo");

                //if (state) {
                //    var sidebarWidthnew = sidebarWidth + popup.width();
                //    popup.width(sidebarWidthnew / 4 + 'px').show();

                //}
                //else {
                //    popup.show();
                //    $('#popupVideo').width("0%");
                //    $('#popupVideo').animate({ width: '+=' + sidebarWidth / 4 + 'px' }, 500);

                //    $('#tvguide').animate({ width: '-=' + popup.width() + 'px' }, 500);
                //    console.log(sidebarWidth)
                //    state = true;
                //}
                //$("#tvguide").width("100%").animate({ width: '-=22%' }, function () {
                //    popup.show()
                //})




            }
        }
    }
    else {
        $("#popupVideo").hide();
    }
}


function LoadChannelAndCloseTvGuide(channelId, element) {
    var elementChannelName = $(element).text()
    var redirectChannelUrl = elementChannelName.split(" ").join("");

    var matches = window.self.location.href.match(/^https?\:\/\/([^\/?#]+)(?:[\/?#]|$)/i);
    var domain = matches[0];
    var accountNumberRedirect = getParameterByName("accountNumber", document.URL);
    void 0
    embeddedChannelUrlRedirect = "/embedded/" + userName + "/" + redirectChannelUrl + "?embeded=true&accountNumber=" + accountNumberRedirect
    // console.log(accountNumberRedirect);
    window.self.location.href = embeddedChannelUrlRedirect;


}

function LoadChannel(channelId, element) {
    activeChannelId = channelId;

    if (activeChannelId) {
        GetScheduleData(activeChannelId, true);
        $('#popupVideo').hide();
        $('#popupChannel').hide();
    }

    if (activeTabChangedOnTvGuide) {
        activeTabChangedOnTvGuide = false;
    }

    displayTvGuide(displayedChannels);
    highlightActiveChannel();

    window.scrollTo(0, 0);
}

function loadNextChannel() {
    var ids = timeline.groupsData.getIds();
    var nextChannelValue;
    var nextChannelFound = false;
    var activeChannel = timeline.groupsData.get(activeChannelId);

    if (activeChannel != null) {
        nextChannelValue = activeChannel.value == (timeline.groupsData.length) ? 1 : activeChannel.value + 1;

        if (nextChannelValue > activeChannel.value) {
            timeline.groupsData.forEach(function (group, id) {
                if (group.value == nextChannelValue && !nextChannelFound) {
                    activeChannelId = group.id;
                    nextChannelFound = true;
                }
            });
        }
    }

    var isSameChannel = false;

    if (!nextChannelFound) {
        activeChannelId = activeTabChangedOnTvGuide ? ids[0] : null;
        if (activeChannelId == null) {
            activeChannelId = ids[ids.length - 1];
            isSameChannel = true;
        }
        activeTabChangedOnTvGuide = false;
    }

    if (!isSameChannel) {
        if (activeChannelId) {
            GetScheduleData(activeChannelId, true);
            highlightActiveChannel();
        }
        else {
            loadNextPage();
        }
    }
    else {
        //console.log(isActiveChannelsByTab);
        if (pageIndex < nextPageIndex) {
            loadNextPage();
        }
        else {
            if (isActiveChannelsByTab) {
                alertify.error('There are no more channels from this creator. Please use our TV guide to see other channels!');
            }
        }
    }

    $('#popupVideo').hide();
    $('#popupChannel').hide();

    window.scrollTo(0, 0);
}

function loadPrevChannel() {
    var ids = timeline.groupsData.getIds();
    var prevChannelValue;
    var prevChannelFound = false;
    var activeChannel = timeline.groupsData.get(activeChannelId);

    if (activeChannel != null) {
        prevChannelValue = activeChannel.value == 1 ? (timeline.groupsData.length) : activeChannel.value - 1;

        if (prevChannelValue < activeChannel.value) {
            timeline.groupsData.forEach(function (group, id) {
                if (group.value == prevChannelValue && !prevChannelFound) {
                    activeChannelId = group.id;
                    prevChannelFound = true;
                }
            });
        }
    }

    var isSameChannel = false;
    if (!prevChannelFound) {
        activeChannelId = activeTabChangedOnTvGuide ? ids[ids.length - 1] : null;
        if (activeChannelId == null) {
            activeChannelId = ids[0];
            isSameChannel = true;
        }
        activeTabChangedOnTvGuide = false;
    }

    if (!isSameChannel) {
        if (activeChannelId) {
            GetScheduleData(activeChannelId, true);
            highlightActiveChannel();
        }
        else {
            loadPrevPage();
        }
    }
    else {
        //console.log(isActiveChannelsByTab);
        if (pageIndex > prevPageIndex) {
            loadPrevPage();
        }
        else {
            if (isActiveChannelsByTab) {
                alertify.error('There are no more channels from this creator. Please use our TV guide to see other channels!');
            }
        }
    }

    $('#popupVideo').hide();
    $('#popupChannel').hide();

    window.scrollTo(0, 0);
}

function highlightActiveChannel() {
    if (activeChannelId > 0) {
        var element = $('#chGroup' + activeChannelId);

        if (element != null) {
            var channelElements = $("div.vis-label");
            $.each(channelElements, function (i, channel) {
                if (channel.style.backgroundColor != "") {
                    channel.style.backgroundColor = "";
                }
            });

            var label = getParentVisLabelElement(element);

            if (label != null && label != undefined) {
                label.style.backgroundColor = "#00C4FF";
            }
        }
    }
}

function getParentVisLabelElement(element) {
    var parent = element[0];

    if (parent != undefined && parent != null) {
        do {
            parent = parent.parentElement;
        }
        while (parent.className != 'vis-label draggable')
    }

    return parent;
}

function formatTime(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;

    return strTime;
}

function GetDurationString(seconds) {

    var hours = Math.floor(seconds / (60 * 60));
    seconds -= hours * (60 * 60);

    var mins = Math.floor(seconds / (60));
    seconds -= mins * (60);

    var duration = '';

    if (hours > 0) {
        duration += (hours + 'hr ');
    }

    if (duration != '' || mins > 0) {
        duration += (mins + 'min ');
    }

    duration += (seconds + 'sec');

    return duration;
}

function loadFirstPage() {
    if (pageIndex > 1) {
        MakeRequestByMode(1);
    }
}

function loadPrevPage() {
    if (pageIndex > 1) {
        MakeRequestByMode(prevPageIndex);
    }
}

function loadNextPage() {
    if (nextPageIndex > pageIndex) {
        MakeRequestByMode(nextPageIndex);
    }
}

function loadLastPage() {
    if (pageCount > pageIndex) {
        MakeRequestByMode(pageCount);
    }
}

function MakeRequestByMode(index) {
    //if (isTopChannelMode) {
    //    showTopChannels(index);
    //}
    //else if (isMyChannelMode) {
    showMyChannels(index);
    //}
    //else if (isFavoriteChannelMode) {
    //    showFavoriteChannels(index);
    //}
    //else if (isChannelByLanguageMode) {
    //    showChannelsByLanguage(index);
    //}
    //else if (isChannelByKeywordMode) {
    //    showChannelsByKeywords(index);
    //}
    //else if (isChannelByCategoryMode) {
    //    showChannelsByCategory(index);
    //}
    //else if (isCreatorChannelsMode) {
    //    showCreatorChannels(index);
    //}
    //else {
    //    showAllChannels(index);
    //}
}


function resetAllModes(activemode) {
    if (activemode != 'bylanguage') {
        $('#ddlLang option[value="0"]').prop('selected', true);
        activeLanguageId = 0;
        isChannelByLanguageMode = false;
    }

    if (activemode != 'bycategory') {
        $('#ddlCategory option[value="0"]').prop('selected', true);
        activeCategoryId = 0;
        isChannelByCategoryMode = false;
    }

    if (activemode != 'topchannels') {
        $('.TLtopChannels').removeClass('TLtopChannelsActive');
        isTopChannelMode = false;
    }

    if (activemode != 'allchannels') {
        $('.TVguideTitle').removeClass('TVguideTitleActive');
        isAllChannelMode = false;
    }

    if (activemode != 'creatorchannels') {
        $('.TLchannlesBy').removeClass('TLchannlesByActive');
        //activeChannelCreator = null;
        isCreatorChannelsMode = false;
    }

    if (activemode != 'mychannels') {
        $('.TLmyChannels').removeClass('TLmyChannelsActive');
        isMyChannelMode = false;
    }

    if (activemode != 'favchannels') {
        $('.TLfavChannels').removeClass('TLfavChannelsActive');
        isFavoriteChannelMode = false;
    }

    if (activemode != 'bykeywords') {
        isChannelByKeywordMode = false;
        $('#txtKeywords').val('');
    }

    closeVideoPopup();
    closeChannelPopup();
}

function openTvGuide() {
    var isTvGuideVisible = $(".TVGuideBottom").is(":visible");

    if (!isTvGuideVisible) {
        $(".TVGuideBottom").show();
        $(".closeGuide").show();
        $("#remoteControl").show();
    }
    else {
        closeTvGuide();
    }
}

function closeTvGuide() {
    var isTvGuideVisible = $(".TVGuideBottom").is(":visible");

    if (isTvGuideVisible) {
        $(".TVGuideBottom").hide();
        $(".closeGuide").hide();
        $("#remoteControl").hide();
    }
}

function LoadLIveVideos() {
    closeTvGuide();
    var liveHeaderHtml = "";

    liveHeaderHtml += '<div class="menuChannelLive">';
    liveHeaderHtml += '<span class="submenuChannelLive submenuChannelLiveTitle">Live Event</span>';
    //liveHeaderHtml+='<span class="submenuChannelLive">Date</span>';
    liveHeaderHtml += '<span class="submenuChannelLive">Start Time</span>';
    liveHeaderHtml += '<span class="submenuChannelLive">End Time</span>';
    liveHeaderHtml += '</div>';
    $(".iconDescription").hide();
    //  $(liveHeaderHtml).insertAfter(".iconDescription");

    $(".sideBarOptions .schedule").removeClass("scheduleactive");
    $(".sideBarOptions .favoriteChannels").removeClass("favoriteChannelsActive");
    $(".sideBarOptions .toprated").removeClass("topRatedActive");
    $(".sideBarOptions .mychannels").removeClass("mychannelsactive");
    $(".sideBarOptions .myChat").removeClass("myChatActive");
    $(".sideBarOptions .liveVideos").addClass("liveVideosActive");







    var clientTime = new Date();
    var clientTimeInUTC = moment.utc(clientTime).format('MM/DD/YYYY HH:mm');
    var params = '{"channelTubeId":' + channelTubeId + ',"targetDate":' + "'" + clientTimeInUTC + "'" + '}';

    $.ajax({
        type: 'POST',
        url: webMethodGetLiveVideos,
        dataType: 'json',
        data: params,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {

            var data = response.hasOwnProperty("d") ? response.d : response;
            void 0

            if (data != null) {
                void 0

                var SchedulesHtml = Controls.BuildLiveVideoScheduleControlForChannel(data);
                if (executeSchedulePolling != 0) {
                    $(".sideContentHolder").html("");
                    $(".embedLive").show();
                    $(".sideContentHolder").html("<a class='closeLivePanel' onclick='CloseLivePanel()'></a>" + liveHeaderHtml + SchedulesHtml);
                    // Since we are regetting the channel schedule, the first video in the schedule will
                    // always be playing first.
                    //UpdateUIForNewVideo(0);
                }

            }
            else {
                void 0
                $(".embedLive").hide();
                $(".sideContentHolder").html("").html("<span class='msg'>there is no live videos scheduled for this channel</>");
            }
        },
        complete: function (res) {
            //  $(".nano").nanoScroller({ alwaysVisible: false });
        }

    });
};
function CloseLivePanel() {

    $(".sideContentHolder").html("");

}
function closeVideoLiveInfoBox() {
    $(".liveEventInfoPopUp").hide();

    $(".liveEventInfoPopUpDescription").removeClass("less").addClass("more");
    $(".showMoreLessLive").text("").text("more...").removeAttr("onclick").attr("onclick", "ShowMoreLiveInfo(this)");

}
function ShowMoreLiveInfo(btn) {
    $(".liveEventInfoPopUpDescription").removeClass("more").addClass("less");
    $("#" + btn.id).text("").text("less").removeAttr("onclick").attr("onclick", "ShowLessLiveInfo(this)");
}
function ShowLessLiveInfo(btn) {
    $(".liveEventInfoPopUpDescription").removeClass("less").addClass("more");
    $("#" + btn.id).text("").text("more...").removeAttr("onclick").attr("onclick", "ShowMoreLiveInfo(this)");
}
function ShowVideoLiveInfoPopup(btn) {
    var id = btn.id;
    var arrProviderVideoId = id.split('_');
    var providerVideoId = arrProviderVideoId[1];

    $(".liveEventInfoPopUp").hide();
    $("#infoLive_" + providerVideoId + ".liveEventInfoPopUp").show();
    $(".nano").nanoScroller({ alwaysVisible: false });
}
function PlayLiveVideo(btn) {

    var videoProviderId = btn.d;
    var btnId = btn.id;
    var idArr = btnId.split(/_(.+)/);
    var videoProviderId = idArr[1];
    void 0
    var srcValue = "https://youtube.com/watch?v=" + videoProviderId;
    var typeValue = "video/youtube";
    var videoItem = { sources: [{ type: typeValue, src: srcValue }] };
    var newPlaylist = [];
    newPlaylist.push(videoItem);
    $(".btnPlayLiveVideo").removeClass("active");
    $("#btn_" + videoProviderId).addClass("active");
    $(".titleLive").removeClass('active')
    $("#videoLiveId_" + videoProviderId).addClass('active');
    void 0
    var fplayer = flowplayer("#complete");
    {
        if (fplayer) {
            // load clip object

            flowplayer("#complete").load({
                live: true,
                sources: [
                    { type: typeValue, src: srcValue },

                ]
            });
            void 0;


            flowplayer(function (api) {

                api.on("load ready", function (e, api, video) {
                    void 0
                });
                void 0;

            });
        }
        else {
            var flowPlayerHolderContentHtml = "<div class='playerbox' id='strimmVideoBox'>" +
                "    <div id='complete'  data-embed='false' data-volume='1' >" +
                "</div>";
            $("#playerImage").hide();

            $("#PlayerHolder").html(flowPlayerHolderContentHtml).show();
            flowplayer.conf.youtube = {
                defaultQuality: "low",
                live: true

            };



            !function () {

                var player = flowplayer("#complete", {
                    playlist: [
                        {
                            sources: [
                                { type: "video/youtube", src: srcValue }
                            ]
                        }

                    ]
                }).one("ready", function (e, api, data) {
                    $(".fp-player").next().remove();
                    api.play();

                });
            }();
        }
    }




}
