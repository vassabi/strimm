

var webMethodGetChannelCategoriesForPublicLibraryJson = "../../WebServices/PublicLibraryService.asmx/GetVideoCategoriesForPublicLibrary";
var webMethodGetPublicLibResult = "/WebServices/PublicLibraryService.asmx/GetAllPublicVideosByPageIndex";
var webMethodRemoveVideoFromPublicLibrary = "../../WebServices/PublicLibraryService.asmx/RemoveVideoFromPUblicLib";

var pageIndex = 1;
var prevPageIndex = 1;
var nextPageIndex = 1;
var channelId = 0;

$(document).ready(function () {
    $("#spnAdmin").text("admin-panel public library")
    $("#loadingDiv").show();
    $('select option[value=0]').attr('selected', 'selected');
    LoadPublicLibrary();

    var pageIndex = 1;
    var prevPageIndex = 1;
    var nextPageIndex = 1;
    $("#loadingDiv").hide();
});
    function LoadPublicLibrary() {
 
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

                GetPublicLibrary();
            }
        });
    };
    pageIndex = 1;
    function GetPublicLibrary(isSearching) {
        countResultIndex = 0;
        countResultIndex = 1;
        isSearching = false;
        var categoryId = $("#ddlPublicLibraryCategory option:selected").val();
       // var keywords = $('#txtSearchVideoByKeywordForPublicLib').val();

        if (isSearching) {
            pageIndex = 1;
            $(".loadedContent").html("");
        }

        if (categoryId == null) {
            categoryId = 0;
        }

        var searchCriteria = {
            PageIndex: pageIndex,
            CategoryId: 0,
            ChannelTubeId: 0,
            Keywords: ""
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
                    var videoControls = BuildViodeBoxControlForAddVideosPage(true, videos); //Controls.BuildVideoRoomControl(pageType, videos, false);
                    $(".loadedContent").append(videoControls);
                }

                $("#sortSelect").show();
            },
            complete: function () {
                $("#loadingDiv").hide();
            }
        });
    };

    function BuildViodeBoxControlForAddVideosPage(inPublicLib, videos) {
    var final = "";

    $.each(videos, function (i, d) {
        if (d != null)
        {
            var videoId = d.VideoTubeId;
            var title = d.Title;
            var descripton = d.Description;
            var viewCounter = d.ViewCounter;
            var durationInSec = d.Duration;
            var categoryName = d.CategoryName;
            var isScheduled = d.IsScheduled;
            var isPrivate = d.IsPrivate;
            var isRRated = d.IsRRated;
            var isRestricted = d.IsRestrictedByProvider;
            var isRemoved = d.IsRemovedByProvider;
            var providerVideoId = d.ProviderVideoId;
            var thumbnail = d.Thumbnail;
            var durationString = d.DurationString;
            var isInChannel = d.IsInChannel;
            var allowDelete = true;
            var dateAdded = d.DateAdded;
            var isExternalVideo = videoId == 0;
            var providerName = d.VideoProviderName;
            var providerImgSrc;

            if (thumbnail == null || thumbnail == "") {
                thumbnail = "/images/comingSoonBG.jpg";
            }

            if (providerName == "vimeo") {
                providerImgSrc = "/images/vimeo-icon.png";
            }
            else if(providerName=="dailymotion")
            {
                providerImgSrc = "/images/dMotion.png";
            }
            else {
                providerImgSrc = "/images/youTube-icon.png";
            }
            if (!thumbnail) {
                thumbnail = providerImgSrc;
            }

            final += "<div class='videoBoxNew'  data-provider='" + providerName + "' data-duration='" + durationInSec + "' data-views='" + viewCounter;
            if (isExternalVideo) {
                videoId = channelBoxIdCount;
                channelBoxIdCount++;

                final += "' data-videoadd='0' id='addBoxContent_" + providerVideoId;
            }
            else {
                if (isInChannel) {
                    final += "' data-videoadd='1' id='addBoxContent_" + videoId;
                }
                else {
                    final += "' data-videoadd='0' id='addBoxContent_" + videoId;
                }
            }
            final += "' date-added='" + dateAdded + "'>" +
                      "<div  class='divProviderLogo'><img src='" + providerImgSrc + "'/></div>" +
                     "<div class='actionsVideoBoxHolder' id='action_" + videoId + "'>";


            if (isRemoved) {
                final += "<div class='videoRemoveMessage'>removed by provider</div>";
                final += "<div class='VideoBoxRemove' id='remove_" + videoId + "' onclick='RemoveVideoFromPL(this)'></div>";
            }
            else if (isRestricted) {
                final += "<div class='videoRemoveMessage'>restricted by provider</div>";
                final += "<div class='VideoBoxRemove' id='remove_" + videoId + "' onclick='RemoveVideoFromPL(this)'></div>";
            }
            else {
                if (providerName == "vimeo") {
                    final += "<div class='VideoBoxPlay' data-boxContentId='" + videoId + "' id='" + providerVideoId + "' onclick='showPlayerVimeo(this)'></div>";
                    final += "<div class='VideoBoxRemove' id='remove_" + videoId + "' onclick='RemoveVideoFromPL(this)'></div>";
                }
                else if(providerName=="dailymotion")
                {
                    final += "<div class='VideoBoxPlay' data-boxContentId='" + videoId + "' id='" + providerVideoId + "' onclick='showPlayerDmotion(this)'></div>";
                    final += "<div class='VideoBoxRemove' id='remove_" + videoId + "' onclick='RemoveVideoFromPL(this)'></div>";
                }
                else {
                    final += "<div class='VideoBoxPlay' data-boxContentId='" + videoId + "' id='" + providerVideoId + "' onclick='showPlayer(this)'></div>";
                    final += "<div class='VideoBoxRemove' id='remove_" + videoId + "' onclick='RemoveVideoFromPL(this)'></div>";
                }

                if (isInChannel) {
                    final += "<span class='recentlyadded'>in channel</span>";
                }
                else {
                    if (isExternalVideo) {
                        final += "<span id='recentlyadded_" + providerVideoId + "' class='recentlyadded'></span>";
                    }
                    else {
                        final += "<span id='recentlyadded_" + videoId + "' class='recentlyadded'></span>";
                    }
                }
            }

            final += "<a class='addsymbol' id='addId_" + videoId +"</a>";
          
           

            final += "</div>";
            final += "<div class='videoBoxOverlay'>";
            final += "</div>";
            final += "<div class='addButtonContainer' id='addButtonHolderId_" + providerVideoId + "' style='background-image: url(" + thumbnail + ");'>";
            if (!isRemoved && !isRestricted && !inPublicLib) {
               
            
                   
                final += "<div class='addButtonHolder' id='addvideosId_" + providerVideoId + "' onclick='addExternalVideoToPL(this)' ></div>";
                   
            }

            final += "</div>" +
                 //"<a class='btnPlay' id='" + providerVideoId + "' onclick='showPlayer(this)'>" +
                 //    "<img class='PLAY-ICON' src='/images/PLAY-ICON(!).png' />" +
                 //    "<img class='IM0G' src='" + thumbnail + "' />" +
                 //  "</a>" +
                    "<div class='videoBoxInfo'>" +
                        "<span class='videoBoxtitle ' title='" + title + "'>" + title + "</span>" +
                        "<span class='videoBoxviews'>views:<strong>" + viewCounter + "</strong></span>" +
                        "<span class='videoBoxduration'>duration:<strong>" + durationString + "</strong></span>" +
                        "<input type='hidden' value='" + durationInSec + "' class='durationHidden'/>" +
                        "<input type='hidden' value='" + descripton + "' class='descriptionHidden'/>";


            if (!isExternalVideo) {
                final += "<span class='videoBoxcateg'>category:<strong>" + categoryName + "</strong></span>";
            }

            final += "<input type='hidden' value='" + videoId + "' class ='hidden'/>" +
                     "</div>" +

                     "</div>";
        }

    });

    return final;
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

    function ChangePublicLibraryCategoryHandler() {
        pageIndex = 1;
        prevPageIndex = 1;
        nextPageIndex = 1;

        $(".loadedContent").html("");

        GetPublicLibrary();
    };

function addPlayer(videoPath) {
        var stringId = videoPath.id;
        var idArr = stringId.split("_");
        var id = idArr[1];
        var tag = document.createElement('script');

        tag.src = "https://www.youtube.com/iframe_api";
        var firstScriptTag = document.getElementsByTagName('script')[0];
        firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
        //console.log(videoPath.id);
        // 3. This function creates an <iframe> (and YouTube player)
        //    after the API code downloads.


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

    function LoadMoreResults() {

        GetPublicLibrary();

    };

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
    };

    function sortVideos(videoBoxes, select, wrapper) {

        switch (select) {
            case "1":
                var sorted = videoBoxes.sort(function (a, b) {
                    var aDuration = b.getAttribute('data-duration');
                    var bDuration = a.getAttribute('data-duration');
                    var result = aDuration - bDuration;
                    return result;
                });
                wrapper.empty().append(sorted);
                break;
            case "2":
                var sorted = videoBoxes.sort(function (a, b) {
                    var aDuration = b.getAttribute('data-duration');
                    var bDuration = a.getAttribute('data-duration');
                    var result = bDuration - aDuration;
                    return result;
                });
                wrapper.empty().append(sorted);
                break;
            case "3":
                var sorted = videoBoxes.sort(function (a, b) {
                    var aDuration = b.getAttribute('data-views');
                    var bDuration = a.getAttribute('data-views');
                    var result = aDuration - bDuration;
                    return result;
                });
                wrapper.empty().append(sorted);
                break;
            case "4":
                var sorted = videoBoxes.sort(function (a, b) {
                    var aDateAdded = ((new Date(b.getAttribute('date-added'))).getTime() * 10000) + 621355968000000000;
                    var bDateAdded = ((new Date(a.getAttribute('date-added'))).getTime() * 10000) + 621355968000000000;
                    var result = -bDateAdded + aDateAdded;
                    return result;
                });
                wrapper.empty().append(sorted);
                break;
        }
    };

    function RemoveVideoFromPL(btn)
    {
        var btnId = btn.id;
        var idArr = btnId.split("_");
        var videoTubeId = idArr[1];

        $.ajax({
            type: "POST",
            url: webMethodRemoveVideoFromPublicLibrary,
            cashe: false,
            data: '{"videoTubeId":'  + videoTubeId+ '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#addBoxContent_" + videoTubeId).hide().css("visibility", "hidden");
                //$("#boxContent_" + videoTubeId).hide().css("visibility", "hidden");

             

               
            }
        });
    }