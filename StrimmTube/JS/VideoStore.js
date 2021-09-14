(function () {

    var GetAllCategoriesCategoriesService = "/WebServices/VideoStoreService.asmx/GetAllCategories";
    var GetAllVideoTubePoByPageIndexService = "/WebServices/VideoStoreService.asmx/GetAllVideoTubePoByPageIndex";
    var GetVideosForExpandentPanelService = "/WebServices/VideoStoreService.asmx/GetVideosForExpandentPanel";
    var userId;
    var ddlCategories;
    var pageIndex = 1;
    var prevPageIndex = 1;
    var nextPageIndex = 1;

    var VideoStore = {
        init: function (uid) {
            userId = uid;

            ddlCategories = $(".ddlVideoStoreCategory");

            selectedCategoryId = 0;

            this.loadCategories();
            this.getMoreVideos();
        },

        loadCategories: function () {
            $.ajax({
                type: "POST",
                url: GetAllCategoriesCategoriesService,
                cashe: false,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    var categories = response.d;
                    if (categories) {
                        $.each(categories, function (i, c) {
                            $('.ddlVideoStoreCategory').append($('<option></option)').attr("value", c.CategoryId).text(c.Name));
                            if (i == 0) {
                                $('.ddlVideoStoreCategory option[value="' + 0 + '"]').prop('selected', true);
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

        selectedCategoryChanged: function () {
            selectedCategoryId = $("#ddlVideoStoreCategory option:selected").val();

            this.getMoreVideos();
        },

        getMoreVideos: function (keywords) {
            var usernameKeywords;
            var videoKeywords;

            if (keywords) {
                if ($("#rdVideosInVideoRoom").is(":checked")) {
                    videoKeywords = keywords.trim();
                }
                else {
                    usernameKeywords = keywords.trim();
                }
            }

            var searchCriteria = {
                PageIndex: pageIndex,
                CategoryId: selectedCategoryId,
                OwnerUsernameKeyword: usernameKeywords,
                VideoContentKeywords: videoKeywords
            };

            $.ajax({
                type: "POST",
                url: GetAllVideoTubePoByPageIndexService,
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
                        pageIndex = data.NextPageIndex;
                        prevPageIndex = data.PrevPageIndex;
                        nextPageIndex = data.NextPageIndex;

                        var pageSize = data.PageSize;
                        var pageCount = data.PageCount;

                        if (data.VideoTubeModels) {
                            
                            var videos = data.VideoTubeModels;
                            //console.log(videos);
                            var videoControls = Controls.BuildVideoBoxControlForVideoStorePage(videos, userId);
                            $("#divVideosHolder").append(videoControls);
                         

                            if (pageIndex == nextPageIndex) {
                                $('#divLoadMore').hide();
                            }
                        }
                        $(".gridder").gridderExpander({
                            scrollOffset: 60,
                            scrollTo: "listitem", // "panel" or "listitem"
                            animationSpeed: 400,
                            animationEasing: "easeInOutExpo",
                            onStart: function () {
                                //console.log("Gridder Inititialized");
                            },
                            onExpanded: function (object) {
                                //console.log("Gridder Expanded");
                                $(".carousel").carousel();
                            },
                            onChanged: function (object) {
                                //console.log("Gridder Changed");
                            },
                            onClosed: function () {
                                //console.log("Gridder Closed");
                            }
                        });
                    
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

        getExpandedContentByVideoId: function (videoId, ownerUserId)
        {
            //get videos info and total videos if the user  
            var expandendContent = 
            $.ajax({
                type: "POST",
                url: GetVideosForExpandentPanelService,
                cashe: false,
                data: '{"videoId":' + videoId + ',"ownerUserId":' + ownerUserId + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    //var firstItem = response.d[0];
                    //content = Controls.BuildVideoExpandedContentControlForVideoStorePage(response);
                    //console.log(response)
                },
                complete: function (response) {
                   // console.log(content)
                    
                }
            });
          //console.log(expandendContent)
            return expandendContent;
        },
         
       
       

        sortVideos: function (selectCtrl) {
            var allvideoboxes = $("#divVideosHolder").find("div.vsVideoBox");
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

        showVideo: function(btn) {
            if (btn) {
                var videoId = $('#' + btn.id).data('video-id');
                ShowStrimmPlayer(videoId);
            }
        },

        findVideosByKeywords: function () {
            var keywords = $('#txtVideoSearchKeywords').val();
            if (keywords && keywords != '') {
                this.getMoreVideos(keywords);
            }
        },

        searchTargetChanged: function () {
            $("#txtVideoSearchKeywords").val('');
        }
    };

    getVideoStore = function () {
        return VideoStore;
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
})();