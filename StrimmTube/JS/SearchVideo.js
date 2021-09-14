var webMethodGetVideoByUrl = "/WebServices/YouTubeWebService.asmx/FindVideoByUrl";

var webMethodRemoveVideo = "/WebServices/YouTubeWebService.asmx/RemoveVideo";
var webMethodGetVideoByKeyWord = "/WebServices/YouTubeWebService.asmx/FindVideosByKeywords";
var webMethodSortVideos = "/WebServices/YouTubeWebService.asmx/SortVideos";
var webMethodGetMoreResults = "/WebServices/YouTubeWebService.asmx/GetMoreResults";
var webMethodGetChannelCategoriesJson = "/WebServices/ChannelWebService.asmx/GetChannelCategories";
var webMethodPostScheduleList = "/WebServices/YouTubeWebService.asmx/PostScheduleList";
var webMethodAddToVideoRoom = "/WebServices/YouTubeWebService.asmx/AddVideoTube";
var webMethodGetPublicLibResult = "/WebServices/PublicLibraryService.asmx/GetPublicLib";
var webApiMethodGetPublicLibResults = "http://localhost:4002/api/public/videos";
var webMethodAddToVrFromPubliLib = "/WebServices/YouTubeWebService.asmx/AddToVrFromPubliLib";
var webMethodAddListToVideoRoom = "/WebServices/VideoRoomService.asmx/AddListToVideoRoom";
var webMethodIsInVideoRoom = "/WebServices/VideoRoomService.asmx/IsInVideoRoom";
var webMethodAddVideoToChannel = "/WebServices/YouTubeWebService.asmx/AddVideoToChannel";

//show description on mouseHover in videoBox
//function ShowDescription(span) {
//    var stringId = span.id;
//    var idArr = stringId.split("_");
//    var id = idArr[1];
//    var spnText = $("#" + stringId).text();
//    $("#description_" + id).show().text(spnText);
//    $("#" + stringId).mouseleave(function () {
//        $("#description_" + id).hide();
//    });
//};
////hide description
//function HideDescription(span) {
//    var stringId = span.id;
//    var idArr = stringId.split("_");
//    var id = idArr[1];
//    $("#description_" + id).hide();
//};
////reset result
var categoryData;
var publicLibStartIndex;
var videoTubeObjects = new Array();
var yt_page_startindex = 1;
var clearScreen = false;

var AddVideoCount = 0;
$(document).ready(function () {

    $(document).keyup(function (event) {
        $('select option[value=0]').attr('selected', 'selected');

        if ($("#btnSearchByKeyword").is(":visible")) {
            if (event.keyCode == 13) {
                $("#btnSearchByKeyword").trigger('click');
            }
        }
        if ($("#btnSearchByURL").is(":visible")) {
            if (event.keyCode == 13) {
                $("#btnSearchByURL").trigger('click');
            }
        }

    });
    //$("#divPrevResults").hide();
    $("#divMenuHolder a.search").addClass("active");

    //$("#inputHolder").onkeypress(function () {
    //    yt_page_startindex = 1;
    //    clearScreen = true;
    //});

    //$('#tab-container').easytabs({
    //    animate: false
    //});
    //$(".spanLink").mouseover(function () {
    //    $("#divShowImage").show();
    //});
    //$(".spanLink").mouseout(function () {
    //    $("#divShowImage").hide();
    //});
    $(window).unload(function () {
        // //console.log("onbeforeload");
        $("select").value = 0;
        $.ajax({
            type: "POST",
            url: webMethodClearSession,
            cashe: false,
            dataType: "json",
            contentType: "application/json",
            success: function (response) {
            }

        });

    });

    window.onbeforeunload = function () {
        $('select option[value="0"]').attr('selected', 'selected');

    };
    //  $(".nano").nanoScroller({ alwaysVisible: true });
    $.ajax({
        type: "POST",
        url: webMethodGetChannelCategoriesJson,
        cashe: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var json = response.d;
            categoryData = JSON.parse(json);
            // //console.log(categoryData);
        }

    });

    // publicLibStartIndex = 1;
  //  window.location.hash = "PublicLib";




});

