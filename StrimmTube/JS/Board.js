var webMethodGetFavoriteChannels = "/WebServices/ChannelWebService.asmx/GetAllFavoriteChannelsForUserByUserIdAndClientTime";
var webMethodGetTopChannelsOnTheAirJson = "/WebServices/ChannelWebService.asmx/GetTopChannelsOnTheAir";
var webMethodGetChannelsByUserIdAndClientTime = "/WebServices/ChannelWebService.asmx/GetChannelsByUserIdAndClientTime";
var webMethodUpdateUserAvatar = "/WebServices/UserService.asmx/UpdateUserAvatar";
var webMethodUpdateuserBackground = "/WebServices/UserService.asmx/UpdateUserBackGround";
var webMethodGetUserBio = "/WebServices/UserService.asmx/GetUserBio";
var webMethodUpdateUserBio = "/WebServices/UserService.asmx/UpdateUserBio";

//$(document).ready(function () {
//    //GetUserChannels();  
//});

var favoriteChannelSetTimeOut;
var topChannelSetTimeOut;
var executeTopChannels = 0;
var dashboardInfoVisible = false;

function ShowNewUserWelcomePopup(username) {
    alertify.alert("Hello " + username + "! Welcome to your TV Network.  Click 'Watch Tutorial' to familiarize yourself with Strimm and to create a first channel.");
}

function GetUserChannels() {
   // $("#loadingDivHolderDash").show();
    var clientTime = getClientTime();
    var favoriteChannel = 0;
    $.ajax({
        type: "POST",
        url: webMethodGetChannelsByUserIdAndClientTime,
        data: '{"userId":' + "'" + boardOwnerUserId + "'" + ',"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            favoriteChannel = Controls.BuildChannelControlForDashboardPage(response, true);
            $("#channelsHolder").html("").html(favoriteChannel);
        },
        complete: function () {
        },
        error: function () {
            //$("#loadingDivHolderDash").hide();
        }
    });
};

function GetFavoriteChannels() {
    var clientTime = getClientTime();
    var favoriteChannel = 0;
    $.ajax({
        type: "POST",
        url: webMethodGetFavoriteChannels,
        data: '{"userId":' + "'" + boardOwnerUserId + "'" + ',"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
          
            if (response.d.length > 0)
            {
                favoriteChannel = Controls.BuildChannelControlForChannelPage(response, true);
            }

            if (favoriteChannel.length > 0 && boardOwnerUserId > 0) {
                $(".sideContentHolder").html("").html(favoriteChannel);
            }
            else {
                if (boardOwnerUserId && boardOwnerUserId > 0) {
                    $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>You have no favorite channels yet</>");
                }
                else {
                    $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>You have no favorite channels yet.  Sign up or login to save your favorites</>");
                }
            }

        },
        complete: function () {
        },
        error: function () {
        }
    });
};

function PollFavoriteChannels() {

    var now = new Date();
    var favoritematch
    var userIdCheked = 0;
    if (userId != null) {
        userIdCheked = userId;
    }
    
    favoriteChannelSetTimeOut = setTimeout(function () {
        var favoriteChannel = 0;
        var clientTime = getClientTime();
        $.ajax({
            type: "POST",
            url: webMethodGetFavoriteChannels,
            data: '{"userId":' + "'" + boardOwnerUserId + "'" + ',"clientTime":' + "'" + clientTime + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                favoriteChannel = Controls.BuildChannelControlForChannelPage(response, true);


                if (favoriteChannel.length > 0 && boardOwnerUserId > 0) {
                    $(".sideContentHolder").html("").html(favoriteChannel);
                }
                else {
                    if (boardOwnerUserId && boardOwnerUserId > 0) {
                        $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>You have no favorite channels yet</>");
                    }
                    else {
                        $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>You have no favorite channels yet.  Sign up or login to save your favorites</>");
                    }
                }
            },
            complete: function () {
                PollFavoriteChannels();
            },
            error: function () {
            }
        });

    }, 30000);

};

function GetTopChannels() {
    var clientTime = getClientTime();
    ajaxTopChannels = $.ajax({
        type: "POST",
        url: webMethodGetTopChannelsOnTheAirJson,
        data: '{"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            var topChannels = Controls.BuildChannelControlForChannelPage(response, true);
            if (topChannels.length > 0) {
                $(".sideContentHolder").html("").html(topChannels);
            }
            else {
                $("#sideBarChannel .sideContentHolder").html("").html("<span class='msg'>The top channels are not currently broadcasting</>");
            }

        },
        complete: function () {
            PollTopChannels();
        },
        error: function () {
        }
    });

};

