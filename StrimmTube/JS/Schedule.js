var webMethodClearSession = "/WebServices/ScheduleWebService.asmx/ClearSession";
var webMethodRemoveVideoSchedule = "/WebServices/ScheduleWebService.asmx/RemoveVideoFromChannelSchedule";
var webMethodRepeatSchedule = "/WebServices/ScheduleWebService.asmx/RepeatSchedule";
var webMethodSetAutoPilotForChannel = "/WebServices/ScheduleWebService.asmx/SetAutoPilotForChannel";
var webMethodUpdateVideoSchedule = "/WebServices/ScheduleWebService.asmx/UpdateVideoSchedule";
var webMethodGetVideoTubeModelForChannelAndCategoryByPage = "/WebServices/ScheduleWebService.asmx/GetVideoTubeModelForChannelAndCategoryByPage";
var webMethodCreateInstantSchedule = "/WebServices/ScheduleWebService.asmx/CreateInstantSchedule"
var webMethodRemoveFromChannel = "/WebServices/ChannelWebService.asmx/RemoveVideoFromChannel";
var webMethodRemoveLiveFromChannel = "/WebServices/ChannelWebService.asmx/RemoveLiveVideoFromChannel";
var webMethodGetChannelCategoriesJson = "/WebServices/ChannelWebService.asmx/GetVideoCategoriesForChannel";
var webMethodAddChannelSchedule = "/WebServices/ScheduleWebService.asmx/CreateChannelSchedule";
var webMethodAddVideoToChannelSchedule = "/WebServices/ScheduleWebService.asmx/AddVideoToChannelSchedule";
var webMethodUpdateScheduleOnDrop = "/WebServices/ScheduleWebService.asmx/UpdateScheduleOnDrop";
var webMethodAddScheduleOnDrop = "/WebServices/ScheduleWebService.asmx/AddScheduleOnDrop";
var webMethodDeleteScheduleOnDrop = "/WebServices/ScheduleWebService.asmx/DeleteScheduleOnDrop";
var webMethodReoderSchedule = "/WebServices/ScheduleWebService.asmx/ReoderSchedule";
var webMethodDeleteChannelSchedule = "/WebServices/ScheduleWebService.asmx/RemoveChannelSchedule";
var webMethodPublishSchedule = "/WebServices/ScheduleWebService.asmx/UpdatePublishFlagForChannelSchedule";
var webMethodAddVideoToChannel = "/WebServices/ChannelWebService.asmx/AddVideoToChannel";
var webMethodAddExternalVideoToChannel = "/WebServices/ChannelWebService.asmx/AddExternalVideoToChannelForCategory";
var webMethodAddExternalVideoToChannelWithCustomDuration = "/WebServices/ChannelWebService.asmx/AddExternalVideoToChannelForCategoryWithCustomDuration";
var webMethodAddAllImportedVideosToChannel = "/WebServices/ChannelWebService.asmx/AddAllImportedYoutubeVideos";
var webMethodAddExternalVideoVimeoToChannel = "/WebServices/ChannelWebService.asmx/AddExternalVideoVimeoToChannelForCategory";
var webMethodAddExternalVideoDmotionToChannel = "/WebServices/ChannelWebService.asmx/AddExternalVideoVimeoToChannelForCategory";
var webMethodGetChannelCategoriesForPublicLibraryJson = "/WebServices/PublicLibraryService.asmx/GetVideoCategoriesForPublicLibrary";
var webMethodGetPublicLibResult = "/WebServices/PublicLibraryService.asmx/GetAllPublicVideosByPageIndex";
var webMethodGetVideoByKeyWord = "/WebServices/YouTubeWebService.asmx/FindVideosByKeywords";
var webMethodGetEmptyChannelCategoriesJson = "/WebServices/ChannelWebService.asmx/GetChannelCategories";
var webMethodGetEmptyChannelCategoriesJsonForVideos = "/WebServices/ChannelWebService.asmx/GetChannelCategoriesForVideos";
var webMethodGetVideoByUrl = "/WebServices/YouTubeWebService.asmx/FindVideoByUrl";
var webMethodGetChannelCategoriesForVideoRoomJson = "/WebServices/VideoRoomService.asmx/GetVideoCategoriesForVideoRoom";
var webMethodLoadVideosFRomVideoRoom = "/WebServices/VideoRoomService.asmx/GetAllVideoTubePoByPageIndexAndUserId";
var webMethodGetChannelScheduleCalendarEvents = "/WebServices/ScheduleWebService.asmx/GetChannelTubeScheduleCalendarEvents";
var webMethodGetChannelSchedulesByDate = "/WebServices/ScheduleWebService.asmx/GetChannelTubeSchedulesByDate";
var webMethodGetNamesOfAllChannelsForUser = "/WebServices/ChannelWebService.asmx/GetNamesOfAllChannelsForUser";
var webMethodRemoveAllVideosFromChannel = "/WebServices/ChannelWebService.asmx/RemoveAllVideosFromChannel";
var webMethodClearRestrictedOrRemovedVideos = "/WebServices/ChannelWebService.asmx/ClearRestrictedOrRemovedVideos";
var webMethodRemoveVideoFromVr = "/WebServices/VideoRoomService.asmx/RemoveVideoFromVideoRoom";
var webMethodGetScheduleByChannelScheduleId = "/WebServices/ScheduleWebService.asmx/GetChannelScheduleByChannelScheduleId";
var webMethodGetVideoTubeByKeywordAndChannelId = "/WebServices/SearchWebService.asmx/GetVideoTubeByKeywordAndChannelId";
var webMethodGetVideoTubeByKeywordForPublicLib = "/WebServices/SearchWebService.asmx/GetVideoTubeByKeywordForPublicLibrary";
var webMethodGetVideoProviders = "/WebServices/ChannelWebService.asmx/GetAllVideoProvideres";
var webMethodAddPrivateVideoToChannel = "/WebServices/ChannelWebService.asmx/AddPrivateVideoToChannel";
var webMethodGetVideoTubeById = "/WebServices/ChannelWebService.asmx/GetVideoByVideoId";
var webMethodUpdatePrivateVideo = "/WebServices/ChannelWebService.asmx/UpdatePrivateVideoById";
var webMethodAddLiveVideo = "/WebServices/ChannelWebService.asmx/AddExternalLiveVideoToChannelForCategory";


var myCalendarMain;
var userId;
var categoryId = 0;
var countResultIndex = 0;
var pageIndex = 1;
var prevPageIndex = 1;
var nextPageIndex = 1;
var pickedDateTime = new Date();
var channelScheduleId = 0;
var scheduleCount = 0;
var activeChannelScheduleId = 0;
var repeatpickedDate = new Date();
var activeTab = 0;
var clearScreen = false;
var userChannels;
var isSchedulePublished;
var pageToken = "";
var pageCountVimeo = 1;
var accessToken = "33750ba7c56c035ea9dd4d9e5ffabd04";
var isFuture = false;
var isToday = false;
var isPast = false;
var shouldLoadAllVideos;
var shouldLoadMyVideos;
var shouldLoadLicensedVideos;
var shouldLoadExternalVideos;
var isfirstChannel = false;
var providerId = 0;
var isBasicMode;
var isReload;
var isBasicModeCookieName = 'isBasicMode_';
var isFirstChannelCookieName = 'isfirstChannel_';
var drake;
var hasValidRokuVideos = true;
var hasHLSVideosOnly = true;
var hasProfPlusPlan = false;
var hasProfPlan = false;

function getURLParameter(name) {
    return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [, ""])[1].replace(/\+/g, '%20')) || null;
};

$(document).ajaxStart(function () {
    $("#loadingDiv").show();
});

$(document).ajaxStop(function () {
    $("#loadingDiv").hide();
});

//$(window).bind('beforeunload', function () {
//    var isPublished = false;
//    if ($(".ancPublishSchedule").is(":visible")) {
//        if ($(".ancPublishSchedule").hasClass("published") || $(".ancPublishSchedule").hasClass("publishedDisactivated")) {
//            isPublished = true;
//        }
//        else {
//            isPublished = false;
//        }
//        if (isPublished == false)
//            return "Warning: please click 'Publish', if you want to activate your schedule";
//    }

//});


    //dragula([document.querySelector('.scheduleContent'), document.querySelector('.scheduleContent')])
    //                 .on('drag', function (el) {
    //                     el.className = el.className.replace('ex-moved', '');
    //                 }).on('drop', function (el) {
    //                     el.className += ' ex-moved';
    //                 }).on('over', function (el, container) {
    //                     container.className += ' ex-over';
    //                 }).on('out', function (el, container) {
    //                     container.className = container.className.replace('ex-over', '');
    //                 });
  



var windowWidth;
var mobile;
function isMobileDevice() {
    return (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino|android|ipad|playbook|silk/i.test(navigator.userAgent || navigator.vendor || window.opera) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test((navigator.userAgent || navigator.vendor || window.opera).substr(0, 4)));
};
function clickHandler(e) {
    var target = e.target;
    if (target === sortable) {
        return;
    }
    target.innerHTML += ' [click!]';
    void 0
    //if (el.classList != "divSchedule gu-transit") {
    //    if (container) {

    //        var prevPlaybackOrderNumber = $(el).prev(".divSchedule").attr("data-playbackOrder");
    //        if (prevPlaybackOrderNumber === undefined) {
    //            prevPlaybackOrderNumber = 1;
    //            addVideoScedule(el);
    //        }
    //        else {
    //            UpdateScheduleOnDrop(prevPlaybackOrderNumber, el)

    //        }
    //    }
    //}
    setTimeout(function () {
        target.innerHTML = target.innerHTML.replace(/ \[click!\]/g, '');
    }, 500);
}
$(document).ready(function () {
    var crossvent = $('.scheduleContent');
    drake = dragula([document.querySelector('.videoBoxHolder.channelsHolder'), document.querySelector('.scheduleContent')], {
        copy: function (el, source) {
            return source === document.querySelector('.videoBoxHolder.channelsHolder');
          


        },
        accepts: function (el, target) {
            return target !== document.querySelector('.videoBoxHolder.channelsHolder');
            

        },
        isContainer: function (el) {
            return el.classList.contains('dragula-container');
        }
    }).on("drop", function (el, container, source) {
        var prevPlaybackOrderNumber = $(el).prev(".divSchedule").attr("data-playbackOrder");
        if (el.classList != "divSchedule gu-transit") {
            if (container) {


                if (prevPlaybackOrderNumber === undefined) {
                    prevPlaybackOrderNumber = 1;

                }
                AddScheduleOnDrop(prevPlaybackOrderNumber, el);
            }
            else if ($(".airtimeHolder").html() === undefined) {
                alertify.alert("Create a custom schedule first by clicking on the 'Custom Schedule' button and try adding this video again.");
            }
            else {
                alertify.alert("Please follow “How Drag & Drop Works” procedure, located above the videos (link) in order to add the video properly"); 
            }
           
        }
        else {
            ReoderSchedule(prevPlaybackOrderNumber, el);
            //console.log("reoder")

           
        }
 
        }).on("drag", function (el, container, source) {

            $(".gu-transit").css("width", "100%");
            $(".gu-transit").css("max-width", "100%");
            $(".gu-transit").height(60);
          
        }).on("over", function (el, container, source) {
           
            $(".gu-transit").css("width", "100%");
            $(".gu-transit").css("max-width", "100%");
            $(".gu-transit").height(60);
            var orderBelow = $(".divSchedule.gu-transit").next(".divSchedule").attr("data-playbackorder");
           

          
           
        }).on("shadow", function (el, container, source) {
            $(".gu-transit").css("width", "100%");
             $(".gu-transit").css("max-width", "100%");
            $(".gu-transit").height(60);
        
        }).on("out", function (el, container, source) {
          
            //if (container.classList[0] == "scheduleContent") {
            //    $("#" + el.id + " .btnRemove").trigger("click");

               
                
            //}
 
});
    //var drakeSortable = dragula([document.getElementById(crossvent)], {
    //    copy: true
        
    //}).on("drop", function (el, container, source) {
       
    //    console.log("drakesort")
    //});

   

    var opts = {
        lines: 12             // The number of lines to draw
       , length: 7             // The length of each line
       , width: 5              // The line thickness
       , radius: 10            // The radius of the inner circle
       , scale: 1.0            // Scales overall size of the spinner
       , corners: 1            // Roundness (0..1)
       , color: '#000'         // #rgb or #rrggbb
       , opacity: 1          // Opacity of the lines
       , rotate: 0             // Rotation offset
       , direction: 1          // 1: clockwise, -1: counterclockwise
       , speed: 1              // Rounds per second
       , trail: 100            // Afterglow percentage
       , fps: 20               // Frames per second when using setTimeout()
       , zIndex: 2e9           // Use a high z-index by default
       , className: 'spinner'  // CSS class to assign to the element
       , top: '50%'            // center vertically
       , left: '50%'           // center horizontally
       , shadow: false         // Whether to render a shadow
       , hwaccel: false        // Whether to use hardware acceleration (might be buggy)
       , position: 'absolute'  // Element positioning
    };

    var target = document.getElementById('loadingDiv')
    var spinner = new Spinner(opts).spin(target)
    //$(".timeZoneSelect").timezones()
    if (isMobileDevice()) {
        setWorkingMode(false);
    }
    windowWidth = window.screen.width < window.outerWidth ?
                  window.screen.width : window.outerWidth;
    mobile = windowWidth < 500;
    //  console.log(matureChannelContentEnabled);
    if (isMobileDevice()) {
        $("#btnStartScheduleOk").val("").val("next");
        $(window).scroll(function () {
            //if you hard code, then use console
            //.log to determine when you want the 
            //nav bar to stick.  
            //console.log($(window).scrollTop())
            if ($(window).scrollTop() > 395) {
                $('.mobileNavSchedule').addClass('navbar-fixed');
            }
            if ($(window).scrollTop() < 395) {
                $('.mobileNavSchedule').removeClass('navbar-fixed');
            }

        });
        $(".videoBoxHolder.channelsHolder").show();
        $(".rightnewSchedulePopup").hide();
    }
    else {
        $("#btnStartScheduleOk").val("").val("create");
        isFirstChannelCookieName = 'isfirstChannel_' + userId;
        isBasicModeCookieName = 'isBasicMode_' + userId;
        isfirstChannel = getCookie(isFirstChannelCookieName);
        void 0;
        if (isfirstChannel == undefined) {
            isfirstChannel = true;
        }
        if (isfirstChannel) {
            TriggerWalkTruByWorkingMode();
        }

    }



    $("#txtSearchVideoByKeywordForChannel").val("");

    //END STEPS WALK THRU

    if (isNewChannel != undefined && isNewChannel == "True") {
        alertify.alert("Congratulations! You just created a new channel. Welcome to your Production Studio.");
    }

    ToggleRemoveMenu();

    if (isautopilot0N == "True") {
        $("#btnInstantSchedule, .ancConfirmDateTime").removeAttr("onclick");
        $("#btnAutopilot").attr("onclick", "SetAutoPilotOff()").attr("class", "checkboxHolderON");
    }
    else {
        $("#btnAutopilot").attr("onclick", "SetAutoPilotOn()").attr("class", "checkboxHolderOFF");
    }
    $("#ddlChannelVideos option[value='0']").attr("selected", true);
    LoadChannelCategories();
    InitializeEventsCalendar();
    RetrieveAvailableChannels();
    LoadMoreVideos();

    pageIndex = 1;

    setWorkingMode(true);



    window.setInterval(function () {
        UpdateAvailableScheduleTimes()
    }, 60000);
    //window.onbeforeunload = function (e) {
    //    var message = "Test.",
    //    e = e || window.event;

    //    // For IE and Firefox
    //    if (e) {
    //        e.returnValue = message;
    //    }

    //    // For Safari
    //    return message;
    //};
});


