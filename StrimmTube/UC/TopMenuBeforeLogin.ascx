<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenuBeforeLogin.ascx.cs" Inherits="StrimmTube.UC.TopMenuBeforeLogin" %>


<link href="/Plugins/alertify/alertify.core.css" rel="stylesheet" />
<link href="/Plugins/alertify/alertify.default.css" rel="stylesheet" id="toggleCSS" />
<script src="/Plugins/alertify/alertify.js"></script>
<%--<script type="text/javascript" src="http://l2.io/ip.js?var=userip"></script>--%>
<script language="JavaScript" src="https://ssl.geoplugin.net/javascript.gp?k=31a4549afc6ac990" type="text/javascript"></script>
<script type="text/javascript" src="https://js.maxmind.com/js/apis/geoip2/v2.1/geoip2.js"></script>
<%--<script src="https://ssl.geoplugin.net/json.gp?k=Fqw9ch1k6Han28xP"></script>--%>
<script src="../Cryptojs/components/core.js"></script>
<script src="../Cryptojs/components/sha256.js"></script>

<script src="../Cryptojs/components/enc-base64.js"></script>
<script type="text/javascript">
    //var country;
    //var userip;
    var isBefore = true;//for search control
    var ip;
    var app;
    var browseMenu = "<%=browseMenuHtml%>";
    var isMenueVisible = false;
    $(document).ready(function () {
        $(".mainMenuBL").click(function (e) {
            e.preventDefault();
            var $div = $(this).next('.mainMenuBLOpen');
            $(".mainMenuBLOpen").not($div).hide();
            console.log("hide menu")
            if ($div.is(":visible")) {

                $div.hide();

                $(".mainMenuBL").removeClass("active")
            } else {
                $div.show();

                $(".mainMenuBL").addClass("active")
            }
        });
        $('html').click(function (e) {

            if ($(e.target).closest('.mainMenuBL').length != 0) {
                return false;
            }
            else {
                var $div = $('.mainMenuBLOpen');
                if ($div.is(":visible") && isMenueVisible == true) {
                    if ($(".subBlockBusiness").is(":hidden")) {
                        $div.hide();
                        $(".mainMenuBL").removeClass("active");
                        isMenueVisible = false;
                    }


                }
                else {
                    isMenueVisible = true;
                }
            }

        });

        $("#chkBxShowHidePassword").attr('checked', false);

        $("#divChannelsCategory ul").empty().html(browseMenu);

        app = window.getApp();

        var onSuccess = function (location) {

            if (location) {
                if (app) {
                    app.init(location);
                }
            }
        };

        var onError = function (error) {
            //alert(
            //    "Error:\n\n"
            //    + JSON.stringify(error, undefined, 4)
            //);
        };

        if (typeof geoip2 != "undefined" && app && !app.isInit()) {
            geoip2.city(onSuccess, onError);
        }

        $(document).keyup(function (event) {
            if (event.keyCode == 13) {
                $("#ancLogin").trigger('click');
            }

        });

        $(".homeActionsHolder .btnCreate").removeAttr("onclick").attr("onclick", "loginModal('create-channel')");
    });

    function ClearLoginCreds() {
        $('#txtUserName').val('');
        $('#txtPassword').val('');
    }

    function ShowHidePasswordLogin() {

        if ($("#loginBox #txtPassword").attr("type") == "password") {
            $("#loginBox #txtPassword").attr("type", "text");
        }
        else {
            $("#loginBox #txtPassword").attr("type", "password");
        }

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

    function CloseFE() {
        $("#divForgotPass").hide();
        RemoveOverlay();
        location.href = "/";
    }

    function ResendLink(e) {
        $("#divResendLink").lightbox_me({
            centered: true,
            onLoad: function () {
                $('#loginBox').hide();

            },
            onClose: function () {
                RemoveOverlay();
            },
            closeSelector: "close"
        });
    }

    function CloseRL() {
        $("#divResendLink").hide();
        RemoveOverlay();
    }

    function closeModal(element) {
        //var ele = $("#" + element);
        element.lightbox_me();
        element.trigger('close');
        RemoveOverlay();
    }

    var WebMethodForgotPassword = "/../WebServices/UserService.asmx/ForgotPass";
    var WebMethodResendActivationLink = "/../WebServices/UserService.asmx/ResendActivationLink";
    var WebMethodCheckeIfLocked = "/../WebServices/UserService.asmx/CheckeIfLocked";
    var WebMethodGetUserByEmail = "/../WebServices/UserService.asmx/GetUserByEmail"
    var IsUserNameUniqueService = "/../WebServices/UserService.asmx/IsUserNameUnique";
    var WebMethodInsertNewUserFromFacebookLogin = "/../WebServices/UserService.asmx/InsertNewUserFromFacebookLogin";
    var WebMethodSetUserIdInSession = "/../WebServices/UserService.asmx/SetUserIdInSession";

    function ResendLinkToEmail() {
        var username = $("#txtResendLink").val();

        if (username != "") {
            if (validateEmail(username) == true) {
                $.ajax({
                    type: "POST",
                    url: WebMethodResendActivationLink,
                    data: '{"email":' + "'" + username + "'" + '}',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        //$("#spnResendLink").text(response.d);
                        $("#txtResendLink").val("");
                        $("#divResendLink").hide();
                        alertify.success("Activation email was resent to you!");
                    }
                });
            }
            else {
                $("#spnResendLink").text("").text("Please enter a valid e-mail");
            }
        }
        else {
            $("#spnResendLink").text("").text("Please enter your e-mail");
        }
    }

    $("#ancLogin").keypress(function (e) {
        if (e.which == 13) {
            e.preventDefault();
            e.Login();
        }
    });