function PollTopChannels() {
    var timeoutInSec = 30000;
    var now = new Date();
    topChannelSetTimeOut = setTimeout(function () {
        if (executeTopChannels > 0) {
            GetTopChannels();
        }
        else {
            return;
        }
        
    }, timeoutInSec);
};

function ShowFavorites() {
    executeTopChannels = 0;
    window.clearTimeout(topChannelSetTimeOut);
    $(".iconDescriptionDashboard").html("favorite channels");
    $(".sideContentHolder").html("");
    $(".sideBarOptionsDashboard .addtofavorite").addClass("addFavoritesActive");
    $(".sideBarOptionsDashboard .toprated").removeClass("topRatedActive");
    $(".sideBarOptionsDashboard .addBio ").removeClass("addBioActive");
    
    GetFavoriteChannels();
    (function poll() {
        PollFavoriteChannels();
    })();
};

function ShowTopChannels() {
    executeTopChannel = 1;
    window.clearTimeout(favoriteChannelSetTimeOut);
    $(".iconDescriptionDashboard").html("top channels");
    $(".sideContentHolder").html("");
    $(".sideBarOptionsDashboard .addtofavorite").removeClass("addFavoritesActive");
    $(".sideBarOptionsDashboard .toprated").addClass("topRatedActive");
    $(".sideBarOptionsDashboard .addBio").removeClass("addBioActive");
    GetTopChannels();
    (function poll() {
        PollTopChannels();
    })();
};

function checkBio(event) {
    var keyCode = event.keyCode || event.which;

    var selStart = event.currentTarget.selectionStart;
    var selEnd = event.currentTarget.selectionEnd;

    var text = $("#txtAreaBio").val();
    // ...
    // different keys do different things
    // Different browsers provide different codes
    // see here for details: http://unixpapa.com/js/key.html    
    // ...
    if (keyCode == 13) {
        if (selStart < selEnd && selStart < text.length) {
            var textstart = text.substring(0,selStart);
            var textend = text.substring(selEnd, text.length);
            $("#txtAreaBio").val(textstart + "\r" + textend);
        }
        else if (selStart == selEnd && selStart < text.length) {
            var textstart = text.substring(0,selStart);
            var textend = text.substring(selEnd, text.length);
            $("#txtAreaBio").val(textstart + "\r" + textend);
        }
        else {
            $("#txtAreaBio").val(text + "\r");
        }
        return false;
    }
}

function showBioEdit() {
    var clientTime = getClientTime();
    $("#bioActionHolder").show();
    var bioValue = $("#txtAreaBioHidden").val();
    $("#lblBio").hide();
    $("#txtAreaBio").val(bioValue);
    //var textarea = document.getElementById('txtAreaBio');
    //textarea.value = '\r\rHELLO';
    $(".sideBarOptionsDashboard .addtofavorite").removeClass("addFavoritesActive");
    $(".sideBarOptionsDashboard .toprated").removeClass("topRatedActive");
    $("#bioEdit").hide();
    //$.ajax({
    //    type: "POST",
    //    url: webMethodGetUserBio,
    //    data: '{"userId":' + "'" + boardOwnerUserId + "'" + ',"clientTime":' + "'" + clientTime + "'" + '}',
    //    dataType: "json",
    //    contentType: "application/json; charset=utf-8",
    //    success: function (response) {

    //        if (response.d.length > 0) {

    //            $(".sideContentHolder").html("").html(response.d);
    //        }

    //        else {
    //            $(".sideContentHolder").html("").html('<span id="spnMsg" class="msg">Hi! \r\n Thank you for visiting my page. \r\n I have not entered my Bio yet, but I will do it soon!</span>');
    //        }
    //    },
    //    complete: function () {
    //    },
    //    error: function () {
    //    }
    //});
};
function closeBioEdit() {
    $("#bioEdit").addClass("editActive").text("edit").removeAttr("onclick").attr("onclick", "showBioEdit()");
    $("#bioActionHolder").hide();
    $("#lblBio").show();
    $("#bioEdit").show();
};

