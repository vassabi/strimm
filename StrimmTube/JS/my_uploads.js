// Define some variables used to remember state.
var playlistId, nextPageToken, prevPageToken;

// After the API loads, call a function to get the uploads playlist ID.
function handleAPILoaded() {
    requestUserUploadsPlaylistId();
   // retrieveMyUploads();
}

// Call the Data API to retrieve the playlist ID that uniquely identifies the
// list of videos uploaded to the currently authenticated user's channel.
function requestUserUploadsPlaylistId() {
    // See https://developers.google.com/youtube/v3/docs/channels/list
    var request = gapi.client.youtube.channels.list({
        mine: true,
        part: 'contentDetails'
    });
    request.execute(function (response) {
        //console.log(response)
        playlistId = response.result.items[0].contentDetails.relatedPlaylists.uploads;
        //requestVideoPlaylist(playlistId);
        retrieveMyUploads();
    });
}

// Retrieve the list of videos in the specified playlist.

function requestVideoPlaylist(playlistId, pageToken) {
    $('#video-container').html('');
    var requestOptions = {
        playlistId: playlistId,
        part: 'snippet',
        maxResults: 25
    };
    if (pageToken) {
        requestOptions.pageToken = pageToken;
    }
    var request = gapi.client.youtube.playlistItems.list(requestOptions);
    var videoids=[];
    request.execute(function (response) {
        // Only show pagination buttons if there is a pagination token for the
        // next or previous page of results.
        nextPageToken = response.result.nextPageToken;
        var nextVis = nextPageToken ? 'visible' : 'hidden';
        $('#next-button').css('visibility', nextVis);
        prevPageToken = response.result.prevPageToken
        var prevVis = prevPageToken ? 'visible' : 'hidden';
        $('#prev-button').css('visibility', prevVis);
        
        var playlistItems = response.result.items;
        if (playlistItems) {
           // console.log(playlistItems);

            //$.each(playlistItems, function (index, item) {
            //   // displayResult(item.snippet);
              
            //    var videoId = item.snippet.resourceId.videoId;
            //    videoids.push(videoId);
            BuildControls(playlistItems);

               
            
        } else {
            $('#video-container').html('Sorry, you have no public videos in the selected account.');
        }
    });

   
   
        
   
}
function retrieveMyUploads() {
    $('#video-container').html('');
    var results = YouTube.Channels.list('contentDetails', { mine: true });
    for (var i in results.items) {
        var item = results.items[i];
        // Get the playlist ID, which is nested in contentDetails, as described in the
        // Channel resource: https://developers.google.com/youtube/v3/docs/channels
        var playlistId = item.contentDetails.relatedPlaylists.uploads;

        var nextPageToken = '';

        // This loop retrieves a set of playlist items and checks the nextPageToken in the
        // response to determine whether the list contains additional items. It repeats that process
        // until it has retrieved all of the items in the list.
        while (nextPageToken != null) {
            var playlistResponse = YouTube.PlaylistItems.list('snippet', {
                playlistId: playlistId,
                maxResults: 25,
                pageToken: nextPageToken
            });

            for (var j = 0; j < playlistResponse.items.length; j++) {
                var playlistItem = playlistResponse.items[j];
                //Logger.log('[%s] Title: %s',
                //           playlistItem.snippet.resourceId.videoId,
                //           playlistItem.snippet.title);
                displayResult(playlistItem);

            }
            nextPageToken = playlistResponse.nextPageToken;
        }
    }
}

// Create a listing for a video.
function displayResult(videoSnippet) {
    //console.log(videoSnippet)
    var title = videoSnippet.title;
    var videoId = videoSnippet.resourceId.videoId;
    $('#video-container').append('<p>' + title + ' - ' + videoId + '</p>');
}

// Retrieve the next page of videos in the playlist.
function nextPage() {
    requestVideoPlaylist(playlistId, nextPageToken);
}

// Retrieve the previous page of videos in the playlist.
function previousPage() {
    requestVideoPlaylist(playlistId, prevPageToken);
}
var webMethodGetVideoByKeyWord = "/WebServices/YouTubeWebService.asmx/GetVideosById";
function BuildControls(playlistItems)
{
    var videoIds='';
    //console.log(playlistItems);
    $.each(playlistItems, function (index, item) {
        // displayResult(item.snippet);

        var videoId = item.snippet.resourceId.videoId;
        if (videoIds=='')
        {
            videoIds = item.snippet.resourceId.videoId + ',';
        }
        else
        {
            videoIds += item.snippet.resourceId.videoId+',';
        }
    });
    //console.log(videoIds);
    //return;
    //var playlistItemsJson = JSON.stringify(plalistItems);
  
        countResultIndex = 0;
        countResultIndex = 1;
        var pageToken=null;
        $.ajax({
            type: "POST",
            url: webMethodGetVideoByKeyWord,
            cashe: false,
            data: '{"videoIds":' + "'" + videoIds + "'" + ',"startIndex":' + 1 + ',"pageToken":' + "'" + pageToken + "'" + '}',
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
                //console.log(response);

                if (data.VideoTubeModels.length != 0) {
                    
                    var videos = data.VideoTubeModels;
                    void 0;
                    var pageType = "add-videos";
                    var videoControls = Controls.BuildViodeBoxControlForAddVideosPage("by-keyword", videos); //Controls.BuildVideoRoomControl(pageType, videos, false);
                    $('#video-container').html('');
                    $('#video-container').append(videoControls);
                    
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