</script>


<div class="mainTopHoplder">
    <a href="/browse-channel?category=AllChannels" class="absGuide">Watch Now</a>

    <div id="divChannelsCategory">
        <div class="closeSymbol" onclick="CloseChannelCategoryMenu()">&#10006;</div>
        <div class="ddHolder"><span class="trBrowseChannels trBrowseChannelsBefore"></span></div>
        <ul>
        </ul>
        <div class="galleryView">
            <a href="/all-channels?category=All%20Channels" class="btnGalleryView">Channel Gallery</a>
        </div>
    </div>



    <div class="subBlock subBlockBusiness" onclick="CloseSubBlockBusiness()">
        <div class="closeSymbol">&#10006;</div>
        <div class="ddHolder"><span class="trBusinesses trBusinessesBefore"></span></div>
        <div class="businessesMenuBlock">
            <a href="/home" class="submobileNav">Business Solutions</a>
            <div class="businessesMenuBlockText">Bring your business or organization to a whole new level</div>
            <a href="/home" class="businessesMenuBlockMore">learn more</a>
        </div>
        <div class="businessesMenuBlock">
            <a href="/create-tv/features-and-benefits" class="submobileNav">features & benefits</a>
            <div class="businessesMenuBlockText">Get access to unique features and benefits</div>
            <a href="/create-tv/features-and-benefits" class="businessesMenuBlockMore">learn more</a>

        </div>
        <div class="businessesMenuBlock businessesMenuBlockNoBorder">
            <a href="/create-tv/pricing" class="submobileNav">plans & pricing</a>
            <div class="businessesMenuBlockText">Set yourself apart, and choose from affordable plans</div>
            <a href="/create-tv/pricing" class="businessesMenuBlockMore">learn more</a>

        </div>
    </div>

    <div class="mainMenuBL"></div>

    <div class="mainMenuBLOpen">
        <div class="fullvh">
            <a class="mobileNav mobileNavCreateChannel customIconNone" onclick="loginModal('sameLocation')">Create Channel</a>
            <a href="/learn-more" class="mobileNav whatIsStrimmMobile">Become a Producer </a>


            <div class="businessesMobileHolder">
                <a href="../create-tv/pricing" class="mobileNav mobileNavHeader businessesMobileIcon  businessesMobileMenu" <%--onclick="ToggleBusinessMobileMenu()"--%>>Plans & Pricing
          <%--        <div id="divImgBusinesses" class="divImg divImgBusinesses">&#10148;</div>--%>
                </a>

            </div>
            <a href="/#FAQ" class="mobileNav customIconFAQ">FAQ </a>
            <div class="guideMobileHolder">
                <div id="divBrowseChannelsHolder">

                    <div id="divBrowseChannels">
                        <a class="mobileNav mobileNavHeader tvGuide" onclick="ToggleChannels()">Watch Now
                        <div id="divImg" class="divImg">&#10148;</div>
                        </a>
                    </div>
                </div>


            </div>


            <a href="/all-channels?category=All%20Channels" class="mobileNav customGallery">Channel Gallery</a>
            <a href="sign-up" class="mobileNav  customSignUp">Sign Up Free</a>
            <a class="mobileNav  customLogin" onclick="loginModal('sameLocation')">Log in</a>


        </div>
    </div>
    <a href="/" class="divLogo">
        <img src="/images/Srtimm-LOGO.svg" />
    </a>
      <a href="../create-tv/pricing#pricingAnc" class="pricingHome" >Pricing </a>
    <a id="LogIn" class="mainSignIn" onclick="loginModal('sameLocation')">sign in</a>
    <a href="../create-tv/pricing" id="exploreBusinessOpt">Get Started</a>