function ReoderSchedule(targetPlaybackOrderNumber, btn) {
    
   
    
    var playbackOrderNumber = $(btn).attr("data-playbackorder");
    void 0;
    void 0;
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var videoTubeId = idArr[1];
    var videoMatureContentEnabled = $("#boxContent_" + videoTubeId).attr("data-maturecontent");
    var isPrivateVideo = $("#boxContent_" + videoTubeId).attr("data-isprivate");
    void 0;
    if (videoMatureContentEnabled == "true") {
        if (!matureChannelContentEnabled) {
            alertify.set({
                'labels': { ok: 'Ok' },
                'defaultFocus': 'ok'
            });
            alertify.alert("channel does not allow mature content");
            return;
        }
    }
    if (isPrivateVideo == "true") {
        if (!showPrivateVideoMode) {
            alertify.set({
                'labels': { ok: 'Ok' },
                'defaultFocus': 'ok'
            });
            alertify.alert("channel does not allow private videos, please allow private videos in channel settings.");
            return;
        }
    }
    void 0
    var channelScheduleIDOnDrag;
    if (activeChannelScheduleId == 0) {
        channelScheduleIDOnDrag = $(btn).closest(".scheduleBoxContentBox").attr("id").split("_")[1];
        void 0
    }
    else {
        channelScheduleIDOnDrag = activeChannelScheduleId
    }
    if (targetPlaybackOrderNumber === undefined) {
        targetPlaybackOrderNumber = 1;
    }
    // console.log(videoMatureContentEnabled);
    if (channelScheduleIDOnDrag != 0) {
        $("#loadingDiv").show();
        $.ajax({
            type: "POST",
            url: webMethodReoderSchedule,
            cashe: false,
            data: '{"channelScheduleId":' + channelScheduleIDOnDrag + ',"targetPlaybackOrderNumber":' + targetPlaybackOrderNumber + ',"playbackOrderNumber":' + playbackOrderNumber + ', "videoTubeId": ' + videoTubeId + '}',
            dataType: "json",
            contentType: "application/json",
            success: function (response) {
                void 0;
                var day = moment(pickedDateTime).format("MM.DD.YY");


                todaysChannelTubes = response.d;

                if (todaysChannelTubes) {
                    if (todaysChannelTubes.length == 1 && todaysChannelTubes[0].ChannelScheduleId == 0) {
                       // alertify.error(todaysChannelTubes[0].Message);

                    }
                    else {
                        var addedDateHTML = "<span class='recentlyadded'>added to " + day + "</span>";
                        $("#boxContent_" + videoTubeId + " .recentlyadded").remove();
                        $("#action_" + videoTubeId + " .VideoBoxPlay").after(addedDateHTML);
                        $(".scheduleThumbHolder").html("");
                        var idsOfScheduleToExpand = new Array();

                        $.each(todaysChannelTubes, function (i, d) {
                            var dayschedule = Controls.BuildChannelScheduleWithVideoForEdit(d);
                            var expandVideoSchedulesList = d.ExpandVideoSchedulesList;
                            if (expandVideoSchedulesList) {
                                idsOfScheduleToExpand[0] = d.ChannelScheduleId;
                            }

                            $(".scheduleThumbHolder").append(dayschedule);

                            if (isBasicMode) {
                                $(".ancRepeat").toggle();
                            }

                            ExpandVideoScheduleList(d.ChannelScheduleId);
                            activeChannelScheduleId = d.ChannelScheduleId;
                            //DisableRepeatButtonsOnChannelSchedules(isAutoPilotOn);
                        });

                        //if (idsOfScheduleToExpand.length > 0) {
                        //    $.each(idsOfScheduleToExpand, function (i, d) {
                        //        ExpandVideoScheduleList(d);
                        //    });
                        //}

                        //// Disable instant schedule for now, since we already created instant schedule for now
                        //// If the new time will become available for schedule creation, re-enable it then
                        //DisableInstantScheduleButton(true);

                        // Do not need to disable "new schedule" button, since
                        // timepicker will have times not available for scheduling already disabled for the time when
                        // schedule already exists.

                        UpdateAvailableScheduleTimes();
                    }


                }
            },
            complete: function (response) {

                $("#recentlyadded_" + videoTubeId).show();

                // $("#loadingDiv").hide();
            }
        });
    }
    
}
function AddScheduleOnDrop(prevOrderNumber, btn) {

    var btnId = btn.id;
    var idArr = btnId.split("_");
    var videoTubeId = idArr[1];
    var videoMatureContentEnabled = $("#boxContent_" + videoTubeId).attr("data-maturecontent");
    var isPrivateVideo = $("#boxContent_" + videoTubeId).attr("data-isprivate");
    void 0;
    if (videoMatureContentEnabled == "true") {
        if (!matureChannelContentEnabled) {
            alertify.set({
                'labels': { ok: 'Ok' },
                'defaultFocus': 'ok'
            });
            alertify.alert("channel does not allow mature content");
            return;
        }
    }
    if (isPrivateVideo == "true") {
        if (!showPrivateVideoMode) {
            alertify.set({
                'labels': { ok: 'Ok' },
                'defaultFocus': 'ok'
            });
            alertify.alert("channel does not allow private videos, please allow private videos in channel settings.");
            return;
        }
    }
    if (activeChannelScheduleId == 0) {
        
        channelScheduleIDOnDrag = $(btn).closest(".scheduleBoxContentBox").attr("id").split("_")[1];
        if (channelScheduleIDOnDrag === undefined) {
            alertify.alert("Create a custom schedule first by clicking on the 'Custom Schedule' button and try adding this video again.");
        }
        void 0
        void 0
    }
    else {
        channelScheduleIDOnDrag = activeChannelScheduleId
    }
    if (channelScheduleIDOnDrag != 0) {
        $("#loadingDiv").show();
        $.ajax({
            type: "POST",
            url: webMethodAddScheduleOnDrop,
            cashe: false,
            data: '{"channelScheduleId":' + channelScheduleIDOnDrag + ',"videoTubeId":' + videoTubeId + ',"orderNumber":' + prevOrderNumber + '}',
            dataType: "json",
            contentType: "application/json",
            success: function (response) {
                var day = moment(pickedDateTime).format("MM.DD.YY");


                todaysChannelTubes = response.d;

                if (todaysChannelTubes) {
                    if (todaysChannelTubes.length == 1 && todaysChannelTubes[0].ChannelScheduleId == 0) {
                        alertify.error(todaysChannelTubes[0].Message);
                        $(".dragula-container .videoBoxNew").remove();

                    }
                    else {
                        var addedDateHTML = "<span class='recentlyadded'>added to " + day + "</span>";
                        $("#boxContent_" + videoTubeId + " .recentlyadded").remove();
                        $("#action_" + videoTubeId + " .VideoBoxPlay").after(addedDateHTML);
                        $(".scheduleThumbHolder").html("");
                        var idsOfScheduleToExpand = new Array();

                        $.each(todaysChannelTubes, function (i, d) {
                            var dayschedule = Controls.BuildChannelScheduleWithVideoForEdit(d);
                            var expandVideoSchedulesList = d.ExpandVideoSchedulesList;
                            if (expandVideoSchedulesList) {
                                idsOfScheduleToExpand[0] = d.ChannelScheduleId;
                            }

                            $(".scheduleThumbHolder").append(dayschedule);

                            if (isBasicMode) {
                                $(".ancRepeat").toggle();
                            }

                            ExpandVideoScheduleList(d.ChannelScheduleId);
                            activeChannelScheduleId = d.ChannelScheduleId;
                            //DisableRepeatButtonsOnChannelSchedules(isAutoPilotOn);
                        });

                        //if (idsOfScheduleToExpand.length > 0) {
                        //    $.each(idsOfScheduleToExpand, function (i, d) {
                        //        ExpandVideoScheduleList(d);
                        //    });
                        //}

                        //// Disable instant schedule for now, since we already created instant schedule for now
                        //// If the new time will become available for schedule creation, re-enable it then
                        //DisableInstantScheduleButton(true);

                        // Do not need to disable "new schedule" button, since
                        // timepicker will have times not available for scheduling already disabled for the time when
                        // schedule already exists.

                        UpdateAvailableScheduleTimes();
                    }


                }
            },
            complete: function (response) {

                $("#recentlyadded_" + videoTubeId).show();

                // $("#loadingDiv").hide();
            }
        });
    }
    else {
        alertify.set({
            'labels': { ok: 'Ok' },
            'defaultFocus': 'ok'
        });
        alertify.alert("Create a custom schedule first by clicking on the 'Custom Schedule' button and try adding this video again.");
    }

}
function DeleteVideoFromScheduleOnDrop(btn) {
   
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var videoTubeId = idArr[1];
    var videoMatureContentEnabled = $("#boxContent_" + videoTubeId).attr("data-maturecontent");
    var isPrivateVideo = $("#boxContent_" + videoTubeId).attr("data-isprivate");
    var playbackOrderNumber = $(btn).attr("data-playbackorder");
  
    if (videoMatureContentEnabled == "true") {
        if (!matureChannelContentEnabled) {
            alertify.set({
                'labels': { ok: 'Ok' },
                'defaultFocus': 'ok'
            });
            alertify.alert("channel does not allow mature content");
            return;
        }
    }
    if (isPrivateVideo == "true") {
        if (!showPrivateVideoMode) {
            alertify.set({
                'labels': { ok: 'Ok' },
                'defaultFocus': 'ok'
            });
            alertify.alert("channel does not allow private videos, please allow private videos in channel settings.");
            return;
        }
    }
    // console.log(videoMatureContentEnabled);
    var channelScheduleIDOnDrag;
    if (activeChannelScheduleId == 0) {
        channelScheduleIDOnDrag = $(btn).closest(".scheduleBoxContentBox").attr("id").split("_")[1];
        void 0
    }
    else {
        channelScheduleIDOnDrag = activeChannelScheduleId
    }
    // console.log(videoMatureContentEnabled);
    if (channelScheduleIDOnDrag != 0) {
        $("#loadingDiv").show();
        $.ajax({
            type: "POST",
            url: webMethodDeleteScheduleOnDrop,
            cashe: false,
            data: '{"channelScheduleId":' + channelScheduleIDOnDrag + ',"videoTubeId":' + videoTubeId + ',"playbackOrderNumber":' + playbackOrderNumber+ '}',
            dataType: "json",
            contentType: "application/json",
            success: function (response) {
                var day = moment(pickedDateTime).format("MM.DD.YY");


                todaysChannelTubes = response.d;

                if (todaysChannelTubes) {
                    if (todaysChannelTubes.length == 1 && todaysChannelTubes[0].ChannelScheduleId == 0) {
                        alertify.error(todaysChannelTubes[0].Message);

                    }
                    else {
                        var addedDateHTML = "<span class='recentlyadded'>added to " + day + "</span>";
                        $("#boxContent_" + videoTubeId + " .recentlyadded").remove();
                        $("#action_" + videoTubeId + " .VideoBoxPlay").after(addedDateHTML);
                        $(".scheduleThumbHolder").html("");
                        var idsOfScheduleToExpand = new Array();

                        $.each(todaysChannelTubes, function (i, d) {
                            var dayschedule = Controls.BuildChannelScheduleWithVideoForEdit(d);
                            var expandVideoSchedulesList = d.ExpandVideoSchedulesList;
                            if (expandVideoSchedulesList) {
                                idsOfScheduleToExpand[0] = d.ChannelScheduleId;
                            }

                            $(".scheduleThumbHolder").append(dayschedule);

                            if (isBasicMode) {
                                $(".ancRepeat").toggle();
                            }

                            ExpandVideoScheduleList(d.ChannelScheduleId);
                            activeChannelScheduleId = d.ChannelScheduleId;
                            //DisableRepeatButtonsOnChannelSchedules(isAutoPilotOn);
                        });

                        //if (idsOfScheduleToExpand.length > 0) {
                        //    $.each(idsOfScheduleToExpand, function (i, d) {
                        //        ExpandVideoScheduleList(d);
                        //    });
                        //}

                        //// Disable instant schedule for now, since we already created instant schedule for now
                        //// If the new time will become available for schedule creation, re-enable it then
                        //DisableInstantScheduleButton(true);

                        // Do not need to disable "new schedule" button, since
                        // timepicker will have times not available for scheduling already disabled for the time when
                        // schedule already exists.

                        UpdateAvailableScheduleTimes();
                    }


                }
            },
            complete: function (response) {

                $("#recentlyadded_" + videoTubeId).show();

                // $("#loadingDiv").hide();
            }
        });
    }
    else {
        alertify.set({
            'labels': { ok: 'Ok' },
            'defaultFocus': 'ok'
        });
        alertify.alert("Create a custom schedule first by clicking on the 'Custom Schedule' button and try adding this video again.");
    }

}
function TriggerWalkTruByWorkingMode() {
    //advancedTour
    if (isBasicMode == true) {
        $("#btnStartTourHolderPS #ancStartTour").show();
        $("#btnStartTourHolderPS #advancedTour").hide();
    }
    else {
        $("#btnStartTourHolderPS #ancStartTour").hide();
        $("#btnStartTourHolderPS #advancedTour").show();
    }

    $.powerTour({
        tours: [
                {
                    trigger: '#ancStartTour',
                    startWith: 1,
                    easyCancel: false,
                    escKeyCancel: true,
                    scrollHorizontal: false,
                    keyboardNavigation: true,
                    loopTour: false,
                    highlightStartSpeed: 200,// new 2.5.0
                    highlightEndSpeed: 200,// new 2.3.0
                    highlight: true,
                    keepHighlighted: false,
                    onStartTour: function (ui) { },
                    onEndTour: function (ui) {
                        void 0
                        setCookie(isFirstChannelCookieName, false, (new Date(2030, 1, 1)));
                        $.powerTour("destroy");
                        
                    },
                    onProgress: function(ui){ },
                    steps: [
                            {
                                hookTo: 'leftSchedule',//not needed
                                content: '#step-one',
                                width: 350,
                                position: 'sc',
                                offsetY: -80,
                                offsetX: -50,
                                fxIn: 'fadeIn',
                                fxOut: 'fadeOut',
                                showStepDelay: 0,
                                center: 'step',
                                scrollSpeed: 400,
                                scrollEasing: 'swing',
                                scrollDelay: 0,
                                timer: '00:00',
                                highlight: true,
                                keepHighlighted: false,
                                keepVisible: false,// new 2.2.0
                                onShowStep: function (ui) { },
                                onHideStep: function (ui) { }
                            },
                            {
                                hookTo: '#btnWatchVideoHolder',
                                content: '#step-two',
                                width: 300,
                                position: 'bm',
                                offsetY: 20,
                                offsetX: -67,
                                fxIn: 'fadeIn',
                                fxOut: 'fadeOut',
                                showStepDelay: 100,
                                center: 'hook',
                                scrollSpeed: 400,
                                scrollEasing: 'swing',
                                scrollDelay: 0,
                                timer: '00:00',
                                highlight: false,
                                keepHighlighted: false,
                                keepVisible: false,// new 2.2.0
                                onShowStep: function (ui) {

                                    // fadd fx class to buy button
                                    //  $('#ancStartTour > .btn').addClass('colorfadingbutton');

                                },
                                onHideStep: function (ui) {

                                    // remove fx class from buy button
                                    // $('#ancStartTour > .btn').removeClass('colorfadingbutton');

                                }
                            },
                            {
                                hookTo: '#channelInfoHolder',
                                content: '#step-three',
                                width: 300,
                                position: 'bl',
                                offsetY: -110,
                                offsetX: 540,
                                fxIn: 'fadeIn',
                                fxOut: 'fadeOut',
                                showStepDelay: 100,
                                center: 'hook',
                                scrollSpeed: 400,
                                scrollEasing: 'swing',
                                scrollDelay: 0,
                                timer: '00:00',
                                highlight: false,
                                keepHighlighted: false,
                                keepVisible: false,// new 2.2.0
                                onShowStep: function (ui) {

                                    // fadd fx class to buy button
                                    //  $('#ancStartTour > .btn').addClass('colorfadingbutton');

                                },
                                onHideStep: function (ui) {

                                    // $('#ancStartTour > .btn').removeClass('colorfadingbutton');

                                }
                            },
                            {
                                hookTo: '#channelInfoHolder',
                                content: '#step-four',
                                width: 300,
                                position: 'bl',
                                offsetY: -50,
                                offsetX: 202,
                                fxIn: 'fadeIn',
                                fxOut: 'fadeOut',
                                showStepDelay: 100,
                                center: 'hook',
                                scrollSpeed: 400,
                                scrollEasing: 'swing',
                                scrollDelay: 0,
                                timer: '00:00',
                                highlight: false,
                                keepHighlighted: false,
                                keepVisible: false,// new 2.2.0
                                onShowStep: function (ui) {

                                    //show the helper images(demo so not needed)


                                },
                                onHideStep: function (ui) {

                                    //hide the helper images(demo so not needed)


                                }
                            },
                            {
                                hookTo: '#divAddVideosMenuHolder',
                                content: '#step-five',
                                width: 300,
                                position: 'rt',
                                offsetY: 55,
                                offsetX: -200,
                                fxIn: 'fadeIn',
                                fxOut: 'fadeOut',
                                showStepDelay: 100,
                                center: 'hook',
                                scrollSpeed: 400,
                                scrollEasing: 'swing',
                                scrollDelay: 0,
                                timer: '00:00',
                                highlight: false,
                                keepHighlighted: false,
                                keepVisible: false,// new 2.2.0 
                                onShowStep: function (ui) {

                                    //$('#hook-four').delay(1000).fadeTo(500, 0.5);

                                    //show the helper images(demo so not needed)


                                },
                                onHideStep: function (ui) {



                                }
                            },
                            {
                                hookTo: '#basicInstantBntHolder',
                                content: '#step-six',
                                width: 300,
                                position: 'ml',
                                offsetY: 75,
                                offsetX: -350,
                                fxIn: 'fadeIn',
                                fxOut: 'fadeOut',
                                showStepDelay: 100,
                                center: 'hook',
                                scrollSpeed: 400,
                                scrollEasing: 'swing',
                                scrollDelay: 0,
                                timer: '00:00',
                                highlight: false,
                                keepHighlighted: false,
                                keepVisible: false,// new 2.2.0
                                onShowStep: function (ui) {



                                },
                                onHideStep: function (ui) {


                                }
                            },
                            {
                                hookTo: '#basicInstantBntHolder',
                                content: '#step-seven',
                                width: 250,
                                position: 'ml',
                                offsetY: 120,
                                offsetX: -380,
                                fxIn: 'fadeIn',
                                fxOut: 'fadeOut',
                                showStepDelay: 100,
                                center: 'hook',
                                scrollSpeed: 400,
                                scrollEasing: 'swing',
                                scrollDelay: 0,
                                timer: '00:00',
                                highlight: false,
                                keepHighlighted: false,
                                keepVisible: false,// new 2.2.0
                                onShowStep: function (ui) {



                                },
                                onHideStep: function (ui) {


                                }
                            },
                            {
                                hookTo: '#basicInstantBntHolder',
                                content: '#step-eight',
                                width: 250,
                                position: 'ml',
                                offsetY: 120,
                                offsetX: -150,
                                fxIn: 'fadeIn',
                                fxOut: 'fadeOut',
                                showStepDelay: 100,
                                center: 'step',
                                scrollSpeed: 400,
                                scrollEasing: 'swing',
                                scrollDelay: 0,
                                timer: '00:00',
                                highlight: false,
                                keepHighlighted: true,
                                keepVisible: false,// new 2.2.0
                                onShowStep: function (ui) { },
                                onHideStep: function (ui) { }
                            },

                            {
                                hookTo: '',//not needed
                                content: '#step-nine',
                                width: 350,
                                position: 'sc',
                                offsetY: -80,
                                offsetX: -50,
                                fxIn: 'fadeIn',
                                fxOut: 'fadeOut',
                                showStepDelay: 0,
                                center: 'step',
                                scrollSpeed: 400,
                                scrollEasing: 'swing',
                                scrollDelay: 0,
                                timer: '00:00',
                                highlight: true,
                                keepHighlighted: false,
                                keepVisible: false,// new 2.2.0
                                onShowStep: function (ui) { },
                                onHideStep: function (ui) { }
                            }

                    ],
                    stepDefaults: [
                           {
                               width: 300,
                               position: 'tr',
                               offsetY: 0,
                               offsetX: 0,
                               fxIn: 'fadeIn',
                               fxOut: 'fadeOut',
                               showStepDelay: 0,
                               center: 'step',
                               scrollSpeed: 400,
                               scrollEasing: 'swing',
                               scrollDelay: 0,
                               timer: '00:00',
                               highlight: true,
                               keepHighlighted: false,
                               keepVisible: false,// new 2.2.0
                               onShowStep: function (ui) { },
                               onHideStep: function (ui) { }
                           }
                    ]
                },
            {
                trigger: '#advancedTour',
                startWith: 1,
                easyCancel: false,
                escKeyCancel: true,
                scrollHorizontal: false,
                keyboardNavigation: true,
                loopTour: false,
                highlightStartSpeed: 200,// new 2.5.0
                highlightEndSpeed: 200,// new 2.3.0
                onStartTour: function (ui) { },
                onEndTour: function (ui) {
                    void 0
                    setCookie(isFirstChannelCookieName, false, (new Date(2030, 1, 1)));
                    $.powerTour("destroy");
                    
                },
                onProgress : function(ui){ },
                steps: [
                        {
                            hookTo: '',//not needed
                            content: '#step-ten',
                            width: 350,
                            position: 'sc',
                            offsetY: -80,
                            offsetX: -50,
                            fxIn: 'fadeIn',
                            fxOut: 'fadeOut',
                            showStepDelay: 0,
                            center: 'step',
                            scrollSpeed: 400,
                            scrollEasing: 'swing',
                            scrollDelay: 0,
                            timer: '00:00',
                            highlight: true,
                            keepHighlighted: false,
                            keepVisible: false,// new 2.2.0
                            onShowStep: function (ui) { },
                            onHideStep: function (ui) { }
                        },

                        {
                            hookTo: '#btnWatchVideoHolder',
                            content: '#step-eleven',
                            width: 300,
                            position: 'bm',
                            offsetY: 20,
                            offsetX: -67,
                            fxIn: 'fadeIn',
                            fxOut: 'fadeOut',
                            showStepDelay: 100,
                            center: 'hook',
                            scrollSpeed: 400,
                            scrollEasing: 'swing',
                            scrollDelay: 0,
                            timer: '00:00',
                            highlight: false,
                            keepHighlighted: false,
                            keepVisible: false,// new 2.2.0
                            onShowStep: function (ui) {

                                // fadd fx class to buy button
                                //  $('#ancStartTour > .btn').addClass('colorfadingbutton');

                            },
                            onHideStep: function (ui) {

                                // remove fx class from buy button
                                // $('#ancStartTour > .btn').removeClass('colorfadingbutton');

                            }
                        },
                            ////////////////////


                        {
                            hookTo: '#channelInfoHolder',
                            content: '#step-thirteen',
                            width: 300,
                            position: 'bl',
                            offsetY: -110,
                            offsetX: 540,
                            fxIn: 'fadeIn',
                            fxOut: 'fadeOut',
                            showStepDelay: 100,
                            center: 'hook',
                            scrollSpeed: 400,
                            scrollEasing: 'swing',
                            scrollDelay: 0,
                            timer: '00:00',
                            highlight: false,
                            keepHighlighted: false,
                            keepVisible: false,// new 2.2.0
                            onShowStep: function (ui) {

                                // fadd fx class to buy button
                                //  $('#ancStartTour > .btn').addClass('colorfadingbutton');

                            },
                            onHideStep: function (ui) {


                            }
                        },

                        {
                            hookTo: '#channelInfoHolder',
                            content: '#step-fourteen',
                            width: 300,
                            position: 'bl',
                            offsetY: -50,
                            offsetX: 202,
                            fxIn: 'fadeIn',
                            fxOut: 'fadeOut',
                            showStepDelay: 100,
                            center: 'hook',
                            scrollSpeed: 400,
                            scrollEasing: 'swing',
                            scrollDelay: 0,
                            timer: '00:00',
                            highlight: false,
                            keepHighlighted: false,
                            keepVisible: false,// new 2.2.0
                            onShowStep: function (ui) {

                                //show the helper images(demo so not needed)


                            },
                            onHideStep: function (ui) {

                                //hide the helper images(demo so not needed)


                            }
                        },

                        {
                            hookTo: '#divAddVideosMenuHolder',
                            content: '#step-fifveteen',
                            width: 300,
                            position: 'ml',
                            offsetY: 55,
                            offsetX: -300,
                            fxIn: 'fadeIn',
                            fxOut: 'fadeOut',
                            showStepDelay: 100,
                            center: 'hook',
                            scrollSpeed: 400,
                            scrollEasing: 'swing',
                            scrollDelay: 0,
                            timer: '00:00',
                            highlight: false,
                            keepHighlighted: false,
                            keepVisible: false,// new 2.2.0 
                            onShowStep: function (ui) {

                                //$('#hook-four').delay(1000).fadeTo(500, 0.5);

                                //show the helper images(demo so not needed)


                            },
                            onHideStep: function (ui) {



                            }
                        },
                        {
                            hookTo: '.leftSortActionHolder',
                            content: '#step-sixteen',
                            width: 300,
                            position: 'ml',
                            offsetY: -10,
                            offsetX: -650,
                            fxIn: 'fadeIn',
                            fxOut: 'fadeOut',
                            showStepDelay: 100,
                            center: 'hook',
                            scrollSpeed: 400,
                            scrollEasing: 'swing',
                            scrollDelay: 0,
                            timer: '00:00',
                            highlight: false,
                            keepHighlighted: false,
                            keepVisible: false,// new 2.2.0
                            onShowStep: function (ui) {



                            },
                            onHideStep: function (ui) {


                            }
                        },
                        {
                            hookTo: '#calendarHolder',
                            content: '#step-seventeen',
                            width: 300,
                            position: 'ml',
                            offsetY: 0,
                            offsetX: 10,
                            fxIn: 'fadeIn',
                            fxOut: 'fadeOut',
                            showStepDelay: 100,
                            center: 'hook',
                            scrollSpeed: 400,
                            scrollEasing: 'swing',
                            scrollDelay: 0,
                            timer: '00:00',
                            highlight: false,
                            keepHighlighted: false,
                            keepVisible: false,// new 2.2.0
                            onShowStep: function (ui) {



                            },
                            onHideStep: function (ui) {


                            }
                        },
                        {
                            hookTo: '#calendarHolder',
                            content: '#step-eighteen',
                            width: 300,
                            position: 'ml',
                            offsetY: 0,
                            offsetX: 10,
                            fxIn: 'fadeIn',
                            fxOut: 'fadeOut',
                            showStepDelay: 100,
                            center: 'step',
                            scrollSpeed: 400,
                            scrollEasing: 'swing',
                            scrollDelay: 0,
                            timer: '00:00',
                            highlight: false,
                            keepHighlighted: true,
                            keepVisible: false,// new 2.2.0
                            onShowStep: function (ui) { },
                            onHideStep: function (ui) { }
                        },
                            {
                                hookTo: '#newSchedulePopup',
                                content: '#step-nineteen',
                                width: 300,
                                position: 'ml',
                                offsetY: 0,
                                offsetX: 10,
                                fxIn: 'fadeIn',
                                fxOut: 'fadeOut',
                                showStepDelay: 100,
                                center: 'step',
                                scrollSpeed: 400,
                                scrollEasing: 'swing',
                                scrollDelay: 0,
                                timer: '00:00',
                                highlight: false,
                                keepHighlighted: true,
                                keepVisible: false,// new 2.2.0
                                onShowStep: function (ui) { },
                                onHideStep: function (ui) { }
                            },
                        {
                            hookTo: '#newSchedulePopup',
                            content: '#step-twenty',
                            width: 300,
                            position: 'ml',
                            offsetY: 0,
                            offsetX: 10,
                            fxIn: 'fadeIn',
                            fxOut: 'fadeOut',
                            showStepDelay: 100,
                            center: 'step',
                            scrollSpeed: 400,
                            scrollEasing: 'swing',
                            scrollDelay: 0,
                            timer: '00:00',
                            highlight: false,
                            keepHighlighted: true,
                            keepVisible: false,// new 2.2.0
                            onShowStep: function (ui) { },
                            onHideStep: function (ui) { }
                        },
                        {
                            hookTo: '#newScheduleActions',
                            content: '#step-twentyOne',
                            width: 250,
                            position: 'ml',
                            offsetY: 30,
                            offsetX: -380,
                            fxIn: 'fadeIn',
                            fxOut: 'fadeOut',
                            showStepDelay: 100,
                            center: 'step',
                            scrollSpeed: 400,
                            scrollEasing: 'swing',
                            scrollDelay: 0,
                            timer: '00:00',
                            highlight: false,
                            keepHighlighted: true,
                            keepVisible: false,// new 2.2.0
                            onShowStep: function (ui) { },
                            onHideStep: function (ui) { }
                        },

            {
                hookTo: '#newScheduleActions',
                content: '#step-twentyTwo',
                width: 250,
                position: 'ml',
                offsetY: 30,
                offsetX: -150,
                fxIn: 'fadeIn',
                fxOut: 'fadeOut',
                showStepDelay: 100,
                center: 'step',
                scrollSpeed: 400,
                scrollEasing: 'swing',
                scrollDelay: 0,
                timer: '00:00',
                highlight: false,
                keepHighlighted: true,
                keepVisible: false,// new 2.2.0
                onShowStep: function (ui) { },
                onHideStep: function (ui) { }
            },
                        {
                            //hookTo: '',//not needed
                            content: '#step-twentyThree',
                            width: 350,
                            position: 'sc',
                            offsetY: -80,
                            offsetX: -50,
                            fxIn: 'fadeIn',
                            fxOut: 'fadeOut',
                            showStepDelay: 0,
                            center: 'step',
                            scrollSpeed: 400,
                            scrollEasing: 'swing',
                            scrollDelay: 0,
                            timer: '00:00',
                            highlight: true,
                            keepHighlighted: false,
                            keepVisible: false,// new 2.2.0
                            onShowStep: function (ui) { },
                            onHideStep: function (ui) { }
                        }
                ],
                stepDefaults: [
                       {
                           width: 300,
                           position: 'tr',
                           offsetY: 0,
                           offsetX: 0,
                           fxIn: 'fadeIn',
                           fxOut: 'fadeOut',
                           showStepDelay: 0,
                           center: 'step',
                           scrollSpeed: 400,
                           scrollEasing: 'swing',
                           scrollDelay: 0,
                           timer: '00:00',
                           highlight: true,
                           keepHighlighted: false,
                           keepVisible: false,// new 2.2.0
                           onShowStep: function (ui) { },
                           onHideStep: function (ui) { }
                       }
                ]
            }
        ]
    });
    //console.log("isfirstChannel" + isfirstChannel);
    if (isfirstChannel == true) {
        void 0
        $.powerTour('run', 1);
    }
};





function showSearchControls() {
    isBasicMode = getIsBasicMode();

    if (isBasicMode == false) {
        $(".leftSortingHolder").hide();
        $(".leftClearHolder").hide();
    }

    $(".iconSearch").hide();
    $(".sortBy").hide();

    $(".closeSearchHolder").show();
    $("#divSearchVideobyKeywordForChannelHolder").show();
}

function hideSearchControls() {
    if (isBasicMode == false) {
        $(".leftSortingHolder").show();
        $(".leftClearHolder").show();
    }

    $(".iconSearch").show();
    $(".sortBy").show();

    $(".closeSearchHolder").hide();
    $("#divSearchVideobyKeywordForChannelHolder").hide();

    if (!isInSearchModeOnStudioPage) {
        $('#txtSearchVideoByKeywordForChannel').val('');
    }
    else {
        ClearSearchedVideosForChannel();
    }
}

function getIsBasicMode() {
    var cookievalue = getCookie(isBasicModeCookieName);
    var isBasic = false;

    if (cookievalue == undefined) {
        isBasic = false;
    }
    else {
        isBasic = cookievalue == 'true';
    }

    return isBasic;
}

function setWorkingMode(isReload) {

    isBasicMode = getIsBasicMode();

    if (isReload) {
        isBasicMode = !isBasicMode;
    }

    if (isBasicMode == true) {

        isBasicMode = false;
        setCookie(isBasicModeCookieName, isBasicMode, (new Date(2030, 1, 1)));

        //if (!$("#divSearchVideobyKeywordForChannelHolder").is(":visible")) {
        //    $(".leftClearHolder").show();
        //    $(".leftSortingHolder ").show();
        //}

        $(".leftClearHolder").show();
        $(".leftSortingHolder ").show();
        $(".iconSearch").show();

        $("#divSearchVideobyKeywordForChannelHolder").hide();

        $(".dhtmlxcalendar_dhx_skyblue").show();
        $('#customSchedule').show();
        $('#customScheduleM').show();
        $('.spnADdVideoPopup, .bntBasicInstantSchedule').removeClass('bntBasicInstantScheduleWide');
        $(".ancRepeat").show();
        $('#toggleAdvancedMode').removeClass('inputAdvancedWrapperOFF').addClass('inputAdvancedWrapper');
        $('#toggleAdvancedModeM').removeClass('inputAdvancedWrapperOFF').addClass('inputAdvancedWrapper');
    }
    else {
        isBasicMode = true;
        setCookie(isBasicModeCookieName, isBasicMode, (new Date(2030, 1, 1)));

        $("#divSearchVideobyKeywordForChannelHolder").hide();
        $(".iconSearch").hide();

        // JIRA 157, if user switches to basic mode while in search mode
        // application should clear search and reload the page.
        var keywords = $('#txtSearchVideoByKeywordForChannel').val();
        if (keywords != null && keywords != '') {
            $('#txtSearchVideoByKeywordForChannel').val('');
            LoadMoreVideos(true);
        }

        $(".sortBy").show();
        $(".closeSearchHolder").hide();
        $(".leftClearHolder").hide();
        $(".leftSortingHolder ").hide();
        $(".dhtmlxcalendar_dhx_skyblue").hide();
        $('#customSchedule').hide();
        $('#customScheduleM').hide();
        $('.spnADdVideoPopup, .bntBasicInstantSchedule').addClass('bntBasicInstantScheduleWide');
        $(".ancRepeat").hide();
        $('#toggleAdvancedMode').removeClass('inputAdvancedWrapper').addClass('inputAdvancedWrapperOFF');
        $('#toggleAdvancedModeM').removeClass('inputAdvancedWrapper').addClass('inputAdvancedWrapperOFF');
    }

    if (isBasicMode == true) {
        $("#btnStartTourHolderPS #ancStartTour").show();
        $("#btnStartTourHolderPS #advancedTour").hide();
        $(".addButtonHolder").hide();
    }
    else {
        $("#btnStartTourHolderPS #ancStartTour").hide();
        $("#btnStartTourHolderPS #advancedTour").show();
        $(".addButtonHolder").show();
    }
}

$(window).unload(function () {
    $.ajax({
        type: "POST",
        url: webMethodClearSession,
        cashe: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
        }
    });
});

function DurationChanged() {
    clearScreen = true;
};

function LoadChannelCategories(categoryId) {
    $.ajax({
        type: "POST",
        url: webMethodGetChannelCategoriesJson,
        cashe: false,
        data: '{"channelId":' + channelId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var cdata = response.d;
            $('#ddlCategory.ddlCategory').html("");
            $.each(cdata, function (i, c) {
                $('#ddlCategory.ddlCategory').append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
            });

            if (categoryId == 'undefined') {
                categoryId = 0;
            }

            $('#ddlCategory.ddlCategory option[value="' + categoryId + '"]').prop('selected', true);
        },
        complete: function (response) {
        }
    });
};

function addPlayer(videoPath) {
    //var stringId = videoPath.id;
    //var idArr = stringId.split("_");
    //var id = idArr[1];
    //var tag = document.createElement('script');

    //tag.src = "https://www.youtube.com/iframe_api";
    //var firstScriptTag = document.getElementsByTagName('script')[0];
    //firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
    //console.log(videoPath.id);
    // 3. This function creates an <iframe> (and YouTube player)
    //    after the API code downloads.

    void 0;
    function onYouTubeIframeAPIReady() {
        player = new YT.Player('related', {
            height: '450',
            width: '800',

            videoId: videoPath.id,
            playerVars: {
                //controls: 0,
                // showinfo: 0,
                autoplay: 1,
                html5: 1

            },
            events: {
                'onReady': onPlayerReady,
                'onStateChange': onPlayerStateChange
            }
        });

        //console.log("onYouTubeIframeAPIReady");
    }


    onYouTubeIframeAPIReady();


    // 4. The API will call this function when the video player is ready.
    function onPlayerReady(event) {
        event.target.playVideo();
    }

    // 5. The API calls this function when the player's state changes.
    //    The function indicates that when playing a video (state=1),
    //    the player should play for six seconds and then stop.
    var done = false;
    function onPlayerStateChange(event) {

        if (event.data == YT.PlayerState.PLAYING && !done) {
            // setTimeout(stopVideo, 6000);

            done = true;
            ////console.log(done);
        }
    }

};




function ShowRepeatBox() {
    $(".repeatBox").lightbox_me({
        centered: true,
        onLoad: function () {
            $('#txtReapeatDate').datepicker({ minDate: 0 });
        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {
            RemoveOverlay();
        }
    });

};

function updateVideoSchedule(channelScheduleId) {
    $("#loadingDiv").show();
    $.ajax({

        type: "POST",
        url: webMethodUpdateVideoSchedule,
        data: '{"channelScheduleId":' + channelScheduleId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            $(".divDate").hide();
            $(".divDateMonthHolder").show();
            if (response.d == "0") {

                alertify.alert("A schedule is already set for this time, please choose another time or edit your schedule.");
            }
            else {
                alertify.success("Schedule has been updated.");
                window.location.href = "schedule";
            }
        },
        complete: function () {
            $("#loadingDiv").hide();
        }
    });

};

function GetVideoByGategory() {
    categoryId = $(".ddlCategory option:selected").val();
    pageIndex = 1;
    $(".videoBoxHolder").empty();
    LoadMoreVideos();
}

function AddNoVideosMessage() {
    if ($("#novideosmsg:visible").length == 0) {
        $(".videoBoxHolder").append("<span id='novideosmsg'>Welcome to your Production Studio! Click on “Watch Tutorial” or “Take a Tour” on top of the page to learn how to operate it. For more details please visit How To page.</span>");
    }
}

function RemoveNoVideosMessage() {
    $("#novideosmsg").hide();
}
function CheckIfInputEmpty() {


    if ($("searchHolder #txtKeyword").val().length != 0) {
        $('.clearSearch').show();
    }
    else {
        $('.clearSearch').hide();
    }

}

var isInSearchModeOnStudioPage = false;

function LoadMoreVideos(isSearching) {
    $("#loadingDiv").show();

    if ($("searchHolder #txtKeyword").val().length != 0) {
        $('.clearSearch').show();
    }
    else {
        $('.clearSearch').hide();
    }

}

var isInSearchModeOnStudioPage = false;

