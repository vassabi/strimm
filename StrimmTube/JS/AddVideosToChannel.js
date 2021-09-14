var webMethodGetPublicLibResult = "/WebServices/PublicLibraryService.asmx/GetAllPublicVideosByPageIndex";
var webMethodGetChannelCategoriesForPublicLibrary = "/WebServices/PublicLibraryService.asmx/GetVideoCategoriesForPublicLibrary";

var webMethodAddMoreVideos = "/WebServices/VideoRoomService.asmx/GetAllVideoTubePoByPageIndexAndUserId";
var webMethodGetChannelCategoriesForVideoRoom = "/WebServices/VideoRoomService.asmx/GetVideoCategoriesForVideoRoom";

var webMethodAddVideoToChannel = "/WebServices/ChannelWebService.asmx/AddVideoToChannel";
var webMethodGetChannelCategoriesJson = "/WebServices/ChannelWebService.asmx/GetChannelCategories";

var webMethodGetVideoByUrl = "/WebServices/YouTubeWebService.asmx/FindVideoByUrl";
var webMethodGetVideoByKeyWord = "/WebServices/YouTubeWebService.asmx/FindVideosByKeywords";

var countResultIndex = 0;
var pageIndex = 1;
var prevPageIndex = 0;
var nextPageIndex = 0;

// -----------------------------------------------------------------------------------------------------
// Common
// -----------------------------------------------------------------------------------------------------
function ChangeCategoryHandler() {
    pageIndex = 1;
}

function AddVideoToChannel(btn) {
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
        }
    });
}
// -----------------------------------------------------------------------------------------------------
// End
// -----------------------------------------------------------------------------------------------------

// -----------------------------------------------------------------------------------------------------
// Public Library Methods
// -----------------------------------------------------------------------------------------------------
function LoadVideoCategoriesForPublicLibrary() {
    $.ajax({
        type: "POST",
        url: webMethodGetChannelCategoriesForPublicLibrary,
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
};

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
// -----------------------------------------------------------------------------------------------------
// End
// -----------------------------------------------------------------------------------------------------

// -----------------------------------------------------------------------------------------------------
// Search By Keywords Methods
// -----------------------------------------------------------------------------------------------------
function GetChannelCategories() {
    $.ajax({
        type: "POST",
        url: webMethodGetChannelCategoriesJson,
        cashe: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var json = response.d;
            var categoryData = JSON.parse(json);
            var select = $(".spnCat");
            select.empty();
            select.append($('<option></option)').attr("value", "0").text("choose category"));
            $.each(categoryData, function () {
                select.append($('<option></option)').attr("value", this.value).text(this.text));
            });
        }
    });
};

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
                    var videoControls = Controls.BuildVideoRoomControl(pageType, videos);
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
            complete: function (response) {
                window.clearInterval(myVar);
            }

        });
    }
    else {
        $("#divResulSearchURL").text("Please enter keyword");
    }

};

function AddCategoriesToSelect() {
    var select = $(".spnCat");
    select.empty();
    select.append($('<option></option)').attr("value", "0").text("choose category"));
    $.each(categoryData, function () {
        select.append($('<option></option)').attr("value", this.value).text(this.text));
    });
};
// -----------------------------------------------------------------------------------------------------
// End
// -----------------------------------------------------------------------------------------------------

// -----------------------------------------------------------------------------------------------------
// Search By URL
// -----------------------------------------------------------------------------------------------------
function GetYouTubeVideoByURL() {
    var uniqueId = GetUniqueId();
    $("#divUrl").show();
    var url = $("#txtURL").val();
    if (url.length <= 0) {
        $("#divResulSearchURL").text("Please enter URL");
    }
    else {
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
                var videoControls = Controls.BuildVideoRoomControl("public-library", videos);

                $("#divResulSearchURL").append(videoControls);
                $(".nano").nanoScroller({ alwaysVisible: true });
            }
        });
    }

};

// -----------------------------------------------------------------------------------------------------
// End
// -----------------------------------------------------------------------------------------------------

// -----------------------------------------------------------------------------------------------------
// Video Room Methods
// -----------------------------------------------------------------------------------------------------
function LoadVideoCategoriesForVideoRoom() {
    $.ajax({
        type: "POST",
        url: webMethodGetChannelCategoriesForVideoRoom,
        cashe: false,
        data: '{"userId":' + userId + '}',
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

            if (pageName != "watch-it-later") {
                LoadMoreVideos();
            }
        }
    });
};

function LoadMoreVideos() {
    var categoryId = $(".ddlCategory option:selected").val();

    $.ajax({
        type: "POST",
        url: webMethodAddMoreVideos,
        cashe: false,
        data: '{"pageIndex":' + pageIndex + ', "userId":' + userId + ', "categoryId":' + categoryId + ', "channelTubeId":' + channelId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#loadingDiv").show();
        },
        success: function (response) {
            $("#loadingDiv").hide();
            if (response.d != "0") {
                var data = response.d;
                pageIndex = data.NextPageIndex;
                prevPageIndex = data.PrevPageIndex;
                nextPageIndex = data.NextPageIndex;
                var pageSize = data.PageSize;
                var pageCount = data.PageCount;

                if (data.PageIndex == pageCount) {
                    $("#ancLoadMore").hide();
                }
                else {
                    $("#ancLoadMore").show();
                }

                if (data.VideoTubeModels) {
                    var videos = data.VideoTubeModels;
                    var pageType = "video-room";
                    var videoControls = Controls.BuildVideoRoomControl(pageType, videos);
                    $("#videoBoxHolder").append(videoControls);
                }

                $("html, body").animate({ scrollTop: $(document).height() }, "fast");
                if (pageName == "watch-it-later") {
                    $(".actions").css("visibility", "hidden");
                    $(".removedVideo").remove();
                    $(".divBoxContent").each(function (index) {
                        //console.log(index + ": " + $(this).id);
                        var varid = $(this).attr("id").split("_");
                        var id = varid[1];
                    });
                }

                //startIndex++;
                $("#loadingDiv").hide();
            }
            else {
                $("#ancLoadMore").removeAttr("onclick").text("").text("no more videos");
                $("html, body").animate({ scrollTop: $(document).height() }, "fast");
                $("#loadingDiv").hide();
                if (startIndex == 0) {
                    $(".videoBoxHolder").html("<span id='lblMessage'>You do not have videos in Video Room yet</span>");
                }
            }

        }
    });
}

function RemoveVideoFromVideoRoom(btn) {
    void 0;
    var btnId = btn.id;
    var idArr = btnId.split("_");
    var videoTubeId = idArr[1];
    void 0;
    $.ajax({
        type: "POST",
        url: webMethodRemoveVideoFromVr,
        cashe: false,
        data: '{"userId":' + userId + ',"videoTubeId":' + videoTubeId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#boxContent_" + videoTubeId).hide().css("visibility", "hidden");
            void 0;
        }
    });
};
// -----------------------------------------------------------------------------------------------------
// End
// -----------------------------------------------------------------------------------------------------
