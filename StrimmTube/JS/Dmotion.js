
var dMotionPageIndex=1;
function GetDmotionVideosByKeyword(keyword, pageIndex, channelId, isLong) {
    //console.log("pageindex: "+pageIndex)
    $("#loadingDiv").show();
    var VideoTubePageModel = [];
    var VideoTubeModel = [];
    var params = "";
    //get dailymotion videos per request
    var data;
    if (isLong)
    {
        params = 'https://api.dailymotion.com/videos?fields=allow_embed,description,duration,id,status,thumbnail_url,title,views_total&private=0&longer_than=20&family_filter=true&search=' + keyword + '&page=' + pageIndex + '&limit=50';
    }
    else
    {
        params = params = 'https://api.dailymotion.com/videos?fields=allow_embed,description,duration,id,status,thumbnail_url,title,views_total&private=0&shorter_than=20&family_filter=true&search=' + keyword + '&page=' + pageIndex + '&limit=50';
    }
    $.getJSON(params, function (data) {
       
      
        $.each(data.list, function (i, v) {


              
            var dmotionvideo = new Object();
            dmotionvideo.VideoTubeId = 0;
            dmotionvideo.ProviderVideoId = v.id;
            dmotionvideo.Title = v.title;
            dmotionvideo.Description = v.description;
            dmotionvideo.Duration = v.duration;
            //dmotionvideo.categoryid=
            dmotionvideo.VideoProviderId = 3
            dmotionvideo.IsRRated = false
            dmotionvideo.IsRemovedByProvider = false
            if (v.status == "published") {
                dmotionvideo.IsRestrictedByProvider = false;
            }
            else {
                dmotionvideo.IsRestrictedByProvider = true;

            }

            dmotionvideo.IsInPublicLibrary = false;

            dmotionvideo.IsPrivate = false;

                dmotionvideo.ThumbnailUrl = v.thumbnail_url;
            dmotionvideo.VideoProviderName = "dailymotion";
            //dmotionvideo.categoryname
            dmotionvideo.IsScheduled = false;
            dmotionvideo.UseCounter = 0;
            dmotionvideo.ViewCounter = v.views_total;

            dmotionvideo.DurationString = GetDurationString(v.duration);
            dmotionvideo.IsInChannel = false;
            dmotionvideo.Message = "";
            dmotionvideo.DateAdded = Date.now;
           // console.log(dmotionvideo);
                if (dmotionvideo.Duration > 0) {
            VideoTubeModel.push(dmotionvideo);
            }


        });
        var PageIndex = pageIndex
        var PrevPageIndex = 0;
        var NextPageIndex = 0;
        var PageSize = 50;
        var PageCount = 0;
        var PageToken = 0;
        VideoTubePageModel.push(PageIndex++);
        VideoTubePageModel.push(PrevPageIndex);
        VideoTubePageModel.push(NextPageIndex);
        VideoTubePageModel.push(PageSize);
        VideoTubePageModel.push(PageCount);
        VideoTubePageModel.push(PageToken);
        VideoTubePageModel.push(VideoTubeModel);

        if (VideoTubeModel.length != 0) {
            var videos = Controls.BuildViodeBoxControlForAddVideosPage("by-keyword", VideoTubeModel, true);
            $("#loadMoreVideos").show();
            $(".loadedContent").append(videos);
            
        }
        else {
            $(".loadedContent").html("").html("<span class='spnMsg'>Sorry, there are no search results</span>");
            $("#loadMoreVideos").hide();
        }
        $("#loadingDiv").hide();
       
    });
    //https: //api.dailymotion.com/videos?fields=allow_embed,description,duration,id,status,thumbnail_url,title,views_total,&flags=hd&private=0&shorter_than=20&page=1&limit=100
    //DM.api('/videos', { search: keyword, fields: "allow_embed,description,duration,id,status,thumbnail_url,title,views_total", flags: "hd", private: "0", longer_than: 20, page: pageIndex, limit: 50 }, function (response) {
       
    //    data = response;
    //    $.each(data.list, function (i, v) {


    //        //console.log(v);
    //        var dmotionvideo = new Object();
    //        dmotionvideo.VideoTubeId = 0;
    //        dmotionvideo.ProviderVideoId = v.id;
    //        dmotionvideo.Title = v.title;
    //        dmotionvideo.Description = v.description;
    //        dmotionvideo.Duration = v.duration;
    //        //dmotionvideo.categoryid=
    //        dmotionvideo.VideoProviderId = 3
    //        dmotionvideo.IsRRated = false
    //        dmotionvideo.IsRemovedByProvider = false
    //        if (v.status == "published") {
    //            dmotionvideo.IsRestrictedByProvider = false;
    //        }
    //        else {
    //            dmotionvideo.IsRestrictedByProvider = true;

    //        }

    //        dmotionvideo.IsInPublicLibrary = false;

    //        dmotionvideo.IsPrivate = false;

    //        dmotionvideo.Thumbnail = v.thumbnail_url;
    //        dmotionvideo.VideoProviderName = "dailymotion";
    //        //dmotionvideo.categoryname
    //        dmotionvideo.IsScheduled = false;
    //        dmotionvideo.UseCounter = 0;
    //        dmotionvideo.ViewCounter = v.views_total;

    //        dmotionvideo.DurationString = GetDurationString(v.duration);
    //        dmotionvideo.IsInChannel = false;
    //        dmotionvideo.Message = "";
    //        dmotionvideo.DateAdded = Date.now;
    //       // console.log(dmotionvideo);
    //        if (dmotionvideo.Duration > 0)
    //        {
    //        VideoTubeModel.push(dmotionvideo);
    //        }


    //    });
    //    var PageIndex = pageIndex
    //    var PrevPageIndex = 0;
    //    var NextPageIndex = 0;
    //    var PageSize = 50;
    //    var PageCount = 0;
    //    var PageToken = 0;
    //    VideoTubePageModel.push(PageIndex++);
    //    VideoTubePageModel.push(PrevPageIndex);
    //    VideoTubePageModel.push(NextPageIndex);
    //    VideoTubePageModel.push(PageSize);
    //    VideoTubePageModel.push(PageCount);
    //    VideoTubePageModel.push(PageToken);
    //    VideoTubePageModel.push(VideoTubeModel);

    //    if (VideoTubeModel.length != 0) {
    //        var videos = Controls.BuildViodeBoxControlForAddVideosPage("by-keyword", VideoTubeModel);
    //        $("#loadMoreVideos").show();
    //        $(".loadedContent").append(videos);
            
    //    }
    //    else {
    //        $(".loadedContent").html("").html("<span class='spnMsg'>Sorry, there are no search results</span>");
    //        $("#loadMoreVideos").hide();
    //    }
    //    $("#loadingDiv").hide();
       
    //});
    //

    //  add result to object to return
   
};

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

