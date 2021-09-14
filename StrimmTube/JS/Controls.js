
var channelBoxIdCount = 0;


var Controls = {

    init: function () {

    },

    BuildCalendarVideoScheduleControl: function (channelScheduleId) {
        //ajax get collection of videos by channelscheduleId

    },

    BuildVideoBoxControlForVideoRoomPage: function (videos, userId) {
     
        var final = "";

        $.each(videos, function (i, d) {
            var videoId = d.VideoTubeId;
            var title = d.Title;
            var description = d.Description;
            var viewCounter = d.ViewCounter;
            var durationInSec = d.Duration;
            var categoryName = d.CategoryName;
            var isScheduled = d.IsScheduled;
            var isPrivate = d.IsPrivate;
            var isRRated = d.IsRRated;
            var isRestricted = d.IsRestrictedByProvider;
            var isRemoved = d.IsRemovedByProvider;
            var providerVideoId = d.ProviderVideoId;
            var thumbnail = d.ThumbnailUrl;
            var durationString = d.DurationString;
            var isInChannel = d.IsInChannel;
            var allowDelete = true;
            var dateAdded = d.DateAdded;
            var isExternalVideo = videoId == 0;
            var providerName = d.VideoProviderName;
            var providerImgSrc;
            var ownerUserId = d.OwnerUserId;
            var ownerUserName = d.OwnerUserName;
            var originDomain = d.OriginDomain;
            //console.log(isRemoved)

            if (providerName != undefined && providerName != null) {
                providerName = providerName.toLowerCase();
            }

            if (providerName == "vimeo") {
                providerImgSrc = "/images/vimeo-icon.png";
            }
            else if (providerName == "youtube") {
                providerImgSrc = "/images/youTube-icon.png";
            }

            final += '<div class="videoBoxNew vrVideoBox" data-provider="' + providerName + '" data-duration="' + durationInSec + '" data-views="' + viewCounter + '" data-videoadd="1" id="boxContent_' + videoId + '" date-added="' + dateAdded + '">';
            final += '  <div class="vrVideoThumbHolder">';
            final += '      <div class="vrActionsVideoBoxHolder" id="action_' + videoId + '">';
            
            if (isRemoved) {
                final += "<div class='videoRemoveMessage'>removed by provider</div>";
            }
            else if (isRestricted) {
                final += "<div class='videoRemoveMessage'>restricted by provider</div>";
            }
            else {
                if (providerName == "vimeo") {
                    final += "<div class='vrVideoBoxPlay' id='player_" + videoId + "' data-video-id='" + providerVideoId + "' onclick=\"videoRoom.showVideo(this,'vimeo')\"></div>";
                }
                else if (providerName == "youtube") {
                    final += "<div class='vrVideoBoxPlay' id='player_" + videoId + "' data-video-id='" + providerVideoId + "' onclick=\"videoRoom.showVideo(this,'youtube')\"></div>";
                }

                else {
                    final += "<div class='vrVideoBoxPlay' id='player_" + videoId + "' data-video-id='" + FormatUrl(originDomain, d.VideoKey, true) + "' onclick=\"videoRoom.showVideo(this,'strimm')\"></div>";
                    if (isPrivate) {
                        final += "<div class='videoPrivateMessage'>private</div>";
                    }
                }
            }

            final += '          <div class="vrVideoBoxRemove" id="remove_' + videoId + '" onclick="videoRoom.deleteVideo(this)"></div>';
            final += '      </div>';
            final += '      <div class="addButtonContainer" style="background-image: url(' + thumbnail + ');">';

            if (providerName != "vimeo" && providerName != "youtube") {
                if (userId == ownerUserId) {
                    final += '          <div class="addButtonHolder" id="edit_' + videoId + '" onclick="videoRoom.editVideo(this)"></div>';
                }
                else {
                    final += '          <div class="addButtonHolder" id="edit_' + videoId + '" onclick="videoRoom.addVideoToAnotherVideoRoom(this)"></div>';
                }
            }

            final += '      </div>';
            final += '  </div>';
            final += '  <div class="videoBoxInfo">';
            final += '      <div class="vrVideoTitleHolder">';
            final += '          <div class="vrVideoTitle" title="' + title + '">' + title + '</div>';
            final += '      </div>';
            //final += '      <div class="vrVideoDescriptionHolder">';
            //final += '          <div class="vrVideoInfoLabel">Description</div>';
            //final += '          <div class="vrVideoContent descriptionContent blockEllipsis" title="' + description + '">' + description + '</div>';
            //final += '      </div>';
            final += '      <div class="vrVideoIDHolder">';
            final += '          <div class="vrVideoInfoLabel">ID</div>';
            final += '          <div class="vrVideoContent" title="bgf53879">bgf53879</div>';
            final += '      </div>';

            if (userId != ownerUserId && ownerUserId > 0) {
                final += '      <div class="vrVideoOwnerCounterHolder">';
                final += '          <div class="vrVideoInfoLabel">Created By</div>';
                final += '          <div class="vrVideoContent" title="Video was created by ' + ownerUserName + '">' + ownerUserName + '</div>';
                final += '      </div>';
            }

            final += '      <div class="vrVideoDurationHolder">';
            final += '          <div class="vrVideoInfoLabel">Duration</div>';
            final += '          <div class="vrVideoContent" title="Video duration is ' + durationString + '">' + durationString + '</div>';
            final += '      </div>';
            final += '      <div class="vrVideoCategoryHolder">';
            final += '          <div class="vrVideoInfoLabel">Category</div>';
            final += '          <div class="vrVideoContent" title="Category is ' + categoryName + '">' + categoryName + '</div>';
            final += '      </div>';
            final += '      <div class="vrVideoViewsHolder">';
            final += '          <div class="vrVideoInfoLabel">Views</div>';
            final += '          <div class="vrVideoContent" title="Video was viewed ' + viewCounter + ' times">' + viewCounter + '</div>';
            final += '      </div>';

            if (ownerUserId > 0) {
                final += '      <div class="vrVideoLicenseCounterHolder">';
                final += '          <div class="vrVideoInfoLabel">Licensed Users</div>';
                final += '          <div class="vrVideoContent" title="Video was licensed by 10 users">10</div>';
                final += '      </div>';
            }

            final += '      <div class="vrVideoAddedDateHolder">';
            final += '          <div class="vrVideoInfoLabel">Added</div>';
            final += '          <div class="vrVideoContent" title="Video was added on ' + dateAdded + '">' + dateAdded + '</div>';
            final += '      </div>';
            final += '  </div>';

            if (ownerUserId > 0) {
                final += '<div>';
                final += '  <div class="vrVideoRentalHolder">';
                final += '      <div class="vrVideoInfoLabel"><strong>Rental</strong></div>';
                final += '      <div class="vrVideoContent" title="Video rental fee is 2 c/v/h (credits per view per hour)><strong>2 c/v/h</strong></div>';
                final += '  </div>';
                final += '  <div class="providerLogo">';
                final += '  </div>';
                final += '</div>';
            }
            else {
                final += '<div>';
                final += '  <div class="vrVideoRentalHolder">';
                final += '  </div>';
                final += '  <div class="providerLogo">';
                final += '      <img src="' + providerImgSrc + '">';
                final += '  </div>';
                final += '</div>';
            }

            final += '</div>';
            final += '</div>';
        });

        return final;
    },

    BuildVideoBoxControlForVideoStorePage: function (videos, userId) {
        var final = "<ul class='gridder'>";

        $.each(videos, function (i, d) {
            var videoId = d.VideoTubeId;
            var title = d.Title;
            var description = d.Description;
            var viewCounter = d.ViewCounter;
            var durationInSec = d.Duration;
            var categoryName = d.CategoryName;
            var isScheduled = d.IsScheduled;
            var isPrivate = d.IsPrivate;
            var isRRated = d.IsRRated;
            var isRestricted = d.IsRestrictedByProvider;
            var isRemoved = d.IsRemovedByProvider;
            var providerVideoId = d.ProviderVideoId;
            var thumbnail = d.ThumbnailUrl;
            var durationString = d.DurationString;
            var isInChannel = d.IsInChannel;
            var allowDelete = true;
            var dateAdded = d.DateAdded;
            var isExternalVideo = videoId == 0;
            var providerName = d.VideoProviderName;
            var providerImgSrc;
            var ownerUserId = d.OwnerUserId;
            var ownerUserName = d.OwnerUserName;
            var ownerPublicUrl = d.OwnerPublicUrl;
            var previewClip = d.VideoPreviewKey;
            var originDomain = d.OriginDomain;

            if (providerName != undefined && providerName != null) {
                providerName = providerName.toLowerCase();
            }


            if (thumbnail == null || thumbnail == '') {
                thumbnail = "/images/comingSoonBG.jpg";
            }

            final += '<li class="gridder-list" data-videoid="'+videoId+'" data-owneruserid="'+ownerUserId+'" data-griddercontent="#gridder-content-'+videoId+'">'
            final += '<div class="videoBoxNew vsVideoBox" data-provider="' + providerName + '" data-duration="' + durationInSec + '" data-views="' + viewCounter + '" data-videoadd="1" id="boxContent_' + videoId + '" date-added="' + dateAdded + '">';
            final += '  <div class="vsVideoThumbHolder">';
            
            final += '      <div class="vsActionsVideoBoxHolder" id="action_' + videoId + '">';
          
            if (previewClip != null && previewClip != '') {
                final += "          <div class='vsVideoBoxPlay' id='player_" + videoId + "' data-video-id='" + FormatUrl(originDomain, previewClip, true) + "' onclick=\"videoStore.showVideo(this)\"></div>";
            }

            final += '      </div>';
            final += '      <div class="addButtonContainer" style="background-image: url(' + thumbnail + ');">';

            if (userId != ownerUserId) {
                final += '          <div class="addButtonHolder" id="edit_' + videoId + '" onclick="videoRoom.licenseVideo(this)"></div>';
            }
            final += '  </div>';
            final += '      </div>';
            final += '  <div class="videoBoxInfo">';
            final += '      <div class="vsVideoTitleHolder">';
            final += '          <div class="vsVideoTitle" title="' + title + '">' + title + '</div>';
            final += '      </div>';
            final += '      <div class="vvVideoDurationHolder">';
            final += '          <div class="vsVideoInfoLabel">Duration</div>';
            final += '          <div class="vsVideoContent" title="Video duration is ' + durationString + '">' + durationString + '</div>';
            final += '      </div>';
            final += '      <div class="vsVideoCategoryHolder">';
            final += '          <div class="vsVideoInfoLabel">Category</div>';
            final += '          <div class="vsVideoContent" title="Category is ' + categoryName + '">' + categoryName + '</div>';
            final += '      </div>';
            
           

            //final += '      <div class="vsVideoDescriptionHolder">';
            //final += '          <div class="vsVideoInfoLabel">Description</div>';
            //final += '          <div class="vsVideoContent descriptionContent blockEllipsis" title="' + description + '">' + description + '</div>';
            //final += '      </div>';
            //final += '      <div class="vsVideoOwnerHolder">';
            //final += '          <div class="vsVideoOwnerInfoLabel">Owner</div>';
            //final += '          <div class="vsVideoOwnerContent" title="Video was created by ' + ownerUserName + '"><a href="/' + ownerPublicUrl + '/video-room">' + ownerUserName + '</a></div>';
            //final += '      </div>';
            //final += '      <div class="vrVideoIDHolder">';
            //final += '          <div class="vsVideoInfoLabel">ID</div>';
            //final += '          <div class="vsVideoContent" title="bgf53879">bgf53879</div>';
            //final += '      </div>';

            //if (userId != ownerUserId && ownerUserId > 0) {
            //    final += '      <div class="vrVideoOwnerCounterHolder">';
            //    final += '          <div class="vsVideoInfoLabel">Created By</div>';
            //    final += '          <div class="vsVideoContent" title="Video was created by ' + ownerUserName + '">' + ownerUserName + '</div>';
            //    final += '      </div>';
            //}


            //final += '      <div class="vsVideoViewsHolder">';
            //final += '          <div class="vsVideoInfoLabel">Views</div>';
            //final += '          <div class="vsVideoContent" title="Video was viewed ' + viewCounter + ' times">' + viewCounter + '</div>';
            //final += '      </div>';

            //if (ownerUserId > 0) {
            //    final += '      <div class="vsVideoLicenseCounterHolder">';
            //    final += '          <div class="vsVideoInfoLabel">Licensed Users</div>';
            //    final += '          <div class="vsVideoContent" title="Video was licensed by 10 users">10</div>';
            //    final += '      </div>';
            //}

            //final += '      <div class="vsVideoAddedDateHolder">';
            //final += '          <div class="vsVideoInfoLabel">Added</div>';
            //final += '          <div class="vsVideoContent" title="Video was added on ' + dateAdded + '">' + dateAdded + '</div>';
            //final += '      </div>';
            //final += '  </div>';

            //if (ownerUserId > 0) {
            //    final += '<div>';
            //    final += '  <div class="vsVideoRentalHolder">';
            //    final += '      <div class="vsVideoInfoLabel"><strong>Rental</strong></div>';
            //    final += '      <div class="vsVideoContent" title="Video rental fee is 2 c/v/h (credits per view per hour)><strong>2 c/v/h</strong></div>';
            //    final += '  </div>';
            //    final += '  <div class="providerLogo">';
            //    final += '  </div>';
            //    final += '</div>';
            //}
            //else {
            //    final += '<div>';
            //    final += '  <div class="vsVideoRentalHolder">';
            //    final += '  </div>';
            //    final += '  <div class="providerLogo">';
            //    final += '      <img src="' + providerImgSrc + '">';
            //    final += '  </div>';
            //    final += '</div>';
            //}

            final += '</div>';
            final += '</div>';
            final += '</li>';
        });
        final+="</ul>"
        return final;
    },

    BuildVideoExpandedContentControlForVideoStorePage:function(videos, videoCount)
    {
        var selectedVideo = videos.d[0];
        ////console.log(videos.d.length)
        var videosFromSameCreator = videos.d.slice(1, 5);
        var sameCreatorVideos = "";
        $.each(videosFromSameCreator, function (i, d) {
            var videoId = d.VideoTubeId;
            var title = d.Title;
            var description = d.Description;
            var viewCounter = d.ViewCounter;
            var durationInSec = d.Duration;
            var categoryName = d.CategoryName;
            var isScheduled = d.IsScheduled;
            var isPrivate = d.IsPrivate;
            var isRRated = d.IsRRated;
            var isRestricted = d.IsRestrictedByProvider;
            var isRemoved = d.IsRemovedByProvider;
            var providerVideoId = d.ProviderVideoId;
            var thumbnail = d.ThumbnailUrl;
            var durationString = d.DurationString;
            var isInChannel = d.IsInChannel;
            var allowDelete = true;
            var dateAdded = d.DateAdded;
            var isExternalVideo = videoId == 0;
            var providerName = d.VideoProviderName;
            var providerImgSrc;
            var ownerUserId = d.OwnerUserId;
            var ownerUserName = d.OwnerUserName;
            var ownerPublicUrl = d.OwnerPublicUrl;
            var previewClip = d.VideoPreviewKey;
            var originDomain = d.OriginDomain;

       
            if (providerName != undefined && providerName != null) {
                providerName = providerName.toLowerCase();
            }
           

            if (thumbnail == null || thumbnail == '') {
                thumbnail = "/images/comingSoonBG.jpg";
            }

           
            sameCreatorVideos += '<div class="videoBoxNew expandedBox" data-provider="' + providerName + '" data-duration="' + durationInSec + '" data-views="' + viewCounter + '" data-videoadd="1" id="boxContent_' + videoId + '" date-added="' + dateAdded + '">';
            sameCreatorVideos += '  <div class="vsVideoThumbHolder">';

            //sameCreatorVideos += '      <div class="expActionsVideoBoxHolder" id="action_' + videoId + '">';

           
            //sameCreatorVideos += '      </div>';
            sameCreatorVideos += '      <div class="addButtonContainer" style="background-image: url(' + thumbnail + ');">';

           

            sameCreatorVideos += '      </div>';
            sameCreatorVideos += '  </div>';
            sameCreatorVideos += '  <div class="videoBoxInfo">';
            sameCreatorVideos += '      <div class="vsVideoTitleHolder">';
            sameCreatorVideos += '          <div class="vsVideoTitle" title="' + title + '">' + title + '</div>';
            sameCreatorVideos += '      </div>';
          
            sameCreatorVideos += '      <div class="vvVideoDurationHolder">';
            sameCreatorVideos += '          <div class="vsVideoInfoLabel">Duration</div>';
            sameCreatorVideos += '          <div class="vsVideoContent" title="Video duration is ' + durationString + '">' + durationString + '</div>';
            sameCreatorVideos += '      </div>';
            sameCreatorVideos += '      <div class="vsVideoCategoryHolder">';
            sameCreatorVideos += '          <div class="vsVideoInfoLabel">Category</div>';
            sameCreatorVideos += '          <div class="vsVideoContent" title="Category is ' + categoryName + '">' + categoryName + '</div>';
            sameCreatorVideos += '      </div>';
           

            sameCreatorVideos += '</div>';
            sameCreatorVideos += '</div>';
        });
     
        var final="";
       
        var videoId = selectedVideo.VideoTubeId;
        var title = selectedVideo.Title;
        var description = selectedVideo.Description;
        var viewCounter = selectedVideo.ViewCounter;
        var durationInSec = selectedVideo.Duration;
        var categoryName = selectedVideo.CategoryName;
        var isScheduled = selectedVideo.IsScheduled;
        var isPrivate = selectedVideo.IsPrivate;
        var isRRated = selectedVideo.IsRRated;
        var isRestricted = selectedVideo.IsRestrictedByProvider;
        var isRemoved = selectedVideo.IsRemovedByProvider;
        var providerVideoId = selectedVideo.ProviderVideoId;
        var thumbnail = selectedVideo.ThumbnailUrl;
        var durationString = selectedVideo.DurationString;
        var isInChannel = selectedVideo.IsInChannel;
        var allowDelete = true;
        var dateAdded = selectedVideo.DateAdded;
        var isExternalVideo = videoId == 0;
        var providerName = selectedVideo.VideoProviderName;
        var providerImgSrc;
        var ownerUserId = selectedVideo.OwnerUserId;
        var ownerUserName = selectedVideo.OwnerUserName;
        var ownerPublicUrl = selectedVideo.OwnerPublicUrl;
        var previewClip = selectedVideo.VideoPreviewKey;
        var originDomain = selectedVideo.OriginDomain;
        var useCounter = selectedVideo.UseCounter;
        var keywords = selectedVideo.Keywords;
        var dateObj = $.datepicker.formatDate('MM dd, yy', new Date(dateAdded));

        if (thumbnail == null || thumbnail == '') {
            thumbnail = "/images/comingSoonBG.jpg";
        }
        //console.log(ownerUserName+","+ownerPublicUrl)
        final+='<div class="expandPanelContentWrapper">';
        final += '<div class="selectedVideoThumbWrapper">';
        final += '      <div class="expActionsVideoBoxHolder" id="action_' + videoId + '">';

        if (previewClip != null && previewClip != '') {
            final += "          <div class='expVideoBoxPlay' id='player_" + videoId + "' data-video-id='" + FormatUrl(originDomain, previewClip, true) + "' onclick=\"videoStore.showVideo(this)\"></div>";
        }
        final += '<div class="addButtonContainer" style="background-image: url(' + thumbnail + ');">';
        final += '</div>';
        final += '</div>';
    
        final+='<div class="videoPopularityHolder">';
        final+='<span class="spnViews">'+viewCounter+'</span>';
        final+='<span class="spnRating">3.75</span>';
        final+='<span class="spnDownloads">'+useCounter+'</span>';
        final+='</div>';
        final+='<div class="licenseHolder">';
        final+='<span>LICENSE @ 1 credit/view/hour</span>';
        final+='</div>';
        final+='</div>';
        final+='<div class="infoListHolder">';
        final+='<ul>';
        final+='<li>';
        final+='<span class=vsVideoTitleExpView>video title</span>';
        final+='<span class="videoTitle">'+title+'</span>';
        final+='</li>';
        final+='<li>';
        final+='<span>description</span>';
        final+='<span class="videoDescription">'+description+'</span>';
        final+='</li>';
        final+='<li>';
        final+='<span>video Id</span>';
        final+='<span class="videoId">ST2345</span>';
        final+='</li>';
        final+='<li>';
        final+='<span>creator</span>';
        final+='<a class="expVideoRoomUrl" href="/'+ownerPublicUrl+'/video-room">';
        final+='<span class="videoCreator">'+ownerUserName+'"</span>';
        final+='</a>';
        final+='</li>';
        final+='<li>';
        final+='<span>language</span>';
        final+='<span class="videoLanguage">english</span>';
        final+='</li>';
        final+='<li>';
        final+='<span>keywords</span>';
        final+='<span class="videoKeywords">'+keywords+'</span>';
        final+='</li>';
        final+='<li>';
        final+='<span>Category</span>';
        final+='<span class="videoCategory">'+categoryName+'</span>';
        final+='</li>';
        final+='<li>';
        final+='<span>duration</span>';
        final+='<span class="videoDuration">'+durationString+'</span>';
        final+='</li>';
        final+='<li>';
        final+='<span>upload</span>';
        final += '<span class="videoUpload">' + dateObj + '</span>';
        final+='</li>';
        final+='<li>';
        final+='<span>quality</span>';
        final+='<a class="videoQuality380">380</a>';
        final+='<a class="videoQuality480">480</a>';
        final+='<a class="videoQuality720">720</a>';
        final+='</li>';
        final+='</ul>';
        final+='</div>';
        if (isRRated)
        {
            final += '<div class="rRated">RRated';

            final += '</div>';
        }        
        final+='<div class="sameCreatorVideosHolder">';
        final+='<span class="spnSameCreator">by same creator</span>';
        final+='<div class="sameCreatorVideosContent">';
        final += sameCreatorVideos;
        final+='</div>';
        final += '<a  href="/' + ownerPublicUrl + '/video-room">more (' + videoCount + ')</a>';
        final+='</div>';

        final += '</div>';

        return final;
    },

    

    BuildVideoBoxControlForSchedulePage: function (videos, justAdded) {
        var final = "";
        //console.log(videos);
        $.each(videos, function (i, d) {
         
            var videoId = d.VideoTubeId;
            var title = d.Title;
            var descripton = d.Description;
            var viewCounter = d.ViewCounter;
            var durationInSec = d.Duration;
            var categoryName = d.CategoryName;
            var isScheduled = d.IsScheduled;
            var isPrivate = d.IsPrivate;
            var privateVideoModeEnabled = d.PrivateVideoModeEnabled;
            var isRRated = d.IsRRated;
            var isRestricted = d.IsRestrictedByProvider;
            var isRemoved = d.IsRemovedByProvider;
            var providerVideoId = d.ProviderVideoId;
            var thumbnail=d.Thumbnail;
            debugger;

            
            var durationString = d.DurationString;
            var isInChannel = d.IsInChannel;
            var allowDelete = true;
            var dateAdded = d.DateAdded;
            var isExternalVideo = videoId == 0;
            var providerName;
           
            
            if (d.VideoProviderName != undefined & d.VideoProviderName != null)
            {
                providerName = d.VideoProviderName.toLowerCase();
            }
           
            var providerImgSrc;
            var matureContentAllowed = d.IsRRated;
            var videoProviderId = d.VideoProviderId;
            var isLiveVideo = d.IsLive;
            var liveStartTime = d.LiveStartTime;
            var startDateinUtc;
            var startDateLocal;
            var startDateUI;
            void 0
            if (liveStartTime)
            {
                var tz = jstz.determine();
               
                var date = new Date(liveStartTime);

                startDateUI = moment.tz(date.toString('YYYY-MM-dd  HH:mm a'), 'Etc/GMT');

                startDateLocal = startDateUI.clone().tz(tz.name());
            }
            
          
           
            if (thumbnail == null || thumbnail == "") {
                thumbnail = "/images/comingSoonBG.jpg";
            }

            var originDomain = d.OriginDomain;

            if (providerName == "vimeo") {
                providerImgSrc = "/images/vimeo-icon.png";
            }
            else if (providerName == "dailymotion") {
                providerImgSrc = "/images/dMotion.png";
            }
            else if (providerName == "youtube") {
                providerImgSrc = "/images/youTube-icon.png";
            }
            if (isLiveVideo)
            {
                final += "<div class='videoBoxNew'  data-islive='" + isLiveVideo + "'  data-maturecontent='" + matureContentAllowed + "' data-provider='" + providerName + "' data-duration='" + durationInSec + "' data-views='" + viewCounter + "' data-videoadd='1' id='boxContentLive_" + videoId + "' date-added='" + dateAdded + "' onclick='ShowReactPlayer(this)'>";
            }
            else
            {
                final += "<div class='videoBoxNew' data-isprivate='" + isPrivate + "' data-maturecontent='" + matureContentAllowed + "' data-provider='" + providerName + "' data-duration='" + durationInSec + "' data-views='" + viewCounter + "' data-videoadd='1' id='boxContent_" + videoId + "' date-added='" + dateAdded + "' onclick='ShowReactPlayer(this)'>";
            }
            final += "<div  class='divProviderLogo'>";
            if (isLiveVideo) {
                final += "<span class='liveMark'>Live</span>"
            }
            
            if (providerImgSrc) {
                final += "<img src='" + providerImgSrc + "'/>";
            }
            if (isRRated)
            {
                final+="<a class='rRatedTag'></a>";
            }
            if (isPrivate)
            {
                final += "<a class='isPrivateTag'></a>";
            }

            final += "</div>";
            final += "<div class='actionsVideoBoxHolder' id='action_" + videoId + "'>";
           
            if (isRemoved) {
                final += "<div class='videoRemoveMessage' id='restricted_" + videoId + "'>removed by provider</div>";
            }
            else if (isRestricted) {
                final += "<div class='videoRemoveMessage' id='restricted_" + videoId + "'>restricted by provider</div>";
            }
            
            else {
                if ((providerName == "vimeo") && (!isPrivate)) {
                    final += "<div class='VideoBoxPlay' data-boxContentId='" + videoId + "' id='" + providerVideoId + "'></div>";
                }
                else if ((providerName == "youtube") && (!isPrivate)) {
                    {
                        if(isLiveVideo)
                        {
                            final += "<div class='VideoBoxPlay isLive'  data-boxContentId='" + videoId + "' id='" + providerVideoId + "'></div>";
                        }
                        else
                        {
                            final += "<div class='VideoBoxPlay'  data-boxContentId='" + videoId + "' id='" + providerVideoId + "'></div>";
                        }
                    }
                   
                }
                else if ((providerName == "dailymotion") && (!isPrivate)) {
                    final += "<div class='VideoBoxPlay' data-boxContentId='" + videoId + "' id='" + providerVideoId + "'></div>";
                }
                else if (isPrivate)
                {
                    final += "<div class='VideoBoxPlay' data-providerName='"+providerName+"' data-url='"+providerVideoId+"' id='private_" + videoId + "'></div>";
                }
                else {
                    final += "<div class='VideoBoxPlay' id='" + providerVideoId + "' onclick='ShowStrimmPlayer(this)'></div>";
                }
               
                if (justAdded) {
                    final += "<span class='recentlyadded'>just in</span>";
                }
            }
            if (isLiveVideo)
            {
                var now = new Date();
                var offset = now.getTimezoneOffset();
                startDateLocal = startDateLocal.add(-offset / 60, 'hours');
                final+="<div class='isLIve infoHolder'>";
                final += "<span class='spnLiveTime'>" + startDateLocal.format('MMMM DD YYYY, HH:mm a') + "</span>";
                final += "</div>";
            }
            //MAX: 5/11/15 - This should really be "in Schedule" message
            //final += "<span id='recentlyadded_"+videoId+"' class='recentlyadded' style='display:none;'>just in</span>";
            if (isLiveVideo)
            {
                final += "<div class='VideoBoxRemove isLive' id='removeLive_" + videoId + "' onclick='RemoveLiveVideoFromChannel(this)'></div>" +
                    "</div>";
            }
            else
            {
                final += "<div class='VideoBoxRemove' id='remove_" + videoId + "' onclick='RemoveVideoFromChannel(this)'></div>" +
                    "</div>";
            }
                
            
            
            final += "<div class='videoBoxOverlay'>";
            final += "</div>";
            
            final += "<div class='addButtonContainer' style='background-image: url(" + thumbnail + ");'>";
            if (!isRemoved && !isRestricted) {
                if (isLiveVideo)
                {
                    final += "<div class='addButtonHolder isLive' id='addId_" + videoId + "' onmouseover='ShowLiveMessage(this)' onmouseout='HideLiveMessage(this)' >"
                    final+="<span class='spanLiveMessage'>This live event has been scheduled already. It will appear in “Live” area of your channel</span>"
                        final+="</div>";
                }
                else
                {
                    final += "<div class='addButtonHolder' id='addId_" + videoId + "' onclick='addVideoScedule(this)' ></div>";
                }
               
            }

            if (descripton && descripton != "") {
                descripton = Controls.EscapeHtml(descripton);
            }
            if (videoProviderId == 4||isPrivate==true) {
                final += "<div class='videoSettings material-icons' onclick='showVideoSettingsPopup(" + videoId + ")'>settings</div>"
            }
            final += "</div>" +
                    "<div class='videoBoxInfo'>" +
                        "<span class='videoBoxtitle ' title='" + title + "'>" + title + "</span>" +
                        "<span class='videoBoxviews'>views:<strong>" + viewCounter + "</strong></span>" +
                        "<span class='videoBoxduration'><strong>" + durationString + "</strong></span>" +
                        "<input type='hidden' value='" + durationInSec + "' class='durationHidden'/>" +
                        "<input type='hidden' value='" + descripton + "' class='descriptionHidden'/>" +
                        "<span class='videoBoxcateg'><strong>" + categoryName + "</strong></span>" +
                        "<input type='hidden' value='" + videoId + "' class ='hidden'/>" +
                    "</div>" +
                "</div>";
        });

        return final;
    },

    BuildViodeBoxControlForAddVideosPage: function (source, videos, isSearch) {
        var final = "";

        $.each(videos, function (i, d) {
            if (d != null) {
                void 0;
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
                var thumnbnailUrlFromPRovider = d.ThumbnailUrl;
                var durationString = d.DurationString;
                var isInChannel = d.IsInChannel;
                var allowDelete = true;
                var dateAdded = d.DateAdded;
                var isExternalVideo = videoId == 0;
                var providerName = d.VideoProviderName;
                var originDomain = d.OriginDomain;
                var isLiveVideo = d.IsLive;
                var liveStartTime = d.LiveStartTime;
                var liveEndTime = d.LiveEndTime;
                var videoProviderId = d.VideoProviderId;
             

                if (providerName != undefined && providerName != null) {
                    providerName = providerName.toLowerCase();
                }

                //console.log("isLIveVideo: " + isLiveVideo);
               
                var providerImgSrc;
              
                if (isSearch)
                {
                    thumbnail = thumnbnailUrlFromPRovider;
                }
                else
                {
                    if (thumbnail == null || thumbnail == "") {
                        thumbnail = "/images/comingSoonBG.jpg";
                    }
                }
               

                if (providerName == "vimeo") {
                    providerImgSrc = "/images/vimeo-icon.png";
                }
                else if (providerName == "dailymotion") {
                    providerImgSrc = "/images/dMotion.png";
                }
                else if (providerName == "youtube") {
                    providerImgSrc = "/images/youTube-icon.png";
                }

                if (!thumbnail) {
                    thumbnail = providerImgSrc;
                }
                if (isLiveVideo) {
                    final += "<div class='videoBoxNew' id='boxContentLive_" + videoId + "' data-videoTubeId='" + videoId + "' data-isLiveVideo='" + isLiveVideo + "'  data-provider='" + providerName + "' data-duration='" + durationInSec + "' data-views='" + viewCounter + "' data-maturecontent='" + isRRated;
                }
                else {
                    final += "<div class='videoBoxNew' data-videoTubeId='" + videoId + "' data-isLiveVide='" + isLiveVideo + "'  data-provider='" + providerName + "' data-duration='" + durationInSec + "' data-views='" + viewCounter + "' data-maturecontent='" + isRRated;
                }
               

                if (isExternalVideo) {
                    videoId = channelBoxIdCount;
                    channelBoxIdCount++;

                    final += "' data-videoadd='0' id='addBoxContent_" + providerVideoId;
                }
                else {
                    if (isInChannel) {
                        final += "' data-videoadd='1' id='addBoxContent_" + providerVideoId;
                    }
                    else {
                        final += "' data-videoadd='0' id='addBoxContent_" + providerVideoId;
                    }
                }

                final += "' date-added='" + dateAdded + "'>";

                if (providerImgSrc) {
                    final += "<div  class='divProviderLogo'>";
                    if (isLiveVideo) {
                        final += "<span class='liveMark'>Live</span>"
                    }
                    final += "<img src='" + providerImgSrc + "'/>";
                    if (isSearch == false)
                    {
                        if (isRRated) {
                            final += "<a class='rRatedTag'></a>";
                        }
                        if (isPrivate) {
                            final += "<a class='isPrivateTag'></a>";
                        }
                    }
                    

                        final+="</div>";
                }
                else
                {
                    final += "<div  class='divProviderLogo'>";
                    if (isLiveVideo) {
                        final += "<span class='liveMark'>Live</span>"
                    }
                    if (isSearch==false)
                    {
                        if (isRRated) {
                            final += "<a class='rRatedTag'></a>";
                        }
                        if (isPrivate) {
                            final += "<a class='isPrivateTag'></a>";
                        }
                    }
                   

                    final += "</div>";
                }
              
                final += "<div class='actionsVideoBoxHolder' id='action_" + videoId + "'>";


                if (isRemoved) {
                    final += "<div class='videoRemoveMessage'>removed by provider</div>";
                }
                else if (isRestricted) {
                    final += "<div class='videoRemoveMessage'>restricted by provider</div>";
                }
                else {
                    if (providerName == "vimeo") {
                        final += "<div class='VideoBoxPlay' id='" + providerVideoId + "' onclick='showPlayerVimeo(this)'></div>";
                    }
                    else if (providerName == "youtube") {
                        if (isLiveVideo) {
                            final += "<div class='VideoBoxPlay isLive'  id='" + providerVideoId + "' onclick='showPlayer(this)'></div>";
                        }
                        else {
                            final += "<div class='VideoBoxPlay' id='" + providerVideoId + "' onclick='showPlayer(this)'></div>";
                        }
                       
                    }
                    else if (providerName == "dailymotion") {
                        final += "<div class='VideoBoxPlay' data-boxContentId='" + videoId + "' id='" + providerVideoId + "' onclick='showPlayerDmotion(this)'></div>";
                    }
                    else {
                        final += "<div class='VideoBoxPlay' id='" + providerVideoId + "' onclick='ShowStrimmPlayer(\"" + originDomain + d.VideoKey + "\")'></div>";
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

                if (source == "video-room") {
                    final += "<a id='remove_" + videoId + "' class='VideoBoxRemove' onclick='RemoveVideoFromVideoRoom(this)'></a>";
                }

                final += "</div>";
                final += "<div class='videoBoxOverlay'>";
                final += "</div>";
                //console.log(isRestricted+", "+isRemoved)
                if (!isRemoved && !isRestricted) {
                    if (isExternalVideo) {
                        final += "<div class='addButtonContainer' id='addButtonHolderId_" + providerVideoId + "' style='background-image: url(" + thumbnail + ");'>";
                        if (!isInChannel) {
                            if (isLiveVideo)
                            {
                                final += "<div class='addButtonHolder' id='addvideosId_" + providerVideoId + "' onclick='ShowStartTimeCalendarForLiveVideo(this)' ></div>";
                            }
                            else
                            {
                                final += "<div class='addButtonHolder' id='addvideosId_" + providerVideoId + "' onclick='addExternalVideoToChannel(this)' ></div>";
                            }
                            
                        }
                    }
                    else {
                        final += "<div class='addButtonContainer' id='addButtonHolderId_" + videoId + "' style='background-image: url(" + thumbnail + ");'>";
                            if (!isInChannel ) {
                            final += "<div class='addButtonHolder' id='addvideosId_" + videoId + "' onclick='addVideoToChannel(this)' ></div>";
                        }
                    }
                }
                else {
                    if (isExternalVideo) {
                        final += "<div class='addButtonContainer' id='addButtonHolderId_" + providerVideoId + "' style='background-image: url(" + thumbnail + ");'>";
                    }
                    else {
                        final += "<div class='addButtonContainer' id='addButtonHolderId_" + videoId + "' style='background-image: url(" + thumbnail + ");'>";
                    }
                }

                if (descripton && descripton != "") {
                    descripton = Controls.EscapeHtml(descripton);
                }

                if (descripton && descripton != "") {
                    descripton = Controls.EscapeHtml(descripton);
                }
                if ((videoProviderId == 4||isPrivate==true)&&(isSearch==false)) {
                final += "<div class='videoSettings material-icons' onclick='showVideoSettingsPopup(" + videoId + ")'>settings</div>"
            }

                final += "</div>" +
                        "<div class='videoBoxInfo'>" +
                            "<span class='videoBoxtitle ' title='" + title + "'>" + title + "</span>" +
                            "<span class='videoBoxviews'>views:<strong>" + viewCounter + "</strong></span>" +
                            "<span class='videoBoxduration'><strong>" + durationString + "</strong></span>" +
                            "<input type='hidden' value='" + durationInSec + "' class='durationHidden'/>" +
                            "<input type='hidden' value='" + descripton + "' class='descriptionHidden'/>";


                if (!isExternalVideo) {
                    final += "<span class='videoBoxcateg'><strong>" + categoryName + "</strong></span>";
                }

                final += "<input type='hidden' value='" + videoId + "' class ='hidden'/>" +
                         "</div>" +

                         "</div>";
            }

            
           
        });

        return final;
    },
    EscapeHtml: function(html) {
        return (html && html != "") ? html.replace(new RegExp("'", 'g'), "&#39") : html;
    },

    //used on pages: schedule, add-videos(by keyword, by url, video-room, public library)
    BuildVideoRoomControl: function (pageType, videos, justAdded) {
        var final = "";

        $.each(videos, function (i, d) {
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
            var thumbnail = d.ThumbnailUrl;
            var durationString = d.DurationString;
            var isInChannel = d.IsInChannel;
            var allowDelete = true;
            var dateAdded = d.DateAdded;
            var isExternalVideo = videoId == 0;
          
            if (thumbnail == null || thumbnail == "") {
                thumbnail = "/images/comingSoonBG.jpg";
            }
           
            if (!isExternalVideo) {
                if (!isInChannel) {
                    final += "<div class='channelBox' data-duration='" + durationInSec + "' data-views='" + viewCounter + "' data-videoadd='0' id='boxContent_" + videoId + "' date-added='" + dateAdded + "'>" +
                         "<div class='actions' id='action_" + videoId + "'>";
                }
                else {
                    final += "<div class='channelBox' data-duration='" + durationInSec + "' data-views='" + viewCounter + "' data-videoadd='1' id='boxContent_" + videoId + "' date-added='" + dateAdded + "'>" +
                         "<div class='actions' id='action_" + videoId + "'>";
                }

            }
            else {
                videoId = channelBoxIdCount;
                channelBoxIdCount++;
                final += "<div class='channelBox' data-duration='" + durationInSec + "' data-views='" + viewCounter + "' data-videoadd='0' id='boxContent_" + providerVideoId + "' date-added='" + dateAdded + "'>" +
                         "<div class='actions' id='action_" + videoId + "'>";
            }

            //diferent functions for different screen types


            if ((pageType == "add-videos") || (pageType == "public-library") || (pageType == "video-room")) {
                if (!isInChannel && !isRemoved && !isRestricted) {
                    if (!isExternalVideo) {
                        if ((pageType == "add-videos")) {
                            final += "<a class='addsymbol' id='addvideosId_" + videoId + "' onclick='addVideoToChannel(this)'>+</a>";
                        }
                        else {
                            final += "<a class='addsymbol' id='addId_" + videoId + "' onclick='addVideoToChannel(this)'>+</a>";
                        }
                    }
                    else {
                        if ((pageType == "add-videos")) {
                            final += "<a class='addsymbol' id='addvideosId_" + providerVideoId + "' onclick='addExternalVideoToChannel(this)'>+</a>";
                        }
                        else {
                            final += "<a class='addsymbol' id='addId_" + providerVideoId + "' onclick='addExternalVideoToChannel(this)'>+</a>";
                        }
                    }
                }
                else if (isRestricted) {
                    if (pageType == "add-videos") {
                        final += "<a class='videoRemoveMessage' id='addvideosId_" + videoId + "'>restricted by provider</a>";
                    }
                    else {
                        final += "<a class='videoRemoveMessage' id='addId_" + videoId + "'>restricted by provider</a>";
                    }
                }
                else if (isRemoved) {
                    if (pageType == "add-videos") {
                        final += "<a class='videoRemoveMessage' id='addvideosId_" + videoId + "'>removed by provider</a>";
                    }
                    else {
                        final += "<a class='videoRemoveMessage' id='addId_" + videoId + "'>removed by provider</a>";
                    }
                }
                else {
                    if (pageType == "add-videos") {
                        final += "<a class='videoAddedMessage' id='addvideosId_" + videoId + "'>added to channel</a>";
                    }
                    else {
                        final += "<a class='videoAddedMessage'id='addId_" + videoId + "'>added to channel</a>";
                    }
                }
            }

            if (pageType == "schedule") {
                if (!isRemoved) {
                    if (justAdded) {
                        final += "<a id='addId_" + videoId + "' class='addsymbol' onclick='addVideoScedule(this)'>+</a><span class='recentlyadded'>just in</span>" +
                                "<a id='remove_" + videoId + "' class='removesymbol' onclick='RemoveVideoFromChannel(this)'>&times;</a>";
                    }
                    else {
                        final += "<a id='addId_" + videoId + "' class='addsymbol' onclick='addVideoScedule(this)'>+</a>" +
                                "<a id='remove_" + videoId + "' class='removesymbol' onclick='RemoveVideoFromChannel(this)'>&times;</a>";

                    }
                }
                else if (isRestricted) {
                    final += "<a class='videoRemoveMessage' id='addId_" + videoId + "'>restricted by provider</a>";
                }
                else {
                    final += "<a class='videoRemoveMessage' id='addId_" + videoId + "'>removed by provider</a>";
                }
            }
            if (pageType == "video-room") {
                final += "<a id='remove_" + videoId + "' class='removesymbol' onclick='RemoveVideoFromVideoRoom(this)'>&times;</a>";
              
            }

            if (descripton && descripton != "") {
                descripton = Controls.EscapeHtml(descripton);
            }

            final += "</div>" +
                     "<a class='btnPlay' id='" + providerVideoId + "' onclick='showPlayer(this)'>" +
                     "<img class='PLAY-ICON' src='/images/PLAY-ICON(!).png' />" +
                     "<img class='IM0G' src='" + thumbnail + "' />" +
                   "</a>" +
                    "<div class='divVideoInformation'>" +
                        "<span class='title' title='" + title + "'>" + title + "</span>" +
                        "<span class='views'>views:" + viewCounter + "</span>" +
                        "<span class='duration'>duration:" + durationString + " </span>" +
                        "<input type='hidden' value='" + durationInSec + "' class='durationHidden'/>" +
                        "<input type='hidden' value='" + descripton + "' class='descriptionHidden'/>" +
                        "<span class='category'>category:&nbsp" + categoryName + "</span>" +
                        "<input type='hidden' value='" + videoId + "' class ='hidden'/>" +
                    "</div>";

            if (isScheduled) {
                final += "<span class='spnInScedule' id='spnInScedule_addId_" + videoId + "' style='display: none;'>in video room already</span>";
            }

            final += "</div>";


        });

        return final;
      
    },

    BuildVideoBoxControlForSearchPage: function (videos) {

        var final = "";

        $.each(videos, function (i, d) {
            if (d != null) {
                var categoryName = d.CategoryName;
                var channelTubeId = d.ChannelTubeId;
                var descripton = d.Description;
                var durationInSec = d.Duration;
                var isInPublicLibrary = d.IsInPublicLibrary;
                var videoId = d.VideoTubeId;
                var title = d.PlayingVideoTubeTitle;
                var isPrivate = d.IsPrivate;
                var isRRated = d.IsRRated;
                var isRemoved = d.IsRemovedByProvider;
                var providerVideoId = d.ProviderVideoId;
                var thumbnail = d.PlayingVideoTubeThumbnail;
                var durationString = d.DurationString;
                var dateAdded = d.DateAdded;
                var providerName = d.VideoProviderName;
                var playtimeMessage = d.PlaytimeMessage;
                var channelName = d.ChannelName;
                var channelUrl = d.UserPublicUrl + "/" + d.ChannelUrl;
                var viewCounter = 0;
                var isExternalVideo = false;
                var isInChannel = false;
                var isRestricted = false;
                var source = "search";
                var providerImgSrc;
                
                if (providerName != undefined && providerName != null) {
                    providerName = providerName.toLowerCase();
                }

                if (thumbnail == null || thumbnail == "") {
                    thumbnail = "/images/comingSoonBG.jpg";
                }

                if (providerName == "vimeo") {
                    providerImgSrc = "/images/vimeo-icon.png";
                }
                else if (providerName == "dailymotion") {
                    providerImgSrc = "/images/dMotion.png";
                }
                else if (providerName == "youtube") {
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


                final += "<div class='VideoBoxPlay' data-boxContentId='" + videoId + "' id='" + providerVideoId + "' >";
                final += "<a style='width:100%; height:100%; display:block;' href='/" + channelUrl + "'></a>"    
                final += "</div>";
                    //if (providerName == "vimeo") {
                    //    final += "<div class='VideoBoxPlay' data-boxContentId='" + videoId + "' id='" + providerVideoId + "' onclick='showPlayerVimeo(this)'></div>";
                    //}
                    //else if (providerName == "dailymotion") {
                    //    final += "<div class='VideoBoxPlay' data-boxContentId='" + videoId + "' id='" + providerVideoId + "' onclick='showPlayerDmotion(this)'></div>";
                    //}
                    //else {
                    //    final += "<div class='VideoBoxPlay' data-boxContentId='" + videoId + "' id='" + providerVideoId + "' onclick='showPlayer(this)'></div>";
                    //}

                final += "</div>";

                final += "<div class='videoBoxOverlay'>";
                final += "</div>";
                    
                final += "<div class='addButtonContainer' id='addButtonHolderId_" + videoId + "' style='background-image: url(" + thumbnail + ");'>";
                        

              
                 
                if (descripton && descripton != "") {
                    descripton = Controls.EscapeHtml(descripton);
                }


                final += "</div>" +
                        "<div class='videoBoxInfo'>" +
                            "<span class='videoBoxtitle ' title='" + title + "'>" + title + "</span>" +
                            "<a class='videoBoxtitle' href='/" + channelUrl + "'>On: " + channelName + "</a>" +
                            "<span class='videoBoxviews'><strong>" + playtimeMessage + "</strong></span>" +
                            "<input type='hidden' value='" + durationInSec + "' class='durationHidden'/>" +
                            "<input type='hidden' value='" + descripton + "' class='descriptionHidden'/>";


                final += "<input type='hidden' value='" + videoId + "' class ='hidden'/>" +
                         "</div>" +

                         "</div>";
            }
        });

        return final;
       
    },

    BuildChannelDailySchedulesDialogControl: function (response) {
        var final = "";
        var schedules = response.d;
        $.each(schedules, function (i, d) {
            var channelScheduleId = d.ChannelScheduleId;
            var message = d.Message;
            var allowDelete = d.AllowDelete;
            var allowEdit = d.AllowEdit;
            var allowRepeat = d.AllowRepeat;
            var videoSchedules = d.VideoSchedules;
            var message = d.Message;
           
            final += "<div id='schedulecontent_" + channelScheduleId + "' class='scheduleContent dragula-container'>";

            final += "<span title='" + message + "' class='spnTitle'>" + message + "</span>";

            $.each(videoSchedules, function (j, v) {
                var title = v.VideoTubeTitle;
                var thumbnail = v.Thumbnail;
                var playtime = v.PlaytimeMessage;
               
                final += "<div class='divWrapper'>";
                final += "<div class='divImgHolder'>" +
                            "<a class='aImgHolder' id='Gi5OP7fecrc'>" +// here should be the ProviderVideoId
                                "<img class='imgSchedule' src='" + thumbnail + "'>" +
                            "</a>" +
                        "</div>" +
                        "<div class='divTitle'>" +
                            "<span title='" + title + "' class='spnTitle'>" + title + "</span>" +
                        "</div>" +
                        "<div class='divTimeHolder'>" +
                            "<span class='timeHolder'>" + airtime + "</span>" +
                        "</div></div>";

            });

            if (allowRepeat) {
                final += "<div class='actionsModal'><a onclick='ShowRepeatSchedule(this)' class='repeat' id='repeat_" + channelScheduleId + "'>Repeat</a></div>";
            }
            if (allowEdit) {
                final += "<div class='actionsModal'><a onclick='EditChannelSchedule(this)' class='repeat' id='edit_" + channelScheduleId + "'>Edit</a></div>";
            }
            if (allowDelete) {
                final += "<div class='actionsModal'><a onclick='DeleteChannelSchedule(this)' class='repeat' id='delete_" + channelScheduleId + "'>Delete</a></div>";
            }
            final += "<div class='repeatBox'>";
            final += "<a id='close' class='close close_x' href='#'>&times;</a>";
            final += "<span class='spnChooseDate'>Please choose date</span>";
            final += "<input class='txtMonth' type='text' placeholder='mm/dd/yyyy' id='txtReapeatDate' />";
            final += "<a class='submitRepeat' id='" + channelScheduleId + "'>Submit</a>";
            final += "<span id='spnMsg'></span>";
            final += "</div>";

            final += "</div>";
        });

        return final;
    },

    BuildChannelControlForBrowseResultsPage: function (response) {
        var final = "";
        var channels = response;

        $.each(channels, function (i, d) {
            var categoryName = d.CategoryName;
            var channelTubeId = d.ChannelTubeId;
            var channelUrl = d.ChannelUrl;
            var channelViewsCount = d.ChannelViewsCount;
            var description = d.Description;
            var isFeatured = d.IsFeatured;
            var isLocked = d.IsLocked;
            var isPrivate = d.IsPrivate;
            var name = d.Name;
            var pictureUrl = d.PictureUrl;
            var rating = d.Rating;
            var subscriberCount = d.SubscriberCount;
            var videoTubeCount = d.VideoTubeCount;
            var channelOwnerUsername = d.ChannelOwnerUserName;
            var channelOwnerPublicUrl = d.ChannelOwnerUrl;
            var playingVideoTubeId = d.PlayingVideoTubeId;
            var createdDate = d.CreatedDate;
          
            if (pictureUrl == null || pictureUrl == "") {
                pictureUrl = "/images/comingSoonBG.jpg";
            }
            
            final += "<div class='chanBox' data-views='" + channelViewsCount + "' data-fans='" + subscriberCount + "' data-createddate='" + createdDate + "' >";
            final += "<a class='fullBox' href='/" + channelOwnerPublicUrl + "/" + channelUrl + "'>";
            final += "</a>";
            if (playingVideoTubeId > 0) {
                final += "<span class='channelTiming'>on air</span>";
            }


            final += "<img class='channelImg' src='" + pictureUrl + "'/>";

            final += "<div class='actionschanBoxHolder'>";
            //TODO CANCEL HREF ON PRIVATE CHANNEL AND ADD PRIVATE ICON IMG
            final += "<div class='chanBoxPlay'><a class='boxChannelName' href='/" + channelOwnerPublicUrl + "/" + channelUrl + "'></a></div>";
            final += "</div>";
            final += "<div class='chanBoxInfo'>";
            final += "<span class='chanBoxtitle chanBoxtitleAdjust'>" + name + "</span>";
            final += "<span class='chanBoxcateg chanBoxcategAdjust'>category:<strong>" + categoryName + "</strong></span>";
            //final += "<span class='chanBoxsub'>fans:<strong>" + subscriberCount + "</strong></span>";
            //final += "<span class='chanBoxviews'> views:<strong>" + channelViewsCount + "</strong></span>";



            final += "</div>";
            
            final += "</div>";
            
           
        });

        return final;
    },

    BuildChannelsForLandingPageControl: function (channels, maxCount, name, userid) {

        var final = "";

        final += "<div class='home-video-box-holder'>";
        final += "<h1 class='video-box-h1'>" + name + "</h1>";

        var dummyCount = 0;

        if (channels) {
            if (channels.length < maxCount) {
                dummyCount = maxCount - channels.length;
            }

            $.each(channels, function (i, d) {
                var categoryName = d.CategoryName;
                var channelTubeId = d.ChannelTubeId;
                var channelUrl = d.ChannelUrl;
                var channelViewsCount = d.ChannelViewsCount;
                var currentScheduleEndTime = d.CurrentScheduleEndTime;
                var currentScheduleStartTime = d.CurrentScheduleStartTime;
                var description = d.Description;
                var name = d.Name;
                var pictureUrl = d.PictureUrl;
                var playingVideoTubeEndTime = d.PlayingVideoTubeEndTime;
                var playingVideoTubeId = d.PlayingVideoTubeId;
                var playingVideoTubeStartTime = d.PlayingVideoTubeStartTime;
                var playingVideoTubeThumbnail = d.PlayingVideoTubeThumbnail;
                var playingVideoTubeTitle = d.PlayingVideoTubeTitle;
                var rating = d.Rating;
                var subscriberCount = d.SubscriberCount;
                var videoTubeCount = d.VideoTubeCount;
                var playtimeMessage = d.PlaytimeMessage;
                var channelOwnerUserName = d.ChannelOwnerUserName;
                var channelOwnerUrl = d.ChannelOwnerPublicUrl;
                var createdDate = d.CreatedDate;
                var channelUrl = "/" + channelOwnerUrl + "/" + d.ChannelUrl;

                if (pictureUrl == null || pictureUrl == "") {
                    pictureUrl = "/images/comingSoonBG.jpg";
                }

               

                final += "<div class='home-video-box-0" + (i + 1) + "'>";
                final += "<div class='chanBox'>";

                if (playingVideoTubeId > 0) {
                    final += "<a class='btnPlay' href='" + channelUrl + "'>";
                }

                final += "<img class='channelImg' src='" + pictureUrl + "' />";

               final += "<img class='PLAY-ICON' src='/images/PLAY-ICON(!).png' />";
             

                final += "<div class='chanBoxInfo'>";
                final += "<span class='chanBoxtitle'>" + name + "</span>";

                if (playingVideoTubeId > 0) {
                    final += "<span class='channelTiming'>on air</span>";
                }

                final += '</div>';
                final += "</a>"

                final += '</div>';
                final += '</div>';
            });
        }
        else {
            dummyCount = maxCount;
        }

        if (dummyCount > 0) {
            var startCount = maxCount - dummyCount;
            for (i = startCount; i < maxCount; i++) {
                if (i + 1 > 9) {
                    final += "<div class='home-video-box-" + (i + 1) + "'></div>";
                }
                else {
                    final += "<div class='home-video-box-0" + (i + 1) + "'></div>";
                }
            }
        }

        final += "<a href='/browse-channel?category=most popular'><div class='home-video-box-add'/></a>";
       
        final += '</div>';

        return final;

    },

    // used on advanced-search page
    BuildChannelControl: function (response) {

        var final = "";
        var channels = response.d;

        $.each(channels, function (i, d) {
            var categoryName = d.CategoryName;
            var channelTubeId = d.ChannelTubeId;
            var channelUrl = d.ChannelUrl;
            var channelViewsCount = d.ChannelViewsCount;
            var currentScheduleEndTime = d.CurrentScheduleEndTime;
            var currentScheduleStartTime = d.CurrentScheduleStartTime;
            var description = d.Description;
            var name = d.Name;
            var pictureUrl = d.PictureUrl;
            var playingVideoTubeEndTime = d.PlayingVideoTubeEndTime;
            var playingVideoTubeId = d.PlayingVideoTubeId;
            var playingVideoTubeStartTime = d.PlayingVideoTubeStartTime;
            var playingVideoTubeThumbnail = d.PlayingVideoTubeThumbnail;
            var playingVideoTubeTitle = d.PlayingVideoTubeTitle;
            var rating = d.Rating;
            var subscriberCount = d.SubscriberCount;
            var videoTubeCount = d.VideoTubeCount;
            var playtimeMessage = d.PlaytimeMessage;
            var channelOwnerUserName = d.ChannelOwnerUserName;
            var createdDate = d.CreatedDate;
            var channelOwnerUrl = d.ChannelOwnerUrl;
            var channelUrl = "/" + channelOwnerUrl + "/" + d.ChannelUrl;

            if (pictureUrl == null || pictureUrl == "") {
                pictureUrl = "/images/comingSoonBG.jpg";
            }

            final += "<div class='chanBox'>";

            // final += "<a href='" + channelUrl + "' title='" + name + "'>";
            final += "<img class='channelImg' src='" + pictureUrl + "' />";
            final += '<div class="actionschanBoxHolder actionHOlderFull">';
            final += '<div class="chanBoxPlay chanBoxPlayFull"> <a class="boxChannelName" href="' + channelUrl + '"></a></div>';
            final += '</div>';
            final += '<div class="chanBoxInfo">';
            final += '<span class="chanBoxtitle chanBoxtitleAdjust">' + name + '</span>';
            final += '<span class="chanBoxcateg">category: <strong>' + categoryName + '</strong></span>';
            //final += '<span class="chanBoxsub">fans:<strong>' + subscriberCount + '</strong></span>';
            //final += '<span class="chanBoxviews"> views:<strong>' + channelViewsCount + '</strong></span>';

            if (playingVideoTubeId > 0) {
                final += "<span class='channelTiming'>on air</span>";
            }
            final += '</div>';
            final += '</div>';

        });
        return final;

    },

    //used on favorite channels page
    BuildFavoriteChannelControl: function (response) {

        var final = "";
        var channels = response.d;
        $.each(channels, function (i, d) {
            var categoryName = d.CategoryName;
            var channelTubeId = d.ChannelTubeId;
            var channelUrl = d.ChannelUrl;
            var channelViewsCount = d.ChannelViewsCount;
            var currentScheduleEndTime = d.CurrentScheduleEndTime;
            var currentScheduleStartTime = d.CurrentScheduleStartTime;
            var description = d.Description;
            var name = d.Name;
            var pictureUrl = d.PictureUrl;
            var playingVideoTubeEndTime = d.PlayingVideoTubeEndTime;
            var playingVideoTubeId = d.PlayingVideoTubeId;
            var playingVideoTubeStartTime = d.PlayingVideoTubeStartTime;
            var playingVideoTubeThumbnail = d.PlayingVideoTubeThumbnail;
            var playingVideoTubeTitle = d.PlayingVideoTubeTitle;
            var rating = d.Rating;
            var subscriberCount = d.SubscriberCount;
            var videoTubeCount = d.VideoTubeCount;
            var playtimeMessage = d.PlaytimeMessage;
            var channelOwnerUserName = d.ChannelOwnerUserName;
            var createdDate = d.CreatedDate;
            var channelOwnerUrl = d.ChannelOwnerUrl;
            var channelUrl = "/" + channelOwnerUrl + "/" + d.ChannelUrl;
            var recentlyAdded = d.CreatedDate;
            var playing = 0;
            var firstWord = name.slice(0, name.indexOf(" "));
            if (playingVideoTubeId > 0) {
                playing = 1;
            }
            if (pictureUrl == null || pictureUrl == "") {
                pictureUrl = "/images/comingSoonBG.jpg";
            }

            var dataParams = " data-added='" + recentlyAdded + "' data-playing='" + playing + "' data-name='" + firstWord + "'";
            final += "<div class='chanBox'" + dataParams + " id='" + channelTubeId + "'>";
            // final += "<a href='" + channelUrl + "' title='" + name + "'>";
            final += "<img class='channelImg' src='" + pictureUrl + "' />";
            final += '<div class="actionschanBoxHolder">';
            final += '<div class="chanBoxPlay"> <a class="boxChannelName" href="' + channelUrl + '"></a></div>';
            final += '<div class="remove VideoBoxRemoveFav"><a class="ancRemove " title="Remove from Favorites" onclick="ShowModalRemove(userId,' + channelTubeId + ')"></a></div>'
            final += '</div>';
            final += '<div class="chanBoxInfo">';
            final += '<span class="chanBoxtitle chanBoxtitleAdjust">' + name + '</span>';
            final += '<span class="chanBoxcateg">category: <strong>' + categoryName + '</strong></span>';
            //final += '<span class="chanBoxsub">fans:<strong>' + subscriberCount + '</strong></span>';
            //final += '<span class="chanBoxviews"> views:<strong>' + channelViewsCount + '</strong></span>';
            if (playingVideoTubeId > 0) {
                final += "<span class='channelTiming'>on air</span>";
            }
            final += '</div>';
            final += '</div>';

        });
        return final;

    },

    // used on advanced-search page
    BuildUserControls: function (response) {

        var final = "";
        var channels = response.d;
        $.each(channels, function (i, d) {
            var accountNumber = d.AccountNumber;
            var address = d.Address;
            var virthDate = d.BirthDate;
            var city = d.City;
            var company = d.Company;
            var country = d.Country;
            var email = d.Email;
            var firstName = d.FirstName;
            var gender = d.Gender;
            var isDeleted = d.IsDeleted;
            var lastName = d.LastName;
            var phoneNumber = d.PhoneNumber;
            var profileImageUrl = d.ProfileImageUrl;
            var stateOrProvince = d.StateOrProvince;
            var userId = d.UserId;
            var userName = d.UserName;
            var userStory = d.UserStory;
            var zipCode = d.ZipCode;
            if (profileImageUrl == null) {
                if (d.Gender == "Male") {
                    profileImageUrl = '/images/imgUserAvatarMale.jpg';
                    
                }
                else {
                    profileImageUrl = '/images/imgUserAvatarFemale.jpg';
                }
            }
            final += "<div class='userBox'>";
            // final += "<a href='" + channelUrl + "' title='" + name + "'>";
            final += "<a class='thumbLink' href='/" + d.PublicUrl.replace(" ", "") + "'>";
            final += "<img class='IMG'  src='" + profileImageUrl + "'/>";
            final += "</a>";
            final += "<div class='userBoxInfo'>";
            final += "<div class='userBoxFirstName'><span class='lblfirstName'>" + firstName + "</span> <span class='lblLastname'>" + lastName + "</span></div>";
            
            final += "<a href='" + d.PublicUrl.replace(" ", "") + "' class='userMainInfoFav'><span class='fchannelNameFav'>" + userName, + "</span></a>";
          
            final += "<span class='userBoxCountryFav'>" + country + "</span>";
            //final += "<div class='userBoxChannelsFav'><span class='lblChannelsFav'>Channels:</span>";
            //final += "<span class='userBoxChannelListFav'>MaxTV, SportTV, MusicTV and other</span>";
            final += "</div></div>";

        });
        return final;
    },

    //used on schedule page
    BuildVideoScheduleHolderBox: function (channelScheduleId, starttime) {
        var newBoxContent = "<div class='scheduleBoxContentBox' id='scheduleBoxContentBox_" + channelScheduleId + "'>";
        
        newBoxContent += "<div class='actionsHolder'>";
        newBoxContent += "<a class='ancPublishSchedule' id='publish_" + channelScheduleId + "' onclick='PublishSchedule(this)'>publish</a>";
        newBoxContent += "<a class='ancEditSchedule' id='edit_" + channelScheduleId + "' onclick='EditSchedule(this)'>edit</a>";
        newBoxContent += "<a class='ancRepeat colorChange' id='repeat_" + channelScheduleId + "'onclick='ShowRepeatCalendar(this)'>Repeat</a>";
        newBoxContent += "<div id='repeatCalendarPickerHolder'style='display:none;'><div id='repeatCalendarPicker'></div></div>";
        newBoxContent += "<a class='ancDeleteSchedule colorChange' id='delete_" + channelScheduleId + "'onclick='DeleteSchedule(this)'>Delete</a>";
        newBoxContent += "</div>";
        newBoxContent += "<div class='airtimeHolder'>"
        newBoxContent += "<span class='playback'>airtime:</span><span class=playbackTime>" + starttime + " -  </span>";
        newBoxContent += "<a class='quickViewArrow' id='ancToggleSchedule_" + channelScheduleId + "' onclick='ToggleSchedule(this)'>▲</a>";
        newBoxContent += "</div>";
        newBoxContent += "<div class='scheduleContent'></div>";
        newBoxContent += "</div>";
        return newBoxContent;
    },

   //used on schedule page
    BuildVideoScheduleControl: function (videoSchedules) {
        var final = "";
        // videoSchedules = response;
        $.each(videoSchedules, function (i, d) {
            var categoryName = d.CategoryName;
            var videoTubeId = d.VideoTubeId;
            var videoTubeTitle = d.VideoTubeTitle;
            var videoProviderName = d.VideoProviderName;
            var providerVideoId = d.ProviderVideoId;
            var playbackOrderNumber = d.PlaybackOrderNumber;
            var playbackStartTime = d.PlaybackStartTime;
            var playbackEndTime = d.PlaybackEndTime;
            var isInPublicLibrary = d.IsInPublicLibrary;
            var isPrivate = d.IsPrivate;
            var isRemovedByProvider = d.IsRemovedByProvider;
            var isRRated = d.IsRRated;
            var duration = d.Duration;
            var description = d.Description;
            var thumbnailUrl = d.ThumbnailUrl;
            var thumbnail = d.Thumbnail;
            var playtimeMessage = d.PlaytimeMessage;
            var allowDeleted = d.AllowDeleted;
            
            
          
            if (videoProviderName != undefined && videoProviderName != null) {
                videoProviderName = videoProviderName.toLowerCase();
            }

            if (thumbnail == null || thumbnail == "") {
                thumbnail = "/images/comingSoonBG.jpg";
            }
           
          

            final += " <div class='divSchedule' id='scheduleThumb_" + videoTubeId + "' style='width:100%;'>";
            final += "<span id='lblTime_" + videoTubeId + "' class='spnTime'>" + playtimeMessage + "</span>";
            final += "<span id='lblTitle_" + videoTubeId + "' class='spnTitle'>" + videoTubeTitle + "</span>";
            final += "<img src='" + thumbnail + "' class='imgScheduleThumb'/>";
            final += "<a class='btnRemove' id='btnRemove_" + videoTubeId + "|" + playbackOrderNumber + "' onclick='removeVideoSchedule(this)'> &times;</a>";
            if (isPrivate) {
                final += "<a class='isPrivateTag'></a>";
            }
            final += "</div>";
           
        });
        return final;
    },

    //used on schedule page
    BuildChannelScheduleWithVideoForEdit: function (d) {
        var final = "";
        var finalB = "";
        //var d = response.d;
      
        //$.each(channelSchedules, function (i, d) {
            var message = d.Message;
            var channelTubeId = d.ChannelTubeId;
            var channelScheduleId = d.ChannelScheduleId;
            var videoSchedules = d.VideoSchedules;
            var allowEdit = d.AllowEdit;
            var allowRepeat = d.AllowRepeat;
            var allowDelete = d.AllowDeleted;
            var startTime = d.StartTime;
            var endTime = d.EndTime;
            var isPublished = d.Published;
            var expandVideoSchedulesList = d.ExpandVideoSchedulesList;

            var now = new Date();
            var start = new Date(parseInt(d.StartDateAndTime.replace(/[^0-9 +]/g, '')));
            var offsetInMin = start.getTimezoneOffset();
            start.setMinutes(start.getMinutes() + offsetInMin);
            var hoursBeforeStart = (start.getTime() - now.getTime()) / 1000 / 60 / 60;

            if (videoSchedules && videoSchedules.length > 0) {
                var lastPlaytime = new Date(parseInt(videoSchedules[videoSchedules.length - 1].PlaybackEndTime.replace(/[^0-9 +]/g, '')));
                lastPlaytime.setMinutes(lastPlaytime.getMinutes() + offsetInMin);
                var isOffAir = (lastPlaytime.getTime() - now.getTime()) < 0;

                $.each(videoSchedules, function (j, v) {
                    var categoryName = v.CategoryName;
                    var videoTubeId = v.VideoTubeId;
                    var videoTubeTitle = v.VideoTubeTitle;
                    var videoProviderName = v.VideoProviderName;
                    var providerVideoId = v.ProviderVideoId;
                    var playbackOrderNumber = v.PlaybackOrderNumber;
                    var playbackStartTime = v.PlaybackStartTime;
                    var playbackEndTime = v.PlaybackEndTime;
                    var isInPublicLibrary = v.IsInPublicLibrary;
                    var isPrivate = v.IsPrivate;
                    var isRemovedByProvider = v.IsRemovedByProvider;
                    var isRRated = v.IsRRated;
                    var duration = v.Duration;
                    var description = v.Description;
                var thumbnail = v.Thumbnail;
                    var playtimeMessage = v.PlaytimeMessage;
                    var allowDeleted = v.AllowDeleted;
                    var startTimeFormat = (new Date(parseInt(playbackStartTime.replace(/[^0-9 +]/g, '')))).format("hh:mm tt");
                    
                    if (thumbnail == null || thumbnail == "") {
                        thumbnail = "/images/comingSoonBG.jpg";
                    }


                    finalB += "<div class='divSchedule' data-playbackOrder='"+playbackOrderNumber+"' data-isprivate='" + isPrivate + "' id='scheduleThumb_" + videoTubeId + "' style='width:100%;'>";
                    if (isPrivate) {
                        finalB += "<a class='isPrivateTag'></a>";
                    }
                    finalB += "<span id='lblTime_" + videoTubeId + "' class='spnTime'>" + playtimeMessage + "</span>";
                finalB += "<span id='lblTitle_" + videoTubeId + "' class='spnTitle' title='" + videoTubeTitle + "'>" + videoTubeTitle + "</span>";
                    finalB += "<img src='" + thumbnail + "' class='imgScheduleThumb'/>";
                    //if (hoursBeforeStart > 24 || !isPublished) {
                        finalB += "<a class='btnRemove' id='btnRemove_" + videoTubeId + "|" + playbackOrderNumber + "|" + channelScheduleId + "' onclick='removeVideoSchedule(this)'>&times;</a>";
                    //}
                    finalB += "</div>";
                });
            }
            else {
                finalB += "<div class='divSchedule' style='width:100%;'>";
                finalB += "<span class='spnTitle samTitle' style='width:99%;'>Click on the '+' (appears on mouse over) on each video to add it to this schedule</span>";
                finalB += "</div>";
            }

            final = "<div class='scheduleBoxContentBox' id='scheduleBoxContentBox_" + channelScheduleId + "'>";
           
            final += "<div class='actionsHolder'>";

            var now = new Date();
            var hoursBeforeStart = (start.getTime() - now.getTime()) / 1000 / 60 / 60;

            if (isPublished) {
                if (isOffAir) {
                    final += "<a class='ancOffAirPublishSchedule publishedDisactivated ' id='publish_" + channelScheduleId + "' >off air</a>";

                }
                else if (hoursBeforeStart < 24 && hoursBeforeStart > -24) {
                    final += "<a class='ancOnAirPublishSchedule publishedDisactivated ' id='publish_" + channelScheduleId + "' onclick='UnPublishSchedule(this)'>on air</a>";
                }
                else {
                    final += "<a class='ancPublishSchedule published' id='publish_" + channelScheduleId + "' onclick='UnPublishSchedule(this)'>published</a>";
                }
            }
            else {
                final += "<a class='ancPublishSchedule' data-hoursBeforeStart='" + hoursBeforeStart + "' id='publish_" + channelScheduleId + "' onclick='PublishSchedule(this)'>publish</a>";
            }

            //final += "<a class='ancEditSchedule' id='edit_" + channelScheduleId + "' onclick='EditSchedule(this)'>edit</a>";
            final += "<a class='ancRepeat colorChange' id='repeat_" + channelScheduleId + "'onclick='ShowRepeatCalendar(this)'>Repeat</a>";
            final += "<div id='repeatCalendarPickerHolder'style='display:none;'><div id='repeatCalendarPicker'></div></div>";

            // MST: 5/11/15: Per Dima still need to display message
            //if (hoursBeforeStart < 24) {
            //    final += "<a class='actionsHolderDisactivated ' id='delete_" + channelScheduleId + "'>delete</a>";
            //}
            //else {
            if (isPublished) {
                if (isOffAir) {
                   

                }
                //else if (hoursBeforeStart < 24 && hoursBeforeStart > -24) {
                //   
                //}
                else {
                    final += "<a class='ancDeleteSchedule colorChange' id='delete_" + channelScheduleId + "'onclick='DeleteSchedule(this)'>Delete</a>";
                }
            }
            else {
            final += "<a class='ancDeleteSchedule colorChange' id='delete_" + channelScheduleId + "'onclick='DeleteSchedule(this)'>Delete</a>";
            }
           
            //}

            final += "</div>";
        final += "<div class='airtimeHolder'>"
            final += "<span class='playback'>Airtime:</span><span class=playbackTime>" + startTime + " - " + endTime + "</span>";
            final += "<a class='quickViewArrow' id='ancToggleSchedule_" + channelScheduleId + "' onclick='ToggleSchedule(this)'>▼</a>";
            final += "</div>";
        final += "<div class='scheduleContent dragula-container' style='display:none;'>";
            final += finalB;
            final += "</div>";
            final += "</div>";
        //});
        return final;
      
    },

    //used on channel
    BuildVideoScheduleControlForChannel: function (videoSchedules) {
        var final = "";
        // videoSchedules = response;
        $.each(videoSchedules, function (i, d) {
            var categoryName = d.CategoryName;
            var videoTubeId = d.VideoTubeId;
            var videoTubeTitle = d.VideoTubeTitle;
            var videoProviderName = d.VideoProviderName;
            var providerVideoId = d.ProviderVideoId;
            var playbackOrderNumber = d.PlaybackOrderNumber;
            var playbackStartTime = d.PlaybackStartTime;
            var playbackEndTime = d.PlaybackEndTime;
            var isInPublicLibrary = d.IsInPublicLibrary;
            var isPrivate = d.IsPrivate;
            var isRemovedByProvider = d.IsRemovedByProvider;
            var isRRated = d.IsRRated;
            var duration = d.Duration;
            var description = d.Description;
            var thumbnail = d.ThumbnailUrl;
            var playtimeMessage = d.PlaytimeMessage;
            var allowDeleted = d.AllowDeleted;

            if (videoProviderName != undefined && videoProviderName != null) {
                videoProviderName = videoProviderName.toLowerCase();
            }

            if (thumbnail == null || thumbnail == "") {
                thumbnail = "/images/comingSoonBG.jpg";
            }

            final += "<a id='order_" + playbackOrderNumber + "' class='sideSchedule'  title='" + videoTubeTitle + "'>";
            final += "<span class='videoAvatar'>";
            final += "<img src='" + thumbnail + "'/></span>";
            final += "<strong>"
            final += "<span class='videoTiming'>" + playtimeMessage + "</span>";
            final += "<span class='videoTitle' title='" + videoTubeTitle + "'>" + videoTubeTitle + "</span>";
            final += "<span class='playingNowShow'>playing </span>";
            final += "<span class='playingNextShow'> next</span>";
            final += "</strong></span>";
            final += "<div id='rec" + videoTubeId + "' class='record' title='Record to watch later' onclick='WatchItLater(" + videoTubeId + ")'></div>";
            final += "</a>";
        });
        return final;
        
    },

    BuildLiveVideoScheduleControlForChannel: function (videoSchedules) {
        var final = "";
        var now = new Date();
        var offset = now.getTimezoneOffset();
        var tz = jstz.determine();

        // videoSchedules = response;
        $.each(videoSchedules, function (i, d) {
            var categoryName = d.CategoryName;
            var videoTubeId = d.VideoTubeId;
            var videoTubeTitle = d.Title;
            var videoProviderName = d.VideoProviderName;
            var providerVideoId = d.ProviderVideoId;
            //var playbackOrderNumber = d.PlaybackOrderNumber;
            var playbackStartTime = d.StartTime;
            var playbackEndTime = d.EndTime;

            var startDateinUtc = new Date(parseInt(playbackStartTime.replace('/Date(', '')));
            var startMinutes = startDateinUtc.getMinutes();
            var startDateInLocal = startDateinUtc.setMinutes(startMinutes-offset);

            var startDateLocal = moment(startDateInLocal).format('MMMM DD YYYY, h:mm a');//moment().format('MMMM Do YYYY, h:mm:ss a');
            var startDateUI = moment(startDateLocal).tz(tz.name()).format("MMMM DD, YYYY");
            var startTimeUI = moment(startDateLocal).tz(tz.name()).format("MMM DD, YYYY | hh:mm a");
            var endDateTimeUI;
            
            startDateinUtc = moment.tz(playbackStartTime.toString('YYYY-MM-dd  HH:mm a'), 'Etc/GMT');

            startDateLocal = startDateinUtc.clone().tz(tz.name()).add(-offset / 60, 'hours');

            startTimeUI = moment(startDateLocal).format("MMM DD, YYYY | hh:mm a");

            if (videoProviderName != undefined && videoProviderName != null) {
                videoProviderName = videoProviderName.toLowerCase();
            }

            if (playbackEndTime == null)
            {
                endDateTimeUI = "---- "
            }
            else
            {
                endTimeInUTC = moment.tz(playbackEndTime.toString('YYYY-MM-dd  HH:mm a'), 'Etc/GMT');
             
                endTimeLocal = endTimeInUTC.clone().tz(tz.name()).add(-offset / 60, 'hours');
             
                endDateTimeUI = moment(endTimeLocal).format("MMM DD, YYYY | hh:mm a");
            }

            var isInPublicLibrary = d.IsInPublicLibrary;
         //   var isPrivate = d.IsPrivate;
            var isRemovedByProvider = d.IsRemovedByProvider;
            var isRRated = d.IsRRated;
           // var duration = d.Duration;
            var description = d.Description;
           
            final+='<div class="infoChannelLive">';
            final += '<span class="subinfoChannelLive titleLive" title="' + videoTubeTitle + '" alt="' + videoTubeTitle + '" id="videoLiveId_' + providerVideoId + '" onclick="PlayLiveVideo(this)">' + videoTubeTitle + '</span>';
            final += '<a id="btn_' + providerVideoId + '" class="btnPlayLiveVideo material-icons" onclick="PlayLiveVideo(this)">play_arrow</a>';
            final+='<span class="liveEventInfo" id="liveVideoInfo_' + providerVideoId + '" onclick="ShowVideoLiveInfoPopup(this)" ></span>';

            final += '<span class="subinfoChannelLive">' + startTimeUI + '</span>';
            final += '<span class="subinfoChannelLive">' + endDateTimeUI + '</span>';

            final += '<input type="hidden" id="inputDescription_' + providerVideoId + '" value="'+description+'"/>'
            final += '</div>';

            final += '<div class="liveEventInfoPopUp" id="infoLive_' + providerVideoId + '">';
            final += '<div class="closeMenuLive closeVideoLiveInfoBox" onclick="closeVideoLiveInfoBox()"></div>';
            final += '<div class="liveEventInfoPopUpTitle">' + videoTubeTitle + '</div>';
            final += '<div class="liveEventInfoPopUpDescription more">'
            final+= '<div class="nano">';
            final += '<div class="content liveInfo">' + description;
            
            final += '</div></div>';
             final+= '</div>';
          
            final += '</div>';
          
        });
        return final;

    },

    BuildScheduleControlForLandingPage: function (videoSchedules) {

       

        var final = "";
        // videoSchedules = response;
        $.each(videoSchedules, function (i, d) {
            var categoryName = d.CategoryName;
            var videoTubeId = d.VideoTubeId;
            var videoTubeTitle = d.VideoTubeTitle;
            var videoProviderName = d.VideoProviderName;
            var providerVideoId = d.ProviderVideoId;
            var playbackOrderNumber = d.PlaybackOrderNumber;
            var playbackStartTime = d.PlaybackStartTime;
            var playbackEndTime = d.PlaybackEndTime;
            var isInPublicLibrary = d.IsInPublicLibrary;
            var isPrivate = d.IsPrivate;
            var isRemovedByProvider = d.IsRemovedByProvider;
            var isRRated = d.IsRRated;
            var duration = d.Duration;
            var description = d.Description;
            var thumbnail = d.Thumbnail;
            var playtimeMessage = d.PlaytimeMessage;
            var allowDeleted = d.AllowDeleted;

            if (videoProviderName != undefined && videoProviderName != null) {
                videoProviderName = videoProviderName.toLowerCase();
            }

            var times = playtimeMessage.split('-');
            var start = times[0] + '-';

            final+='<div class="scheduleHome" id="order_' + playbackOrderNumber + '">';
            final += '<div class="scheduleHomeTime"> ';
            final += "<span class='videoTiming'>" + start + "</span>";
            final += '<span class="playingNextHome"> Next</span>';
            final += '<span class="playingNowHome"> Playing</span>';
            final += '</div>';
            final += '<div class="scheduleHomeTitle" title=" ' + videoTubeTitle + '">' + videoTubeTitle + '</div>';
            final += '</div>';


           
          

        });
        return final;

    },

    BuildChannelControlForDashboardPage: function (response, isPlayTimeMessageOn) {
        var channelTubeModels = response.d;
        void 0;
        var final = "";
        $.each(channelTubeModels, function (i, d) {
            var categoryName = d.CategoryName;
            var channelTubeId = d.ChannelTubeId;
            var channelUrl = d.ChannelUrl;
            var channelViewsCount = d.ChannelViewsCount;
            var currentScheduleEndTime = d.CurrentScheduleEndTime;
            var currentScheduleStartTime = d.CurrentScheduleStartTime;
            var description = d.Description;
            var name = d.Name;
            var pictureUrl = d.PictureUrl;
            var playingVideoTubeEndTime = d.PlayingVideoTubeEndTime;
            var playingVideoTubeId = d.PlayingVideoTubeId;
            var playingVideoTubeStartTime = d.PlayingVideoTubeStartTime;
            var playingVideoTubeThumbnail = d.PlayingVideoTubeThumbnail;
            var playingVideoTubeTitle = d.PlayingVideoTubeTitle;
            var rating = d.Rating;
            var subscriberCount = d.SubscriberCount;
            var videoTubeCount = d.VideoTubeCount;
            var playtimeMessage = d.PlaytimeMessage;
            var channelOwnerUserName = d.ChannelOwnerUserName;
            var createdDate = d.CreatedDate;
            var channelOwnerUrl = d.ChannelOwnerUrl;

            if (pictureUrl == null || pictureUrl == "") {
                pictureUrl = "/images/comingSoonBG.jpg";
            }

            final += "<div class='chanBox'>";
            final += "<img class='channelImg' src='" + pictureUrl + "' />";
            final += "<div class='actionschanBoxHolder'>";
            final += "<div id='chanBoxEdit' class='chanBoxEdit'><a class='ancEdit ancEditChannel' href='/" + channelOwnerUrl + "/my-studio/" + name.toLowerCase().replace(" ", "") + "'></a></div>";
            final += "<div class='chanBoxPlay'><a class='boxChannelName' href='/" + channelOwnerUrl + "/" + name.toLowerCase().replace(" ", "") + "'></a></div>";
            final += "</div>";
            final += "<div class='chanBoxInfo'>";
            final += "<span class='chanBoxtitle  chanBoxtitleAdjust'>" + name + "</span>";
            final += "<span class='chanBoxcateg'>category: <strong>" + categoryName + "<strong></span>";
            //final += "<span class='chanBoxsub'>fans:<strong>" + subscriberCount + "</strong></span>";
            //final += "<span class='chanBoxviews'> views:<strong>" + channelViewsCount + "</strong></span>";

            if (playingVideoTubeId > 0) {
                final += "<span class='channelTiming'>on air</span>";
            }

            final += "</div>";
            final += "</div>";

        });

        return final;
    },

    //CHANNEL BOX USED ON CHANNEL AND ON DASHBOARD PAGES
    BuildChannelControlForChannelPage: function (response, isPlayTimeMessageOn) {
        var channelTubeModels = response.d;
        void 0;
        var final = "";
        $.each(channelTubeModels, function (i, d) {
            var categoryName = d.CategoryName;
            var channelTubeId = d.ChannelTubeId;
            var channelUrl = d.ChannelUrl;
            var channelViewsCount = d.ChannelViewsCount;
            var currentScheduleEndTime = d.CurrentScheduleEndTime;
            var currentScheduleStartTime = d.CurrentScheduleStartTime;
            var description = d.Description;
            var name = d.Name;
            var pictureUrl = d.PictureUrl;
            var playingVideoTubeEndTime = d.PlayingVideoTubeEndTime;
            var playingVideoTubeId = d.PlayingVideoTubeId;
            var playingVideoTubeStartTime = d.PlayingVideoTubeStartTime;
            var playingVideoTubeThumbnail = d.PlayingVideoTubeThumbnail;
            var playingVideoTubeTitle = d.PlayingVideoTubeTitle;
            var rating = d.Rating;
            var subscriberCount = d.SubscriberCount;
            var videoTubeCount = d.VideoTubeCount;
            var playtimeMessage = d.PlaytimeMessage;
            var channelOwnerUserName = d.ChannelOwnerUserName;
            var channelOwnerUrl = d.ChannelOwnerUrl;
            var createdDate = d.CreatedDate;
            if (pictureUrl == null || pictureUrl == "") {
                pictureUrl = "/images/comingSoonBG.jpg";
            }
            
            final += "<a class='sideChannel' href='/" + channelOwnerUrl + "/" + channelUrl + "' title='" + name + "' data-views='" + channelViewsCount + "' date-fans='" + subscriberCount + "' date-createdDate='" + createdDate + "'>";
            final += "<span class='channelAvatar'>";
            final += "<img src='" + pictureUrl + "' /></span>";
            final += "<strong>";
            final += "<span class='channelTitle'>" + name + "</span>";

            if (playingVideoTubeTitle != null) {
                final += "<span class='videoTitle' title='" + playingVideoTubeTitle + "'>" + playingVideoTubeTitle + "</span>";
            }

            if (playingVideoTubeId > 0) {
                final += "<span class='playTime'>" + playtimeMessage + "</span>";
            }
            //if (playingVideoTubeId == 0) {

            //    final += "<span class='channelTiming'>Not broadcasting now</span>";
            //}
            //final += "<span class='spnSubscribers'>fans: " + subscriberCount + "</span>";
            //final += "<span class='spnViews'>views: " + channelViewsCount + "</span>";

            if (playingVideoTubeId > 0) {
                final += "<span class='channelTiming'>on air</span>";
            }

            final += "</strong>";
            final += "</a>";
        });

        return final;
    },

    BuildVideoScheduleControlForChannelPage: function (videoSchedules) {
        var final = "";
        $.each(videoSchedules, function (i, d) {
            var categoryName = d.CategoryName;
            var description = d.Description;
            var duration = d.Duration;
            var isRemovedByProvider = d.IsRemovedByProvider;
            var isRRated = d.IsRRated;
            var playbackEndTime = d.PlaybackEndTime;
            var playbackOrderNumber = d.PlaybackOrderNumber;
            var playbackStartTime = d.PlaybackStartTime;
            var providerVideoId = d.ProviderVideoId;
            var thumbnail = d.ThumbnailUrl;
            var videoProviderName = d.VideoProviderName;
            var videoTubeTitle = d.VideoTubeTitle;
            var videoTubeId = d.VideoTubeId;
            var playtimeMessage = d.PlaytimeMessage;
            
            if (videoProviderName != undefined && videoProviderName != null) {
                videoProviderName = videoProviderName.toLowerCase();
            }

            if (thumbnail == null || thumbnail == "") {
                thumbnail = "/images/comingSoonBG.jpg";
            }

            final += "<a class='sideSchedule' href='#' title='" + videoTubeTitle + ">";
            final += "<span class='videoAvatar'>";
            final += "<img src='" + thumbnail + "'/></span>";
            final += "<strong>";
            final += "<span class='videoTiming'>" + playtimeMessage + "</span>";
            final += "<span class='videoTitle' title='" + videoTubeTitle + "'>" + videoTubeTitle + "</span>";
            final += "</strong>";
            final += "</span></a>";
        });

        return final;
    },

    BuildImportPrivateVideosView: function(name, isYoutubeActive, isVimeoActive, isDmotionActive, isPro, isMatureContentEnabeled)
    {
        var final="";
        final += '<div id="importPrivateVideoHolder" >';
        final += '<span id="spnAttention">Attention: The TV programs with private videos are ONLY shown on embedded channels and will not broadcast on Strimm</span>'
        final+='<ul id="leftSide" class="ulImportVideoDetails">';
        final+='<li id="liImageEditor">';
        //final+='<span class="title">Video Thumbnail</span>';
        final+='<div class="image-editor">';
        final+= '<div class="image-size-label">';
        final+= '</div>';
        final+= '<div class="cropit-image-preview-container">';
        final+='<div class="cropit-image-preview"></div>';
        final+='</div>';
        final+='<div class="minImgSize">(Minimum image size: 640px X 480px)</div>';
        final+='<input type="range" class="cropit-image-zoom-input" />';
        final+='<div class="image-size-label">Move cursor to resize image</div>';
        final+='<input type="file" class="cropit-image-input" />';
        final+='<div class="select-image-btn">Pick Video Thumbnail</div>';
        final+='</div>';
        final+='<div>';
       
        final+='</li>';
        final+='</ul>';
        final+='<ul id="rightSide" class="ulImportVideoDetails">';
        final += '<li>';
        //final+='<span>Pick Provider</span>';
        final+='<div id="pickProvider">';
        //////URL///////////
       
            final += "<div id='videoProviderHolder' class='styled-selectSortSearch categoryKeyword'>";
            final += "<select id='ddlSelectProviderUrl' onchange='SetVideoProvider(this)'>";
            
            final += "</select>";
            final += "</div>";
            
           

        final += '</div>';
        final += "</li>";
        final += "<li>";
        final+='<div id="privateVideoDesc">';
        final += '<input id="txtPrivateVideoTitle" style="" type="text" placeholder="Enter Video Title" />';
        final += '</div>';
        final += "</li>";
        final += "<li>";
        final += '<input id="privateVideoUrl" onchange="SetProviderName(this)" style="" type="text" placeholder="Complete Video URL" />';
        final += "</li>";

        final += "<li>";
        final += "<div id='divUrlCategory'>";
        final += "<div id='ddlByUrlCategoryHolder' class='styled-selectSortSearch categoryKeyword'>";
        final += "<select id='ddlByUrlCategory' class='ddlCategory categoryPrVodeos ddlborder' ></select>";
        final += "</div>";
        final += "</div>";
        final += "</li>";
       
        final += "<li class='editTextArea'>";
        final+='<textarea style="" placeholder="Video Description"></textarea>';
        final += "</li>";
        final += "<li class='editModalcheckBoxHolder'>";
        final += '<div id="checkBoxHolder">';
        if (isMatureContentEnabeled)
        {
            final += '<input type="checkbox" id="checkMatureContentForm" />';
        }
        else
        {
            final += '<input type="checkbox" disabled id="checkMatureContentForm" />';
        }
        
        final+='<label for="checkMatureContentForm">Mature Content</label>';
        final += '<span id="spnMatureInfo" title="You can add mature content to the channel. To activate this option, please switch ALLOW MATURE CONTENT ON in the channel settings first. The video must be individually marked as Mature as well. The channel with mature content can only play outside of Strimm, on your own website." onclick="ShowSnippetPopup(this)">i</span>';
        final += '</div>';
 
        final += "</li>";
        final += "<li class='editModalvideoDurationHolder'>";
        final += '<div id="videoDurationHolder">';

        final+='<span>duration:</span>'
        final += '<input id="txtHour" placeholder="hh" type="number"  max="99"  />';
        final+='<span>:</span>';
        final += '<input type="number"  max="59" id="txtmMinutes" placeholder="mm" />';
        final+='<span>:</span>';
        final += '<input type="number"  max="59" id="txtmSeconds" placeholder="ss" />';
        final += '<span id="spnMatureInfo" title="Please enter an exact video duration in seconds, to avoid schedule interruption." onclick="ShowSnippetPopup(this)">i</span>';
        final+='</div>';
        final += '</li>';
        final += "<li class='editBtnAddPrivateVideoToChannel'>";
        final+='<a class="btnAddVideo export" onclick="AddPrivateVideoToChannel()">Add Video</a>'
            final+='</li>';
        final += ' </ul>';
        final += '</div>';

        return final;
    },

    BuildRegularImportVideoView: function(name, isYoutubeActive, isVimeoActive, isDmotionActive, isPro, isMatureContentEnabeled)
    {
        var final = "";
        final += "<div class='selectStep'>";
        final += "<span class='selectStepOPt'> 1 </span>";
        final += "</div>";


        //////URL///////////
        if (isYoutubeActive && isVimeoActive && isDmotionActive) {
            final += "<div id='videoProviderHolder' class='styled-selectSortSearch categoryKeyword'>";
            final += "<select id='ddlSelectProviderUrl' onchange='SetVideoProvider(this)'>";
            final += "<option value='0'>select provider</option>";
            final += "<option value='1'>youtube</option>";
            final += "<option value='2'>vimeo</option>";
            final += "<option value='3'>dailymotion</option>";
            final += "</select>";
            final += "</div>";
            final += "<div class='selectStepHolder'>";
            final += "<div class='selectStepInnerHolder'>";
            final += "<div class='selectStep'>";
            final += "<span class='selectStepOPt'> 2 </span>";
            final += "</div>";
            final += "</div>";
            final += "</div>";
            final += "<input type='text' id='txtURL' class='searchInput' placeholder='URL' style='float: left;'/>";
            final += "<input type='button' id='btnSearchByURL' class='btnImport' value='import' onclick='GetYouTubeVideoByURL()' style='float: left;'/>";

        }


        else if (isYoutubeActive && isVimeoActive && !isDmotionActive) {

            final += "<select id='ddlSelectProviderUrl'  onchange='SetVideoProvider(this)'>";
            final += "<option value='0'>select provider</option>";
            final += "<option value='1'>youtube</option>";
            final += "<option value='2'>vimeo</option>";
            final += "</select>";
            final += "<input type='text' id='txtURL' class='searchInput' placeholder='Import URL from YouTube' style='float: left;'/>";
            final += "<input type='button' id='btnSearchByURL' class='btnFirst' value='import' onclick='GetYoutubeVideosByUrl()' style='float: left;'/>";
        }

        else if (isYoutubeActive && !isVimeoActive && isDmotionActive) {

            final += "<select id='ddlSelectProviderUrl'  onchange='SetVideoProvider(this)'>";
            final += "<option value='0'>select provider</option>";
            final += "<option value='1'>youtube</option>";
            final += "<option value='3'>dailymotion</option>";
            final += "</select>";
            final += "<input type='text' id='txtURL' class='searchInput' placeholder='URL' style='float: left;'/>";
            final += "<input type='button' id='btnSearchByURL' class='btnFirst' value='import' onclick='GetYoutubeVideosByUrl()' style='float: left;'/>";
        }

        else if (!isYoutubeActive && isVimeoActive && isDmotionActive) {

            final += "<select id='ddlSelectProviderUrl'  onchange='SetVideoProvider(this)'>";
            final += "<option value='0'>select provider</option>";
            final += "<option value='2'>vimeo</option>";
            final += "<option value='3'>dailymotion</option>";
            final += "</select>";
            final += "<input type='text' id='txtURL' class='searchInput' placeholder='URL' style='float: left;'/>";
            final += "<input type='button' id='btnSearchByURL' class='btnFirst' value='import' onclick='GetYoutubeVideosByUrl()' style='float: left;'/>";
        }

        else if (isYoutubeActive && !isVimeoActive && !isDmotionActive) {

            final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration' onclick='DurationChanged()'></input>";

            final += "<span class='videoMinus'>videos &gt;20 min </span>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Enter Keywords' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='search' onclick='GetYoutubeVideosByUrl(1)' />";
        }
        else if (!isYoutubeActive && isVimeoActive && !isDmotionActive) {

            final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration' onclick='DurationChanged()'></input>";

            final += "<span class='videoMinus'>videos &gt;20 min </span>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Enter Keywords' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='search' onclick='GetYoutubeVideosByUrl(2)' />";
        }
        else if (!isYoutubeActive && !isVimeoActive && isDmotionActive) {

            final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration' onclick='DurationChanged()'></input>";

            final += "<span class='videoMinus'>videos &gt;20 min </span>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Enter Keywords' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='search' onclick='GetYoutubeVideosByUrl(3)' />";
        }


        final += "<div id='divUrlCategory' style='float: left;'>";
        final += "<div class='selectStep'>";
        final += "<span class='selectStepOPt'> 3 </span>";
        final += "</div>";
        final += "<div id='ddlByUrlCategoryHolder' class='styled-selectSortSearch categoryKeyword'>";
        final += "<select id='ddlByUrlCategory' class='ddlCategory ddlborder' onChange='SetCategoryUrl()'></select>";
        final += "</div>";
        final += "</div>";
        final += "</div>";
        final += "<div id='divShowImage'></div>";
        return final;
    },

    BuildAddVideosPopup: function (name, isYoutubeActive, isVimeoActive, isDmotionActive, isPro, isMatureContentEnable, showPrivateVideoMode) {
        void 0;
        void 0;
        var final = "<link href='/Plugins/EasyTabs/css/easytab.css' rel='stylesheet' />";
        final += "<script src='/Plugins/EasyTabs/js/jquery.hashchange.min.js' type='text/javascript'></script>";
        final += "<script src='/Plugins/EasyTabs/js/jquery.easytabs.min.js' type='text/javascript'></script>";
        final += "<link href='/css/VideoFinderPopUp.css' rel='stylesheet' />";

        final += "<div id='popUpWrapper'>";
        final += "<a class='closeBut' onclick='HideAddVideoPopup()'></a>";
        final += "<h1 class='videoFinderH1'>Add Videos to " + name + "</h1>";

        final += "<div id='tabsWrapper'>";
        final += "<div id='tab-container' class='tab-container'>";
        final += "<ul class='etabs'>";
        final += "<li class='tab searchKey' onclick='LoadSearchByKeywordsTab()' title='Search for publicly available, unrestricted videos directly on Strimm, by keywords.'>Search";
        //final += "<span class='YTicon closer'></span>";
        final += "</li>";
        final += "<li class='tab searchUrl' onclick='LoadSearchByUrlTab()' title='Copy YouTube URL of specific video and paste it here. If it is unrestricted, it can be added to your channel.'>Import</li>";
        //final += "<li class='tab publicLib' onclick='LoadSearchPublicLibraryTab()' title='Public Library is a collection of pre-selected and pre-categorized videos. Please remember that any Strimm user can add these videos to their channel as well.'>Public Library</li>";
        final += "<li class='tab searchVR' onclick='LoadSearchVideoRoomTab()' title='My Videos is your personal video library. All videos added by you to your account  are located here and can be added to any of your channels'>My Videos</li>";
        final += "</ul>";
       
        final += "</div>";
        
        final += "<div id='SearchPublicLibrary'>";
        final += "<div class='selectStepInnerHolder'>";
        final += "<div class='styled-selectSortSearch categoryKeyword'>";
        final += "<select id='ddlPublicLibraryCategory' class='ddlCategory ddlCategoryPopup ddlborder' onChange='ChangePublicLibraryCategoryHandler()'></select>";
        final += "</div>";
        final += "</div>";
        //final += "<div id='addVideoInstructionsBlock' class='addVideoInstructionsBlock'>Choose category and click '+' (appears on mouse over) to add video to your channel</div>";
        final += "<div id='divSearchVideobyKeywordForPublicLibHolder'>";
        //final += "<div class='searchAbsHolder'>";
        //final += "<input type='text' id='txtSearchVideoByKeywordForPublicLib' placeholder='Search for videos on this page' onkeyup='publicLibrarySearchInputKeyUp()'/>";
        //final += " <a id='btnClearSerachedVideosForPublicLib' onclick='ClearSearchedVideosForPublicLib()'>x</a>";
        //final += "<a id='btnSearchVideoByKeywordForPublicLib' onkeyup='GetPublicLibrary(true)'></a>";
        //final += " </div>";
        final += " </div>";
      
        final += "</div>";
       

        final += "<div id='SearchByKeyWords'>";
      
        final += "<div class='inputHolderKeywords'>";

        final += "<div class='selectStepHolder'>";
        final += "<div class='selectStepInnerHolder'>";
        final += "<div class='selectStep filterStep'>";
        final += "<span class='selectStepOPt'>filter</span>";
        final += "</div>";
        final += "<div class='stepWrapper'>";
        
        final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration'  onclick='DurationChanged()'></input>";
        
        final += "<span class='videoMinus'>Videos &gt;20 min </span>";
        final += "</div>";
        final += "</div>";
        final += "</div>";


        final += "<div class='selectStepHolder'>";
        final += "<div class='selectStepInnerHolder'>";
        final += "<div class='selectStep'>";
        final += "<span class='selectStepOPt'> 1 </span>";

        final += "</div>";
        if (isYoutubeActive && isVimeoActive && isDmotionActive) {
            final += "<div class='styled-selectSortSearch categoryKeyword'>";
            final += "<select id='ddlSelectProvider' onchange='SetVideoProvider(this)'>";
            final += "<option value='0'>select provider</option>";
            final += "<option value='1'>youtube</option>";
            final += "<option value='2'>vimeo</option>";
            final += "<option value='3'>dailymotion</option>";
            final += "</select>";
        final += "</div>";
            final += "</div>";
            final += "</div>";
           
       
        final += "<div class='selectStepHolder'>";
        final += "<div class='selectStepInnerHolder'>";
            final += "<div class='selectStep'>";
            final += "<span class='selectStepOPt'> 2</span>";
            final += "</div>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Search for videos by keyword' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='' onclick='GetVideoByKeyword()' />";
            final += "</div>";
            final += "</div>";

        }
        else if (isYoutubeActive && isVimeoActive && !isDmotionActive) {
            
            final += "<select id='ddlSelectProvider'  onchange='SetVideoProvider(this)'>";
            final += "<option value='0'>select provider</option>";
            final += "<option value='1'>youtube</option>";
            final += "<option value='2'>vimeo</option>";
            final += "</select>";
            final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration' onclick='DurationChanged()'></input>";
            final += "<span class='videoMinus'>videos &gt;20 min </span>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Enter Keywords' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='' onclick='GetVideoByKeyword()' />";
           
        }
        else if (isYoutubeActive && !isVimeoActive && isDmotionActive) {
            
            final += "<select id='ddlSelectProvider'  onchange='SetVideoProvider(this)'>";
            final += "<option value='0'>select provider</option>";
            final += "<option value='1'>youtube</option>";
            final += "<option value='3'>dailymotion</option>";
            final += "</select>";
            final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration' onclick='DurationChanged()'></input>";
            final += "<span class='videoMinus'>videos &gt;20 min </span>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Enter Keywords' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='' onclick='GetVideoByKeyword()' />";
           
        }
        else if (!isYoutubeActive && isVimeoActive && isDmotionActive) {

            final += "<select id='ddlSelectProvider'  onchange='SetVideoProvider(this)'>";
            final += "<option value='0'>select provider</option>"; 
            final += "<option value='2'>vimeo</option>";
            final += "<option value='3'>dailymotion</option>";
            final += "</select>";
            final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration' onclick='DurationChanged()'></input>";
            final += "<span class='videoMinus'>videos &gt;20 min </span>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Enter Keywords' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='' onclick='GetVideoByKeyword()' />";
           
        }
        else if (isYoutubeActive && !isVimeoActive && !isDmotionActive) {
            
            final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration' onclick='DurationChanged()'></input>";
            final += "<span class='videoMinus'>videos &gt;20 min </span>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Enter Keywords' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='' onclick='GetVideoByKeyword(1)' />";
        }
        else if (!isYoutubeActive && isVimeoActive && !isDmotionActive) {
            final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration' onclick='DurationChanged()'></input>";
            final += "<span class='videoMinus'>videos &gt;20 min </span>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Enter Keywords' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='' onclick='GetVideoByKeyword(2)' />";
        }
        else if (!isYoutubeActive && !isVimeoActive && isDmotionActive) {
            final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration' onclick='DurationChanged()'></input>";
            final += "<span class='videoMinus'>videos &gt;20 min </span>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Enter Keywords' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='' onclick='GetVideoByKeyword(3)' />";
        }
       
             
        final += "<div class='selectStepHolder'>";
        final += "<div class='selectStepInnerHolder'>";
        final += "<div id='divKeywordCategory' style='float: left; display:none; margin-left:0'>";
        final += "<div class='selectStep'>";
        final += "<span class='selectStepOPt'>3 </span>";
        final += "</div>";
        final += "<div class='styled-selectSortSearch categoryKeyword'>";
        final += "<select id='ddlByKeywordsCategory' class='ddlCategory ddlborder'/>";
        final += "</div>";
        final += "</div>";
        final += "</div>";
        final += "</div>";
        final += "</div>";
        final += "</div>";

        final += "<div id='SearchByUrl'>";
        //final += "<div class='radioHolder'>";
        final += "<div class='selectStepHolder'>";
        final += "<div class='SearchByUrlInnerHolder'>";
        final += "<input class='importURLradio' type='radio' name='radioImport' checked='checked' id='btnImportByUrl' onclick='ImportByUrl()' />";
        //final += "</div>";
        final += "<label class='importURLLable' for='btnImportByUrl'>Import by URL</label>";
        final += "</div>";
        final += "</div>";
        final += "<div class='selectStepHolder'>";
        //final += "<div class='SearchByUrlInnerHolder'>";
        //final += "<div class='radioHolder'>";
        //final += "<input class='importYourVideosradio'  type=radio name='radioImport' checked='checked' id='btnImportYoutubeUserUploads' onclick='ImportYoutubUserUploads()'/>";
        //final += "</div>";
        //final += "</div>";
        //final += "<label class='importYourVideosLable' for='btnImportYoutubeUserUploads'>Import all of your YouTube videos</label>";
        
        if (showPrivateVideoMode)
        {
            //if (isMatureContentEnable) {
            //    final += "<div class='checkMatureBoxHolder'>"
            //    final += "<input class='matureContent'  type='checkbox'  id='btnMatureContent' />";
            //    final += "</div>";
            //}
            //else {
            //    final += "<div class='checkMatureBoxHolder'>"
            //    final += "<input class='matureContent'  disabled  type='checkbox'  id='btnMatureContent' />";
            //    final += "</div>";
            //}
            //final += "<label class='lblMatureContent' for='btnMatureContent'>Include Mature Content</label>";

            final += "<div class='checkImportPrivateBoxHolder'>"
            final += "<input class='importPrivate'  type='checkbox'  id='btnImportPrivate' onchange='GetPrivateVideoView()' />";
            final += "<label class='lblImportPrivate' for='btnImportPrivate'>Import Private Video</label>";
            final += '<span id="spnMatureInfo" title="Add videos marked private or unlisted on YouTube, Vimeo and Dailymotion. Also, add direct video links from private servers, including mature content in allowed format. This option is available to subscribers and it is for embedded channels only" onclick="ShowSnippetPopup(this)">i</span>';
            final += "</div>";
           
        }
       

        final += "<div class='inputHolderUrl'>";
        final += "<div class='selectStepHolder'>";
        final += "<div class='SearchByUrlInnerHolder'>";
        final += "<div class='selectStep'>";
        final += "<span class='selectStepOPt'> 1 </span>";
        final += "</div>";
        final += "</div>";
       

        //////URL///////////
        if (isYoutubeActive && isVimeoActive && isDmotionActive) {
            final += "<div id='videoProviderHolder' class='styled-selectSortSearch categoryKeyword'>";
            final += "<select id='ddlSelectProviderUrl' onchange='SetVideoProvider(this)'>";
            final += "<option value='0'>select provider</option>";
            final += "<option value='1'>youtube</option>";
            final += "<option value='2'>vimeo</option>";
            final += "<option value='3'>dailymotion</option>";
            final += "</select>";
            final += "</div>";
            final += "</div>";
            final += "<div class='selectStepHolder'>";
            final += "<div class='SearchByUrlInnerHolder'>";
            final += "<div class='selectStep'>";
            final += "<span class='selectStepOPt'> 2 </span>";
            final += "</div>";
            final += "</div>";
            final += "<input type='text' id='txtURL' class='searchInput' placeholder='URL' style='float: left;'/>";
            final += "<input type='button' id='btnSearchByURL' class='btnImport' value='import' onclick='GetYouTubeVideoByURL()' style='float: left;'/>";
            final += "</div>";
        }


        else if (isYoutubeActive && isVimeoActive && !isDmotionActive) {

            final += "<select id='ddlSelectProviderUrl'  onchange='SetVideoProvider(this)'>";
            final += "<option value='0'>select provider</option>";
            final += "<option value='1'>youtube</option>";
            final += "<option value='2'>vimeo</option>";
            final += "</select>";
            final += "<input type='text' id='txtURL' class='searchInput' placeholder='Import URL from YouTube' style='float: left;'/>";
            final += "<input type='button' id='btnSearchByURL' class='btnFirst' value='import' onclick='GetYoutubeVideosByUrl()' style='float: left;'/>";
        }
       
        else if (isYoutubeActive && !isVimeoActive && isDmotionActive) {

            final += "<select id='ddlSelectProviderUrl'  onchange='SetVideoProvider(this)'>";
            final += "<option value='0'>select provider</option>";
            final += "<option value='1'>youtube</option>";
            final += "<option value='3'>dailymotion</option>";
            final += "</select>";
            final += "<input type='text' id='txtURL' class='searchInput' placeholder='URL' style='float: left;'/>";
            final += "<input type='button' id='btnSearchByURL' class='btnFirst' value='import' onclick='GetYoutubeVideosByUrl()' style='float: left;'/>";
           
        }
  
        else if (!isYoutubeActive && isVimeoActive && isDmotionActive) {

            final += "<select id='ddlSelectProviderUrl'  onchange='SetVideoProvider(this)'>";
            final += "<option value='0'>select provider</option>";
            final += "<option value='2'>vimeo</option>";
            final += "<option value='3'>dailymotion</option>";
            final += "</select>";
            final += "<input type='text' id='txtURL' class='searchInput' placeholder='URL' style='float: left;'/>";
            final += "<input type='button' id='btnSearchByURL' class='btnFirst' value='import' onclick='GetYoutubeVideosByUrl()' style='float: left;'/>";
        }

        else if (isYoutubeActive && !isVimeoActive && !isDmotionActive) {
            
            final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration' onclick='DurationChanged()'></input>";
          
            final += "<span class='videoMinus'>videos &gt;20 min </span>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Enter Keywords' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='search' onclick='GetYoutubeVideosByUrl(1)' />";
        }
        else if (!isYoutubeActive && isVimeoActive && !isDmotionActive) {
            
            final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration' onclick='DurationChanged()'></input>";
            
            final += "<span class='videoMinus'>videos &gt;20 min </span>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Enter Keywords' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='search' onclick='GetYoutubeVideosByUrl(2)' />";
        }
        else if (!isYoutubeActive && !isVimeoActive && isDmotionActive) {
           
            final += "<input id='chkIsLongDuration' class='chkGreenMarkStyle' type='checkbox' name='chkDuration' onclick='DurationChanged()'></input>";
           
            final += "<span class='videoMinus'>videos &gt;20 min </span>";
            final += "<input type='text' id='txtKeyword' onfocus='ClearTxtKeywordInput()' class='searchInput' placeholder='Enter Keywords' />";
            final += "<input type='button' id='btnSearchByKeyword' class='btnFirst' value='search' onclick='GetYoutubeVideosByUrl(3)' />";
        }
      
        final += "<div class='selectStepHolder'>";
        final += "<div id='divUrlCategory' style='float: left;'>";
        final += "<div class='selectStep'>";
        final += "<span class='selectStepOPt'> 3 </span>";
        final += "</div>";
        final += "<div id='ddlByUrlCategoryHolder' class='styled-selectSortSearch categoryKeyword'>";
        final += "<select id='ddlByUrlCategory' class='ddlCategory ddlborder' onChange='SetCategoryUrl()'></select>";
        final += "</div>";
        final += "</div>";
        final += "</div>";
        final += "</div>";
        final += "<div id='divShowImage'></div>";

        //import youtube 
        final += "<div class='selectStepHolder'>";
        final += "<a id='btnImportUserUploads' onclick='ImportUserUploads()'>Import Videos</a>";
        final += "</div>";
        final += "<div id='txtPopoupBlockigComment' class='txtPopoupBlockigComment'>* If you're unable to view the Google Login pop-up after clicking on the <strong>\"Import Videos\"</strong> button above, your browser may be configured to block all pop-up windows. Please update your pop-up browser settings to allow pop-ups from <a class='linkBlack'> www.strimm.com</a> and <a class='linkBlack'> www.google.com</a> and try again.</div>";
        final += "<div id='importUserYoutubeUploadsHolder'>";
        final += "<div id='stepImport1' class='selectStep'>";
        final += "<span class='selectStepOPt'> 1 </span>";
        final += "</div>";
        final += "<div id='ddlYoutubeUserUploadsCategoryHolder' class='styled-selectSortSearch categoryKeyword'>";
        final += "<select id='ddlYoutubeUserUploadsCategory' class='ddlCategory ddlborder' ></select>";
        final += "</div>";
        final += "<div id='stepImport2'  class='selectStep'>";
        final += "<span class='selectStepOPt'> 2 </span>";
        final += "</div>";
        final += "<div id='importUserYoutubeVideosActionHolder'>";
        final += "<a id='btnAddAllToChannel' class='addAll' onclick='addAllImportedYoutubeVideosToChannel()'>Add all videos to channel</a>";
        final += "<div class='g-signin2' data-onsuccess='onSignIn'></div>";   
        final += "</div>";
        //final += "<div id='userYoutubeUploadsContent'></div>"
        final += "</div>";
        final += "</div>";
        //final += "<div id='addVideoInstructions' class='addVideoInstructions' style='display:none;'>Choose category and click '+' (appears on mouse over) to add video to your channel</div>";
        final += "<div id='SearchVideoRoom'>";

        if (isPro == true) {
            final += "   <div class='videosSorting'>";
            final += "<div class='videosSortingOpt'><input type='checkbox' id='chkShowMyVideos' class='chkGreenMarkStyle' onclick='VideoSearchCriteriaChanged()'><label class='videoMinus'>My Videos</label></div>";
            final += "<div class='videosSortingOpt'><input type='checkbox' id='chkShowLicensedVideos' class='chkGreenMarkStyle' onclick='VideoSearchCriteriaChanged()'><label class='videoMinus'>Licensed Videos</label></div>";
            final += "<div  class='videosSortingOpt'><input type='checkbox' id='chkShowExternalProvidersVideos' class='chkGreenMarkStyle' onclick='VideoSearchCriteriaChanged()'><label class='videoMinus'>YouTube & Vimeo</label></div>";
            final += "   </div>";
        }

        final += "   <div id='divSearchVideobyKeywordForVideoRoomHolder'>";
        final += "	    <div class='searchAbsHolder'>";
        final += "		    <input type='text' id='txtSearchVideoByKeywordForVideoRoom' placeholder='Search for videos on this page' onkeyup='videoRoomSearchInputKeyUp()'/>";
        final += " 		    <a id='btnClearSerachedVideosForVideoRoom' onclick='ClearSearchedVideosForVideoRoom()'>x</a>";
        final += "		    <a id='btnSearchVideoByKeywordForVideoRoom' onclick='LoadVideosFromVideoRoom(true)'></a>";
        final += " 	    </div>";
        final += "   </div>";
        final += "<div class='ddlVideoRoomCategoryHolder'>";
        final += "<div class='selectStepHolder'>";
        final += "<div class='SearchByUrlInnerHolder'>";
        final += "<div class='styled-selectSortSearch categoryKeyword'>";
        final += "       <select id='ddlVideoRoomCategory' class='ddlCategory ddlborder' onChange='ChangeVideoRoomCategoryHandler()'></select>";
        final += "</div>";
        final += "</div>";
        final += "</div>";
        final += "</div>";
        final += "</div>";

        final += "</div>";

        final += "<div id='addVideoInstructions' class='addVideoInstructions'>Choose category and click '+' (appears on mouse over) to add video to your channel</div>";
        final += "<div class='loadedContent'>";
       
       
        final += "<div/>"
        final += "</div>";
        final += "<div class='loadMoreBtnHolder'>";
        final += "<input type='button' id='loadMoreVideos' class='classloadMoreVideos' value='Load More' onclick='LoadMoreResults()' />";
        final += "</div>";
        //final += "<div id='divTakeTourGetVideo'>";
        //final += "<a id='ancTakeTourGetVideos'> take a tour </a>";
        //final += "</div>";
        final += "<div class='styled-selectSort sortAbsolutePL'>";
        final += "<div class='sort'>";
        final += "<select id='sortSelect' onchange='sortVideosForAdd(this)'>";
        final += "<option value='0'>sort results</option>";
        final += "<option value='1'>longer to shorter</option>";
        final += "<option value='2'>shorter to longer</option>";
        final += "<option value='3'>most viewed</option>";
        final += "<option value='4'>recently added</option>";
        final += "</select>";
        final += "</div>";
        final += "</div>";
        final += "</div>";
        final += "<div id='durationPopup' style='display:none;'>";
        final += "<div id='durationPopupWrapper'>";
        final += "<a id='close_x' class='close close_x' href='#' onclick='ClearAndCloseDurationPopup()'>X</a>";
        final += "<h1 class='videoFinderH1'>please add duration for this video</h1>";
        final += "<div class='durationInputsHolder'>";
        final += "<input id='customDurationHour' type='text' placeholder='hour (up to 24)'/>";
        final += "<input id='customDurationMinutes' type='text' placeholder='minutes (up to 60)'/>";
        final += "<input id='customDurationSeconds' type='text' placeholder='seconds (up to 60)'/>";
        final += "</div>";
        final += "<a id='proceedAddVideo' >ok</a>";
        final += "</div>";
        final += "</div>";

        final += "</div>";


        return final;
    },

    BuildVideoBoxControlForWatchItLaterPage: function (videos, justAdded) {
        var final = "";

        $.each(videos, function (i, d) {
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
            var thumbnail = d.ThumbnailUrl;
            var durationString = d.DurationString;
            var isInChannel = d.IsInChannel;
            var allowDelete = true;
            var dateAdded = d.DateAdded;
            var isExternalVideo = videoId == 0;
            var originDomain = d.OriginDomain;
            var providerName = d.VideoProviderName;

            if (providerName != undefined && providerName != null) {
                providerName = providerName.toLowerCase();
            }

            if (thumbnail == null || thumbnail == "") {
                thumbnail = "/images/comingSoonBG.jpg";
            }

            final += "<div class='videoBoxNew' data-name='" + title.toLowerCase() + "' id='boxContent_" + videoId + "' date-added='" + dateAdded + "'>" +
                        "<div class='actionsVideoBoxHolder' id='action_" + videoId + "'>";

            if (isRemoved) {
                final += "<div class='videoRemoveMessage' id='" + providerVideoId + "'>removed by provider</div>";
            }
            else if (isRestricted) {
                final += "<div class='videoRemoveMessage' id='" + providerVideoId + "'>restricted by provider</div>";
            }
            else {
                if (providerName == "vimeo" || providerName == "youtube" || providerName == "dailymotion") {
                final += "<div class='VideoBoxPlay' data-boxContentId='" + videoId + "' id='" + providerVideoId + "' onclick='showPlayer(this)'></div>";
                }
                else {
                    final += "<div class='VideoBoxPlay' id='" + providerVideoId + "' onclick='ShowStrimmPlayer(\"" + FormatUrl(originDomain, d.VideoKey, true) + "\")'></div>";
                }

                if (justAdded) {
                    final += "<span class='recentlyadded'>just in</span>";
                }
            }

            if (descripton && descripton != "") {
                descripton = Controls.EscapeHtml(descripton);
            }

            final += "<div class='VideoBoxRemove' id='remove_" + videoId + "' onclick='ShowModalRemove(" + videoId + ")'></div>" +
                    "</div> <div class='videoBoxOverlay'> </div>" +
                    "<div class='addButtonContainer' style='background-image: url(" + thumbnail + ");'>" +
                    "</div>" +
                    "<div class='videoBoxInfo'>" +
                        "<span class='videoBoxtitle ' title='" + title + "'>" + title + "</span>" +
                        "<span class='videoBoxviews'>views:<strong>" + viewCounter + "</strong></span>" +
                        "<span class='videoBoxduration'><strong>" + durationString + "</strong></span>" +
                        "<input type='hidden' value='" + durationInSec + "' class='durationHidden'/>" +
                        "<input type='hidden' value='" + descripton + "' class='descriptionHidden'/>" +
                        "<span class='videoBoxcateg'><strong>" + categoryName + "</strong></span>" +
                        "<input type='hidden' value='" + videoId + "' class ='hidden'/>" +
                    "</div>" +
                "</div>";
        });

        return final;
    },

    BuildViodeBoxControlForPublicLibraryAdmin: function (videos) {
        var final = "";

        $.each(videos, function (i, d) {
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
            var thumbnail = d.ThumbnailUrl;
            var durationString = d.DurationString;
            var isInChannel = d.IsInChannel;
            var allowDelete = true;
            var dateAdded = d.DateAdded;
            var isExternalVideo = videoId == 0;
            var isLiveVideo = d.IsLive;
            if (thumbnail == null || thumbnail == "") {
                thumbnail = "/images/comingSoonBG.jpg";
            }

            final += "<div class='videoBoxNew' data-duration='" + durationInSec + "' data-views='" + viewCounter;

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
                     "<div class='actionsVideoBoxHolder' id='action_" + videoId + "'>";


            if (isRemoved) {
                final += "<div class='videoRemoveMessage'>removed by provider</div>";
            }
            else if (isRestricted) {
                final += "<div class='videoRemoveMessage'>restricted by provider</div>";
            }
            else {
                final += "<div class='VideoBoxPlay' data-boxContentId='" + videoId + "' id='" + providerVideoId + "' onclick='showPlayer(this)'></div>";
                if (isInChannel) {
                   // final += "<span class='recentlyadded'>in channel</span>";
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

           
                final += "<a id='remove_" + videoId + "' class='VideoBoxRemove' onclick='RemoveVideoFromPublicLibrary(this)'></a>";
           

            final += "</div>";
            final += "<div class='videoBoxOverlay'>";
            final += "</div>";

            if (!isRemoved && !isRestricted) {
                if (isExternalVideo) {
                    
                    final += "<div class='addButtonContainer' id='addButtonHolderId_" + providerVideoId + "' style='background-image: url(" + thumbnail + ");'>";
                  // final += "<div class='addButtonHolder' id='addvideosId_" + providerVideoId + "' onclick='addExternalVideoToChannel(this)' ></div>";
                }
                else {
                    final += "<div class='addButtonContainer' id='addButtonHolderId_" + videoId + "' style='background-image: url(" + thumbnail + ");'>";
                    if (!isInChannel) {
                       // final += "<div class='addButtonHolder' id='addvideosId_" + videoId + "' onclick='addVideoToChannel(this)' ></div>";
                    }
                }
            }
            else {
                if (isExternalVideo) {
                    final += "<div class='addButtonContainer' id='addButtonHolderId_" + providerVideoId + "' style='background-image: url(" + thumbnail + ");'>";
                }
                else {
                    final += "<div class='addButtonContainer' id='addButtonHolderId_" + videoId + "' style='background-image: url(" + thumbnail + ");'>";
                }
            }

            if (descripton && descripton != "") {
                descripton = Controls.EscapeHtml(descripton);
            }

            final += "</div>" +
                    "<div class='videoBoxInfo'>" +
                        "<span class='videoBoxtitle ' title='" + title + "'>" + title + "</span>" +
                        "<span class='videoBoxviews'>views:<strong>" + viewCounter + "</strong></span>" +
                        "<span class='videoBoxduration'><strong>" + durationString + "</strong></span>" +
                        "<input type='hidden' value='" + durationInSec + "' class='durationHidden'/>" +
                        "<input type='hidden' value='" + descripton + "' class='descriptionHidden'/>" +
                        "<span class='videoBoxcateg'><strong>" + categoryName + "</strong></span>" +
                        "<input type='hidden' value='" + videoId + "' class ='hidden'/>" +
                    "</div>" +
                "</div>";
        });

        return final;
    },

};

function ShowRepeatSchedule(element) {
    var stringId = element.id;
    var idArr = stringId.split("_");
    var id = idArr[1];
   
    if ($("#schedulecontent_" + id + " .repeatBox").is(":visible")) {
        $("#schedulecontent_" + id + " .repeatBox").hide();      
    }   
    else {
        $("#schedulecontent_" + id + " .repeatBox").show();
        ShowCalendar(id);    
    }
}

function ShowCalendar(id) {
    var repeatPic = $("#schedulecontent_" + id + " .txtMonth");

    var minDateInst = new Date();
    minDateInst.setMinutes(minDateInst.getMinutes() + 15);
    var priorDateSelectionInst = null;
    repeatPic.datetimepicker({
        numberOfMonths: 1,
        minDate: minDateInst,
        onSelect: function (selectedDate) {
            var picDate = repeatPic.datetimepicker('getDate');
            var dayspan = picDate.getDay() - priorDateSelectionInst.getDay();
            if (dayspan > 0) {
                picDate.setHours(0);
                picDate.setMinutes(0);
                repeatPic.datetimepicker('setDate', picDate);
            }
            priorDateSelectionInst = picDate;
        },
        dateFormat: 'mm/dd/yy',
        closeText: "Ok"

        // maxDate: 30
    });


    repeatPic.click(function () {

        var selection = repeatPic.datetimepicker('getDate');
        if (selection == null) {
            repeatPic.datetimepicker('setDate', minDateInst);
            priorDateSelectionInst = minDateInst;
        }
        else {
            priorDateSelectionInst = selection;
        }
    });
};


function FormatUrl(originDomain, key, isVideo) {
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
}