function LoadMoreVideos(isSearching) {
    // console.log("here")
    //console.log(isSearching);
    $("#loadingDiv").show();
    var categoryId = $("#ddlCategory option:selected").val();
    var keywords = $('#txtSearchVideoByKeywordForChannel').val();

    if (isSearching) {
        isInSearchModeOnStudioPage = true;
        pageIndex = 1;
        $(".videoBoxHolder").html("");
    }
    else {
        isInSearchModeOnStudioPage = false;
    }

    if (categoryId == null) {
        categoryId = 0;
    }

    var searchCriteria = {
        PageIndex: pageIndex,
        CategoryId: categoryId,
        ChannelTubeId: channelId,
        Keywords: keywords
    }

    $.ajax({
        type: "POST",
        url: webMethodGetVideoTubeModelForChannelAndCategoryByPage,
        cashe: false,
        data: "{'searchCriteria':" + JSON.stringify(searchCriteria) + "}",
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            debugger;
            if (response.d != null) {
                var data = response.d;
                pageIndex = data.NextPageIndex;
                prevPageIndex = data.PrevPageIndex;
                nextPageIndex = data.NextPageIndex;
                var pageSize = data.PageSize;
                var pageCount = data.PageCount;
                // console.log(response.d);
                $("#ancLoadMore").hide();
                if (data.VideoTubeModels == undefined || data.VideoTubeModels.length == 0) {
                    AddNoVideosMessage();
                }
                else {
                    if (data.PageIndex < pageCount) {
                        $("#ancLoadMore").show();
                    }
                }

                if (data.VideoTubeModels) {
                    if (isSearching) {
                        $(".videoBoxHolder").empty();
                    }
                    var videos = data.VideoTubeModels;
                    checkValidRokuVideos(videos);
                    var pageType = "schedule";
                    var videoControls = Controls.BuildVideoBoxControlForSchedulePage(videos, false);
                    $(".videoBoxHolder").append(videoControls);

                    // hide all add buttons on video boxes in basic mode
                    // class='addButtonHolder'
                    var isBasicMode = getIsBasicMode();
                    if (isBasicMode) {
                        $(".addButtonHolder").hide();
                    }
                    

                    $("#loadingDiv").hide();
                }

            }
            else {
                $("#ancLoadMore").removeAttr("onclick").text("").text("no more videos");
                $("#loadingDiv").hide();
            }

            //var $leftScheduleContainer = $(".leftSchedule");
            //var leftScheduleHeight = $leftScheduleContainer.height();
            //var leftSchedulePosition = $leftScheduleContainer.position();
            //var $scheduleContainer = $(".right");
            //var scheduleContainerPosition = $scheduleContainer.position();
            //var newScheduleContainerHeight = leftScheduleHeight - (scheduleContainerPosition.top - leftSchedulePosition.top);
            //var $window = $(window).on('resize', function () {
            //    $scheduleContainer.height(newScheduleContainerHeight);
            //}).trigger('resize'); //on page load

        },
        complete: function (response) {
            $("#loadingDiv").hide();
        }
    });


};

function checkValidRokuVideos(videos) {
    hasHLSVideosOnly = true;
    hasValidRokuVideos = true;
    for (var i = 0; i < videos.length; i++) {
        if (videos[i].VideoProviderName == "youtube" || videos[i].VideoProviderName == "dailymotion") {
            hasValidRokuVideos = false;
            hasHLSVideosOnly = false;
            return;
        }
    }

}

function showInfo() {
    $('.showInfoDiv').lightbox_me({
        centered: true,
        onClose: function () {
            RemoveOverlay();
        }
    });
}

function showDragPopup() {
    $('#modalDragInfo').lightbox_me({
        centered: true,
        onLoad: function () {
            
        },
        onClose: function () {
            RemoveOverlay();
        }
    });
}

function ShowCalendarPicker() {
    pickedDateTime = new Date();
    $("#calendarPicker").show();
    myCalendar = new dhtmlXCalendarObject("calendarPicker");
    myCalendar.showTime();
    myCalendar.show();

    var date = new Date();
    var curr_date = date.getDate();
    var curr_month = date.getMonth() + 1;
    var curr_year = date.getFullYear();

    if (curr_date < 10) curr_date = "0" + curr_date;
    if (curr_month < 10) curr_month = "0" + curr_month;

    var nowDate = curr_year + "-" + curr_month + "-" + curr_date;
    $("#calendarPicker .dhtmlxcalendar_time_cont ul.dhtmlxcalendar_line li").append("<a class='ancConfirmDateTime' onclick='CreateNewChannelSchedule()'>ok</a");
    $("#calendarPicker .dhtmlxcalendar_time_cont ul.dhtmlxcalendar_line li").append("<a class='ancCloseCalendar' onclick='CloseCalendar()'>close</a");
    myCalendar.setDate(nowDate);
    pickedDateTime = myCalendar.getDate();

    myCalendar.attachEvent("onChange", function (d, s) {
        pickedDateTime = d;
    });

    myCalendar.attachEvent("onClick", function (d) {
        pickedDateTime = d;
    });
};

function CloseCalendar() {
    $("#calendarPicker").hide();
    myCalendar = new dhtmlXCalendarObject("calendarPicker");
    myCalendar.hide();

    $("#repeatCalendarPickerHolder").hide();
    myCalendar = new dhtmlXCalendarObject("repeatCalendarPicker");

    myCalendar.hide();
};

function CreateNewChannelSchedule() {
    $("#loadingDiv").show();

    var curr_date = pickedDateTime.getDate();
    var curr_month = pickedDateTime.getMonth();
    var curr_year = pickedDateTime.getFullYear();
    var minutes = pickedDateTime.getMinutes();

    if (minutes == 0) {
        minutes = "00";
    }

    if (minutes.length == 1) {
        minutes += minutes + "0";
    }

    var now = new Date();
    now.setMonth(pickedDateTime.getMonth());
    now.setDate(pickedDateTime.getDate());
    now.setYear(pickedDateTime.getFullYear());
    var formatedDate = "Schdedule for " + GetFormattedScheduleDate(now);
    var scheduleDate = moment(pickedDateTime).format('MM/DD/YYYY HH:mm');// myCalendar.getFormatedDate("%m/%d/%Y %H:%i", pickedDateTime);// pickedDateTime.format("MM/dd/yyyy HH:mm"); //

    //add channelScheduleToDB
    void 0;
    $.ajax({
        type: "POST",
        url: webMethodAddChannelSchedule,
        cashe: false,
        data: '{"channelTubeId":' + channelId + ',"scheduleDate":' + "'" + scheduleDate + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            CloseCalendar();

            todaysChannelTubes = response.d;

            if (todaysChannelTubes) {
                if (todaysChannelTubes.length == 1 && todaysChannelTubes[0].ChannelScheduleId == 0) {
                    alertify.error(todaysChannelTubes[0].Message);
                    return;
                }

                $(".scheduleThumbHolder").html("");
                var idsOfScheduleToExpand = new Array();
                $.each(todaysChannelTubes, function (i, d) {
                    var dayschedule = Controls.BuildChannelScheduleWithVideoForEdit(d);
                    var expandVideoSchedulesList = d.ExpandVideoSchedulesList;
                    if (expandVideoSchedulesList) {
                        idsOfScheduleToExpand[0] = d.ChannelScheduleId;
                    }

                    $(".scheduleThumbHolder").append(dayschedule);

                    if (isBasicMode) {
                        $(".ancRepeat").toggle();
                    }

                    var isEmptySchedule = d.EndTime == "";
                    DisablePublishButton(d.ChannelScheduleId, isEmptySchedule);
                    ExpandVideoScheduleList(d.ChannelScheduleId);
                    activeChannelScheduleId = d.ChannelScheduleId;
                    //DisableRepeatButtonsOnChannelSchedules(isAutoPilotOn)
                });

                alertify.success("Schedule start time has been set, please add videos and click PUBLISH to activate it.");
                //if (idsOfScheduleToExpand.length > 0) {
                //    $.each(idsOfScheduleToExpand, function (i, d) {
                //        ExpandVideoScheduleList(d);
                //    });
                //}
            }

            UpdateAvailableScheduleTimes();
        },
        complete: function (response) {
            curr_month += 1;
            LoadChannelScheduleCalendarEvents(curr_month, curr_year);
            $("#loadingDiv").hide();
        }
    });

};

function addVideoScedule(btn) {
    void 0
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var videoTubeId = idArr[1];
    var videoMatureContentEnabled = $("#boxContent_" + videoTubeId).attr("data-maturecontent");
    var isPrivateVideo = $("#boxContent_" + videoTubeId).attr("data-isprivate");
    void 0;
    if (videoMatureContentEnabled == "true") {
        if (!matureChannelContentEnabled) {
            alertify.set({
                'labels': { ok: 'Ok' },
                'defaultFocus': 'ok'
            });
            alertify.alert("channel does not allow mature content");
            return;
        }
    }
    if (isPrivateVideo == "true") {
        if (!showPrivateVideoMode) {
            alertify.set({
                'labels': { ok: 'Ok' },
                'defaultFocus': 'ok'
            });
            alertify.alert("channel does not allow private videos, please allow private videos in channel settings.");
            return;
        }
    }
    // console.log(videoMatureContentEnabled);
    if (activeChannelScheduleId != 0) {
        $("#loadingDiv").show();
        $.ajax({
            type: "POST",
            url: webMethodAddVideoToChannelSchedule,
            cashe: false,
            data: '{"channelScheduleId":' + activeChannelScheduleId + ',"videoTubeId":' + videoTubeId + '}',
            dataType: "json",
            contentType: "application/json",
            success: function (response) {
                var day =moment(pickedDateTime).format("MM.DD.YY");


                todaysChannelTubes = response.d;

                if (todaysChannelTubes) {
                    if (todaysChannelTubes.length == 1 && todaysChannelTubes[0].ChannelScheduleId == 0) {
                        alertify.error(todaysChannelTubes[0].Message);

                    }
                    else {
                        var addedDateHTML = "<span class='recentlyadded'>added to " + day + "</span>";
                        $("#boxContent_" + videoTubeId + " .recentlyadded").remove();
                        $("#action_" + videoTubeId + " .VideoBoxPlay").after(addedDateHTML);
                        $(".scheduleThumbHolder").html("");
                        var idsOfScheduleToExpand = new Array();

                        $.each(todaysChannelTubes, function (i, d) {
                            var dayschedule = Controls.BuildChannelScheduleWithVideoForEdit(d);
                            var expandVideoSchedulesList = d.ExpandVideoSchedulesList;
                            if (expandVideoSchedulesList) {
                                idsOfScheduleToExpand[0] = d.ChannelScheduleId;
                            }

                            $(".scheduleThumbHolder").append(dayschedule);

                            if (isBasicMode) {
                                $(".ancRepeat").toggle();
                            }

                            ExpandVideoScheduleList(d.ChannelScheduleId);
                            activeChannelScheduleId = d.ChannelScheduleId;
                            //DisableRepeatButtonsOnChannelSchedules(isAutoPilotOn);
                        });

                        //if (idsOfScheduleToExpand.length > 0) {
                        //    $.each(idsOfScheduleToExpand, function (i, d) {
                        //        ExpandVideoScheduleList(d);
                        //    });
                        //}

                        //// Disable instant schedule for now, since we already created instant schedule for now
                        //// If the new time will become available for schedule creation, re-enable it then
                        //DisableInstantScheduleButton(true);

                        // Do not need to disable "new schedule" button, since
                        // timepicker will have times not available for scheduling already disabled for the time when
                        // schedule already exists.

                        UpdateAvailableScheduleTimes();
                    }


                }
            },
            complete: function (response) {

                $("#recentlyadded_" + videoTubeId).show();

                // $("#loadingDiv").hide();
            }
        });
    }
    else {
        alertify.set({
            'labels': { ok: 'Ok' },
            'defaultFocus': 'ok'
        });
        alertify.alert("Create a custom schedule first by clicking on the 'Custom Schedule' button and try adding this video again.");
    }

};

function addVideoSceduleWithOrderNumber(btn, prevOrderNumber) {
    void 0
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var videoTubeId = idArr[1];
    var videoMatureContentEnabled = $("#boxContent_" + videoTubeId).attr("data-maturecontent");
    var isPrivateVideo = $("#boxContent_" + videoTubeId).attr("data-isprivate");
    void 0;
    if (videoMatureContentEnabled == "true") {
        if (!matureChannelContentEnabled) {
            alertify.set({
                'labels': { ok: 'Ok' },
                'defaultFocus': 'ok'
            });
            alertify.alert("channel does not allow mature content");
            return;
        }
    }
    if (isPrivateVideo == "true") {
        if (!showPrivateVideoMode) {
            alertify.set({
                'labels': { ok: 'Ok' },
                'defaultFocus': 'ok'
            });
            alertify.alert("channel does not allow private videos, please allow private videos in channel settings.");
            return;
        }
    }
    // console.log(videoMatureContentEnabled);
    if (activeChannelScheduleId != 0) {
        $("#loadingDiv").show();
        $.ajax({
            type: "POST",
            url: webMethodAddVideoToChannelScheduleWithOrderNumber,
            cashe: false,
            data: '{"channelScheduleId":' + activeChannelScheduleId + ',"videoTubeId":' + videoTubeId + '}',
            dataType: "json",
            contentType: "application/json",
            success: function (response) {
                var day = moment(pickedDateTime).format("MM.DD.YY");


                todaysChannelTubes = response.d;

                if (todaysChannelTubes) {
                    if (todaysChannelTubes.length == 1 && todaysChannelTubes[0].ChannelScheduleId == 0) {
                        alertify.error(todaysChannelTubes[0].Message);

                    }
                    else {
                        var addedDateHTML = "<span class='recentlyadded'>added to " + day + "</span>";
                        $("#boxContent_" + videoTubeId + " .recentlyadded").remove();
                        $("#action_" + videoTubeId + " .VideoBoxPlay").after(addedDateHTML);
                        $(".scheduleThumbHolder").html("");
                        var idsOfScheduleToExpand = new Array();

                        $.each(todaysChannelTubes, function (i, d) {
                            var dayschedule = Controls.BuildChannelScheduleWithVideoForEdit(d);
                            var expandVideoSchedulesList = d.ExpandVideoSchedulesList;
                            if (expandVideoSchedulesList) {
                                idsOfScheduleToExpand[0] = d.ChannelScheduleId;
                            }

                            $(".scheduleThumbHolder").append(dayschedule);

                            if (isBasicMode) {
                                $(".ancRepeat").toggle();
                            }

                            ExpandVideoScheduleList(d.ChannelScheduleId);
                            activeChannelScheduleId = d.ChannelScheduleId;
                            //DisableRepeatButtonsOnChannelSchedules(isAutoPilotOn);
                        });

                        //if (idsOfScheduleToExpand.length > 0) {
                        //    $.each(idsOfScheduleToExpand, function (i, d) {
                        //        ExpandVideoScheduleList(d);
                        //    });
                        //}

                        //// Disable instant schedule for now, since we already created instant schedule for now
                        //// If the new time will become available for schedule creation, re-enable it then
                        //DisableInstantScheduleButton(true);

                        // Do not need to disable "new schedule" button, since
                        // timepicker will have times not available for scheduling already disabled for the time when
                        // schedule already exists.

                        UpdateAvailableScheduleTimes();
                    }


                }
            },
            complete: function (response) {

                $("#recentlyadded_" + videoTubeId).show();

                // $("#loadingDiv").hide();
            }
        });
    }
    else {
        alertify.set({
            'labels': { ok: 'Ok' },
            'defaultFocus': 'ok'
        });
        alertify.alert("Create a custom schedule first by clicking on the 'Custom Schedule' button and try adding this video again.");
    }

};

function removeVideoSchedule(btn) {
    void 0;
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var videoTubeIdPlaybackOrderNumberArr = idArr[1].split("|");
    var videoTubeId = videoTubeIdPlaybackOrderNumberArr[0];
    var playbackOrderNumber = videoTubeIdPlaybackOrderNumberArr[1];
    var activeChannelScheduleId = videoTubeIdPlaybackOrderNumberArr[2];

    $.ajax({
        type: "POST",
        url: webMethodRemoveVideoSchedule,
        data: '{"channelScheduleId":' + activeChannelScheduleId + ',"videoTubeId":' + videoTubeId + ',"playbackOrderNumber":' + playbackOrderNumber + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            todaysChannelTubes = response.d;

            if (todaysChannelTubes) {
                if (todaysChannelTubes.length == 1 && todaysChannelTubes[0].ChannelScheduleId == 0) {
                    alertify.error(todaysChannelTubes[0].Message);
                    return;
                }

                $(".scheduleThumbHolder").html("");
                var idsOfScheduleToExpand = new Array();
                $.each(todaysChannelTubes, function (i, d) {
                    var dayschedule = Controls.BuildChannelScheduleWithVideoForEdit(d);
                    var expandVideoSchedulesList = d.ExpandVideoSchedulesList;
                    if (expandVideoSchedulesList) {
                        idsOfScheduleToExpand[0] = d.ChannelScheduleId;
                    }

                    $(".scheduleThumbHolder").append(dayschedule);

                    if (isBasicMode) {
                        $(".ancRepeat").toggle();
                    }

                    activeChannelScheduleId = videoTubeIdPlaybackOrderNumberArr[2];

                    ExpandVideoScheduleList(d.ChannelScheduleId);
                    //DisableRepeatButtonsOnChannelSchedules(isAutoPilotOn);
                });

                //if (idsOfScheduleToExpand.length > 0) {
                //    $.each(idsOfScheduleToExpand, function (i, d) {
                //        ExpandVideoScheduleList(d);
                //    });
                //}

                // Disable instant schedule for now, since we already created instant schedule for now
                // If the new time will become available for schedule creation, re-enable it then
                DisableInstantScheduleButton(true);

                // Do not need to disable "new schedule" button, since
                // timepicker will have times not available for scheduling already disabled for the time when
                // schedule already exists.
                UpdateAvailableScheduleTimes();
            }
        },
        complete: function (response) {
            $("#recentlyadded_" + videoTubeId).hide();
        },
        error: function (response) {
            //console.log(response.status + " " + response.statusText);
        }

    });


};

function formatAMPM(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12;
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
};

function CreateInstantSchedule() {
    if (isMobileDevice()) {
        ToggleScheduleMobile();
    }
    var hasEmptySchedules = false;

    if (todaysChannelTubes && todaysChannelTubes.length > 0) {
        $.each(todaysChannelTubes, function (i, c) {
            if (c.EndTime == "") {
                hasEmptySchedules = true;
            }
        });
    }

    if (hasEmptySchedules) {
        alertify.set({
            labels: {
                ok: "Yes",
                cancel: "No"
            },
            buttonFocus: "ok"
        });
        alertify.confirm("Proceeding with the requested action will remove your empty schedule. Do you want to proceed?", function (e) {
            if (e) {
                CreateSchedule();
            }
        });
    }
    else {
        CreateSchedule();
    }
};

function CreateSchedule() {
    $("#loadingDiv").show();
    var hasMinVideosInChannel = IsMinimumVideosInChannels();

    if (!hasMinVideosInChannel) {
        alertify.error("There should be at least 3 different and non-restricted videos in your list in order to create an Instant Schedule or use Autopilot.");
    }
    else {
        var curr_month = pickedDateTime.getMonth();
        var curr_year = pickedDateTime.getFullYear();

        var now = new Date();
        var scheduleDate = moment(pickedDateTime).format('MM/DD/YYYY HH:mm');

        var diff = pickedDateTime.getTime() - now.getTime();
        if (diff > 0 && now.getDate() != pickedDateTime.getDate() && now.getYear() <= pickedDateTime.getYear()) {
            scheduleDate = moment(pickedDateTime).format("MM/DD/YYYY") + ' 00:00';
        }
        void 0;

        $("#loadingDiv").show();

        $.ajax({
            type: "POST",
            url: webMethodCreateInstantSchedule,
            data: '{"channelTubeId":' + channelId + ',"clientDateAndTime":"' + scheduleDate + '"}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {

                todaysChannelTubes = response.d;
                void 0
                if (todaysChannelTubes) {
                    if (todaysChannelTubes.length == 1 && todaysChannelTubes[0].ChannelScheduleId == 0) {
                        alertify.error(todaysChannelTubes[0].Message);
                        return;
                    }

                    $(".scheduleThumbHolder").html("");
                    var idsOfScheduleToExpand = new Array();
                    $.each(todaysChannelTubes, function (i, d) {
                        var dayschedule = Controls.BuildChannelScheduleWithVideoForEdit(d);
                        var expandVideoSchedulesList = d.ExpandVideoSchedulesList;
                        if (expandVideoSchedulesList) {
                            idsOfScheduleToExpand[0] = d.ChannelScheduleId;
                        }

                        $(".scheduleThumbHolder").append(dayschedule);
                        Publish(d.ChannelScheduleId);
                      
                        if (isBasicMode) {
                            $(".ancRepeat").toggle();
                            if (mobile) {
                                ToggleScheduleMobile();
                            }
                            //alertify.set({
                            //    labels: {
                            //        ok: "Yes",
                            //        cancel: "No"
                            //    },
                            //    buttonFocus: "ok"
                            //});

                            //alertify.confirm("Would you like to PUBLISH your schedule? Please note that once it is published, it cannot be deleted. Click 'Yes' to publish it! Click 'No' if you want to publish it later.", function (e) {
                            //    if (e) {
                            //        Publish(d.ChannelScheduleId);
                            //        if (mobile) {
                            //            ToggleScheduleMobile();
                            //        }
                            //    }
                            //    else {
                            //        if (mobile) {
                            //            ToggleScheduleMobile();
                            //        }
                            //    }
                            //});
                        }

                        ExpandVideoScheduleList(d.ChannelScheduleId);
                        activeChannelScheduleId = 0;
                        //DisableRepeatButtonsOnChannelSchedules(isAutoPilotOn);
                    });

                    //if (idsOfScheduleToExpand.length > 0) {
                    //    $.each(idsOfScheduleToExpand, function (i, d) {
                    //        ExpandVideoScheduleList(d);
                    //    });
                    //}

                    // Disable instant schedule for now, since we already created instant schedule for now
                    // If the new time will become available for schedule creation, re-enable it then
                    DisableInstantScheduleButton(true);

                    // Do not need to disable "new schedule" button, since
                    // timepicker will have times not available for scheduling already disabled for the time when
                    // schedule already exists.
                    if (isMobileDevice()) {
                        var msg = alertify.success('Schedule was successfully created. Click PUBLISH to activate it!');
                        msg.delay(3);


                    }
                    else {
                        alertify.success("Schedule was successfully created. Click PUBLISH to activate it!");
                    }


                    //CheckIfIsPublished();
                }
                else {
                    alertify.error("Instant schedule creation failed, please try again later.");
                }

                UpdateAvailableScheduleTimes();
            },
            complete: function () {
                curr_month += 1;
                LoadChannelScheduleCalendarEvents(curr_month, curr_year);
                $("#loadingDiv").hide();
            },
            error: function (response) {
                //console.log(response.status + " " + response.statusText);
            }
        });
    }
}

dateTimeReviver = function (key, value) {
    var a;
    if (typeof value === 'string') {
        a = /\/Date\((\d*)\)\//.exec(value);
        if (a) {
            return new Date(+a[1]);
        }
    }
    return value;
}

function CancelInstantSchedule(btn) {
    DeleteSchedule(btn);
    $(".scheduleThumbHolder").html("");
}

function ToggleSchedule(btn) {
    var btnId = btn.id;
    var idArr = btnId.split("_");
    ExpandVideoScheduleList(idArr[1]);
    activeChannelScheduleId = idArr[1];
};

function ExpandVideoScheduleList(channelScheduleId) {
    //activeChannelScheduleId = channelScheduleId;

    if ($("#scheduleBoxContentBox_" + channelScheduleId + " .scheduleContent").is(":visible")) {
        $("#scheduleBoxContentBox_" + channelScheduleId + " .scheduleContent").hide();
        $("#ancToggleSchedule_" + channelScheduleId).text("").text("▼");
        //$("#edit_" + channelScheduleId).text("").text("edit");
        //$("#repeat_" + channelScheduleId).attr("onclick", "ShowRepeatCalendar(this)").removeClass("aDisabled");
        //$("#delete_" + channelScheduleId).attr("onclick", "DeleteSchedule(this)").removeClass("aDisabled");

        //DisableNewScheduleButton(false);
        //DisableInstantScheduleButton(false);
    }
    else {
        $("#scheduleBoxContentBox_" + channelScheduleId + " .scheduleContent").show();
        $("#ancToggleSchedule_" + channelScheduleId).text("").text("▲")
        //$("#edit_" + channelScheduleId).text("").text("cancel");
        //$("#repeat_" + channelScheduleId).removeAttr("onclick").addClass("aDisabled");
        //$("#delete_" + channelScheduleId).removeAttr("onclick").addClass("aDisabled");
        //DisableNewScheduleButton(true);
        //DisableInstantScheduleButton(true);
    }
};

function EditSchedule(element) {
    ToggleSchedule(element);
};

function UnPublishSchedule(btn) {
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var channelSchedId = idArr[1];

    $.ajax({
        type: "POST",
        url: webMethodPublishSchedule,
        data: '{"channelScheduleId":' + channelSchedId + ',"shouldPublish":' + false + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var responseModel = response.d;
            if (responseModel.IsSuccess) {
                $("#publish_" + channelSchedId).text("").text("publish").removeAttr("class", "published").addClass("ancPublishSchedule");
                $("#publish_" + channelSchedId).removeAttr("onclick").attr("onclick", "PublishSchedule(this)");
                //$("#scheduleBoxContentBox_" + channelSchedId + " .scheduleContent").hide();
                //$("#ancToggleSchedule_" + channelSchedId).text("").text("▼")

                var month = myCalendarMain.getDate().getMonth() + 1;
                var year = myCalendarMain.getDate().getYear() % 100 + 2000;
                var day = myCalendarMain.getDate().getDay();

                void 0;
                myCalendarMain.setTooltip(pickedDateTime, "");

                LoadChannelScheduleCalendarEvents(month, year);
                LoadChannelTubesSchedulesByDate(channelId, myCalendarMain.getDate());

                alertify.success('Schedule was successfully removed off the air.');
            }
            else {
                alertify.error('Schedule deactivation failed, please try again later.');
            }

            //alert(responseModel.Message);
            GetChannelCategoriesWithCountsWithoutUIUpdate();
        }
    });
}

function PublishSchedule(btn) {
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var channelSchedId = idArr[1];

    var hoursBeforeStart = $("#" + btn.id).attr('data-hoursBeforeStart'); //btn.attributes[3].value
    
    var ContainPrivateVideos = $("#scheduleBoxContentBox_" + channelSchedId).find("[data-isprivate='true']");
    void 0
    var alertMessage;
    if (ContainPrivateVideos.length > 0) {
        alertMessage = "This schedule contains private videos. The TV programs with private videos are ONLY shown on embedded channels and will not broadcast on Strimm";
        
            alertify.set({
                'labels': {
                    ok: 'Ok', cancel: 'Cancel'
                },
                'defaultFocus': 'cancel'
            }); alertify.confirm(alertMessage, function (e) {
                //}); alertify.confirm("Once published, schedule goes ON AIR immediately and cannot be canceled or deleted.", function (e) {
                if (e) {
                    Publish(channelSchedId);
                }
            });
        

    }




    else {
        Publish(channelSchedId);
    }
};

function Publish(channelSchedId) {
    $.ajax({
        type: "POST",
        url: webMethodPublishSchedule,
        data: '{"channelScheduleId":' + channelSchedId + ',"shouldPublish":' + true + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var responseModel = response.d;
            //  console.log(responseModel);
            if (responseModel.IsSuccess) {
                $("#publish_" + channelSchedId).text("").text("published").attr("class", "ancPublishSchedule published");
                $("#publish_" + channelSchedId).removeAttr("onclick").attr("onclick", "UnPublishSchedule(this)");

                var month = myCalendarMain.getDate().getMonth() + 1;
                var year = myCalendarMain.getDate().getYear() % 100 + 2000;

                LoadChannelScheduleCalendarEvents(month, year);
                LoadChannelTubesSchedulesByDate(channelId, myCalendarMain.getDate());
                isSchedulePublished = true;

                alertify.success('Schedule was successfully published.');
            }
            else {
                alertify.error(response.d.Message);
            }

            GetChannelCategoriesWithCountsWithoutUIUpdate();
        },
        complete: function (response) {


        }
    });
}

