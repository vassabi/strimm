//urls to webservices
var WebMethodLogin = "/../WebServices/UserService.asmx/Login";
var WebMethodEncryptedLogin = "/../WebServices/UserService.asmx/EncryptedLogin";
var WebMethodUpdateUserLocation = "/../WebServices/UserService.asmx/UpdateUserLocation";
var WebMethodForgotPassword = "/../WebServices/UserService.asmx/ForgotPass";
var WebMethodSignOut = "/../WebServices/UserService.asmx/SignOut";
var webMethodGetOffset = "/../WebServices/ChannelWebService.asmx/GetOffset";
var webMethodSendFeedback = "/../WebServices/UserService.asmx/SendFeedback"
var webMethodGetChannelCategoriesWithCurrentlyPlayingChannelCount = "/../WebServices/SearchWebService.asmx/GetCategoriesWithCurrentlyPlayingChannelCountBrowseChannels";
var webMethodAddCreateChannelUCToModal = "/../WebServices/ChannelWebService.asmx/AddCreateChannelControlToPage";

var webMethodIsValidSession = "/../WebServices/UserService.asmx/IsValidSession";
var webMethodResetPassword = "/../WebServices/UserService.asmx/ResetPassword";

var UpdateUserInterests = "/WebServices/UserService.asmx/UpdateUserInterests";

var toggledMenuVisible = false;
var toggledBrowseChannelsMenuVisible = false;
var toggledPrefMenuVisible = false;
var toggledClearVideosMenuVisible = false;
var dateFrom;
var dateTill;

(function () {
    var userInfoObj = getCookie('init');
    
    var App = {
        init: function (location) {
            if (location) {
                userInfoObj = {
                    City: location.city.names.en,
                    State: location.subdivisions[0].iso_code,
                    Country: location.country.names.en,
                    PostalCode: location.postal.code,
                    UserIp: location.traits.ip_address,
                    Longitude: location.location.longitude,
                    Latitude: location.location.latitude,
                    Timezone: location.location.time_zone
                };
                setCookie('init', JSON.stringify(userInfoObj), 30);
            }
        },

        getUserInfo: function () {
            return userInfoObj;
        },

        isInit: function () {
            return (userInfoObj != undefined);
        }
    };

    getApp = function () {
        return App;
    }
})();

function RemoveOverlay() {
    var $currentOverlays = $(".js_lb_overlay:visible");
    if ($currentOverlays) {
        $currentOverlays.remove();
    }   
}

var Main = {
    GetOffSet: function () {
        var dt = new Date();
        var tz = dt.getTimezoneOffset();
        return tz;
    }
}

function getQueryStringParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function CreateMyChannel() {
    ajaxTopChannels = $.ajax({
        type: "POST",
        url: webMethodIsValidSession,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d && response.d) {
                showPopup();
            }
            else {
                loginModal('create-channel');
            }
        }
    });
}

function showPopup() {
    var $modal = $find('myid');

    $('#imgChannelAvatar').attr('src', '/images/comingSoonBG.jpg')
    $('#txtChannelName').val('');
    $('#lblChannelUrl').text('');
    $('#ddlChannelCategory').prop('selectedIndex', 0);

    if ($modal) {
        $modal.show()
    }
}

function GetCreateChannelUC(channelName) {
    void 0;
    $('#modalCreateChannel').lightbox_me({
        centered: true,
        onLoad: function () {
            $.ajax({
                type: "POST",
                url: webMethodAddCreateChannelUCToModal,
                data: '{"channelname":' + "'" + channelName + "'" + ',"userId":' + "'" + userId + "'" + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (responce) {
                    $("#createChannelHolder").append(responce.d);
                }
            });
        },
        onClose: function () {
            RemoveOverlay();
        }
    });

};

function BrowserDetection() {

    var userAgent = navigator.userAgent.toLowerCase(),
    browser = '',
    version = 0;

    $.browser.chrome = /chrome/.test(navigator.userAgent.toLowerCase());

    if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
        //$("body").attr("class", "explorer");//test for MSIE x.x;
        var ieversion = new Number(RegExp.$1) // capture x.x portion and store as a number
        if (ieversion) {
            if (ieversion == 9) {
                $("body").removeAttr("class").attr("class", "explorer_9");
            }
            else if (ieversion > 9) {
                $("body").removeAttr("class").attr("class", "explorer");
            }
            else if (ieversion >= 8) {
                $("body").removeAttr("class").attr("class", "explorer_8");
            }
            else if (ieversion >= 7) {
                var msg = "We are sorry, but Internet Explorer 7 is not supported any longer by our site. Please use a latest version of Internet Explorer or other browsers, like Firefox, Safari or Chrome";
                ShowMessage(msg);
                //  alert("We are sorry, but Internet Explorer 7 is not supported any longer by our site. Please use a latest version of Internet Explorer or other browsers, like Firefox, Safari or Chrome");
                window.location = "https://www.google.com/";
            }
            else if (ieversion >= 6) {
                var msg = "We are sorry, but Internet Explorer 6 is not supported any longer by our site. Please use a latest version of Internet Explorer or other browsers, like Firefox, Safari or Chrome";
                ShowMessage(msg);
                // alert("We are sorry, but Internet Explorer 6 is not supported any longer by our site. Please use a latest version of Internet Explorer or other browsers, like Firefox, Safari or Chrome");
                window.location = "https://www.google.com/";
            }
        }
    }
    // Is this a version of Chrome?
    if ($.browser.chrome) {
        userAgent = userAgent.substring(userAgent.indexOf('chrome/') + 7);
        userAgent = userAgent.substring(0, userAgent.indexOf('.'));
        version = userAgent;
        // If it is chrome then jQuery thinks it's safari so we have to tell it it isn't
        $.browser.safari = false;
        browser = "Chrome";
        $("body").removeAttr("class").attr("class", browser);
    }

    // Is this a version of Safari?
    if ($.browser.safari) {
        userAgent = userAgent.substring(userAgent.indexOf('safari/') + 7);
        userAgent = userAgent.substring(0, userAgent.indexOf('.'));
        version = userAgent;
        browser = "Safari";
        $("body").removeAttr("class").attr("class", browser);
    }

    // Is this a version of Mozilla?
    if ($.browser.mozilla) {
        //Is it Firefox?
        if (navigator.userAgent.toLowerCase().indexOf('firefox') != -1) {
            userAgent = userAgent.substring(userAgent.indexOf('firefox/') + 8);
            userAgent = userAgent.substring(0, userAgent.indexOf('.'));
            version = userAgent;
            browser = "Firefox"
            $("body").removeAttr("class").attr("class", browser);
        }
            // If not then it must be another Mozilla
        else {
            browser = "Mozilla (not Firefox)"
            
        }
    }

    // Is this a version of Opera?
    if ($.browser.opera) {
        userAgent = userAgent.substring(userAgent.indexOf('version/') + 8);
        userAgent = userAgent.substring(0, userAgent.indexOf('.'));
        version = userAgent;
        browser = "Opera";
        $("body").removeAttr("class").attr("class", browser);
    }
}

