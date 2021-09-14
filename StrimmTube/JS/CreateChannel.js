//(function () {

    //WEBSERVICES-test
    var webMethodCheckChannelNameIsTaken = "/WebServices/ChannelWebService.asmx/CheckChannelNameIsTaken";
    var webMethodCheckChannelNameIsReserved = "/WebServices/ChannelWebService.asmx/CheckChannelNameIsReserved";
    var webMethodGetChannelCategoriesJsonForCreateChannel = "/WebServices/ChannelWebService.asmx/GetChannelCategories";
    var webMethodGetChannelLanguagesForCreateChannel = "/WebServices/ChannelWebService.asmx/GetChannelLanguages";
    var webMethodCreateNewChannel = "/WebServices/ChannelWebService.asmx/CreateNewChannel";
    var webMethodSaveOttSettings = "/WebServices/ChannelWebService.asmx/SaveOttSettings";
    var webMethodUpdateChannelForModal = "/WebServices/ChannelWebService.asmx/UpdateChannelForModal";
    var webMethodDeleteChannelForModal = "/WebServices/ChannelWebService.asmx/DeleteChannelForModal";
    var webMethodGetChannelTubeByChannelId = "/WebServices/ChannelWebService.asmx/GetChannelTubeByChannelId";
    var webMethodGenerateRokuApp = "/WebServices/RokuAppGeneratorService.asmx/GenerateApp";
    var webMethodGetChannelRokuSettings = "/WebServices/RokuAppGeneratorService.asmx/GetChannelTubeRokuSettings";
    var webMethodUpsertChannelRokuSettings   = "/WebServices/RokuAppGeneratorService.asmx/UpsertChannelTubeRokuSettings";
    var webMethodGetUserRokuApp = "/WebServices/RokuAppGeneratorService.asmx/GetUserRokuApp";
    var webMethodGetUserProductSubscriptionsByUserId = "/WebServices/UserService.asmx/GetUserProductSubscriptions";
    var webMethodGetVideoTubeModelForChannelAndCategoryByPage = "/WebServices/ScheduleWebService.asmx/GetVideoTubeModelForChannelAndCategoryByPage";

    //END WEBSERVICES

    //GLOBAL VARIABLES
    var channelName;
    var channelUrl;
    var channelId;
    var isChannelNameTaken;
    var isChannelNameReserved;
    var b64;
    var b64Logo;
    var fileSize;
    var fileName;
    var openFile;
    var channelCount;
    var maxUserCount;
    var channelCategory;
    var $imageCropper;
    var $imageLogoCropper;
    var userChannelEntitlments;
    var channelsAvailableToCustomLabelCount;
    var channelsAvailableToEmbedCount;
    var channelsAvailableToPasswordProtectCount;
    var channelsAvailableToWhiteLabelCount;
    var channelsAvailableToMute;
    var channelsAvailableToMatureContentCount;
    var privateVideosForChannelsAvailableCount;
    var customLogo;
    var channelsEmbeddedCount;
    var channelsPurchasedCount;
    var customLabelChannelsPurchaseCount;
    var customLabeledChannelCount;
    var passwordProtectedChannelCount;
    var passwordProtectedChannelsPuchaseCount;
    var whiteLabeledChannelsCount;
    var whiteLabeledChannelsPurchasedCount;
    var mutedChannelsPurchasedCount;
    var matureContentChannelCount;
    var playerControlsChannelCount;
    var channelsAvailableToPlayerControlsCount;
    var privateVideosAllowedCount;
   var privateVideoModeEnabledForCreateChannel;
   

    var isPasswordProtectedNewChannel = false;
    var isWhiteLabeledNewChannel=false;
    var embedEnabledNewChannel=false;
    var muteOnStartupNewChannel = true;
    var showPlayerControls = true;
    var customLabelNewChannel;
    var subscriberDomainNewChannel;
    var channelPassword;
    var customLabelText;
   
   
    var matureContentEnabledCreateChannel;
    var embedOnlyMode = false;
    var privateVideoModeEnabled;
    var confirmChange = false;
    var toggleMuteHasClass;
    var togglePasswordHasClass;
    var toggleCustomBrandingHasClass;
    var toggleControlsHasClass;
    var toggleMatureContentHasClass;
    var toggleEmbedOnlyModeHasClass;
    var toggleEmbeddedChannelHasClass;
    var toggleWhiteLabelHasClass;
var playLiveFirst = false;
var keepGuideOpen = false;
var hasProfPlan = false;
var hasProfPlusPlan = false;
var hasValidRokuVideos = false;
var needRokuAppGenerate = false;

    //END GLOBAL VARIABLES
    $(document).on('blur', '.createChannelForm #txtChannelNameForm', function () {
        CreateChannel.ValidateChannelName();
    });
    $(document).on('change', '#createChannelModal input', function () {
        confirmChange = true;
        void 0
    });
    $(document).on('change', '#createChannelModal select', function () {
        confirmChange = true;
        void 0;
        void 0
    });
    $(document).on('change', '#createChannelModal textarea', function () {
        confirmChange = true;
        void 0;
    });
    $(document).on('change', '#createChannelModal .select-image-btn', function () {
        confirmChange = true;
        void 0;
    });
   
    

    //#createChannelModal #toggleMute, #createChannelModal #togglePassword, #createChannelModal #toggleCustomBranding, #createChannelModal #toggleControls, #createChannelModal #toggleMatureContent, #createChannelModal #toggleEmbedOnlyMode'