function ShowRepeatCalendar(btn) {
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var channelSchedId = idArr[1];
    repeatpickedDate = new Date();

    $("#repeatCalendarPickerHolder").show();
    myCalendar = new dhtmlXCalendarObject("repeatCalendarPicker");
    myCalendar.showTime();
    myCalendar.show();

    $('.dhtmlxcalendar_cell dhtmlxcalendar_time_hdr, .dhtmlxcalendar_time_img, .dhtmlxcalendar_label_hours, .dhtmlxcalendar_label_colon, .dhtmlxcalendar_label_minutes').css("visibility", "hidden");
    var date = new Date();
    date.setDate(date.getDate() + 1);

    myCalendar.setSensitiveRange(date, null);
    myCalendar.setInsensitiveDays([pickedDateTime]);

    $("#repeatCalendarPickerHolder .dhtmlxcalendar_time_cont ul.dhtmlxcalendar_line li").append("<a class='ancConfirmDateTime' onclick='RepeatSchedule(" + channelSchedId + ")'>ok</a");
    $("#repeatCalendarPickerHolder .dhtmlxcalendar_time_cont ul.dhtmlxcalendar_line li").append("<a class='ancCloseCalendar' onclick='CloseCalendar()'>close</a");

    myCalendar.setDate(date);
    pickedDateTime = myCalendar.getDate();
    repeatpickedDate = date;

    myCalendar.attachEvent("onChange", function (d, s) {
        repeatpickedDate = d;
    });

    myCalendar.attachEvent("onClick", function (d) {
        repeatpickedDate = d;
    });
};

function SetAutoPilotOn() {
    var hasMinVideosInChannel = IsMinimumVideosInChannels();

    if (!hasMinVideosInChannel) {
        alertify.error("There should be at least 3 different and non-restricted videos in your list in order to create an Instant Schedule or use Autopilot.");
    }
    else {
        $.ajax({
            type: "POST",
            url: webMethodSetAutoPilotForChannel,
            data: '{"channelTubeId":' + channelId + ',"isAutoPilot":' + true + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            complete: function () {
                $("#btnAutopilot").attr("onclick", "SetAutoPilotOff()").attr("class", "checkboxHolderON");

                isautopilot0N = true;

                $("#loadingDiv").hide();
            }
        });
    }
};

function SetAutoPilotOff() {
    $.ajax({
        type: "POST",
        url: webMethodSetAutoPilotForChannel,
        data: '{"channelTubeId":' + channelId + ',"isAutoPilot":' + false + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        complete: function () {
            $("#btnAutopilot").attr("onclick", "SetAutoPilotOn()").removeAttr("class", "checkboxHolderON").attr("class", "checkboxHolderOFF");

            isautopilot0N = false;

            //DisableRepeatButtonsOnChannelSchedules(isAutoPilotOn);
            DisableInstantScheduleButton(false);
            DisableNewScheduleButton(false);

            $("#loadingDiv").hide();
        }
    });
}

function RepeatSchedule(channelSchedId) {
    var curr_date = repeatpickedDate.getDate();
    var curr_month = repeatpickedDate.getMonth() + 1;
    var curr_year = repeatpickedDate.getFullYear();
    var repeateScheduleDate = myCalendar.getFormatedDate("%m/%d/%Y %H:%i", repeatpickedDate);

    $("#loadingDiv").show();

    $.ajax({
        type: "POST",
        url: webMethodRepeatSchedule,
        data: '{"channelScheduleId":' + "'" + channelSchedId + "'" + ',"channelTubeId":' + "'" + channelId + "'" + ',"targetDate":' + "'" + repeateScheduleDate + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d.Message) {
                alertify.error(response.d.Message);
            }
            else {
                alertify.success("Schedule was successfully repeated on " + myCalendar.getFormatedDate("%m/%d/%Y", repeateScheduleDate) + ".");
            }
        },
        complete: function () {
            $("#repeatCalendarPickerHolder").hide();
            LoadChannelScheduleCalendarEvents(curr_month, curr_year);

            $("#loadingDiv").hide();   //  $(".nano").nanoScroller({ alwaysVisible: true });
        }
    });

};

function DeleteSchedule(btn) {

    var btnId = btn.id;
    var idArr = btnId.split("_");
    var channelSchedId = idArr[1];
    scheduleCount--;

    DeleteChannelScheduleById(channelSchedId);
};

function DeleteChannelScheduleById(channelScheduleIdToDelete) {
   
  
    var targetDate = moment(pickedDateTime).format('MM/DD/YYYY HH:mm')
    void 0;
    //get schedule by id
    //define if schedule is published and its more than 24 hours till show
    //show message confirmation delete if false
    //show message than cant delete schedule if true
   // return;
    $.ajax({
        type: "POST",
        url: webMethodGetScheduleByChannelScheduleId,
        data: '{"channelScheduleId":' + channelScheduleIdToDelete + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var re = /-?\d+/;
            var m = re.exec(response.d.StartTime);
            var startTimeSchedule = new Date(parseInt(m[0]));
            var now = new Date();
            var diffHours = Math.abs(startTimeSchedule - now) / 36e5;

            //if (diffHours < 24 && response.d.Published == true) {
            //    reset();
            //    alertify.alert("Schedule cannot be deleted with less than 24 hours left to air time.");
            //    return;
            //}
            //else {
            if (diffHours < 24 && response.d.Published == true) {
                reset();
                alertify.set({
                    'labels': { ok: 'Yes', cancel: 'No' },
                    'defaultFocus': 'cancel'
                });
                alertify.confirm('Are you sure that you want to delete this schedule? Deleting a schedule in the middle of broadcast will effect all viewers in different timezones.', function (e) {
                    if (e) {
                        OnOkDeleteSchedule(channelScheduleIdToDelete, channelId, targetDate)
                    };
                });
            }
            else {
                reset();
                alertify.set({
                    'labels': { ok: 'Yes', cancel: 'No' },
                    'defaultFocus': 'cancel'
                });
                alertify.confirm('Are you sure that you want to delete this schedule? ', function (e) {
                    if (e) {
                        OnOkDeleteSchedule(channelScheduleIdToDelete, channelId, targetDate)
                    };
                });
            }
        }
    });

}

function OnOkDeleteSchedule(channelScheduleIdToDelete, channelId, targetDate) {
   
    $.ajax({
        type: "POST",
        url: webMethodDeleteChannelSchedule,
        data: '{"channelScheduleId":' + channelScheduleIdToDelete + ',"channelTubeId":' + channelId + ', "targetDate":"' + targetDate + '"}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            todaysChannelTubes = response.d;
            void 0;
            $(".scheduleThumbHolder").html("");

            $.each(todaysChannelTubes, function (i, d) {
                var dayschedule = Controls.BuildChannelScheduleWithVideoForEdit(d);
                $(".scheduleThumbHolder").append(dayschedule);
                if (isBasicMode) {
                    $(".ancRepeat").toggle();
                }

                ExpandVideoScheduleList(d.ChannelScheduleId);

                if (d.VideoSchedules.length == 1) {
                    activeChannelScheduleId = d.ChannelScheduleId;
                }
                else {
                    activeChannelScheduleId = 0;
                }
                //DisableRepeatButtonsOnChannelSchedules(isAutoPilotOn);
            });

            var month = myCalendarMain.getDate().getMonth() + 1;
            var year = myCalendarMain.getDate().getYear() % 100 + 2000;

            void 0;
            myCalendarMain.setTooltip(pickedDateTime, "");
            LoadChannelScheduleCalendarEvents(month, year);

            DisableInstantScheduleButton(false);

            UpdateAvailableScheduleTimes();

            if (todaysChannelTubes && todaysChannelTubes.length > 0) {
                var scheduleEndTime = todaysChannelTubes[todaysChannelTubes.length - 1].EndDateAndTime;
                if (scheduleEndTime != null)
                {
                    var scheduleEntTimeDate = new Date(parseInt(scheduleEndTime.substr(6)));

                    var endHour = scheduleEntTimeDate.getHours();
                    var endMin = scheduleEntTimeDate.getMinutes();
                }
              

                DisableNewScheduleButton((endHour >= 23 && endMin > 15));
            }
            else {
                DisableNewScheduleButton(false);
            }

            $(".recentlyadded").hide();
            $("#scheduleBoxContentBox_" + channelScheduleIdToDelete).remove();

            alertify.success("Schedule was successfully deleted.");
        }
    });

}

var isYoutubeProviderActive = 1;
var isVimeoProviderActive = 1;
var b64Private = "";
var $privateVideoImageCropper;
function GetPrivateVideoView() {
    b64Private = "";

    var isChecked = $("#btnImportPrivate").is(':checked');
    // console.log(isChecked);
    isYoutubeProviderActive = isYoutubeActive;
    isVimeoProviderActive = isVimeoActive;
    var isMatureContentEnabled = matureChannelContentEnabled;
    var inputHolderUrlHtml = "";
    if (isChecked) {

        inputHolderUrlHtml = Controls.BuildImportPrivateVideosView(channelName, isYoutubeActive, isVimeoActive, isDmotionActive, isPro, isMatureContentEnabled);
        $(".inputHolderUrl").html("").html(inputHolderUrlHtml);
        //    console.log(inputHolderUrlHtml)
        InitCropPrivateVideoThumbnail(false)
        $.ajax({
            type: "POST",
            url: webMethodGetEmptyChannelCategoriesJsonForVideos,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: '{"videoTypeId":' + 2 + '}',
            async: false,
            success: function (response) {
                var cdata = response.d;
                document.getElementById("ddlByUrlCategory").options.length = 0;
                if (cdata) {
                    $("#importPrivateVideoHolder #ddlByUrlCategory.categoryPrVodeos").html("").append($('<option></option)').attr("value", "0").text("choose category"));
                    $.each(cdata, function (i, c) {
                        $("#importPrivateVideoHolder #ddlByUrlCategory.categoryPrVodeos").append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
                    });

                }
            }
        });
        $.ajax({
            type: "POST",
            url: webMethodGetVideoProviders,
            cashe: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (response) {
                var cdata = response.d;
                document.getElementById("ddlSelectProviderUrl").options.length = 0;
                if (cdata) {
                    $("#importPrivateVideoHolder #ddlSelectProviderUrl").html("").append($('<option></option)').attr("value", "0").text("choose provider"));
                    $.each(cdata, function (i, c) {
                        $("#importPrivateVideoHolder #ddlSelectProviderUrl").append($('<option></option)').attr("value", c.VideoProviderId).text(c.Name));
                    });
                }
            }
        });
    }
    else {
        inputHolderUrlHtml = Controls.BuildRegularImportVideoView(channelName, isYoutubeActive, isVimeoActive, isDmotionActive, isPro, isMatureContentEnabled);
        $(".inputHolderUrl").html("").html(inputHolderUrlHtml);
        $("#divUrlCategory").hide();
        $.ajax({
            type: "POST",
            url: webMethodGetEmptyChannelCategoriesJson,
            cashe: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var cdata = response.d;
                document.getElementById('ddlByUrlCategory').options.length = 0;
                if (cdata) {
                    $("#ddlByUrlCategory").append($('<option></option)').attr("value", "0").text("choose category"));
                    $.each(cdata, function (i, c) {
                        $("#ddlByUrlCategory").append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
                    });
                }
            }
        });
    }

}

function AddPrivateVideoToChannel() {
    debugger;
    b64Private = "";
    var channelTubeId = null;
    var provideId = null;
    var description = null;
    var title = null;
    var videoUrl = null;
    var categoryId = null;
    var isMatureContent = null;
    var hours = null;
    var minutes = null;
    var seconds = null;
    var durationInSec = null;
    var thumbnailUrl = null;
    var thumbnailImageData = null;

    channelTubeId = channelId;
    provideId = $("#importPrivateVideoHolder #ddlSelectProviderUrl option:selected").val();
    description = $("#importPrivateVideoHolder #rightSide textarea").val();
    title = $("#importPrivateVideoHolder #txtPrivateVideoTitle").val();
    videoUrl = $("#importPrivateVideoHolder #privateVideoUrl").val();
    categoryId = $("#importPrivateVideoHolder #ddlByUrlCategory option:selected").val();
    isMatureContent = $('#importPrivateVideoHolder #checkMatureContentForm').is(":checked");
    hours = $("#importPrivateVideoHolder #txtHour").val();
    minutes = $("#importPrivateVideoHolder #txtmMinutes").val();
    seconds = $("#importPrivateVideoHolder #txtmSeconds").val();
    durationInSec = hours * 3600 + minutes * 60 + seconds * 1;
    thumbnailUrl = "";


    var imageData = $privateVideoImageCropper.cropit('export', {
        type: 'image/jpeg',
        quality: .9,
        originalSize: true
    });

    if (imageData != null) {
        b64Private = imageData.split("base64,")[1];//string fileName, string imageData, int userId
        if (b64Private === undefined || b64Private == "undefined") {
            b64Private = "";
        }
    }
    var imageSize = $privateVideoImageCropper.cropit('imageSize');
    // console.log(imageSize)
    if (title.length == 0 || title == "" || title == null) {
        alertify.error("Please enter title");
        return;
    }
    else if (provideId == null || provideId == 0) {
        alertify.error("Please pick provider");
        return;
    }
    else if (videoUrl.length == 0 || videoUrl == "" || videoUrl == null) {
        alertify.error("Please enter video url");
        return;
    }
    else if (categoryId == null || categoryId == 0) {
        alertify.error("Please pick category");
        return;
    }
    else if (imageSize) {
        if (imageSize.width > 640 || imageSize.height > 480) {
            void 0;
            // alertify.error("image thumbnail size is not valid, ");
            return;
        }

    }
    else if (durationInSec == 0) {
        alertify.error("Please add duration");
        return;

    }

    // remove '' "" chars from title and descritpion (conflicts with string parameters)
    var descr = description.replace("'", " ").replace('"', " ");
    var customVideoUrl = "";//mydomain.com/video.mp4
    var params = "";

    void 0

    if (provideId == 4) {
        customVideoUrl = videoUrl.slice(videoUrl.indexOf(':') + 1);
        void 0
        params = '{"channelTubeId":' + channelTubeId + ',"userId":' + userId + ',"provideId":' + provideId + ',"description":' + "'" + description + "'" + ',"title":' + "'" + title + "'" + ',"videoUrl":' + "'" + customVideoUrl + "'" + ',"categoryId":' + categoryId + ',"isMatureContent":' + isMatureContent + ',"durationInSec":' + durationInSec + ',"thumbnailUrl":' + "'" + thumbnailUrl + "'" + ',"thumbnailImageData":' + "'" + b64Private + "'" + '}';
        //  console.log("CUSTOMURL: " + customVideoUrl);
        $.ajax({
            type: "POST",
            url: webMethodAddPrivateVideoToChannel,
            data: params,
            cashe: false,
            async: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                debugger;
                var data = response.d;

                if (data && data.Message == null) {
                    var videos = [data];



                    var pageType = "schedule";
                    var videoControls = Controls.BuildVideoBoxControlForSchedulePage(videos, true);
                    var existingHtml = $(".videoBoxHolder").html();



                    $(".videoBoxHolder").empty().append(videoControls).append(existingHtml);
                    RemoveNoVideosMessage();
                    alertify.alert("video added to channel");

                }
                else {
                    if (data) {
                        alertify.error(data.Message);
                    }
                    else {
                        alertify.error("video already added to your channel");
                    }
                }


            },
            complete: function () {

            },
            error: function (request, status, error) {
                void 0
            }
        });
    }
    else {
        var flowplayerVideoUrl = "";
        // console.log(provideId);
        if (provideId == 1) {
            var videoid = videoUrl.match(/(?:https?:\/{2})?(?:w{3}\.)?youtu(?:be)?\.(?:com|be)(?:\/watch\?v=|\/)([^\s&]+)/);
            if (videoid != null) {
                // console.log("video id = ", videoid[1]);
                flowplayerVideoUrl = "https://youtube.com/watch?v=" + videoid[1];
                // console.log(flowplayerVideoUrl);
            }
        }
        else if (provideId == 2) {
            var match = /vimeo.*\/(\d+)/i.exec(videoUrl);
            if (match) {
                // The grouped/matched digits from the regex
                flowplayerVideoUrl = "https://vimeo.com/" + match[1];
                //  console.log(flowplayerVideoUrl);
            }
        }
        else if (provideId == 3) {
            var dailymotionUrl = getDailyMotionId(videoUrl)
            flowplayerVideoUrl = "https://www.dailymotion.com/video/" + dailymotionUrl;
            //console.log(flowplayerVideoUrl);
        }
        else {
            flowplayerVideoUrl = videoUrl;
        }


        params = '{"channelTubeId":' + channelTubeId + ',"userId":' + userId + ',"provideId":' + provideId + ',"description":' + "'" + description + "'" + ',"title":' + "'" + title + "'" + ',"videoUrl":' + "'" + flowplayerVideoUrl + "'" + ',"categoryId":' + categoryId + ',"isMatureContent":' + isMatureContent + ',"durationInSec":' + durationInSec + ',"thumbnailUrl":' + "'" + thumbnailUrl + "'" + ',"thumbnailImageData":' + "'" + b64Private + "'" + '}';

        $.ajax({
            type: "POST",
            url: webMethodAddPrivateVideoToChannel,
            data: params,
            cashe: false,
            async: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {

                var data = response.d;

                if (data && data.Message == null) {
                    var videos = [data];



                    var pageType = "schedule";
                    var videoControls = Controls.BuildVideoBoxControlForSchedulePage(videos, true);
                    var existingHtml = $(".videoBoxHolder").html();



                    $(".videoBoxHolder").empty().append(videoControls).append(existingHtml);
                    RemoveNoVideosMessage();
                    alertify.alert("video added to channel");

                }
                else {
                    if (data) {
                        alertify.error(data.Message);
                    }
                    else {
                        alertify.error("this video already added to your channel");
                    }
                }
            },
            complete: function () {

            },
            error: function (request, status, error) {
                void 0
            }
        });
    }






};
function getDailyMotionId(url) {
    var m = url.match(/^.+dailymotion.com\/((video|hub)\/([^_]+))?[^#]*(#video=([^_&]+))?/);
    return m ? m[5] || m[3] : null;
}
var updatedVideoThumbB64;
var $editVideoCropper;
function showVideoSettingsPopup(videoId) {
    void 0
    $('#editPrivateVideoModal').lightbox_me({
        centered: true,
        onLoad: function () {
            $.ajax({
                type: "POST",
                url: webMethodGetVideoTubeById,
                cashe: false,
                async: false,
                data: '{"videoTubeId":' + videoId + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    $('#editPrivateVideoModal').trigger("reset");
                    //Init cropp

                    $editVideoCropper = $('#editPrivateVideoModal .image-editor');
                    $editVideoCropper.cropit({
                        imageBackground: true,
                        imageBackgroundBorderWidth: 60
                    })
                    $editVideoCropper.cropit({ allowCrossOrigin: true });
                    $editVideoCropper.cropit({ allowDragNDrop: true });
                    $editVideoCropper.find('.select-image-btn').click(function () {
                        $editVideoCropper.find('.cropit-image-input').click();
                    });
                    $editVideoCropper.find('.export').click(function () {
                        var imageData = $editVideoCropper.cropit('export', {
                            type: 'image/jpeg',
                            quality: .9,
                            originalSize: true
                        });
                        if (imageData != null) {
                            updatedVideoThumbB64 = imageData.split("base64,")[1];//string fileName, string imageData, int userId
                            if (updatedVideoThumbB64 === undefined || updatedVideoThumbB64 == "undefined") {
                                updatedVideoThumbB64 = "";
                            }
                        }
                    });

                    //video data population

                    //video title
                    var videoObject = response.d;


                    // console.log(videoObject)
                    var title = videoObject.Title;
                    $("#editPrivateVideoModal #txtPrivateVideoTitle").val("").val(title);
                    //video provider
                    switch (videoObject.VideoProviderId) {
                        case 1:
                            $("#editPrivateVideoModal #spnProvider").text("").text("Youtube Provider");
                            break;
                        case 2:
                            $("#editPrivateVideoModal #spnProvider").text("").text("Vimeo Provider");
                            break;
                        case 3:
                            $("#editPrivateVideoModal #spnProvider").text("").text("Dailymotion Provider");
                            break;
                        case 4:
                            $("#editPrivateVideoModal #spnProvider").text("").text("Custom Provider");
                    }
                    //video url
                    var videoUrl = videoObject.ProviderVideoId;
                    $("#privateVideoUrl").text("").text(videoUrl);
                    //category
                    $.ajax({
                        type: "POST",
                        url: webMethodGetChannelCategoriesJsonForCreateChannel,
                        cashe: false,
                        async: false,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (response) {
                            $("#editPrivateVideoModal select#ddlByUrlCategory").html("").html("<option value=0>Choose Category</option>")
                            $.each(response.d, function (key, val) {
                                $("#editPrivateVideoModal select#ddlByUrlCategory").append($('<option>', { value: val.CategoryId }).text(val.Name));
                            })


                            $("#editPrivateVideoModal select#ddlByUrlCategory option[value='" + videoObject.CategoryId + "']").attr("selected", "selected");

                        },
                        error: function (request, status, error) {

                        }
                    });
                    //description
                    $("#editPrivateVideoModal textarea").val("").val(videoObject.Description);
                    //mature content
                    var isRrated = videoObject.IsRRated;
                    if (isRrated) {
                        $('#editPrivateVideoModal #checkMatureContentForm').attr('checked', true);
                    }
                    else {
                        $('#editPrivateVideoModal #checkMatureContentForm').attr('checked', false);
                    }
                    //duration
                    var hours = Math.floor(videoObject.Duration / 3600);
                    var minutes = Math.floor((videoObject.Duration - (hours * 3600)) / 60);
                    var seconds = Math.floor(videoObject.Duration % 60);

                    if (hours > 0) {
                        $("#editPrivateVideoModal #txtHour").removeAttr("value").attr("value", hours);
                    }
                    else {
                        $("#editPrivateVideoModal #txtHour").removeAttr("value").attr("value", 0);
                    }

                    if (minutes > 0) {
                        $("#editPrivateVideoModal #txtmMinutes").removeAttr("value").attr("value", minutes);
                    }
                    else {
                        $("#editPrivateVideoModal #txtmMinutes").removeAttr("value").attr("value", 0);
                    }
                    if (seconds > 0) {
                        $("#editPrivateVideoModal #txtmSeconds").removeAttr("value").attr("value", seconds);
                    }
                    else {
                        $("#editPrivateVideoModal #txtmSeconds").removeAttr("value").attr("value", 0);
                    }

                    $("#editPrivateVideoModal .btnUpdateVideo").removeAttr("onclick").attr("onclick", "UpdatePrivateVideo(" + videoObject.VideoTubeId + "," + videoObject.VideoProviderId + ",'" + videoUrl + "')");
                }


            });




        },
        onClose: function () {

            updatedVideoThumbB64 = "";
            var imageEditor = ' <div class="image-editor"><div class="image-size-label"></div><div class="cropit-image-preview-container"><div class="cropit-image-preview"></div></div><div class="minImgSize">(Minimum image size: 640px X 480px)</div><input type="range" class="cropit-image-zoom-input" /><div class="image-size-label">Move cursor to resize image</div><input type="file" class="cropit-image-input" /><div class="select-image-btn">Pick Video Thumbnail</div></div>';
            $("#editPrivateVideoModal #liImageEditor").html("").html(imageEditor);

            void 0;

            void 0;
        }
    });

}
function UpdatePrivateVideo(videoId, providerId, videoUrl) {
    updatedVideoThumbB64 = "";
    var channelTubeId = null;

    var description = null;
    var title = null;

    var categoryId = null;
    var isMatureContent = null;
    var hours = null;
    var minutes = null;
    var seconds = null;
    var durationInSec = null;
    var thumbnailUrl = null;
    var thumbnailImageData = null;

    channelTubeId = channelId;

    description = $("#editPrivateVideoModal #rightSide textarea").val();
    title = $("#editPrivateVideoModal #txtPrivateVideoTitle").val();

    categoryId = $("#editPrivateVideoModal #ddlByUrlCategory option:selected").val();
    isMatureContent = $('#editPrivateVideoModal #checkMatureContentForm').is(":checked");
    hours = $("#editPrivateVideoModal #txtHour").val();
    minutes = $("#editPrivateVideoModal #txtmMinutes").val();
    seconds = $("#editPrivateVideoModal #txtmSeconds").val();
    durationInSec = hours * 3600 + minutes * 60 + seconds * 1;
    thumbnailUrl = "";


    var imageData = $editVideoCropper.cropit('export', {
        type: 'image/jpeg',
        quality: .9,
        originalSize: true
    });

    if (imageData != null) {
        updatedVideoThumbB64 = imageData.split("base64,")[1];//string fileName, string imageData, int userId
        if (updatedVideoThumbB64 === undefined || updatedVideoThumbB64 == "undefined") {
            updatedVideoThumbB64 = "";
        }
    }
    var imageSize = $editVideoCropper.cropit('imageSize');

    if (title.length == 0 || title == "" || title == null) {
        alertify.error("Please enter title");
        return;
    }


    else if (categoryId == null || categoryId == 0) {
        alertify.error("Please pick category");
        return;
    }
    else if (imageSize) {
        if (imageSize.width > 640 || imageSize.height > 480) {
            void 0
            //alertify.error("image thumbnail size is not valid");
            return;
        }

    }
    else if (durationInSec == 0) {
        alertify.error("Please add duration");
        return;

    }

    // remove '' "" chars from title and descritpion (conflicts with string parameters)
    var descr = description.replace("'", " ").replace('"', " ");
    //var customVideoUrl = "";//mydomain.com/video.mp4
    var params = "";


    //int userId, int videoTubeId, int provideId, string description, string title, string videoUrl, int categoryId, bool isMatureContent, double durationInSec, string thumbnailUrl, string thumbnailImageData
    params = '{"userId":' + userId + ',"videoTubeId":' + videoId + ',"provideId":' + providerId + ',"description":' + "'" + description + "'" + ',"title":' + "'" + title + "'" + ',"videoUrl":' + "'" + videoUrl + "'" + ',"categoryId":' + categoryId + ',"isMatureContent":' + isMatureContent + ',"durationInSec":' + durationInSec + ',"thumbnailUrl":' + "'" + thumbnailUrl + "'" + ',"thumbnailImageData":' + "'" + updatedVideoThumbB64 + "'" + '}';

    $.ajax({
        type: "POST",
        url: webMethodUpdatePrivateVideo,
        data: params,
        cashe: false,
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            var data = response.d;

            if (data && data.Message == null) {
                var videos = [data];


                $("#boxContent_" + videoId).hide();
                var pageType = "schedule";
                var videoControls = Controls.BuildVideoBoxControlForSchedulePage(videos, false);
                var existingHtml = $(".videoBoxHolder").html();



                $(".videoBoxHolder").empty().append(videoControls).append(existingHtml);
                RemoveNoVideosMessage();
                alertify.alert("video has been updated");

            }
            else {
                if (data) {
                    alertify.error(data.Message);
                }
            }


        },
        complete: function () {

        },
        error: function (request, status, error) {
            void 0
        }
    });
};









