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

var webMethodTvGuideAllChannels = "/WebServices/ChannelWebService.asmx/GetTvGuideAllChannels";
var webMethodTvGuideTopChannels = "/WebServices/ChannelWebService.asmx/GetTvGuideTopChannels";
var webMethodTvGuideFavoriteChannelsForUser = "/WebServices/ChannelWebService.asmx/GetTvGuideFavoriteChannelsForUser";
var webMethodTvGuideUserChannelsById = "/WebServices/ChannelWebService.asmx/GetTvGuideUserChannelsById";
var webMethodTvGuideChannelsByLanguage = "/WebServices/ChannelWebService.asmx/GetTvGuideChannelsByLanguage";
var webMethodTvGuideChannelsByKeywords = "/WebServices/ChannelWebService.asmx/GetTvGuideChannelsByKeywords";
var webMethodTvGuideChannelsByCategory = "/WebServices/ChannelWebService.asmx/GetTvGuideChannelsByCategory";
var webMethodTvGuideUserChannelsByPublicUrl = "/WebServices/ChannelWebService.asmx/GetTvGuideUserChannelsByPublicUrl";

var webMethodGetChannelCategories = "/WebServices/ChannelWebService.asmx/GetChannelCategories";
var webMethodGetChannelLanguages = "/WebServices/ChannelWebService.asmx/GetChannelLanguages";

var webMethodValidateChannelPassword = "/WebServices/ChannelWebService.asmx/ValidateChannelPassword";

var pageIndex = 1;
var prevPageIndex = 1;
var nextPageIndex = 1;
var pageCount = 0;

var isAllChannelMode = false;
var isTopChannelMode = false;
var isMyChannelMode = false;
var isFavoriteChannelMode = false;
var isChannelByLanguageMode = false;
var isChannelByKeywordMode = false;
var isChannelByCategoryMode = false;
var isCreatorChannelsMode = false;
var isBrowseViewActive = false;
var playerHolder = null;

var activeCategoryId = 0;
var activeCategoryName;
var activeLanguageId = 0;
var activeChannels = [];
var languages = [];
var categories = [];
var activeChannelId;
var activeChannelCreator;
var activeChannel;
var loadFirstChannel = false;
var isActiveChannelsByTab = false;

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
var currentlyPlayingVideoStartTime;
var playlistStr;
var isMatch = false;
var isMobile = false;
var recordedVideos = [];
var displayedChannels = [];
var activeTabChangedOnTvGuide = false;
var isMobileLandscape = false;
var isMobilePortrait = false;
var tag = document.createElement('script');
var isChannelProtected = false;
var isPasswordValid = false;

tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

var mX, mY, isOn = false;
$(document).mousemove(function (e) {
    mX = e.pageX;
    mY = e.pageY;
}).mouseover();

//$(document).ajaxStart(function () {
//    $("#loadingDiv").show();
//});
//$(document).ajaxStop(function () {
//    $("#loadingDiv").hide();
//});

$(document).ready(function () {
    //var userInfoObj = getCookie('init');
    //setCookie('init', JSON.stringify(userInfoObj), 30);
    //if isChannelPasswordProtected and no cookie session for channel id and password

    //if (isChannelPasswordProtected&&isPasswordValid==false)
    //{
    //    executeSchedulePolling = 0;
    //    ShowChannelPasswordPopup();
       
    //}
    //else
    //{
        var container = document.getElementById('mytimeline');

        var opts = {
            lines: 12             // The number of lines to draw
            , length: 7             // The length of each line
            , width: 5              // The line thickness
            , radius: 10            // The radius of the inner circle
            , scale: 1.0            // Scales overall size of the spinner
            , corners: 1            // Roundness (0..1)
            , color: 'white'         // #rgb or #rrggbb
            , opacity: 1          // Opacity of the lines
            , rotate: 0             // Rotation offset
            , direction: 1          // 1: clockwise, -1: counterclockwise
            , speed: 1              // Rounds per second
            , trail: 100            // Afterglow percentage
            , fps: 20               // Frames per second when using setTimeout()
            , zIndex: 2e9           // Use a high z-index by default
            , className: 'spinner'  // CSS class to assign to the element
            , top: '450px'            // center vertically
            , left: '50%'           // center horizontally
            , shadow: false         // Whether to render a shadow
            , hwaccel: false        // Whether to use hardware acceleration (might be buggy)
            , position: 'absolute'  // Element positioning
        }
        var target = document.getElementById('loadingDiv')
        var spinner = new Spinner(opts).spin(target)

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

        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('=');
        if (hashes != undefined && hashes.length == 2) {
            activeCategoryName = hashes[1].replace(/%20/g, " ");
        }

        populateLanguages();
        populateChannelCategories();

        setTimeout(function () {
            $(".nano").nanoScroller({ alwaysVisible: false });
            HideInfoBar();
            HideSchedule();
        }, 6000);

        if (categoryName != undefined) {
            $('#ddlCategory option[value="0"]').prop('selected', true);

            showChannelsByCategory();
        }

        if (isMobileDevice()) {
            if (screen.width <= 736 && screen.width > screen.height) {
                isMobileLandscape = true;
                setTimeout(function () {
                    HideInfoBarMobile();
                }, 3000);
            }

            $(window).on("orientationchange", function (event) {
                //isMobileLandscape = event != null && event != undefined && event.orientation == 'landscape';
                //isMobilePortrait = !isMobileLandscape;

                if (screen.width <= 567 && screen.width < screen.height) {
                    isMobileLandscape = false;
                    ShowInfoBarMobile();
                }
                else if (screen.width <= 736 && screen.width > screen.height) {
                    isMobileLandscape = true;
                    setTimeout(function () {
                        HideInfoBarMobile(event);
                    }, 3000);
                }
            });
        }
    //}
  

});

function isMobileDevice() {
    return (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino|android|ipad|playbook|silk/i.test(navigator.userAgent || navigator.vendor || window.opera) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test((navigator.userAgent || navigator.vendor || window.opera).substr(0, 4)));
};

function HideInfoBarMobile(event) {
    //isMobileLandscape = event != null && event != undefined && event.orientation == 'landscape';
    //isMobilePortrait = !isMobileLandscape;

    if (isMobileLandscape) {
        $(".ChannelInfoCL").hide('slide', { direction: 'down' }, 1000);
        $(".ChannelInfoCLCloseMobile").show();
    }
}

function ShowInfoBarMobile() {
    $(".ChannelInfoCLCloseMobile").hide('slide', { direction: 'up' }, 1000);
    $(".ChannelInfoCL").show();
}

$('.ytp-button-fullscreen-enter').on("click", (function () {
    //console.log("fullscfreen youtube up");
    })
);