function detectIEVersion() {
    if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
        //$("body").attr("class", "explorer");//test for MSIE x.x;
        var ieversion = new Number(RegExp.$1) // capture x.x portion and store as a number
        if (ieversion == 9) {
            $("body").removeAttr("class").attr("class", "explorer_9");
        }
        else if (ieversion > 9) {
            $("body").removeAttr("class").attr("class", "explorer");
        }
        else if (ieversion >= 8) {
            $("body").removeAttr("class").attr("class", "explorer_8");
        }
        else if (ieversion >= 7) {
            var msg = "We are sorry, but Internet Explorer 7 is not supported any longer by our site. Please use a latest version of Internet Explorer or other browsers, like Firefox, Safari or Chrome";
            ShowMessage(msg);
          //  alert("We are sorry, but Internet Explorer 7 is not supported any longer by our site. Please use a latest version of Internet Explorer or other browsers, like Firefox, Safari or Chrome");
            window.location = "https://www.google.com/";
        }
        else if (ieversion >= 6) {
            var msg = "We are sorry, but Internet Explorer 6 is not supported any longer by our site. Please use a latest version of Internet Explorer or other browsers, like Firefox, Safari or Chrome";
            ShowMessage(msg);
           // alert("We are sorry, but Internet Explorer 6 is not supported any longer by our site. Please use a latest version of Internet Explorer or other browsers, like Firefox, Safari or Chrome");
            window.location = "https://www.google.com/";
        }
       
    }
   
};
function ToggleTVGuideMobileMenu() {
    void 0;
    if ($(".subBlockTVGuide").is(":visible")) {
        $(".subBlockTVGuide").hide();
        $(".mobileNavHeader.tvGuide").removeClass("active");
    }
    else {
        $(".subBlockTVGuide").show();
        $(".mobileNavHeader.tvGuide").addClass("active");
    }

};

function ToggleBusinessMobileMenu()
{
    if ($(".subBlockBusiness").is(":visible")) {
        $(".subBlockBusiness").hide();
        $(".mobileNavHeader.businessesMobileMenu").removeClass("active");
    }
    else {
        $(".subBlockBusiness").show();
        $(".mobileNavHeader.businessesMobileMenu").addClass("active");
    }
}
$(document).ready(function () {
    
    
        //$(document).click(function (e) {
        //    var p = $(e.target).closest('.mobileMenu').length
        //    if (!p) {
        //        $(".mobileMenuOpen").hide();
        //    }
        //});
    
   
   

    //if (window.location.href.lastIndexOf('Default.aspx') > 0 || window.location.href == (window.location.origin + '/') ||
    //    window.location.href.lastIndexOf('/home') > 0) {
    //    $('#divFooter').hide();
    //}
    //else {
    //    $('#divFooter').show();
    //}
  
    //if (isUserLocked !== undefined || isUserLocked!=='undefined')
    //{
    //    if (isUserLocked == true)
    //    {
    //        ClearAllUserCookies();
    //        SignOut();
    //    }
        
       

    //}
    //var opts = {
    //    lines: 12             // The number of lines to draw
    //    , length: 7             // The length of each line
    //    , width: 5              // The line thickness
    //    , radius: 10            // The radius of the inner circle
    //    , scale: 1.0            // Scales overall size of the spinner
    //    , corners: 1            // Roundness (0..1)
    //    , color: '#000'         // #rgb or #rrggbb
    //    , opacity: 1 / 4          // Opacity of the lines
    //    , rotate: 0             // Rotation offset
    //    , direction: 1          // 1: clockwise, -1: counterclockwise
    //    , speed: 1              // Rounds per second
    //    , trail: 100            // Afterglow percentage
    //    , fps: 20               // Frames per second when using setTimeout()
    //    , zIndex: 2e9           // Use a high z-index by default
    //    , className: 'spinner'  // CSS class to assign to the element
    //    , top: '450px'            // center vertically
    //    , left: '50%'           // center horizontally
    //    , shadow: false         // Whether to render a shadow
    //    , hwaccel: false        // Whether to use hardware acceleration (might be buggy)
    //    , position: 'absolute'  // Element positioning
    //}
    //var target = document.getElementById('loadingDiv')
    //var spinner = new Spinner(opts).spin(target)

    //GetChannelCategoriesWithCountsWithoutUIUpdate();

    var queryString = getQueryStringParameterByName("create-channel")
    if (queryString == 1) {
        CreateChannel.RedirectToCreateChannel();
    }
    
    
    //if ($.browser.device = (/android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase())))
    //{
    //    alert("I am device!");
    //}
   // detectIEVersion();
    BrowserDetection();
   
    var userAgent = navigator.userAgent.toLowerCase();
    //alert(userAgent);
  
    //alert($.browser.version + "," + window.navigator.userAgent);
    //if ($.browser.msie && parseFloat($.browser.version) >=7) {
    //    alert($.browser.version);
    //}
    $("#divMyStaff").mouseover(function () {
        $("#divMyStaffMenu").slideDown(500);
    });
    $("#divMyStaff #divMyStaffMenu").mouseleave(function () {
        $("#divMyStaffMenu").stop(true).slideUp(500);
    });
    //$("#divMyStaff").hover(function () {
    //    $(this).find("#divMyStaffMenu").animate({opacity:0.50}).delay(500).fadeToggle(400);
    //});
    //var offset = Main.GetOffSet();
    //$.ajax({
    //    type: "POST",
    //    url: webMethodGetOffset,
    //    data: '{"offset":' + offset + '}',
    //    dataType: "json",
    //    contentType: "application/json; charset=utf-8",
    //    success: function (response) {

    //    }
    //});
    
   
   
   // GetFeaturedChannels();

    $('html').click(function () {
        if (toggledMenuVisible) {
            ToggleMenu();
        }
        if (toggledPrefMenuVisible) {
            ToggleProfMenu();
        }
        if (toggledBrowseChannelsMenuVisible) {
            ToggleChannels();
        }
        if (toggledClearVideosMenuVisible) {
            ToggleClearVideosMenu();
        }

        //var $div = $('.mobileMenuOpen');
        //if ($div.is(":visible") && isMenueVisible == true) {
        //    if ($(".subBlockBusiness").is(":hidden"))
        //    {
        //        $div.hide();
        //        $(".mobileMenu").removeClass("active");
        //        isMenueVisible = false;
        //    }
           
            
        //}
        //else {
        //    isMenueVisible = true;
        //}
    });

    $('#ancMenu').click(function (event) {
        event.stopPropagation();
    });
    $('#divBrowseChannels').click(function (event) {
        event.stopPropagation();
    });
    $('#spnHello').click(function (event) {
        event.stopPropagation();
    });
    $('.leftClearHolder').click(function (event) {
        event.stopPropagation();
    });
});