function SetProviderName(el) {
    var url = $("#importPrivateVideoHolder #privateVideoUrl").val();
    var providerName = parseVideo(url);

    if (providerName) {
        return providerName;

    }
    else {
        alertify.error("video url is not valid");
        return;
    }


}
function validateYouTubeUrl(url) {
    
    if (url != undefined || url != '') {
        var regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=|\?v=)([^#\&\?]*).*/;
        var match = url.match(regExp);
        if (match && match[2].length == 11) {
            // Do anything for being valid
            // if need to change the url to embed url then use below line            
            return true;
        } else {
            return false
            // Do anything for not being valid
        }
    }
}
function parseVideo(url) {
    var regYoutube = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=|\?v=)([^#\&\?]*).*/;
    var regVimeo = "vimeo\.com/(?:.*#|.*/)?([0-9]+)";
    var regDailymotion = /(?:dailymotion\.com(?:\/video|\/hub)|dai\.ly)\/([0-9a-z]+)(?:[\-_0-9a-zA-Z]+#video=([a-z0-9]+))?/g;
    if (validateYouTubeUrl(url)) {
        void 0
        $('#importPrivateVideoHolder #ddlSelectProviderUrl option[value=1]').attr('selected', 'selected');
        return 'youtube';
    }
    else if (url.match(regDailymotion)) {
        $('#importPrivateVideoHolder #ddlSelectProviderUrl option[value=3]').attr('selected', 'selected');
        return 'dailymotion';
    }
    else if (url.match(regVimeo)) {
        $('#importPrivateVideoHolder #ddlSelectProviderUrl option[value=2]').attr('selected', 'selected');
        return 'vimeo';
    }
    else {
        var ext = url.substr((url.lastIndexOf('.') + 1));
        //  console.log(ext);
        switch (ext) {
            case 'mp4':
            case 'webm':
                $('#importPrivateVideoHolder #ddlSelectProviderUrl option[value=4]').attr('selected', 'selected');
                return "custom";
            case 'm3u8':
                $('#importPrivateVideoHolder #ddlSelectProviderUrl option[value=4]').attr('selected', 'selected');
                return "custom";
            case 'mp3':
                $('#importPrivateVideoHolder #ddlSelectProviderUrl option[value=4]').attr('selected', 'selected');
                return "custom";
            case 'mpd':
                $('#importPrivateVideoHolder #ddlSelectProviderUrl option[value=4]').attr('selected', 'selected');
                return "custom";
            case 'ogv':
                $('#importPrivateVideoHolder #ddlSelectProviderUrl option[value=4]').attr('selected', 'selected');
                return "custom";
            default:
                if (url.indexOf("https://www.facebook.com/") >= 0) {
                    return "custom";
                }
                if (url.indexOf("https://www.twitch.tv/videos/") >= 0) {
                    return "custom";
                }
                if (url.indexOf("https://streamable.com/") >= 0) {
                    return "custom";
                }
                alertify.error("wrong video format");
                return;
        }

    }
}

function InitCropPrivateVideoThumbnail(isCreating) {



    var filename = "";

    void 0

    $privateVideoImageCropper = $('#importPrivateVideoHolder .image-editor');

    $privateVideoImageCropper.cropit({
        imageBackground: true,
        imageBackgroundBorderWidth: 60
    })
    $privateVideoImageCropper.cropit({ allowCrossOrigin: true });
    $privateVideoImageCropper.cropit({ allowDragNDrop: true });
    //$imageCropper.cropit('imageSrc', imgSrc);

    $privateVideoImageCropper.find('.select-image-btn').click(function () {
        $privateVideoImageCropper.find('.cropit-image-input').click();
    });


    $('.export').click(function () {

        var imageData = $privateVideoImageCropper.cropit('export', {
            type: 'image/jpeg',
            quality: .9,
            originalSize: true
        });

        if (imageData != null) {

            b64Private = imageData.split("base64,")[1];//string fileName, string imageData, int userId
            if (b64Private === undefined || b64 == "undefined") {
                b64Private = "";
            }

            //  console.log(b64Private)

        }
    });

}

function ShowAddVideoPopup() {
    //console.log("ShowAddVideoPopup");
    var $popUp = $("#popUpOverlay");
    $('#addVideoInstructions').hide();

    isYoutubeProviderActive = isYoutubeActive;
    isVimeoProviderActive = isVimeoActive;
    isMatureContentEnabled = matureChannelContentEnabled;
    // var isPrivateContentEnabled=
    var popupHtml = Controls.BuildAddVideosPopup(channelName, isYoutubeActive, isVimeoActive, isDmotionActive, isPro, isMatureContentEnabled, showPrivateVideoMode);
    void 0;

   
    void 0;
    $popUp.empty().append(popupHtml)

    var pageHeight = (document.height !== undefined) ? document.height : document.body.offsetHeight;
    var $window = $(window).on('resize', function () {
        $popUp.height(pageHeight);
    }).trigger('resize'); //on page load

    $popUp.show();

    LoadSearchByKeywordsTab();

    pageIndex = 1;
    prevPageIndex = 1;
    nextPageIndex = 1;

}

function HideAddVideoPopup() {
    $("#popUpOverlay").hide();
}

function LoadMoreResults() {
    if (activeTab == 0) {
        GetPublicLibrary();
    }
    else if (activeTab == 1) {
        pageIndex++;
        GetYoutubeVideosByKeyword(providerId);

    }
    else if (activeTab == 2) {
        GetYouTubeVideoByURL();
    }
    else if (activeTab == 3) {
        LoadVideosFromVideoRoom();
    }
}

// ------------------------------------------------------------------------------------------------------------------
// Public library functions - START
//-------------------------------------------------------------------------------------------------------------------
function LoadSearchPublicLibraryTab() {
    activeTab = 0;

    ClearActiveTabs();

    //$("#SearchPublicLibrary").addClass("active");
    //$("#SearchPublicLibrary").show();
    //$("#ddlPublicLibraryCategory").show();
    //$("#ddlPublicLibraryCategory").empty();

    $(".publicLib").addClass("tabActive");
    $(".searchKey, .searchUrl, .searchVR").removeClass("tabActive");
    $('#addVideoInstructions').show();

    ToggleSearchPublicLibraryTabVisiblity(true);

    $.ajax({
        type: "POST",
        url: webMethodGetChannelCategoriesForPublicLibraryJson,
        cashe: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var cdata = response.d;
            document.getElementById('ddlPublicLibraryCategory').options.length = 0;
            if (cdata) {
                $.each(cdata, function (i, c) {
                    $('#ddlPublicLibraryCategory').append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
                    if (i == 0) {
                        $('#ddlPublicLibraryCategory option[value="0"]').prop('selected', true);
                    }
                });
            }

            GetPublicLibrary(true);
        }
    });
};

function ChangePublicLibraryCategoryHandler() {
    pageIndex = 1;
    prevPageIndex = 1;
    nextPageIndex = 1;

    $(".loadedContent").html("");

    GetPublicLibrary();
}

function GetPublicLibrary(isSearching) {
    countResultIndex = 0;
    countResultIndex = 1;

    var categoryId = $("#ddlPublicLibraryCategory option:selected").val();
    var keywords = $('#txtSearchVideoByKeywordForPublicLib').val();

    if (isSearching) {
        pageIndex = 1;
        $(".loadedContent").html("");
    }

    if (categoryId == null) {
        categoryId = 0;
    }

    var searchCriteria = {
        PageIndex: pageIndex,
        CategoryId: categoryId,
        ChannelTubeId: channelId,
        Keywords: keywords
    }

    $.ajax({
        type: "POST",
        url: webMethodGetPublicLibResult,
        cashe: false,
        data: '{"criteria":' + JSON.stringify(searchCriteria) + '}',
        dataType: "json",
        contentType: "application/json",
        beforeSend: function () {
            $("#loadingDiv").show();
        }, success: function (response) {
            var data = response.d;
            pageIndex = data.NextPageIndex;
            prevPageIndex = data.PrevPageIndex;
            nextPageIndex = data.NextPageIndex;
            var pageSize = data.PageSize;
            var pageCount = data.PageCount;

            if (data.PageIndex == pageCount) {
                $("#loadMoreVideos").hide();
            }
            else {
                $("#loadMoreVideos").show();
            }

            if (data.VideoTubeModels && data.VideoTubeModels.length > 0) {
                var videos = data.VideoTubeModels;
                var pageType = "add-videos";
                var videoControls = Controls.BuildViodeBoxControlForAddVideosPage("public-library", videos, false); //Controls.BuildVideoRoomControl(pageType, videos, false);
                $(".loadedContent").append(videoControls);
            }

            $("#sortSelect").show();
        },
        complete: function () {
            $("#loadingDiv").hide();
        }
    });
};
// ------------------------------------------------------------------------------------------------------------------
// Public library functions - END
//-------------------------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------------------------
// Search by keyword functions - START
//-------------------------------------------------------------------------------------------------------------------
function LoadSearchByKeywordsTab() {
    activeTab = 1;
    pageIndex = 1;
    nextPageIndex = 1;
    prevPageIndex = 1;
    ClearActiveTabs();
    $('#divKeywordCategory').hide();
    $('#addVideoInstructions').hide();
    $("#loadMoreVideos").hide();
    $("#radioYoutube").attr("checked", "checked");
    //$("#SearchByKeywords").addClass("active");
    //$("#SearchByKeywords").show();
    //$(".inputHolderKeywords").show();
    //$("#txtKeyword").empty().show();
    //$("#btnSearchByKeyword").show();
    //$("#divKeywordCategory").show();
    //$("#ddlByKeywordsCategory").show();
    //$("#ddlByKeywordsCategory").empty();

    $(".searchKey").addClass("tabActive");
    $(".publicLib, .searchUrl, .searchVR").removeClass("tabActive");

    ToggleSearchByKeyWordsTabVisiblity(true);

    $(document).keyup(function (event) {
        if ($("#btnSearchByKeyword").is(":visible")) {
            clearScreen = true;
            if (event.keyCode == 13) {
                $("#btnSearchByKeyword").trigger('click');
            }
        }
    });

    $.ajax({
        type: "POST",
        url: webMethodGetEmptyChannelCategoriesJsonForVideos,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: '{"videoTypeId":' + 2 +  '}',
     
        success: function (response) {

            var cdata = response.d;
            document.getElementById('ddlByKeywordsCategory').options.length = 0;
            if (cdata) {
                $("#ddlByKeywordsCategory").append($('<option></option)').attr("value", "0").text("choose category"));
                $.each(cdata, function (i, c) {
                    $("#ddlByKeywordsCategory").append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
                });
            }
        }
    });
};

function ClearTxtKeywordInput() {
    $("#txtKeyword").val('');
    pageToken = "";
}

function GetYouTubeVideosByKeyword() {
    var providerId = 1;

    if ($('#radioYoutube') || isYoutubeProviderActive) {
        if ($('#radioYoutube').is(':checked')) {
            pageCountVimeo = 1;
            providerId = 1;
        }
        else {
            var pageCountVimeo = 1;
            providerId = 2;
        }
    }
    else {
        pageCountVimeo = 1;
        providerId = 1;
    }
}

function ClearContent() {
    $(".loadedContent").empty();
    $("#loadMoreVideos").hide();

    pageIndex = 1;
    prevPageIndex = 1;
    nextPageIndex = 1;
    pageToken = "";
}

function SetVideoProvider(select) {
    providerId = $("#" + select.id + " option:selected").val();
    // console.log(providerId);
}

function GetVideoByKeyword(videoProviderId) {
    ClearContent();
    GetYoutubeVideosByKeyword(providerId);

}

function GetYoutubeVideosByKeyword(videoProviderId) {
    void 0
    if (videoProviderId != undefined) {
        providerId = videoProviderId;
    }

    if (providerId == 0) {
        alertify.error("Please select a video provider");
        return;
    }
    else if ($("#txtKeyword").val().length == 0) {
        $(".loadedContent").html("");
        alertify.error("Please enter search keywords");
        $("#loadMoreVideos").hide();
        return;
    }
    else {
        var keyword = $("#txtKeyword").val();
        var isLong = $("#chkIsLongDuration").is(':checked');

        countResultIndex = 0;
        countResultIndex = 1;
        $("#loadingDiv").show();

        $('#divKeywordCategory').show();
        $('#addVideoInstructions').show();

        if (providerId == 3) {

            GetDmotionVideosByKeyword(keyword, pageIndex, channelId, isLong);

        }
        else {
            $.ajax({
                type: "POST",
                url: webMethodGetVideoByKeyWord,
                cashe: false,
                data: '{"searchString":' + "'" + keyword + "'" + ',"startIndex":' + pageIndex + ',"channelTubeId":' + channelId + ',"pageToken":' + "'" + pageToken + "'" + ',"providerId":' + providerId + ',"isLong":' + isLong + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    var data = response.d;
                    pageIndex = data.NextPageIndex;
                    prevPageIndex = data.PrevPageIndex;
                    var pageSize = data.PageSize;
                    var pageCount = data.PageCount;
                    void 0;
                    pageToken = data.PageToken;
                    void 0;

                    if (data.VideoTubeModels.length != 0) {
                        //console.log(data)
                        var videos = data.VideoTubeModels;
                        void 0;
                        var pageType = "add-videos";
                        var videoControls = Controls.BuildViodeBoxControlForAddVideosPage("by-keyword", videos, true); //Controls.BuildVideoRoomControl(pageType, videos, false);

                        if (clearScreen) {
                            $(".loadedContent").html("");
                        }

                        $(".loadedContent").append(videoControls);

                        if (providerId == 1) {
                            if (!pageToken) {
                                $("#loadMoreVideos").hide();
                            }
                            else {
                                $("#loadMoreVideos").show();
                            }
                        }
                        else {
                            if (pageIndex == prevPageIndex) {
                                $("#loadMoreVideos").hide();
                            }
                            else {
                                $("#loadMoreVideos").show();
                            }
                        }
                    }
                    else {
                        $(".loadedContent").html("").html("<span class='spnMsg'>Sorry, there are no search results</span>");
                        $("#loadMoreVideos").hide();
                    }

                    clearScreen = false;

                },
                complete: function (response) {
                    $("#loadingDiv").hide();
                }
            });
        }
    }
}


//function GetVimeoVideosByKeyword() {
//    $("#loadingDiv").show();
//    if ($("#txtKeyword").val().length > 0) {
//        var keyword = $("#txtKeyword").val();

//        var urlX = 'https://api.vimeo.com/videos?query=' + keyword + '&method=vimeo.videos.getThumbnailUrls&page=' + pageCountVimeo + '&sort=duration&direction=desc&per_page=50&access_token='+accessToken;
//        //var newurlX=https://vimeo.com/search?from=everyone&field_title=1&field_description=1&field_tags=1&time=any&filter_plays=0&filter_likes=0&filter_comments=0&duration_min=00%3A30%3A00&duration_max=&quality[]=sd&quality[]=hd&cc=by&q=amazing
//        var VideoTubePageModel = [];
//        var VideoTubeModel = [];

//        $.getJSON(urlX, function (data) {


//            $.each(data, function (i, c) {
//                console.log(data);
//                if ((i == "data") && (data != null)) {
//                    $.each(c, function (i, v) {
//                        //console.log(v);
//                        var vimeoVideo = new Object();
//                        var r = /(videos|video|channels|\.com)\/([\d]+)/, uri = v.uri;
//                        var id = uri.match(r)[2];

//                        vimeoVideo.VideoTubeId = 0;
//                        vimeoVideo.ProviderVideoId = id.toString();
//                        vimeoVideo.Title = v.name;
//                        vimeoVideo.Description = v.description;
//                        vimeoVideo.Duration = v.duration;
//                        //vimeoVideo.CategoryId=
//                        vimeoVideo.VideoProviderId = 2
//                        vimeoVideo.IsRRated = false
//                        vimeoVideo.IsRemovedByProvider = false
//                        if ((v.status == "available") && (v.privacy.embed == 'public')) {
//                            vimeoVideo.IsRestrictedByProvider = false;
//                        }
//                        else {
//                            vimeoVideo.IsRestrictedByProvider = true;

//                        }

//                        vimeoVideo.IsInPublicLibrary = false;
//                        if (v.privacy.view == "anybody") {
//                            vimeoVideo.IsPrivate = true;
//                        }
//                        else {
//                            vimeoVideo.IsPrivate = false;
//                        }
//                        if (v.pictures) {
//                            vimeoVideo.Thumbnail = v.pictures.sizes[2].link;
//                        }
//                        vimeoVideo.VideoProviderName = "vimeo";
//                        //vimeoVideo.CategoryName
//                        vimeoVideo.IsScheduled = false;
//                        vimeoVideo.UseCounter = 0;
//                        vimeoVideo.ViewCounter = v.stats.plays;

//                        vimeoVideo.DurationString = GetDurationString(v.duration);
//                        vimeoVideo.IsInChannel = false;
//                        vimeoVideo.Message = "";
//                        vimeoVideo.DateAdded = Date.now;
//                        //console.log(vimeoVideo)
//                        VideoTubeModel.push(vimeoVideo);
//                        // $("#content").text(id);
//                    });
//                    var PageIndex = 0
//                    var PrevPageIndex = 0;
//                    var NextPageIndex = 0;
//                    var PageSize = 50;
//                    var PageCount = 0;
//                    var PageToken = 0;
//                    VideoTubePageModel.push(PageIndex);
//                    VideoTubePageModel.push(PrevPageIndex);
//                    VideoTubePageModel.push(NextPageIndex);
//                    VideoTubePageModel.push(PageSize);
//                    VideoTubePageModel.push(PageCount);
//                    VideoTubePageModel.push(PageToken);
//                    VideoTubePageModel.push(VideoTubeModel);
//                    var videos = VideoTubeModel;
//                    var pageType = "add-videos";
//                    var videoControls = Controls.BuildViodeBoxControlForAddVideosPage("by-keyword", videos);
//                    //  console.log(videoControls);
//                    $(".loadedContent").append(videoControls);
//                    $("#loadMoreVideos").show();
//                    $("#loadingDiv").hide();
//                    pageCountVimeo++;
//                }
//                else
//                {
//                    $("#loadMoreVideos").hide();
//                }




//             //   console.log(VideoTubePageModel);

//            })
//        });

//    }
//    else {
//        $(".loadedContent").text("Please enter keyword");
//    }
//};

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
}
// ------------------------------------------------------------------------------------------------------------------
// Search by keyword functions - END
//-------------------------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------------------------
// Search by URL functions - START
//-------------------------------------------------------------------------------------------------------------------
function LoadSearchByUrlTab() {
    activeTab = 2;
    ClearActiveTabs();
    $("#loadMoreVideos").hide();
    $('#addVideoInstructions').hide();
    $("#radioYoutubeUrl").attr("checked", "checked");
    $("#importUserYoutubeUploadsHolder").hide();
    $("#btnImportYoutubeUserUploads").removeClass("active");

    $("#btnImportByUrl").removeClass('active');
    $(".inputHolderUrl").hide();
    $('#btnImportByUrl').prop('checked', true);

    ImportByUrl();

    //$("#SearchByUrl").addClass("active");
    //$("#SearchByUrl").show();
    //$(".inputHolderUrl").show();
    //$("#txtURL").empty().show();
    //$("#btnSearchByURL").show();
    //$("#divUrlCategory").show();
    //$("#ddlByUrlCategory").show();
    //$("#ddlByUrlCategory").empty();
    //$(".loadedContent").empty();

    $(".searchUrl").addClass("tabActive");
    $(".publicLib, .searchKey, .searchVR").removeClass("tabActive");
    ImportByUrl();
    ToggleSearchByUrlTabVisiblity(true);

    $(document).keyup(function (event) {
        if ($("#btnSearchByURL").is(":visible")) {
            clearScreen = true;
            if (event.keyCode == 13) {
                $("#btnSearchByURL").trigger('click');
            }
        }
    });

    $.ajax({
        type: "POST",
        url: webMethodGetEmptyChannelCategoriesJsonForVideos,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: '{"videoTypeId":' + 2 + '}',
        success: function (response) {
            var cdata = response.d;
            document.getElementById('ddlByUrlCategory').options.length = 0;
            if (cdata) {
                $("#ddlByUrlCategory").append($('<option></option)').attr("value", "0").text("choose category"));
                $.each(cdata, function (i, c) {
                    $("#ddlByUrlCategory").append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
                });
            }
        }
    });
};

function GetYouTubeVideoByURL() {
    ClearContent();
    GetYoutubeVideosByUrl(providerId);
};


function ValidURL(url) {
    var regexp = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/
    return regexp.test(url);
}
function GetYoutubeVideosByUrl(videoProviderId) {
    //console.log(providerId);
    debugger;
    if (videoProviderId != undefined) {
        providerId = videoProviderId;
    }

    if (providerId == 0) {
        alertify.error("Please select a video provider");
        return;
    }

    //console.log(providerId);

    var url = $("#txtURL").val();
    var urlIsValid = ValidURL(url);

    //    console.log(url.length);

    if ((url.length == 0) || (!urlIsValid)) {
        $('#addVideoInstructions').hide();
        $(".loadedContent").html(""); //.html("<span class='spnMsg'>Please enter URL</span>");
        alertify.error("Please enter a valid URL");
    }
    else {
        if (providerId == 3) {
            $("#txtURL").val('');
            GetDailyMotionByUrl(url);
        }
        else {
            $.ajax({
                type: "POST",
                url: webMethodGetVideoByUrl,
                cashe: false,
                data: '{"url":' + "'" + url + "'" + ',"channelTubeId":' + channelId + ',"providerId":' + providerId + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    var video = response.d;
                    //console.log(video);
                    if (video != null) {
                        //if (video.Message) {
                        //    alertify.error(video.Message);
                        //}
                        // else {
                        var videos = {};
                        videos[0] = video;
                        var videoControls = Controls.BuildViodeBoxControlForAddVideosPage("by-url", videos, true); //Controls.BuildVideoRoomControl("add-videos", videos, false);
                        $("#txtURL").val('');
                        $('#divUrlCategory').show();
                        $(".loadedContent").empty().append(videoControls);
                        $("#loadMoreVideos").hide();
                        $('#addVideoInstructions').show();
                    }
                    else {
                        if (providerId == 0) {
                            alertify.error("Please select a correct video provider in Step #1");
                        }
                        else {
                            alertify.error("Unable to retrieve video. Possible reasons are: video is restricted by provider or private or has mature content");
                        }

                        $(".loadedContent").html("");
                        $('#divUrlCategory').hide();
                        $("#loadMoreVideos").hide();
                        $('#addVideoInstructions').hide();
                    }

                    $("#loadingDiv").hide();
                }
            });
        }
    }
};
function ShowTimeZoneSelect() {
    $("#timeZoneNameChange, #timeZoneName").hide();
    $(".timeZoneSelect, #newTimeZone, #cancelTimeZone").show();
}
function ChangeTimeZoneNameAndTime() {
    //set timaZoneName data-offset
    var selectedItemText = $(".timeZoneSelect option:selected").text();
    var dataOffset = $(".timeZoneSelect option:selected").attr("data-offset");
    $("#timeZoneName").removeAttr("data-offset").attr("data-offset", dataOffset)
    $("#timeZoneName").text("").text(selectedItemText);
    $(".timeZoneSelect,  #newTimeZone, #cancelTimeZone").hide();
    $("#timeZoneName, #timeZoneNameChange").show();

}
function getTimezoneName() {
    var tz = jstz.determine(); // Determines the time zone of the browser client


    // var format = 'YYYY/MM/DD HH:mm:ss ZZ';
    var zoneFormat = 'Z';
    //alert(moment.tz('Europe/London').format(format));
    //return moment.tz(tz.name()).format(format)+" "+tz.name();
    return "(GMT " + moment.tz(tz.name()).format(zoneFormat) + ") " + tz.name();
    // return moment.tz(tz.name() + "(GMT " + tz.name().format(format) + ")");
}
function getTimeZoneOffset() {
    var tz = jstz.determine(); // Determines the time zone of the browser client  
    var zoneFormat = 'Z';

    // console.log(zoneFormat);
    return moment.tz(tz.name()).format(zoneFormat);

}
function CancelTimeZone() {

    $(".timeZoneSelect,  #newTimeZone, #cancelTimeZone").hide();
    $("#timeZoneName, #timeZoneNameChange").show();

}
function SetCategoryUrl() {

}

$(".liveStartTime, .liveEndStartTime, .liveStartDate, .liveEndDate").focus(function () {
    $(".startDateTimePickerHolderLive .spnError").text("");
});
function SaveLiveVideo(providerVideoId) {
    $(".startDateTimePickerHolderLive .spnError").text("")
    var startTimePicked = $('.liveStartTime').timepicker('getTime', [new Date()]);
    var startEndPicked = $('.liveEndStartTime').timepicker('getTime', [new Date()]);
    var startDatePicked = $(".liveStartDate").datepicker("getDate");
    var endDatePicked = $(".liveEndDate").datepicker("getDate");
  
   
     if ($(".liveStartDate").datepicker("getDate") === null) {
        $(".startDateTimePickerHolderLive .spnError").text("").text("please add start date");
        return;
    }
    else if ($(".liveStartTime").timepicker('getTime', [new Date()]) === null) {
        $(".startDateTimePickerHolderLive .spnError").text("").text("please add start time");
        return;
    }
    else if ((endDatePicked!==null)&&((startDatePicked.getDate() == endDatePicked.getDate()) && (startEndPicked.getTime() <= startTimePicked.getTime())))
   
    {
        var selected = new Date(startTimePicked).getDate();
        void 0
        $(".startDateTimePickerHolderLive .spnError").text("").text("end time is not valid");
        return;
    }
      
    
        
    else {
        var now = new Date();
        var offset = now.getTimezoneOffset();
        var tz = jstz.determine();

        void 0;
        void 0;

        startDatePicked.setHours(startTimePicked.getHours())
        // startDatePicked.setMinutes(startTimePicked.getMinutes()+offset);
        startDatePicked.setMinutes(startTimePicked.getMinutes());
        void 0;
        var startTimeUTC = moment(startDatePicked).format('MM/DD/YYYY HH:mm');
        startTimeUTC = moment(startDatePicked).tz("Etc/GMT").format("MM/DD/YYYY HH:mm");
        void 0
       
        var endTimeUTC = "";
        var endDatePicked = $(".liveEndDate").datepicker("getDate");
        var endTimePicked = $('.liveEndStartTime').timepicker('getTime', [new Date()]);
       
       
        if ((endDatePicked != null || endTimePicked != undefined) && (endTimePicked != null || endTimePicked != undefined)) {
           
            void 0;
            void 0;
           endDatePicked.setHours(endTimePicked.getHours());
            // endDatePicked.setMinutes(endTimePicked.getMinutes() + offset);
            endDatePicked.setMinutes(endTimePicked.getMinutes());
            endTimeUTC = moment(endDatePicked).tz("Etc/GMT").format("MM/DD/YYYY HH:mm");
           
           
           
            
        }
        var startTimeParsed = Date.parse(startTimeUTC);
        var endTimeParsed = Date.parse(endTimeUTC);
        if (endTimeParsed != null || endTimeParsed != ""||endTimeParsed!==undefined)
        {
            if (endTimeParsed < startTimeParsed) {

                $(".startDateTimePickerHolderLive .spnError").text("").text("end time is not valid");
                return;
            }
            else
            {
                var params = '{"providerVideoId":' + "'" + providerVideoId + "'" + ',"channelTubeId":' + channelTubeId + ',"categoryId":' + categoryId + ',"startTimeUTC":' + "'" + startTimeUTC + "'" + ',"endTimeUTC":' + "'" + endTimeUTC + "'" + ',"userId":' + userId + '}';
                void 0;

                $.ajax({
                    type: "POST",
                    url: webMethodAddLiveVideo,
                    data: params,
                    cashe: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        var data = response.d;
                        //     console.log("LIVE VIDEO: "+response.d);
                        if (data && data.Message == null) {
                            var videos = [data];
                            var pageType = "schedule";
                            var videoControls = Controls.BuildVideoBoxControlForSchedulePage(videos, true);
                            var existingHtml = $(".videoBoxHolder").html();

                            $("#recentlyadded_" + providerVideoId).text("in channel");
                            $("#addvideosId_" + providerVideoId).removeAttr("onclick").hide();

                            $(".videoBoxHolder").empty().append(videoControls).append(existingHtml);
                            RemoveNoVideosMessage();
                        }
                    },
                    complete: function () {
                        $('.startDateTimePickerHolderLive').trigger('close');
                    },
                    error: function (request, status, error) {
                        void 0;
                    }
                });
            }
            }

        }
        
       
}
function ShowLiveMessage(btn) {
    var btnId = btn.id;

    $("#" + btnId + " .spanLiveMessage").show();


}