function openLoginDialog() {
    $("#see_id").html("Thank you for watching. Please Login or Sign Up to continue. It's free.")
    $('.signUpNow').show();
    $('#loginBox').lightbox_me({
        centered: true,
        onLoad: function () {
            $('#spanMessage').empty();
            $('#txtUserName').val('');
            $('#txtPassword').val('');
            $('#loginBox').find('input:first').focus();
            $("#loginBox #actions #ancLogin").removeAttr("onclick").attr("onclick", 'Login("sameLocation")');
        },
        onClose: function () {
            RemoveOverlay();
            location.href = "/";
        }
    });
};

// 3. This function creates an <iframe> (and YouTube player)
//    after the API code downloads.
function onYouTubeIframeAPIReady() {
    ResetUIForProperPlayer(true, false);

    var playerElement = isBrowseViewActive ? 'playerSmall' : 'player';
    player = new YT.Player(playerElement, {
        //height: '390',
        //width: '640',
        playerVars: {
            autohide: 1,
            showinfo: 0,
            autoplay: 1,
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
        var playerElement = isBrowseViewActive ? 'playerSmall' : 'player';
        player = new YT.Player(playerElement, {
            //height: '546',
            //width: '728',
            playerVars: {
                autohide: 1,
                controls: 0,
                showinfo: 0,
                autoplay: 1,
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
        if (startTime == undefined) {
        startTime = parseInt(parseFloat(currentVideo.PlayerPlaybackStartTimeInSec).toFixed());
}
    }
    else {
        startTime = 0;
    }

    function onYouTubeIframeAPIReady() {
        var playerElement = isBrowseViewActive ? 'playerSmall' : 'player';
        player = new YT.Player(playerElement, {
            //height: '546',
            //width: '728',
            playerVars: {
                controls: 1,
                showinfo: 0,
                autoplay: 1,
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

            $("#ChannelPageInfoHolder .videoTitle h1").text("").text(currentVideo.VideoTubeTitle)
            }
    };

    function onYouTubeIframeAPIReadyIE() {
        if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
            $("iframe.playerbox").attr("allowfullscreen");
            player = new YT.Player('player', {
                //height: '546',
                //width: '728',
                playerVars: {
                    controls: 1,
                    showinfo: 0,
                    autoplay: 1,
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

                playBackOrderNum = currentlyPlayingVideo.PlaybackOrderNumber;

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
    var playerElement = isBrowseViewActive ? 'dMotionPlayerSmall' : 'dMotionPlayer';
    var PlayerHolderHtml = "<div id='" + playerElement + "'>" +
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
            iframe.attr("src", "//player.vimeo.com/video/" + currentVideo.ProviderVideoId + "?api=1&player_id=myVideo;autoplay=1");
        
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
        videoPlaylistIndex++;

        if (videoPlaylistIndex != 0) {
            startTime = 0;
        }

        var nextVideo = playList[videoPlaylistIndex];

        if (nextVideo) {
            if (playList[videoPlaylistIndex].VideoProviderName == "vimeo") {
                iframe.removeAttr("src").attr("src", "//player.vimeo.com/video/" + playList[videoPlaylistIndex].ProviderVideoId + "?api=1&player_id=myVideo;autoplay=1");
            }
            else if (playList[videoPlaylistIndex].VideoProviderName == "youtube") {
                if (videoPlaylistIndex != 0) {
            startTime = 0;
        }
                iframe.removeAttr("src").css("display", "none");
                InitYoutubePlayer(playList, videoPlaylistIndex, startTime);
        }
        else {
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
}

function InitDMotionPlayer(playList, playBackOrderNumber, startTime) {
   
    ResetUIForProperPlayer(true, true);
    //$("#dMotionPlayer").css("display", "block");

    var playerElement = isBrowseViewActive ? 'dMotionPlayerSmall' : 'dMotionPlayer';
    var dplayer = DM.player(playerElement, { video: playList[playBackOrderNumber].ProviderVideoId, width: "100%", height: "100%", params: { autoplay: 1, html: 1, allowfullscreen: 1, start: startTime } });

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

        ProcessNextVideo(playBackOrderNumber, startTime);
    });
}
window.onbeforeunload = function () {

    void 0;

};
function DmotionPlayerPlay(dplayer, playList, playBackOrderNumber, startTime) {
    $("#player.playerbox").hide();

    dplayer.addEventListener("apiready", function (e) {

        $("#ChannelPageInfoHolder .videoTitle h1").text("").text(playList[playBackOrderNumber].VideoTubeTitle);

        UpdateUIForNewVideo(playBackOrderNumber);

        e.target.play();
        });
}

function SchedulePolling() {
    $("#tvGuideControl").hide();
    //$("#tvGuideControl").slideDown(1000);
    GetScheduleData();

    //MST:Disabling for now per conversation with Dima
    //if (userId=="" || userId == undefined || userId == 0) {
    //    setTimeout(function () {
    //        openLoginDialog();
    //    }, 30000);
    //}
};

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
        autoplay: true
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

function GetScheduleData(channelId, dontRegetChannels) {
    $('#loadingDiv').show();
    
    var timeOfRequest = getClientTime();

    var shouldReplaceUrl = false;
    
    if (channelId != null && channelId != undefined) {
        channelTubeId = channelId;
        shouldReplaceUrl = true;
    }

    if (isMobileDevice()) {
        isBrowseViewActive = false;
        switchToFullVideo();
    }

    $.ajax({
        type: 'POST',
        url: webMethodGetChannelPreviewDataJson,
        dataType: 'json',
        data: '{"clientTime":' + "'" + timeOfRequest + "'" + ',"channelId":' + channelTubeId + ',"userId":' + userIdCheked + '}',
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var displayNoScheduleInfo = false;
            var displayRestrictedChannelInfo = false;

            if (response.d != null) {
                $("#playerbox").toggle();

                AddChannelViewCount();

                isMatch = true;

                var data = response.d;
                activeChannel = data.Channel;
                var schedule = data.Schedule;
                var user = data.User;

                if (activeChannel != null) {
                    
                    UpdateChannelInfoOnUI(activeChannel, shouldReplaceUrl);
                    InitSocial(activeChannel);
                    UpdateSocialMetaTags(activeChannel);
                    UpdateCreatorInfoOnUI(user);

                    if (dontRegetChannels == undefined || (dontRegetChannels != undefined && !dontRegetChannels)) {
                        showCreatorChannels();
                    }
                }

                playlist = data.Playlist;

                if (playlist != null) {
                    for (var i = 0; i < playlist.length; i++) {
                        playlistArr[i] = playlist[i].ProviderVideoId;
                    }

                    currChannelSchedule = data.ActiveSchedule.ChannelScheduleId;
                    var SchedulesHtml = Controls.BuildVideoScheduleControlForChannel(data.Playlist);
                    var firstVideo = playlist[0];

                    if (firstVideo) {
                        startTime = parseInt(parseFloat(firstVideo.PlayerPlaybackStartTimeInSec).toFixed());

                        UpdateCurrentlyPlayingVideoInfoOnUI(firstVideo);
                    }
                    else {
                        startTime = 0;
                    }

                    if (HasMultipleProviders(data.Playlist)) {
                        ProcessNextVideo(0, startTime);
                    }
                    else {
                            currentlyPlayingVideo = firstVideo;
                            currentlyPlayingVideoStartTime = new Date();
                            currentlyPlayingVideoStartTime.setTime(currentlyPlayingVideoStartTime.getTime() - (startTime * 1000));
                            playBackOrderNum = 0;

                        if (firstVideo.VideoProviderName == "vimeo") {
                            InitVimeoPlayer(playlist, 0, startTime);
                        }
                        else if (firstVideo.VideoProviderName == "youtube") {
                            if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                                onYouTubeIframeAPIReadyIE();
                            }
                            else {
                                onYouTubeIframeAPIReady();
                            }
                        }
                        else if (firstVideo.VideoProviderName == "dailymotion") {
                            InitDMotionPlayer(playlist, 0, startTime);
                        }
                        else {
                            InitFlowPlayer(playlist, -1, startTime);
                        }
                    }

                            //MAX: need to show currently playing channel on top
                    $("#sideBarChannel .sideContentHolder").html("").html(SchedulesHtml);

                        // Max: this code prevented player from being displayed on switching from lineup mode to full video mode
                        // after browse channel link was clicked. 
                        //if ($("#PlayerHolder") != undefined && $("#PlayerHolder").is(":visible") == false) {
                        //    $("#PlayerHolder").toggle();
                        //    $("#playerImage").toggle();
                        //}
                }
                else {
                    if (response.d.IsRestricted) {
                        displayRestrictedChannelInfo = true;
                    }
                    else {
                        displayNoScheduleInfo = true;
                    }
                }
            }
            else {
                if (response.d.IsRestricted) {
                    displayRestrictedChannelInfo = true;
                }
                else {
                    displayNoScheduleInfo = true;
                }
            }

            if (displayNoScheduleInfo) {
                if ($("#PlayerHolder").is(":visible") == true) {
                    $("#PlayerHolder").toggle();
                    $("#playerImage").removeAttr("src").attr("src", "/images/sorryChannelIL1200px.jpg");
                    $("#playerImage").toggle();
                }

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
            else if (displayRestrictedChannelInfo) {
                if ($("#PlayerHolder").is(":visible") == true) {
                    $("#PlayerHolder").toggle();
                    $("#playerImage").removeAttr("src").attr("src", "/images/privateChannel.jpg");
                    $("#playerImage").toggle();
                }

                $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>" + response.d.Message + "</>");
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
            $('#loadingDiv').hide();
        }
    });
};

function InitSocial(channel) {
    var url = window.location.href;
    var description = channel.Description;
    var title = channel.Name;
    var media = channel.PictureUrl;

    if (channel.PictureUrl == null || channel.PictureUrl == undefined || channel.PictureUrl == '') {
        media = 'https://' + location.host + '/images/comingSoon.jpg';
    }

    var social = '<a href="#" data-type="twitter" data-url="' + url + '" data-description="' + description + '" data-via="strimm" class="prettySocial fa fa-twitter" data-title="' + title + '" data-media="' + media + '"></a>' +
             '<a href="#" data-type="facebook" data-url="' + url + '" data-description="' + description + '" data-via="strimm" class="prettySocial fa fa-facebook" data-title="' + title + '" data-media="' + media + '"></a>' +
             '<a href="#" data-type="googleplus" data-url="' + url + '" data-description="' + description + '" data-via="strimm" class="prettySocial fa fa-google-plus" data-title="' + title + '" data-media="' + media + '"></a>' +
             '<a href="#" data-type="pinterest" data-url="' + url + '" data-description="' + description + '" data-via="strimm" class="prettySocial fa fa-pinterest" data-title="' + title + '" data-media="' + media + '"></a>';

    $("#newSocilaHolder").html('').html(social);
    $('.prettySocial').prettySocial();
}

function UpdateSocialMetaTags(channel) {
    if (channel != null) {
        var channelPictureUrl;
        if (channel.PictureUrl != null && channel.PictureUrl != undefined && channel.PictureUrl != '') {
            channelPictureUrl = channel.PictureUrl;
        }
        else
        {
            channelPictureUrl = '/images/comingSoon.jpg';
        }
        $('title').text('Strimm TV |' + channel.Name);
        $('meta[name=description]').text(channel.Description);

        $('meta[property="og:url"]').attr('content', location.href);
        $('meta[property="og:type"]').attr('content', 'video.tv_show');
        $('meta[property="og:title"]').attr('content', channel.Name);
        $('meta[property="og:description"]').attr('content', channel.Description);

        if (channel.PictureUrl != null && channel.PictureUrl != undefined && channel.PictureUrl != '') {
            $('meta[property="og:image"]').attr('content', channel.PictureUrl);
        }
        else {
            $('meta[property="og:image"]').attr('content', '/images/comingSoon.jpg');
        }

        $('meta[property="og:site_name"]').attr('content', 'www.strimm.com');
        $('meta[property="fb:app_id"]').attr('content', fbAppId);

        FB.XFBML.parse();

        $('meta[name="twitter:card"]').attr('content', 'summary_large_image');
        $('meta[name="twitter:site"]').attr('content', '@StrimmTV');
        $('meta[name="twitter:title"]').attr('content', channel.Name);
        $('meta[name="twitter:description"]').attr('content', channel.Description);
        $('meta[name="twitter:creator"]').attr('content', channel.ChannelOwnerUserName);

        if (channel.PictureUrl != null && channel.PictureUrl != undefined && channel.PictureUrl != '') {
            $('meta[name="twitter:image"]').attr('content', channel.PictureUrl);
        }
        else {
            $('meta[name="twitter:image"]').attr('content', '/images/comingSoon.jpg');
        }

        $('meta[itemprop="name"]').attr('content', channel.Name);
        
        if (channel.PictureUrl != null && channel.PictureUrl != undefined && channel.PictureUrl != '') {
            $('meta[itemprop="image"]').attr('content', channel.PictureUrl);
        }
        else {
            $('meta[itemprop="image"]').attr('content', '/images/comingSoon.jpg');
        }

        $('meta[itemprop="description"]').attr('content', channel.Description);

        $('meta[name="p:domain_verify"]').attr('content', '1283df95a7aa0e31f24f5cc4402be76a');

        FB.api(
            "/video.tv_show",
            "POST",
            {
                "object": "{\"fb:app_id\":\"" + fbAppId +
                    "\",\"og:type\":\"video.tv_show\",\"og:url\":\"" + location.href.replace(new RegExp("/", "gi"), "\\\\\\/") +
                    "\",\"og:title\":\"" + channel.Name +
                    "\",\"og:image\":\"" + channelPictureUrl.replace(new RegExp("/", "gi"), "\\\\\\/")
            },
            function (response) {
                if (response && !response.error) {
                    /* handle the result */
                }
            }
        );
    }
}

function UpdateChannelInfoOnUI(channel, shouldReplaceUrl) {
    if (channel != null && channel != undefined) {
        void 0
        activeChannelCreator = channel.ChannelOwnerUrl;
        activeChannelId = channel.ChannelTubeId;

        $('#PlayerHolder').show();
        $('#playerImage').hide();

        $("#ancUserName").text(channel.ChannelOwnerUserName);
        $(".creatorMobileName").text("").text("by " + channel.ChannelOwnerUserName);
        $(".TLchannlesBy").text("Channels by " + (channel.ChannelOwnerUserName.length > 6 ? (channel.ChannelOwnerUserName.substring(0, 6) + "...") : channel.ChannelOwnerUserName));
        $(".TLchannlesBy").prop("title", "Channels by " + channel.ChannelOwnerUserName);

        if (shouldReplaceUrl) {
            var matches = window.location.href.match(/^https?\:\/\/([^\/?#]+)(?:[\/?#]|$)/i);
            var domain = matches[0];

            history.pushState({}, channel.Name + '| Strimm TV', domain + channel.ChannelOwnerUrl + '/' + channel.Url);
        }

        $("#ancUserName").prop("href", "/" + channel.ChannelOwnerUrl);

        $("#ancName").text(channel.Name);
        $("#lblCategory").text(channel.CategoryName);

        if (channel.PictureUrl != null && channel.PictureUrl != undefined && channel.PictureUrl != '') {
            $("#imgChannelAvatarChannel").prop("src", channel.PictureUrl);
        }
        else {
            $("#imgChannelAvatarChannel").prop("src", '/images/comingSoon.jpg');
        }

        $("#spnAVGRating").text(channel.Rating.toFixed(1));
        $("#rateit1").rateit();
        $("#rateit1").rateit('value', channel.UserChannelRating.toFixed(1));

        if (channel.Description != null && channel.Description != '') {
            $("#chDescription").text(channel.Description);
        }
        else {
            $("#chDescription").text('Thank you for visiting my channel. Watch it and enjoy!');
        }


        $(".channelTitlePlayer").text(channel.Name);
        $("#channelCreatorName").text(channel.ChannelOwnerUserName);
        $("#ancUserName").text(channel.ChannelOwnerUserName);
        $(".spnVisitMyPage").prop("href", "/" + channel.ChannelOwnerUrl);

        $("#lineupChannelName").text(channel.Name);
        
        if (channel.IsSubscribed) {
            if (userIdCheked > 0) {
                $("#lnkAddToFave").removeAttr('onclick').attr('onclick', 'RemoveFromFavorite()');
                $("#lnkAddToFave").prop('title', 'Remove from Favorites');
            }
            else {
                $("#lnkAddToFave").removeAttr('onclick').attr('onclick', "loginModal('sameLocation')");
                $("#lnkAddToFave").prop('title', 'Remove from Favorites');
            }
            $("#lnkAddToFave").addClass('addtofavoriteActive');
        }
        else {
            if (userIdCheked > 0) {
                $("#lnkAddToFave").removeAttr('onclick').attr('onclick', 'AddToFavorite()');
                $("#lnkAddToFave").prop('title', 'Add to Favorites');
            }
            else {
                $("#lnkAddToFave").removeAttr('onclick').attr('onclick', "loginModal('sameLocation')");
                $("#lnkAddToFave").prop('title', 'Add to Favorites');
            }
            $("#lnkAddToFave").removeClass('addtofavoriteActive');
        }

        if (channel.IsLike) {
            if (userIdCheked > 0) {
                $("#ancLike").removeAttr('onclick').attr('onclick', 'UnLike()');
                $("#ancLike").prop('title', 'You like this channel. Click again, if you want to remove it.');
            }
            else {
                $("#ancLike").removeAttr('onclick').attr('onclick', "loginModal('sameLocation')");
                $("#ancLike").prop('title', 'You like this channel. Click again, if you want to remove it.');
            }
            $("#ancLike").addClass('likeActive');
        }
        else {
            if (userIdCheked > 0) {
                $("#ancLike").removeAttr('onclick').attr('onclick', 'Like()');
                $("#ancLike").prop('title', 'Like this channel');
            }
            else {
                $("#ancLike").removeAttr('onclick').attr('onclick', "loginModal('sameLocation')");
                $("#ancLike").prop('title', 'Like this channel');
            }
            $("#ancLike").removeClass('likeActive');
        }
    }
}

//videoCL
function UpdateCurrentlyPlayingVideoInfoOnUI(video) {
    if (video != null && video != undefined) {
        $(".TLrecordLed").removeAttr('onclick').attr('onclick', 'WatchItLater(' + video.VideoTubeId + ')');

        $('.videoTitlePlayer').text(video.VideoTubeTitle);
        $('.videoTitlePlayer').prop("title", video.VideoTubeTitle);
        $('.videoDurationPlayer').text(video.PlaytimeMessage);

        $("#lineupVideoTitle").text(video.VideoTubeTitle);
        $("#lineupVideoDescription").text(video.Description);
        $("#lineupVideoDescription").prop('title', video.Description);

        //$("#lineupVideoDuration").text(video.Description);
        $("#lineupVideoPlaytime").text(video.PlaytimeMessage);
        $(".recHolder").prop("onclick", "WatchItLater(" + video.VideoTubeId + ")");
    }
}

function expandSearchControl() {
    var screenWidth = $(window).width();
    if (screenWidth <= 1220) {
        $("#categorySearch").hide();
        $("#languageSearch").hide();
        $(".keywordHolderIcon").hide();
        $(".keywordHolder").show();
    }
}

function closeSearch() {
    $("#categorySearch").show();
    $("#languageSearch").show();
    $(".keywordHolderIcon").show();
    $(".keywordHolder").hide();
    showAllChannels();
}

function UpdateCreatorInfoOnUI(user) {
    if (user != null && user != undefined) {
        if (user.UserStory != null && user.UserStory != "") {
            $("#chCreatorBio").text(user.UserStory);
        }
        else {
            $("#chCreatorBio").text('Thank you for visiting my channel. More about me..... coming soon!');
        }

        if (user.ProfileImageUrl != null) {
            $(".channelCreatorImg").prop("src", user.ProfileImageUrl);
            $(".TLcreatorImg").css('background-image', "url('" + user.ProfileImageUrl + "')");
            $(".creatorMobileImg").css('background-image', "url('" + user.ProfileImageUrl + "')");
        }
        else {
            $(".channelCreatorImg").prop("src", (user.Gender == 'Male') ? defaultMaleAvatar : defaultFemaleAvatar);
            $(".TLcreatorImg").css('background-image', "url('" + (user.Gender == 'Male') ? defaultMaleAvatar : defaultFemaleAvatar + "')");
            $(".creatorMobileImg").css('background-image', "url('" + (user.Gender == 'Male') ? defaultMaleAvatar : defaultFemaleAvatar + "')");
        }
    }
}

//function toggleVideo(state) {
//    // if state == 'hide', hide. Else: show video
//    var div = document.getElementById("player");
//    var iframe = $("iframe.playerbox").contentWindow;
//    div.style.display = state == 'hide' ? 'none' : '';
//    func = state == 'hide' ? 'pauseVideo' : 'playVideo';
//    iframe.postMessage('{"event":"command","func":"' + func + '","args":""}', '*');
//}

function switchToFullVideo() {
    var startTime = (new Date() - currentlyPlayingVideoStartTime) / 1000;
   
    //toggleVideo();
    playerHolder.html("");

    if (isBrowseViewActive) {
        isBrowseViewActive = false;

        $(".channelInfoPlayerTOP").show("fade", { direction: "in" }, 1000);
        $(".browseChannelsView").hide("fade", { direction: "out" }, 1000);
        //$("#PlayerHolder").show("fade", { direction: "in" }, 1000);
        $("#remoteControl").removeClass('TLnavigationHolderChanLine').addClass('TLnavigationHolder');
        $("#switchMode").removeClass('switchModeList').addClass('switchMode');
        $("#mytimeline").removeClass('mytimelineList').addClass('mytimeline');
        $("#tvGuideControl").removeClass('TVGuideBottomList').addClass('TVGuideBottom');

        $(".mainHolder").show();
        $(".TLinfoTopPannelFull").show();
        //$("#tvGuideControl").slideDown(1000);
        $("#tvGuideControl").hide();
        $('body').css('overflow-y', 'hidden');
    }
    else {
        isBrowseViewActive = true;

        $(".channelInfoPlayerTOP").hide("fade", { direction: "out" }, 1000);
        $(".browseChannelsView").show(); //("fade", { direction: "in" }, 1000);
        //$("#PlayerHolder").hide("fade", { direction: "out" }, 1000);
        $("#remoteControl").removeClass('TLnavigationHolder').addClass('TLnavigationHolderChanLine');
        $("#switchMode").removeClass('switchMode ').addClass('switchModeList');
        $("#tvGuideControl").removeClass('TVGuideBottom').addClass('TVGuideBottomList');

        $(".mainHolder").hide();
        $(".TLinfoTopPannelFull").hide();
        $("#tvGuideControl").show();
        //$("#tvGuideControl").slideUp(1000);
        $('body').css('overflow-y', 'auto');
    }

    $(".closeGuide").hide();

    if (playBackOrderNum != undefined) {
        ProcessNextVideo(playBackOrderNum, startTime);
    }
}

function openTvGuide() {
    var isTvGuideVisible = $(".TVGuideBottom").is(":visible");

    if (!isBrowseViewActive && !isTvGuideVisible) {
        $(".TVGuideBottom").show();
        $(".closeGuide").show();
    }
    else {
        closeTvGuide();
    }
}

function closeTvGuide() {
    if (!isBrowseViewActive) {
        $(".TVGuideBottom").hide();
        $(".closeGuide").hide();
    }
}

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

    var dailyMotionPlayerHolderHtmlSmall = "<div id='dMotionPlayerSmall'>" +
                  "</div>" +
                  "<div class='playerbox' id='playerSmall'>" +
                  "<iframe class='playerbox video-tracking' style='display:none;' id='myVideo'  width='40%' height='120px' data-frameborder='0' data-webkitallowfullscreen='true' data-mozallowfullscreen='true' data-allowfullscreen='true'>" +
                  "</iframe>" +
                  "</div>";

    var flowPlayerHolderContentHtml = "<div class='playerbox' id='strimmVideoBox'>" +
                                  "    <div id='complete' class='flowplayer functional' data-ratio='0.4167' data-key='$437712314481272' >" +
                                  "</div>";

    var externalPlayerHolderContentHtml = "<div class='playerbox' id='player'>" +
                          "<iframe class='playerbox video-tracking' id='myVideo'  width='853' height='100%' data-frameborder='0' data-webkitallowfullscreen='true' data-mozallowfullscreen='true' data-allowfullscreen='true'>" +
                          "</iframe>" +
                          "</div>";

    var externalPlayerHolderContentHtmlSmall = "<div class='playerbox' id='playerSmall'>" +
                      "<iframe class='playerbox video-tracking' id='myVideo' data-frameborder='0' data-webkitallowfullscreen='true' data-mozallowfullscreen='true' data-allowfullscreen='true'>" +
                      "</iframe>" +
                      "</div>";

    playerHolder = isBrowseViewActive ? $("#PlayerHolderPreview") : $("#PlayerHolder");

    playerHolder.html("");

    if (isExternal) {
        if (isDailyMotion) {
            playerHolder.html(isBrowseViewActive ? dailyMotionPlayerHolderHtmlSmall : dailyMotionPlayerHolderHtml);
        }
        else {
            playerHolder.html(isBrowseViewActive ? externalPlayerHolderContentHtmlSmall : externalPlayerHolderContentHtml);
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
$(window).bind('beforeunload', function (playlist, playBackOrderNum) {
    var params = '{"visitorId":' + visitorId + ',"durationCount":' + durationCount + '}';

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
    var startTimeForUpdate = parseInt(parseFloat(currentlyPlayingVideo.PlayerPlaybackStartTimeInSec).toFixed());
    AddVideoViewEnd(currentlyPlayingVideo.VideoTubeId)
 
});
function ProcessNextVideo(nextVideoPlaylistIndex, startTime) {
    if (nextVideoPlaylistIndex != undefined) {
        currentlyPlayingVideo = playlist[nextVideoPlaylistIndex];


        playBackOrderNum = nextVideoPlaylistIndex;
        AddVideoViewStartForUser(currentlyPlayingVideo.VideoTubeId);
        if (playBackOrderNum != 0) {
            var prevVideo = playlist[nextVideoPlaylistIndex - 1];
            AddVideoViewEnd(prevVideo.VideoTubeId);
            // AddNewOnEnd(prevVideo.VideoTubeId, currentlyPlayingVideo.VideoTubeId, null);
            //AddNewOnEnd onbeforeunload
        }
        currentlyPlayingVideoStartTime = new Date();
        currentlyPlayingVideoStartTime.setTime(currentlyPlayingVideoStartTime.getTime() - (startTime * 1000));
    }

    if (currentlyPlayingVideo) {
        var videoProvider = currentlyPlayingVideo.VideoProviderName;

        UpdateCurrentlyPlayingVideoInfoOnUI(currentlyPlayingVideo);

        if (videoProvider == "vimeo") {
            InitVimeoPlayer(playlist, nextVideoPlaylistIndex, startTime);
        }
        else if (videoProvider == "youtube") {
            InitYoutubePlayer(playlist, nextVideoPlaylistIndex, startTime);
        }
        else if (videoProvider == "dailymotion") {
            InitDMotionPlayer(playlist, nextVideoPlaylistIndex, startTime);
        }
        else {
            InitFlowPlayer(playlist, nextVideoPlaylistIndex, startTime);
        }
      
    }
    else {
        GetScheduleData(activeChannelId, true);
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
};

function AddVideoViewEnd(currentVideoId)
{
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
        }
       
    });
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
        if (userId ==  "" || userId == undefined || userId == NULL) {
            userId = 0;
        }
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
            $(".options .addtofavorite").removeAttr("onclick").attr("onclick", "RemoveFromFavorite()").text("").removeAttr("title").attr("title", "remove from favorite");
            var pattern = /[0-9]+/g;

            //var funText = $("#lblSubscribers").text();
            //var funNumberStr = funText.match(pattern);
            //var funNumber = parseInt(funNumberStr[0]);
            //var funsAfterAdd = funNumber + 1;
            //$("#lblSubscribers").text("").text("fans: " + funsAfterAdd);

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

            //var funText = $("#lblSubscribers").text();
            //var funNumberStr = funText.match(pattern);
            //var funNumber = parseInt(funNumberStr[0]);
            //var funsAfterRemove = funNumber - 1;
            //$("#lblSubscribers").text("").text("fans: " + funsAfterRemove)
            alertify.success("You have successfully unsubscribed from this channel.");
        },
        complete: function () {
        },
        error: function () {
        }
    });
};

function WatchItLater(videoId) {

    if (userId == "" || userId == undefined || userId == 0) {
        closeVideoPopup();
        openLoginDialog();
        return;
    }

    if (videoId != null && videoId != undefined) {
        videoIdToRecord = videoId;
    }
    else {
        videoIdToRecord = playingVideoId;
    }

    if (videoIdToRecord != 0) {
        var clientTime = getClientTime();
        $.ajax({
            type: "POST",
            url: webMethodAddVideoToArchive,
            data: '{"videoId":' + videoIdToRecord + ',"clientTime":' + "'" + clientTime + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (recordedVideos.indexOf(videoIdToRecord) == -1) {
                    recordedVideos.push(videoIdToRecord);
                }

                $(".TLrecordLed").addClass('TLrecordLedActive');
                $(".watchlater").text("");
                $(".watchlater").addClass('watchlaterActive');

                $("#rec" + videoIdToRecord).addClass("recordActive");

                alertify.success("Video was successfully added to your 'Watch Later' list.");
            },
            complete: function () {
            },
            error: function () {
            }
        });
    };
};
//function ShowChannelPasswordPopup() {

//    $('#channelPasswordModal').lightbox_me({
//        centered: true,
//        onLoad: function () {
//        },
//        onClose: function () {
           
//            if(executeSchedulePolling==0)
//            {
//                window.location="/home"
//            }
//            else
//            {
//                RemoveOverlay();
//            }
//        }
//    });
//};
//function ValidateChannelPassword()
//{
//    var pass = $("#channelPasswordInput").val();
//    var params = '{"channelId":' +  channelTubeId + 
//                ',"password":' + "'" + pass + "'" +'}';
//    $.ajax({
//        type: "POST",
//        url: webMethodValidateChannelPassword,
//        data: params,
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        success: function (response) {
//            if (response == false)
//            {
//                executeSchedulePolling = 0;
//                $("#channelPasswordModal #passMessage").text("").text("password is not correct");
                
//                isPasswordValid = false;

//                return false;
                
//            }
//            else {
//                executeSchedulePolling = 1;
//                isPasswordValid = true;
//                SchedulePolling();
//                $('#channelPasswordModal').trigger("close");
//            }
          
//        },
//        error: function (jqXHR, textStatus, errorThrown) {
//        }
//    });

//}
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
    var selectedOption = $('#ddlCategoryAbuse option:selected').text();

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
            $(".sideContentHolder").html("<div/>");
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

    if (activePanelName != undefined) {
        if (activePanelName == 'schedule') {
            ShowSchedule();
        }
        //else if (activePanelName == 'mychannels') {
        //    ShowMyChannels();
        //}
        //else if (activePanelName == 'favorite') {
        //    ShowFavorites();
        //}
        //else if (activePanelName == 'topchannels') {
        //    ShowTopChannels();
        //}
        else if (activePanelName == 'chat') {
            GetFBComments();
        }
    }
    $(".nano").nanoScroller({ alwaysVisible: false });
};

function HideSchedule() {

    $(".hideInfoScheduleBlock").hide('slide', { direction: 'right' }, 500);
    $(".showScheduleWrapper").show();
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
    $(".channelWatchigDescription").css("height", "80px");
    $(".channelWatchigDescription p").css("height", "auto")
    $("#chDescription").removeClass("descriptionPFull").addClass('descriptionP');
    $(".channelWatchigDescription .moreInfo").text("").text("more").removeAttr("onclick").attr("onclick", "showChannelDescription()");
};

function showCreatorBio() {
    $("#innerLeftWrapperChannel, #titleHolder, .descriptionHolder, #innerRightWrapper, #ChannelPageSocialHolder, .infoDevider").show();
    $(".channelCreatorBIO").css("height", "350px");
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
                $(".seeMoreChannels").hide();
            }
            else {
                var currentlyPlayingChannels = Controls.BuildChannelControlForBrowseResultsPage(response.d, true);
                $(".moreChannelsContent").show().html(currentlyPlayingChannels);
                //console.log(response.d.channelTubeId)
                if (response.d == undefined || response.d.length <= 6) {
                    $(".seeMoreChannels").hide();
                }
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

function OpenEmbededArea() {
    var embeddedChannelUrl = (accountNumber != undefined)
                                ? "https://" + domainName + "/embedded/" + activeChannel.ChannelOwnerUrl + "/" + activeChannel.ChannelUrl + "?embeded=true&account=" + accountNumber
                                : "https://" + domainName + "/embedded/" + activeChannel.ChannelOwnerUrl + "/" + activeChannel.ChannelUrl + "?embeded=true";
   
    $('#embeddedCodeHolder').show();
    $(".embeddedCode").addClass("embedActive").removeAttr('onclick').attr("onclick", "CloseEmbededArea()");
    var embededCode = "<iframe frameborder='0'  scrolling='no' allowfullscreen width='100%'  height='100%'   src='" + embeddedChannelUrl + "'></iframe>";
   
    $("#embededCodeCopyArea").text("").text(embededCode);
    $("#embeddedCodeHolder").show();
    $('#embededCodeCopyArea').select();
}

function CloseEmbededArea() {
    $('#embeddedCodeHolder').hide();
    $('.embeddedCode').removeAttr("embedActive").removeAttr('onclick').attr("onclick", "OpenEmbededArea()");
}

//////////////////// TIMELINE CODE ///////////////////////////////////
function loadAllChannels() {
    loadFirstChannel = true;
    switchToFullVideo();
    showAllChannels();
}

function tabShowAllChannels() {
    isActiveChannelsByTab = false;
    if (!$(".TVGuideBottom").is(":visible")) {
        showAllChannels();
    }
    openTvGuide();
}

function showAllChannels(index) {
    $("#loadingDiv").show();

    resetAllModes('allchannels');

    isAllChannelMode = true;

    var date = new Date();
    var timeOfRequest = (date.getMonth() + 1) + '-' + date.getDate() + '-' + date.getFullYear() + '-' + date.getHours() + '-' + date.getMinutes();

    pageIndex = (index == undefined) ? 1 : index;

    $('.TVguideTitle').addClass('TVguideTitleActive');

    $.ajax({
        type: 'POST',
        url: webMethodTvGuideAllChannels,
        dataType: 'json',
        data: '{"clientTime":' + "'" + timeOfRequest + "'" + ', "pageIndex":' + pageIndex + '}',
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            displayedChannels = response.d;

            var hasChannels = displayedChannels.ActiveChannels != null && displayedChannels.ActiveChannels.length > 0;

            if (displayedChannels) {

                if (loadFirstChannel && hasChannels) {
                    var firstChannelId = displayedChannels.ActiveChannels[0].Channel.ChannelTubeId;
                    LoadChannel(firstChannelId);
                    loadFirstChannel = false;
                }

                displayTvGuide(displayedChannels);
                highlightActiveChannel();

                if (hasChannels) {
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
};

function tabShowTopChannels() {
    isActiveChannelsByTab = false;
    if (!$(".TVGuideBottom").is(":visible")) {
        showTopChannels();
    }
    openTvGuide();
}

function showTopChannels(index) {
    $("#loadingDiv").show();

    resetAllModes('topchannels');

    isTopChannelMode = true;

    var date = new Date();
    var timeOfRequest = (date.getMonth() + 1) + '-' + date.getDate() + '-' + date.getFullYear() + '-' + date.getHours() + '-' + date.getMinutes();

    pageIndex = (index == undefined) ? 1 : index;

    $('.TLtopChannels').addClass('TLtopChannelsActive');

    $.ajax({
        type: 'POST',
        url: webMethodTvGuideTopChannels,
        dataType: 'json',
        data: '{"clientTime":' + "'" + timeOfRequest + "'" + ', "pageIndex":' + pageIndex + '}',
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            displayedChannels = response.d;

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

function tabShowCreatorChannels() {
    isActiveChannelsByTab = true;
    if (!$(".TVGuideBottom").is(":visible")) {
        showCreatorChannels();
    } 
    openTvGuide();
}

function showCreatorChannels(index) {
    $("#loadingDiv").show();

    resetAllModes('creatorchannels');

    isCreatorChannelsMode = true;

    var date = new Date();
    var timeOfRequest = (date.getMonth() + 1) + '-' + date.getDate() + '-' + date.getFullYear() + '-' + date.getHours() + '-' + date.getMinutes();

    pageIndex = (index == undefined) ? 1 : index;

    $('.TLchannlesBy').addClass('TLchannlesByActive');

    $.ajax({
        type: 'POST',
        url: webMethodTvGuideUserChannelsByPublicUrl,
        dataType: 'json',
        data: '{"clientTime":' + "'" + timeOfRequest + "'" + ', "publicUrl":' + "'" + activeChannelCreator + "'" + ', "pageIndex":' + pageIndex + '}',
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            displayedChannels = response.d;

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

function tabShowMyChannels() {
    isActiveChannelsByTab = false;
    if (!$(".TVGuideBottom").is(":visible")) {
        showMyChannels();
    } 
    openTvGuide();
}

function showMyChannels(index) {
    $("#loadingDiv").show();

    resetAllModes('mychannels');

    isMyChannelMode = true;

    var date = new Date();
    var timeOfRequest = (date.getMonth() + 1) + '-' + date.getDate() + '-' + date.getFullYear() + '-' + date.getHours() + '-' + date.getMinutes();

    pageIndex = (index == undefined) ? 1 : index;

    $('.TLmyChannels').addClass('TLmyChannelsActive');

    $.ajax({
        type: 'POST',
        url: webMethodTvGuideUserChannelsById,
        dataType: 'json',
        data: '{"clientTime":' + "'" + timeOfRequest + "'" + ', "userId":' + userIdCheked + ', "pageIndex":' + pageIndex + '}',
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            displayedChannels = response.d;

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

function tabShowFavoriteChannels() {
    isActiveChannelsByTab = false;
    if (!$(".TVGuideBottom").is(":visible")) {
        showFavoriteChannels();
    }
    
    openTvGuide();
}

function showFavoriteChannels(index) {
    $("#loadingDiv").show();

    resetAllModes('favchannels');

    isFavoriteChannelMode = true;

    var date = new Date();
    var timeOfRequest = (date.getMonth() + 1) + '-' + date.getDate() + '-' + date.getFullYear() + '-' + date.getHours() + '-' + date.getMinutes();

    pageIndex = (index == undefined) ? 1 : index;

    $('.TLfavChannels').addClass('TLfavChannelsActive');

    $.ajax({
        type: 'POST',
        url: webMethodTvGuideFavoriteChannelsForUser,
        dataType: 'json',
        data: '{"clientTime":' + "'" + timeOfRequest + "'" + ', "userId":' + userIdCheked + ', "pageIndex":' + pageIndex + '}',
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            displayedChannels = response.d;

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

function showChannelsByLanguage(index) {
    $("#loadingDiv").show();

    resetAllModes('bylanguage');

    isChannelByLanguageMode = true;

    activeLanguageId = $("#ddlLang option:selected").val();

    var date = new Date();
    var timeOfRequest = (date.getMonth() + 1) + '-' + date.getDate() + '-' + date.getFullYear() + '-' + date.getHours() + '-' + date.getMinutes();

    pageIndex = (index == undefined) ? 1 : index;

    $.ajax({
        type: 'POST',
        url: webMethodTvGuideChannelsByLanguage,
        dataType: 'json',
        data: '{"clientTime":' + "'" + timeOfRequest + "'" + ', "languageId":' + activeLanguageId + ', "pageIndex":' + pageIndex + '}',
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            displayedChannels = response.d;

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

function loadChannelsByCategoryId(id) {
    activeCategoryId = id;
    loadFirstChannel = true;

    $(".closeGuide").hide();

    playerHolder = isBrowseViewActive ? $("#PlayerHolderPreview") : $("#PlayerHolder");

    switchToFullVideo();
    showChannelsByCategory();

    $('.TVguideTitle').addClass('TVguideTitleActive');
}

function showChannelsByCategory(index) {
    $("#loadingDiv").show();

    resetAllModes('bycategory');

    activeCategoryId = (loadFirstChannel == true) ? activeCategoryId : $("#ddlCategory option:selected").val();

    isChannelByCategoryMode = true;

    var date = new Date();
    var timeOfRequest = (date.getMonth() + 1) + '-' + date.getDate() + '-' + date.getFullYear() + '-' + date.getHours() + '-' + date.getMinutes();

    pageIndex = (index == undefined) ? 1 : index;

    if (activeCategoryId != undefined) {
        $.ajax({
            type: 'POST',
            url: webMethodTvGuideChannelsByCategory,
            dataType: 'json',
            data: '{"clientTime":' + "'" + timeOfRequest + "'" + ', "categoryId":' + activeCategoryId + ', "pageIndex":' + pageIndex + '}',
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                displayedChannels = response.d;

                var hasActiveChannels = displayedChannels.ActiveChannels != null && displayedChannels.ActiveChannels.length > 0;

                if (displayedChannels) {
                    if (loadFirstChannel && hasActiveChannels) {
                        activeChannelId = displayedChannels.ActiveChannels[0].Channel.ChannelTubeId;
                        LoadChannel(activeChannelId);
                        loadFirstChannel = false;
                    }

                    displayTvGuide(displayedChannels);
                    highlightActiveChannel();

                    if (hasActiveChannels) {
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
}

function showChannelsByKeywords(index) {
    $("#loadingDiv").show();

    resetAllModes('bykeywords');

    isChannelByKeywordMode = true;

    var date = new Date();
    var timeOfRequest = (date.getMonth() + 1) + '-' + date.getDate() + '-' + date.getFullYear() + '-' + date.getHours() + '-' + date.getMinutes();

    pageIndex = (index == undefined) ? 1 : index;

    var keywords = $("#txtKeywords").val();

    $.ajax({
        type: 'POST',
        url: webMethodTvGuideChannelsByKeywords,
        dataType: 'json',
        data: '{"clientTime":' + "'" + timeOfRequest + "'" + ', "keywords":' + "'" + keywords + "'" + ', "pageIndex":' + pageIndex + '}',
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            displayedChannels = response.d;

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
                }
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
}

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

function closeVideoPopup() {
    $(".TLrecordLed").removeClass('TLrecordLedActive');
    $("#popupVideo").hide();
    $("#popupChannel").hide();
    $(".creatorInfoPopUP").hide();
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
    closeVideoPopup();

    $("#popupVideo").show();
}

function openChannelPopup() {
    closeChannelPopup();
    closeVideoPopup();

    $("#popupChannel").show();
}

function displayChannelInfoPopup(channelIdentifier) {
    closeChannelPopup();
    closeVideoPopup();

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

                    if (user.ProfileImageUrl != '' && user.ProfileImageUrl != null) {
                        userAvatar.css('background-image', "url('" + user.ProfileImageUrl + "')");
                    }
                    else {
                        userAvatar.css('background-image', "url('" + (user.Gender == 'Male') ? defaultMaleAvatar : defaultFemaleAvatar + "')");
                    }

                    var command = "LoadChannelAndCloseTvGuide(" + targetChannel.ChannelTubeId + ")";

                    watch.removeAttr('onclick').attr('onclick', command);

                    //popup.css('left', mX - 85);
                    //popup.css('top', mY - (isBrowseViewActive ? 680 : 525));
                    //popup.css('z-index', 300);

                    if (isBrowseViewActive) {
                        popup.css('top', $('#divPagination').position().top + 200);
                    }

                    popup.show();
                }
            }
        }
    }
    else {
        $("#popupChannel").hide();
    }
}

function displayVideoInfoPopup(itemIdentifier) {
    closeChannelPopup();
    closeVideoPopup();

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

                if (recordedVideos.indexOf(videoSchedule.VideoTubeId) != -1) {
                    $(".TLrecordLed").addClass('TLrecordLedActive');
                }

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

                if (isBrowseViewActive) {
                    $("#videoWatchNow").hide();
                }
                else {
                    $("#videoWatchNow").show();
                }

                //popup.css('left', mX - 110);
                //popup.css('top', mY - (isBrowseViewActive ? 670 : 650));
                //popup.css('z-index', 300);

                if (isBrowseViewActive) {
                    popup.css('top', $('#divPagination').position().top + 200);
                }

                popup.show();
            }
        }
    }
    else {
        $("#popupVideo").hide();
    }
}

function LoadChannelAndCloseTvGuide(channelId, element) {
    closeTvGuide();
    LoadChannel(channelId, element);
}

function LoadChannel(channelId, element) {
    // check if channel is password protected
  
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
    if (isTopChannelMode) {
        showTopChannels(index);
    }
    else if (isMyChannelMode) {
        showMyChannels(index);
    }
    else if (isFavoriteChannelMode) {
        showFavoriteChannels(index);
    }
    else if (isChannelByLanguageMode) {
        showChannelsByLanguage(index);
    }
    else if (isChannelByKeywordMode) {
        showChannelsByKeywords(index);
    }
    else if (isChannelByCategoryMode) {
        showChannelsByCategory(index);
    }
    else if (isCreatorChannelsMode) {
        showCreatorChannels(index);
    }
    else {
        showAllChannels(index);
    }
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

function populateLanguages() {
    $.ajax({
        type: "POST",
        url: webMethodGetChannelLanguages,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            languages = response.d;
            $("#ddlLang").html("").html("<option value=0>All Languages</option>")
            $.each(response.d, function (key, val) {
                if (key == 11) {
                    $("#ddlLang").append('<option style="border-bottom:1px solid black;" disabled="disabled">&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;</option>');
                }
                $("#ddlLang").append($('<option>', { value: val.LanguageId }).text(val.Name));
            })

            $('#ddlLang option[value="0"]').prop('selected', true);
        },
        error: function (request, status, error) {

        }
    });
}

function populateChannelCategories() {
    $.ajax({
        type: "POST",
        url: webMethodGetChannelCategories,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            categories = response.d;

            $('#ddlCategory').html("").html("<option value=0>All Categories</option>");

            $.each(categories, function (i, c) {
                if (activeCategoryName != undefined && activeCategoryName != '' && activeCategoryName == c.Name) {
                    activeCategoryId = c.CategoryId;
                }

                $("#ddlCategory").append($('<option>', { value: c.CategoryId }).text(c.Name));
            });

            $('#ddlCategory option[value="' + activeCategoryId + '"]').prop('selected', true);

        },
        complete: function (response) {
        }
    });
};

function channelSearchTvGuideInputKeyUp() {
    var key = event.keyCode || event.charCode;

    var shouldSearch = false;
    var keywordsSpecified = $("#txtKeywords").val().length != 0;

    if (keywordsSpecified) {
        $('#btnClearSerachedVideosForChannel').show();
    }
    else {
        $('#btnClearSerachedVideosForChannel').hide();
    }

    switch (key) {
        case 13:
            if (keywordsSpecified) {
                shouldSearch = true;
            }
            break;
        case 46:
            if (isInSearchMode) {
                shouldSearch = true;
            }
            break;
        default:
            break;
    }

    if (keywordsSpecified) {
        if (shouldSearch) {
            isInSearchMode = true;
            showChannelsByKeywords();
        }
    }
    else {
        showAllChannels();
    }

}