function ToggleChannels() {
    void 0
    if ($("#profMenuDdl").is(":visible")) {
        $("#profMenuDdl").hide();
        $("#helloMenuList .trHello").hide();
       // $("#profMenuDdl #helloImg").text(" ").text("▼");
        toggledPrefMenuVisible = false;
    }

    if ($("#menuTopList").is(":visible")) {
        $("#menuTopList").hide();
        //$("#divTopMenuHolder .divImg").text(" ").text("▼");
        toggledMenuVisible = false;
    }

    if ($("#divChannelsCategory").is(":visible")) {
        $("#divChannelsCategory").hide();
       // $("#divBrowseChannels a #divImg").text(" ").text("▼");
        toggledBrowseChannelsMenuVisible = false;
    }
    //if ($(".subBlockBusiness").is(":visible"))
    //{
    //    $(".subBlockBusiness").hide();
    //    GetChannelCategoriesWithCounts();
    //    $("#divChannelsCategory").show();
    //    // $("#divBrowseChannels a #divImg").text(" ").text("▲");
    //    toggledBrowseChannelsMenuVisible = true;
    //}
    else {
        $(".subBlockBusiness").hide();
         GetChannelCategoriesWithCounts();
        $("#divChannelsCategory").show();
       // $("#divBrowseChannels a #divImg").text(" ").text("▲");
        toggledBrowseChannelsMenuVisible = true;
    }
};

function ToggleMenu() {
    if ($("#profMenuDdl").is(":visible")) {
        $("#profMenuDdl").hide();
        $("#helloMenuList .trHello").hide();
      $("#profMenuDdl #helloImg").text(" ").text("▼");
        toggledPrefMenuVisible = false;
    }

    if ($("#divChannelsCategory").is(":visible")) {
        $("#divChannelsCategory").hide();
       // $("#divBrowseChannels a #divImg").text(" ").text("▼");
        toggledBrowseChannelsMenuVisible = false;
    }

    if ($("#menuTopList").is(":visible")) {
        $("#menuTopList").hide();
      //  $("#divTopMenuHolder .divImg").text(" ").text("▼");
        toggledMenuVisible = false;
    }
    else {
        $("#menuTopList").show();
      //  $("#divTopMenuHolder .divImg").text(" ").text("▲");
        toggledMenuVisible = true;
    }
}

function ToggleProfMenu() {
    if (toggledBrowseChannelsMenuVisible) {
        $("#divChannelsCategory").hide();
       // $("#divBrowseChannels a #divImg").text(" ").text("▼");
        toggledBrowseChannelsMenuVisible = false;
    }

    if (toggledMenuVisible) {
        $("#menuTopList").hide();
       // $("#divTopMenuHolder .divImg").text(" ").text("▼");
        toggledMenuVisible = false;
    }
    
    if ($("#profMenuDdl").is(":visible")) {
        $("#profMenuDdl").hide();
        $("#helloMenuList .trHello").hide();
        $("#profMenuDdl #helloImg").text(" ").text("▼");
        toggledPrefMenuVisible = false;
    }
    else {
        $("#profMenuDdl").show();
        $("#profMenuDdl #helloImg").text(" ").text("▼");
        $("#helloMenuList .trHello").show();
        toggledPrefMenuVisible = true;
    }
}

function ToggleChannelMenu() {
    if ($(".menuAddVideosDropdown").is(":visible")) {
        $(".menuAddVideosDropdown").hide();
    }
    else {
        $(".menuAddVideosDropdown").show();
    }
}

function ToggleClearVideosMenu() {
    if ($(".removeOptions").is(":visible")) {
        $(".removeOptions").hide();
        toggledClearVideosMenuVisible = false;
    }
    else {
        $(".removeOptions").show();
        toggledClearVideosMenuVisible = true;
    }
}
function CloseChannelCategoryMenu()
{
    if(("#divChannelsCategory").is(":visible"))
    {
        ("#divChannelsCategory").hide();
    }
    
}
function CloseSubBlockBusiness()
{
    if ($(".subBlockBusiness").is(":visible"))
    {
        $(".subBlockBusiness").hide();
    }
    
}

//signout
function SignOut() {
    var isfacebookuser = getCookie('isfacebook');

    if (isfacebookuser == "true") {
        facebookLogout();
    }

    $.ajax({
        type: "POST",
        url: WebMethodSignOut,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            ClearAllUserCookies();
        },
        complete: function () {
            window.location.href = "home";
        }
    });
};

function getCookie(cname) {
    return $.cookie(cname)
}

function setCookie(c_name, value, exdays) {
    $.cookie(c_name, value, { path: '/', expires: exdays });
}

function setCookieInMins(c_name, value, mins) {
    var date = new Date();
    date.setTime(date.getTime() + (mins * 60 * 1000));
    $.cookie(c_name, value, { path: '/', expires: date });
}

function deleteCookie(name) {
    $.cookie(name, null, { path: '/', expires: -1 });
}

function getClientTime() {
    var clientTime = getCookie('ClientTime'); 

    if (clientTime == undefined) {
        var now = new Date();
        clientTime = now.format("MM-dd-yyyy-H-mm");
    }

    return clientTime;
}

function setClientTime() {
    var now = new Date();
    var clientDateTime = GetDateAndTime(now);
    //console.log("Client Time: " + clientDateTime);

    setCookie('ClientTime', clientDateTime, 30);

    return clientDateTime;
}