function CloseOTTSettingsPopup() {
    $('#ottSettingsModal').trigger('close');
}

    function CloseCreateChannelPopup() {
       
            var queryString = getQueryStringParameterByName("create-channel")
            void 0;
            if (queryString == 1) {
                var url = window.location.href.split('?')[0];
                history.pushState({}, null, url);
                // window.location.replace(url);
                void 0;
            }
            
            var toggleMuteClassChanged = $("#toggleMute").attr('class');
            var togglePasswordClassChanged = $("#togglePassword").attr('class');
            var toggleCustomBrandingClassChanged = $("#toggleCustomBranding").attr('class');
            var toggleControlsClassChanged = $("#toggleControls").attr('class');
            var toggleMatureContentClassChanged = $("#toggleMatureContent").attr('class');
            var toggleEmbedOnlyModeClassChanged = $("#toggleEmbedOnlyMode").attr('class');
            var toggleEmbeddedChannelClassChanged = $("#toggleEmbeddedChannel").attr('class');
            var toggleWhiteLabelClassChanged = $("#toggleWhiteLabel").attr('class');
            void 0;
            void 0
            if (confirmChange == false)
            {
                if (toggleMuteClassChanged != toggleMuteHasClass) {
                    confirmChange = true;
                }
               
                else if (togglePasswordClassChanged != togglePasswordHasClass) {
                    confirmChange = true;
                }
                
                else if (toggleCustomBrandingClassChanged != toggleCustomBrandingHasClass) {
                    confirmChange = true;
                }
               
                else if (toggleControlsClassChanged != toggleControlsHasClass) {
                    confirmChange = true;
                }
               
                else if (toggleMatureContentClassChanged != toggleMatureContentHasClass) {
                    confirmChange = true;
                }
               
                else if (toggleEmbedOnlyModeClassChanged != toggleEmbedOnlyModeHasClass) {
                    confirmChange = true;
                }
                
                else if (toggleEmbeddedChannelClassChanged != toggleEmbeddedChannelHasClass) {
                    confirmChange = true;
                }
                
                else if (toggleWhiteLabelClassChanged != toggleWhiteLabelHasClass) {
                    confirmChange = true;
                }
            
               
            }
            
           
            void 0
            if (confirmChange) {
                void 0;
                void 0;

               var r = confirm("Are you sure you want to close channel settings without saving updates?");
                if (r == true) {
                    $('#createChannelModal').trigger('close');
                    confirmChange = false;
                } else {
                    return;
                }
            }
            else
            {
                $('#createChannelModal').trigger('close');
                confirmChange = false;
            }

            $("#snippetPopup").trigger("close");

        };
        function CloseConfirmationMessage()
        {
            $('#conformationMessage').trigger('close');
        }

        function CloseCreateChannelPopupConfirmed()
        {
            $('#createChannelModal').trigger('close');
            $('#conformationMessage').trigger('close');
        }
 

        //$(function () {
        //   CreateChannel.ShowPassword();
       
        //  });
        decodeBase64 = function (s) {
       
            var e = {}, i, b = 0, c, x, l = 0, a, r = '', w = String.fromCharCode, L = s.length;
            var A = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!+/";
            for (i = 0; i < 64; i++) { e[A.charAt(i)] = i; }
            for (x = 0; x < L; x++) {
                c = e[s.charAt(x)]; b = (b << 6) + c; l += 6;
                while (l >= 8) { ((a = (b >>> (l -= 8)) & 0xff) || (x < (L - 2))) && (r += w(a)); }
            }
            return r;
      
        };
 
       
        var CreateChannel = {

            Init: function (channelcount, maxusercount) {
                channelCount = channelcount;
                maxUserCount = maxUserCount;
                //this.GetChannelCategory();
                //this.GetLanguage();
            },
            ShowChannelPassword: function () {
                void 0
                if ($("#channelPassHolder #chkBxShowHidePassword:checked").length > 0) {
                    var pswd = $("#txtPasswordProtected").val();
                    $("#txtPasswordProtected").attr("id", "txtpassword2");
                    $("#txtpassword2").after($("<input id='txtPasswordProtected' type='text'>"));
                    $("#txtpassword2").remove();
                    $("#txtPasswordProtected").val(pswd);
                }
                else { // vice versa
                    var pswd = $("#txtPasswordProtected").val();
                    $("#txtPasswordProtected").attr("id", "txtpassword2");
                    $("#txtpassword2").after($("<input id='txtPasswordProtected' type='password'>"));
                    $("#txtpassword2").remove();
                    $("#txtPasswordProtected").val(pswd);
                }
           
                //var isUpgradeChannelModalUp = $("#createChannelModal .createChannelForm ").hasClass("upgradeChannel");
                //if(isUpgradeChannelModalUp)
                //var passValue = "";
                //if (channelPassword) {
                //    console.log(channelPassword)
                //    passValue = decodeBase64(channelPassword);
                //}
                //else {
                //    passValue = $("#txtPasswordProtected").val();
                //}
                //if ($("#txtPasswordProtected").attr("type") == "password") {
                //    $("#txtPasswordProtected").attr("type", "text").val(passValue);
                //}
                //else {
                //    $("#txtPasswordProtected").attr("type", "password");
                //}



                //    console.log("here");
                //$(".showpasswordlabel").each(function (index, input) {
                //    var $input = $(input);
                //    $('<label class="showpasswordlabel"/>').append(
                //        $("<input type='checkbox' class='showpasswordcheckbox chkGreenMarkStyle' />").click(function () {
                //            if (channelPassword == ""||channelPassword==null)
                //            {

                //                var change = $(this).is(":checked") ? "text" : "password";
                //                var rep = $("<input type='" + change + "' />")
                //                    .attr("id", $input.attr("id"))
                //                    .attr("name", $input.attr("name"))
                //                    .attr('class', $input.attr('class'))
                //                    .val($("#txtPasswordProtected").val())
                //                    .insertBefore($input);
                //                $input.remove();
                //                $input = rep;
                //            }
                //            else
                //            {
                //                var change = $(this).is(":checked") ? "text" : "password";
                //                var rep = $("<input type='" + change + "' />")
                //                    .attr("id", $input.attr("id"))
                //                    .attr("name", $input.attr("name"))
                //                    .attr('class', $input.attr('class'))
                //                    .val(decodeBase64(channelPassword))
                //                    .insertBefore($input);
                //                $input.remove();
                //                $input = rep;
                //            }


                //        })
                //    ).append($("<span class='showPassUpgrade'/>").text("Show password")).insertAfter($input);
                //    //).append($("<span/>").text("Show password")).insertAfter($input);
                //});
            },
            GetChannelCategory: function () {

                $.ajax({
                    type: "POST",
                    url: webMethodGetChannelCategoriesJsonForCreateChannel,
                    cashe: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        $("#createChannelModal select#txtChannelCategory").html("").html("<option value=0>Choose Category</option>")
                        $.each(response.d, function (key, val) {

                            $("#createChannelModal select#txtChannelCategory").append($('<option>', { value: val.CategoryId })
    .text(val.Name));
                        })

                    },
                    error: function (request, status, error) {

                    }
                });
            },

            GetLanguage: function (isCreating) {
                $.ajax({
                    type: "POST",
                    url: webMethodGetChannelLanguagesForCreateChannel,
                    cashe: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        $("#createChannelModal select#slctLang").html("").html("<option value=0 disabled='disabled'>Choose Language</option>")
                        $.each(response.d, function (key, val) {
                            if (key == 11) {
                                $("#createChannelModal select#slctLang").append('<option style="border-bottom:1px solid black;" disabled="disabled">&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;</option>');
                            }

                            $("#createChannelModal select#slctLang").append($('<option>', { value: val.LanguageId })
    .text(val.Name));
                        })

                        if (isCreating) {
                            languageId = 0;
                        }

                        $("#createChannelModal select#slctLang option[value='" + languageId + "']").attr("selected", "selected");

                    },
                    error: function (request, status, error) {

                    }
                });
            },

            RedirectToCreateChannel: function () {

                if (channelCount == maxUserChannelCount) {

                    alertify.alert("You have reached the maximum number of channels allowed per account. The maximum is 12 channels.");
                }
                else {
                    $("#createChannelModal #txtChannelNameForm").replaceWith("<input id='txtChannelNameForm'  placeholder=' Channel Name ' class='cap'/>");
                    $("#createChannelModal #imgChannelAvatar").removeAttr("src").attr("src", "/images/comingSoonBG.jpg");
                    $("#createChannelModal #TextURL").text("").text("Channel URL");
                    $("#createChannelModal #btnCreate").show();
                    $("#createChannelModal .pageTitle").text("").text("create your own channel");
                    $("#createChannelModal #btnDelete").hide();
                    $("#createChannelModal #btnUpdate").hide();
                    $("#createChannelModal #txtAreaChannelDesc").val('');
               
                
               
                    this.DisableClickOnSubscribtion();
                    $("#createChannelModal").lightbox_me({
                        centered: true,
                        onLoad: function () {
                            $(".createChannelForm").removeClass("upgradeChannel");
                            if ($("#toggleEmbeddedChannel").is(".inputEmbeddedON")) {

                                $("#toggleEmbeddedChannel").removeClass("inputEmbeddedON").addClass("inputEmbeddedOFF");

                                embedEnable = false;

                                CreateChannel.ResetEmbeddingOptions();
                                //hide fileds 

                            }
                            CreateChannel.ResetEmbeddingOptions();
                            $(".createModalRight li.subcribtionOFF").hide();
                            CreateChannel.InitCropChannelAvatar(true);
                            CreateChannel.InitCropChannelLogo();
                            CreateChannel.InitChannelSubscribtion();
                            matureContentEnabledCreateChannel = false;
                            showPlayerControls = true;
                            embedOnlyMode = false;
                            privateVideoModeEnabledForCreateChannel = false;
                            $("#toggleControls").removeClass("controlsInputOFF").addClass("controlsInputON");
                            $("#toggleMatureContent").removeClass("matureInputON").addClass("matureInputOFF");
                            $("#toggleEmbedOnlyMode").removeClass("inputEmbeddedON").addClass("inputEmbeddedOFF");
                            $("#togglePrivateMode").removeClass("privateInputON").addClass("privateInputOFF");
                            if (embedEnable) {
                          
                                $("#toggleMatureContent").removeClass("opacityNotActive");
                                $("#toggleMatureContent").attr("onclick", "CreateChannel.ToggleMatureContent()");
                           
                            }
                            else {
                                $("#toggleMatureContent").addClass("opacityNotActive");
                                $("#toggleMatureContent").removeAttr("onclick");
                            }

                        },
                        onClose: function () {
                       
                            RemoveOverlay();
                            CreateChannel.ClearAllCreateChannelFields();
                        },
                        closeSelector: "close"
                    });
                }
            },
            InitChannelSubscribtion: function () {
                userChannelEntitlments = CreateChannel.GetUserChannelEntitlements();
             
                channelsAvailableToCustomLabelCount = userChannelEntitlments.responseJSON.d.Response.ChannelsAvailableToCustomLabelCount;
                channelsAvailableToEmbedCount = userChannelEntitlments.responseJSON.d.Response.ChannelsAvailableToEmbedCount;
                channelsAvailableToPasswordProtectCount = userChannelEntitlments.responseJSON.d.Response.ChannelsAvailableToPasswordProtectCount;
                channelsAvailableToWhiteLabelCount = userChannelEntitlments.responseJSON.d.Response.ChannelsAvailableToWhiteLabelCount;
                channelsAvailableToMute = userChannelEntitlments.responseJSON.d.Response.ChannelsAvailableToMute;
                channelsAvailableToMatureContentCount = userChannelEntitlments.responseJSON.d.Response.ChannelsAvailableToMatureContentCount;
              

                channelsEmbeddedCount = userChannelEntitlments.responseJSON.d.Response.ChannelsEmbeddedCount;
                channelsPurchasedCount = userChannelEntitlments.responseJSON.d.Response.ChannelsPurchasedCount;
                customLabelChannelsPurchaseCount = userChannelEntitlments.responseJSON.d.Response.CustomLabelChannelsPurchaseCount;
                customLabeledChannelCount = userChannelEntitlments.responseJSON.d.Response.CustomLabeledChannelCount;
                passwordProtectedChannelCount = userChannelEntitlments.responseJSON.d.Response.PasswordProtectedChannelCount;
                passwordProtectedChannelsPuchaseCount = userChannelEntitlments.responseJSON.d.Response.PasswordProtectedChannelsPuchaseCount;
                whiteLabeledChannelsCount = userChannelEntitlments.responseJSON.d.Response.WhiteLabeledChannelsCount;
                whiteLabeledChannelsPurchasedCount = userChannelEntitlments.responseJSON.d.Response.WhiteLabeledChannelsPurchasedCount;
                mutedChannelsPurchasedCount = userChannelEntitlments.responseJSON.d.Response.MutedChannelsPurchasedCount;
                matureContentChannelCount = userChannelEntitlments.responseJSON.d.Response.MatureContentChannelCount;
                playerControlsChannelCount = userChannelEntitlments.responseJSON.d.Response.PlayerControlsChannelCount;
                channelsAvailableToPlayerControlsCount = userChannelEntitlments.responseJSON.d.Response.ChannelsAvailableToPlayerControlsCount;
                privateVideosAllowedCount =userChannelEntitlments.responseJSON.d.Response.PrivateVideosAllowedCount;
                privateVideosForChannelsAvailableCount = userChannelEntitlments.responseJSON.d.Response.PrivateVideosForChannelsAvailableCount
                
                
            },

            GetUserChannelEntitlements: function () {
                var webMethodGetUserChannelEntitlements = "/WebServices/UserService.asmx/GetUserChannelEntitlements";
                var channelEntitlements = $.ajax({
                    type: "POST",
                    url: webMethodGetUserChannelEntitlements,
                    data: '{"userId":' + userId + '}',
                    cashe: false,
                    async: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                       
                    },
                    error: function (request, status, error) {
                        void 0;
                    }
                });

                return channelEntitlements;
            },
            ClearAllCreateChannelFields: function () {
                //var queryString = getQueryStringParameterByName("create-channel")
                //void 0;
                //if (queryString == 1) {
                //    var url = window.location.href.split('?')[0];
                //    window.location.replace(url);
                //    void 0;
                //}
                b64 = "";
                b64Logo = "";
               
                $("#createChannelModal #txtChannelNameForm").val("");
                $("#createChannelModal select option[value='0']").attr("selected", "selected");
                $("#createChannelModal #fuChannelAvatar").val("");
                $("#createChannelModal #imgChannelAvatar").removeAttr('src').attr('src', '/images/comingSoonBG.jpg');
                $("#createChannelModal #TextURL").text("").text("Channel URL");
                $("#spnChannelNameError").text("");
                $("#createChannelModal #txtAreaChannelDesc").val('');
                $('#spnChannelLogoErr').hide();
                $("#createChannelModal #txtChannelLogo").removeClass("createChannelError");
                $("#createChannelModal #txtChannelCategory").removeClass("categoryErr");
                $('#spnChannelNameError').hide();
                $('#spnChannelNameErr').hide();
                $("#createChannelModal #txtPasswordProtected").val("");
                var imageEditor1 = '<div class="image-editor1"><div class="image-size-label"></div><div class="cropit-image-preview-container"><div class="cropit-image-preview" ></div></div><div class="select-image-btn">Upload image</div><div class="minImgSize"> (Minimum image size: 200px X 200px)</div><input type="range" class="cropit-image-zoom-input"/><div class="image-size-label">Move cursor to resize image</div><input type="file" class="cropit-image-input"/></div>';
             
                var imageEditor2 = '<div class="image-editor2 logo"><div class="image-size-label"></div><div class="cropit-image-preview-container"><div class="cropit-image-preview" ></div></div><div class="select-image-btn">Upload image</div><div class="minImgSize"> (Minimum image size: 200px X 200px)</div><input type="range" class="cropit-image-zoom-input"/><div class="image-size-label">Move cursor to resize image</div><input type="file" class="cropit-image-input"/></div>';

                $("#txtBranding").removeAttr("checked").attr("checked", "checked");
                $("#imageBranding").removeAttr("checked");
                
                $("#createChannelModal #liImageEditor").html("").html(imageEditor1 + imageEditor2);
                $("#createChannelModal :input ,#createChannelModal textarea, #createChannelModal select, #createChannelModal span").each(function () {
                    $(this).removeClass('categoryErr');
                });


            },
            setErrorMessage: function (msg) {
                $("#spnChannelNameError").text("").append("<br/>").append(msg);

                if (msg == "") {
                    $("#spnChannelNameError").hide();
                }
                else {
                    $("#spnChannelNameError").show();
                }
            },

            openFile: function (event) {

                var input = event.target;

                var reader = new FileReader();
                reader.onload = function () {

                    var dataURL = reader.result;
                    $("#createChannelModal #imgChannelAvatar").attr('src', dataURL)
                                .width(70)
                                .height(70);
                    b64 = dataURL.split("base64,")[1];

                };
                var f = input.files[0];
                fileSize = f.size || f.fileSize;

                //var size = readImage(input.files[0]);
                reader.readAsDataURL(input.files[0]);

                ValidateImage(input.id, input.files[0].size);
            },
            //VALIDATIONS

            InitCropChannelAvatar: function (isCreating) {

                this.GetChannelCategory();
                this.GetLanguage(isCreating);
                
                var filename = "";

                void 0

                $imageCropper = $('#createChannelModal .image-editor1');
                //if (channelPictureUrl != null && channelPictureUrl!==undefined)
                //{

                //    $imageCropper.cropit('imageSrc', channelPictureUrl);
                //}
                $imageCropper.cropit({
                    imageBackground: true,
                    imageBackgroundBorderWidth: 60
                })
                $imageCropper.cropit({ allowCrossOrigin: true });
                $imageCropper.cropit({ allowDragNDrop: true });
                //$imageCropper.cropit('imageSrc', imgSrc);

                $imageCropper.find('.select-image-btn').click(function () {
                    $imageCropper.find('.cropit-image-input').click();
                });


                $imageCropper.find('.export').click(function () {

                    var imageData = $imageCropper.cropit('export', {
                        type: 'image/jpeg',
                        quality: .9,
                        originalSize: true
                    });

                    if (imageData != null) {
                        b64 = imageData.split("base64,")[1];//string fileName, string imageData, int userId
                        if (b64 === undefined || b64 == "undefined") {
                            b64 = "";
                        }



                    }
                });

            },

            InitCropRokuImageAvatarHD: function () {
                $imageCropperHd = $('#ottSettingsModal .image-editor-hd');
                
                //$imageCropperHd.cropit({
                //    imageBackground: true,
                //    imageBackgroundBorderWidth: 10,
                //    allowCrossOrigin: true,
                //    allowDragNDrop: true,
                //    width: 290,
                //    heiht: 218
                //})

                $imageCropperHd.find('.select-image-btn').click(function () {
                    $imageCropperHd.find('.cropit-image-input').click();
                    $imageCropperHd.find('.cropit-image-input').on("change", function () {
                        CreateChannel.getImgData($imageCropperHd.find('.image-preview'), this, $imageCropperHd.find('.img-validation'), 290, 218, $("#hdnHdImageValid"), $("#hdnHdImageData"));
                    });
                });

                
                //$imageCropperHd.find('.export').click(function () {

                //    var imageData = $imageCropperHd.cropit('export', {
                //        type: 'image/png',
                //        originalSize: true
                //    });

                //    if (imageData != null) {
                //        b64 = imageData.split("base64,")[1];//string fileName, string imageData, int userId
                //        if (b64 === undefined || b64 == "undefined") {
                //            b64 = "";
                //        }
                //    }
                //});
            },
            InitCropRokuImageAvatarSD: function () {
                $imageCropperSd = $('#ottSettingsModal .image-editor-sd');

                //$imageCropperSd.cropit({
                //    imageBackground: true,
                //    imageBackgroundBorderWidth: 10,
                //    allowCrossOrigin: true,
                //    allowDragNDrop: true,
                //    width: 246,
                //    heiht: 240
                //})

                $imageCropperSd.find('.select-image-btn').click(function () {
                    $imageCropperSd.find('.cropit-image-input').click();
                    $imageCropperSd.find('.cropit-image-input').on("change", function () {
                        CreateChannel.getImgData($imageCropperSd.find('.image-preview'), this, $imageCropperSd.find('.img-validation'), 246, 140, $("#hdnSdImageValid"), $("#hdnSdImageData"));
                    });
                });

                //$imageCropperSd.find('.export').click(function () {

                //    var imageData = $imageCropperSd.cropit('export', {
                //        type: 'image/png',
                //        quality: .9,
                //        originalSize: true
                //    });

                //    if (imageData != null) {
                //        b64 = imageData.split("base64,")[1];//string fileName, string imageData, int userId
                //        if (b64 === undefined || b64 == "undefined") {
                //            b64 = "";
                //        }
                //    }
                //});
            },
            getImgData: function (target, fs, validation, w, h, isvalid, dataholder) {
                CreateChannel.RokuAppRegenerateRequired();
                const files = fs.files[0];
                if (files) {
                    const fileReader = new FileReader();
                    fileReader.readAsDataURL(files);
                    fileReader.addEventListener("load", function () {
                        var image = new Image();
                        image.src = this.result;
                        $(target).attr("style", "background-image:url(" + image.src + ")");
                        image.onload = function () {
                            $(dataholder).val(image.src);

                            $(validation).empty();
                            $(isvalid).val("1")

                            if (image.width !== w || image.height !== h) {
                                $(validation).append("Image size should be " + w + "X" + h + " exactly<br>");
                                $(isvalid).val("0")
                            }
                            if (image.currentSrc.indexOf("/png;") < 0)
                            {
                                $(validation).append("Image should be in PNG format.");
                                $(isvalid).val("0")
                            }
                        };
                    });
                }
            },
            GenerateDownloadRokuApp: function () {
                var appName = $("#txtRokuAppName").val();
                var adLink = $("#txtRokuAdLink").val();
                var ppLink = $("#txtPrivacyPolicyLink").val();
                var appAbout = $("#txtAboutApp").val();
                var hdImageValid = $("#hdnHdImageValid").val() === "1";
                var sdImageValid = $("#hdnSdImageValid").val() === "1";
                var hdImageData = $("#hdnHdImageData").val();
                var sdImageData = $("#hdnSdImageData").val();

                if (!appName) {
                    $("#appValidation").empty().append("App name is not specified");
                    $("#txtRokuAppName").focus();
                    return;
                }
                if (!hdImageValid) {
                    $("#appValidation").empty().append("Image #1 is not valid");
                    return;
                }
                if (!sdImageValid) {
                    $("#appValidation").empty().append("Image #2 is not valid");
                    return;
                }
                $("#appValidation").empty();
                var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                var params = {
                    AppName: appName,
                    AdLink: adLink,
                    AppAbout: appAbout,
                    PrivacyPolicyLink: ppLink,
                    HdImageData: hdImageData,
                    SdImageData: sdImageData,
                    Username: currChannel.ChannelOwnerUserName,
                    UserId: currChannel.UserId
                };
                $.ajax({
                    type: "POST",
                    url: webMethodGenerateRokuApp,
                    data: JSON.stringify(params),
                    cashe: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (needRokuAppGenerate) {
                            var appFile = response.d;
                            window.location.href = "/RokuGeneratedApps/" + appFile;
                        }
                        CloseOTTSettingsPopup();
                    },
                    complete: function () {
                    },
                    error: function (request, status, error) {
                        void 0;
                    }
                });

            },

            InitCropChannelLogo:function()
            {
                $imageLogoCropper = $('#createChannelModal .image-editor2');
               
               
                $imageLogoCropper.cropit({
                    imageBackground: true,
                    imageBackgroundBorderWidth: 60
                })
                $imageLogoCropper.cropit({ allowCrossOrigin: true });
                $imageLogoCropper.cropit({ allowDragNDrop: true });
                $imageLogoCropper.find('.select-image-btn').click(function (event) {
                   
                    $imageLogoCropper.find('.cropit-image-input').click();
                });


                $imageLogoCropper.find('.export').click(function () {

                    var imageData = $imageLogoCropper.cropit('export', {
                        type: 'image/jpeg',
                        quality: .9,
                        originalSize: true
                    });

                    if (imageData != null) {
                        b64Logo = imageData.split("base64,")[1];//string fileName, string imageData, int userId
                        if (b64Logo === undefined || b64Logo == "undefined") {
                            b64Logo = "";
                        }



                    }
                });

                //if (customLogoUrl != null && customLogoUrl !== undefined) {
                //    console.log(customLogoUrl);
                //    $imageLogoCropper.cropit('imageSrc', customLogoUrl);
                //}
               
            },
            ValidateChannelName: function () {

                var $channelNameInput = $(".createChannelForm #txtChannelNameForm").val();
                var $channelNameControl = $(".createChannelForm #txtChannelNameForm");
                isChannelNameTaken = CreateChannel.CheckIfChannelNameExist($channelNameInput);
                void 0;
                isChannelNameReserved = CreateChannel.CheckIfChannelNameReserved($channelNameInput);
                var isChannelNameValid;
                var regexp = /^[a-z\d\-_\s]+$/i;

                if ($channelNameInput.search(regexp) == -1) {
                    isChannelNameValid = false;
                }
                else {

                    isChannelNameValid = true;
                }

                var isNameValid = $channelNameInput.match()

                //if empty
                if ($channelNameInput.trim() == "") {
                    $channelNameControl.addClass("createChannelError");
                    $("#divBoardContent ul li span#spnChannelNameErr").text("").show();

                    this.setErrorMessage("*Please provide channel name");
                    $("#btnCreate").removeAttr("onclick");
                    return false;
                }
                    //if too short
                else if ($channelNameInput.trim().length < 3 || $channelNameInput.trim().length > 20) {
                    $channelNameControl.addClass("createChannelError");
                    $("#divBoardContent ul li span#spnChannelNameErr").show();
                    this.setErrorMessage("*Channel name must have 3-20 characters only, including spacing");
                    $("#btnCreate").removeAttr("onclick");
                    return false;

                }
                    //if isExist
                else if (isChannelNameTaken) {
                    $channelNameControl.addClass("createChannelError");
                    $("#divBoardContent ul li span#spnChannelNameErr").show();
                    this.setErrorMessage("*Channel name is not available, please choose another channel name");
                    $("#btnCreate").removeAttr("onclick");
                    return false;
                }
                    //if isreserved
                else if (isChannelNameReserved) {
                    $channelNameControl.addClass("createChannelError");
                    $("#divBoardContent ul li span#spnChannelNameErr").show();
                    this.setErrorMessage("*This name may be reserved as a premium name for trademark holders. Please contact us with proof of legal rights to this name, if you wish to have it, or choose another name.");
                    $("#btnCreate").removeAttr("onclick");
                    return false;
                }
                    //if has special characters
                else if (!isChannelNameValid) {
                    $channelNameControl.addClass("createChannelError");
                    $("#divBoardContent ul li span#spnChannelNameErr").show();
                    this.setErrorMessage("*No double spacing and special characters, like &,!,', /,#,? in the channel name");
                    $("#btnCreate").removeAttr("onclick");
                    return false;
                }
                else {
                    $channelNameControl.removeClass("createChannelError");
                    $("#divBoardContent ul li span#spnChannelNameErr").hide();
                    this.setErrorMessage("");
                    $("#createChannelModal #TextURL").text("").text(domainName + "/" + userUrl + "/" + $channelNameInput.replace(/\s/g, ""));

                    if (document.getElementById("btnCreate").hasAttribute("onclick") == false) {
                        document.getElementById("btnCreate").setAttribute("onclick", "CreateChannel.CreateNewChannel()");
                    }

                    return true;
                }

            },

            CheckIfChannelNameExist: function (channelName) {
                var isExist = $.ajax({
                    type: "POST",
                    url: webMethodCheckChannelNameIsTaken,
                    data: '{"channelName":' + "'" + channelName + "'" + '}',
                    cashe: false,
                    async: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {


                    },
                    error: function (request, status, error) {
                        void 0;
                    }
                });
                return isExist.responseJSON.d;
            },

            CheckIfChannelNameReserved: function (channelName) {
                var isExist = $.ajax({
                    type: "POST",
                    url: webMethodCheckChannelNameIsReserved,
                    data: '{"channelName":' + "'" + channelName + "'" + '}',
                    cashe: false,
                    async: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {

                    },
                    error: function (request, status, error) {
                        void 0;
                    }
                });
                return isExist.responseJSON.d;
            },

            ValidateImage: function (fuId, fileSize) {
                //check image size
                var maxUploadSize = 300 * 1024;// max upload size 200kb
                filename = $("#createChannelModal #fuChannelAvatar").val();
         
                var validExtensions = /(\.jpg|\.jpeg|\.png)$/i;
           
                if (fileSize != null) {
                    if (fileSize > maxUploadSize) {
                        $("#createChannelModal #txtChannelLogo").addClass("createChannelError");
                        $("#divBoardContent ul li span#spnChannelLogoErr").show();
                        this.setErrorMessage("*Image is too big. Please choose a smaller image. Max image size is 30MB");
                        $("#btnCreate").removeAttr("onclick");
                        return false;
                    }
               
                        //check image extention
                    else if (!validExtensions.test(filename)) {
                        $("#createChannelModal #txtChannelLogo").addClass("createChannelError");
                        $("#divBoardContent ul li span#spnChannelLogoErr").show();
                        this.setErrorMessage("*Image have to be .jpg or .png files only");
                        $("#btnCreate").removeAttr("onclick");
                        return false;
                    }
                    else {
                        $("#createChannelModal #txtChannelLogo").removeClass("createChannelError");
                        $("#divBoardContent ul li span#spnChannelLogoErr").hide();
                        this.setErrorMessage("");

                        if (document.getElementById("btnCreate").hasAttribute("onclick") == false) {
                            document.getElementById("btnCreate").setAttribute("onclick", "CreateChannel.CreateNewChannel()");
                        }

                        return true;
                    }
                }
                else {
                    if (document.getElementById("btnCreate").hasAttribute("onclick") == false) {
                        document.getElementById("btnCreate").setAttribute("onclick", "CreateChannel.CreateNewChannel()");
                    }
                    return true;
                }

            },


            //END VALIDATIONS


            //CREATE CHANNEL 
            CreateNewChannel: function () {

                var isLogoModeActive = false;
                var imageData = $imageCropper.cropit('export', {
                    type: 'image/jpeg',
                    quality: .9,
                    originalSize: true
                });

                var imageLogoData=$imageLogoCropper.cropit('export', {
                    type: 'image/png',
                    quality: .9,
                    originalSize: true
                });

                if (imageData != null) {
                    b64 = imageData.split("base64,")[1];//string fileName, string imageData, int userId
                    if (b64 === undefined || b64 == "undefined") {
                        b64 = "";
                    }
                }

                if (imageLogoData != null) {
                    b64Logo = imageLogoData.split("base64,")[1];//string fileName, string imageData, int userId
                    if (b64Logo === undefined || b64Logo == "undefined") {
                        b64Logo = "";
                        
                    }
                    else
                    {
                        isLogoModeActive = true;
                    }
                }

                var isChannelNameValid = CreateChannel.ValidateChannelName();
                var isImageValid = CreateChannel.ValidateImage();
                var selectedCategoryValue = $("#createChannelModal select#txtChannelCategory option:selected").val();
                var selectedLanguageValue = $("#createChannelModal select#slctLang option:selected").val();
                var channelDescription = $("#txtAreaChannelDesc").val();
                void 0;
                //remove error class from all controls in form
                $("#createChannelModal :input ,#createChannelModal textarea, #createChannelModal select, #createChannelModal span").each(function () {
                    $(this).removeClass('categoryErr');
                });
          
                if (selectedCategoryValue == "0") {
                
                    $("#createChannelModal #txtChannelCategory").addClass("categoryErr");
                    this.setErrorMessage("*Please select channel category");
                    return;
                }
                if (selectedLanguageValue == "0") {

                    $("#createChannelModal select#slctLang").addClass("categoryErr");
                    this.setErrorMessage("*Please select channel language");
                    return;
                }
                if (b64 == "" || b64 == null)
                {
                    $("#createChannelModal #txtChannelLogo").addClass("createChannelError");
                    $("#divBoardContent ul li span#spnChannelLogoErr").show();
                    this.setErrorMessage("*Please add channel avatar");
               
                    return;
                }
                if (isChannelNameValid && isImageValid) {
                    $("#loadingDiv ").show();
                    var categoryId = selectedCategoryValue;
                    //var filename = $("#createChannelModal #fuChannelAvatar").val().split("\\").join("/");
                    var filename = "";
                    var channelName = $(".createChannelForm #txtChannelNameForm").val().replace(/ +/g, " ")
                    var channelUrl = channelName.replace(/\s/g, "");
                    if (b64 === undefined || b64 == "undefined") {
                        b64 = "";
                    }
                    if (b64Logo === undefined || b64Logo == "undefined") {
                        b64Logo = "";
                    }
                    var domainName = $("#txtEmbeddingDomain").val();

                    var channelPassword = $("#txtPasswordProtected").val();
                    var regexp = /^[a-z\d\-_\s]+$/i;
                    
                    var customLabelNewChannel = $("#txtBrandingName").val();
                    //if (customLabelNewChannel.search(regexp) == -1) {
                    //    $("#loadingDiv").hide();
                    //    alertify.error("No double spacing and special characters, like &,!,', /,#,? in the custom label");
                    //    return;
                    //}
                   
                    if (embedEnabledNewChannel) {

                        if (domainName == "" || domainName == undefined || domainName == null) {
                            alertify.error("please add domain name");
                            return;
                        }
                        //var isDomainVerified = CreateChannel.VerifyDomainName(domainName)
                        //if (!isDomainVerified) {
                        //    alertify.error("please add valid domain name");
                        //    return;
                        //}
                    }

                    if (isPasswordProtectedNewChannel) {
                        if (channelPassword == "" || channelPassword == undefined || channelPassword == null) {
                            alertify.error("please add password");
                            return;
                        }
                    }
                    else {
                        channelPassword = "";

                    }
                    if (!channelsAvailableToCustomLabelCount) {
                        customLabelNewChannel = "";
                    }
                    
                    //if (($('#txtBranding').is(':checked')) && (customLabelNewChannel.trim().length = 0))
                    //{
                    //    this.setErrorMessage("*Please add custom barnding text");
                    //    return;
                    //}

                    if (typeof liveFirst !== 'undefined') {
                        playLiveFirst = liveFirst;
                    }
                    else {
                        void 0;
                    }
                    //bool isWhiteLabeled, string channelPassword, bool embedEnabled,bool muteOnStartup,  string customLabel, string subscriberDomain
                    var params = '{"categoryId":' + categoryId + ',"channelName":' + "'" + channelName + "'" + ',"channelUrl":' + "'" + channelUrl + "'" + ',"channelImgData":' + "'" + b64 + "'" + ',"userId":' + userId + ',"fileName":' + "'" + filename + "'" + ',"channelDescription":' + "'" + channelDescription + "'" + ',"languageId":' + selectedLanguageValue + ',"isWhiteLabeled":' + isWhiteLabeledNewChannel + ',"channelPassword":' + "'" + channelPassword + "'" + ',"embedEnabled":' + embedEnabledNewChannel + ',"muteOnStartup":' + muteOnStartupNewChannel + ',"customLabel":' + "'" + customLabelNewChannel + "'" + ',"customLogoImgData":' + "'" + b64Logo + "'" + ',"subscriberDomain":' + "'" + domainName + "'" + ',"embedOnlyMode":' + embedOnlyMode + ',"matureContentEnabled":' + matureContentEnabledCreateChannel + ',"showPlayerControls":' + showPlayerControls + ',"isPrivate":' + privateVideoModeEnabledForCreateChannel + ',"isLogoModeActive":' + isLogoModeActive + ',"playLiveFirst":' + playLiveFirst + ',"keepGuideOpen":' + keepGuideOpen +'}';
                    //console.log(params)
                    if (isImageValid) {
                        $.ajax({
                            type: "POST",
                            url: webMethodCreateNewChannel,
                            data: params,
                            cashe: false,
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                if (response.d == true) {
                                    void 0;
                                    CreateChannel.ClearAllCreateChannelFields();
                                
                                    window.location.href = "/" + userUrl + "/my-studio/" + channelUrl;

                                }
                                else {

                                    alertify.error("*Fail create new channel, please try again later");
                                }


                            },
                            complete: function () {
                                $("#loadingDiv").hide();
                                $('#createChannelModal').trigger('close');
                            },
                            error: function (request, status, error) {
                                void 0;
                            }
                        });
                    }

                }


            },
            OpenEmbededArea: function () {

                $('#embededCodeCopyArea').show();

                var embededCode = '<div id="uzerDiv" style="max-width: 100%; height: auto; display: block; margin: auto;">';
                embededCode += '<div id="embeddedDiv" style="position: relative; min-height: 200px; overflow: hidden; height: 500px">';
                embededCode += '<meta name="viewport" content="width=device-width">';
                embededCode += '<iframe id="strimm-iframe" allow="autoplay" allowfullscreen width="100%" height="100%" style="margin: 0; border: none; position: absolute; top: 0; left: 0; width: 100%; height: 100%; overflow: hidden;" ';

                var iframeDomain = "https://iframe.strimm.com/";

                if (!(domainName.startsWith("www.strimm.com") ||
                    domainName.startsWith("strimm.com"))) {
                    iframeDomain = "http://iframe-stage.strimm.com/";
                }

                var guideMode = "float";

                if (keepGuideOpen == false) {
                    guideMode = "fixed;"
                }

                var iframeUrl = iframeDomain + userUrl + "/" + channelUrl + "?guideMode=" + guideMode + "&keepGuideOpened=" + keepGuideOpen;

                embededCode += 'src="' + iframeUrl + '">';
                embededCode += '</iframe></div></div>';
                embededCode = '<script src="' + iframeDomain + 'embedded-iframe.js"></script>' + embededCode;

                $('#embededCodeCopyArea').val(embededCode);
                $('#embededCodeCopyArea').select();

                this.UpdateFbEmbededContent();
            },

            ResetEmbeddingOptions: function () {

                $("#txtEmbeddingDomain").val("").hide();
                $("#toggleWhiteLabel").removeClass("inputWhiteLabelON").addClass("inputWhiteLabelOFF");
                $("#toggleMute").removeClass("inputMusicON").addClass("inputMusicOFF");
                $("#toggleLifeFirst").removeClass("inputLiveFirstON").addClass("inputMusicOFF");
                $("#toggleKeepGuide").removeClass("inputKeepOpenON").addClass("inputKeepOpenOFF");
                if ($("#togglePassword").hasClass("inputPasswordON"))
                {
                    $("#togglePassword").removeClass("inputPasswordON").addClass("inputPasswordOFF");
                }
                
                $("#txtPasswordProtected, #txtBrandingName").val("").hide();
                $(".showpasswordlabel").hide();
                $("#toggleCustomBranding").removeClass("inputBrandingON").addClass("inputBrandingOFF");
                $("#txtPasswordProtected").val("");
                embedEnable = false;
                isPasswordProtected = false;
                isWhiteLabeled = false;
                muteOnStartup = false;
                playLiveFirst = false;
                liveFirst = false;
                keepGuideOpen = false;




            },
            CloseEmbededArea: function () {
                $('#embededCodeCopyArea').val("").hide();

                $('.embeddedCode').removeAttr("embedActive").removeAttr('onclick').attr("onclick", "OpenEmbededArea()");
                this.ResetEmbeddingOptions()
            },
            //UpdateSubscribtions: function (subscribtionName, value) {

            //    var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
          
            //    this.InitChannelSubscribtion();
            //},

            ToggleMatureContent: function () {
                var isUpgradeChannelModalUp = $("#createChannelModal .createChannelForm ").hasClass("upgradeChannel")
                $("#spnSubscribtionError").text("");
                if (isUpgradeChannelModalUp == false) {

                 
                    if (channelsAvailableToMatureContentCount  <= 0) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for mature content");
                        return;

                    }
                    else {
                        if ($("#toggleMatureContent").is(".matureInputOFF")) {

                            $("#toggleMatureContent").removeClass("matureInputOFF").addClass("matureInputON");
                          
                            matureContentEnabledCreateChannel = true;
                            
                        }
                        else if ($("#toggleMatureContent").is(".matureInputON")) {

                            $("#toggleMatureContent").removeClass("matureInputON").addClass("matureInputOFF");
                          
                            matureContentEnabledCreateChannel = false;
                           
                        }
                    }

                }

                else {
                    var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                    void 0
                    if (channelsAvailableToMatureContentCount <= 0 && currChannel.MatureContentEnabled == false) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for this option");
                        return;

                    }
                    else {
                        if ($("#toggleMatureContent").is(".matureInputOFF")) {

                            $("#toggleMatureContent").removeClass("matureInputOFF").addClass("matureInputON");
                          
                            matureContentEnabledCreateChannel = true;
                        }
                        else if ($("#toggleMatureContent").is(".matureInputON")) {

                            $("#toggleMatureContent").removeClass("matureInputON").addClass("matureInputOFF");
                         
                            matureContentEnabledCreateChannel = false;
                        }

                    }

                }

            },

            TogglePrivateVideoMode: function()
            {
                var isUpgradeChannelModalUp = $("#createChannelModal .createChannelForm ").hasClass("upgradeChannel")
                $("#spnSubscribtionError").text("");
                if (isUpgradeChannelModalUp == false) {


                    if (privateVideosForChannelsAvailableCount <= 0) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for this option");
                        return;

                    }
                    else {
                        if ($("#togglePrivateMode").is(".privateInputOFF")) {

                            $("#togglePrivateMode").removeClass("privateInputOFF").addClass("privateInputON");

                            privateVideoModeEnabledForCreateChannel = true;
                           

                        }
                        else if ($("#togglePrivateMode").is(".privateInputON")) {

                            $("#togglePrivateMode").removeClass("privateInputON").addClass("privateInputOFF");

                            privateVideoModeEnabledForCreateChannel = false;
                            

                        }
                    }
                    void 0

                }

                else {
                    var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                    void 0
                    if (privateVideosForChannelsAvailableCount <= 0 && currChannel.IsPrivate == false) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for this option");
                        return;

                    }
                    else {
                        if ($("#togglePrivateMode").is(".privateInputOFF")) {

                            $("#togglePrivateMode").removeClass("privateInputOFF").addClass("privateInputON");

                            privateVideoModeEnabled = true;
                        }
                      else if ($("#togglePrivateMode").is(".privateInputON")) {

                          $("#togglePrivateMode").removeClass("privateInputON").addClass("privateInputOFF");

                            privateVideoModeEnabled = false;
                        }

                    }
                    void 0
                }
            },

            RokuAppRegenerateRequired: function () {
                needRokuAppGenerate = true;
                $("#genRokuAppBtn").text("Generate and download your ROKU app");
            },

            ToggleAddChannelToRoku: function () {
                if ($("#toggleAddChannelToRoku").hasClass("inputEmbeddedOFF")) {
                    if (!hasProfPlusPlan) {
                        $("#addToRokuError").empty().append("Please subscribe to Professional Plus plan to use this feature");
                        return;
                    }
                    if (!hasValidRokuVideos) {
                        $("#addToRokuError").empty().append("Your channel contains videos that are not compatible with Roku");
                        return;
                    }
                    $("#toggleAddChannelToRoku").removeClass("inputEmbeddedOFF").addClass("inputEmbeddedON");
                    $(".ottData").show();
                    $(".ottDemo").hide();
                    CreateChannel.UpsertChannelRokuSettings(true);
                }
                else if($("#toggleAddChannelToRoku").is(".inputEmbeddedON")) {
                    $("#toggleAddChannelToRoku").removeClass("inputEmbeddedON").addClass("inputEmbeddedOFF");
                    $(".ottData").hide();
                    $(".ottDemo").show();
                    CreateChannel.UpsertChannelRokuSettings(false);
                }
            },

            ToggleEmbedEnabled: function () {
                var isUpgradeChannelModalUp = $("#createChannelModal .createChannelForm ").hasClass("upgradeChannel");
                $("#spnSubscribtionError").text("");
                //For new Channel createion isUpgradeChannelModalUp = false
                if (isUpgradeChannelModalUp == false) {
               
                    var isChannelNameValid = CreateChannel.ValidateChannelName();
                    if (isChannelNameValid) {
                        var channelName = $(".createChannelForm #txtChannelNameForm").val().replace(/ +/g, " ")
                        channelUrl = channelName.replace(/\s/g, "");

                        if ($("#toggleEmbeddedChannel").hasClass("inputEmbeddedOFF")) {
                      
                            if (channelsAvailableToEmbedCount <= 0 ) {
                                $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe to embed this channel");
                                return;

                            }

                            $("#toggleEmbeddedChannel").removeClass("inputEmbeddedOFF").addClass("inputEmbeddedON");

                            embedEnabledNewChannel = true;
                            $(".createModalRight li.subcribtionOFF").show();
                            //show fileds
                            CreateChannel.OpenEmbededArea()
                            $("#txtEmbeddingDomain").show();
                            $("#divBoardContent ul li span.copyText").show();
                            $(".subscrEmbed").show();

                            this.EnableClickOnSubscribtion();

                        }
                        else if ($("#toggleEmbeddedChannel").is(".inputEmbeddedON")) {

                            $("#toggleEmbeddedChannel").removeClass("inputEmbeddedON").addClass("inputEmbeddedOFF");
                            embedEnabledNewChannel = false;
                            CreateChannel.CloseEmbededArea();
                            //hide fileds 
                            $("#txtEmbeddingDomain").hide();
                            $("#divBoardContent ul li span.copyText").hide();
                            $(".subscrEmbed").hide();

                            this.DisableClickOnSubscribtion();


                        }
                    }
                }
                else {
                    if ($("#toggleEmbeddedChannel").is(".inputEmbeddedOFF")) {
                    
                        var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                        if (channelsAvailableToEmbedCount <= 0 && currChannel.EmbedEnabled == false) {
                            void 0
                            $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe to embed this channel");
                            return;

                        }

                        $("#toggleEmbeddedChannel").removeClass("inputEmbeddedOFF").addClass("inputEmbeddedON");
                        embedEnable = true;
                        //show fileds
                        CreateChannel.OpenEmbededArea()
                        $(".createModalRight li.subcribtionOFF").show();
                        $("#txtEmbeddingDomain").show();
                        $("#divBoardContent ul li span.copyText").show();
                        $(".subscrEmbed").hide();
                        $(".subcribtion").removeClass("opacityNotActive");
                        this.EnableClickOnSubscribtion();
                    }
                    else if ($("#toggleEmbeddedChannel").is(".inputEmbeddedON")) {

                        $("#toggleEmbeddedChannel").removeClass("inputEmbeddedON").addClass("inputEmbeddedOFF");
                        embedEnable = false;
                        CreateChannel.CloseEmbededArea();
                        //hide fileds 
                        $("#txtEmbeddingDomain").hide();
                        $("#divBoardContent ul li span.copyText").hide();
                        $(".subscrEmbed").show();
                        $(".subcribtion").addClass("opacityNotActive");
                        this.DisableClickOnSubscribtion();


                    }
                }
            },

            ToggleWhiteLabel: function () {
                var isUpgradeChannelModalUp = $("#createChannelModal .createChannelForm ").hasClass("upgradeChannel")
                $("#spnSubscribtionError").text("");
                //For new Channel createion isUpgradeChannelModalUp = false
                if (isUpgradeChannelModalUp == false) {
                    if (channelsAvailableToWhiteLabelCount <= 0) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for white-label option");
                        return;
                    }
                    else {
                        if ($("#toggleWhiteLabel").is(".inputWhiteLabelOFF")) {

                            $("#toggleWhiteLabel").removeClass("inputWhiteLabelOFF").addClass("inputWhiteLabelON");
                            isWhiteLabeledNewChannel = true;
                        }
                        else if ($("#toggleWhiteLabel").is(".inputWhiteLabelON")) {

                            $("#toggleWhiteLabel").removeClass("inputWhiteLabelON").addClass("inputWhiteLabelOFF");
                            isWhiteLabeledNewChannel = false;

                        }
                    }
                }
                else {
                    var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                    if (channelsAvailableToWhiteLabelCount <= 0 && currChannel.IsWhiteLabeled==false) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for white-label option");
                        return;
                    }
                    else {
                        if ($("#toggleWhiteLabel").is(".inputWhiteLabelOFF")) {

                            $("#toggleWhiteLabel").removeClass("inputWhiteLabelOFF").addClass("inputWhiteLabelON");
                            isWhiteLabeled = true;
                        }
                        else if ($("#toggleWhiteLabel").is(".inputWhiteLabelON")) {

                            $("#toggleWhiteLabel").removeClass("inputWhiteLabelON").addClass("inputWhiteLabelOFF");
                            isWhiteLabeled = false;

                        }
                    }

                }

            },

            ToggleMute: function () {
                var isUpgradeChannelModalUp = $("#createChannelModal .createChannelForm ").hasClass("upgradeChannel")
                $("#spnSubscribtionError").text("");
                if (isUpgradeChannelModalUp == false) {

                    void 0;
                    if (channelsAvailableToMute <= 0) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for this option");
                        return;

                    }
                    else {
                        if ($("#toggleMute").is(".inputMusicOFF")) {

                            $("#toggleMute").removeClass("inputMusicOFF").addClass("inputMusicON");
                            $("span.muteRight").text("").text("Sound ON")
                            muteOnStartupNewChannel = false;
                            muteOnStartup = false;
                        }
                        else if ($("#toggleMute").is(".inputMusicON")) {

                            $("#toggleMute").removeClass("inputMusicON").addClass("inputMusicOFF");
                            $("span.muteRight").text("").text("Sound OFF")
                            muteOnStartupNewChannel = true;
                            muteOnStartup = true;
                        }
                    }

                }

                else {
                    var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                    void 0;
                    if (channelsAvailableToMute <= 0 && currChannel.MuteOnStartup==false) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for this option");
                        return;

                    }
                    else {
                        if ($("#toggleMute").is(".inputMusicOFF")) {

                            $("#toggleMute").removeClass("inputMusicOFF").addClass("inputMusicON");
                            $("span.muteRight").text("").text("Sound ON")
                            muteOnStartup = false;
                        }
                        else if ($("#toggleMute").is(".inputMusicON")) {

                            $("#toggleMute").removeClass("inputMusicON").addClass("inputMusicOFF");
                            $("span.muteRight").text("").text("Sound OFF")
                            muteOnStartup = true;
                        }

                    }

                }



            },
            ToggleLifeFirst: function () {
                var isUpgradeChannelModalUp = $("#createChannelModal .createChannelForm ").hasClass("upgradeChannel")
                $("#spnSubscribtionError").text("");
                if($("#toggleLifeFirst").is(".inputLiveFirstOFF"))
                {
                    void 0
                    liveFirst = true;
                    $("#toggleLifeFirst").removeClass("inputLiveFirstOFF").addClass("inputLiveFirstON")
                }
                else
                {
                    void 0
                    liveFirst = false;
                    $("#toggleLifeFirst").removeClass("inputLiveFirstON").addClass("inputLiveFirstOFF")
                }



            },
            ToggleKeepGuide: function () {
                var isUpgradeChannelModalUp = $("#createChannelModal .createChannelForm ").hasClass("upgradeChannel")
                $("#spnSubscribtionError").text("");
                if ($("#toggleKeepGuide").is(".inputKeepGuideOFF")) {
                    void 0
                    keepGuideOpen = true;
                    $("#toggleKeepGuide").removeClass("inputKeepGuideOFF").addClass("inputKeepGuideON")
                }
                else {
                    void 0
                    keepGuideOpen = false;
                    $("#toggleKeepGuide").removeClass("inputKeepGuideON").addClass("inputKeepGuideOFF")
                }

                this.OpenEmbededArea();
                this.UpdateFbEmbededContent();
            },
            UpdateFbEmbededContent: function () {
                var iframeDomain = "https://iframe.strimm.com/";

                if (!(domainName.startsWith("www.strimm.com") ||
                      domainName.startsWith("strimm.com"))) {
                    iframeDomain = "http://iframe-stage.strimm.com/";
                }

                var guideMode = "float";

                if (keepGuideOpen == false) {
                    guideMode = "fixed";
                }
                var facebookEmbedUrl = iframeDomain + userUrl + "/" + channelUrl + "?guideMode=" + guideMode + "&keepGuideOpened=" + keepGuideOpen;

                $("#facebookCodeCopyArea").val('');
                $("#facebookCodeCopyArea").val(facebookEmbedUrl);
                $('#facebookCodeCopyArea').select();
            },
            TogglePassword: function () {
                var isUpgradeChannelModalUp = $("#createChannelModal .createChannelForm ").hasClass("upgradeChannel")
                $("#spnSubscribtionError").text("");
                if (isUpgradeChannelModalUp == false) {
                    if (channelsAvailableToPasswordProtectCount <= 0) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for password protection");
                        return;

                    }
                    else {
                        if ($("#togglePassword").is(".inputPasswordOFF")) {

                            $("#togglePassword").removeClass("inputPasswordOFF").addClass("inputPasswordON");
                            isPasswordProtectedNewChannel = true;
                            $("#txtPasswordProtected").show();
                            $("#showPassUpgrade").show();
                            $(".showpasswordlabel").show();
                            $("#txtPasswordProtected").val(decodeBase64(channelPassword));
                            $(".upgragePackg.password").hide();

                        }
                        else if ($("#togglePassword").is(".inputPasswordON")) {

                            $("#togglePassword").removeClass("inputPasswordON").addClass("inputPasswordOFF");
                            isPasswordProtectedNewChannel = false;
                            $("#txtPasswordProtected").hide();
                            $("#txtPasswordProtected").val("");
                            $(".showpasswordlabel").hide();
                            $(".upgragePackg.password").show();

                        }
                    }
                }
                else {
                    var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                    if ((channelsAvailableToPasswordProtectCount <= 0) && (currChannel.ChannelPassword == null || currChannel.ChannelPassword=="")) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for password protection");
                        return;

                    }
                    else {
                        if ($("#togglePassword").is(".inputPasswordOFF")) {

                            $("#togglePassword").removeClass("inputPasswordOFF").addClass("inputPasswordON");
                            isPasswordProtected = true;
                            $("#txtPasswordProtected").show();
                            $("#txtPasswordProtected").val("");
                            $("#showPassUpgrade").show();
                            $(".showpasswordlabel").show();
                            $(".upgragePackg.password").hide();


                        }
                        else if ($("#togglePassword").is(".inputPasswordON")) {

                            $("#togglePassword").removeClass("inputPasswordON").addClass("inputPasswordOFF");
                            isPasswordProtected = false;
                            $("#txtPasswordProtected").hide();
                            $("#txtPasswordProtected").val("");
                            $(".showpasswordlabel").hide();
                            $(".upgragePackg.password").show();
                        }
                    }

                }

            },

            ToggleCustomBranding: function () {
                var isUpgradeChannelModalUp = $("#createChannelModal .createChannelForm ").hasClass("upgradeChannel")
                $("#spnSubscribtionError").text("");
                if (isUpgradeChannelModalUp == false) {
                    if (channelsAvailableToCustomLabelCount <= 0) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for custom branding");
                        return;

                    }
                    else {
                        if ($("#toggleCustomBranding").is(".inputBrandingOFF")) {

                            $("#toggleCustomBranding").removeClass("inputBrandingOFF").addClass("inputBrandingON");

                            customLabelNewChannel = true;
                            $("#txtBrandingName").show();
                            
                            $("#customBrandingOptions").show();
                            $(".upgragePackg.customLabel").hide();
                            //this.InitCropChannelLogo();
                        }
                        else if ($("#toggleCustomBranding").is(".inputBrandingON")) {

                            $("#toggleCustomBranding").removeClass("inputBrandingON").addClass("inputBrandingOFF");
                            customLabelNewChannel = false;
                            $("#txtBrandingName").val("");
                            customLabel = "";
                            $('.image-editor2.logo input.cropit-image-input').val('');
                            $('.image-editor2.logo .cropit-preview').removeClass('cropit-image-loaded');
                            $('.image-editor2.logo .cropit-preview-image').removeAttr('style');
                            $('.image-editor2.logo .cropit-preview-image').attr('src', '');
                            $("#txtBrandingName").hide();
                            $(".image-editor2.logo").hide();
                            $("#customBrandingOptions").hide();
                            $(".upgragePackg.customLabel").show();
                        }
                    }

                }
                else {
                    var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                    if ((channelsAvailableToCustomLabelCount <= 0) && (currChannel.CustomLabel == null || currChannel.CustomLabel=="")) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for custom branding");
                        return;

                    }
                    else {
                        if ($("#toggleCustomBranding").is(".inputBrandingOFF")) {

                            $("#toggleCustomBranding").removeClass("inputBrandingOFF").addClass("inputBrandingON");
                            enabelCutomBranding = true;
                            $("#txtBrandingName").show();
                            $("#customBrandingOptions").show();
                            $(".upgragePackg.customLabel").hide();
                            this.InitCropChannelLogo();
                        }
                        else if ($("#toggleCustomBranding").is(".inputBrandingON")) {

                            $("#toggleCustomBranding").removeClass("inputBrandingON").addClass("inputBrandingOFF");
                            enabelCutomBranding = false;
                            $("#txtBrandingName").hide();
                            $("#txtBrandingName").val("");
                            $('.image-editor2.logo input.cropit-image-input').val('');
                            $('.image-editor2.logo .cropit-preview').removeClass('cropit-image-loaded');
                            $('.image-editor2.logo .cropit-preview-image').removeAttr('style');
                            $('.image-editor2.logo .cropit-preview-image').attr('src', '');
                            $("#customBrandingOptions").hide();
                            $(".image-editor2.logo").hide();
                            $(".upgragePackg.customLabel").show();

                        }

                    }

                }

            },

            TextBrandingChange:function()
            {
                $(".inputCustomBranding").show();
                $(".image-editor2.logo").hide();
                $('.image-editor2.logo input.cropit-image-input').val('');
                $('.image-editor2.logo .cropit-preview').removeClass('cropit-image-loaded');
                $('.image-editor2.logo .cropit-preview-image').removeAttr('style');
                $('.image-editor2.logo .cropit-preview-image').attr('src', '');
                var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                $("#txtBrandingName").val(currChannel.CustomLabel);

               

            },

            ImageBrandingChange:function()
            {
               // $("#txtBrandingName").val("");
                $(".inputCustomBranding").hide();
                $(".image-editor2.logo").show();
            },

            ToggleShowControls:function ()
            {
                var isUpgradeChannelModalUp = $("#createChannelModal .createChannelForm ").hasClass("upgradeChannel")
                $("#spnSubscribtionError").text("");
                //For new Channel createion isUpgradeChannelModalUp = false
                if (isUpgradeChannelModalUp == false) {
                    if (channelsAvailableToPlayerControlsCount <= 0) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for player controls");
                        return;
                    }
                    else {
                        if ($("#toggleControls").is(".controlsInputOFF")) {

                            $("#toggleControls").removeClass("controlsInputOFF").addClass("controlsInputON");
                            showPlayerControls = true;
                        }
                        else if ($("#toggleControls").is(".controlsInputON")) {

                            $("#toggleControls").removeClass("controlsInputON").addClass("controlsInputOFF");
                            showPlayerControls = false;

                        }
                    }
              
                }
                else {
                    var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                    if (channelsAvailableToPlayerControlsCount <= 0) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for player controls");
                        return;
                    }
                    else {
                        if ($("#toggleControls").is(".controlsInputOFF")) {

                            $("#toggleControls").removeClass("controlsInputOFF").addClass("controlsInputON");
                            showPlayerControls = true;
                        }
                        else if ($("#toggleControls").is(".controlsInputON")) {

                            $("#toggleControls").removeClass("controlsInputON").addClass("controlsInputOFF");
                            showPlayerControls = false;

                        }
                    }

                }
           
            },

            ToggleEmbedOnlyMode: function () {
                var isUpgradeChannelModalUp = $("#createChannelModal .createChannelForm ").hasClass("upgradeChannel")
                $("#spnSubscribtionError").text("");
                //For new Channel createion isUpgradeChannelModalUp = false
                if (isUpgradeChannelModalUp == false) {
                    //if (channelsAvailableToWhiteLabelCount <= 0) {
                    //    $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for show player controls");
                    //    return;
                    //}
                    //else {
                    if ($("#toggleEmbedOnlyMode").is(".inputEmbeddedOFF")) {

                        $("#toggleEmbedOnlyMode").removeClass("inputEmbeddedOFF").addClass("inputEmbeddedON");
                        embedOnlyMode = true;
                    }
                    else if ($("#toggleEmbedOnlyMode").is(".inputEmbeddedON")) {

                        $("#toggleEmbedOnlyMode").removeClass("inputEmbeddedON").addClass("inputEmbeddedOFF");
                        embedOnlyMode = false;

                    }
                    //}

                }
                else {
                    var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                    if (channelsAvailableToWhiteLabelCount <= 0) {
                        $(".createChannelForm .spnSubscribtionError").text("").text("Please subscribe for embeded mode");
                        return;
                    }
                    else {
                        if ($("#toggleEmbedOnlyMode").is(".inputEmbeddedOFF")) {

                            $("#toggleEmbedOnlyMode").removeClass("inputEmbeddedOFF").addClass("inputEmbeddedON");
                            embedOnlyMode = true;
                        }
                        else if ($("#toggleEmbedOnlyMode").is(".inputEmbeddedON")) {

                            $("#toggleEmbedOnlyMode").removeClass("inputEmbeddedON").addClass("inputEmbeddedOFF");
                            embedOnlyMode = false;

                        }
                    }

                }
            },

            DisableClickOnSubscribtion: function () {
                $("#toggleWhiteLabel, #toggleMute, #togglePassword, #toggleCustomBranding, #toggleControls, #toggleEmbedOnlyMode").removeAttr("onclick");
                $(".subcribtion").addClass("opacityNotActive");
                $("#toggleMatureContent").addClass("opacityNotActive");
                $("#toggleMatureContent").removeAttr("onclick");
                $("li.allowSubscribtion").hide();
                this.CloseEmbededArea();
            },

            EnableClickOnSubscribtion: function () {
                $("li.allowSubscribtion").show();
                $("#toggleEmbedOnlyMode").attr('onclick', 'CreateChannel.ToggleEmbedOnlyMode()');
                $("#toggleWhiteLabel").attr('onclick', 'CreateChannel.ToggleWhiteLabel()');
                $("#toggleMute").attr('onclick', 'CreateChannel.ToggleMute()');
                $("#togglePassword").attr('onclick', 'CreateChannel.TogglePassword()');
                $("#toggleCustomBranding").attr('onclick', 'CreateChannel.ToggleCustomBranding()');
                $("#toggleControls").attr("onclick", "CreateChannel.ToggleShowControls()");
                $("#toggleMatureContent").removeClass("opacityNotActive");
                $("#toggleMatureContent").attr("onclick", "CreateChannel.ToggleMatureContent()");
                $(".subcribtion").removeClass("opacityNotActive");

            },
            //END CREATE CHANNEL

            GetUpdateChannelPopup: function () {

                var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                this.InitCropChannelLogo();
                this.InitCropChannelAvatar();
                $("#txtEmbeddingDomain").val("");
                $("#txtPasswordProtected").val("");
                $("#txtBrandingName").val("");
                $(".spnSubscribtionError").text("");
                this.InitChannelSubscribtion();
                void 0;
               
                if (currChannel != null || currChannel != undefined) {
               

                    channelPassword = currChannel.ChannelPassword;
                    customLabel = currChannel.CustomLabel;
                    if (currChannel.CustomLogo != null) {
                        customLogo = currChannel.CustomLogo;
                    }
                    isWhiteLabeled = currChannel.IsWhiteLabeled;
                    embedEnable = currChannel.EmbedEnabled;
                    matureContentEnabledCreateChannel = currChannel.MatureContentEnabled;
                    showPlayerControls = currChannel.CustomPlayerControlsEnabled;
                    embedOnlyMode = currChannel.EmbedOnlyModeEnabled;
                    privateVideoModeEnabled = currChannel.IsPrivate;
                    keepGuideOpen = currChannel.KeepGuideOpen
                   
                       
                   
                        
                    if (customLogo != null || customLogo !== undefined || customLogo != "") {
                        
                        $imageLogoCropper.cropit('imageSrc', currChannel.customLogo);
                    }

                    if (embedEnable) {

                        $("#createChannelModal #divBoardContent ul li.subcribtionOFF").show();
                        $("#txtEmbeddingDomain").val("").val(currChannel.UserDomain);
                        this.EnableClickOnSubscribtion();
                    }
                    else {
                        $("#createChannelModal #divBoardContent ul li.subcribtionOFF").hide();
                        this.DisableClickOnSubscribtion();

                    }


                    if (channelPassword) {
                        void 0
                        isPasswordProtected = true;
                        var decodedChannelPass = decodeBase64(currChannel.ChannelPassword);
                        //console.log(decodedChannelPass);
                        var $input = $("#txtPasswordProtected");
                        //$input.removeAttr("placeholder");
                        //$input.attr("value", decodedChannelPass);
                        $("#txtPasswordProtected").attr("value", decodedChannelPass);


                        //  $('#txtPasswordProtected').val(decodedChannelPass);
                        // $("input[name=pass]").val = decodedChannelPass;
                        //$("#txtPasswordProtected").val(decodedChannel);
                    }
                    else {
                        isPasswordProtected = false;
                        $("#txtPasswordProtected").text("");
                    }

                    if (currChannel.MuteOnStartup == true) {
                        muteOnStartup = true;
                    }
                    else {
                        muteOnStartup = false;
                    }
                    void 0
                    if (customLabel != null) {
                        if (customLabel.length > 1 || isLogoModeActive == true) {
                        enabelCutomBranding = true;
                    }
                    }
                  
                    
                    else {
                        enabelCutomBranding = false;
                       
                    }
                }
                var imageUrl = $("#channelInfoHolder").find("#imgChannelAvatar").attr("src");
                $("#createChannelModal #txtChannelNameForm").replaceWith("<span id='txtChannelNameForm' class='cap'>" + channelName + "</span>");
                $("#createChannelModal #imgChannelAvatar").removeAttr("src").attr("src", imageUrl);
                $("#createChannelModal #TextURL").text("").text(domainName + "/" + userUrl + "/" + channelUrl);
                $("#createChannelModal #btnUpdate").show();
                $("#createChannelModal .pageTitle").text("").text("update channel");
                $("#createChannelModal #btnDelete").show();
                $("#createChannelModal #btnCreate").hide();
                $("#txtAreaChannelDesc").val(channelDescription);

                $("#createChannelModal select#slctLang option[value='" + languageId + "']").attr("selected", "selected");
                if (embedEnable) {
                    $("#toggleEmbeddedChannel").removeClass("inputEmbeddedOFF").addClass("inputEmbeddedON");
                    $("#txtEmbeddingDomain").show();
                    CreateChannel.OpenEmbededArea();
                }
                else {
                    $("#toggleEmbeddedChannel").removeClass("inputEmbeddedON").addClass("inputEmbeddedOFF");
                    $("#txtEmbeddingDomain").hide();
                    CreateChannel.CloseEmbededArea();
                }
                if (isWhiteLabeled) {
                    $("#toggleWhiteLabel").removeClass("inputWhiteLabelOFF").addClass("inputWhiteLabelON");
                }
                else {
                    $("#toggleWhiteLabel").removeClass("inputWhiteLabelON").addClass("inputWhiteLabelOFF");
                }
                if (muteOnStartup) {
                    $("#toggleMute").removeClass("inputMusicON").addClass("inputMusicOFF");
                    $(".muteRight").text("").text("Sound OFF")
                }
                else {
                    $("#toggleMute").removeClass("inputMusicOFF").addClass("inputMusicON");
                    $(".muteRight").text("").text("Sound ON")
                }
                void 0
                if (liveFirst)
                {
                  
                    $("#toggleLifeFirst").removeClass("inputLiveFirstOFF").addClass("inputLiveFirstON");
                }
                else
                {
                    $("#toggleLifeFirst").removeClass("inputLiveFirstON").addClass("inputLiveFirstOFF");
                }
                if (keepGuideOpen) {

                    $("#toggleKeepGuide").removeClass("inputKeepGuideOFF").addClass("inputKeepGuideON");
                }
                else {
                    $("#toggleKeepGuide").removeClass("inputKeepGuideON").addClass("inputKeepGuideOFF");
                }
                
                if (isPasswordProtected == true) {
                    if ($("#togglePassword").hasClass("inputPasswordOFF"))
                    {
                        $("#togglePassword").removeClass("inputPasswordOFF").addClass("inputPasswordON");
                    }
                   
                    $("#txtPasswordProtected").show();
                    $(".showpasswordlabel").show();
                }
                else {
                    if ($("#togglePassword").hasClass("inputPassworON"))
                    {
                        $("#togglePassword").removeClass("inputPassworON").addClass("inputPassworOFF");
                    }
                  
                    $("#txtPasswordProtected").hide();
                    $(".showpasswordlabel").hide();
                }
                void 0
                if (enabelCutomBranding == true) {
                    $("#toggleCustomBranding").removeClass("inputBrandingOFF").addClass("inputBrandingON");
                    $("#customBrandingOptions").show();
                    
                    $(".upgragePackg.customLabel").hide();
                    $("#txtBranding,#imageBranding").removeAttr("checked");
                    if(currChannel.IsLogoModeActive==true)
                    {
                        //$('.image-editor2.logo .cropit-preview-image').addClass('cropit-image-loaded').css("background-image","url('"+currChannel.CustomLogo+"')");
                        $("#imageBranding").attr("checked", "checked");
                       
                        $(".image-editor2.logo").show();
                       
                    }
                   
                    else
                    {
                        $("#txtBrandingName").val("").show();
                        $("#txtBrandingName").show().val("").val(currChannel.CustomLabel);
                        $("#txtBranding").attr("checked", "checked");
                        $(".image-editor2.logo").hide();
                    }
                }
                else {
                    $("#toggleCustomBranding").removeClass("inputBrandingON").addClass("inputBrandingOFF");
                    $("#txtBrandingName").hide();
                }
           
                if (matureContentEnabledCreateChannel)
                {
                   
                    $("#toggleMatureContent").removeClass("matureInputOFF").addClass("matureInputON");
               
               
                }
                else
                {
                    $("#toggleMatureContent").removeClass("matureInputON").addClass("matureInputOFF");
               
                }

                if (privateVideoModeEnabled) {
                    $("#togglePrivateMode").removeClass("privateInputOFF").addClass("privateInputON");
                }
                else
                {
                    $("#togglePrivateMode").removeClass("privateInputON").addClass("privateInputOFF");
                }

                if (showPlayerControls) {
                   
                    $("#toggleControls").removeClass("controlsInputOFF").addClass("controlsInputON");
                }
                else {
                   
                    $("#toggleControls").removeClass("controlsInputON").addClass("controlsInputOFF");
                }
                if (embedOnlyMode)
                {
                   
                    $("#toggleEmbedOnlyMode").removeClass("inputEmbeddedOFF").addClass("inputEmbeddedON");
                }
                else {
                    
                    $("#toggleEmbedOnlyMode").removeClass("inputEmbeddedON").addClass("inputEmbeddedOFF");
                }
                $("#createChannelModal").lightbox_me({

                    centered: true,
                    closeClick: false,
                    closeEsc:false,
                    onLoad: function () {

                        CreateChannel.InitCropChannelAvatar(false);
                        $(".createChannelForm").addClass("upgradeChannel")
                        $.ajax({
                            type: "POST",
                            url: webMethodGetChannelCategoriesJsonForCreateChannel,
                            cashe: false,
                            //async:false,
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                $("#createChannelModal select#txtChannelCategory").html("").html("<option value=0>Choose Category</option>")
                                $.each(response.d, function (key, val) {
                                    $("#createChannelModal select#txtChannelCategory").append($('<option>', { value: val.CategoryId }).text(val.Name));
                                })

                                if (categoryId == 0) {
                                    categoryId = currChannel.CategoryId;

                                }
                                $("#createChannelModal select#txtChannelCategory option[value='" + categoryId + "']").attr("selected", "selected");
                                void 0;

                            },
                            error: function (request, status, error) {

                            }
                        });

                         toggleMuteHasClass = $("#toggleMute").attr('class');
                        togglePasswordHasClass = $("#togglePassword").attr('class');
                         toggleCustomBrandingHasClass = $("#toggleCustomBranding").attr('class');
                         toggleControlsHasClass = $("#toggleControls").attr('class');
                        toggleMatureContentHasClass = $("#toggleMatureContent").attr('class');
                         toggleEmbedOnlyModeHasClass = $("#toggleEmbedOnlyMode").attr('class');
                         toggleEmbeddedChannelHasClass = $("#toggleEmbeddedChannel").attr('class');
                         toggleWhiteLabelHasClass = $("#toggleWhiteLabel").attr('class');
                    },
                    onClose: function (event) {
                   
                        //categoryId = currChannel.CategoryId;
                        //channelPassword = currChannel.ChannelPassword;
                        //customLabel = currChannel.CustomLabel;
                        //isWhiteLabeled = currChannel.IsWhiteLabeled;
                        //embedEnable = currChannel.EmbedEnabled;
                        //matureContentEnabledCreateChannel = currChannel.MatureContentEnabled;
                        //showPlayerControls = currChannel.CustomPlayerControlsEnabled;
                        //embedOnlyMode = currChannel.EmbedOnlyModeEnabled;


                        CreateChannel.ClearAllCreateChannelFields();
                    },
                    closeSelector: "close"
                });
            },

            GetOTTSettingsPopup: function () {
                $("#ottSettingsModal").lightbox_me({
                    centered: true,
                    closeClick: false,
                    closeEsc: false,
                    onLoad: function () {
                        CreateChannel.InitCropRokuImageAvatarHD();
                        CreateChannel.InitCropRokuImageAvatarSD();
                        //CreateChannel.GetChannelRokuSettings();
                        CreateChannel.GetUserRokuApp();
                        CreateChannel.GetUserSubscribtions();
                    },
                    onClose: function (event) {
                    },
                    closeSelector: "close"
                });
            },

            GetUserSubscribtions: function () {
                var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;

                $.ajax({
                    type: "POST",
                    url: webMethodGetUserProductSubscriptionsByUserId,
                    cashe: false,
                    async: false,
                    data: '{"userId":' + currChannel.UserId + '}',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        var subscribtions = response.d.Response;
                        for (var i = 0; i < subscribtions.length; i++) {
                            if (subscribtions[i].ProductName == "Professional" && subscribtions[i].ProductSubscriptionStatus == "Active") {
                                hasProfPlan = true;
                            }
                            if (subscribtions[i].ProductName == "Professional Plus" && subscribtions[i].ProductSubscriptionStatus == "Active") {
                                hasProfPlusPlan = true;
                            }
                        }
                    },
                    error: function (request, status, error) {

                    }
                });

            },

            GetChannelRokuSettings: function () {
                var currChannelId = channelId;
                var params = {
                    channelId: currChannelId
                };
                $.ajax({
                    type: "POST",
                    url: webMethodGetChannelRokuSettings,
                    data: JSON.stringify(params),
                    cashe: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response.d && response.d != "null") {
                            var settings = JSON.parse(response.d);
                            if (settings.AddedToRoku && $("#toggleAddChannelToRoku").hasClass("inputEmbeddedOFF")) {
                                CreateChannel.ToggleAddChannelToRoku();
                            }
                        }
                    },
                    complete: function () {
                    },
                    error: function (request, status, error) {
                        void 0;
                    }
                });
            },

            GetUserRokuApp: function () {
                var currChannel = this.GetChannelTubeByChannelId().responseJSON.d;
                var params = {
                    userId: currChannel.UserId
                };
                $.ajax({
                    type: "POST",
                    url: webMethodGetUserRokuApp,
                    data: JSON.stringify(params),
                    cashe: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response.d && response.d != "null") {
                            if ($("#toggleAddChannelToRoku").hasClass("inputEmbeddedOFF")) {
                                CreateChannel.ToggleAddChannelToRoku();
                            }
                            needRokuAppGenerate = false;
                            $("#genRokuAppBtn").text("Update");
                            var app = JSON.parse(response.d);
                            $("#txtRokuAppName").val(app.AppName);
                            $("#txtPrivacyPolicyLink").val(app.PrivacyPolicyLink);
                            $("#txtRokuAdLink").val(app.AdLink);
                            $("#txtAboutApp").val(app.About);
                            $("#hdImagePreview").attr("style", "background-image:url(data:image/png;base64," + app.ImageHD + ")");
                            $("#sdImagePreview").attr("style", "background-image:url(data:image/png;base64," + app.ImageSD + ")");
                            $("#hdnHdImageData").val(app.ImageHD);
                            $("#hdnSdImageData").val(app.ImageSD);
                            $("#hdnHdImageValid").val("1");
                            $("#hdnSdImageValid").val("1");
                        }
                    },
                    complete: function () {
                    },
                    error: function (request, status, error) {
                        void 0;
                    }
                });
            },

            UpsertChannelRokuSettings: function (addedToRoku) {
                var currChannelId = channelId;
                var params = {
                    channelId: currChannelId,
                    addedToRoku: addedToRoku
                };
                $.ajax({
                    type: "POST",
                    url: webMethodUpsertChannelRokuSettings,
                    data: JSON.stringify(params),
                    cashe: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        
                    },
                    complete: function () {
                    },
                    error: function (request, status, error) {
                        

                        void 0;
                    }
                });
            },

            GetChannelTubeByChannelId: function (currentChannel) {
                var currChannelId = channelId;
                var params = '{"channelId":' + currChannelId + '}';
                return $.ajax({
                    type: "POST",
                    url: webMethodGetChannelTubeByChannelId,
                    async: false,
                    data: params,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response.d != null) {
                            void 0;
                        }
                    },
                    error: function (request, status, error) {

                    }
                });
            },

            //UPDATE CHANNEL
            UpdateChannelForModal: function () {

                void 0;
                var isLogoModeActive;
                this.InitCropChannelLogo();
                var imageData = $imageCropper.cropit('export', {
                    type: 'image/jpeg',
                    quality: .9,
                    originalSize: true
                });

                if (imageData != null) {
                    b64 = imageData.split("base64,")[1];//string fileName, string imageData, int userId
                    if (b64 === undefined || b64 == "undefined") {
                        b64 = "";
                    }
                }

                var imageDataLogo = $imageLogoCropper.cropit('export', {
                    type: 'image/jpeg',
                    quality: .9,
                    originalSize: true
                });

                if (imageDataLogo != null) {
                    b64Logo = imageDataLogo.split("base64,")[1];//string fileName, string imageData, int userId
                    if (b64Logo === undefined || b64Logo == "undefined") {
                        b64Logo = "";
                    }
                }
                var filename = ""; //$("#createChannelModal #fuChannelAvatar").val().split("\\").join("/");
                var selectedCategoryValue = $("#createChannelModal select#txtChannelCategory option:selected").val();
                var selectedLanguageValue = $("#createChannelModal select#slctLang option:selected").val();
                var domainName = $("#txtEmbeddingDomain").val();

                var channelPassword = $("#txtPasswordProtected").val();
                var customLabelText = $("#txtBrandingName").val();
                void 0
                var regexp = /^[a-z\d\-_\s]+$/i;
                void 0;
                if (enabelCutomBranding && customLabelText.length>1)
                {
                    
                    //if (customLabelText.search(regexp) == -1) {
                    $("#loadingDiv").hide();
                    //alertify.error("No double spacing and special characters, like &,!,', /,#,? in the custom label");
                    //return;
                //}
                }
                else
                {
                    customLabelText = "";
                }
                
                if (embedEnable) {

                    if (domainName == "" || domainName == undefined || domainName == null) {
                        alertify.error("please add domain name");
                        return;
                    }
                    //var isDomainVerified = CreateChannel.VerifyDomainName(domainName)
                    //if (!isDomainVerified) {
                    //    alertify.error("please add valid domain name");
                    //    return;
                    //}
                }

                if (isPasswordProtected) {
                    if (channelPassword == "" || channelPassword == undefined || channelPassword == null) {
                        alertify.error("please add password");
                        return;
                    }
                }
                if (selectedCategoryValue != 0) {
                    categoryId = selectedCategoryValue;
                }

                if ($("#toggleMute").hasClass("inputMusicOFF"))
                {
                    muteOnStartupNewChannel = true;
                    muteOnStartup = true;
                }
                if (b64 === undefined || b64 == "undefined") {
                    b64 = "";
                }
                if (b64Logo === undefined || b64Logo == "undefined") {
                    b64Logo = "";
                }
                if ($('#txtBranding').is(":checked"))
                {
                    isLogoModeActive = false;
                }
                if ($('#imageBranding').is(":checked"))
                {
                    isLogoModeActive = true;
                }
               
                //int channelId, string fileName, string imageData, int categoryId, int userId
                var channelDescriptionUpdate = $("#txtAreaChannelDesc").val();
                //isWhiteLabeled = currChannel.IsWhiteLabeled;
                //  embedEnable = currChannel.EmbedEnabled;
                //isPasswordProtected,muteOnStartup,enabelCutomBranding
                
                if (typeof liveFirst !== 'undefined') {
                    playLiveFirst = liveFirst;
                }
                var params = '{"channelTubeId":' + channelTubeId + ',"languageId":' + selectedLanguageValue + ',"fileName":' + "'" + filename + "'" + ',"imageData":' + "'" + b64 + "'" + ',"categoryId":' + selectedCategoryValue + ',"userId":' + userId + ',"channelDescription":' + "'" + channelDescriptionUpdate + "'" + ',"isWhiteLabeled":' + isWhiteLabeled + ',"embedEnable":' + embedEnable + ',"channelPassword":' + "'" + channelPassword + "'" + ',"customLabel":' + "'" + customLabelText + "'" + ',"muteOnStartup":' + muteOnStartup + ',"domainName":' + "'" + domainName + "'" + ',"embedOnlyMode":' + embedOnlyMode + ',"matureContentEnabled":' + matureContentEnabledCreateChannel + ',"showPlayerControls":' + showPlayerControls + ',"isPrivate":' + privateVideoModeEnabled + ',"imageLogoData":' + "'" + b64Logo + "'" + ',"isLogoModeActive":' + isLogoModeActive + ',"playLiveFirst":' + playLiveFirst + ',"keepGuideOpen":' + keepGuideOpen + '}';
               
              
                $.ajax({
                    type: "POST",
                    url: webMethodUpdateChannelForModal,
                    data: params,
                    cashe: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        $.each(response, function (i, d) {
                            if (d.PictureUrl == "") {
                                d.PictureUrl = '/images/comingSoonBG.jpg';
                            }

                            $("#imgChannelAvatar").removeAttr("src").attr("src", d.PictureUrl);
                            $("#lblCategory").text("").text("category: " + d.CategoryName);
                            //console.log(d.Description);
                            languageId = d.LanguageId;
                            channelDescription = d.Description;
                            showPrivateVideoMode = d.IsPrivate;
                            privateVideoModeEnabled = d.IsPrivate;
                            $("#createChannelModal select#slctLang option[value='" + d.LanguageId + "']").attr("selected", "selected");
                            //console.log(channelDescription);
                            matureChannelContentEnabled =d.MatureContentEnabled
                            alertify.success("Channel has been successfully updated.");
                      

                        })
                        void 0
                    },
                    complete: function (response) {
                        $("#loadingDiv").hide();
                       // CloseCreateChannelPopup();
                        $('#createChannelModal').trigger('close');
                        $("#snippetPopup").trigger("close");
                    },
                    error: function (request, status, error) {
                        void 0;
                    }
                });

            },
            //END UPDATE CHANNEL


            //VALIDATE CHANGES MADE IN UPDATE CHANNEL
            UpdateChannelChangesValidation:function(){},
            //END VALIDATE CHANGES MADE IN UPDATE CHANNEL
            //DELETE CHANNEL

            DeleteChannel: function () {
                alertify.confirm("If you proceed, the channel and all of its schedules will be deleted. Click 'Ok' to proceed or 'Cancel' to abort", function (e) {
                    if (e) {
                        var params = '{"channelTubeId":' + channelTubeId + '}';
                        $.ajax({
                            type: "POST",
                            url: webMethodDeleteChannelForModal,
                            data: params,
                            cashe: false,
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (response) {
                                window.location.href = "/" + userUrl;

                            },
                            complete: function () {


                                $('#createChannelModal').trigger('close');
                            },
                            error: function (request, status, error) {
                                void 0;
                            }
                        });
                    }
                });
            },
            //END DELETE CHANNEL
            VerifyDomainName: function (domainName) {
                // strip off "http://" and/or "www."
                domainName = domainName.replace("http://", "");
                domainName = domainName.replace("https://", "");
                domainName = domainName.replace("www.", "");
                domainName = domainName.replace(" ", "");

                var reg = /(?=^.{1,254}$)(^(?:(?!\d+\.)[a-zA-Z0-9_\-]{1,63}\.?)+(?:[a-zA-Z]{2,})$)/; ///^[a-zA-Z0-9][a-zA-Z0-9-]{1,61}[a-zA-Z0-9]\.[a-zA-Z]{2,}$/;
                return true; //reg.test(domainName);
            }



        };

