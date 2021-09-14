<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="StrimmTube.Dashboard" %>

<%@ Register Src="~/UC/CreateChannelUC.ascx" TagPrefix="uc" TagName="CreateChannelUC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    My Network | Strimm  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://www.strimm.com/<%=userBoardUrl%>/" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <%: System.Web.Optimization.Styles.Render("~/bundles/dashboard/css") %>
    <link href="/Plugins/WalkThru/css/powertour.2.5.1.css" rel="stylesheet" />
    <link href="/Plugins/WalkThru/css/powertour-style-clean.css" rel="stylesheet" />
    <link href="/Plugins/WalkThru/css/powertour-connectors.css" rel="stylesheet" />
    <link href="/Plugins/WalkThru/css/animate.min.css" rel="stylesheet" />
    <%: System.Web.Optimization.Scripts.Render("~/bundles/dashboard/js") %>
    <<%--script src="/Plugins/WalkThru/js/powertour.2.5.1.js"></script>--%>
   
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="/Plugins/Share42/share42.js"></script>
    <script type="text/javascript">

        /* share42.com | 28.05.2014 | (c) Dimox */
        window.onload = function () { var e = document.getElementsByTagName('div'); for (var k = 0; k < e.length; k++) { if (e[k].className.indexOf('share42init') != -1) { if (e[k].getAttribute('data-url') != -1) var u = e[k].getAttribute('data-url'); if (e[k].getAttribute('data-title') != -1) var t = e[k].getAttribute('data-title'); if (e[k].getAttribute('data-image') != -1) var i = e[k].getAttribute('data-image'); if (e[k].getAttribute('data-description') != -1) var d = e[k].getAttribute('data-description'); if (e[k].getAttribute('data-path') != -1) var f = e[k].getAttribute('data-path'); if (e[k].getAttribute('data-icons-file') != -1) var fn = e[k].getAttribute('data-icons-file'); if (!u) u = location.href; if (!t) t = document.title; if (!fn) fn = 'icons.png'; function desc() { var meta = document.getElementsByTagName('meta'); for (var m = 0; m < meta.length; m++) { if (meta[m].name.toLowerCase() == 'description') { return meta[m].content; } } return ''; } if (!d) d = desc(); u = encodeURIComponent(u); t = encodeURIComponent(t); t = t.replace(/\'/g, '%27'); i = encodeURIComponent(i); d = encodeURIComponent(d); d = d.replace(/\'/g, '%27'); var fbQuery = 'u=' + u; if (i != 'null' && i != '') fbQuery = 's=100&p[url]=' + u + '&p[title]=' + t + '&p[summary]=' + d + '&p[images][0]=' + i; var s = new Array('"#" data-count="fb" onclick="window.open(\'https://www.facebook.com/sharer.php?m2w&' + fbQuery + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=550, height=440, toolbar=0, status=0\');return false" title="Share on Facebook"', '"#" data-count="gplus" onclick="window.open(\'https://plus.google.com/share?url=' + u + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=550, height=440, toolbar=0, status=0\');return false" title="Share on Google+"', '"#" data-count="pin" onclick="window.open(\'https://pinterest.com/pin/create/button/?url=' + u + '&media=' + i + '&description=' + t + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=600, height=300, toolbar=0, status=0\');return false" title="Pin It"', '"#" data-count="twi" onclick="window.open(\'https://twitter.com/intent/tweet?text=' + t + '&url=' + u + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=550, height=440, toolbar=0, status=0\');return false" title="Share on Twitter"'); var l = ''; for (j = 0; j < s.length; j++) l += '<a rel="nofollow" style="display:inline-block;vertical-align:bottom;width:32px;height:32px;margin:0 6px 6px 0;padding:0;outline:none;background:url(image.php?i=facebook,google-plus,pinterest,twitter&size=32&) -' + 32 * j + 'px 0 no-repeat" href=' + s[j] + ' target="_blank"></a>'; e[k].innerHTML = '<span id="share42">' + l + '</span>'; } }; };

        var userAvatar = "<%=userAvatar%>";
        var userId = "<%=userId%>";
        var boardOwnerUserId = '<%=boardOwnerUserId%>';
        var favChannelCount = '<%=favoriteChannelCount%>';
        var userName = "<%=userName%>";
        var isOwner = '<%=isOwner%>';
        var userDescription = '<%=userDescription%>';
        var isIE8 = false;



        $(document).ready(function () {
            if ($('.mobileMenu').css('display') == 'block') {
                $("#stepFiveHolder").remove();
               // $(".sideContentHolder").hide();
                $(".btnDashboardMobileMenu.btnChannel").on("click", function () {
                    $(".channelsHolderDashboard").show();
                    $(".sideContentHolder").hide();
                    $(".btnDashboardMobileMenu").removeClass("btnDashboardMobileMenuActive");
                    $(".btnDashboardMobileMenu.btnChannel").addClass("btnDashboardMobileMenuActive")
                });
                $(".btnDashboardMobileMenu.btnFavorites").on("click", function () {
                    $(".channelsHolderDashboard").hide();
                    $(".sideContentHolder").show();
                    $(".btnDashboardMobileMenu").removeClass("btnDashboardMobileMenuActive");
                    $(".btnDashboardMobileMenu.btnFavorites").addClass("btnDashboardMobileMenuActive")
                });
                $(".btnDashboardMobileMenu.btnTop").on("click", function () {
                    $(".channelsHolderDashboard").hide();
                    $(".sideContentHolder").show();
                    $(".btnDashboardMobileMenu").removeClass("btnDashboardMobileMenuActive");
                    $(".btnDashboardMobileMenu.btnTop").addClass("btnDashboardMobileMenuActive")
                });
                $(".btnDashboardMobileMenu.btnBio").on("click", function () {
                    $(".channelsHolderDashboard").hide();
                    $(".sideContentHolder").show();
                    $(".btnDashboardMobileMenu").removeClass("btnDashboardMobileMenuActive");
                    $(".btnDashboardMobileMenu.btnBio").addClass("btnDashboardMobileMenuActive")
                });

            }
            //Walk Thrue//
            var isNewUser = getCookie("isNewUser"); // set on user confirmation (confirmation.aspx)
            //console.log(isNewUser);

            $.powerTour({
                tours: [
                        {
                            trigger: '#ancStartTour',
                            startWith: 1,
                            width: 600,
                            height: 300,
                            easyCancel: false,
                            escKeyCancel: true,
                            scrollHorizontal: false,
                            keyboardNavigation: true,
                            loopTour: false,
                            highlightStartSpeed: 200,// new 2.5.0
                            highlightEndSpeed: 200,// new 2.3.0
                            highlight: true,
                            keepHighlighted: true,
                            onStartTour: function (ui) { },
                            onEndTour: function (ui) {
                                setCookie('isNewUser', false, 0);
                            },
                           
                            steps: [
                                    {
                                        hookTo: '',//not needed
                                        content: '#step-one',
                                        width: 350,
                                        position: 'sc',
                                        offsetY: 0,
                                        offsetX: 0,
                                        fxIn: 'lightSpeedIn',
                                        fxOut: 'bounceOutLeft',
                                        showStepDelay: 0,
                                        center: 'step',
                                        scrollSpeed: 400,
                                        scrollEasing: 'swing',
                                        scrollDelay: 0,
                                        timer: '00:00',
                                        highlight: true,
                                        keepHighlighted: false,
                                        keepVisible: false,// new 2.2.0
                                        onShowStep: function (ui) { },
                                        onHideStep: function (ui) { }
                                    },
                                    {
                                        hookTo: '#divPlaceHolderStepTwo',
                                        content: '#step-two',
                                        width: 300,
                                        position: 'bm',
                                        offsetY: 10,
                                        offsetX: 0,
                                        fxIn: 'fadeIn',
                                        fxOut: 'fadeOut',
                                        showStepDelay: 100,
                                        center: 'hook',
                                        scrollSpeed: 400,
                                        scrollEasing: 'swing',
                                        scrollDelay: 0,
                                        timer: '00:00',
                                        highlight: false,
                                        keepHighlighted: false,
                                        keepVisible: false,// new 2.2.0
                                        onShowStep: function (ui) {

                                            // fadd fx class to buy button
                                            //  $('#ancStartTour > .btn').addClass('colorfadingbutton');

                                        },
                                        onHideStep: function (ui) {

                                            // remove fx class from buy button
                                            // $('#ancStartTour > .btn').removeClass('colorfadingbutton');

                                        }
                                    },
                                    {
                                        hookTo: '#stepThreeDiv',
                                        content: '#step-three',
                                        position: 'rm',
                                        width: 300,
                                        offsetY: -240,
                                        offsetX: -50,
                                        fxIn: 'fadeIn',
                                        fxOut: 'fadeOut',
                                        showStepDelay: 200,
                                        center: 'hook',
                                        scrollSpeed: 400,
                                        scrollEasing: 'swing',
                                        scrollDelay: 0,
                                        timer: '00:00',
                                        highlight: false,
                                        keepHighlighted: false,
                                        keepVisible: false,// new 2.2.0
                                        onShowStep: function (ui) {

                                            // fadd fx class to buy button
                                            //  $('#ancStartTour > .btn').addClass('colorfadingbutton');

                                        },
                                        onHideStep: function (ui) {

                                            // remove fx class from buy button
                                            // $('#ancStartTour > .btn').removeClass('colorfadingbutton');

                                        }
                                    },
                                    {
                                        hookTo: '#dashboardAvatar',
                                        content: '#step-four',
                                        position: 'rt',
                                        offsetY: 0,
                                        offsetX: 10,
                                        fxIn: 'fadeIn',
                                        fxOut: 'fadeOut',
                                        showStepDelay: 100,
                                        center: 'hook',
                                        scrollSpeed: 400,
                                        scrollEasing: 'swing',
                                        scrollDelay: 0,
                                        timer: '00:00',
                                        highlight: false,
                                        keepHighlighted: false,
                                        keepVisible: false,// new 2.2.0
                                        onShowStep: function (ui) {

                                            //show the helper images(demo so not needed)


                                        },
                                        onHideStep: function (ui) {

                                            //hide the helper images(demo so not needed)


                                        }
                                    },
                                    {
                                        hookTo: '#stepFiveHolder',
                                        content: '#step-five',
                                        position: 'rb',
                                        offsetY: -120,
                                        offsetX: 5,
                                        fxIn: 'fadeIn',
                                        fxOut: 'fadeOut',
                                        showStepDelay: 100,
                                        center: 'hook',
                                        scrollSpeed: 400,
                                        scrollEasing: 'swing',
                                        scrollDelay: 0,
                                        timer: '00:00',
                                        highlight: false,
                                        keepHighlighted: false,
                                        keepVisible: false,// new 2.2.0 
                                        onShowStep: function (ui) {

                                            //$('#hook-four').delay(1000).fadeTo(500, 0.5);

                                            //show the helper images(demo so not needed)


                                        },
                                        onHideStep: function (ui) {



                                        }
                                    },
                                    {
                                        hookTo: '.sideBarOptionsDashboard',
                                        content: '#step-six',
                                        position: 'bm',
                                        offsetY: 7,
                                        offsetX: 62,
                                        fxIn: 'fadeIn',
                                        fxOut: 'fadeOut',
                                        showStepDelay: 100,
                                        center: 'hook',
                                        scrollSpeed: 400,
                                        scrollEasing: 'swing',
                                        scrollDelay: 0,
                                        timer: '00:00',
                                        highlight: false,
                                        keepHighlighted: false,
                                        keepVisible: false,// new 2.2.0
                                        onShowStep: function (ui) {



                                        },
                                        onHideStep: function (ui) {


                                        }
                                    },
                                    {
                                        hookTo: '.sideBarOptionsDashboard',
                                        content: '#step-seven',
                                        width: 300,
                                        position: 'bm',
                                        offsetY: 7,
                                        offsetX: -10,
                                        fxIn: 'fadeIn',
                                        fxOut: 'fadeOut',
                                        showStepDelay: 100,
                                        center: 'hook',
                                        scrollSpeed: 400,
                                        scrollEasing: 'swing',
                                        scrollDelay: 0,
                                        timer: '00:00',
                                        highlight: false,
                                        keepHighlighted: false,
                                        keepVisible: false,// new 2.2.0
                                        onShowStep: function (ui) {



                                        },
                                        onHideStep: function (ui) {


                                        }
                                    },
                                    {
                                        hookTo: '.sideBarOptionsDashboard',
                                        content: '#step-eight',
                                        width: 300,
                                        position: 'bm',
                                        offsetY:7,
                                        offsetX: 85,
                                        fxIn: 'fadeIn',
                                        fxOut: 'fadeOut',
                                        showStepDelay: 100,
                                        center: 'hook',
                                        scrollSpeed: 400,
                                        scrollEasing: 'swing',
                                        scrollDelay: 0,
                                        timer: '00:00',
                                        highlight: false,
                                        keepHighlighted: false,
                                        keepVisible: false,// new 2.2.0
                                        onShowStep: function (ui) {



                                        },
                                        onHideStep: function (ui) {


                                        }
                                    },
                                    //{
                                    //    hookTo: '.sideChannel:first',
                                    //    content: '#step-eight',
                                    //    width: 469,
                                    //    position: 'sc',
                                    //    offsetY: 50,
                                    //    offsetX: 0,
                                    //    fxIn: 'fadeIn',
                                    //    fxOut: 'fadeOut',
                                    //    showStepDelay: 100,
                                    //    center: 'step',
                                    //    scrollSpeed: 400,
                                    //    scrollEasing: 'swing',
                                    //    scrollDelay: 0,
                                    //    timer: '00:00',
                                    //    highlight: false,
                                    //    keepHighlighted: true,
                                    //    keepVisible: false,// new 2.2.0
                                    //    onShowStep: function (ui) { },
                                    //    onHideStep: function (ui) { }
                                    //},
                                    {
                                        hookTo: '#socialHolder',
                                        content: '#step-nine',
                                        width: 300,
                                        position: 'br',
                                        offsetY: 12,
                                        offsetX: 95,
                                        fxIn: 'fadeIn',
                                        fxOut: 'fadeOut',
                                        showStepDelay: 100,
                                        center: 'hooks',
                                        scrollSpeed: 400,
                                        scrollEasing: 'swing',
                                        scrollDelay: 0,
                                        timer: '00:00',
                                        highlight: false,
                                        keepHighlighted: true,
                                        keepVisible: false,// new 2.2.0
                                        onShowStep: function (ui) { },
                                        onHideStep: function (ui) { }
                                    }
                            ],
                            stepDefaults: [
                                    {
                                        width: 350,
                                        height: 200,
                                        position: 'tr',
                                        offsetY: 0,
                                        offsetX: 0,
                                        fxIn: 'fadeIn',
                                        fxOut: 'fadeOut',
                                        showStepDelay: 0,
                                        center: 'step',
                                        scrollSpeed: 400,
                                        scrollEasing: 'swing',
                                        scrollDelay: 0,
                                        timer: '00:00',
                                        highlight: true,
                                        keepHighlighted: false,
                                        keepVisible: false,// new 2.2.0
                                        onShowStep: function (ui) { },
                                        onHideStep: function (ui) { }
                                    }
                            ]
                        }

                ]
            });
            if (isNewUser == 'true') {
                $.powerTour('run', 1);
            }
            // Use this to run the first tour on page load
            //$.powerTour( 'run' , 1 );
            //Walk Thrue end//
            var pinterest = '<div itemscope itemtype="https://schema.org/Movie">' +
                '<meta itemprop="url" content="' + window.location.href + '" />' +
                '<h1 itemprop="name">' + "<%=userName%>" + '</h1>' +
                            '<span itemprop="description">' + '<%=userDescription%>' + '</span>' +
                            '</div>';
            $('head').append(pinterest);

            var $share42 = $('.share42init');

            $share42.attr("data-url", window.location.href)
                    .attr("data-title", "<%=userName%>")
                    .attr("data-image", '<%=userAvatar%>')
                    .attr("data-description", '<%=userDescription%>');

            //$('#share_button').click(function(e){
            //    e.preventDefault();
            //    FB.ui(
            //    {
            //        method: 'feed',
            //        name: 'MaxST TV Network',
            //        link: 'https://qa2.strimm.com/MaxST',
            //        picture: 'https://s3.amazonaws.com//tubestrimmqa/4/avatar/img[1].jpg',
            //        caption: 'Why you should watch this channel',
            //        description: 'TV Network of the future. See all channels',
            //        message: 'This is custom message'
            //    });
            //});

            //if (favChannelCount > 0) {
            //    ShowFavorites();
            //}
            //else {
            //    $(".sideBarOptionsDashboard .addtofavorite").removeClass("addFavoritesActive");
            //    $(".sideBarOptionsDashboard .toprated").addClass("topRatedActive");
            //    ShowTopChannels();
            //}
            //ShowBio();
            if (isOwner != 'True') {
                $("#btnWatchVideoHolderDashboard").hide();
            }
        });

    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <script>
        //window.fbAsyncInit = function () {
        //    FB.init({
        //        appId: '576305899083877', //'576305899083877',
        //        xfbml: true,
        //        status: true,
        //        cookie: true,
        //        version: 'v2.3'
        //    });
        //};

        //(function (d, s, id) {
        //    var js, fjs = d.getElementsByTagName(s)[0];
        //    if (d.getElementById(id)) return;
        //    js = d.createElement(s); js.id = id;
        //    js.src = "HTTPS://connect.facebook.net/en_US/all.js#xfbml=1&status=0&appId=576305899083877&version=v2.3";
        //    fjs.parentNode.insertBefore(js, fjs);
        //}(document, 'script', 'facebook-jssdk'));
    </script>

            <div id="avatarInfo">
                <div id="stepThreeDiv"><a id="ancCropBackground" onclick="ShowCropBackgroundModal()" runat="server" clientidmode="Static"></a></div>

 <div class="gradient"></div>

                <asp:Label runat="server" ID="lblAbout" ClientIDMode="Static"></asp:Label>

                <div id="DashboardBackgroungImg">
                    <a href="#">
                        <asp:Image runat="server" ID="imgDashboard" ClientIDMode="Static" ImageUrl="/images/defaultBG.jpg" /></a>
                </div>

         

                <div id="headMenu">
                    <asp:Label runat="server" ID="lblUserInfo" ClientIDMode="Static"></asp:Label>
                    <asp:Label runat="server" ID="lblCountry" ClientIDMode="Static"></asp:Label>
                  
                    <div class="btnTutorialTourHolderDashboard">
                    <div id="divPlaceHolderStepTwo">
                        <div id="btnWatchVideoHolderDashboard"><a id="ancHowTo" onclick="ShowTutorialPlayer('<%=howToPageTutorialVideoId%>', false)">watch tutorial &#9654;</a></div>
            </div>
                    <div id="btnStartTourHolder" ClientIdMode="Static" runat="server"><a id="ancStartTour">
                        or <span class="videoOrTour videoOrTourDashboard"> take a tour &#9654;</span> </a></div>
                </div>
                </div>

                <div id="dashboardAvatar">

            <asp:Image runat="server" id="imgDashboardAvatar" ClientIDMode="Static" ImageUrl="/images/imgUserAvatar.jpg" />
   <div class="ancCropBackgroundHolderAvatar"></div>
            <a id="ancCrop" onclick="ShowCropModal()" runat="server" ClientIDMode="Static"></a>
       
                </div>

            </div>

    <div class="dashboardMobileMenu">
  <a class="btnDashboardMobileMenuDuo duoTour">Take a tour</a>
        <a id="ashboardMobileAddChannel" class="btnDashboardMobileMenuDuo duoChannel" runat="server" onclick="CreateChannel.RedirectToCreateChannel()" clientidmode="Static">Create Channel</a>
    </div>
    <div class="dashboardMobileMenu">
        <a class="btnDashboardMobileMenu btnDashboardMobileMenuActive btnChannel">Channels</a>
        <a class="btnDashboardMobileMenu btnFavorites" onclick="ShowFavorites()">Favorites</a>
        <a class="btnDashboardMobileMenu btnTop" onclick="ShowTopChannels()">Top</a>
        <a class="btnDashboardMobileMenu btnBio" onclick="GetBio()" >Bio</a>

    </div>
    <div id="mainWrapper">


        <a onclick="ShowFeedback()" class="feedeback rotate"></a>

        <div class="leftWrapperRS">
          <%--  <a class="ancEditBlack" onclick="ToggleDashboardForm()" runat="server" ClientIdMode="static" id="ancToggleUserInfo"></a>--%>

            <div id="mainContent">

                <div class="channelsHolderDashboard">
                    <div id="headH1">
 <a id="a1" runat="server" onclick="CreateChannel.RedirectToCreateChannel()" clientidmode="Static" class="addChannel">Create Channel</a>
                        <h1>channels</h1>
                        <asp:Label runat="server" ID="lblChannelCount" ClientIDMode="Static"></asp:Label>
                         
                        <div id="socialHolder">
<%--<span class="shareSocial"> share</span>--%>
                            <div id="emailBtn" class="emailBtnSocial" onclick="emailCurrentPage();"></div>
                            <div class="share42init" style="float: right;"></div>
                            <script type="text/javascript" src="/Plugins/Share42/share42.js"></script>
                        </div>
                          


                        <%-- <div id="socialHolder">
                     <a href="#" id="share_button" class="shareLink">share link</a>
         
             <a href="https://twitter.com/share" class="twitter-share-button" data-via="StrimmInc" data-size="large" data-text="Strimm - Social TV Network of the 21st Century - ">Tweet</a>
                        <script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^https:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');</script>

                <!-- Place this tag where you want the +1 button to render. -->
    <div class="g-plusone"></div>

    <!-- Place this tag after the last +1 button tag. -->
    <script type="text/javascript">
        (function () {
            var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
            po.src = 'https://apis.google.com/js/platform.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
        })();
    </script>
                </div>--%>
                    </div>

<%--                        <div id="loadingDivDash">
            <div id="loadingDivHolderDash" style="display:block;">
             <img src="..//images/ajax-loader(3).gif" />
            </div>
        </div>--%>
       
                    <div id="channels">

                        <div id="channelsHolder" runat="server" clientidmode="static">
                        </div>

                        <script>
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
                        </script>

                        <div id="stepFiveHolder">
                            <a id="ancCreateChannel" runat="server" onclick="CreateChannel.RedirectToCreateChannel()" clientidmode="Static" class="addChannel"></a>
                        </div>

                    </div>
                </div>
            </div>

        <div id="sideBarChannel">
            <span class="iconDescriptionDashboard">bio</span>
            <div class="sideBarOptionsDashboard">
                <%-- <a class="schedule scheduleactive " title="Schedule" onclick="ShowSchedule()">Schedule</a>--%>

                <a class="toprated" title="Top Channels" onclick="ShowTopChannels()"></a>
                <a class="addtofavorite" title="Favorites" onclick="ShowFavorites()"></a>
                 <a class="addBio addBioActive" title="Bio" onclick="GetBio()" ></a>

                <%--    <a class="rings " title="Rings" onclick="ShowMyChannels()">My Channels</a>--%>
            </div>
            <div class="minHeightMobile">
                <div class="sideContentHolder">
                    <div id="bioWrapper" >
                        <div id="bioEditHolder" runat="server">
                            <a id="bioEdit" ClientIDMode="Static" runat="server" onclick="showBioEdit()">edit</a>
                            <div id="bioActionHolder">
                                <textarea id="txtAreaBio" cols="37" rows="10" maxlength="300" onkeyup="checkBio(event);"></textarea>
<%--                                <asp:TextBox ClientIDMode="Static" runat="server" ID="txtAreaBio" Columns="40" Rows="10" TextMode="MultiLine" MaxLength="300"></asp:TextBox>--%>
                                <span id="spnLimit">300 characters maximum</span>
                                <a id="btnSaveBio" onclick="UpdateUserBio()">save</a>
                                <a id="btnCancelEdit" onclick="closeBioEdit()">cancel</a>
                            </div>                            
                        </div>

                        <div id="bioHolder">
                                <input id="txtAreaBioHidden" type="hidden" />
                            <asp:Label runat="server" ID="lblBio" ClientIDMode="Static"></asp:Label>
        </div>

                        
    </div>
                </div>
            </div>

        </div>

        </div>

        

    </div>

    <div id="dboardHolder" runat="server" class="dashBoardForm" style="display: none;">
        <div id="popUpOverlay">
            <div id="dashboardProfileHolder">

                <ul>
                    <h1 class="popupHeader">Edit your dashboard</h1>
                    <a class="closeEditFoarm" onclick="CloseDashBoardForm()">&times;</a>
                    <%--  <li> <asp:TextBox runat="server" ID="txtTitle" placeholder="title (3-15 characters)"></asp:TextBox></li>--%>
                    <li>
                        <asp:Label runat="server" ID="lblInfoMsg" ClientIDMode="Static"></asp:Label></li>
                    <li>
                        <asp:Image runat="server" ID="imgDashBoardAvatarPrev" ClientIDMode="Static" ImageUrl="/images/comingSoonBG.jpg" />
                        <div class="liUpload">
                            <span>&plus; upload logo or avatar <%--<strong>200px X 200px</strong>--%></span>

                            <asp:FileUpload runat="server" ID="fuAvatar" ClientIDMode="Static" placeholder="logo or avatar (1 mb max., .jpeg or .jpg)" onchange="readURL(this, 'imgDashBoardAvatarPrev','fuAvatar')" />
                        </div>


                    </li>
                    <li>
                        <asp:Image runat="server" ID="imgDashboardBackgroundPrev" ClientIDMode="Static" ImageUrl="/images/comingSoonBG.jpg" />
                        <div class="liUpload">
                            <span>&plus; upload backgroung image <%--<strong>350px X 1200px</strong>--%></span><asp:FileUpload runat="server" ID="fuBackground" ClientIDMode="Static" placeholder="backgroung image (size: xxxx)" onchange="readURL(this, 'imgDashboardBackgroundPrev', 'fuBackground')" />
                        </div>
                    </li>
                    <li>
                        <asp:TextBox TextMode="MultiLine" Rows="9" Columns="45" runat="server" ID="txtBio" ClientIDMode="Static"
                            placeholder="My Philosophy (250 characters max)"></asp:TextBox>
                    </li>
                    <li>
                        <asp:Button runat="server" ID="btnSaveDashBoardInfo" ClientIDMode="Static" Text="save" OnClick="btnSaveDashBoardInfo_Click" />

                    </li>

                </ul>
            </div>
        </div>

    </div>

    <div class="playerbox" style="display: none;">
        <div id="related" class="player"></div>
        <div id="content-container">
        </div>
        <a id="close_x" class="close close_x closePlayerBox" href="#">close</a>
    </div>

    <div id="divCropImgContainer">

            </div>

    <div id="divCropImgBackgroundContainer">
            </div>

    <uc:FeedBack ID="FeedBack1" runat="server" pageName="My Network" />
    <!-- *********** -->
    <!-- STEPS  TOUR -->
    <!-- *********** -->
    <div id="step-one" class="single-step ltr-text">

        <p class="takeTourP">
         <span class="welcomeWalkTour"> Welcome to your <br/> Network Page! </span> 
            I am Sam & will be your tour guide.  We will become best buds! Follow me to discover how to get the most out of your Strimm experience.
        </p>
         <div class="guideSam"></div>
        <footer>
            <a href="#" class="btn btn-default endTour" data-powertour-action="stop">Close</a>
            <a href="#" id="btnStartTour" class="btn btn-primary pull-right next" data-powertour-action="next">Take a tour</a>


        </footer>
        </div>

    <div id="step-two" class="single-step ltr-text">
        <span class="connectorarrow-tm">
            <!-- custom -->
        </span>

        <p class="takeTourP">
       Anytime you want to learn more, just click the Watch Tutorial button for a video tutorial about this page
        </p>
            <div class="guideSam sam04"></div>
        <footer>
            <div class="pull-left">
                <a href="#" class="btn btn-default prev" data-powertour-action="prev">Prev</a>
                <a href="#" class="btn btn-primary next" data-powertour-action="next">Next</a>

            </div>
            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>
        </footer>
    </div>

    <div id="step-three" class="single-step">
        <span class="connectorarrow-tl">
            <!-- custom -->
        </span>

        <p class="takeTourP">
   Want to know how to customize your background image? It's easy! To add, update or customize the background image of your page, just click the camera icon, choose an image, crop it and upload.Voila!
        </p>
              <div class="guideSam sam02"></div>
        <footer>
            <div class="pull-left">
                <a href="#" class="btn btn-default prev" data-powertour-action="prev">Prev</a>
                <a href="#" class="btn btn-primary next" data-powertour-action="next">Next</a>

            </div>
            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>
        </footer>
    </div>

    <div id="step-four" class="single-step ltr-text">
        <span class="connectorarrow-lt">
            <!-- custom -->
        </span>

        <p class="takeTourP">
  Now I don’t know about you, but I love taking selfies!  In order to add, update or customize your avatar, just click the camera icon, choose an image, crop it and upload. Easy as 1-2-3.
        </p>
              <div class="guideSam sam01"></div>
        <footer>
            <div class="pull-left">
                <a href="#" class="btn btn-default prev" data-powertour-action="prev">Prev</a>
                <a href="#" class="btn btn-primary next" data-powertour-action="next">Next</a>

            </div>
            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>
        </footer>
    </div>

    <div id="step-five" class="single-step ltr-text">
        <span class="connectorarrow-lm">
            <!-- custom -->
        </span>

        <p class="takeTourP">
        Here is where you get to use your imagination and create your own one of a kind channel.  To create a new channel just click the big + button. Now lets customize your channel! Pick a channel name, a designated category and then upload an avatar image to make your channel stand out!  It's easy! Keep in mind you can create up to 12 channels of your choice.  Happy Creating!
        </p>
              <div class="guideSam sam02"></div>
        <footer>
            <div class="pull-left">
                <a href="#" class="btn btn-default prev" data-powertour-action="prev">Prev</a>
                <a href="#" class="btn btn-primary next" data-powertour-action="next">Next</a>

            </div>
            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>
        </footer>
    </div>

    <div id="step-six" class="single-step ltr-text">
        <span class="connectorarrow-tr">
            <!-- custom -->
        </span>

        <p class="takeTourP">
     Want to know the easy way to find your favorite channels?  Just click the star tab to display  links directly to your favorites.
        </p>
              <div class="guideSam sam03"></div>
        <footer>
            <div class="pull-left">
                <a href="#" class="btn btn-default prev" data-powertour-action="prev">Prev</a>
                <a href="#" class="btn btn-primary next" data-powertour-action="next">Next</a>

            </div>
            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>
        </footer>
    </div>
 <div id="step-seven" class="single-step ltr-text">
        <span class="connectorarrow-tr">
            <!-- custom -->
        </span>

        <p class="takeTourP">
   Don’t know what to watch?  No problem.  Just click the up arrow tab to view the top rated Strimm channels.  You will always find something cool to watch here.
        </p>
           <div class="guideSam sam03"></div>
        <footer>
            <div class="pull-left">
                <a href="#" class="btn btn-default prev" data-powertour-action="prev">Prev</a>
                <a href="#" class="btn btn-primary next" data-powertour-action="next">Next</a>

            </div>
            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>
        </footer>
    </div>

    <div id="step-eight" class="single-step ltr-text">
        <span class="connectorarrow-tr">
            <!-- custom -->
        </span>

        <p class="takeTourP">
          Tell us all about you! Simply click "Edit" and let your imagination run wild.
        </p>
            <div class="guideSam sam03"></div>
        <footer>
            <div class="pull-left">
                <a href="#" class="btn btn-default prev" data-powertour-action="prev">Prev</a>
                <a href="#" class="btn btn-primary next" data-powertour-action="next">Next</a>

            </div>
            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>
        </footer>
    </div>

    <div id="step-nine" class="single-step ltr-text">
        <span class="connectorarrow-tr">
            <!-- custom -->
        </span>

        <p class="takeTourP">
         Are you a social butterfly like me?  Do you love to share with your friends?  Its so easy!  Share the content of this page using your favorite social media platform, or use them all.  Happy Socializing!
        </p>
              <div class="guideSam sam04"></div>
        <footer>
            <div class="pull-left">
    
            
            </div>
            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>
        </footer>
    </div>
     <script async="async" src="https://www.youtube.com/iframe_api"></script>
    <script src="//www.google.com/jsapi" type="text/javascript"></script>
     <script type="text/javascript">
         google.load("swfobject", "2.1");
    </script>
</asp:Content>