function GetDateAndTime(dt) {
    var arr = new Array(dt.getMonth()+1, dt.getDate(), dt.getFullYear(), dt.getHours(), dt.getMinutes());

    for (var i = 0; i < arr.length; i++) {
        if (arr[i].toString().length == 1) arr[i] = "0" + arr[i];
    }

    return arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4];
}

function facebookLogout() {
    var cookieValue = getCookie("isfacebook");

    if (cookieValue == "true") {
        FB.logout(function (response) {
            void 0;
        });
    }
}

// function put control to center
jQuery.fn.center = function () {
    this.css("position", "absolute");
    this.css("top", Math.max(0, (($(window).height() - $(this).outerHeight()) / 2) +
                                                $(window).scrollTop()) + "px");
    this.css("left", Math.max(0, (($(window).width() - $(this).outerWidth()) / 2) +
                                                $(window).scrollLeft()) + "px");
    return this;
}

//ajax calendar extender on signup page
function CheckDate(sender, args) {
    var selectedDate = sender._selectedDate;
    var today = new Date();
    if (selectedDate > today) {
        $("#txtBirthDate").val("");
        var msg = "Selected date is not valid.";
        alertify.alert(msg);
        return;
    }
    else {
        dateFrom = selectedDate;
    }
}

function loginModal(hrefType) {
    $("#see_id").html("Log in to Strimm or Sign Up")
    $('.signUpNow').show();
    $('#loginBox').lightbox_me({
        centered: true,
        onLoad: function () {
            $('#spanMessage').empty();
            $('#txtUserName').val('');
            $('#txtPassword').val('');
            $('#loginBox').find('input:first').focus();
            $("#loginBox #actions #ancLogin").removeAttr("onclick").attr("onclick", 'Login("' + hrefType + '")');
        },
        onClose: function () {
            RemoveOverlay();
        }
    });
    //  e.preventDefault();
};

function WelcomeLoginUp(hrefType) {
    $('.signUpNow').hide();
    $('#loginBox').lightbox_me({
        centered: true,
        onLoad: function () {
            $('#spanMessage').empty();
            $('#txtUserName').val('');
            $('#txtPassword').val('');
            $('#loginBox').find('input:first').focus();
            $("#see_id").html("Log in to Strimm and Welcome Aboard!")
            $("#loginBox #actions #ancLogin").removeAttr("onclick").attr("onclick", 'Login("' + hrefType + '")');
        },
        onClose: function () {
            RemoveOverlay();
        }
    });
}

function ShowAgeVerificationPopup() {
    $('#ageVerificationPopup').lightbox_me({
        centered: true,
        onLoad: function () {
        },
        onClose: function () {
            RemoveOverlay();
        }
    });
}

function CloseAgeVerification() {
    $("#ageVerificationPopup").hide();
    $('#ageVerificationPopup').trigger('close');
}

function forgotModal(e) {
    $("#divForgotPass").lightbox_me({
        centered: true,
        onLoad: function () {
            $('#loginBox').hide();
            $('#divForgotPass').find('input:first').focus();
        },
        onClose: function () {
            RemoveOverlay();
            location.href = "/";
        },
        closeSelector: "close"
    });
    // e.preventDefault();
}

function closeModal(element) {
    //var ele = $("#" + element);
    element.lightbox_me();
    element.trigger('close');
    element.hide();
    RemoveOverlay();
}

function CreateKeepMeLoginCookie(userName, isfacebook) {   
    $.ajax({
        type: "POST",
        url: WebMethodGetUserByEmail,
        data: '{"email":' + "'" + userName + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d != null) {
                user = response.d;
                $.each(response, function (i, d) {
                    setLoginCookies(d.UserId, isfacebook);
                });
            }
        }
    });  
}

function ClearAllUserCookies() {
    deleteCookie("userId");
    deleteCookie("isfacebook");
    deleteCookie("dontshowmsgarch");
    deleteCookie("dontshowmsgfav");
    deleteCookie("ASP.NET_SessionId");
}

function setLoginCookies(userid, isfacebook) {
    clearLoginCookies();
    setCookie('isfacebook', isfacebook, 30);
    setCookie('userId', userid, 30);
}

function clearLoginCookies() {
    deleteCoologinkie('userId');
    deleteCookie('isfacebook');
}

var WebMethodCheckeIfDeleted = "/../WebServices/UserService.asmx/IsUserDeleted";
var WebMethodCheckIfUserIsTemporary = "/../WebServices/UserService.asmx/IsTemporaryUser";
var isUserDeleted = false;

function CheckIfUserDeleted(email) {

    $.ajax({
        type: "POST",
        url: WebMethodCheckeIfDeleted,
        data: '{"email":' + "'" + email + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            isUserDeleted = response.d;

        }
    });


};

function CheckIfUserIsTemporary(email)
{
    $.ajax({
        type: "POST",
        url: WebMethodCheckIfUserIsTemporary,
        data: '{"email":' + "'" + email + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
          var  isTempUser = response.d;
            if (isTempUser) {

                $("#spanMessage").text("").text("Please confirm your account, the confirmation link was sent to your email");

                return;
            }
        }
    });

}