</div>

<div class="secondaryMenu">
    <div class="businessMenu">
        <%--   <a href="../home" class="businessNavigation homeIcon"></a>--%>
        <a href="/" class="businessNavigation businessNavigationA">business solutions</a>
        <a href="/create-tv/features-and-benefits" class="businessNavigation businessNavigationB">features & benefits</a>
        <a href="/create-tv/pricing" class="businessNavigation businessNavigationC">plans & pricing</a>
        <a href="/FAQ" class="businessNavigation businessNavigationC">FAQ</a>


    </div>
</div>


<div class="menuBL">
    <div id="divTopWrapper" class="ShadowDropDown ">

        <div id="divTop">


            <a href="/">
                <div id="divLogo">
                    <img src="/images/Srtimm-LOGO.svg" />
                </div>
            </a>


            <div class="leftTopHolderBeforeLogin">
                <div class="whatIsStrimmTop">
                    <a href="/LearnMore.aspx">what is strimm</a>
                </div>

                <%--        <div id="divBrowseChannelsHolder">
                <div id="divBrowseChannels" >
                    <a onclick="ToggleChannels()">TV Guide
                        <div id="divImg" class="divImg">▼</div>
                    </a>
                </div>--%>
            </div>
            <a href="/" class="businessSolBeforeLog">Business Solutions</a>
            <%--            <div id="divChannelsCategory">
                <div class="ddHolder">
                    <span class="trBrowseChannels"></span>
                </div>
                <ul>
                </ul>
                <div class="galleryView">
                    <a href="/all-channels?category=All%20Channels" class="btnGalleryView">Channel Gallery</a>
                </div>
            </div>--%>


            <div class="advancedSearch" style="display: none">
                <a class="advanced" title="advanced search" href="#" onclick="GetSearchControlBefore()">
                    <span class="spnAdvSearch">search </span>
                    <div class="AdvSearchImgHN"></div>
                </a>
            </div>

        </div>
        <div id="topRightHolderBL">
            <div class="btnCreateChannel">
                <a id="createChannelLink" class="spnCreateChannel" onclick="loginModal('create-channel')">create channel</a>
            </div>

            <div id="divTopNav">
                <%-- <a class="signUpFree" href="sign-up" >sign up free </a>--%>
            </div>
        </div>
    </div>

    <!--LOGIN BOX-->
    <div id="loginBox">


        <div class="loginBoxContainer">
            <h3 id="see_id" class="sprited">Log in to Strimm</h3>
            <div id="divLoginLeft">

                <span id="spanMessage"></span>
                <div id="loginForm">
                    <div class="optionOr">or</div>
                    <label>
                        <input type="email" autocapitalize="none" autocorrect="off" id="txtUserName" placeholder="Enter email" autocomplete="on" /></label>
                    <label>
                        <input type="password" id="txtPassword" placeholder="Enter password" /></label>
                    <label id="lblShowHidePass">
                        <input type="checkbox" id="chkBxShowHidePassword" onchange="ShowHidePasswordLogin()" />
                        Show password</label>
                    <div class="fogortresendHolder">
                        <a id="forgotPass" onclick="forgotModal()">forgot password?</a>
                        <%--   <a id="resentCofirmLink" onclick="ResendLink()">resend activation link</a>--%>
                    </div>
                </div>
                <div id="actions">
                    <a id="ancLogin" onclick="Login()">log in</a>

                    <label id="lblKeepMeLoggedIn">
                        <input type="checkbox" checked="checked" id="keepLogIn">Keep me logged in</label>

                    <div class="signUpNow">
                        <span id="spnDontYouHave">Don't have an account?</span>
                        <a id="ancSignUp" href="/sign-up">Sign up. It's free.</a>
                    </div>

                    <script>
                        var userInfo;

                        //FACEBOOK LOGIN
                        function LoginWithFacebook() {
                            FB.getLoginStatus(function (response) {
                                GetUserInfo(false);
                            });
                        }

                        function ManualLoginWithFacebook() {
                            FB.login(function (response) {
                                GetUserInfo(false);
                            },
                            {
                                scope: 'read_stream, user_birthday, user_location, user_hometown, user_photos, email',
                                return_scopes: true
                            },
                            { perms: "user_hometown,user_about_me,email,user_address,age_range" });
                        }

                        var userName = "";
                        var userId = "";

                        function GetUserByEmail(email) {
                            var user;
                            $.ajax({
                                type: "POST",
                                url: WebMethodGetUserByEmail,
                                data: '{"email":' + "'" + email + "'" + '}',
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                success: function (response) {

                                    if (response.d != null) {
                                        user = response.d;
                                        //console.log(user);

                                        $.each(response, function (i, d) {
                                            userName = d.UserName;
                                            userId = d.UserId;

                                            UpdateUserLocation(userName);

                                            $.ajax({
                                                type: "POST",
                                                url: WebMethodSetUserIdInSession,
                                                data: '{"userId":' + "'" + userId + "'" + '}',
                                                dataType: "json",
                                                contentType: "application/json; charset=utf-8",
                                                success: function (response) {
                                                    window.location.href = "/" + d.PublicUrl;
                                                }
                                            });

                                        });

                                    }
                                    else {
                                        //console.log("GetUserByEmail popup")
                                        $("#divGetPublicname").lightbox_me({
                                            centered: true,
                                            onLoad: function () {
                                                $('#loginBox').hide();
                                            },
                                            onClose: function () {
                                                facebookLogout();
                                                RemoveOverlay();
                                            },
                                            closeSelector: "close"
                                        });

                                    }

                                }
                            });

                            return user;
                        };

                        function GetUserInfo(isAutoLogin) {
                            FB.api('/me?fields=gender,location,age_range,first_name,last_name,email,id', function (res) {
                                //console.log(JSON.stringify(res));
                                userInfo = res;

                                var email = userInfo.email;

                                console.log(userInfo)
                                console.log(email);
                                //replace for ajax call 
                                //if user return null up public name modal
                                //if user not null save userId session and redirect to board
                                if (email != undefined && email != '' && email != null) {

                                    var user;

                                    $.ajax({
                                        type: "POST",
                                        url: WebMethodGetUserByEmail,
                                        data: '{"email":' + "'" + email + "'" + '}',
                                        dataType: "json",
                                        contentType: "application/json; charset=utf-8",
                                        success: function (response) {
                                            if (response.d != null) {
                                                user = response.d;

                                                $.each(response, function (i, d) {
                                                    userName = d.UserName;
                                                    userId = d.UserId;

                                                    UpdateUserLocation(userName);

                                                    var cookieValue = getCookie("userId");

                                                    if ((userId == cookieValue && isAutoLogin) || !isAutoLogin) {
                                                        $.ajax({
                                                            type: "POST",
                                                            url: WebMethodSetUserIdInSession,
                                                            data: '{"userId":' + "'" + userId + "'" + '}',
                                                            dataType: "json",
                                                            contentType: "application/json; charset=utf-8",
                                                            success: function (response) {
                                                                if (!isAutoLogin) {
                                                                    setLoginCookies(userId, true);
                                                                }
                                                                window.location.href = "/" + d.PublicUrl;
                                                            }
                                                        });
                                                    }
                                                });
                                            }
                                            else {
                                                //console.log("GetUserByEmail popup")
                                                $("#divGetPublicname").lightbox_me({
                                                    centered: true,
                                                    onLoad: function () {
                                                        $('#loginBox').hide();
                                                    },
                                                    onClose: function () {
                                                        RemoveOverlay();
                                                    },
                                                    closeSelector: "close"
                                                });
                                            }
                                        }
                                    });
                                }
                                else {
                                    console.log('FB did not return valid e-mail');
                                    window.location.href = "/";
                                }
                            });
                        };

                        function ClosePublicNameModal() {
                            facebookLogout();
                            $("#divGetPublicname").hide();
                        };

                        function IsUserNameUnique(userName) {
                            var isUnique = false;

                            userName = userName.replace("'", "%27");
                            $.ajax({
                                type: "POST",
                                url: IsUserNameUniqueService,
                                data: '{"userName":' + "'" + userName + "'" + '}',
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                success: function (response) {
                                    if (response.d) {
                                        isUnique = response.d;

                                    }

                                },
                                async: false,
                                error: function (response) {
                                    //console.log(response.status + " " + response.statusText);
                                }
                            });
                            return isUnique;
                        };

                        //check public name value on fly
                        function ValidatePublicName() {
                            var userName = $("#txtPublicName").val();
                            var userNameIsUnique = IsUserNameUnique(userName.trim());
                            //console.log(userNameIsUnique);
                            if (userName.trim().length < 3 || userName.trim().length > 15) {
                                $("#divGetPublicname #spnError").text("").text("Public Name must be 3-15 characters");
                                return false;

                            }
                            else if (!userNameIsUnique) {
                                $("#divGetPublicname #spnError").text("").text("Public Name is taken");
                                return false;

                            }
                            else {
                                $("#divGetPublicname #spnError").hide();
                                return true;

                            }
                        }

                        $("#txtPublicName").focus(function () {
                            $("#divGetPublicname #spnError").text("");
                        });

                        function CreateNewUserFromFacebookLogin() {

                            var firstname = userInfo.first_name;
                            var lastName = userInfo.last_name;
                            var ageRange = userInfo.age_range;
                            var gender = "MALE";

                            if (gender != "undefined" && gender != undefined) {
                                gender = userInfo.gender.toUpperCase();
                            }

                            var email = userInfo.email;
                            var age;
                            var publicName;

                            var userInfoObj = null;
                            var userip = '';
                            var country = '';
                            var city = '';
                            var state = '';
                            var zip = '';

                            if (app != null && app != undefined) {
                                var user = app.getUserInfo();

                                if (user != null && user != undefined) {
                                    userInfoObj = JSON.parse(user);
                                    userip = userInfoObj != null ? userInfoObj.UserIp : '';
                                }
                                country = userInfoObj != null ? userInfoObj.Country : '';
                                city = userInfoObj != null ? userInfoObj.City : '';
                                state = userInfoObj != null ? userInfoObj.State : '';
                                zip = userInfoObj != null ? userInfoObj.PostalCode : '';
                            }

                            $.each(ageRange, function (key, value) {
                                // define if one of value is min 13
                                if (value < 18) {
                                    age = value;
                                }
                            });

                            //if min 13 show message
                            if (age < 18) {
                                alertify.alert("You are too young to sign up!");
                                ClosePublicNameModal();
                            }
                            else {

                                var isuserNameValid = ValidatePublicName();
                                if (!isuserNameValid) {
                                    return;
                                }
                                else {
                                    publicName = $("#txtPublicName").val().replace(/\s/g, "");
                                    var params = '{"userName":' + "'" + publicName + "'" + ',"userIp":' + "'" + userip + "'" + ',"email":' + "'" + email + "'" +
                                                        ',"firstName":' + "'" + firstname + "'" + ',"lastName":' + "'" + lastName + "'" + ',"countryName":' + "'" + country + "'" + ',"gender":' + "'" + gender + "'" +
                                                        ',"facebookId":' + "'" + userInfo.id + "'" + ',"city":' + "'" + city + "'" + ',"state":' + "'" + state + "'" + ',"zipcode":' + "'" + zip + "'" + '}';
                                    $.ajax({
                                        type: "POST",
                                        url: WebMethodInsertNewUserFromFacebookLogin,
                                        data: params,
                                        dataType: "json",
                                        contentType: "application/json; charset=utf-8",
                                        success: function (response) {
                                            var userId = response.d;
                                            if (userId > 0) {
                                                document.location.href = "/" + publicName;
                                                setLoginCookies(userId, true);
                                            }
                                            else {
                                                document.location.href = "/home";
                                            }
                                        },
                                        error: function (response) {
                                        }
                                    });
                                }
                            }
                        }

                    </script>

                </div>
            </div>
        </div>
        <div id="divLoginRight">
            <div class="facebookLogSignUp" onclick="ManualLoginWithFacebook();">
                <fb:login-button show-faces="false"
                    scope="read_stream, user_birthday, user_location, user_hometown, user_photos, email"
                    perms="user_hometown,user_about_me,email,user_address,age_range"
                    autologoutlink="true" width="200" size="xlarge" max-rows="1"
                    onlogin="LoginWithFacebook();" style="opacity: 0; width: 100%;">
                            
                            </fb:login-button>
            </div>
            <div class="rightFBconnection">
                <ul>
                    <li>Connect with your friends to share your channels</li>
                    <li>Chat with channel owner and others</li>
                    <li>Instant login</li>
                </ul>
            </div>

            <a class="close close_x closeLogin" href="#" onclick="ClearLoginCreds()">close</a>
        </div>
    </div>
