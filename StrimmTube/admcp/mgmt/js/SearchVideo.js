var webMethodGetVideoByUrl = "../../WebServices/YouTubeWebService.asmx/GetVideoByUrl";
var webMethodClearSession = "../../WebServices/YouTubeWebService.asmx/ClearSession";
var webMethodRemoveVideo = "../../WebServices/YouTubeWebService.asmx/RemoveVideo";
var webMethodGetVideoByKeyWord = "../../WebServices/YouTubeWebService.asmx/GetVideoByKeyWord";
var webMethodSortVideos = "../../WebServices/YouTubeWebService.asmx/SortVideos";
var webMethodGetMoreResults = "../../WebServices/YouTubeWebService.asmx/GetMoreResults";
var webMethodGetChannelCategoriesJson = "../../WebServices/ChannelWebService.asmx/GetChannelCategories";
var webMethodPostScheduleList = "../../WebServices/YouTubeWebService.asmx/PostScheduleList";
//var webMethodAddToVideoRoom = "../../WebServices/YouTubeWebService.asmx/AddVideoTube";
var webMethodAddToPublicLibrary = "../../WebServices/PublicLibraryService.asmx/AddVideoToPUblicLibrary";
var webMethodCheckIfVideoInPublicLib = "../../WebServices/PublicLibraryService.asmx/CheckIfVideoInOublicLib";
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
$(document).ready(function () {
    $(document).keyup(function (event) {
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
    
    $(".spanLink").mouseover(function () {
        $("#divShowImage").show();
    });
    $(".spanLink").mouseout(function () {
        $("#divShowImage").hide();
    })
    $(window).unload(function () {
       // //console.log("onbeforeload");
        $("select").value = 0;
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
    window.onbeforeunload = function () {
        $('select option[value="0"]').attr('selected', 'selected');
    };
    //$(".nano").nanoScroller({ alwaysVisible: true });
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
});
function addCategoriesToSelect() {
    var select = $(".spnCat");
    select.empty();
    select.append($('<option></option)').attr("value", "0").text("choose category"));
    $.each(categoryData, function () {
        select.append($('<option></option)').attr("value", this.value).text(this.text));
    });
}
//show video info
function ToggleVideoInfo(element) {
    var stringId = element.id;
   // var matches = stringId.match(/(.*?_.*?)(_.*)/);
   // var firstPart = matches[1];
   // var secondPart = matches[2];
     var idArr = stringId.split("_");
     var id = idArr[1];
     var videopathId = $("#boxContent_" + id + " .divVideoThumb a").attr("id");
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
}
function ResetResult() {
    $.ajax({
        type: "POST",
        url: webMethodClearSession,
        cashe: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#divResulSearchURL").html("");
            $(".nano").nanoScroller({ alwaysVisible: true });
            $("#divPageResults").hide();
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
//function addPlayer(videoPath) {
//    var stringId = videoPath.id;
//    var idArr = stringId.split("_");
//    var id = idArr[1];
//    //flowplayer("related", "flowplayer.youtube/flowplayer.swf", {
//    //    plugins: {
//    //        youtube: {
//    //            url: "flowplayer.youtube/flowplayer.youtube-3.2.11.swf",
//    //            enableGdata: true,
//    //            loadOnStart: true,
//    //            onVideoRemoved: function () {
//    //                //console.log("Video Removed");
//    //            },
//    //            onVideoError: function () {
//    //                //console.log("Incorrect Video ID");
//    //            },
//    //            onEmbedError: function () {
//    //                //console.log("Embed Not Allowed");
//    //            },
//    //            onApiData: function (data) {
//    //                // use the function defined above to show the related clips
//    //                // //console.log(data);
//    //                // use the function defined above to show the related clips
//    //                // showInfo(data, "#playlist1");
//    //                //load the related list for the first clip only
//    //                // if (shownRelatedList) return;
//    //                // showRelatedList(data, "#playlist1");
//    //                // shownRelatedList = true;
//    //            },
//    //            displayErrors: false
//    //        }
//    //    },
//    //    clip: {
//    //        provider: "youtube",
//    //        url: "api:" + videoPath.id,
//    //        autoPlay: true,
//    //        // start: 50
//    //        onLastSecond: function (clip) {
//    //            // //console.log(data[clip.index].title);
//    //        }
//    //    },
//    //    //log: {
//    //    //    level: 'debug',
//    //    //    filter: 'org.electroteque.youtube.*, org.flowplayer.controller.*'
//    //    //}
//    //});
//    function onYouTubeIframeAPIReady() {
//        player = new YT.Player('related', {
//            height: '546',
//            width: '728',

//            videoId: videoPath.id,
//            playerVars: {
//                //controls: 0,
//                // showinfo: 0,
//                autoplay: 1,
//                html5: 1

//            },
//            events: {
//                'onReady': onPlayerReady,
//                'onStateChange': onPlayerStateChange
//            }
//        });

//        //console.log("onYouTubeIframeAPIReady");
//    }


//    onYouTubeIframeAPIReady();


//    // 4. The API will call this function when the video player is ready.
//    function onPlayerReady(event) {
//        event.target.playVideo();
//    }

//    // 5. The API calls this function when the player's state changes.
//    //    The function indicates that when playing a video (state=1),
//    //    the player should play for six seconds and then stop.
//    var done = false;
//    function onPlayerStateChange(event) {

//        if (event.data == YT.PlayerState.PLAYING && !done) {
//            // setTimeout(stopVideo, 6000);

//            done = true;
//            ////console.log(done);
//        }
//    }
//};

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
//get videoByKeyword
function GetYouTubeVideoByKeyword() {

    $("#divPrevResults").show();
    if (durationVal == null && durationVal == undefined) {
        durationVal = 0;
    }
    if ($("#txtKeyword").val().length > 0) {
        var keyword = $("#txtKeyword").val();
        $("#divNext").show();       
        $("#divPageResults").show();
        $(".newresult").removeClass("hidden").attr("onclick", "GetMoreResults()");
        $("#divPrevResults").html("").append("<a onclick='GetPrevResult(" + 1 + ",this)' class='prevResult active'>" + 1 + "</a>");
        countResultIndex = 0;
        countResultIndex=1;
        $.ajax({
            type: "POST",
            url: webMethodGetVideoByKeyWord,
            cashe: false,
            data: '{"keyword":' + "'" + keyword + "'" + ',"durationVal":' + "'" + durationVal + "'" + ',"startIndex":' + "'" + countResultIndex + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#divSortByURL").show();
                $("#divResulSearchURL").html("").html(response.d);
                addCategoriesToSelect();
                $(".nano").nanoScroller({ alwaysVisible: true });
                $(".nano").nanoScroller({ scroll: 'top' });
            }
        });
    }
}
function sortVideos(select) {
    var value;
    var selectedValue = select.value;
    if (selectedValue !== null && selectedValue !== undefined) {
        value = selectedValue;
    }
    else {
        value = 0;
    }
    $.ajax({
        type: "POST",
        url: webMethodSortVideos,
        cashe: false,
        data: '{"value":' + "'" + value + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#divResulSearchURL").html(response.d);
            addCategoriesToSelect();
            $(".nano").nanoScroller({ alwaysVisible: true });
        }
    });
};
// get more results
function GetMoreResults() {
    countResultIndex++;
    if (durationVal == null && durationVal == undefined) {
        durationVal = 0;
    }
    if (countResultIndex == 18) {
        $(".newresult").removeAttr("onclick").addClass("hidden");
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
            success: function (response) {
                $("#divResulSearchURL").html("").html(response.d);
                addCategoriesToSelect();
                $(".prevResult").removeClass("active");
                $("#divPrevResults").append("<a onclick='GetPrevResult(" + countResultIndex + ",this)' class='prevResult active'>" + countResultIndex + "</a>");
                $('select option[value="0"]').attr('selected', 'selected');
                $(".nano").nanoScroller({ alwaysVisible: true });
                $(".nano").nanoScroller({ scroll: 'top' });
            },
            complete: function (response) {
            }
        });
    };
}
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
}
function isCheckedById(id) {
    
    var checked = $("#boxContent_" + id + " .chkBox:checked").length;   

    if (checked == 0) {
        return false;
    } else {
        return true;
    }
}
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
    if (videoTube.categoryId == 0)
    {
        alert("Please choose video category");
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
            url: webMethodAddToPublicLibrary,
            cashe: false,
            data: "{'videoTube':" + JSON.stringify(videoTube) + "}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                //$("#boxContent_" + id + " .divCustomize span.spnError").text("").text(response.d);
                $("#txtURL").val('');
                location.reload();
            }
        });
    }
}

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
}