function AddRemoveSelectedVideoToArray(btn) {
    void 0
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var id = idArr[1];
    var videoTube = new Object();
    //console.log(videoTube);
    videoTube.categoryId = SetCategory();
    //if (videoTube.categoryId == 0) {
    //    alert("Please select category of where videos shall be added to");
    //    return;
    //}
    videoTube.title = $("#boxContent_" + id + " .title").text();
    videoTube.description = $("#txtCustomizeDescription_" + id).text();
    videoTube.videoPath = $("#boxContent_" + id + " .btnPlay").attr("id");
    //var durString = $("#boxContent_" + id + " .duration").text().split(",");
    videoTube.duration = $("#boxContent_" + id + " .durationHidden").val();
    videoTube.videoThumbnail = $("#boxContent_" + id + " img").attr("src");
    videoTube.videoCount = 0;
    videoTube.isScheduled = false;
    videoTube.useCount = 0;
    videoTube.provider = "youtube";
    videoTube.r_rated = isCheckedById(id);
    var isChecked = $('#' + btnId).is(":checked")
    void 0;
    if (isChecked == true) {
        videoTubeObjects.push(videoTube);
        AddVideoCount++;
    }
    else {
        videoTubeObjects.splice($.inArray(videoTube, videoTubeObjects), 1);
        if (AddVideoCount != 0) {
            AddVideoCount--;
        }
        // videoTubeObjects.remove(videoTube);


    }
    void 0;
    // videoTubeObjects.push(videoTube);
};

function addVideoToChannel(btn) {
    var strId = btn.id;
    var idArr = strId.split("_");
    var id = idArr[1];
    var boxContent = "#boxContent_" + id;
    var VideoTubeModel = new Object();
    VideoTubeModel.title = $(boxContent + " .divVideoInformation .title").text();
    VideoTubeModel.description = $(boxContent + " .descriptionHidden").val();
    VideoTubeModel.duration = $(boxContent + " .durationHidden").val();
    VideoTubeModel.categoryId = categoryId;
    VideoTubeModel.videoProviderId=1;  
    VideoTubeModel.providerVideoId = $(boxContent + " .btnPlay").prop("id");
    VideoTubeModel.thumbnail = $(boxContent + " .IM0G").prop("src"); 
    $.ajax({
        type: "POST",
        url: webMethodAddVideoToChannel,
        cashe: false,
        data: '{"videoModel":' + JSON.stringify(VideoTubeModel) + ',"channelTubeId":' + channelTubeId + ',"categoryId":' + categoryId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $(boxContent + " .actions a").text("").text("added to channel").removeAttr("onclick");
            $(boxContent).attr("data-videoadd", "1");
        }
    });
};
//$(function () {
//    $("li.tab.searchKey a, li.tab.searchUrl a").click(function () {
//        $("#divResulSearchURL").html("");
//        $("#divPageResults").hide();
//        $(".nano").nanoScroller({ alwaysVisible: true });
//        $("#ddlCategory").hide();
//        $("#btnResetResultURL").show();
//        $(".newresult").show();
//        var publicLibStartIndex = 1;
//         videoTubeObjects = new Array;
//        AddVideoCount = 0;
//        removePubliLibControl();
//        $('select option[value=0]').attr('selected', 'selected');


//    });

//});
//$(function () {
//    $("li.tab.publicLib a").click(function () {
//        $("#ddlCategory").show();
//        ResetResult();
//        var publicLibStartIndex = 1;
       
//        $(".nano").nanoScroller({ alwaysVisible: true });
//        $("#btnResetResultURL").hide();
//        $(".newresult").hide();
       
//        $('select option[value=0]').attr('selected', 'selected');
//        videoTubeObjects = new Array;
//        AddVideoCount = 0;
//        GetPublicLibrary();
      
       