function Login(hrefType) {
    var username = $("#txtUserName").val().toLowerCase();
    
    var password = $("#txtPassword").val();
    var pagename = "";
    var url = window.location.pathname;
    var fileName = url.substring(url.lastIndexOf('/') + 1);
    var keepLogin = $('#keepLogIn').is(':checked');
    CheckIfUserDeleted(username);
   // CheckIfUserIsTemporary(username);
    if (isUserDeleted) {
        $("#spanMessage").text("").text("Your account has been deleted. Please contact Strimm for details.");
        return;
    }
   
    
    if (fileName != "") {
        var match = document.location.pathname.match(/[^\/]+$/);
        if (match) {
            pagename = document.location.pathname.match(/[^\/]+$/)[0];
        }
    }

   
    var data;
    if ((username != "") && (password != "")) {
        if (validateEmail(username) == true) {
            //$("#spanMessage").ajaxStart(function () {
            //    $(this).text("please wait..");
            //});
            void 0
            $.ajax({
                type: "POST",
                url: WebMethodCheckeIfLocked,
                data: '{"userName":' + "'" + username + "'" + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    $("#spanMessage").text("").text("please wait...")
                },
                success: function (response) {
                    if (response.d == true) {
                        //console.log(response.d);
                        $("#spanMessage").text("").text("Your account has been locked. Please contact Strimm administator.");
                    }
                    else {
                        var saltString = password+"-"+username;
                       
                        var hash = CryptoJS.SHA256(saltString);

                        var saltedPassword = hash.toString(CryptoJS.enc.Base64);
                      
                        $.ajax({
                            type: "POST",
                            url: WebMethodEncryptedLogin,
                            data: '{"userName":' + "'" + username + "'" + ',"saltedPassword":' + "'" + saltedPassword + "'" + '}',
                           // url: WebMethodLogin,
                           // data: '{"userName":' + "'" + username + "'" + ',"password":' + "'" + password + "'" + '}',
                           
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                           
                            success: function (response) {
                                void 0
                                if (response.d.length>0) {
                                    if (hrefType == 'create-channel') {                                       
                                        document.location.href = "/home?create-channel=1";
                                    }
                                    else if ((hrefType == 'sameLocation') && ((pagename == "home") || pagename == "") || (hrefType == 'password-recovery')) {
                                        document.location.href = "/" + response.d;
                                    }                                 
                                    else {                                      
                                        document.location.reload(true);
                                    }
                                    UpdateUserLocation(response.d);
                                    if (keepLogin) {
                                        CreateKeepMeLoginCookie(username, false);
                                    }
                                }
                                else {
                                    $("#spanMessage").text("").text("username or password is incorrect");
                                }

                                
                            },
                            error: function (response) {
                                void 0;
                            }
                        });
                    }
                },
                error: function (response) {
                }
            });
        }
        else {
            $("#spanMessage").text("please enter a valid e-mail");
        }
    }
    else {
        $("#spanMessage").text("please enter your e-mail and password");
    }
}

function UpdateUserLocation(username) {
    if (username != "") {
        var cookie = getCookie('init');

        $.ajax({
            type: "POST",
            url: WebMethodUpdateUserLocation,
            data: '{"location":' + cookie + ',"username":' + '"' + username + '"' + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
            }
        });
    }
}

function validateAlphaNumeric(text) {
    var re = /^[a-zA-Z0-9 ]+$/;
    return re.test(text);
}

function validateText(text) {
    var re = /^[a-zA-Z ]+$/;
    return re.test(text);
}

function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function ForgotPass() {
    var username = $("#txtEmailForgot").val();
    if (validateEmail(username)) {
        $.ajax({
            type: "POST",
            url: WebMethodForgotPassword,
            data: '{"userName":' + "'" + username + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#txtEmailForgot").val("");
                $("#divForgotPass").hide();
                alertify.success(response.d);
            },
            error: function (response) {
            }
        });
    }
    else {
        $("#spnForgotPass").text("please specify a valid e-mail address");
    }
}

//END functions for top menu before login
function ShowFeedback() {
    $("#feedbackModal").lightbox_me({
        centered: true,
        onLoad: function () {

        },
        onClose: function () {
            RemoveOverlay();
        }
    });
}

function SendFeedback(pageName) {
    var selectedOption = $('#ddlFeedCat option:selected').text();
    var comments = $("#txtFeedback").val();
    
    if (comments.length < 1) {

        alertify.alert("Please select your feedback type from the drop down and enter your message below.");
        return;
    }
    $.ajax({
        type: "POST",
        url: webMethodSendFeedback,
        data: '{"pageName":' + "'" + pageName + "'" + ',"selectedOption":' + "'" + selectedOption + "'" + ',"comments":' + "'" + comments + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#lblMsgFeedback").text("please wait");
        },
        success: function (response) {
            $('#feedbackModal a.close').trigger("click");
            alertify.success("Thank you for your feedback.");
        },
        complete: function () {
        }
    });
}

function TogglePageHelp() {
    $(".pageComent").toggle();
    if ($(".pageComent").is(":visible")) {
        $(".pageHelp").text("about this page ▴");
    }
    else {
        $(".pageHelp").text("about this page ▾");
    }
}

var catetoriesWithChannelCounts;

function GetChannelCategoriesWithCountsWithoutUIUpdate() {
    var clientTime = setClientTime();
    //$("#divChannelsCategory").addClass("loading");
    $.ajax({
        type: "POST",
        url: webMethodGetChannelCategoriesWithCurrentlyPlayingChannelCount,
        cashe: false,
        data: '{"clientDateAndTime":"' + clientTime + '"}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.d) {
                //$("#divChannelsCategory").removeClass("loading");
                catetoriesWithChannelCounts = response.d;

            }
        }
    });
}

function GetChannelCategoriesWithCounts() {
    if (catetoriesWithChannelCounts && catetoriesWithChannelCounts != null) {
        var categories = catetoriesWithChannelCounts;

                var holder = $("#divChannelsCategory ul");
                var html = "";
                var currentColumn = 0;
                var currentRow = 0;
                var maxColumnCount = 6;
                var maxRowCount = 2;

                $.each(categories, function (i, c) {
            var chCountElement = $("#ch" + c.CategoryId);
            if (chCountElement) {
                var count = c.ChannelCount == 0 ? '' : c.ChannelCount;
                chCountElement.text(count);
            }
                });

        GetChannelCategoriesWithCountsWithoutUIUpdate();
        }
}

function ShowError(message) {
    $('#modalInfo').lightbox_me({
        centered: true,
        onLoad: function () {
            $('#infoMessage').html(message);
            $('#dialogImage').removeAttr('src').attr('src', '/images/error.ico');
           // $('#modalInfo').find('a:okButton').focus()
        },
        onClose: function () {
            RemoveOverlay();
        }
    });

}

function ShowWarning(message) {
    $('#modalInfo').lightbox_me({
        centered: true,
        onLoad: function () {
            $('#infoMessage').html(message);
            $('#dialogImage').removeAttr('src').attr('src', '/images/warn.ico');
           // $('#modalInfo').find('a:okButton').focus()
        },
        onClose: function () {
            RemoveOverlay();
        }
    });
}

function ShowMessage(message) {
    $('#modalInfo').lightbox_me({
        centered: true,
        onLoad: function () {
            $('#infoMessage').html(message);
            $('#dialogImage').removeAttr('src').attr('src', '/images/info.ico');
            //$('#modalInfo').find('a:okButton').focus()
        },
        onClose: function () {
            RemoveOverlay();
        }
    });
}

