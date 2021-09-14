(function () {

    var GetVideoDurationService = "/WebServices/VideoUploadWebService.asmx/GetVideoDuration";
    var GetVideoPreviewClipService = "/WebServices/VideoUploadWebService.asmx/GetVideoPreviewClip";
    var GetCustomVideoThumbnailService = "/WebServices/VideoUploadWebService.asmx/GetCustomVideoThumbnail";
    var GetVideoThumbnailsService = "/WebServices/VideoUploadWebService.asmx/GetVideoThumbnails";
    var SubmitVideoForTranscodingService = "/WebServices/VideoUploadWebService.asmx/SubmitVideoForTranscoding";
    var InitializeCustomVideoTubeUploadForUser = "/WebServices/VideoUploadWebService.asmx/InitializeCustomVideoTubeUploadForUser"
    var UpdateUploadedVideoService = "/WebServices/VideoUploadWebService.asmx/UpdateUploadedVideo";
    var LoadVideoRoomCategoriesService = "/WebServices/VideoRoomService.asmx/GetVideoCategoriesForVideoRoom";
    var GetAllVideoTubePoByPageIndexAndUserIdService = "/WebServices/VideoRoomService.asmx/GetAllVideoTubePoByPageIndexAndUserId";
    var GetAllCategoriesService = "/WebServices/VideoRoomService.asmx/GetAllCategories";
    var GetCustomVideoTubeByIdService = "/WebServices/VideoRoomService.asmx/GetCustomVideoTubeById";
    var RemoveVideoFromVideoRoomService = "/WebServices/VideoRoomService.asmx/DeleteVideoTubeById";

    var key;
    var uploadedFileName;
    var uploadedVideoPath;
    var customVideo;
    var userId;
    var ddlCategories;
    var selectedCategoryId;
    var shouldLoadAllVideos;
    var shouldLoadMyVideos;
    var shouldLoadLicensedVideos;
    var shouldLoadExternalVideos;
    var pageIndex = 1;
    var prevPageIndex = 1;
    var nextPageIndex = 1;
    var customVideoFile;
    var awsAccessKey;
    var awsPolicy;
    var awsSignature;
    var awsS3UploadBucket;    
    var progressBarValue;
    var customVideoThumbnail;
    var defaultThumbnailImage = '/images/comingSoonBG.jpg';
    var cloudfrontWebDomain;
    var cloudfrontRtmpDomain;
    var cloudfrontStagingDomain;

    var VideoRoom = {
       
        init: function (uid, cfWeb, cfRtmp, cfStaging) {
            userId = uid;
            cloudfrontWebDomain = cfWeb;
            cloudfrontRtmpDomain = cfRtmp;
            cloudfrontStagingDomain = cfStaging;

            ddlCategories = $(".ddlVrCategory");

            shouldLoadMyVideos = true;
            shouldLoadLicensedVideos = true;
            shouldLoadExternalVideos = true;

            $('#chkShowMyVideos').prop('checked', shouldLoadMyVideos);
            $('#chkShowExternalProvidersVideos').prop('checked', shouldLoadLicensedVideos);
            $('#chkShowLicensedVideos').prop('checked', shouldLoadExternalVideos);

            selectedCategoryId = 0;

            this.loadCategories();
            this.videoSearchCriteriaChanged();
        },

        loadCategories: function() {
            $.ajax({
                type: "POST",
                url: LoadVideoRoomCategoriesService,
                cashe: false,
                data: '{"userId":' + userId + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    var categories = response.d;
                    if (categories) {
                        $.each(categories, function (i, c) {
                            $('.ddlVrCategory').append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
                            if (i == 0) {
                                $('.ddlVrCategory option[value="' + 0 + '"]').prop('selected', true);
                }
            });

                        if (selectedCategoryId === undefined) {
                            selectedCategoryId = 0;
                        }
                    }
                },
                complete: function (response) {
            }
            });

        },

        videoSearchCriteriaChanged: function () {
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

            this.getMoreVideos();
        },

        getMoreVideos: function (resetPaging) {
            var keywords = $('#txtVideoSearchKeywords').val();

            if (resetPaging) {
                pageIndex = 1;
                prevPageIndex = 1;
                nextPageIndex = 1;
            }

            var searchCriteria = {
                PageIndex : pageIndex,
                UserId : userId,
                CategoryId: selectedCategoryId,
                RetrieveMyVideos : shouldLoadMyVideos,
                RetrieveLicensedVideos : shouldLoadLicensedVideos,
                RetrieveExternalVideos : shouldLoadExternalVideos,
                VideoContentKeywords: keywords
            };

            $.ajax({
                type: "POST",
                url: GetAllVideoTubePoByPageIndexAndUserIdService,
                cashe: false,
                data: "{'searchCriteria':" + JSON.stringify(searchCriteria) + "}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    showLoadingDiv(true);
                },
                success: function (response) {
                    var data = response.d;

                    $("#divVideosHolder").html("");

                    if (data) {
                        pageIndex       = data.NextPageIndex;
                        prevPageIndex   = data.PrevPageIndex;
                        nextPageIndex   = data.NextPageIndex;

                        var pageSize    = data.PageSize;
                        var pageCount   = data.PageCount;

                        if (data.VideoTubeModels) {
                            var videos = data.VideoTubeModels;
                            var videoControls = Controls.BuildVideoBoxControlForVideoRoomPage(videos, userId);
                            $("#divVideosHolder").append(videoControls);

                            if (pageIndex == nextPageIndex) {
                                $('#divLoadMore').hide();
                            }
                        }
                    }
                    else {
                        $("html, body").animate({ scrollTop: $(document).height() }, "fast");

                        if (startIndex == 0) {
                            $("#divVideosHolder").html("<span id='lblMessage'>You do not have videos in Video Room yet</span>");
                        }
                    }

                    showLoadingDiv(false);
        }
            });        
        },

        selectedCategoryChanged: function() {
            selectedCategoryId = $("#ddlVrCategory option:selected").val();

            this.getMoreVideos();
        },

        sortVideos: function (selectCtrl) {
            var allvideoboxes = $("#divVideosHolder").find("div.vrVideoBox");
            var wrapper = $('#divVideosHolder');

            var value;
            var selectedValue = selectCtrl.value;
            if (selectedValue !== null && selectedValue !== undefined) {
                value = selectedValue;
            }
            else {
                value = 0;
        }

            sortDisplayedVideos(allvideoboxes, value, wrapper);
        },

        uploadNewVideo: function () {
            showVideoUploadPopup();
        },

        cancelVideoChanges: function () {
            $("#uploadVideoPopup").hide();
            RemoveOverlay();

            customVideo = null;
        },

        editVideo: function (btn) {
            if (btn) {
                var stringId = btn.id;
                var idArr = stringId.split("_");
                var id = idArr[1];

        $.ajax({
            type: "POST",
                    url: GetCustomVideoTubeByIdService,
            cashe: false,
                    data: "{'videoTubeId':" + id + "}",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                        showLoadingDiv(true);
                    },
                    success: function (response) {
                        var data = response.d;
                        
                        if (data) {
                            if (data.IsSuccess) {
                                customVideo = data.Response;
                                showVideoUploadPopup(customVideo);
                            }
                            else {
                                alertify.error("Server error. Failed to retrieve specified video");
                            }
                        }
                        else {
                            alertify.error("Server error. Failed to retrieve specified video");
                        }

                        showLoadingDiv(false);
                    }
                });
            }
            else {
                alertify.error("Server error. Failed to retrieve specified video");
            }
        },

        deleteVideo: function (btn) {
            if (btn) {
                var stringId = btn.id;
                var idArr = stringId.split("_");
                var id = idArr[1];

                $.ajax({
                    type: "POST",
                    url: RemoveVideoFromVideoRoomService,
                    cashe: false,
                    data: '{"userId":' + userId + ',"videoTubeId":' + id + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
                    beforeSend: function () {
                        showLoadingDiv(true);
                    },
            success: function (response) {
                        var data = response.d;

                        if (data) {
                            if (data.IsSuccess) {
                                customVideo = null;
                                $("#boxContent_" + id).hide();
                                alertify.success(data.Response);
                }
                            else {
                                alertify.error(data.Response);
            }
                        }
                        else {
                            alertify.error("Server error. Failed to delete specified video");
                        }
                    }
        });
    }
            else {
                alertify.error("Server error. Failed to retrieve specified video");
            }
        },

        showVideo: function (btn,provider) {
            if (btn) {
                var videoId = $('#' + btn.id).data('video-id');

                if (provider === 'youtube') {
                    ShowYouTubePlayer(videoId);
                }
                else if (provider === 'vimeo') {
                    ShowVimeoPlayer(videoId);
                }
                else if (provider === 'strimm') {
                    ShowStrimmPlayer(videoId);
                }
            }
        }
    };

    var VideoUploader = {

        init: function (access_key, policy, signature, s3UploadUrl) {
            awsAccessKey = access_key;
            awsPolicy = policy;
            awsSignature = signature;
            awsS3UploadBucket = s3UploadUrl;
        },

        onFileSelected: function () {
            customVideoFile = document.getElementById('btnBrowseFile').files[0];
            var allowUpload = false;
            if (customVideoFile) {
                allowUpload = true;
            }
            $("#btnUpload").prop("disabled", !allowUpload);
        },

        uploadVideo: function () {
            showUserNotificationArea('progressbar');

            var fd = new FormData();

            uploadedFileName = (new Date).getTime() + '-' + customVideoFile.name;
            key = stagingFolder + "/" + uploadedFileName;
            uploadedVideoPath = s3UploadUrl + key;

            fd.append('key', key);
            fd.append('acl', 'public-read');
            fd.append('Content-Type', customVideoFile.type);
            fd.append('AWSAccessKeyId', awsAccessKey);
            fd.append('policy', awsPolicy);
            fd.append('signature', awsSignature);
            fd.append("file", customVideoFile);

            var xhr = new XMLHttpRequest();

            xhr.upload.addEventListener("progress", uploadToS3Progress, false);
            xhr.addEventListener("load", uploadToS3Complete, false);
            xhr.addEventListener("error", uploadToS3Failed, false);
            xhr.addEventListener("abort", uploadToS3Canceled, false);

            $("#progressBar").progressbar({ value: 0 });

            //MUST BE LAST LINE BEFORE YOU SEND 
            xhr.open('POST', 'https://' + awsS3UploadBucket, true);
            xhr.send(fd);
        },

        updateVideo: function () {
            var jsonCustomVideo = updateVideoModelFromUI();

            $.ajax({
                type: "POST",
                url: UpdateUploadedVideoService,
                data: '{"videoModel":' + jsonCustomVideo + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d) {
                        var data = response.d;
                        if (data) {
                            if (data.IsSuccess) {
                                VideoRoom.cancelVideoChanges();
                                VideoRoom.getMoreVideos();
                                alertify.success("Video record was successfully updated.")
                            }
                            else {
                                alertify.error(data.Response);
                            }
                        }
                        else {
                            showUserNotificationArea('errormessage', 'Video update failed. Please try again later');
                        }
                    }
                },
                async: true,
                error: function (response) {
                    showUserNotificationArea('errormessage', 'Video update failed. Please try again later');
            }
            });
        },

        videoCategoryChanged: function () {
            if (customVideo) {
                customVideo.CategoryId = $("#ddlVideoCategory option:selected").val();
            }
        },

        selectFirstThumbnailAsActive: function () {
            if (customVideo) {
                customVideo.ActiveThumbnailKey = customVideo.FirstThumbnailKey;
            }
        },

        selectSecondThumbnailAsActive: function () {
            if (customVideo) {
                customVideo.ActiveThumbnailKey = customVideo.SecondThumbnailKey;
            }
        },

        selectThirdThumbnailAsActive: function () {
            if (customVideo) {
                customVideo.ActiveThumbnailKey = customVideo.ThirdThumbnailKey;
            }
        },

        getCustomThumbnail: function () {
            $("#progressLabel").text("Extracting custom video thumbnail....");

            var userHrs = $("#customThumbnailHrs").val();
            var userMin = $("#customThumbnailMin").val();
            var userSec = $("#customThumbnailSec").val();

            var timeframeHrs = 0;
            var timeframeMin = 0;
            var timeframeSec = 0;

            if (userHrs) {
                timeframeHrs = userHrs.trim();
            }

            if (userMin) {
                timeframeMin = userMin.trim();
            }

            if (userSec) {
                timeframeSec = userSec.trim();
            }

            var timeframeInSec = timeframeHrs * 60 * 60 + timeframeMin * 60 + timeframeSec;

            if (timeframeInSec > customVideo.Duration) {
                showUserNotificationArea('errormessage', "Entered timeframe for a custom thumbnail exceed video duration of '" + customVideo.Duration + "' sec.");
        }
            else {
                $.ajax({
                    type: "POST",
                    url: GetCustomVideoThumbnailService,
                    data: '{"videoTubeId":' + customVideo.VideoTubeId + ',"timeFrameInSec":' + timeframeInSec + '}',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response.d) {
                            var data = response.d;

                            if (data.IsSuccess) {
                                customVideoThumbnail = data.Response;
                                $("#imgCustomThumbnail").prop('src', 'https://' + cloudfrontWebDomain + '/' + customVideoThumbnail);
                                clearCustomThumbnailTimeframe();
                            }
                            else {
                                showUserNotificationArea('errormessage', data.Message);
                            }
                        }
                    },
                    async: true,
                    error: function (response) {
                        showUserNotificationArea('errormessage', 'Error occured while retrieving custom video thumbnail. Please try again later');
                    }
                });
            }
        },

        setCustomThumbnailAsActive: function () {
            if (customVideo) {
                if (customVideo.FirstThumbnailKey === customVideo.ActiveThumbnailKey) {
                    customVideo.FirstThumbnailKey = customVideoThumbnail;
                }
                else if (customVideo.SecondThumbnailKey === customVideo.ActiveThumbnailKey) {
                    customVideo.SecondThumbnailKey = customVideoThumbnail;
                }
                else {
                    customVideo.ThirdThumbnailKey = customVideoThumbnail;
                }

                customVideo.ActiveThumbnailKey = customVideoThumbnail;
            }

            refreshThumbnails();
            clearCustomThumbnailControls();
        },

        playVideoPreviewClip: function () {
            if (customVideo) {
                var popupDialog = $('#uploadVideoPopup');
                if (popupDialog) {
                    popupDialog.hide();
                    ShowStrimmPlayer('//' + cloudfrontWebDomain + '/' + customVideo.VideoPreviewKey, 0, function () {
                        popupDialog.show();
                    });
                }
            }
        },

        privacyFlagChanged: function () {
            var privacy = $('input[name=rdPrivacy]:checked').val();
            var isPublic = true;

            if (privacy == 'private') {
                isPublic = false;
            }

            if (customVideo) {
                customVideo.IsPrivate = !isPublic;
            }
        }
    };

    showUserNotificationArea = function (area, message) {
        $("#divFileUploadHolder").hide();
        $("#divErrorMessage").hide();
        $("#divExistingFileHolder").hide();
        $("#progressBar").hide();

        if (area == 'existingfile') {
            $("#divExistingFileHolder").show();
        }
        else if (area == 'progressbar') {
            $("#progressBar").show();
        }
        else if (area == 'errormessage') {
            $("#spnErrorMessage").empty().val(message);
            $("#divErrorMessage").show();
        }
        else {
            $("#divFileUploadHolder").show();
        }
    }

    loadVideoCategoriesForCustomVideo = function () {
        $('#ddlVideoCategory').empty();

        $.ajax({
            type: "POST",
            url: GetAllCategoriesService,
            cashe: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var categories = response.d;
                if (categories) {
                    $.each(categories, function (i, c) {
                        if (customVideo) {
                            if (customVideo.CategoryId != c.CategoryId) {
                                $('#ddlVideoCategory').append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
                            }
                            else {
                                $('#ddlVideoCategory').append($('<option selected></option)').attr("value", c.CategoryId).text(c.Name));
                            }
                        }
                        else {
                            if (i > 0) {
                                $('#ddlVideoCategory').append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
                            }
                            else {
                                $('#ddlVideoCategory').append($('<option selected></option)').attr("value", c.CategoryId).text(c.Name));
                            }
                        }
                    });
                }
            },
            complete: function (response) {
            }
        });
    }

    showVideoUploadPopup = function (existingVideo) {
        $("#uploadVideoPopup").lightbox_me({
            centered: true,
            onLoad: function () {
                initUploadVideoPopup(existingVideo);
            },
            onClose: function () {
                RemoveOverlay();
            },
            closeSelector: "close"
        });
    }

    getVideoRoom = function () {
        return VideoRoom;
    };

    getVideoUploader = function () {
        return VideoUploader;
    };

    showLoadingDiv = function (shouldShow) {
        if (shouldShow) {
            $("#loadingDiv").show();
        }
        else {
            $("#loadingDiv").hide();
                }
    };

    sortDisplayedVideos = function (videoBoxes, sortType, wrapper) {
        switch (sortType) {
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
        };
    };

    initUploadVideoPopup = function (existingVideo) {
        loadVideoCategoriesForCustomVideo();

        if (existingVideo) {
            showUserNotificationArea('existingfile');
             
            if (existingVideo.Title != 'Not Set' && existingVideo.Title != '' && existingVideo.Title != null) {
                $("#txtVideoTitle").val(existingVideo.Title);
            }

            $("#txtVideDescription").val(existingVideo.Description);
            $("#txtVideoKeywords").val(existingVideo.Keywords);
            $("#txtRentalFeeAmount").val(existingVideo.RentalFee);

            $('#chkPrivate').prop('checked', existingVideo.IsPrivate);
            $('#chkPublic').prop('checked', !existingVideo.IsPrivate);
            $('#chkIsRRated').prop('checked', existingVideo.IsRRated);

            // if video link points to the file in staging area than its url
            // will start with https://, otherwise it will be represented by the key which
            // means we would need to add a cloudfront domain info to it.
            var videoLink = existingVideo.VideoKey;
            var pattern = /^((http|https|ftp):\/\/)/;

            if (!pattern.test(videoLink)) {
                videoLink = "//" + cloudfrontWebDomain + videoLink;
            }

            $('#divCustomVideoLink').attr('href', videoLink);
            $('#divVideoPreviewHolder').attr('href', "//" + cloudfrontWebDomain + '/' + existingVideo.VideoPreviewKey);

            $('#imgFirstThumbnail').prop('src', "https://" + cloudfrontWebDomain + '/' + existingVideo.FirstThumbnailKey);
            $('#imgSecondThumbnail').prop('src', "https://" + cloudfrontWebDomain + '/' + existingVideo.SecondThumbnailKey);
            $('#imgThirdThumbnail').prop('src', "https://" + cloudfrontWebDomain + '/' + existingVideo.ThirdThumbnailKey);
        }
        else {
            $('#btnBrowseFile').replaceWith($("#btnBrowseFile").clone(true));

            showUserNotificationArea('fileupload');
           
            $("#txtVideoTitle").val('');
            $("#txtVideDescription").empty();
            $("#txtVideoKeywords").empty();
            $("#txtRentalFeeAmount").empty();

            $('#divCustomVideoLink').removeAttr('href');
            $('#divVideoPreviewHolder').removeAttr('href');

            $('#chkPublic').prop('checked', false);
            $('#chkPrivate').prop('checked', false);
            $('#chkIsRRated').prop('checked', false);

            $("#divLeftSideHolder").children().prop('disabled', true);
            $("#divRightSideHolder").children().prop('disabled', true);
            $("#divUpdateVideo").prop('disabled', true);

            $('#imgFirstThumbnail').prop('src', defaultThumbnailImage);
            $('#imgSecondThumbnail').prop('src', defaultThumbnailImage);
            $('#imgThirdThumbnail').prop('src', defaultThumbnailImage);
        }

        clearCustomThumbnailControls();
    };

    clearCustomThumbnailControls = function () {
        $('#imgCustomThumbnail').prop('src', defaultThumbnailImage);

        clearCustomThumbnailTimeframe();
    };

    clearCustomThumbnailTimeframe = function () {
        $('#customThumbnailHrs').val('0');
        $('#customThumbnailMin').val('0');
        $('#customThumbnailSec').val('0');
    };

    refreshThumbnails = function () {
        $('#imgFirstThumbnail').prop('src', "https://" + cloudfrontWebDomain + '/' + customVideo.FirstThumbnailKey);
        $('#imgSecondThumbnail').prop('src', "https://" + cloudfrontWebDomain + '/' + customVideo.SecondThumbnailKey);
        $('#imgThirdThumbnail').prop('src', "https://" + cloudfrontWebDomain + '/' + customVideo.ThirdThumbnailKey);
    };

    uploadToS3Progress = function (evt) {
        if (evt.lengthComputable) {
            var progressPercent = Math.round(evt.loaded * 100 / evt.total);
            $("#progressBar").progressbar({
                value: progressPercent
            });
            if (progressPercent < 100) {
                $("#progressLabel").text(progressPercent + "%");
    }
        } else {
            $("#progressBar").progressbar("option", "value", false);
        }
    }

    uploadToS3Complete = function (evt) {
        $("#progressLabel").text("Video was successfully uploaded.");
        $("#progressBar").progressbar({ value: 0 });
        initializeVideoUpload();
    }

    uploadToS3Failed = function (evt) {
        //console.log("failed to upload video");
    }

    uploadToS3Canceled = function (evt) {
        //console.log("video upload was canceled");
    }

    initializeVideoUpload = function () {
        $("#progressLabel").text("Initializing video processing....");

        var clientTime = getClientTime();

        $.ajax({
            type: "POST",
            url: InitializeCustomVideoTubeUploadForUser,
            data: '{"userId":' + "'" + userId + "'" + ',"filename":' + "'" + uploadedFileName + "'" + ',"videoTubeStagingKey":' + "'" + uploadedVideoPath + "'" + ',"clientDateTime":' + "'" + clientTime + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.d) {
                    incrementprogressBar(25);
                    var data = response.d;

                    if (data) {
                        if (data.IsSuccess) {
                            customVideo = response.d.Response;
                            //console.log(customVideo);
                        }
                        else {
                            showUserNotificationArea('errormessage', response.d.Message);
                        }
                    }
                    else {
                        showUserNotificationArea('errormessage', 'Error occured while initializing new video record. Please try again later');
                    }
                }

                // Retrieve video duration                    
                submitVideoForTranscoding();
            },
            async: true,
            error: function (response) {
                showUserNotificationArea('errormessage', 'Error occured while initializing new video record. Please try again later');
                incrementprogressBar(25);
                //console.log(response);
            }
        });
    }

    submitVideoForTranscoding = function () {
        $("#progressLabel").text("Submitting video for transcoding....");

        $.ajax({
            type: "POST",
            url: SubmitVideoForTranscodingService,
            data: '{"videoTubeId":' + customVideo.VideoTubeId + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.d) {
                    incrementprogressBar(25);
                    var data = response.d;

                    if (data) {
                        if (data.IsSuccess) {
                            //console.log(data.Response);
    }
                        else {
                            showUserNotificationArea('errormessage', data.Message);
                        }
                    }
                    else {
                        showUserNotificationArea('errormessage', 'Error occured while submitting video for transcoding. Please try again later');
                    }

                    retrieveVideoPreviewClip();
                }
            },
            async: true,
            error: function (response) {
                showUserNotificationArea('errormessage', 'Error occured while submitting video for transcoding. Please try again later');
                incrementprogressBar(25);
                //console.log(response);
            }
        });
    }

    retrieveVideoPreviewClip = function () {
        $("#progressLabel").text("Extracting video preview clip....");

        $.ajax({
            type: "POST",
            url: GetVideoPreviewClipService,
            data: '{"videoTubeId":' + customVideo.VideoTubeId + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.d) {
                    incrementprogressBar(25);
                    var data = response.d;

                    if (data) {
                        if (data.IsSuccess) {
                            customVideo.VideoPreviewKey = data.Response;

                            $("#divVideoPreviewHolder").prop('disabled', false);
                            $("#divVideoPreviewHolder").prop('data-src', "//" + cloudfrontWebDomain + '/' + customVideo.VideoPreviewKey);
                        }
                        else {
                            showUserNotificationArea('errormessage', data.Message);
                        }
                    }
                    else {
                        showUserNotificationArea('errormessage', 'Error occured while generating video preview clip. Please try again later');
                    }

                    // Need to retrieve video thumbnails
                    retrieveVideoThumbnails();
                }
            },
            async: true,
            error: function (response) {
                showUserNotificationArea('errormessage', 'Error occured while generating video preview clip. Please try again later');
                incrementprogressBar(25);
                //console.log(response);
            }
        });
                }

    retrieveVideoThumbnails = function () {
        $("#progressLabel").text("Extracting video thumbnails....");

        $.ajax({
            type: "POST",
            url: GetVideoThumbnailsService,
            data: '{"videoTubeId":' + customVideo.VideoTubeId + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.d) {
                    incrementprogressBar(25);
                    $("#progressLabel").text("Video pre-processing complete!");
                    var data = response.d;

                    if (data) {
                        if (data.IsSuccess) {
                            var videoThumbnails = data.Response;

                            customVideo.FirstThumbnailKey = videoThumbnails[0];
                            customVideo.SecondThumbnailKey = videoThumbnails[1];
                            customVideo.ThirdThumbnailKey = videoThumbnails[2];

                            customVideo.ActiveThumbnailKey = customVideo.FirstThumbnailKey;

                            initUploadVideoPopup(customVideo);
                        }
                        else {
                            showUserNotificationArea('errormessage', data.Message);
                        }
                    }
                    else {
                        showUserNotificationArea('errormessage', 'Error occured while generating video thumbnails. Please try again later');
                    }
                }
            },
            async: true,
            error: function (response) {
                showUserNotificationArea('errormessage', 'Error occured while generating video thumbnails. Please try again later');
            }
        });
    }

    incrementprogressBar = function (delta) {
        progressBarValue += delta;
        $("#progressBar").progressbar({ value: progressBarValue });
        }

    updateVideoModelFromUI = function () {
        customVideo.Title = $("#txtVideoTitle").val();
        customVideo.Description = $("#txtVideDescription").val();
        customVideo.Keywords = $("#txtVideoKeywords").val();

        var rrated = $('#chkRRated').val();
        if (rrated == 'on') {
            customVideo.IsRRated = true;
        }

        var privacy = $('input[name=rdPrivacy]:checked').val();
        if (privacy == 'private') {
            customVideo.IsPrivate = true;
        }
        else {
            customVideo.IsPrivate = false;
        }

        var jsonCustomVideo = JSON.stringify(customVideo);

        return jsonCustomVideo;
    };


   
    //getClientTime = function () {
    //    var date = new Date();
    //    var clientTime = date.getDate() + '-' + date.getMonth() + '-' + date.getYear() + '-' + date.getHours() + '-' + date.getMinutes();
    //    return clientTime;
    //}

})();