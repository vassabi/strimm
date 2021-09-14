var webMethodGetAllArchivedVideos = "/WebServices/VideoRoomService.asmx/GetArchivedVideosByUserId";
var webMethodRemoveFromArchive = "/WebServices/VideoRoomService.asmx/DeleteArchivedVideoByVideotubeIdAndUserId";

function GetWatchItLaterVideos() {
  
    $.ajax({
        type: "POST",
        url: webMethodGetAllArchivedVideos,
        cashe: false,
        data: '{"userId":' + userId + '}',
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
          
            if (response.d.length > 0) {


                $("#lblChannelCount").text("").text(response.d.length);
                var videos = response.d;
                var pageType = "watchitlater";
                var videoControls = Controls.BuildVideoBoxControlForWatchItLaterPage(videos, false);

                $("#videoBoxHolder").html("").html(videoControls);
                //  $(".leftSchedule .nano").nanoScroller({ scroll: 'bottom' });
                // startIndex++;
                $("#loadingDiv").hide();
                //ScrollerUp();

            }
            else {
                $("#lblChannelCount").text("").text("0");
                $("#lblmessage").text("").text("You have no selected videos to watch");
                $("#ancLoadMore").removeAttr("onclick").text("").text("You have no selected videos to watch");
                $("#loadingDiv").hide();
            }


        },
        complete: function (response) {
            $("#loadingDiv").hide();
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
            RemoveOverlay();
        }
    });
    //  e.preventDefault();
};

function addPlayer(videoPath) {
    var stringId = videoPath.id;
    var idArr = stringId.split("_");
    var id = videoPath.id;
    //tag.src = "https://www.youtube.com/iframe_api";
    //var firstScriptTag = document.getElementsByTagName('script')[0];
    //firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
    // //console.log(videoPath.id);
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

        //  //console.log("onYouTubeIframeAPIReady");
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

function RemoveFromArchive(id) {
    if ($("#modalRemove #msgRemove").is(':checked')) {
        $.cookie('dontshowmsgarch', 'yes', { expires: 30 });
    }
   
    $.ajax({
        type: "POST",
        url: webMethodRemoveFromArchive,
        cashe: false,
        data: '{"userId":' + userId + ' ,"videoTubeId":' + id + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d == true)
            {
                $("#boxContent_" + id).hide();
            }
            else
            {
                alertify.error("Removing the selected video from the list failed, please try again later.");
            }
        },
        complete:function()
        {
            CloseModalRemove();
            GetWatchItLaterVideos();
        }
    });

};

function ShowModalRemove(id) {
   
    void 0;
    void 0;
    if (!$.cookie('dontshowmsgarch')) {
        //moved modal stuff in if case
        $('#modalRemove').lightbox_me({
            centered: true,
            onLoad: function () {
                $("#removeOk").attr("onclick", "RemoveFromArchive(" + id + ")");
            },
            onClose: function () {
                RemoveOverlay();
            }
        });
    }
    else {
        RemoveFromArchive(id);
    }

}

function CloseModalRemove() {

    $("#modalRemove").hide();
    $('#modalRemove').trigger('close');
};

function SortWatchLaterVideos(select) {
    var videos = $("#videoBoxHolder").find("div.videoBoxNew");
    var wrapper = $('#videoBoxHolder');

    var value;
    var selectedValue = select.value;
    if (selectedValue !== null && selectedValue !== undefined) {
        value = selectedValue;
    }
    else {
        value = 0;
    }

    SortVideos(videos, value, wrapper);
};

function SortVideos(videos, select, wrapper) {

    switch (select) {
        case "1":
            var sorted = videos.sort(function (a, b) {
                var aDateAdded = ((new Date(b.getAttribute('date-added'))).getTime() * 10000) + 621355968000000000;
                var bDateAdded = ((new Date(a.getAttribute('date-added'))).getTime() * 10000) + 621355968000000000;
                var result = -bDateAdded + aDateAdded;
                void 0;
                return result;
            });
            wrapper.empty().append(sorted);
            break;
       
        case "2":
            var sorted = videos.sort(function (a, b) {
                var aName = b.getAttribute('data-name');
                var bName = a.getAttribute('data-name');
                var result = aName < bName;
                void 0;
                return result;

            });
            wrapper.empty().append(sorted);
            break;
        case "3":
            var sorted = videos.sort(function (a, b) {
                var aName = b.getAttribute('data-name');
                var bName = a.getAttribute('data-name');
                var result = aName > bName;
                void 0;
                return result;

            });
            wrapper.empty().append(sorted);
            break;
    }
};

function ShowSearched() {
    var detachedControls;
    var searchedControls;
    $("#videoBoxHolder").find("div.videoBoxNew").removeAttr("style");
    var inputVal = $("#txtSearchByText").val();
    if (inputVal.length == 0) {
        return;
    }
    else {
       // console.log(searchedControls);
         searchedControls = $("#videoBoxHolder").find("div.videoBoxNew[data-name*='" + inputVal + "']");
        
        
        detachedControls = searchedControls.detach().css("border", "3px solid #fc0").css("box-shadow","1px 2px 3px #999").css("height","181px");
        //console.log(searchedControls);
        $("#videoBoxHolder").prepend(detachedControls);
    }




}

function  ClearSearchResult()
{
    $("#videoBoxHolder").find("div.videoBoxNew").removeAttr("style");
    $("#txtSearchByText").val('');
}