function HideLiveMessage(btn) {
    var btnId = btn.id;

    $("#" + btnId + " .spanLiveMessage").hide();
}
function getCurrentTime(date) {
    var hours = date.getHours(),
        minutes = date.getMinutes(),
        ampm = hours >= 12 ? 'pm' : 'am';

    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;

    return hours + ':' + minutes + ' ' + ampm;
}
function GetVimeoVideosByUrl() {
    var url = $("#txtURL").val();
    $("#txtURL").val('');
    regExp = "vimeo\\.com/(?:.*#|.*/videos/)?([0-9]+)";
    var videoId;
    var match = url.match(regExp);
    if (match) {
        videoid = url.split('/')[url.split('/').length - 1];
        void 0;
    }
    else {
        $(".loadedContent").text("");
        alertify.error("Invalid URL specified");
        return;
    }

    if (url.length == 0) {
        $(".loadedContent").text("");
        alertify.error("Please enter a valid URL");
        return;
    }
    else {

        var urlX = 'https://api.vimeo.com/videos/' + videoid + '?access_token=' + accessToken;
        $.getJSON(urlX, function (data) {
            var VideoTubePageModel = [];
            var VideoTubeModel = [];

            $.getJSON(urlX, function (data) {




                if (data != null) {

                    void 0;
                    var vimeoVideo = new Object();
                    var r = /(videos|video|channels|\.com)\/([\d]+)/, uri = data.uri;
                    var id = uri.match(r)[2];

                    vimeoVideo.VideoTubeId = 0;
                    vimeoVideo.ProviderVideoId = id.toString();
                    vimeoVideo.Title = data.name;
                    vimeoVideo.Description = data.description;
                    vimeoVideo.Duration = data.duration;
                    //vimeoVideo.CategoryId=
                    vimeoVideo.VideoProviderId = 2
                    vimeoVideo.IsRRated = false
                    vimeoVideo.IsRemovedByProvider = false
                    if ((data.status == "available") && (data.privacy.embed == 'public')) {
                        vimeoVideo.IsRestrictedByProvider = false;
                    }
                    else {
                        vimeoVideo.IsRestrictedByProvider = true;

                    }

                    vimeoVideo.IsInPublicLibrary = false;
                    if (data.privacy.view == "anybody") {
                        vimeoVideo.IsPrivate = false;
                    }
                    else {
                        vimeoVideo.IsPrivate = true;
                    }
                    vimeoVideo.Thumbnail = data.pictures.sizes[2].link;
                    vimeoVideo.VideoProviderName = "vimeo";
                    //vimeoVideo.CategoryName
                    vimeoVideo.IsScheduled = false;
                    vimeoVideo.UseCounter = 0;
                    vimeoVideo.ViewCounter = data.stats.plays;

                    vimeoVideo.DurationString = GetDurationString(data.duration);
                    vimeoVideo.IsInChannel = false;
                    vimeoVideo.Message = "";
                    vimeoVideo.DateAdded = Date.now;
                    //console.log(vimeoVideo)
                    VideoTubeModel.push(vimeoVideo);
                    // $("#content").text(id);

                    var PageIndex = 0
                    var PrevPageIndex = 0;
                    var NextPageIndex = 0;
                    var PageSize = 50;
                    var PageCount = 0;
                    var PageToken = 0;
                    VideoTubePageModel.push(PageIndex);
                    VideoTubePageModel.push(PrevPageIndex);
                    VideoTubePageModel.push(NextPageIndex);
                    VideoTubePageModel.push(PageSize);
                    VideoTubePageModel.push(PageCount);
                    VideoTubePageModel.push(PageToken);
                    VideoTubePageModel.push(VideoTubeModel);
                    var videos = VideoTubeModel;
                    var pageType = "add-videos";
                    var videoControls = Controls.BuildViodeBoxControlForAddVideosPage("by-url", videos, true);
                    //  console.log(videoControls);
                    $(".loadedContent").append(videoControls);


                }
                else {
                    $("#loadMoreVideos").hide();
                }




                //   console.log(VideoTubePageModel);

            })
        });
    }



};
// ------------------------------------------------------------------------------------------------------------------
// Search by URL functions - END
//-------------------------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------------------------
// Search Video Room functions - START
//-------------------------------------------------------------------------------------------------------------------
function LoadSearchVideoRoomTab() {
    activeTab = 3;
    pageIndex = 1;
    nextPageIndex = 1;
    prevPageIndex = 1;

    shouldLoadAllVideos = true;

    if (shouldLoadAllVideos) {
        $('#chkShowMyVideos').prop('checked', true);
        $('#chkShowLicensedVideos').prop('checked', true);
        $('#chkShowExternalProvidersVideos').prop('checked', true);

        shouldLoadMyVideos = true;
        shouldLoadLicensedVideos = true;
        shouldLoadExternalVideos = true;
    }

    ClearActiveTabs();
    $('#addVideoInstructions').show();


    $(".searchVR").addClass("tabActive");
    $(".searchKey, .searchUrl, .publicLib").removeClass("tabActive");

    ToggleSearchVideoRoomTabVisiblity(true);

    LoadVideoRoomTabCategories(true);
};

function LoadVideoRoomTabCategories(triggerVideoLoad) {
    var selectedCategoryId = 0;
    if (triggerVideoLoad == false) {
        selectedCategoryId = $("#ddlVideoRoomCategory option:selected").val();
    }
    $.ajax({
        type: "POST",
        url: webMethodGetChannelCategoriesForVideoRoomJson,
        cashe: false,
        data: '{"userId":' + userId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var cData = response.d;
            if (cData) {
                document.getElementById('ddlVideoRoomCategory').options.length = 0;
                $.each(cData, function (i, c) {
                    $('#ddlVideoRoomCategory').append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
                });
                $('#ddlVideoRoomCategory option[value="' + selectedCategoryId + '"]').prop('selected', true);
            }

            if (triggerVideoLoad == true) {
                LoadVideosFromVideoRoom();
            }
        }
    });
}

function ChangeVideoRoomCategoryHandler() {
    pageIndex = 1;
    prevPageIndex = 1;
    nextPageIndex = 1;

    $(".loadedContent").html("");

    LoadVideosFromVideoRoom();
}

function LoadVideosFromVideoRoom(isSearching) {
    var categoryId = $("#ddlVideoRoomCategory option:selected").val();
    var keywords = $('#txtSearchVideoByKeywordForVideoRoom').val();

    if (isSearching) {
        $(".loadedContent").html("");
    }

    var searchCriteria = {
        PageIndex: pageIndex,
        UserId: userId,
        CategoryId: categoryId,
        RetrieveMyVideos: shouldLoadMyVideos,
        RetrieveLicensedVideos: shouldLoadLicensedVideos,
        RetrieveExternalVideos: shouldLoadExternalVideos,
        Keywords: keywords,
        ChannelTubeId: channelId
    };

    $.ajax({
        type: "POST",
        url: webMethodLoadVideosFRomVideoRoom,
        cashe: false,
        data: "{'searchCriteria':" + JSON.stringify(searchCriteria) + "}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#loadingDiv").show();
        },
        success: function (response) {
            $("#loadingDiv").hide();
            if (response.d) {
                var data = response.d;
                pageIndex = data.NextPageIndex;
                prevPageIndex = data.PrevPageIndex;
                nextPageIndex = data.NextPageIndex;
                var pageSize = data.PageSize;
                var pageCount = data.PageCount;

                if (data.PageIndex >= pageCount) {
                    $("#loadMoreVideos").hide();
                }
                else {
                    $("#loadMoreVideos").show();
                }

                if (data.VideoTubeModels && data.VideoTubeModels.length > 0) {
                    var videos = data.VideoTubeModels;
                    var pageType = "video-room";
                    var videoControls = Controls.BuildViodeBoxControlForAddVideosPage(pageType, videos, false); //Controls.BuildVideoRoomControl(pageType, videos, false);
                    $(".loadedContent").append(videoControls);
                }
                else {
                    if (pageCount == 0) {
                        $(".loadedContent").html("");
                    }
                }


                $("#loadingDiv").hide();
            }
            else {
                $("#loadMoreVideos").hide();
                $("html, body").animate({ scrollTop: $(document).height() }, "fast");
                $("#loadingDiv").hide();
                if (startIndex == 0) {
                    $(".loadedContent").html("<span id='lblMessage'>You do not have videos in Video Room yet</span>");
                }
            }
        }
    });
}

function VideoSearchCriteriaChanged() {
    shouldLoadMyVideos = $('#chkShowMyVideos').is(':checked');
    shouldLoadLicensedVideos = $('#chkShowLicensedVideos').is(':checked');
    shouldLoadExternalVideos = $('#chkShowExternalProvidersVideos').is(':checked');

    shouldLoadAllVideos = shouldLoadMyVideos && shouldLoadLicensedVideos && shouldLoadExternalVideos;

    if (shouldLoadAllVideos) {
        $('#chkShowMyVideos').prop('checked', true);
        $('#chkShowLicensedVideos').prop('checked', true);
        $('#chkShowExternalProvidersVideos').prop('checked', true);

        shouldLoadMyVideos = true;
        shouldLoadLicensedVideos = true;
        shouldLoadExternalVideos = true;
    }

    pageIndex = 1;

    this.LoadMoreResults();
}

// ------------------------------------------------------------------------------------------------------------------
// Search Video Room functions - END
//-------------------------------------------------------------------------------------------------------------------

function ClearActiveTabs() {
    //$("#SearchPublicLibrary").removeClass("active").hide();
    //$("#SearchByKeywords").removeClass("active").hide();
    //$("#SearchByUrl").removeClass("active").hide();
    //$("#SearchVideoRoom").removeClass("active").hide();
    //$("#btnSearchByURL").hide();
    //$("#txtKeyword").hide();
    //$("#btnSearchByKeyword").hide();
    //$("#txtURL").hide();
    //$("#divKeywordCategory").hide();
    //$(".loadedContent").html("");
    //$(".inputHolderKeywords").hide();
    //$(".inputHolderUrl").hide();
    //$("#ddlVideoRoomCategory").hide();
    //$("#ddlPublicLibraryCategory").hide();
    //$("#divUrlCategory").hide();
    //$("#ddlByUrlCategory").hide();
    //$("#divKeywordCategory").hide();
    //$("#ddlByKeywordsCategory").hide();

    ToggleSearchPublicLibraryTabVisiblity(false);
    ToggleSearchByKeyWordsTabVisiblity(false);
    ToggleSearchByUrlTabVisiblity(false);
    ToggleSearchVideoRoomTabVisiblity(false);

    $(".loadedContent").html("");
    //$('#userYoutubeUploadsContent').empty();
};

function ToggleSearchPublicLibraryTabVisiblity(isVisible) {
    if (isVisible) {
        $("#SearchPublicLibrary").show();
        $("#ddlPublicLibraryCategory").show();
    }
    else {
        $("#SearchPublicLibrary").hide();
    }
};

function ToggleSearchByKeyWordsTabVisiblity(isVisible) {
    if (isVisible) {
        $("#SearchByKeyWords").show();
        $(".inputHolderKeywords").show();
        $("#divUrlCategory").show();
    }
    else {
        $("#SearchByKeyWords").hide();
    }
};

function ToggleSearchByUrlTabVisiblity(isVisible) {
    if (isVisible) {
        $("#SearchByUrl").show();
        //$(".inputHolderUrl").show();
    }
    else {
        $("#SearchByUrl").hide();
    }
};

function ToggleSearchVideoRoomTabVisiblity(isVisible) {
    if (isVisible) {
        $("#SearchVideoRoom").show();
        $("#ddlVideoRoomCategory").show();
    }
    else {
        $("#SearchVideoRoom").hide();
    }
};

function addVideoToChannel(btn) {
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var videoTubeId = idArr[1];


    $.ajax({
        type: "POST",
        url: webMethodAddVideoToChannel,
        cashe: false,
        data: '{"channelTubeId":' + channelId + ',"videoTubeId":' + videoTubeId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var data = response.d;

            if (data && data.Message == null) {
                var videos = [data];
                var pageType = "schedule";
                var videoControls = Controls.BuildVideoBoxControlForSchedulePage(videos, true);
                var existingHtml = $(".videoBoxHolder").html();

                $("#recentlyadded_" + videoTubeId).text("in channel");
                $("#addvideosId_" + videoTubeId).removeAttr("onclick").hide();

                $(".videoBoxHolder").empty().append(videoControls).append(existingHtml);
                RemoveNoVideosMessage();
            }
            else {
                if (data) {
                    alertify.error(data.Message);
                }
            }

            LoadChannelCategories();
        }
    });
};

function RemoveVideoFromVideoRoom(btn) {
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var videoTubeId = idArr[1];
    //  console.log("here RemoveVideoFromVideoRoom schedule js");
    $.ajax({
        type: "POST",
        url: webMethodRemoveVideoFromVr,
        cashe: false,
        data: '{"userId":' + userId + ',"videoTubeId":' + videoTubeId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#addBoxContent_" + videoTubeId).hide().css("visibility", "hidden");

            var child = document.getElementById('boxContent_' + videoTubeId);
            var videoRommChild = $("#addBoxContent_" + videoTubeId);
            // console.log(child);
            if (child) {
                var parent = document.getElementById('ContentPlaceHolder1_videoBoxHolder');
                parent.remove(child);
            }
            if (videoRommChild) {

                $('div[data-videoTubeId="' + videoTubeId + '"]').hide();
                // var element = $("#popUpOverlay .videoBoxNew#addBoxContent_" + videoTubeId).css()
                // console.log(btnId)
                // parent.remove(videoRommChild);
            }

            LoadVideoRoomTabCategories(false);
        }
    });
};

function addExternalVideoToChannel(btn) {
    debugger;
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var providerVideoId = "";

    if (idArr.length == 2) {
        providerVideoId = idArr[1];
    }
    else if (idArr.length > 2) {
        $.each(idArr, function (i, p) {
            if (i > 0) {
                providerVideoId += p;
            }

            if (i > 0 && i < idArr.length - 1) {
                providerVideoId += "_";
            }
        });
    }

    var providerName = $("#addBoxContent_" + providerVideoId).attr("data-provider");
    var categoryId = 0;
    if (activeTab == 1) {
        categoryId = $("#ddlByKeywordsCategory option:selected").val();
    }
    else if (activeTab == 2) {
        categoryId = $("#ddlByUrlCategory option:selected").val();
        var isuloadsselected = $('.importYourVideosradio').is(':checked');
        if (isuloadsselected == true) {
            categoryId = $("#ddlYoutubeUserUploadsCategory option:selected").val();
        }
    }

    var isRRated = $("#addBoxContent_" + providerVideoId).attr("data-maturecontent");

    if (categoryId == 0 || categoryId == undefined) {
        alertify.alert("Please select a category.");
    }
    else {
        if (providerName == 'youtube') {
            var selectedVideoDuration = $("#addBoxContent_" + providerVideoId).attr("data-duration");
            if (selectedVideoDuration == 0) {

                ShowAddDurationPopup(providerVideoId, categoryId);
            }
            else {
                $.ajax({
                    type: "POST",
                    url: webMethodAddExternalVideoToChannel,
                    cashe: false,
                    data: '{"providerVideoId":\'' + providerVideoId + '\', "channelTubeId":' + channelId + ',"categoryId":' + categoryId + '}',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {

                        var data = response.d;

                        if (data && data.Message == null) {
                            var videos = [data];



                            var pageType = "schedule";
                            var videoControls = Controls.BuildVideoBoxControlForSchedulePage(videos, true);
                            var existingHtml = $(".videoBoxHolder").html();

                            $("#recentlyadded_" + providerVideoId).text("in channel");
                            $("#addvideosId_" + providerVideoId).removeAttr("onclick").hide();

                            $(".videoBoxHolder").empty().append(videoControls).append(existingHtml);
                            RemoveNoVideosMessage();


                        }
                        else {
                            if (data) {
                                alertify.error(data.Message);
                            }
                        }

                        LoadChannelCategories(categoryId);
                    }
                });
            }

        }
        else if (providerName == 'dailymotion') {
            AddDmotionVideoToChannel(providerVideoId);
        }
        else {
            var urlX = 'https://api.vimeo.com/videos/' + providerVideoId + '?access_token=' + accessToken;
            $.getJSON(urlX, function (data) {

                var VideoTubeModel = [];

                $.getJSON(urlX, function (data) {

                    if (data != null) {

                        var vimeoVideo = new Object();
                        var r = /(videos|video|channels|\.com)\/([\d]+)/, uri = data.uri;
                        var id = uri.match(r)[2];

                        vimeoVideo.VideoTubeId = 0;
                        vimeoVideo.ProviderVideoId = id.toString();
                        vimeoVideo.Title = data.name;
                        vimeoVideo.Description = data.description;
                        vimeoVideo.Duration = data.duration;
                        //vimeoVideo.CategoryId=
                        vimeoVideo.VideoProviderId = 2
                        vimeoVideo.IsRRated = isRRated
                        vimeoVideo.IsRemovedByProvider = false
                        if ((data.status == "available") && (data.privacy.embed == 'public')) {
                            vimeoVideo.IsRestrictedByProvider = false;
                        }
                        else {
                            vimeoVideo.IsRestrictedByProvider = true;

                        }

                        vimeoVideo.IsInPublicLibrary = false;
                        if (data.privacy.view == "anybody") {
                            vimeoVideo.IsPrivate = false;
                        }
                        else {
                            vimeoVideo.IsPrivate = true;
                        }

                        if (data.pictures && data.pictures.sizes.length >= 3) {
                            vimeoVideo.Thumbnail = data.pictures.sizes[2].link;
                        }

                        vimeoVideo.VideoProviderName = "vimeo";
                        //vimeoVideo.CategoryName
                        vimeoVideo.IsScheduled = false;
                        vimeoVideo.UseCounter = 0;
                        vimeoVideo.ViewCounter = 0;
                        vimeoVideo.DurationString = GetDurationString(data.duration);
                        vimeoVideo.IsInChannel = false;
                        vimeoVideo.Message = "";
                        vimeoVideo.DateAdded = Date.now;
                        vimeoVideo.CategoryId = categoryId;
                        //console.log(vimeoVideo)
                        VideoTubeModel.push(vimeoVideo);
                        $.ajax({
                            type: "POST",
                            url: webMethodAddExternalVideoVimeoToChannel,
                            cashe: false,
                            data: '{"videoTubeModel":' + JSON.stringify(vimeoVideo) + ',"channelTubeId":' + channelId + ',"categoryId":' + categoryId + '}',
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                var data = response.d;

                                if (data && data.Message == null) {
                                    var videos = [data];
                                    var pageType = "schedule";
                                    var videoControls = Controls.BuildVideoBoxControlForSchedulePage(videos, true);
                                    var existingHtml = $(".videoBoxHolder").html();

                                    $("#recentlyadded_" + providerVideoId).text("in channel");
                                    $("#addvideosId_" + providerVideoId).removeAttr("onclick").hide();

                                    $(".videoBoxHolder").empty().append(videoControls).append(existingHtml);
                                    RemoveNoVideosMessage();
                                }
                                else {
                                    if (data) {
                                        alertify.error(data.Message);
                                    }
                                }

                                LoadChannelCategories(categoryId);
                            }
                        });



                    }
                    else {
                        $("#loadMoreVideos").hide();
                    }




                    //   console.log(VideoTubePageModel);

                })
            });
        }
    }
}
function ClearAndCloseDurationPopup() {
    $('#durationPopup').trigger('close');
}
function ShowAddDurationPopup(providerVideoId, categoryId) {
    $('#durationPopup').lightbox_me({
        centered: true,
        onLoad: function () {

            $("#durationPopup #proceedAddVideo").attr("onclick", 'ProceedAddVideoToCahnnel("' + providerVideoId + '",' + categoryId + ')');
            $("#durationPopup #customDurationHour, #durationPopup #customDurationMinutes, #durationPopup #customDurationSeconds").val("");

        },
        onClose: function () {
            $("#durationPopup #proceedAddVideo").removeAttr("onclick");
            $("#durationPopup #customDurationHour, #durationPopup #customDurationMinutes, #durationPopup #customDurationSeconds").val("");
        }
    });
}

function ProceedAddVideoToCahnnel(providerVideoId, categoryId) {

    var hour = $("#customDurationHour").val();
    var min = $("#customDurationMinutes").val();
    var sec = $("#customDurationSeconds").val();
    var customDuration = parseInt(hour * 3600) + parseInt(min * 60) + parseInt(sec);
    // console.log(typeof (providerVideoId))

    $.ajax({
        type: "POST",
        url: webMethodAddExternalVideoToChannelWithCustomDuration,
        cashe: false,
        data: '{"providerVideoId":' + "'" + providerVideoId + "'" + ', "channelTubeId":' + channelId + ',"categoryId":' + categoryId + ',"customDuration":' + customDuration + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            var data = response.d;

            if (data && data.Message == null) {
                var videos = [data];



                var pageType = "schedule";
                var videoControls = Controls.BuildVideoBoxControlForSchedulePage(videos, true);
                var existingHtml = $(".videoBoxHolder").html();

                $("#recentlyadded_" + providerVideoId).text("in channel");
                $("#addvideosId_" + providerVideoId).removeAttr("onclick").hide();

                $(".videoBoxHolder").empty().append(videoControls).append(existingHtml);
                RemoveNoVideosMessage();
                ClearAndCloseDurationPopup();

            }
            else {
                if (data) {
                    alertify.error(data.Message);
                }
            }

            LoadChannelCategories(categoryId);
        }
    });
}

function DisplayNoVideoMessageIfNeeded() {
    var hiddenVideos = $(".channelsHolder .videoBoxNew:hidden");
    var allVideos = $(".channelsHolder .videoBoxNew");

    if (!isInSearchModeOnStudioPage) {
        if (hiddenVideos && allVideos) {
            if (hiddenVideos.length == allVideos.length) {
                AddNoVideosMessage();
            }
            else {
                RemoveNoVideosMessage();
            }
        }
    }
}

function RemoveVideoFromChannel(btn) {

    var r = confirm("Are you sure you want to delete this video?")
    if (r == true) {
        var btnId = btn.id;
        var idArr = btnId.split("_");
        var videoTubeId = idArr[1];
        void 0;
        $.ajax({
            type: "POST",
            url: webMethodRemoveFromChannel,
            cashe: false,
            data: '{"channelTubeId":' + channelId + ',"videoTubeId":' + videoTubeId + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#boxContent_" + videoTubeId).hide().css("visibility", "hidden");
                LoadChannelCategories();
                DisplayNoVideoMessageIfNeeded();
                LoadChannelTubesSchedulesByDate(channelId, myCalendarMain.getDate());
            }
        });
    }
    else {
        return;
    }

};
function RemoveLiveVideoFromChannel(btn) {
    var r = confirm("Are you sure you want to delete this video?")
    if (r == true) {
        var btnId = btn.id;
        var idArr = btnId.split("_");
        var videoTubeId = idArr[1];
        void 0;
        $.ajax({
            type: "POST",
            url: webMethodRemoveLiveFromChannel,
            cashe: false,
            data: '{"channelTubeId":' + channelId + ',"videoLiveTubeId":' + videoTubeId + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#boxContentLive_" + videoTubeId).remove();
                LoadChannelCategories();
                var wrapper = $('.videoBoxHolder');
                var liveVideos = wrapper.find(".videoBoxNew");
                void 0;
                if (liveVideos.length == 0) {
                    wrapper.empty().append("<span id='novideosmsg'>There are no selected videos in your channel</span><a id='btnBackToAll' onclick='LoadMoreVideos(true)'>Back to all videos</a>");
                    $("#ddlChannelVideos option[value='0']").attr("selected", true);
                }
               
                
            }
        });
    }
    else {
        return;
    }
};

function removeClass(element, className) {
    var newClassName = "";
    var i;
    var classes = element.className.split(" ");
    for (i = 0; i < classes.length; i++) {
        if (classes[i] !== className) {
            newClassName += classes[i] + " ";
        }
    }
    this.className = newClassName;
};

function HighLightVideoBox(elemnt) {
    //console.log(elemnt);
    var id = $("#" + elemnt.id).attr('data-boxcontentid');
    //console.log(id)
    $(".videoBoxNew").each(function () {
        //  $(this).css("border", "1px solid #eee");
        $(this).removeClass("highlight")
    });
    $(".videoBoxNew.videoBoxWhatched").css("border", "1px solid #f18a11");
    var boxContent = "boxContent_" + id;
    var boxContentLive = "boxContentLive_" + id;

    var addBoxContent = "addBoxContent_" + elemnt.id;
    if (boxContent && id != undefined) {
        $("#" + boxContent).addClass('highlight');
        // $("#" + boxContent).css("border", "1px solid #f18a11");
        $("#" + addBoxContent).addClass('highlight');
        // $("#" + addBoxContent).css("border", "1px solid #f18a11");
        //console.log(boxContent);
    }
    else if (boxContentLive && id != undefined) {
        $("#" + boxContentLive).addClass('highlight');
        // $("#" + boxContent).css("border", "1px solid #f18a11");
        $("#" + addBoxContent).addClass('highlight');
        // $("#" + addBoxContent).css("border", "1px solid #f18a11");
        //console.log(boxContent);
    }
    else if (addBoxContent) {
        $("#" + addBoxContent).addClass('highlight');
        //$("#" + addBoxContent).css("border", "1px solid #f18a11");
    }


};