function ShowConfirmation(message, okDelegate) {
    $('#modalConfirm').lightbox_me({
        centered: true,
        onLoad: function () {
            $('#confirmMessage').html(message);
            $('#dialogConfirmImage').removeAttr('src').attr('src', '/images/warn.ico');
            $('#continueButton').click(function () {
                CloseConfirmation();
                okDelegate();
            });
            $('#modalConfirm').find('a:continueButton').focus()
        },
        onClose: function () {
            RemoveOverlay();
        }
    });
}

function CloseConfirmation() {
    $("#modalConfirm").hide();
    $('#modalConfirm').trigger('close');
}

function CloseMessage() {
    $("#modalInfo").hide();
    $('#modalInfo').trigger('close');
}

function AddCreateChannelUCToPage(channelName, userId) {
    ResizeCreateChannelPopup('#createChannelPopup');
    $("#updtateChannelPopup").hide();
    $("#createChannelPopup").show();
    //$.ajax({
    //    type: "POST",
    //    url: webMethodAddCreateChannelUCToModal,
    //    data: '{"channelname":' + "'" + channelName + "'" + ',"userId":'  + userId  + '}',
    //    dataType: "json",
    //    contentType: "application/json; charset=utf-8",
    //    success: function (response) {
    //        $("#createChannelPopup").html("").html(response.d);
    //    }
    //});
};

function ResizeCreateChannelPopup(element) {
    var $popUp = $(element);


    var pageHeight = (document.height !== undefined) ? document.height : document.body.offsetHeight;
    var $window = $(window).on('resize', function () {
        $popUp.height(pageHeight);
    }).trigger('resize'); //on page load


};


function CloseCreateChannel(element) {
    $("#createChannelPopup").hide();
    $("#updtateChannelPopup").hide();
}

function AddUpdateChannelToPage() {
    ResizeCreateChannelPopup("#updtateChannelPopup");
    $("#createChannelPopup").hide();
    $("#updtateChannelPopup").show();
};

function ResetPassword(etoken) {
    var newPassWord = $("#txtNewPassword").val();
    var reEnterPass = $("#txtReEnter").val();

    if (newPassWord.length < 8) {
        $("#spnMsgPassword").text("").text("Password is too short.");
        return;
    }
    else if (newPassWord != reEnterPass) {
        $("#spnMsgPassword").text("").text("Passwords do not match.");
        return;
    }
    else {
        var clientTime = getClientTime();
        var params = '{"clientDateTime":' + "'" + clientTime + "'" + ',"newPassword":' + "'" + newPassWord + "'" + ',"etoken":' + "'" + etoken + "'" + '}';
        $.ajax({
            type: "POST",
            url: webMethodResetPassword,
            data: params,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                alertify.success("Thank you! Your password was successfully reset.")
                if (response.d) {
                    $("#passwordForm").css("visibility", "hidden");
                    $("#passwordContent").html("<span id='spnPasswordChanged'>Thank you! Your password has been reset. <a id='ancLoginPasswordReset'  class='loginPasswordReset' onclick=loginModal('password-recovery')>Go Log In!</a> </span>")
                }
            },
            error: function () {
            }
        });
    }

};

function OpenCreateChannelConfirmation() {
    alertify.confirm("Please create a channel to access Production Studio.", function (e) {
        if (e) {
            RedirectToCreateChannel();
        }
        else {
            window.location.href = "/" + userUrl;
        }
    });
}

function ShowTutorialPlayer(videoId, isLandingPage) {
    if (videoId) {
        $('.playerboxtutorial').lightbox_me({
            centered: true,
            onLoad: function () {
                AddYouTubePlayer(videoId, 'relatedtutorial', isLandingPage);
            },
            overlayCSS: {
                background: 'black',
                opacity: .8
            },
            onClose: function () {
                if (isLandingPage) {
                    if (isPlayerOnMute) {
                        playerLandingPage.mute();
                    }
                    else {
                        playerLandingPage.unMute(100);
                    }
                }

                player = new YT.Player('relatedtutorial')
                $(".playerboxtutorial").html('<div id="relatedtutorial" class="playertutorial"></div> <div id="content-containertutorial"></div><a id="close_x" class="close close_x closePlayerBox" href="#">close</a>')
                player.destroy();
                RemoveOverlay();
            }
        });
    }
};

function ShowYouTubePlayer(videoId, isLandingPage) {

    $('.playerbox-youtube').lightbox_me({
        centered: true,
        onLoad: function () {
            AddYouTubePlayer(videoId, 'related-youtube', isLandingPage)
        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {
            player = new YT.Player('related-youtube')
            $(".playerbox").html('<div id="related-youtube" class="player"></div> <div id="content-container-youtube"></div><a id="close_x-youtube" class="close close_x closePlayerBox" href="#">close</a>')
            player.destroy();
            RemoveOverlay();
        }
    });
};

function AddYouTubePlayer(videoId, targetElement, isLandingPage) {

    if (isLandingPage) {
        playerLandingPage.mute();
    }

    if (videoId == undefined) {
        return;
    }

    function onYouTubeIframeAPIReady() {
        player = new YT.Player(targetElement, {
            //height: '600',
            //width: '800',
            videoId: videoId,
            playerVars: {
                autoplay: 1,
                html5: 1,
                rel: false,
        },
            events: {
                'onReady': onPlayerReady,
                'onStateChange': onPlayerStateChange
            }
        });
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
            done = true;
        }
    }
};