function getDailyMotionId(url) {
   
 
   
    var ret = [];
    var re = /(?:dailymotion\.com(?:\/video|\/hub)|dai\.ly)\/([0-9a-z]+)(?:[\-_0-9a-zA-Z]+#video=([a-z0-9]+))?/g;
    var m;

    while ((m = re.exec(url)) != null) {

        if (m.index === re.lastIndex) {
            re.lastIndex++;
        }
        //console.log(m);
        ret.push(m[2] ? m[2] : m[1]);
    }
  
    //if ($.isEmptyObject(ret))
    //{
    //    ret = null;
    //}
    return ret;
    
};


function GetDailyMotionByUrl(url) {
   
    var VideoTubeModel = [];
    $("#loadMoreVideos").hide();
   
    if (url.length == 0) {
        $(".loadedContent").html("").html("<span class='spnMsg'>Please enter URL</span>");

    }
    else {
  
        var id = getDailyMotionId(url);
      
        //console.log($.isEmptyObject(id));
        if ($.isEmptyObject(id))
        {
            $(".loadedContent").html("").html("<span class='spnMsg'>Please select correct video provider in step 1</span>");
           
            void 0;
        }
        if (id != null || id != "") {

        var data;
        DM.api('/video/' + id, { fields: "allow_embed,description,duration,id,status,thumbnail_url,title,views_total" }, function (response) {
            if (response.error) {
                $(".loadedContent").html("").html("<span class='spnMsg'>" + response.error.message + "</span>");
            }
            else {
                data = response;
                    //console.log(data);
                var dmotionvideo = new Object();
                dmotionvideo.VideoTubeId = 0;
                dmotionvideo.ProviderVideoId = data.id;
                dmotionvideo.Title = data.title;
                dmotionvideo.Description = data.description;
                dmotionvideo.Duration = data.duration;
                //dmotionvideo.categoryid=
                dmotionvideo.VideoProviderId = 3
                dmotionvideo.IsRRated = false
                dmotionvideo.IsRemovedByProvider = false
                if (data.status == "published") {
                    dmotionvideo.IsRestrictedByProvider = false;
                }
                else {
                    dmotionvideo.IsRestrictedByProvider = true;

                }

                dmotionvideo.IsInPublicLibrary = false;

                dmotionvideo.IsPrivate = false;

                    dmotionvideo.ThumbnailUrl = data.thumbnail_url;
                    //console.log(dmotionvideo.Thumbnail);
                dmotionvideo.VideoProviderName = "dailymotion";
                //dmotionvideo.categoryname
                dmotionvideo.IsScheduled = false;
                dmotionvideo.UseCounter = 0;
                dmotionvideo.ViewCounter = data.views_total;

                dmotionvideo.DurationString = GetDurationString(data.duration);
                dmotionvideo.IsInChannel = false;
                dmotionvideo.Message = "";
                dmotionvideo.DateAdded = Date.now;
                // console.log(dmotionvideo);
                VideoTubeModel.push(dmotionvideo);
                if (VideoTubeModel.length != 0) {
                    $('#divUrlCategory').show();
                    $('#addVideoInstructions').show();

                    var videos = Controls.BuildViodeBoxControlForAddVideosPage("by-url", VideoTubeModel,true);
                    if (clearScreen) {
                        $(".loadedContent").html("");
                    }
                    $(".loadedContent").append(videos);

                }
                else {

                    $(".loadedContent").html("").html("<span class='spnMsg'>Sorry, there are no search results</span>");

                }
            }

        });
        }
        else
        {
            $(".loadedContent").html("").html("<span class='spnMsg'>Please select correct video provider in step 1</span>");
        }

    }

};


//function initDMotionPlayer(element) {
//    iframe = "<iframe src='//www.dailymotion.com/embed/video/" + element.id + "'?PARAMS' html autoplay width='579' height='321' frameborder='0' allowfullscreen></iframe>";
//    $(".playerBoxDMotion .playerDmotion").html("").html(iframe);




//};

function initDMotionPlayer(element)
{
    // PARAMS is a javascript object containing parameters to pass to the player if any (eg: {autoplay: 1})
    var player = DM.player("playerDmotion", { video: element.id, width: "100%", height: "450", params: { autoplay: 1, html:1, allowfullscreen:1 } });

    // 4. We can attach some events on the player (using standard DOM events)
    player.addEventListener("apiready", function (e) {
        e.target.play();
    });
};

function AddDmotionVideoToChannel(providerVideoId)
{
    var dmotionvideo = new Object();
   
    DM.api('/video/' + providerVideoId, { fields: "allow_embed,description,duration,id,status,thumbnail_url,title,views_total" }, function (response) {
        if (response.error&&response!=null) {
            $(".loadedContent").html("").html("<span class='spnMsg'>" + response.error.message + "</span>");
        }
        else {
            data = response;
           
            dmotionvideo.VideoTubeId = 0;
            dmotionvideo.ProviderVideoId = data.id;
            dmotionvideo.Title = data.title;
            dmotionvideo.Description = data.description;
            dmotionvideo.Duration = data.duration;
            //dmotionvideo.categoryid=
            dmotionvideo.VideoProviderId = 3
            dmotionvideo.IsRRated = false
            dmotionvideo.IsRemovedByProvider = false
            if (data.status == "published") {
                dmotionvideo.IsRestrictedByProvider = false;
            }
            else {
                dmotionvideo.IsRestrictedByProvider = true;

            }

            dmotionvideo.IsInPublicLibrary = false;

            dmotionvideo.IsPrivate = false;

            dmotionvideo.Thumbnail = data.thumbnail_url;
            //console.log(dmotionvideo.Thumbnail);
            dmotionvideo.VideoProviderName = "dailymotion";
            //dmotionvideo.categoryname
            dmotionvideo.IsScheduled = false;
            dmotionvideo.UseCounter = 0;
            dmotionvideo.ViewCounter = data.views_total;

            dmotionvideo.DurationString = GetDurationString(data.duration);
            dmotionvideo.IsInChannel = false;
            dmotionvideo.Message = "";
            dmotionvideo.DateAdded = Date.now;
            // console.log(dmotionvideo);
           
          
               
                 $.ajax({
                            type: "POST",
                            url: webMethodAddExternalVideoDmotionToChannel,
                            cashe: false,
                            data: '{"videoTubeModel":' + JSON.stringify(dmotionvideo) + ',"channelTubeId":' + channelId + ',"categoryId":' + categoryId + '}',
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

    });
}