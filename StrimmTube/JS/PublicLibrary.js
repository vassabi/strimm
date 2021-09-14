var webMethodSortVideos = "/WebServices/YouTubeWebService.asmx/SortVideos";
var webMethodGetMoreResults = "/WebServices/YouTubeWebService.asmx/GetMoreResults";
var webMethodGetChannelCategoriesJson = "/WebServices/PublicLibraryService.asmx/GetVideoCategoriesForPublicLibrary";
var webMethodPostScheduleList = "/WebServices/YouTubeWebService.asmx/PostScheduleList";
var webMethodAddVideoToChannel = "/WebServices/ChannelWebService.asmx/AddVideoToChannel";
var webMethodGetPublicLibResult = "/WebServices/PublicLibraryService.asmx/GetAllPublicVideosByPageIndex";
var webMethodAddToVrFromPubliLib = "/WebServices/YouTubeWebService.asmx/AddToVrFromPubliLib";
var webMethodAddListToVideoRoom = "/WebServices/VideoRoomService.asmx/AddListToVideoRoom";
var webMethodIsInVideoRoom = "/WebServices/VideoRoomService.asmx/IsInVideoRoom";
var webMethodAddMoreVideosByCategory = "/WebServices/PublicLibraryService.asmx/AddMoreVideosByCategoryPublicLib";
var webMethodClearSession = "/WebServices/YouTubeWebService.asmx/ClearSession";


var categoryData;
var publicLibStartIndex;
var startIndexByCategory = 0;
var videoTubeObjects = new Array();

var AddVideoCount = 0;

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
    
    pageIndex = 1;

    $.ajax({
        type: "POST",
        url: webMethodGetChannelCategoriesJson,
        cashe: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            categoryData = response.d;
            $.each(categoryData, function (i, c) {
                $('.ddlCategory').append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
                if (i == 0) {
                    $('.ddlCategory option[value="' + 0 + '"]').prop('selected', true);
                }
            });

            GetPublicLibrary();
        }
    });

});

//get videoByKeyword
function ChangeCategoryHandler() {
    pageIndex = 1;
    $("#divResulSearchURL").empty();
    GetPublicLibrary();
}

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

function addPlayer(videoPath) {
    var stringId = videoPath.id;
    var idArr = stringId.split("_");
    var id = idArr[1];    
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

function GetUniqueId() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
};