function ShowVimeoPlayer(videoId) {
    $('.playerbox-vimeo').lightbox_me({
        centered: true,
        onLoad: function () {
            AddVimeoPlayer(videoId)
        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {
            $("#vimeoBox .playerVimeo").empty();
            RemoveOverlay();
        }
    });
}

function AddVimeoPlayer(videoId) {
    var iframe = "<div id='divVimeoPlayer'><iframe id='myVideo' class='video-tracking' src='//player.vimeo.com/video/" + videoId + "?title=0&amp&autoplay=1;&amp;color=a4a9ab' width='579' height='321'  frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>";
    $("#vimeoBox .playerVimeo").html(iframe);

    window.$('.divVimeoPlayer').each(function () {

        // get the vimeo player(s)
        var iframe = window.$(this);
        var player = window.$f(iframe[0]);
        void 0;
        // When the player is ready, add listeners for pause, finish, and playProgress
        player.addEvent('ready', function () {
            void 0;

            //these are the three standard events Vimeo's API offers
            player.addEvent('play', onPlay);
            player.addEvent('pause', onPause);
            player.addEvent('finish', onFinish);
            // player.api('seekTo', 375);
        });
    });

    //define the custom events to push to Optimizely
    //appending the id of the specific video (dynamically) is recommended

    //to make this script extensible to all possible videos on your site
    function onPause(id) {
        void 0;
        window['optimizely'] = window['optimizely'] || [];
        window.optimizely.push(["trackEvent", id + "Pause"]);
    }

    function onFinish(id) {
        void 0;
        window['optimizely'] = window['optimizely'] || [];
        window.optimizely.push(["trackEvent", id + "Finish"]);
    }

    function onPlay(id) {
        void 0;
        window['optimizely'] = window['optimizely'] || [];
        window.optimizely.push(["trackEvent", id + "Play"]);
    }
}

function ShowStrimmPlayer(elem) {
    void 0;
    var videoKey = elem.id;
    
    var ext = videoKey.substr((videoKey.lastIndexOf('.') + 1));
    void 0;
    //if (seekTime == undefined) {
    //    seekTime = 0;
    //}

    //var playlist = [
    //    [
    //        { ext: videoKey },
    //    ]
    //];

    $('.playerbox-strimm').lightbox_me({
        centered: true,
        onLoad: function () {
            PlayVideoWithHtml5FlowPlayer(ext,videoKey);
        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {
            $("#playerFlow").empty();
            RemoveOverlay();
         
        }
    });
}

function PlayVideoWithHtml5FlowPlayer(ext,videoKey) {
    //console.log(playlist)
    //console.log(seekTime)
    //console.log(apiKey)
    var strType = "video/"+ ext;
    $("#playerFlow").flowplayer({
      
        clip: {
            sources: [
                  { type: strType, src: videoKey }
                  
            ]
        },
        bufferTime:5,
        ratio: 9 / 16,
        autoplay: true,
    mute:false
   
       // autoplay: true
    }).one('ready', function (ev, api) {
        // exclude devices which do not allow autoplay
        // because this will have no effect anyway
        //if (flowplayer.support.firstframe) {
        //    api.seek(seekTime).play(0);
        //}
        //console.log(api);
       
    }).on('finish', function (e) {
        
    });
}

function PlayVideoWithFlashFlowPlayer(playlist, seekTime, cfDomain, fpKey, callback) {
    //$("#playerFlow").flowplayer("/flowplayer/flash/3.2.17/flowplayer.commercial-3.2.17.swf", {
    //    //key: fpKey,
    //    plugins: {
    //        pseudo: {
    //            url: "flowplayer.pseudostreaming-3.2.13.swf"
    //        }
    //    },

    //    // clip properties
    //    clip: {

    //        // our clip uses pseudostreaming
    //        provider: 'pseudo',

    //        url: 'http://p.demo.flowplayer.netdna-cdn.com/vod/demo.flowplayer/bbb-800.mp4'
    //    }
    //});
    flowplayer("playerFlow", "https://releases.flowplayer.org/swf/flowplayer-3.2.18.swf", {
        // this will enable pseudostreaming support
        plugins: {
            pseudo: {
                url: "flowplayer.pseudostreaming-3.2.13.swf"
            }
        },

        // clip properties
        clip: {

            // our clip uses pseudostreaming
            provider: 'pseudo',

            url: 'https://p.demo.flowplayer.netdna-cdn.com/vod/demo.flowplayer/bbb-800.mp4'
        }

    });
    //$("#playerFlow").flowplayer("/flowplayer/flash/3.2.17/flowplayer.commercial-3.2.17.swf", {
    //    key: fpKey,
    //    playlist: playlist,
    //    play: { opecity: 0 },
    //    clip: {
    //        onBeforePause: function () {
    //            return false;
    //        },
    //        onFinish: function (clip) {
    //            if (clip.index == len - 1) {
    //                document.location.reload(true);
    //            }
    //        },
    //        provider: 'cloudfront',
    //        // combined streams are configured in the "streams" node as follows:
    //        onLastSecond: function (clip) {
    //            if (callback) {
    //                callback(videoKey);
    //            }
    //        }
    //    },
    //    // streaming plugins are configured under the plugins node
    //    plugins: {
    //        // here is our rtmp plugin configuration
    //        cloudfront: {
    //            url: "/flowplayer/plugins/rtmp/flowplayer.rtmp-3.2.13.swf",
    //            // netConnectionUrl defines where the streams are found
    //            netConnectionUrl: 'rtmp://s3b78u0kbtx79q.cloudfront.net/cfx/st'// + cfDomain
    //        },
    //        controls: {
    //            // url: "Flowplayer/commercial/flowplayer.controls-3.2.15.swf",
    //            buttonColor: 'rgba(0, 0, 0, 0.9)',
    //            buttonOverColor: '#000000',
    //            backgroundColor: '#D7D7D7',
    //            backgroundGradient: 'medium',
    //            sliderColor: '#FFFFFF',
    //            sliderBorder: '1px solid #808080',
    //            volumeSliderColor: '#FFFFFF',
    //            volumeBorder: '1px solid #808080',
    //            timeColor: '#000000',
    //            durationColor: '#535353',
    //            play: false,
    //            scrubber: false,
    //            width: 250
    //        },
    //    },
    //    canvas: {
    //        backgroundGradient: 'none'
    //    }
    //});
}

function PlayVideoWithRtmpFlowPlayer(videoKey, seekTime, cfDomain, fpKey, callback) {
    fpKey = $('#playerFlow').data('key');
    flowplayer("playerFlow", "/flowplayer/flash/3.2.17/flowplayer.commercial-3.2.17.swf", {
        key: fpKey
    });
    $f("playerFlow", "/flowplayer/flash/3.2.17/flowplayer.commercial-3.2.17.swf", {
        key: fpKey,
        playlist: [
          [
            { mp4: videoKey },
            { mp4: videoKey },
            { mp4: videoKey },
            { mp4: videoKey }
          ]
        ],
        play: { opacity: 0 },
        clip: {
            onBeforePause: function () {
                return false;
            },
            onFinish: function (clip) {
                if (clip.index == len - 1) {
                    document.location.reload(true);
                }
            },
            provider: 'cloudfront',
            // combined streams are configured in the "streams" node as follows:
            onLastSecond: function (clip) {
                if (callback) {
                    callback(videoKey);
                }
            }
        },
        // streaming plugins are configured under the plugins node
        plugins: {
            // here is our rtmp plugin configuration
            cloudfront: {
                url: "/flowplayer/plugins/rtmp/flowplayer.rtmp-3.2.13.swf",
                // netConnectionUrl defines where the streams are found
                netConnectionUrl: 'rtmp://' + cfDomain
            },
            controls: {
                url: "Flowplayer/commercial/flowplayer.controls-3.2.15.swf",
                buttonColor: 'rgba(0, 0, 0, 0.9)',
                buttonOverColor: '#000000',
                backgroundColor: '#D7D7D7',
                backgroundGradient: 'medium',
                sliderColor: '#FFFFFF',
                sliderBorder: '1px solid #808080',
                volumeSliderColor: '#FFFFFF',
                volumeBorder: '1px solid #808080',
                timeColor: '#000000',
                durationColor: '#535353',
                play: false,
                scrubber: false,
                width: 250
            },
        },
        canvas: {
            backgroundGradient: 'none'
        }
    });
}

function TriggerCreateChannel() {
    $("#btnCreateChannel").trigger("click");
    $("#createChannelLink").trigger("click");
}

function emailCurrentPage() {
    window.location.href = "mailto:?subject=" + document.title + "&body=" + escape(window.location.href);
}

var InterestsManager = {
    init: function (userId) {
        id = userId;

        $('#chkAnimalsAndPets').prop('checked', false);
        $('#chkHomeGarden').prop('checked', false);
        $('#chkAutomotive').prop('checked', false);
        $('#chkHowTo').prop('checked', false);
        $('#chkComedy').prop('checked', false);
        $('#chkKidsFamily').prop('checked', false);
        $('#chkCookingFood').prop('checked', false);
        $('#chkMusicArt').prop('checked', false);
        $('#chkDiscovery').prop('checked', false);
        $('#chkOther').prop('checked', false);
        $('#chkEducation').prop('checked', false);
        $('#chkReligion').prop('checked', false);
        $('#chkEntertainment').prop('checked', false);
        $('#chkSports').prop('checked', false);
        $('#chkFashionStyle').prop('checked', false);
        $('#chkTechnology').prop('checked', false);
        $('#chkGaming').prop('checked', false);
        $('#chkTravelLeisure').prop('checked', false);
        $('#chkHealthFitness').prop('checked', false);
    },

    loadInterests: function (userId) {
        $.ajax({
            type: "POST",
            url: GetUserInterests,
            cashe: false,
            data: '{"userId":' + userId + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var userInterests = response.d;
                var count = 0;

                if (userInterests) {
                    $.each(userInterests, function (i, c) {
                        switch (c.CategoryId) {
                            case 2:
                                //$('#chkAnimalsPets').prop('checked', !c.Unsubscribed);
                                break;
                            default:
                                break;
                        };
                    });
                }
            },
            complete: function (response) {
            }
        });

    },

    checkSelectedAtleastOne: function () {
        var selected = false;
        var i = 0;

        $(".checkBoxInterestSelect").each(function () {
            if ($(this).is(':checked')) {
                i += 1;
            }
        });

        if (i >= 5) {
            $("#userInterests_Submit").removeClass('userInterests_SubmitDisable').addClass('userInterests_Submit');
        }
        else {
            $("#userInterests_Submit").removeClass('userInterests_Submit').addClass('userInterests_SubmitDisable');
        }


        return i;
    },

    saveUserInterests: function () {
        var count = this.checkSelectedAtleastOne();

        if (count < 5) {
            return;
        }

        var interests = '';

        interests = this.addToList(interests, $('#chkAnimalsAndPets').is(':checked'), 2);
        interests = this.addToList(interests, $('#chkHomeGarden').is(':checked'), 20);
        interests = this.addToList(interests, $('#chkAutomotive').is(':checked'), 3);
        interests = this.addToList(interests, $('#chkHowTo').is(':checked'), 9);
        interests = this.addToList(interests, $('#chkComedy').is(':checked'), 19);
        interests = this.addToList(interests, $('#chkKidsFamily').is(':checked'), 10);
        interests = this.addToList(interests, $('#chkCookingFood').is(':checked'), 6);
        interests = this.addToList(interests, $('#chkMusicArt').is(':checked'), 11);
        interests = this.addToList(interests, $('#chkDiscovery').is(':checked'), 14);
        interests = this.addToList(interests, $('#chkOther').is(':checked'), 13);
        interests = this.addToList(interests, $('#chkEducation').is(':checked'), 4);
        interests = this.addToList(interests, $('#chkReligion').is(':checked'), 21);
        interests = this.addToList(interests, $('#chkEntertainment').is(':checked'), 5);
        interests = this.addToList(interests, $('#chkSports').is(':checked'), 1);
        interests = this.addToList(interests, $('#chkFashionStyle').is(':checked'), 18);
        interests = this.addToList(interests, $('#chkTechnology').is(':checked'), 15);
        interests = this.addToList(interests, $('#chkGaming').is(':checked'), 7);
        interests = this.addToList(interests, $('#chkTravelLeisure').is(':checked'), 17);
        interests = this.addToList(interests, $('#chkHealthFitness').is(':checked'), 8);

        interests = interests.slice(0, -1);

        $.ajax({
            type: "POST",
            url: UpdateUserInterests,
            cashe: false,
            data: '{"userId":' + userId + ',"interests":' + "'" + interests + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var success = response.d.IsSuccess;
                if (success) {
                    alertify.success("Your interests were successfully updated");
                    $('#userInterestsModal').trigger('close');
                    $('#userInterestsModal').hide();
                }
                else {
                    alertify.error("Something happped. Your interests were not updated. Please try again later");
                }
            },
            complete: function (response) {
            }
        });
    },

    addToList: function(interests, selected, interestId) {
        if (selected && selected == true) {
            interests += (interestId + ',');
        }
        return interests;
    },

    closePopup: function() {
        $('#userInterestsModal').trigger('close');
        $('#userInterestsModal').hide();
    }
};

function ShowSnippetPopup(element) {

    var titleStr = $(element).attr("title");
    $('#snippetPopup').lightbox_me({
        centered: true,
        onLoad: function () {
            $("#snippetContent").text("").text(titleStr);

        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {


        }
    });
};
getInterests = function () {
    return InterestsManager;
};