//    })
//});
function addPublicLibMoreResultControl() {
    $("#divPageResults").append("<a class='ancLoadMorePubLib' onclick='GetMorePublicLib()'>load more</a>");
};
function removePubliLibControl() {
    $("#divPageResults a.ancLoadMorePubLib").remove();
};
function addCategoriesToSelect() {
    var select = $(".spnCat");
    select.empty();
    select.append($('<option></option)').attr("value", "0").text("choose category"));
    $.each(categoryData, function () {
        select.append($('<option></option)').attr("value", this.value).text(this.text));
    });
};
//show video info
function ToggleVideoInfo(element) {
    var stringId = element.id;
   // var matches = stringId.match(/(.*?_.*?)(_.*)/);
   // var firstPart = matches[1];
   // var secondPart = matches[2];
     var idArr = stringId.split("_");
     var id = idArr[1];
    // $("#txtCustomTitle_" + id).text($("#boxContent_" + id + " .divVideoInformation span.title").text());
     //console.log(stringId);
    // //console.log(secondPart);
         $("#boxContent_" + id + " .divCustomize").show();        
};
function HideCustomize(btn) {
    var stringId = btn.id;
    var idArr = stringId.split("_");
    var id = idArr[1];
    $("#boxContent_" + id + " .divCustomize").hide();
};
function ResetResult() {
    $(".newresult").text(" ").text("get more results").attr("onclick", "GetMoreResults()");
    //$(".newresult").hide();
    $.ajax({
        type: "POST",
        url: webMethodClearSession,
        cashe: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#divResulSearchURL").html("");
            $(".nano").nanoScroller({ alwaysVisible: true });
            $("#txtKeyword").val("");
            $('select option[value=0]').attr('selected', 'selected');
           // $("#divPageResults").hide();
        }
    });
};
function RemoveVideoBox(anchor) {
    var stringId = anchor.id;
    var idArr = stringId.split("_");
    var id = idArr[1];
    //console.log(id);
    $("#boxContent_" + id).hide();
    $.ajax({
        type: "POST",
        url: webMethodRemoveVideo,
        data: '{"videoIndex":' + "'" + id + "'" + '}',
        cashe: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $(".nano").nanoScroller({ alwaysVisible: true });
        }
    });
};
//play video
function addPlayer(videoPath) {
    var stringId = videoPath.id;
    var idArr = stringId.split("_");
    var id = idArr[1];
    //flowplayer("related", "flowplayer.youtube/flowplayer.swf", {
    //    plugins: {
    //        youtube: {
    //            url: "flowplayer.youtube/flowplayer.youtube-3.2.11.swf",
    //            enableGdata: true,
    //            loadOnStart: true,
    //            onVideoRemoved: function () {
    //                //console.log("Video Removed");
    //            },
    //            onVideoError: function () {
    //                //console.log("Incorrect Video ID");
    //            },
    //            onEmbedError: function () {
    //                //console.log("Embed Not Allowed");
    //            },
    //            onApiData: function (data) {
    //                // use the function defined above to show the related clips
    //                // //console.log(data);
    //                // use the function defined above to show the related clips
    //                // showInfo(data, "#playlist1");
    //                //load the related list for the first clip only
    //                // if (shownRelatedList) return;
    //                // showRelatedList(data, "#playlist1");
    //                // shownRelatedList = true;
    //            },
    //            displayErrors: false
    //        }
    //    },
    //    clip: {
    //        provider: "youtube",
    //        url: "api:" + videoPath.id,
    //        autoPlay: true,
    //        // start: 50
    //        onLastSecond: function (clip) {
    //            // //console.log(data[clip.index].title);
    //        }
    //    },
    //    //log: {
    //    //    level: 'debug',
    //    //    filter: 'org.electroteque.youtube.*, org.flowplayer.controller.*'
    //    //}
    //});
    function onYouTubeIframeAPIReady() {
        player = new YT.Player('related', {
            height: '546',
            width: '728',

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
//generate unique id 
function GetUniqueId() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
};
function getYouTubeVideoByURL() {
    var uniqueId = GetUniqueId();
    $("#divUrl").show();
    var url = $("#txtURL").val();
    if (url.length<=0)
    {
        $("#divResulSearchURL").text("Please enter a valid URL");
    }
    else
    {
        $.ajax({
            type: "POST",
            url: webMethodGetVideoByUrl,
            cashe: false,
            data: '{"url":' + "'" + url + "'" + ',"channelTubeId":' + channelTubeId + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var video = response.d;
                var videos = {};
                videos[0] = video;
                var videoControls = Controls.BuildVideoRoomControl("public-library", videos, false);

                $("#divResulSearchURL").append(videoControls);
                addCategoriesToSelect();
                $(".nano").nanoScroller({ alwaysVisible: true });
            }
        });
    }
  
};
var durationVal;
function getValue(select) {
    var selectedValue = select.value;
    if (selectedValue !== null && selectedValue !== undefined) {
        durationVal = selectedValue;
    }
    else {
        durationVal = 0;
    }
};
var countResultIndex = 0;
//get public lib

//                    var final = "<div class='channelBox' id='boxContent_" + videoId + "'>" +
//                                    "<div class='actions' id='action_{videoId}'>" +
//                                        "<input id='addId_" + videoId + "' type='checkbox' onchange='AddRemoveSelectedVideoToArrayPL(this)'/>" +
//                                        "<input id='addId_" + videoId + "' type='checkbox' onchange='AddRemoveSelectedVideoToArrayPL(this)'/>" +
//                                         "<span id='spnSelectaddId_" + videoId + "'>select</span>";
//                    if (isRestricted) {
//                        final += "<span class='spnError'>This video is restricted by YouTube</span>";
//                    }

//                    final += "</div>" +
//                             "<a class='btnPlay' id='" + providerVideoId + "' onclick='showPlayer(this)'>" +
//                             "<img class='IMG' src='" + thumbnail + "' />" +
//                           "</a>" +
//                            "<div class='divVideoInformation'>" +
//                                "<span class='title' title='" + title + "'>" + title + "</span>" +
//                                "<span class='views'>views:" + viewCounter + "</span>" +
//                                "<span class='duration'>duration:" + durationInSec + " seconds</span>" +
//                                "<input type='hidden' value='" + durationInSec + "' class='durationHidden'/>" +
//                                "<span class='category'>category&nbsp" + categoryName + "</span>" +
//                                "<input type='hidden' value='" + videoId + "' class ='hidden'/>" +
//                            "</div>";

//                    if (isScheduled) {
//                        final += "<span class='spnInScedule' id='spnInScedule_addId_" + videoId + "' style='display: none;'>in video room already</span>";
//                    }

//                    final += "</div>";

//                    $("#divResulSearchURL").append(final);
//                });
//            }

//            else {
//                $("#divResulSearchURL").html("<div id='no'>No Video</div>");
//            }
//        }
//    });
//};
function myTimer() {
    var date = new Date();
    void 0;
}
function GetYouTubeVideosByKeyword() {
    var myVar = setInterval(function () { myTimer() }, 1000);


    $("#divKeyword, #divSortByURL").show();

    if (durationVal == null && durationVal == undefined) {
        durationVal = 0;
    }

    if ($("#txtKeyword").val().length > 0) {
        $('select option[value=0]').attr('selected', 'selected');
        var keyword = $("#txtKeyword").val();
        $("#divNext").show();
        $("#divPageResults").show();
        countResultIndex = 0;
        countResultIndex = 1;
        
        $.ajax({
            type: "POST",
            url: webMethodGetVideoByKeyWord,
            cashe: false,
            data: '{"searchString":' + "'" + keyword + "'" + ',"startIndex":' + yt_page_startindex + ',"channelTubeId":' + channelTubeId + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loadingDiv").show();
            },
            success: function (response) {
               
                $("#divSortByURL").show();

                var data = response.d;
                pageIndex = data.NextPageIndex;
                prevPageIndex = data.PrevPageIndex;
                yt_page_startindex = data.NextPageIndex;
                var pageSize = data.PageSize;
                var pageCount = data.PageCount;

                if (data.VideoTubeModels) {
                    var videos = data.VideoTubeModels;
                    var pageType = "public-library";
                    var videoControls = Controls.BuildVideoRoomControl(pageType, videos, false);
                    //$("#sortSelect").val('1');
                   
                    
                    if (clearScreen) {
                        $("#divResulSearchURL").html("");
                    }
                    $("#divResulSearchURL").append(videoControls);
                   
                }

                clearScreen = false;
                addCategoriesToSelect();
               
                $(".nano").nanoScroller({ alwaysVisible: true });
                $(".nano").nanoScroller({ scroll: 'top' });
                $("#loadingDiv").hide();
            },
            complete: function(response)
            {
                window.clearInterval(myVar);
            }

        });
    }
    else {
        $("#divResulSearchURL").text("");
        alertify.error("Please enter search keywords");
    }

};

//function GetYouTubeVideosByKeyword() {
//    $("#divKeyword, #divSortByURL").show();

//    if (durationVal == null && durationVal == undefined) {
//        durationVal = 0;
//    }

//    if ($("#txtKeyword").val().length > 0) {
//        $('select option[value=0]').attr('selected', 'selected');
//        var keyword = $("#txtKeyword").val();
//        $("#divNext").show();
//        $("#divPageResults").show();
//        countResultIndex = 0;
//        countResultIndex = 1;

//        var yt_url = "http://gdata.youtube.com/feeds/api/videos?q=" + yt_page_startindex + "&status=active&hd&duration=long&max-results=50&videoDuration=long&videoDefinition=high&safeSearch=moderate&v=2&alt=jsonc";
//        $.ajax({
//            type: "GET",
//            url: yt_url,
//            cashe: false,
//            data: '{}',
//            dataType: "json",
//            contentType: "application/json; charset=utf-8",
//            beforeSend: function () {
//                $("#loadingDiv").show();
//            },
//            success: function (response) {
//                $("#loadingDiv").hide();
//                $("#divSortByURL").show();

//                var videos = response.data.items;
//                var length = videoTubeObjects.length;
//                yt_page_startindex = response.data.startIndex + 1;
                
//                $.each(videos, function (i, d) {
//                    var hours = Math.floor(d.duration / 3600);
//                    var min = Math.floor((d.duration - (hours * 3600)) / 60);
//                    var sec = Math.floor(d.duration - (hours * 3600) - (min * 60));

//                    var video = {
//                        CategoryName: d.category,
//                        CategoryName: d.category,
//                        Description: d.description,
//                        ProviderVideoId: d.id,
//                        Rating: d.rating,
//                        Thumbnail: d.thumbnail.sqDefault,
//                        Title: d.title,
//                        Duration: d.duration / 60,
//                        DurationString: hours + "h:" + min + "min:" + sec + "sec",
//                        IsRestrictedByProvider: false,
//                        IsRemovedByProvider: false,
//                        IsPrivate: false,
//                        IsScheduled: false,
//                        IsRRated: false,
//                        VideoTubeId: 0,
//                        ViewCounter: d.viewCount
//                    };
//                    videoTubeObjects[length + i] = video;
//                });

//                var sortedVideos = videoTubeObjects.sort(function (a, b) {
//                    if (a.Duration > b.Duration) {
//                        return -1;
//                    }
//                    else if (a.Duration < b.Duration) {
//                        return 1;
//                    }
//                    return 0;
//                });

//                var final = Controls.BuildVideoRoomControl("public-library", sortedVideos);

//                if (clearScreen) {
//                    $("#divResulSearchURL").html("");
//                }
//                $("#divResulSearchURL").append(final);
//                clearScreen = false;
//                addCategoriesToSelect();
//                $(".nano").nanoScroller({ alwaysVisible: true });
//                $(".nano").nanoScroller({ scroll: 'top' });
//            }
//        });
//    }
//    else {
//        $("#divResulSearchURL").text("Please enter keyword");
//    }

//};

//get videoByKeyword
function GetYouTubeVideoByKeyword() {
    //var CatId = SetCategory();
    //if (CatId == "0")
    //{
    //    alert("Please select category of where videos shall be added to");
    //    return;
    //}
    $("#divKeyword, #divSortByURL").show();
    if (durationVal == null && durationVal == undefined) {
        durationVal = 0;
    }
    if ($("#txtKeyword").val().length > 0) {
        $('select option[value=0]').attr('selected', 'selected');
        var keyword = $("#txtKeyword").val();
        $("#divNext").show();
        $("#divPageResults").show();
       // $(".newresult").removeClass("hidden").attr("onclick", "GetMoreResults()");
        //$("#divPrevResults").html("").append("<a onclick='GetPrevResult(" + 1 + ",this)' class='prevResult active'>" + 1 + "</a>");
        countResultIndex = 0;
        countResultIndex = 1;
        $.ajax({
            type: "POST",
            url: webMethodGetVideoByKeyWord,
            cashe: false,
            data: '{"keyword":' + "'" + keyword + "'" + ',"durationVal":' + "'" + durationVal + "'" + ',"startIndex":' + "'" + countResultIndex + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend:function()
            {
                $("#loadingDiv").show();
            },
            success: function (response) {
                $("#loadingDiv").hide();
                $("#divSortByURL").show();
                $("#divResulSearchURL").html("").html(response.d);
                addCategoriesToSelect();
                $(".nano").nanoScroller({ alwaysVisible: true });
                $(".nano").nanoScroller({ scroll: 'top' });
            }
        });
    }
    else {
        $("#divResulSearchURL").text("");
        alertify.error("Please enter search keywords");
    }
};

function sortVideos(select) {
    var $wrapper = $('#divResulSearchURL');
    void 0;
  
    var value;
    var selectedValue = select.value;
    if (selectedValue !== null && selectedValue !== undefined) {
        value = selectedValue;
    }
    else
    {
        value="1";
    }
   
    switch(value)
    {
        case "1":
            $wrapper.find('.channelBox').sort(function (a, b) {
                return +b.getAttribute('data-duration') - +a.getAttribute('data-duration');
            })
  .appendTo($wrapper);
            break;
        case "2":
            $wrapper.find('.channelBox').sort(function (a, b) {
                return +a.getAttribute('data-duration') - +b.getAttribute('data-duration');
            })
 .appendTo($wrapper);
            break;
        case "3":
            $wrapper.find('.divBoxContent').sort(function (a, b) {
                return +b.getAttribute('data-views') - +a.getAttribute('data-views');
            })
 .appendTo($wrapper);
        case "4": 
            $wrapper.find('.channelBox').sort(function (a, b) {
                return +b.getAttribute('data-videoadd') - +a.getAttribute('data-videoadd');
            })
 .appendTo($wrapper);
            break;

    }
};
// get more results
function GetMoreResults() {
    countResultIndex++;

    if (durationVal == null && durationVal == undefined) {
        durationVal = 0;
    }
    if (countResultIndex == 18) {
       // $(".newresult").removeAttr("onclick").addClass("hidden");
    }
    // //console.log(durationVal);
    if ($("#txtKeyword").val().length > 0) {
        var keyword = $("#txtKeyword").val();
        //  //console.log(keyword);
        $.ajax({
            type: "POST",
            url: webMethodGetMoreResults,
            cashe: false,
            data: '{"keyword":' + "'" + keyword + "'" + ',"durationVal":' + "'" + durationVal + "'" + ',"countIndex":' + "'" + countResultIndex + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $("#loadingDiv").show();
            },
            success: function (response) {
                $("#loadingDiv").hide();
                if (response.d != 0) {
                    $("#divResulSearchURL").append(response.d);
                    addCategoriesToSelect();
                    // $(".prevResult").removeClass("active");
                    // $("#divPrevResults").append("<a onclick='GetPrevResult(" + countResultIndex + ",this)' class='prevResult active'>" + countResultIndex + "</a>");
                    // $('select option[value="0"]').attr('selected', 'selected');
                    $(".nano").nanoScroller({ alwaysVisible: true });
                    $(".nano").nanoScroller({ scroll: 'top' });
                }
                else {
                    $(".newresult").text(" ").text("no more results").removeAttr("onclick");
                }


            },
            complete: function (response) {
            }
        });
    };
};
function GetPrevResult(countIndex,link) {
    if (durationVal == null && durationVal == undefined) {
        durationVal = 0;
    }
   // //console.log(durationVal);
    if ($("#txtKeyword").val().length > 0) {
        var keyword = $("#txtKeyword").val();
    //    //console.log(keyword);
        $.ajax({
            type: "POST",
            url: webMethodGetMoreResults,
            cashe: false,
            data: '{"keyword":' + "'" + keyword + "'" + ',"durationVal":' + "'" + durationVal + "'" + ',"countIndex":' + "'" + countIndex + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#divResulSearchURL").html(response.d);
                addCategoriesToSelect();
                $(".prevResult").removeClass("active");
                $(link).addClass("active");
                $('select option[value="0"]').attr('selected', 'selected');               
                $(".nano").nanoScroller({ alwaysVisible: true });
            }
        });
    };
   // countResultIndex = countIndex;
};
function addToSchedule(btn) {
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var id = idArr[1];
    var schdeduleList = new Object();
    schdeduleList.title = $("#txtCustomTitle_" + id).text();
    schdeduleList.description = $("#txtCustomizeDescription_" + id).text();
    schdeduleList.category = $("#selectId_" + id + " option:selected").text();
    schdeduleList.thumbnailUrl = $("#boxContent_" + id + " img").attr("src");
    // var durString = $("#boxContent_"+id+" .duration").text().split(",");
    schdeduleList.duration = $("#boxContent_" + id + " .durationHidden").val();
    schdeduleList.videoProvider = "youtube";
    schdeduleList.videoId = $("#boxContent_" + id + " .btnPlay").attr("id");
    // //console.log(schdeduleList);
    if (schdeduleList.title.length > 0) {
        //create jsonObject
        // var jsonScheduleListData = [{ videoProvider: "youtube", thumbnailUrl: thumbUrl, duration: duration, description: description, videoId: videoId }]
        //check if not in schedule yet
        $.ajax({
            type: "POST",
            url: webMethodPostScheduleList,
            cashe: false,
            data: "{'scheduleList':" + JSON.stringify(schdeduleList) + "}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#boxContent_" + id + " .divCustomize span.spnError").text("").text(response.d);
                //console.log(response.d);
            }
        });
    }
};
function isCheckedById(id) {

    var checked = $("#boxContent_" + id + " .chkBox:checked").length;

    if (checked == 0) {
        return false;
    } else {
        return true;
    }
};