function showPlayer(elemnt) {
    if (elemnt.parentElement.parentElement.id.indexOf("addBoxContent") == 0) {
        $(".loadedContent").find("div.videoBoxInfo").removeClass("videoBoxInfoWhatched")

        if (!$("#" + elemnt.parentElement.parentElement.id).find("div.videoBoxInfo").hasClass("videoBoxInfoWhatched")) {
            $("#" + elemnt.parentElement.parentElement.id).find("div.videoBoxInfo").addClass("videoBoxInfoWhatched");
        }
    }

    $('.playerbox').lightbox_me({
        centered: true,
        onLoad: function () {
            addPlayer(elemnt);
            HighLightVideoBox(elemnt);
        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {

            player = new YT.Player('related')
            $(".playerbox").html('<div id="related" class="player"></div> <div id="content-container"></div><a id="close_x" class="close close_x closePlayerBox" href="#">close</a>')
            player.destroy();
            RemoveOverlay();
        }
    });
};

function showPlayerVimeo(element) {
    $('.playerboxVimeo').lightbox_me({
        centered: true,
        onLoad: function () {

            if (element.parentElement.parentElement.id.indexOf("addBoxContent") == 0) {
                $(".loadedContent").find("div.videoBoxInfo").removeClass("videoBoxInfoWhatched")

                if (!$("#" + element.parentElement.parentElement.id).find("div.videoBoxInfo").hasClass("videoBoxInfoWhatched")) {
                    $("#" + element.parentElement.parentElement.id).find("div.videoBoxInfo").addClass("videoBoxInfoWhatched");
                }
            }

            initPlayer(element);
            HighLightVideoBox(element);
        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {
            $("#vimeoBox .playerVimeo").empty();
            //vimeoPlayer = new YT.Player('related')
            //$(".playerbox").html('<div id="related" class="player"></div> <div id="content-container"></div><a id="close_x" class="close close_x closePlayerBox" href="#">close</a>')
            //vimeoPlayer.destroy();
            RemoveOverlay();
        }
    });
};

function showPlayerDmotion(element) {
    $('.playerBoxDMotion').lightbox_me({
        centered: true,
        onLoad: function () {
            initDMotionPlayer(element);
            //console.log(element);
            HighLightVideoBox(element);
        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {

            $("iframe.playerDmotion").remove();
            $(".playerBoxDMotion").html("<div class='playerDmotion' id='playerDmotion'></div> <a id='close_x' class='close close_x closePlayerBox' href='#'></a>");


            RemoveOverlay();
        }
    });
};

function ShowReactPlayer(element) {

    $('.playerBoxReactplayer').lightbox_me({
        centered: true,
        onLoad: function () {
            initReactPlayer(element);
            //console.log(element);
            //HighLightVideoBox(element);
        },
        overlayCSS: {
            background: 'black',
            opacity: .8,
        },
        onClose: function () {
            window.STRIMM_PLAYER = {
                url: null,
                startDate: null,
            }
            //window.STRIMM_PLAYER.url = null;
            //$("#STRIMM_PLAYER_ROOT").remove();
            //$(".playerBoxReactplayer").html("<div id='STRIMM_PLAYER_ROOT'></div> <a id='close_x' class='close close_x closePlayerBox' href='#'></a>");
            RemoveOverlay();
        }
    });
}

function ShowFlowPlayer(element) {

    $('.playerBoxFlowplayer').lightbox_me({
        centered: true,
        onLoad: function () {
            initFlowpplayer(element);
            //console.log(element);
            HighLightVideoBox(element);
        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {

            $(".playerFlowpplayer").remove();
            $(".playerBoxFlowplayer").html("<div class='playerFlowpplayer' id='playerFP'></div> <a id='close_x' class='close close_x closePlayerBox' href='#'></a>");


            RemoveOverlay();
        }
    });
}
function initReactPlayer(element) {
    var newPlaylist = [];
    var providerName = $("#" + element.id).attr("data-provider");
    var source = $("#" + element.id).attr("data-url");
    var typeValue = "video/" + providerName;
    var videobox = $("#" + element.id).find(".VideoBoxPlay")[0];
    var videoid = videobox.id;
    if (providerName == "custom") {
        source = $(videobox).attr("data-url");
        var ext = source.substr((source.lastIndexOf('.') + 1));
        if (source.lastIndexOf('www.twitch.tv') >= 0) {
            ext = "twitch";
            source += "?parent=stagingst.strimm.com";
        }
        if (source.lastIndexOf('www.facebook.com') >= 0)
            ext = "facebook";
        typeValue = "video/" + ext;
    }
    if (providerName == "youtube") {
        source = "https://www.youtube.com/watch?v=" + videoid;
    }
    if (providerName == "dailymotion") {
        source = "https://www.dailymotion.com/video/" + videoid;
    }
    if (providerName == "vimeo") {
        source = "https://vimeo.com/" + videoid;
    }
    if (!source.startsWith("https"))
        source = "https:" + source;
    var playlistItem = { sources: [{ type: typeValue, src: source }] };
    newPlaylist.push(playlistItem);
    debugger;
    window.STRIMM_PLAYER = {
        url: source,
        startDate: new Date()
    }
}
function initFlowpplayer(element) {
    //  console.log(element);
    var newPlaylist = [];
    var providerName = $("#" + element.id).attr("data-providerName");
    var source = $("#" + element.id).attr("data-url");
    var typeValue = "video/" + providerName;

    if (providerName == "custom") {
        var ext = source.substr((source.lastIndexOf('.') + 1));
        typeValue = "video/" + ext;

    }
    var playlistItem = { sources: [{ type: typeValue, src: source }] };

    newPlaylist.push(playlistItem);
    // console.log(newPlaylist);
    !function () {

        var player = flowplayer("#playerFP", {
            autoplay: true,
            playlist: [
            {
                sources: [
                { type: typeValue, src: source }
                ]
            }

            ]
        }).one("ready", function (e, api, data) {
            $(".fp-player").next().remove();
            api.play();

        });
    }();



}
function initPlayer(element) {
    var iframe = "<div id='divVimeoPlayer'><iframe id='myVideo' class='video-tracking' src='https://player.vimeo.com/video/" + element.id + "?autoplay=1;&amp;color=a4a9ab' width='800' height='450' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>";
    $("#vimeoBox .playerVimeo").html(iframe);

    window.$('.divVimeoPlayer').each(function () {

        // get the vimeo player(s)
        var iframe = window.$(this);
        var player = window.$f(iframe[0]);
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
function sortVideosForChannel(select) {
    var allvideoboxes = $("#ContentPlaceHolder1_videoBoxHolder").find("div.channelBox");
    var wrapper = $('.videoBoxHolder');

    var value;
    var selectedValue = select.value;
    if (selectedValue !== null && selectedValue !== undefined) {
        value = selectedValue;
    }
    else {
        value = 0;
    }

    sortVideos(allvideoboxes, value, wrapper);
}

function sortVideosForAdd(select) {
    var allvideoboxes = $(".loadedContent").find("div.videoBoxNew");
    var wrapper = $('.loadedContent');

    var value;
    var selectedValue = select.value;
    if (selectedValue !== null && selectedValue !== undefined) {
        value = selectedValue;
    }
    else {
        value = 0;
    }

    sortVideos(allvideoboxes, value, wrapper);
}

function SortVideosForChannel(select) {
    var allvideoboxes = $(".channelsHolder").find("div.videoBoxNew");
    var wrapper = $('.videoBoxHolder');

    var value;
    var selectedValue = select.value;
    if (selectedValue !== null && selectedValue !== undefined) {
        value = selectedValue;
    }
    else {
        value = 0;
    }

    sortVideos(allvideoboxes, value, wrapper);
}

function sortVideos(videoBoxes, select, wrapper) {
    

    switch (select) {


        case "1":
            var sorted = videoBoxes.sort(function (a, b) {
                var aDuration = b.getAttribute('data-duration');
                var bDuration = a.getAttribute('data-duration');
                var result = aDuration - bDuration;
                return result;
            });
            if (sorted.length != 0)
            {
                wrapper.empty().append(sorted);
            }
            else
            {
                wrapper.empty().append("<span id='novideosmsg'>There are no selected videos in your channel</span><a id='btnBackToAll' onclick='LoadMoreVideos(true)' >Back to all videos</a>");
                $("#ddlChannelVideos option[value='0']").attr("selected", true);
            }
            
            break;
        case "2":
            var sorted = videoBoxes.sort(function (a, b) {
                var aDuration = b.getAttribute('data-duration');
                var bDuration = a.getAttribute('data-duration');
                var result = bDuration - aDuration;
                return result;
            });
            if (sorted.length != 0) {
                wrapper.empty().append(sorted);
            }
            else {
                wrapper.empty().append("<span id='novideosmsg'>There are no selected videos in your channel</span><a id='btnBackToAll' onclick='LoadMoreVideos(true)' >Back to all videos</a>");
                $("#ddlChannelVideos option[value='0']").attr("selected", true);
            }
            break;
        case "3":
            var sorted = videoBoxes.sort(function (a, b) {
                var aDuration = b.getAttribute('data-views');
                var bDuration = a.getAttribute('data-views');
                var result = aDuration - bDuration;
                return result;
            });
            if (sorted.length != 0) {
                wrapper.empty().append(sorted);
            }
            else {
                wrapper.empty().append("<span id='novideosmsg'>There are no selected videos in your channel</span><a id='btnBackToAll' onclick='LoadMoreVideos(true)' >Back to all videos</a>");
                $("#ddlChannelVideos option[value='0']").attr("selected", true);
            }
            break;
        case "4":
            var sorted = videoBoxes.sort(function (a, b) {
                var aDateAdded = ((new Date(b.getAttribute('date-added'))).getTime() * 10000) + 621355968000000000;
                var bDateAdded = ((new Date(a.getAttribute('date-added'))).getTime() * 10000) + 621355968000000000;
                var result = -bDateAdded + aDateAdded;
                return result;
            });
            if (sorted.length != 0) {
                wrapper.empty().append(sorted);
            }
            else {
                wrapper.empty().append("<span id='novideosmsg'>There are no selected videos in your channel</span><a id='btnBackToAll' onclick='LoadMoreVideos(true)' >Back to all videos</a>");
                $("#ddlChannelVideos option[value='0']").attr("selected", true);
            }
            break;
        case "5":
            var liveVideos = wrapper.find("[data-islive='true']");
            //var sorted = videoBoxes.sort(function (a, b) {
            //    var isLive = a.getAttribute('data-islive');
            //    console.log(isLive);
            //    return isLive=="true";
            //});
            void 0
            if (liveVideos.length != 0)
            {
                wrapper.empty().append(liveVideos);
            }
            else
            {
                wrapper.empty().append("<span id='novideosmsg'>There are no selected videos in your channel</span><a id='btnBackToAll' onclick='LoadMoreVideos(true)'>Back to all videos</a>");
                $("#ddlChannelVideos option[value='0']").attr("selected", true);
            }
            
            break;
        case "6":

            var privateVideos = wrapper.find("[data-isprivate='true']");
            if (privateVideos.length != 0)
            {
                wrapper.empty().append(privateVideos);
            }
            else
            {
               
                wrapper.empty().append("<span id='novideosmsg'>There are no selected videos in your channel</span><a id='btnBackToAll' onclick='LoadMoreVideos(true)' >Back to all videos</a>");
                $("#ddlChannelVideos option[value='0']").attr("selected", true);
                    
            }
            //var sorted = videoBoxes.sort(function (a, b) {
            //    var isLive = a.getAttribute('data-islive');
            //    console.log(isLive);
            //    return isLive=="true";
            //});
           
            break;

        case "7":
            wrapper.empty()
            LoadMoreVideos(true);

            break;
    }
};

//-------------------------------------------------------------------------
// Events Calendar functions
//-------------------------------------------------------------------------
function InitializeEventsCalendar() {
    if (mobile) {
        myCalendarMain = new dhtmlXCalendarObject("calendarHolderMobile");
    }
    else {
        myCalendarMain = new dhtmlXCalendarObject("calendarHolder");
    }

    myCalendarMain.hideTime();
    myCalendarMain.show();

    var date = new Date();
    var curr_date = date.getDate();
    var curr_month = date.getMonth() + 1;
    var curr_year = date.getFullYear();

    if (curr_date < 10) curr_date = "0" + curr_date;
    if (curr_month < 10) curr_month = "0" + curr_month;

    var nowDate = curr_year + "-" + curr_month + "-" + curr_date;

    myCalendarMain.setDate(nowDate);

    LoadChannelTubesSchedulesByDate(channelId, date);
    LoadChannelScheduleCalendarEvents(curr_month, curr_year);

    myCalendarMain.attachEvent("onClick", function (d) {
        var hasEmptySchedules = false;
        var deleteEmptySchedules = false;

        if (todaysChannelTubes && todaysChannelTubes.length > 0) {
            $.each(todaysChannelTubes, function (i, c) {
                if (c.EndTime == "") {
                    hasEmptySchedules = true;
                }
            });
        }

        var shouldLoadChannelSchedules = false;

        if (hasEmptySchedules) {
            LoadChannelTubesSchedulesByDate(channelId, d);
            //JIRA-167: Removing popup
            //alertify.confirm("Proceeding with the requested action will remove your empty schedule. Do you want to proceed?", function (e) {
            //    if (e) {
            //        LoadChannelTubesSchedulesByDate(channelId, d);
            //    }
            //    else {
            //        myCalendarMain.setDate(pickedDateTime);
            //    }
            //}); 
        }
        else {
            var isPublished = false;
            var scheduleIds = [];

            if ($(".ancPublishSchedule").is(":visible")) {
                $.each($(".ancPublishSchedule"), function (i, c) {
                    $pelement = $('#' + c.id);
                    if ($pelement.hasClass("published") || $pelement.hasClass("publishedDisactivated")) {
                        isPublished = true;
                    }
                    else {
                        isPublished = false;
                        scheduleIds.push(c.id);
                    }
                });

                //if ($(".ancPublishSchedule").hasClass("published") || $(".ancPublishSchedule").hasClass("publishedDisactivated")) {
                //    isPublished = true;
                //}
                //else {
                //    isPublished = false;
                //}

                if (isPublished == false) {

                    alertify.set({
                        'labels': { ok: 'Publish', cancel: 'Cancel' },
                        'defaultFocus': 'Publish'
                    });
                    alertify.confirm("One or more inactive schedules detected. Click PUBLISH to activate your schedule(s) or CANCEL to continue without activating.", function (e) {
                        if (e) {
                            $.each(scheduleIds, function (i, c) {
                                var btnId = c; //$(".ancPublishSchedule")[0].id;
                                var idArr = btnId.split("_");
                                var channelScheduleId = idArr[1];
                                Publish(channelScheduleId);
                            });
                            myCalendarMain.setDate(pickedDateTime);
                        }
                        else {
                            LoadChannelTubesSchedulesByDate(channelId, d);
                        }
                    });
                }
                else {
                    LoadChannelTubesSchedulesByDate(channelId, d);
                }
            }
            else {
                LoadChannelTubesSchedulesByDate(channelId, d);
            }
        }
    });

    myCalendarMain.attachEvent("onChange", function (d) {
        LoadChannelScheduleCalendarEvents(d.getMonth(), d.getYear());
        UpdateAvailableScheduleTimes();
    });

    myCalendarMain.attachEvent("onArrowClick", function (d_old, d_new) {
        LoadChannelScheduleCalendarEvents(d_new.getMonth(), d_new.getYear());
        UpdateAvailableScheduleTimes();
    });
}

function DeleteEmptySchedules() {
    if (todaysChannelTubes && todaysChannelTubes.length > 0) {
        $.each(todaysChannelTubes, function (i, c) {
            if (c.EndTime == "") {
                DeleteChannelScheduleById(c.ChannelScheduleId);
            }
        });
    }
}

function LoadChannelScheduleCalendarEvents(month, year) {
    var date = new Date();
    var tmonth = month || date.getMonth();
    var tyear = year || date.getYear();

    $.ajax({
        type: "POST",
        url: webMethodGetChannelScheduleCalendarEvents,
        data: '{"month":' + month + ',"year":' + year + ',"channelTubeId":' + channelId + '}',
        cashe: false,
        dataType: "json",
        contentType: "application/json",
        success: function (response) {

            var events = response.d;
            $.each(events, function (i, d) {
                var date = new Date(events[i].StartTime);
                var toolTip = "<span style='color:red;'>" + events[i].Message + "</span>";
                myCalendarMain.setTooltip(date, toolTip, true, true);
            });
        }
    });
};

var todaysChannelTubes;

function LoadChannelTubesSchedulesByDate(channelId, date) {

    //CheckIfIsPublished();
    void 0
    $("#loadingDiv").show();

    pickedDateTime = date;
    var targetDate = date;
    var month = date.getMonth() + 1;
    var targetChannelId = channelId || 0;
    var month = targetDate.getMonth() + 1;

    var todaysDay = (new Date()).getDate();
    var pickedDay = pickedDateTime.getDate();

    var diff = pickedDateTime.getTime() - (new Date()).getTime();

    DisableNewScheduleButton(diff < 0 && todaysDay != pickedDay);
    DisableInstantScheduleButton(diff < 0 && todaysDay != pickedDay);
    
    var requestData = '{channelTubeId: "' + targetChannelId + '" , scheduleDate: "' + month + '-' + targetDate.getDate() + '-' + targetDate.getFullYear() + '"}';

    if (targetChannelId) {
        $.ajax({
            type: "POST",
            url: webMethodGetChannelSchedulesByDate,
            data: requestData,
            cashe: false,
            dataType: "json",
            contentType: "application/json",
            success: function (response) {
                debugger;
                todaysChannelTubes = response.d;
                void 0;
                $(".scheduleThumbHolder").html("");

                $.each(todaysChannelTubes, function (i, d) {
                    var dayschedule = Controls.BuildChannelScheduleWithVideoForEdit(d);

                    $(".scheduleThumbHolder").append(dayschedule);

                    ExpandVideoScheduleList(d.ChannelScheduleId);

                    if (d.VideoSchedules.length == 1) {
                        activeChannelScheduleId = d.ChannelScheduleId;
                    }
                    else {
                        activeChannelScheduleId = 0;
                    }

                    //DisableRepeatButtonsOnChannelSchedules(isAutoPilotOn);

                    var now = new Date();
                    var todaysDay = now.getDate();
                    var pickedDay = pickedDateTime.getDate();

                    if (todaysDay == pickedDay && d.EndDateAndTime) {
                        var startTime = new Date(parseInt(d.StartDateAndTime.substr(6)));
                        var endTime = new Date(parseInt(d.EndDateAndTime.substr(6)));
                        var startMils = startTime.getMinutes() + startTime.getHours() * 60;
                        var endMils = endTime.getMinutes() + endTime.getHours() * 60;
                        var nowMils = now.getMinutes() + now.getHours() * 60;

                        var shouldDisableInstantSchedule = startMils <= nowMils && endMils >= nowMils;
                        if (shouldDisableInstantSchedule) {
                            DisableInstantScheduleButton(true);
                        }
                    }

                    if (isBasicMode) {
                        $(".ancRepeat").toggle();
                    }
                });

                var now = new Date();
                now.setMonth(date.getMonth());
                now.setDate(date.getDate());
                now.setYear(date.getFullYear());
                var formatedDate = "Schedule for " + GetFormattedScheduleDate(date);
                $("#divPickedDate span#spnPickedDate").text(formatedDate);
                $("#newScheduleHeader span#newScheduleDate").text(GetFormattedScheduleDate(date));//date.format("F j, Y"));

                $("#loadingDiv").hide();

                if (todaysChannelTubes && todaysChannelTubes.length > 0) {
                    var scheduleEndTime = todaysChannelTubes[todaysChannelTubes.length - 1].EndDateAndTime;
                    var scheduleEntTimeDate = new Date(parseInt(scheduleEndTime.substr(6)));

                    var endHour = scheduleEntTimeDate.getHours();
                    var endMin = scheduleEntTimeDate.getMinutes();

                    DisableNewScheduleButton((endHour >= 23 && endMin > 15));
                }
                else {
                    DisableNewScheduleButton(false);
                }

                UpdateAvailableScheduleTimes();
            }
        });
    }
}

function GetFormattedScheduleDate(date) {
    // Saturday, December 1, 2014
    return moment(date).format("ddd, MMM DD, YYYY"); //date.format("l, F j, Y");
}

function DisableNewScheduleButton(isDisabled) {
    if (isDisabled) {
        $("#btnStartScheduleOk").removeAttr("onclick").addClass("disabled");
        $("#newScheduleStartTimePicker").addClass("disabled");
    }
    else {
        $("#btnStartScheduleOk").removeClass("disabled").attr("onclick", "StartNewScheduleCreation()");
        $("#newScheduleStartTimePicker").removeClass("disabled");
    }
};

function DisableRepeatButtonsOnChannelSchedules(isDisabled) {
    if (todaysChannelTubes) {
        $.each(todaysChannelTubes, function (i, c) {
            if (isDisabled) {
                $("#repeat_" + c.ChannelScheduleId).addClass("disabled").removeAttr("onclick");
            }
            else {
                $("#repeat_" + c.ChannelScheduleId).removeClass("disabled").attr("onclick", "ShowRepeatCalendar(this)");
            }
        });
    }
}

function DisableInstantScheduleButton(isDisabled) {
    if (isDisabled) {
        $("#btnInstantSchedule").removeClass('bntBasicInstantSchedule').addClass("bntBasicInstantScheduleDisabled").removeAttr("onclick");
    }
    else {
        $("#btnInstantSchedule").removeClass("bntBasicInstantScheduleDisabled").addClass("bntBasicInstantSchedule").attr("onclick", "CreateInstantSchedule()");
    }
};

function DisablePublishButton(channelScheduleId, isEmptySchedule) {
    if (isEmptySchedule) {
        $("#publish_" + channelScheduleId).addClass("disabled");
    }
    else {
        $("#publish_" + channelScheduleId).removeClass("disabled");
    }
}

function RetrieveAvailableChannels(callback) {
    var requestData = '{userId: "' + userId +  '"}';
    $.ajax({
        type: "POST",
        url: webMethodGetNamesOfAllChannelsForUser,
        data: requestData,
        cashe: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            userChannels = response.d;

            if (userChannels) {
                $.each(userChannels, function (i, c) {
                    $('.ddlAvailableChannels').append($('<option></option)').attr("value", c.ChannelTubeId).text(c.Name));
                    if (c.ChannelTubeId == channelId) {
                        $('.ddlAvailableChannels option[value="' + c.ChannelTubeId + '"]').prop('selected', true);
                    }
                });
            }
            else {
                alertify.error("Error occured while retrieving users channels.");
                window.location.href = "/";
            }
        }
    });
}

function ChangeActiveChannel() {
    var id = $(".ddlAvailableChannels option:selected").val();
    var url = GetTargetChannelStudioUrl(id);
    void 0;
    window.location.href = url;
}

function GetTargetChannelStudioUrl(id) {
    var url;
    if (userChannels) {
        $.each(userChannels, function (i, c) {
            if (c.ChannelTubeId == id) {
                url = "/" + c.ChannelOwnerUrl + "/my-studio/" + c.ChannelUrl;
            }
        });
    }
    return url;
}

function GetTargetChannelUrl(id) {
    var url;
    if (userChannels) {
        $.each(userChannels, function (i, c) {
            if (c.ChannelTubeId == id) {
                url = c.ChannelUrl;
            }
        });
    }
    return url;
}

//-------------------------------------------------------------------------
// Schedule creation
//-------------------------------------------------------------------------
function UpdateAvailableScheduleTimes() {

    var blockoutTimes = new Array();

    if (todaysChannelTubes) {
        blockoutTimes = new Array(todaysChannelTubes.length);
    }

    var now = new Date();
    var max = new Date();

    var diff = pickedDateTime.getTime() - now.getTime();

    isToday = (now.getDate() == pickedDateTime.getDate() && now.getMonth() == pickedDateTime.getMonth() && now.getYear() == pickedDateTime.getYear());

    if (!isToday && diff > 0) {
        isFuture = true;
    }
    else {
        if (!isToday) {
            isPast = true;
        }
    }

    $("#newScheduleStartTimePicker").remove();

    now.setDate(pickedDateTime.getDate());
    now.setMonth(pickedDateTime.getMonth());
    now.setYear(pickedDateTime.getFullYear());

    max.setDate(pickedDateTime.getDate());
    max.setMonth(pickedDateTime.getMonth());
    max.setYear(pickedDateTime.getFullYear());
    max.setMinutes(59);
    max.setHours(23);

    // Define blockout time when schedule can no longer be
    // created
    blockoutTimes[0] = ['0:00am', now.format("hh:mm tt")];

    var increment = 1;
    var lastScheduleEndTime;

    if (todaysChannelTubes && todaysChannelTubes.length > 0) {
        blockoutTimes[0] = ['0:00am', todaysChannelTubes[0].StartTime];
        $.each(todaysChannelTubes, function (i, s) {
            var bStart = s.StartTime;
            var bEnd = s.EndTime;

            lastScheduleEndTime = s.EndTime;

            if (bEnd == "") {
                bEnd = bStart;

            }

            blockoutTimes[i + increment] = [bStart, bEnd];
        });
    }
    else {
        blockoutTimes = new Array(todaysChannelTubes.length);
    }

    // Define possible schedule start time
    var min = now.getMinutes();

    min += 15;

    var remainder = min % 10;
    if (remainder > 0) {
        min += (10 - remainder);
    }

    now.setMinutes(min);

    if (isToday) {
        $("<input id='newScheduleStartTimePicker'  class='time ui-timepicker-input' placeholder='Pick start time ▼' type='text'/>").insertBefore("#btnStartScheduleOk");

        var isNextScheduleTimeToday = (now.getDate() == pickedDateTime.getDate() && now.getMonth() == pickedDateTime.getMonth() && now.getYear() == pickedDateTime.getYear());

        var hrs = 0;
        var mins = 0;

        if (lastScheduleEndTime != undefined && lastScheduleEndTime != '') {
            var hrsmins = lastScheduleEndTime.split(" ");
            var isPM = hrsmins[1] == "PM";
            hrsmins = hrsmins[0].split(":");
            hrs = isPM ? parseInt(hrsmins[0]) + 12 : parseInt(hrsmins[0]);
            mins = parseInt(hrsmins[1]);
        }

        if (hrs >= 23 && mins > 45) {
            if (mobile) {
                $("#customSchedule").removeAttr('onclick').attr('onclick', 'displayCustomSchedulePopup()');
                $("#customScheduleM").removeAttr('onclick').attr('onclick', 'displayCustomSchedulePopup()');
                $("#newScheduleStartTimePicker").timepicker({
                    'disableTimeRanges': blockoutTimes,
                    'step': 10,
                    'setTime': now,
                    'minTime': now,
                    'maxTime': max,
                    'forceRoundTime': true,
                    'typeaheadHighlight': true
                });
            }
            else {
                $("#customSchedule").removeAttr('onclick').attr('onclick', 'displayScheduleFullAlert()');
                $("#customScheduleM").removeAttr('onclick').attr('onclick', 'displayScheduleFullAlert()');
            }





        }
        else {
            $("#customSchedule").removeAttr('onclick').attr('onclick', 'displayCustomSchedulePopup()');
            $("#customScheduleM").removeAttr('onclick').attr('onclick', 'displayCustomSchedulePopup()');
            $("#newScheduleStartTimePicker").timepicker({
                'disableTimeRanges': blockoutTimes,
                'step': 10,
                'setTime': now,
                'minTime': now,
                'maxTime': max,
                'forceRoundTime': true,
                'typeaheadHighlight': true
            });
        }
    }
    else if (isFuture) {
        now.setMinutes(0);
        now.setHours(0);

        $("<input id='newScheduleStartTimePicker'  class='time ui-timepicker-input' placeholder='Pick start time ▼' type='text'/>").insertBefore("#btnStartScheduleOk");

        var hrs = 0;
        var mins = 0;

        if (lastScheduleEndTime != undefined && lastScheduleEndTime != '') {
            var hrsmins = lastScheduleEndTime.split(" ");
            var isPM = hrsmins[1] == "PM";
            hrsmins = hrsmins[0].split(":");
            hrs = isPM ? parseInt(hrsmins[0]) + 12 : parseInt(hrsmins[0]);
            mins = parseInt(hrsmins[1]);
        }

        if (hrs >= 23 && mins > 45) {
            $("#customSchedule").removeAttr('onclick').attr('onclick', 'displayScheduleFullAlert();');
            $("#customScheduleM").removeAttr('onclick').attr('onclick', 'displayScheduleFullAlert();');
        }
        else {
            $("#customSchedule").removeAttr('onclick').attr('onclick', 'displayCustomSchedulePopup()');
            $("#customScheduleM").removeAttr('onclick').attr('onclick', 'displayCustomSchedulePopup()');
            $("#newScheduleStartTimePicker").timepicker({
                'disableTimeRanges': blockoutTimes,
                'step': 10,
                'setTime': now,
                'minTime': now,
                'maxTime': max,
                'forceRoundTime': true,
                'typeaheadHighlight': true
            });
        }
    }
    else {
        // Cannot create custom schedule in the past
        // Need to make this disabled
        $("<input id='newScheduleStartTimePicker'  class='time ui-timepicker-input' placeholder='Pick start time ▼' type='text'/>").insertBefore("#btnStartScheduleOk");
        $("#newScheduleStartTimePicker").click(function () {
            alertify.alert("Custom schedule cannot be created in the past.");
        });
    }
};



function displayScheduleFullAlert() {
    if (isMobileDevice()) {
        alertify.set({
            labels: {
                ok: "Ok"
            },
            buttonFocus: "ok"
        });
        alertify.alert("This feature is only available on desktop.");
    }
    else {
        alertify.set({
            labels: {
                ok: "Ok"
            },
            buttonFocus: "ok"
        });
        alertify.alert("Unable to create a custom schedule. The day\'s schedule is full.");
    }

}

function StartNewScheduleCreation() {
    //CheckIfIsPublished();
    var pickedTime = $("#newScheduleStartTimePicker").timepicker('getTime', [new Date()]);

    if (pickedTime != null) {
        var hasEmptySchedules = false;
        var deleteEmptySchedules = false;

        if (todaysChannelTubes && todaysChannelTubes.length > 0) {
            $.each(todaysChannelTubes, function (i, c) {
                if (c.EndTime == "") {
                    hasEmptySchedules = true;
                }
            });
        }

        var shouldCreateSchedule = !hasEmptySchedules;

        if (hasEmptySchedules) {
            alertify.confirm("Proceeding with the requested action will remove your empty schedule. Do you want to proceed?", function (e) {
                if (e) {
                    shouldCreateSchedule = true;
                }
            });
        }

        if (shouldCreateSchedule) {
            pickedDateTime.setHours(pickedTime.getHours());
            pickedDateTime.setMinutes(pickedTime.getMinutes());

            CreateNewChannelSchedule();
        }
        else {
            UpdateAvailableScheduleTimes();
        }
    }

    $('#customScheduleCreatePopup').hide();
    RemoveOverlay();
}

//----------------------------------------------------------------------------------------------------------
// Removal of Videos from Channel
//----------------------------------------------------------------------------------------------------------
function RemoveRestrictedVideosFromChannel() {
    ToggleClearVideosMenu();
    var allvideoboxes = $(".videoBoxHolder  ").find("div.videoBoxNew");
    var restictedVideoIds = "";
    $.each(allvideoboxes, function (i, v) {
        var elementId = v.id;
        var idArr = elementId.split("_");
        var videoTubeId = idArr[1];
        var $actionLink = $("#restricted_" + videoTubeId);
        var actionText = $actionLink.text();
        if (actionText == "removed by provider" || actionText == "restricted by provider") {
            restictedVideoIds += videoTubeId;
            if (allvideoboxes.length - 1 > i) {
                restictedVideoIds += ",";
                $("#boxContent_" + videoTubeId).hide();
                $("#boxContentLive_" + videoTubeId).hide();
                DisplayNoVideoMessageIfNeeded();
            }
        }
    });

    if (restictedVideoIds != "") {
        $.ajax({
            type: "POST",
            url: webMethodClearRestrictedOrRemovedVideos,
            cashe: false,
            data: '{"channelId": ' + channelId + ', "videoIds": "' + restictedVideoIds + '"}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                alertify.success(response.d);
            }
        });
    }
}

function ClearAllVideosFromChannel() {
    ToggleClearVideosMenu();

    alertify.confirm("Are you sure you want to clear all videos from this channel?", function (e) {
        if (e) {
            $.ajax({
                type: "POST",
                url: webMethodRemoveAllVideosFromChannel,
                cashe: false,
                data: '{"channelId":' + channelId + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    $("#ContentPlaceHolder1_videoBoxHolder").html("");
                    AddNoVideosMessage();
                    LoadChannelTubesSchedulesByDate(channelId, myCalendarMain.getDate());
                    DisplayNoVideoMessageIfNeeded();
                    alertify.success(response.d);
                }
            });
        }
    });
};

function CheckIfIsPublished() {
    var isPublished = false;
    if ($(".ancPublishSchedule").is(":visible")) {
        if ($(".ancPublishSchedule").hasClass("published") || $(".ancPublishSchedule").hasClass("publishedDisactivated")) {
            isPublished = true;
        }
        else {
            isPublished = false;
        }

        if (isPublished == false) {
            alertify.success("Schedule is not active. Click PUBLISH to activate your schedule.");
            //alertify.confirm("please click 'Publish', if you want to activate your schedule", function (e) {
            //    if (e) {
            //        isSchedulePublished = true;
            //        var btnId = $(".ancPublishSchedule")[0].id;
            //        var idArr = btnId.split("_");
            //        var channelScheduleId = idArr[1];
            //        Publish(channelScheduleId);
            //    }
            //    else {
            //        isSchedulePublished = false;
            //    }
            //});
        }
    }
};

//---------------------------------------------------------------------------------------------------------
// CROP
//---------------------------------------------------------------------------------------------------------
var webMethodUpdateChannelForModal = "/WebServices/ChannelWebService.asmx/UpdateChannelForModal";
var avatarCropitInitialized = false;
function ShowCropModalChannelAvatar() {
    var imageSrc = $("#imgChannelAvatar").attr("src");

    $('#divChannelCropContainer').lightbox_me({
        centered: true,
        onLoad: function () {
            if (!avatarCropitInitialized) {
                avatarCropitInitialized = true;
                InitCropChannelAvatar(imageSrc);
            }
        },
        onClose: function () {
            $imageCropper = $('#divChannelCropContainer .image-editor');
            $imageCropper.cropit('reenable');
            //$imageCropper.cropit('imageSrc', "");
            RemoveOverlay();
        }
    });
};

function InitCropChannelAvatar(imgSrc) {

    var fileNameIndex = imgSrc.lastIndexOf("/") + 1;
    var filename = imgSrc.substr(fileNameIndex);

    void 0

    $imageCropper = $('#divChannelCropContainer .image-editor');

    $imageCropper.cropit({ imageBackground: false })
    $imageCropper.cropit({ allowCrossOrigin: true });
    $imageCropper.cropit({ allowDragNDrop: true });
    //$imageCropper.cropit('imageSrc', imgSrc);

    $imageCropper.find('.select-image-btn').click(function () {
        $imageCropper.find('.cropit-image-input').click();
    });

    $imageCropper.find('.export').click(function () {
        var imageData = $imageCropper.cropit('export', {
            type: 'image/jpeg',
            quality: .9,
            originalSize: true
        });

        if (imageData != null) {
            var b64 = imageData.split("base64,")[1];//string fileName, string imageData, int userId
            if (b64 === undefined || b64 == "undefined") {
                b64 = "";
            }

            //int channelId, string fileName, string imageData, int categoryId, int userId
            var params = '{"channelTubeId":' + channelTubeId + ',"fileName":' + "'" + filename + "'" + ',"imageData":' + "'" + b64 + "'" + ',"channelImgData":' + "'" + b64 + "'" + ',"categoryId":' + channelCategoryValue + ',"userId":' + userId + '}';
            $.ajax({
                type: "POST",
                url: webMethodUpdateChannelForModal,
                data: params,
                cashe: false,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d) {
                        $("#imgChannelAvatar").removeAttr("src").attr("src", imageData);
                        alertify.success("Channel avatar has been updated.");
                    }
                    else {
                        alertify.error("Channel avatar was not updated. Please try again.");
                    }

                    $('#divChannelCropContainer').trigger('close');
                },
                complete: function () {
                    $('#divChannelCropContainer').trigger('close');
                },
                error: function (request, status, error) {
                    void 0;
                }
            });
        }
    });
};

//---------------------------------------------------------------------------------------------------------
// END CROP
//---------------------------------------------------------------------------------------------------------

//---------------------------------------------------------------------------------------------------------
// Toogle Remove all menu
//---------------------------------------------------------------------------------------------------------
function ToggleRemoveMenu() {
    var $menu = $(".removeOptions");
    if ($menu.is(":visible")) {
        $menu.hide();
    }
    else {
        $menu.show();
    }
};

//---------------------------------------------------------------------------------------------------------
// Check for minium video count on channel
//---------------------------------------------------------------------------------------------------------
function IsMinimumVideosInChannels() {
    var allVideos = $(".videoBoxHolder").find("div.videoBoxNew");
    var minimumReached = false;

    if (allVideos && allVideos.length >= 3) {
        var allRestrictedVideos = $(".videoBoxHolder").find("div.videoRemoveMessage");
        if (allRestrictedVideos && allRestrictedVideos.length > 0) {
            var totalUnrestrictedOrRemoved = allVideos.length - allRestrictedVideos.length;
            if (totalUnrestrictedOrRemoved > 3) {
                minimumReached = true;
            }
        }
        else {
            minimumReached = true;
        }
    }

    return minimumReached;
};

//---------------------------------------------------------------------------------------------------------
// Search videos by keywords on schedule page and get videos popup
//---------------------------------------------------------------------------------------------------------
function SearchVideoByKeywordForChannel() {
    $("#ancLoadMore").hide();
    var arrKeywords = new Array();
    arrKeywords = $("#txtSearchVideoByKeywordForChannel").val().split(" ");
    if ($("#txtSearchVideoByKeywordForChannel").val() == "") {
        $("#loadingDiv").hide();
        alertify.error("please enter the keyword");
        return;
    }
    else {
        var params = '{"keywords":' + JSON.stringify(arrKeywords) + ',"channelTubeId":' + channelId + '}';

        $.ajax({
            type: "POST",
            url: webMethodGetVideoTubeByKeywordAndChannelId,
            cashe: false,
            data: params,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loadingDiv").show();
            },
            success: function (response) {


                if (response.d != null) {

                    $("#txtSearchVideoByKeywordForChannel").val("");
                    var data = response.d;
                    void 0;
                    if (data) {
                        var searchedVideoControls = Controls.BuildVideoBoxControlForSchedulePage(data, false);
                        if (searchedVideoControls.length != 0) {
                            $(".videoBoxHolder").html("").html(searchedVideoControls);
                            $("#loadingDiv").hide();

                        }
                        else {
                            alertify.error("no search results");
                            $("#loadingDiv").hide();
                        }

                    }

                }

            }
        });
    }
};

function SearchVideoByKeywordForPublicLib() {
    $("#loadMoreVideos").hide();
    var arrKeywords = new Array();
    arrKeywords = $("#txtSearchVideoByKeywordForPublicLib").val().split(" ");
    if ($("#txtSearchVideoByKeywordForPublicLib").val() == "") {
        $("#loadingDiv").hide();
        alertify.error("please enter the keyword");
        return;
    }
    else {
        var params = '{"keywords":' + JSON.stringify(arrKeywords) + ',"channelTubeId":' + channelId + '}';

        $.ajax({
            type: "POST",
            url: webMethodGetVideoTubeByKeywordForPublicLib,
            cashe: false,
            data: params,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loadingDiv").show();
            },
            success: function (response) {


                if (response.d != null) {

                    $("#txtSearchVideoByKeywordForPublicLib").val("");
                    var data = response.d;
                    void 0;
                    if (data) {
                        var pageType = "add-videos";
                        var videoControls = Controls.BuildViodeBoxControlForAddVideosPage("public-library", data, false);
                        if (videoControls.length != 0) {
                            $(".loadedContent").html("").html(videoControls);
                            $("#loadingDiv").hide();

                        }
                        else {
                            alertify.error("no search results");
                            $("#loadingDiv").hide();
                        }

                    }

                }

            }
        });
    }
};