</div>

<!--FORGOT PASSWORD BOX-->
<div id="divForgotPass">
    <div class="divPopupContainer">
        <div class="popupHeader">Forgot password </div>
        <%--<span class="spnPopUpMes">Please enter your email address </span><br />--%>

        <input id="txtEmailForgot" placeholder="Enter your email address">

        <a class="close_x closeFogotPassword" onclick="CloseFE()"></a>

        <a id="ancForgotPass" onclick="ForgotPass()">Get Password</a>
        <span id="spnForgotPass"></span>
    </div>
</div>

<div id="divResendLink">
    <div class="divPopupContainer">
        <a class="close_x" onclick="CloseRL()"></a>
        <div class="popupHeader">Resend activation link </div>

        <input id="txtResendLink" placeholder="Enter your email address" />
        <a id="resendLink" onclick="ResendLinkToEmail()">Resend</a>
    </div>
</div>

<div id="divGetPublicname" style="display: none;">

    <a class="close_x closeGetPublicname" onclick="ClosePublicNameModal()"></a>

    <div class="divPopupContainer">

        <div class="infoPlasAbs" title="Public name is a name shown to public on your Network and Channels. 
It can be your name or a company name. 
It should have 3-15 characters and may include space, dash, 
apostrophe and/or '&' characters only.">
        </div>
        <input id="txtPublicName" placeholder="Please enter your public name" />
        <span id="spnError"></span>
        <a id="ancPublicName" onclick="CreateNewUserFromFacebookLogin()">Ok</a>
    </div>
</div>



<div class="playerboxtutorial" style="display: none;">
    <a id="relatedtutorial" class="playertutorial"></a>
    <div id="content-containertutorial">
    </div>
    <a id="close_x" class="close close_x closePlayerBox" href="#">close</a>
</div>