function GetBio() {
    $("#bioEdit").addClass("editActive").text("edit").removeAttr("onclick").attr("onclick", "showBioEdit()");
    $(".sideBarOptionsDashboard .addtofavorite").removeClass("addFavoritesActive");
    $(".sideBarOptionsDashboard .toprated").removeClass("topRatedActive");
    $(".iconDescriptionDashboard").text("").text("bio");
    $(".sideBarOptionsDashboard .addBio").addClass('addBioActive');

    $.ajax({
        type: "POST",
        url: webMethodGetUserBio,
        data: '{"userId":' + boardOwnerUserId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var bioHtml = "";
            //console.log(isOwner)

            var bio = '';
            var editBio = '';

            if (response.d == '') {
                bio += "Hi!<br/>Thank you for visiting my page.<br/>I have not entered my Bio yet, but I will do it soon!";
                editBio = "Hi!\r\nThank you for visiting my page.\r\nI have not entered my Bio yet, but I will do it soon!";
            }
            else {
                editBio = response.d.replace(/(\r\n|\n)/g, "\r\n");
                bio = response.d.replace(/(^|\r\n|\n)/g, "<br/>");
            }

            if (isOwner == "True") {
                bioHtml += '<div id="bioWrapper" >';
                bioHtml += '<div id="bioEditHolder" runat="server">'
                bioHtml += '<a id="bioEdit" ClientIDMode="Static" runat="server" onclick="showBioEdit()">edit</a>';
                bioHtml += '<div id="bioActionHolder">';
                bioHtml += '<textarea id="txtAreaBio" cols="37" rows="10" maxlength="300" onkeyup="checkBio(event);"></textarea>';
                bioHtml += '<span id="spnLimit">300 characters limit</span>';
                bioHtml += '<a id="btnSaveBio" onclick="UpdateUserBio()" >save</a>';
                bioHtml += '<a id="btnCancelEdit" onclick="closeBioEdit()">cancel</a>';
                bioHtml += '</div>';
                bioHtml += '</div>';
                bioHtml += '<div id="bioHolder">';
                bioHtml += '<input id="txtAreaBioHidden" type="hidden" value="' + editBio + '"/>';
                bioHtml += '<label id="lblBio">' + bio + '</label>';
                bioHtml += '</div>';
                bioHtml += '</div>';
            }
            else {
                bioHtml += '<div id="bioWrapper" >';
                bioHtml += '<div id="bioHolder">';
                bioHtml += '<input id="txtAreaBioHidden" type="hidden" value="' + editBio + '"/>';
                bioHtml += '<label id="lblBio">' + bio + '</label>';
                bioHtml += '</div>';
                bioHtml += '</div>';
            }
            $(".sideContentHolder").html("").html(bioHtml);



        },
        complete: function () {
        },
        error: function () {
        }
    });
    
};

function UpdateUserBio()
{
    var bio = $("#txtAreaBio").val();
    if (bio != null && $("#txtAreaBio:contains('I have not entered my Bio yet')")==true) {
        bio = '';
    }

    $.ajax({
        type: "POST",
        url: webMethodUpdateUserBio,
        data: '{"userId":' + boardOwnerUserId + ',"userStory":' + "'" + bio + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            GetBio();
            alertify.success("Your Bio was successfuly updated");
            
        },
        complete: function () {
        },
        error: function () {
        }
    });
}
function ToggleDashboardForm()
{
    if ($(".dashBoardForm").is(":visible")) {
        $(".dashBoardForm").hide();
        $(".channelBox").show();
        
    }
    else {
        $(".dashBoardForm").show();
        $(".channelBox").hide();
       // dashboardInfoVisible = true;
        ResizePopup();      
    }
}

function CloseDashBoardForm() {

    $(".dashBoardForm").hide();
    $(".channelBox").show();
};
//$('html').click(function (evt) {
//    if ((evt.target.id == "ancEdit") || (evt.target.id == "dashboardProfileHolder"))
//    {
//        return;
//    }
//    else
//    {
//        if ($(".dashBoardForm").is(":visible")) {
//            CloseDashBoardForm();
//        }
//    }

   
//});


function ResizePopup() {
    var $popUp = $("#popUpOverlay");

    var pageHeight = (document.height !== undefined) ? document.height : document.body.offsetHeight;
    var $window = $(window).on('resize', function () {
        $popUp.height(pageHeight);
    }).trigger('resize'); //on page load
};

function ValidateBoardImage(fuId, fileSize) {
    //check image size
    var maxUploadSize = 300 * 1024;// max upload size 300kb
    //console.log(input);
    filename = $("#"+fuId).val();
    var validExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    if (fileSize != null) {
        if (fileSize > maxUploadSize) {
            $("#btnSaveDashBoardInfo").attr('disabled', 'disabled');
            $("#lblInfoMsg").text("").text("*Image is too big");
            return false;
        }
            //check image extention
        else if (!validExtensions.test(filename)) {
            $("#btnSaveDashBoardInfo").attr('disabled', 'disabled');
            $("#lblInfoMsg").text("").text("*Image must be .jpg or .png files only");
            return false;
        }
        else {
            $("#btnSaveDashBoardInfo").removeAttr('disabled');
            $("#lblInfoMsg").text("");
            return true;
        }

    }
    else {
        return true;
    }

};