function getYouTubeVideoByURL() {
    var uniqueId = GetUniqueId();
    $("#divUrl").show();
    var url = $("#txtURL").val();
    if (url.length <= 0) {
        $("#divResulSearchURL").text("");
        alertify.error("Please enter URL");
    }
    else {
        $.ajax({
            type: "POST",
            url: webMethodGetVideoByUrl,
            cashe: false,
            data: '{"url":' + "'" + url + "'" + ',"uniqueId":' + "'" + uniqueId + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#divResulSearchURL").append(response.d);
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
var pageIndex = 1;
var prevPageIndex = 0;
var nextPageIndex = 0;

function ChangeCategoryHandler() {
    pageIndex = 1;
}

function GetPublicLibrary() {
    $("#loadingDiv").show();
    addPublicLibMoreResultControl();
    //$("#divPrevResults").hide();
    //$(".newresult").hide();
    $("#divNext").show();
    //$("#btnResetResultURL").hide();
    $("#divPageResults").show();
    //$(".newresult").removeClass("hidden").attr("onclick", "GetMoreResults()");
    $("#divPrevResults").html("").append("<a onclick='GetPrevResult(" + 1 + ",this)' class='prevResult active'>" + 1 + "</a>");
    countResultIndex = 0;
    countResultIndex = 1;
    var categoryId = $("#ddlCategory option:selected").val();
    $.ajax({
        type: "POST",
        url: webMethodGetPublicLibResult,
        cashe: false,
        data: '{"pageIndex":' + pageIndex + ',"categoryId":' + categoryId + ',"channelTubeId":' + channelTubeId + '}',
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var data = response.d;
            pageIndex = data.NextPageIndex;
            prevPageIndex = data.PrevPageIndex;
            nextPageIndex = data.NextPageIndex;
            var pageSize = data.PageSize;
            var pageCount = data.PageCount;

            if (data.PageIndex == pageCount) {
                $("#loadModeResults").hide();
            }
            else {
                $("#loadModeResults").show();
            }

            if (data.VideoTubeModels) {
                var videos = data.VideoTubeModels;
                var pageType = "public-library";
                var videoControls = Controls.BuildVideoRoomControl(pageType, videos);
                $("#divResulSearchURL").append(videoControls);
            }

            $("#divSortByURL").show();
        },
        complete: function () {
            $("#loadingDiv").hide();
        }
    });
    
};

function GetPublicLibraryByCatregory()
{
    ChangeCategoryHandler();
    GetPublicLibrary();
}

function sortVideos(select) {
    var $wrapper = $('#divResulSearchURL');


    var value;
    var selectedValue = select.value;
    if (selectedValue !== null && selectedValue !== undefined) {
        value = selectedValue;
    }
    else {
        value = 0;
    }
    switch (value) {
        case "1":
            $wrapper.find('.divBoxContent').sort(function (a, b) {
                return +b.getAttribute('data-duration') - +a.getAttribute('data-duration');
            })
  .appendTo($wrapper);
            break;
        case "2":
            $wrapper.find('.divBoxContent').sort(function (a, b) {
                return +a.getAttribute('data-duration') - +b.getAttribute('data-duration');
            })
 .appendTo($wrapper);
            break;
        case "3":
            $wrapper.find('.divBoxContent').sort(function (a, b) {
                return +b.getAttribute('data-views') - +a.getAttribute('data-views');
            })
 .appendTo($wrapper);
            break;

    }
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
        var msg = "Please select a category for the videos to be added to.";
        ShowMessage(msg);
        return;
    }
    else {
        var r;
        if (type != "pubLib") {
            r = confirm("you adding " + AddVideoCount + " videos to your video room and schedule list under " + categoryName + " category");
        }
        else {
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
                        if (this.checked == "1") {
                            var arrid = $(this).attr("id");
                            var idArr = arrid.split("_");
                            var id = idArr[1];
                            void 0;
                            $("#boxContent_" + id + " .actions input").css("display", "none");
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
//function addToVideoRoom(btn) {
//    var btnId = btn.id;
//    var idArr = btnId.split("_");
//    var id = idArr[1];
//    var videoTube = new Object();
//    //console.log(videoTube);
//    videoTube.title = $("#boxContent_" +  id + " .divVideoInformation .title").text();    
//    videoTube.videoPath = $("#boxContent_" + id + " .btnPlay").attr("id");
//    //var durString = $("#boxContent_" + id + " .duration").text().split(",");
//    videoTube.duration = $("#boxContent_" + id + " .durationHidden").val();
//    videoTube.videoThumbnail = $("#boxContent_" + id + " img").attr("src");
//    videoTube.videoCount = 0;
//    videoTube.isScheduled = false;
//    videoTube.useCount = 0;
//    videoTube.categoryId = $("#selectId_" + id + " option:selected").val();
//    if (videoTube.categoryId == 0) {
//        alert("Please select category of where videos shall be added to");
//        return;
//    }
//    videoTube.provider = "youtube";
//    videoTube.r_rated = isCheckedById(id);

//    if (videoTube.title.length > 0) {
//        //create jsonObject
//        // var jsonScheduleListData = [{ videoProvider: "youtube", thumbnailUrl: thumbUrl, duration: duration, description: description, videoId: videoId }]
//        //check if not in schedule yet
//        $.ajax({
//            type: "POST",
//            url: webMethodAddVideoToChannel,
//            cashe: false,
//            data: "{'videoTube':" + JSON.stringify(videoTube) + "}",
//            dataType: "json",
//            contentType: "application/json; charset=utf-8",
//            success: function (response) {
//                $("#boxContent_" + id + " .divCustomize span.spnError").text("").text(response.d);
//            }
//        });
//    }
//};
function addVideoToChannel(btn)
{
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var videoTubeId = idArr[1];
    void 0;
    $.ajax({
                    type: "POST",
        url: webMethodAddVideoToChannel,
                    cashe: false,
                    data: '{"channelTubeId":' + channelTubeId + ',"videoTubeId":' + videoTubeId + '}',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                       // console.log(response.d);
                        //$("#boxContent_" + id + " .divCustomize span.spnError").text("").text(response.d);
                        $("#addId_" + videoTubeId).text("").text("Added to channel").removeAttr("onclick");
                    }
                });
}

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

function CheckIfExistInVR(boxContainer) {

    var videoPath = $("#" + boxContainer.id + " .btnPlay").attr("id");
    $.ajax({
        type: "POST",
        url: webMethodIsInVideoRoom,
        data: '{"videoPath":' + "'" + videoPath + "'" + '}',
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

function GetMorePublicLib() {
    var categoryVal = $(".ddlCategory option:selected").val();
    publicLibStartIndex++;
    $("#loadingDiv").show();
    $.ajax({
        type: "POST",
        url: webMethodGetPublicLibResult,
        cashe: false,
        // data: '{"startIndex":' + publicLibStartIndex + ',"selectedValue":' + categoryVal + '}',
        data: '{"startIndex":' + publicLibStartIndex + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d != "0") {
                //console.log(response.d);
                $("#divResulSearchURL").append(response.d);

                $("#loadingDiv").hide();

            }
            else {
                $(".ancLoadMorePubLib").removeAttr("onclick").text("").text("no more videos");

            }

        }
    });
}

function LoadMoreByCategory() {
    var categoryVal = $(".ddlCategory option:selected").val();
    publicLibStartIndex++;
    $("#loadingDiv").show();
    $.ajax({
        type: "POST",
        url: webMethodAddMoreVideosByCategory,
        cashe: false,
        data: '{"startIndex":' + startIndexByCategory + ',"selectedValue":' + categoryVal + '}',

        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d != "0") {
              
                $("#divResulSearchURL").append(response.d);

            }
            else {
                void 0;
                $(".ancLoadMorePubLib").removeAttr("onclick").text("").text("no more videos");


            }

        },
        complete: function () {
            $("#loadingDiv").hide();
        }
    });

}