function AddRemoveSelectedVideoToArrayPL(btn) {
    void 0
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var id = idArr[1];
    var videoTube = new Object();
    //console.log(videoTube);
    videoTube.categoryId = $("#boxContent_" + id + " input.hidden").val();
    //if (videoTube.categoryId == 0) {
    //    alert("Please select category of where videos shall be added to");
    //    return;
    //}
    videoTube.title = $("#boxContent_" + id + " .title").text();
    videoTube.description = $("#txtCustomizeDescription_" + id).text();
    videoTube.videoPath = $("#boxContent_" + id + " .btnPlay").attr("id");
    //var durString = $("#boxContent_" + id + " .duration").text().split(",");
    videoTube.duration = $("#boxContent_" + id + " .durationHidden").val();
    videoTube.videoThumbnail = $("#boxContent_" + id + " img").attr("src");
    videoTube.videoCount = 0;
    videoTube.isScheduled = false;
    videoTube.useCount = 0;
    videoTube.provider = "youtube";
    videoTube.r_rated = isCheckedById(id);
    var isChecked = $('#' + btnId).is(":checked")
    void 0;
    if (isChecked == true) {
        videoTubeObjects.push(videoTube);
        AddVideoCount++;
    }
    else {
        videoTubeObjects.splice($.inArray(videoTube, videoTubeObjects), 1);
        if (AddVideoCount != 0) {
            AddVideoCount--;
        }
        // videoTubeObjects.remove(videoTube);


    }
   
    // videoTubeObjects.push(videoTube);
};
var categoryName;
function SetCategory() {


    var selectedCatId = $('#ddlKeyWord :selected').val();
    $(".ddlKeyWord option[value='" + selectedCatId + "']").attr('selected', 'selected');

    return selectedCatId;
};
function SetCategoryUrl() {
    var selectedCatId = $('#ddlUrl :selected').val();
    $(".ddlKeyWord option[value='" + selectedCatId + "']").attr('selected', 'selected');
    return selectedCatId;
};
function AddAllSelectedToVR(type) {

    var categoryName
    var catId;
    if (type == "keyword") {
        categoryName = $('#ddlKeyWord :selected').text();
        catId = SetCategory();
    }
    else {
        categoryName = $('#ddlUrl :selected').text();
        catId = SetCategoryUrl();
    }

    if ((catId == 0) && (type != "pubLib")) {
        ShowMessage("Please select a category for the videos to be added to.");
        return;
    }
    else {
        var r;
        if (type != "pubLib")
        {
            r = confirm("you adding " + AddVideoCount + " videos to your video room and schedule list under " + categoryName + " category");
        }
        else
        {
            r = confirm("you adding " + AddVideoCount + " videos to your video room and schedule");
        }
         
        if (r == true) {
            $.ajax({
                type: "POST",
                url: "WebServices/VideoRoomService.asmx/AddAllToVideoRoom",
                cashe: false,
                data: "{'videoTubeList':" + JSON.stringify(videoTubeObjects) + ',"categoryId":' + catId + "}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    $("#loadingDiv").show();
                },
                success: function (response) {
                    videoTubeObjects = new Array();
                    $("#loadingDiv").hide();
               
                    //$("#boxContent_" + id + " .divCustomize span.spnError").text("").text(response.d);
                    ShowMessage(response.d);
                    AddVideoCount = 0;
                    //get all boxes marked as Video is added  
                    $(':checkbox').each(function () {
                        if(this.checked=="1")
                        {
                            var arrid = $(this).attr("id");
                            var idArr = arrid.split("_");
                            var id = idArr[1];
                            void 0;
                            $("#boxContent_" + id +" .actions input").css("display", "none");
                            $("#boxContent_" + id + " .spnError").show().text("").text("Video is added");
                          
                        }
                    });
                }
            });
        }
        else {
            return;
        }
    }

};
function addToVideoTube(btn) {
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var id = idArr[1];
    var videoTube = new Object();
    //console.log(videoTube);
    videoTube.title = $("#txtCustomTitle_" + id).text();
    videoTube.description = $("#txtCustomizeDescription_" + id).text();
    videoTube.videoPath = $("#boxContent_" + id + " .btnPlay").attr("id");
    //var durString = $("#boxContent_" + id + " .duration").text().split(",");
    videoTube.duration = $("#boxContent_" + id + " .durationHidden").val();
    videoTube.videoThumbnail = $("#boxContent_" + id + " img").attr("src");
    videoTube.videoCount = 0;
    videoTube.isScheduled = false;
    videoTube.useCount = 0;
    videoTube.categoryId = $("#selectId_" + id + " option:selected").val();
    if (videoTube.categoryId == 0) {
        alertify.alert("Please select a category for the videos to be added to.");
        return;
    }
    videoTube.provider = "youtube";
    videoTube.r_rated = isCheckedById(id);

    if (videoTube.title.length > 0) {
        //create jsonObject
        // var jsonScheduleListData = [{ videoProvider: "youtube", thumbnailUrl: thumbUrl, duration: duration, description: description, videoId: videoId }]
        //check if not in schedule yet
        $.ajax({
            type: "POST",
            url: webMethodAddToVideoRoom,
            cashe: false,
            data: "{'videoTube':" + JSON.stringify(videoTube) + "}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#boxContent_" + id + " .divCustomize span.spnError").text("").text(response.d);
            }
        });
    }
};
function AddToVRFromPublic(btn) {
    var btnId = btn.id;
    var idArr = btnId.split("_")
    var id = idArr[1];
    $.ajax({
        type: "POST",
        url: webMethodAddToVrFromPubliLib,
        cashe: false,
        data: "{'publicId':" + id + "}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#boxContent_" + id + " .divCustomize span.spnError").text("").text(response.d);
            //console.log(response.d);
            $("#" + btn.id).remove();
            $(".spnInScedule#spnInScedule_" + btn.id).show();
            $(btn.id).hide();

        }
    });
};
function showPlayer(elemnt) {
    $('.playerbox').lightbox_me({
        centered: true,
        onLoad: function () {
            addPlayer(elemnt)
        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {

            player = new YT.Player('related')
            $(".playerbox").html('<div id="related" class="player"></div> <div id="content-container"></div><a id="close_x" class="close close_x closePlayerBox" href="#">close</a>')
            player.destroy();
            //console.log("in closed")
        }
    });
    //  e.preventDefault();
};
function CheckIfExistInVR(boxContainer)
{
   
    var videoPath = $("#" + boxContainer.id + " .btnPlay").attr("id");   
    $.ajax({
        type: "POST",
        url: webMethodIsInVideoRoom,
        data: '{"videoPath":' + "'" + videoPath +"'"+ '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d == true) {
                $("#" + boxContainer.id + " .actions input").css("display", "none");
                $("#" + boxContainer.id + " .spnError").show().text("").text("Already in video room");
                $("#" + boxContainer.id).removeAttr("onmouseover");
            }
        }
    });
}