function readURL(input, imgId, fuId) {
  
   
    if (input.files && input.files[0]) {
        var f = input.files[0];
        var fileSize = f.size || f.fileSize;
        ValidateBoardImageValidateImage(fuId, fileSize);
        var reader = new FileReader();
        void 0
        reader.onload = function (e) {
            $("#" + imgId)
            .attr('src', e.target.result)
            .width(50)
            .height(50);
           
           
           
           
        };
        reader.readAsDataURL(input.files[0]);
    }
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
};

function addPlayer(videoPath) {
    void 0
    //var stringId = videoPath.id;
    //var idArr = stringId.split("_");
    var id = videoPath;
   
    //console.log(videoPath.id);
    // 3. This function creates an <iframe> (and YouTube player)
    //    after the API code downloads.


    function onYouTubeIframeAPIReady() {
        player = new YT.Player('related', {
            height: '546',
            width: '728',

            videoId: videoPath,
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

//////////CROP//////////////////////////
var api;
var jcrop_api;
var cropWidth = 200;
var cropHeight = 200;
var avatarCropitInitialized = false;
var backgroundCropitInitialized = false;

function ShowCropModal() {
    var imageSrc = $("#imgDashboardAvatar").attr("src");
   
    var imageCropitHtml = '<div class="image-editor">';
    imageCropitHtml += '<a class="close close_x" href="#"></a>';
    imageCropitHtml += '<div class="image-size-label">';
    imageCropitHtml += '<h1 class="popupHeader">Image Editor</h1>';
    imageCropitHtml += '</div>';
    imageCropitHtml += '<div class="cropit-image-preview-container">';
    imageCropitHtml += '<div class="cropit-image-preview"></div>';
    imageCropitHtml += '</div>';
    imageCropitHtml += '<div class="select-image-btn uploadImgDashboard">Upload Image</div>';
    imageCropitHtml += '<div class="minImgSize">(Minimum image size: 200px X 200px)</div>';
    imageCropitHtml += '<input type="range" class="cropit-image-zoom-input" />';
    imageCropitHtml += '<div class="image-size-label">Move cursor to resize image</div>';
    imageCropitHtml += '<a class="export"> Save</a>';
    imageCropitHtml += '<input type="file" class="cropit-image-input" id="avatarInput" />';
    imageCropitHtml += '</div>';

    $('#divCropImgContainer').html('');
    $('#divCropImgContainer').html(imageCropitHtml);

    $('#divCropImgContainer').lightbox_me({
        centered: true,
        onLoad: function () {
            InitCropItAvatar(imageSrc);
        },
        onClose: function() {
            $imageCropper = $('#divCropImgContainer .image-editor');
            $imageCropper.cropit('reenable');
            //$imageCropper.cropit('imageSrc', "");
            RemoveOverlay();
        }
    });
}

function ShowCropBackgroundModal() {
    var imageSrc = $("#imgDashboard").attr("src");

    var cropithtml = '<div class="image-editor">';
    cropithtml += '<a class="close close_x" href="#"></a>';
    cropithtml += '<div class="image-size-label">';
    cropithtml += '<h1 class="popupHeader">Image Editor</h1>';
    cropithtml += '</div>';
    cropithtml += '<div class="cropit-image-preview-container">';
    cropithtml += '<div class="cropit-image-preview"></div>';
    cropithtml += '</div>';
    cropithtml += '<div id="imageSelectButton" class="select-image-btn imageUploadDashboard">Upload Background Image</div>';
    cropithtml += '<div class="minImgSize">(Minimum image size: 250px X 900px)</div>';
    cropithtml += '<input id="backgroundRange" type="range" class="cropit-image-zoom-input" />';
    cropithtml += '<div class="image-size-label">Move cursor to resize image</div>';
    cropithtml += '<a class="export">Save</a>';
    cropithtml += '<input id="backgroundInput" type="file" class="cropit-image-input" />';
    cropithtml += '</div>';

    $('#divCropImgBackgroundContainer').html('');
    $('#divCropImgBackgroundContainer').html(cropithtml);

    $('#divCropImgBackgroundContainer').lightbox_me({
        centered: true,
        onLoad: function () {
            InitCropItBackground(imageSrc);
        },
        onClose: function() {
            $imageCropper = $('#divCropImgBackgroundContainer .image-editor');
            $imageCropper.cropit('reenable');
            RemoveOverlay();
        }
    });
}





function showPrev(coords)
{
    var rx = 100 / coords.w;
    var ry = 100 / coords.h;

    $('#prevImg').css({
        width: Math.round(rx * 500) + 'px',
        height: Math.round(ry * 370) + 'px',
        marginLeft: '-' + Math.round(rx * coords.x) + 'px',
        marginTop: '-' + Math.round(ry * coords.y) + 'px'
    });
}

function InitCropItAvatar(imgSrc) {
  
    var fileNameIndex = imgSrc.lastIndexOf("/") + 1;
    var filename = imgSrc.substr(fileNameIndex);

    void 0

    $imageCropper = $('#divCropImgContainer .image-editor');

    //$imageCropper.cropit({
    //    imageBackground: true,
    //    imageBackgroundBorderWidth: 60
    //})

    $imageCropper.cropit('imageBackground', false);
    $imageCropper.cropit({ allowCrossOrigin: true });
    $imageCropper.cropit({ allowDragNDrop: true });
    $imageCropper.cropit('imageSrc', "");

    $imageCropper.find('.select-image-btn').click(function () {
        $imageCropper.find('.cropit-image-input').click();
    });

    $imageCropper.find('.export').click(function () {

        if ($("#avatarInput").val() != "") {
            filename = $("#avatarInput").val().split("\\").join("/");
            filename = filename.substring(filename.lastIndexOf('/') + 1);

            var imageData = $imageCropper.cropit('export', {
                type: 'image/jpeg',
                quality: .9,
                originalSize: true
            });

            var b64 = imageData.split("base64,")[1];//string fileName, string imageData, int userId
            var params = '{"fileName":' + "'" + filename + "'" + ',"imageData":' + "'" + b64 + "'" + ',"userId":' + userId + '}';

            $("#divCropImgContainer").hide();
            $("#loadingDiv").show();

            $.ajax({
                type: "POST",
                url: webMethodUpdateUserAvatar,
                data: params,
                cashe: false,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d) {
                        $("#imgDashboardAvatar").removeAttr("src").attr("src", response.d);
                        alertify.success("Avatar has been updated.");
                    }
                    else {
                        alertify.error("Avatar was not upaded. Please try again.")
                    }
                    $('#divCropImgContainer').trigger('close');
                },
                complete: function () {

                    $imageCropper.cropit('imageState', null);
                    $("#loadingDiv").hide();
                },
                error: function (request, status, error) {
                    void 0;
                }
            });
        }       
   });
};

function InitCropItBackground(imgSrc) {

    var fileNameIndex = imgSrc.lastIndexOf("/") + 1;
    var filename = imgSrc.substr(fileNameIndex);

    $imageCropper = $('#divCropImgBackgroundContainer .image-editor');

    $imageCropper.cropit('imageBackground', false);
    $imageCropper.cropit({ allowCrossOrigin: true });
    $imageCropper.cropit({ allowDragNDrop: true });
    $imageCropper.cropit('imageSrc', "");

    $imageCropper.find('#imageSelectButton').click(function () {
        $imageCropper.find('#backgroundInput').click();
    });

    $imageCropper.find('.export').click(function () {
        var imageData =  $imageCropper.cropit('export', {
            type: 'image/jpeg',
            quality: .9,
            originalSize: true
        });

        if ($("#backgroundInput").val() != "") {
            filename = $("#backgroundInput").val().split("\\").join("/");
            filename = filename.substring(filename.lastIndexOf('/') + 1);

            if (filename != "") {
                var b64 = imageData.split("base64,")[1];//string fileName, string imageData, int userId
                var params = '{"fileName":' + "'" + filename + "'" + ',"imageData":' + "'" + b64 + "'" + ',"userId":' + userId + '}';

                $("#divCropImgBackgroundContainer").hide();
                $("#loadingDiv").show();

                $.ajax({
                    type: "POST",
                    url: webMethodUpdateuserBackground,
                    data: params,
                    cashe: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response.d) {
                            $("#imgDashboard").removeAttr("src").attr("src", response.d);
                            alertify.success("Background image has been updated.");
                        }
                        else {
                            alert.error("Background image was not updated. Please try again.");
                        }
                        $('#divCropImgBackgroundContainer').trigger('close');
                    },
                    complete: function () {
                        $("#loadingDiv").hide();
                    },
                    error: function (request, status, error) {
                        void 0;
                    }
                });
            }
        }
    });
};

$(document).ready(function () {
    GetBio();
});