function ClearSearchedVideosForChannel(isInSearchMode) {
    $("#txtSearchVideoByKeywordForChannel").val('');
    $("#btnClearSerachedVideosForChannel").hide();
    if (isInSearchMode) {
        isInSearchMode = false;
        LoadMoreVideos(true);
    }
};

function ClearSearchedVideosForPublicLib() {
    $("#txtSearchVideoByKeywordForPublicLib").val('');
    $("#btnClearSerachedVideosForPublicLib").hide();
    if (isInPubLibSearchMode) {
        isInPubLibSearchMode = false;
        GetPublicLibrary(true);
    }
};

function ClearSearchedVideosForVideoRoom() {
    $('#txtSearchVideoByKeywordForVideoRoom').val('');
    $("#btnClearSerachedVideosForVideoRoom").hide();
    if (isInVideoRoomSearchMode) {
        isInVideoRoomSearchMode = false;
        LoadVideosFromVideoRoom(true);
    }
};

//---------------------------------------------------------------------------------------------------------
// End Search videos by keywords for schedule page
//---------------------------------------------------------------------------------------------------------
function displayCustomSchedulePopup() {
    if (isMobileDevice()) {
        alertify.alert("This feature is only available on desktop.");
        return;
    }
    $('#customScheduleCreatePopup').show();
    $('#newScheduleHeader').show();
    $('#newScheduleTimePicker').show();
    $('#customScheduleCreatePopup').lightbox_me({
        centered: true,
        onLoad: function () {
        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {
            $('#customScheduleCreatePopup').hide();
            RemoveOverlay();
        }
    });
};

function displayHLSLinkPopup() {
    $('#getHLSLinkPopup').show();
    $('#getHLSLinkPopup').lightbox_me({
        centered: true,
        onLoad: function () {
            CheckUserSubscribtions(function () {
                if (hasProfPlan || hasProfPlusPlan) {
                    $("#linksHolder").show();
                    $("#planMissingHolder").empty();
                    $("#planMissingHolder").hide();
                    $("#hlsLinkText").val(window.location.protocol + "//" + window.location.hostname + "/hls/" + channelId + ".m3u8");
                    $("#jsonLinkText").val(window.location.protocol + "//" + window.location.hostname + "/hls/" + channelId + ".json");
                }
                else {
                    $("#planMissingHolder").show();
                    $("#linksHolder").hide();
                    $("#planMissingHolder").empty();
                    $("#planMissingHolder").append("<div class=\"subscribe_error\">Please subscribe to Professional plan to use this feature.</div>");
                }
            });
        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {
            $('#getHLSLinkPopup').hide();
            RemoveOverlay();
        }
    });
}

function CheckUserSubscribtions(callBack) {
    $.ajax({
        type: "POST",
        url: webMethodGetUserProductSubscriptionsByUserId,
        cashe: false,
        async: false,
        data: '{"userId":' + userId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var subscribtions = response.d.Response;
            for (var i = 0; i < subscribtions.length; i++) {
                if (subscribtions[i].ProductName == "Professional" && subscribtions[i].ProductSubscriptionStatus == "Active") {
                    hasProfPlan = true;
                }
                if (subscribtions[i].ProductName == "Professional Plus" && subscribtions[i].ProductSubscriptionStatus == "Active") {
                    hasProfPlusPlan = true;
                }
            }
            callBack();
        },
        error: function (request, status, error) {

        }
    });
}

function CopyHlsLink() {
    var copyText = document.getElementById("hlsLinkText");
    copyText.select();
    copyText.setSelectionRange(0, 99999); /* For mobile devices */
    document.execCommand("copy");
}

function ImportByUrl() {

    $("#importUserYoutubeUploadsHolder").hide();
    $("#btnImportYoutubeUserUploads").removeClass("active");

    $("#btnImportByUrl").addClass('active');
    $(".inputHolderUrl").show();

    $('.styled-selectSortSearch').show();
    $('#btnImportUserUploads').hide();
    //$('#userYoutubeUploadsContent').html('');
    $('.loadedContent').html('');
    $('#divUrlCategory').hide();
    $('#addVideoInstructions').hide();
    $('#txtPopoupBlockigComment').hide();
    $("#btnImportPrivate").removeAttr("disabled");
};

function ImportYoutubUserUploads() {
    $("#btnImportByUrl").removeClass('active');
    $(".inputHolderUrl").hide();
    $("#btnImportYoutubeUserUploads").addClass("active");
    $('#btnImportUserUploads').show();
    //$('#userYoutubeUploadsContent').html('');
    $('.loadedContent').html('');
    $('#addVideoInstructions').hide();
    $('#txtPopoupBlockigComment').show();
    $("#btnImportPrivate").attr("disabled", true)
}

function channelSearchInputKeyUp(e) {
    var key = event.keyCode || event.charCode;

    var shouldSearch = false;
    var keywordsSpecified = $("#txtSearchVideoByKeywordForChannel").val().length != 0;

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

    if (shouldSearch) {
        isInSearchMode = true;
        LoadMoreVideos(true);
    }
}

var isInPubLibSearchMode;

function publicLibrarySearchInputKeyUp() {
    var key = event.keyCode || event.charCode;

    var shouldSearch = false;
    var keywordsSpecified = $("#txtSearchVideoByKeywordForPublicLib").val().length != 0;

    if (keywordsSpecified) {
        $('#btnClearSerachedVideosForPublicLib').show();
    }
    else {
        $('#btnClearSerachedVideosForPublicLib').hide();
    }

    switch (key) {
        case 13:
            if (keywordsSpecified) {
                shouldSearch = true;
            }
            break;
        case 46:
            if (isInPubLibSearchMode) {
                shouldSearch = true;
            }
            break;
        default:
            break;
    }

    if (shouldSearch) {
        isInPubLibSearchMode = true;
        GetPublicLibrary(true);
    }
}

var isInVideoRoomSearchMode;

function videoRoomSearchInputKeyUp() {
    var key = event.keyCode || event.charCode;

    var shouldSearch = false;
    var keywordsSpecified = $("#txtSearchVideoByKeywordForVideoRoom").val().length != 0;

    if (keywordsSpecified) {
        $('#btnClearSerachedVideosForVideoRoom').show();
    }
    else {
        $('#btnClearSerachedVideosForVideoRoom').hide();
    }

    switch (key) {
        case 13:
            if (keywordsSpecified) {
                shouldSearch = true;
            }
            break;
        case 46:
            if (isInVideoRoomSearchMode) {
                shouldSearch = true;
            }
            break;
        default:
            break;
    }

    if (shouldSearch) {
        isInVideoRoomSearchMode = true;
        LoadVideosFromVideoRoom(true);
    }
}

/////////////////////////////////////////////////////////////////YOUTUBE USER VIDEOS BULK UPLOAD/////////////////////////////////////////////////////////

var playlistId;
var nextPageToken;
var prevPageToken;
var webMethodGetVideoById = "/WebServices/YouTubeWebService.asmx/GetVideosById";
var VideoTubeProviderIds = [];
var guserid = 0;

var OAUTH2_CLIENT_ID = ydevClientId; //'666520747239-qbp19slc5q976naob54lnt3oh513lr93.apps.googleusercontent.com';
var API_KEY = ydevApiKey; //'AIzaSyBVrO3E0WKHaO4NnXM-qlWR6tPk5CUs8zU';

function ImportUserUploads() {
    void 0
    googleApiClientReady();

    $.ajax({
        type: "POST",
        url: webMethodGetEmptyChannelCategoriesJsonForVideos,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: '{"videoTypeId":' + 2 + '}',
        success: function (response) {
            var cdata = response.d;
            document.getElementById('ddlYoutubeUserUploadsCategory').options.length = 0;
            if (cdata) {
                $("#ddlYoutubeUserUploadsCategory").append($('<option></option)').attr("value", "0").text("choose category"));
                $.each(cdata, function (i, c) {
                    $("#ddlYoutubeUserUploadsCategory").append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
                });
            }
        }
    });
}

// Upon loading, the Google APIs JS client automatically invokes this callback.
googleApiClientReady = function () {
    gapi.auth.init(function () {
        window.setTimeout(login, 1);
    });
}

/**
 * This is a login method that application will call for ever import my videos request
 * from the user
 */
function login() {
    var myParams = {
        'clientid': OAUTH2_CLIENT_ID,
        'cookiepolicy': 'single_host_origin',
        'callback': 'loginCallback',
        'approvalprompt': 'force',
        'scope': 'https://www.googleapis.com/auth/plus.login https://www.googleapis.com/auth/youtube.readonly'
    };
    gapi.auth.signIn(myParams);

    return false;
};

/**
 * This callback method will be called per successful user login into his google
 * account. It will ensure that necessary authorizations were obtained for user account, 
 * user successfully logged in and will start the process of retrieving videos
 * from his uploads' playlist.
 */
function loginCallback(result) {
    if (result['status']['signed_in']) {
        var request = gapi.client.plus.people.get(
        {
            'userId': 'me'
        });

        request.execute(function (resp) {
            guserid = resp['id'];

            retrieveUserUploadsPlaylistId(guserid);
        });
    }
};

/**
 * This method will load all neceassary google apis for retrieving user and youtube data
 */
function onLoadCallback() {
    gapi.client.setApiKey(API_KEY);
    gapi.client.load('plus', 'v1', function () {
        gapi.client.load('youtube', 'v3', function () { });
    });
}

/**
 * This method will retrieve playlist from user's channel
 * that is associated with user's uploads
 * See https://developers.google.com/youtube/v3/docs/channels/list
 */
function retrieveUserUploadsPlaylistId(guserid) {
    var request = gapi.client.youtube.channels.list({
        mine: true,
        userId: guserid,
        part: 'contentDetails'
    });

    request.execute(function (response) {
        if (response.result && response.result.items[0].contentDetails && response.result.items[0].contentDetails.relatedPlaylists) {
            playlistId = response.result.items[0].contentDetails.relatedPlaylists.uploads;
            retrieveVideosInPlaylist(playlistId, guserid);
            $('.loadedContent').html("");
        }
    });
}

/**
 * This method will retrieve all videos that are part 
 * of a specific playlist and associated with a specific user's account
 */
function retrieveVideosInPlaylist(playlistId, guserid) {
    var requestOptions = {
        playlistId: playlistId,
        part: 'snippet,status',
        maxResults: 50,
        userId: guserid,
        key: API_KEY
    };

    if (pageToken) {
        requestOptions.pageToken = pageToken;
    }

    //console.log(pageToken)
    var request = gapi.client.youtube.playlistItems.list(requestOptions);

    var videoids = [];

    request.execute(function (response) {

        $('#txtPopoupBlockigComment').hide();

        if (response && response.result) {
            nextPageToken = response.result.nextPageToken;

            var playlistItems = response.result.items;

            if (playlistItems && playlistItems.length > 0) {
                //console.log(playlistItems);

                buildVideoBoxControlsForUserVideos(playlistItems);

                if (nextPageToken) {
                    requestVideoPlaylist(playlistId, nextPageToken);
                }
                $('#btnAddAllToChannel').show();
                $('.styled-selectSortSearch').show();
                $('#importUserYoutubeUploadsHolder').show();
            }
            else {
                $('.loadedContent').html('Sorry, you have no public videos in the selected account.');
                $('#btnAddAllToChannel').hide();
                $('.styled-selectSortSearch').hide();
                $('#importUserYoutubeUploadsHolder').hide();
            }
        }

        logout();
    });
};

/**
 * This method will build a UI videobox control for every video id
 * retrieved from the user's playlist and display all of the on the screen
 */
function buildVideoBoxControlsForUserVideos(playlistItems) {
    var videoIds = '';
    
    $.each(playlistItems, function (index, item) {
        var videoId = item.snippet.resourceId.videoId;

        if (videoIds == '') {
            videoIds = item.snippet.resourceId.videoId + ',';
        }
        else {
            videoIds += item.snippet.resourceId.videoId + ',';
        }

        VideoTubeProviderIds.push(videoId);
    });


    countResultIndex = 0;
    countResultIndex = 1;

    var pageToken = null;

    $.ajax({
        type: "POST",
        url: webMethodGetVideoById,
        cashe: false,
        data: '{"videoIds":' + "'" + videoIds + "'" + ',"startIndex":' + 1 + ',"pageToken":' + "'" + pageToken + "'" + ',"channelTubeId":' + channelId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var data = response.d;

            pageIndex = data.NextPageIndex;
            prevPageIndex = data.PrevPageIndex;

            var pageSize = data.PageSize;
            var pageCount = data.PageCount;

            pageToken = data.PageToken;

            if (data.VideoTubeModels.length != 0) {
                var videos = data.VideoTubeModels;

                var pageType = "add-videos";
                var videoControls = Controls.BuildViodeBoxControlForAddVideosPage("by-keyword", videos, false);

                $("#importUserYoutubeUploadsHolder").show();
                $('.loadedContent').append(videoControls);
                $('#addVideoInstructions').show();
            }
            else {
                $(".loadedContent").html("").html("<span class='spnMsg'>Sorry, you have no public videos in the selected account</span>");
                $('#addVideoInstructions').hide();
                $("#loadMoreVideos").hide();
            }

            clearScreen = false;
        },
        complete: function (response) {
            $("#loadingDiv").hide();
        }
    });

};

/**
 * This method will add all of the videos retrieved from user's playlist
 * to user's channel on strimm
 */
function addAllImportedYoutubeVideosToChannel() {
    var categoryId = 0;
    categoryId = $("#ddlYoutubeUserUploadsCategory option:selected").val();

    if (categoryId == 0) {
        alertify.alert("Please select a category.");
    }
    else {
        $.ajax({
            type: "POST",
            url: webMethodAddAllImportedVideosToChannel,
            cashe: false,
            data: '{"videoTubeModel":' + JSON.stringify(VideoTubeProviderIds) + ',"channelId":' + channelId + ',"categoryId":' + categoryId + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var data = response.d;
                // console.log(VideoTubeProviderIds);
                if (data != null && data.Message == null && data.length != 0) {
                    var videos = data.VideoTubeModels;
                    //console.log(data);
                    var pageType = "schedule";
                    var videoControls = Controls.BuildVideoBoxControlForSchedulePage(data, true);
                    var existingHtml = $(".videoBoxHolder").html();

                    $.each(VideoTubeProviderIds, function (i, d) {
                        $("#recentlyadded_" + d).text("in channel");
                        $("#addvideosId_" + d).removeAttr("onclick").hide();
                    });

                    $(".videoBoxHolder").empty().append(videoControls).append(existingHtml);

                    RemoveNoVideosMessage();
                    VideoTubeProviderIds = [];
                }
                else {
                    if (data) {
                        if (data.Message == undefined) {
                            data.Message = "Restricted or removed videos cannot be added.";
                        }
                        alertify.error(data.Message);
                    }
                }

                LoadChannelCategories(categoryId);
            }
        });
    }
};

/**
 * This method will log current google user out and remove strimm's authorizations 
 * for his account
 */
function logout() {
    var token = gapi.auth.getToken();

    if (token) {
        var accessToken = token.access_token;

        if (accessToken) {
            var revokeUrl = 'https://accounts.google.com/o/oauth2/revoke?token=' + accessToken;

            $.ajax({
                type: 'GET',
                url: revokeUrl,
                async: false,
                contentType: "application/json",
                dataType: 'jsonp',
                success: function (nullResponse) {
                    // Do something now that user is disconnected
                    // The response is always undefined.
                },
                error: function (e) {
                    // Handle the error
                    // console.log(e);
                    // You could point users to manually disconnect if unsuccessful
                    // https://plus.google.com/apps
                }
            });
        }
    }

    gapi.auth.setToken(null);
    gapi.auth.signOut();


};

function ToggleVideosMobile() {
    scrollToElement("#newScheduleActionsM", 30);
    $('.videoBoxHolder:visible').hide();
    $('.videoBoxHolder:hidden').show();
    $(".rightnewSchedulePopup").hide();
    $(".ancLoadMoreHolder").show();
    $(".leftSortActionHolder").show();
};

function ToggleScheduleMobile() {
    scrollToElement("#newScheduleActionsM", 30);

    $('.rightnewSchedulePopup:visible').hide();
    $('.rightnewSchedulePopup:hidden').show();
    $(".videoBoxHolder").hide();
    $(".ancLoadMoreHolder").hide();
    $(".leftSortActionHolder").hide();
};
function scrollToElement(selector, topRange) {
    // event.preventDefault();

    $('html,body').animate({ scrollTop: $($(selector)).offset().top - topRange }, 900);
}



