<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="ChannelPage.aspx.cs" Inherits="StrimmTube.ChannelPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
<meta name="description" content="Watch and enjoy Strimm TV channel" />
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server" ClientIDMode="Static">

    <%: System.Web.Optimization.Styles.Render("~/bundles/channelPage/css") %>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/channelPage/js") %>
    <link href="/flowplayer/html5/5.5.2/skin/functional.css" rel="stylesheet" />
    <script src="/flowplayer/html5/5.5.2/flowplayer.min.js"></script>
    <script src="//cdn.flowplayer.com/releases/native/stable/plugins/hls.min.js"></script>
    <script src="/JS/Froogaloop.js"></script>
    <script src="https://www.youtube.com/iframe_api"></script>
    <script src="/JS/swfobject.js" type="text/javascript"></script>
     <script src="https://api.dmcdn.net/all.js"></script>
<script>
 
</script>
    <style>
        body {
    background-color: #333;
}
    </style>

    <script type="text/javascript">


        /* share42.com | 28.05.2014 | (c) Dimox */
        window.onload = function () { var e = document.getElementsByTagName('div'); for (var k = 0; k < e.length; k++) { if (e[k].className.indexOf('share42init') != -1) { if (e[k].getAttribute('data-url') != -1) var u = e[k].getAttribute('data-url'); if (e[k].getAttribute('data-title') != -1) var t = e[k].getAttribute('data-title'); if (e[k].getAttribute('data-image') != -1) var i = e[k].getAttribute('data-image'); if (e[k].getAttribute('data-description') != -1) var d = e[k].getAttribute('data-description'); if (e[k].getAttribute('data-path') != -1) var f = e[k].getAttribute('data-path'); if (e[k].getAttribute('data-icons-file') != -1) var fn = e[k].getAttribute('data-icons-file'); if (!u) u = location.href; if (!t) t = document.title; if (!fn) fn = 'icons.png'; function desc() { var meta = document.getElementsByTagName('meta'); for (var m = 0; m < meta.length; m++) { if (meta[m].name.toLowerCase() == 'description') { return meta[m].content; } } return ''; } if (!d) d = desc(); u = encodeURIComponent(u); t = encodeURIComponent(t); t = t.replace(/\'/g, '%27'); i = encodeURIComponent(i); d = encodeURIComponent(d); d = d.replace(/\'/g, '%27'); var fbQuery = 'u=' + u; if (i != 'null' && i != '') fbQuery = 's=100&p[url]=' + u + '&p[title]=' + t + '&p[summary]=' + d + '&p[images][0]=' + i; var s = new Array('"#" data-count="fb" onclick="window.open(\'https://www.facebook.com/sharer.php?m2w&' + fbQuery + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=550, height=440, toolbar=0, status=0\');return false" title="Share on Facebook"', '"#" data-count="gplus" onclick="window.open(\'https://plus.google.com/share?url=' + u + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=550, height=440, toolbar=0, status=0\');return false" title="Share on Google+"', '"#" data-count="pin" onclick="window.open(\'https://pinterest.com/pin/create/button/?url=' + u + '&media=' + i + '&description=' + t + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=600, height=300, toolbar=0, status=0\');return false" title="Pin It"', '"#" data-count="twi" onclick="window.open(\'https://twitter.com/intent/tweet?text=' + t + '&url=' + u + '\', \'_blank\', \'scrollbars=0, resizable=1, menubar=0, left=100, top=100, width=550, height=440, toolbar=0, status=0\');return false" title="Share on Twitter"'); var l = ''; for (j = 0; j < s.length; j++) l += '<a rel="nofollow" style="display:inline-block;vertical-align:bottom;width:32px;height:32px;margin:0 6px 6px 0;padding:0;outline:none;background:url(image.php?i=facebook,google-plus,pinterest,twitter&size=32&) -' + 32 * j + 'px 0 no-repeat" href=' + s[j] + ' target="_blank"></a>'; e[k].innerHTML = '<span id="share42">' + l + '</span>'; } }; };

        var channelTubeId = "<%=channelTubeId%>";
        var userId = "<%=userId%>";
        var isMyChannel = "<%=isMyChannel%>";
        var isIE8 = false;
        var facebookUrl = "";
        var userName = "<%=userName%>";
        var channelName = '<%=channelName%>';
        var rating = '<%=userRating%>';
        var isYoutubeActive = JSON.parse("<%=IsYoutubeActive%>".toLowerCase());
        var isVimeoActive = JSON.parse("<%=IsVimeoActive%>".toLowerCase());
        var userIdCheked = 0;
        var hideFooter = "<%=HideOldFooter%>";
        var iLikeThisChannel = JSON.parse("<%=iLikeThisCahnnel%>".toLowerCase());
        var isChannelPasswordProtected = JSON.parse("<%=isChannelPasswordProtected%>".toLowerCase()); 
        if (userId != null) {
            userIdCheked = userId;
        }

        facebookUrl = window.location.href;

        $(".rateit").bind('rated', function (event, value) { alert(value) });

        $(document).ready(function () {
            if (iLikeThisChannel) {
                $("#ancLike").addClass("likeActive");

            }
            if (userId == 0) {

                $(".rateit").rateit('readonly', true);
                $(".rateit").css('cursor', "pointer");
                $(".rateit").click(function () {
                    loginModal('sameLocation');
                })
            }

            if (hideFooter == "True") {
                $('#divFooter').hide();
            }

            //new ResizeSensor(jQuery('#mainWrapper'), function () {
            //    var $leftWrapperRSContainer = $("#mainWrapper");
            //    var leftWrapperRSContainerHeight = $leftWrapperRSContainer.height();
            //    $("#sideBarChannel").height(leftWrapperRSContainerHeight);
            //});

           var pinterest = '<div itemscope itemtype="https://schema.org/Movie">' +
                                '<h1 itemprop="name">' + '<%=channelName%>' + '\n' + '<%=channelDescription%>' + '</h1>' +
                                '<span itemprop="description">' + '<%=channelDescription%>' + '</span>' +
                                '<meta itemprop="url" content="' + window.location.href + '" />' +
                           '</div>';
            $('head').append(pinterest);

            $('.share42init').attr("data-url", window.location.href)
                              .attr("data-title", '<%=channelName%>')
                              .attr("data-image", '<%=channelImgAvatarUrl%>')
                              .attr("data-description", '<%=channelDescription%>');


            var viewCountSetTimeOut = setTimeout(function () {
                var clientTime = getClientTime();
                if (isMyChannel != "True") {
                    var params = '{"userId":' + userId + ',"channelTubeId":' + channelTubeId + ',"viewTime":' + "'" + clientTime + "'" + '}';
                    $.ajax({
                        type: 'POST',
                        url: webMethodAddUserChannelTubeView,
                        dataType: 'json',
                        data: params,
                        contentType: "application/json; charset=utf-8",
                        success: function (response) {
                            //console.log(response.d);
                        }
                    });
                }
            }, 120000);// set for 2 min from the page load, check last count for current user for this channel if it was today already skip the add

            if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                var ieversion = new Number(RegExp.$1) // capture x.x portion and store as a number
                if (ieversion && ieversion >= 8) {
                    isIE8 = true;
                    $("#tweetUpdates").empty().html("<span id=spnMsg style='display:block; color:#555; font-size:13px;'>Your current version of Internet Explorer does not support Facebook communication on our site. Please use a latest version of Internet Explorer or another browser, like Firefox or Chrome</span>");
                }
            }

            if (executeSchedulePolling == 1)   {
                (function poll() {

                    SchedulePolling()
                })();
            }

            if (userId == 0) {
                $(".watchlater").removeAttr("onclick").attr("onclick", "loginModal('sameLocation')");
            }
        });

    </script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%= new StrimmTube.CorsUpload { }.ToString()%>

    <script type="text/javascript">
        google.load("swfobject", "2.1");

        //window.fbAsyncInit = function () {
        //    FB.init({
        //        appId: '576305899083877',
        //        status: true,
        //        cookie: true,
        //        xfbml: true,
        //        version: 'v2.0'
        //    });
        //};

        //(function () {
        //    var e = document.createElement('script'); e.async = true;
        //    e.src = document.location.protocol +
        //    '//connect.facebook.net/en_US/all.js#xfbml=1&status=0&appId=576305899083877"';
        //    $("#fb-root").append(e);
        //   // document.getElementById('fb-root').appendChild(e);
        //}());

        //if (!isIE8) {
        //    (function (d, s, id) {
        //        var js, fjs = d.getElementsByTagName(s)[0];
        //        if (d.getElementById(id)) return;
        //        js = d.createElement(s); js.id = id;
        //        js.src = "HTTPS://connect.facebook.net/en_US/all.js#xfbml=1&status=0&appId=576305899083877&version=v2.0";
        //        fjs.parentNode.insertBefore(js, fjs);
        //    }(document, 'script', 'facebook-jssdk'));
        //}
    </script>

    <div id="mainWrapper" class="block">
        
        <%--<div id="loadingDiv">
            <div id="loadingDivHolder">
                <img src="/images/ajax-loader(3).gif" />
            </div>
        </div>--%>

        <div id="sideBarChannel">
            <span class="iconDescription"> channel schedule</span>
                <div class="sideBarOptions">
                    <a class="mychannels " title="My Channels" onclick="ShowMyChannels()"></a>
                    <a class="toprated" title="Top Channels" onclick="ShowTopChannels()"></a>
                    <a class="favoriteChannels" title="Favorite" onclick="ShowFavorites()"></a>
                   
                    <a class="schedule scheduleactive " title="Schedule" onclick="ShowSchedule()"></a>
                </div>
                <div style="min-height:735px; max-height:2000px;">
                    <div class="sideContentHolder">
                </div>
            </div>         
        </div>
        
        <div class="leftWrapperRS">
            <div id="PlayerHolder">
                <div class="playerbox" id="player">
                    <iframe class="playerbox video-tracking" id="myVideo"  width="853" height="480" data-frameborder="0" data-webkitallowfullscreen="true" data-mozallowfullscreen="true" data-allowfullscreen="true"></iframe>
                    <asp:Image ID="Image1" runat="server" />
                </div>
            </div>
            <img id="playerImage"  class="placeholderImage"/>

            <div id="ChannelPageInfoHolder">
           
                <div id="ratingHolder">
                    <span id="spnAVGRating"><%=channelAVGRating%></span>
                    <span class="yourRating">Your Channel Rating:</span>
                    <input type="range" min="0" max="5" value="<%=userRating%>"  step="0.5" id="backing2" />          
                    <div class="rateit" id="rateit1" data-rateit-backingfld="#backing2" data-rateit-resetable="false"></div>

                    <script type="text/javascript">
                        var webMethodSetChannelRating = "/WebServices/ChannelWebService.asmx/SetChannelRatingByUserIdAndChannelTubeId";
                        var webMethodGetRating = "/WebServices/ChannelWebService.asmx/GetChannelRatingByChannelId"

                        $("#rateit1").bind('rated', function (event, value) {
                            //console.log(userId);
                            if (userId != 0) {
                                var enteredDate = getClientTime();
                                var params = '{"userId":' + userId + ',"channelTubeId":' + channelTubeId + ',"ratingValue":' + value + ',"enteredDate":' + "'" + enteredDate + "'" + '}';
                                $.ajax({
                                    type: 'POST',
                                    url: webMethodSetChannelRating,
                                    dataType: 'json',
                                    data: params,
                                    contentType: "application/json; charset=utf-8",
                                    success: function (response) {
                                        alertify.success("Channel rating was successfully updated.");
                                        if (response.d == true) {
                                            $.ajax({
                                                type: 'POST',
                                                url: webMethodGetRating,
                                                dataType: 'json',
                                                data: '{"channelTubeId":' + channelTubeId + '}',
                                                contentType: "application/json; charset=utf-8",
                                                success: function (response) {
                                                    $("#spnAVGRating").text("").text(response.d);
                                                }
                                            });
                                        }
                                    }
                                });
                            }
                            else {
                                loginModal('sameLocation');
                            }
                        });
                    </script>
               </div>
     
               <div id="channelPageInfoWrapper"> 

                    <div id="innerLeftWrapperChannel">
                        <asp:Image ClientIDMode="Static" itemprop="image" ID="imgChannelAvatar" runat="server"  />
                    </div>

                    <div class="videoTitle videoTitleMain">
                        <h1> </h1>
                        <span class="Rrated"></span>
                    </div>
                
                    <div id="innerRightWrapper">
                        <div id="titleHolder">
                            <asp:Label ID="ancName" runat="server" ClientIDMode="Static"></asp:Label>
                            <a id="ancUserName" runat="server" ClientIDMode="static"></a>
                        </div>
                        <div id="categoryHolder">
                            <asp:Label runat="server" ClientIDMode="Static" ID="lblCategory"></asp:Label>
                        </div>
                        <div id="channelInfo">
                            <asp:Label runat="server" ID="lblSubscribers" ClientIDMode="Static"></asp:Label>
                            <asp:Label runat="server" ID="lblViews" ClientIDMode="Static"></asp:Label>
                            <asp:Label runat="server" ID="lblLikes" ClientIDMode="Static"></asp:Label>
                        </div>

                    </div>
 
                </div>

                <div id="channelMenuHolder"></div>
 
            </div>

            <div id="ChannelPageSocialHolder">
                <div class="options">
                    <a class="addtofavorite" runat="server" id="lnkAddToFave" ClientIDMode="Static"></a>
                    <a class="watchlater" onclick="WatchitLater()" title="Save video to watch it later"></a>
                    <a class="abusereport" onclick="ShowAbuseModal()" title="Report inappropriate content"></a>
                    <a class="like"  runat="server" ClientIdMode="Static" id="ancLike"></a>
                </div>
                <div id="socialHolder">
<%--                    <span class="shareSocial shareSocialchannel"> share</span>--%>
                    <div class="share42init" style="float: right;"></div>
                    <script type="text/javascript" src="/Plugins/Share42/share42.js"></script>
                </div>
            </div>

<%--            <div id="divAddSense">
                <script type="text/javascript">
                    var ad_idzone = "1777962",
                                    ad_width = "728",
                                    ad_height = "90";
                </script>
                <script type="text/javascript" src="https://ads.exoclick.com/ads.js"></script>
                <noscript>
                    <a href="https://main.exoclick.com/img-click.php?idzone=1777962" target="_blank">
                        <img src="https://syndication.exoclick.com/ads-iframe-display.php?idzone=1777962&output=img&type=728x90" width="728" height="90">
                    </a>
                </noscript>
            </div>--%>

            <div id="ChannelPageSocialCommentHolder">
                <div class="tweetUpdates">
                    <div class="fb-comments" data-href="<%=absoluteChannelUrl%>" data-width="100%" data-numposts="5" data-colorscheme="light" data-version="v2.3"></div> 
                </div>
            </div>
        </div>

    </div>
  
    <div id="abuseModal" style="display: none;">
       <h1 class="popupHeader">Report an Issue</h1>
        <a id="close_x" class="close close_x closeAbuseReport" href="#"><span> &times;</span></a>
        <div class="typeAbuse abuse">
            <h3 class="H3abuseReport">Select type of issue</h3>
            <asp:DropDownList runat="server" ID="ddlCategory" class="ddlCategoryAbuse" ClientIDMode="Static">
                <asp:ListItem>Copyright infringement</asp:ListItem>
                <asp:ListItem>Adult content</asp:ListItem>
                <asp:ListItem>Drug content</asp:ListItem>
                <asp:ListItem>Inappropriate content</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="videoAbuse abuse">
            <h3 class="H3abuseReport">Video Title</h3>
            <asp:TextBox ID="txtVideoTitle"  class="ddlCategoryAbuse"  runat="server" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="commentAbuse abuse">
            <h3 class="H3abuseReport">Comments</h3>
            <asp:TextBox ID="txtComments" CssClass="fontFamily" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="6" Columns="84"></asp:TextBox>
        </div>
        <div class="submitAbuse">
            <asp:Label runat="server" ID="lblMsg" ClientIDMode="Static"></asp:Label>
            <a onclick="SendAbuseReport()">report</a>

        </div>
    </div>

<div class="homeBlocksHolder">
    <div id="divFooterNH" class="default regular">
 
 <h1 class="paragraphSloganBlackFoooterBasic plainNH"> Continuous broadcast from your own channel</h1>
           <div class="bottomButtonsHolderFoooterBasic">
               <a class="bottomSignUpFoooterBasic" href="/sign-up">sign up </a>
               <a class="learnMoreHomeFoooterBasic" href="/learn-more">Learn More</a>
               </div>

            <div class="holderNH">

 <div class="column columnHN ">
      <a href="Default.aspx" class="logoFooterLInk">
<div class="logoFooter"></div>
          </a>

                  <div class="column columnFooterSocial">
                    
                    <ul class="footerSocialHolder">
<li class="footerSocial footerSocialFacebook"><a href="https://www.facebook.com/strimmTV" target="_blank"></a></li>
<li class="footerSocial footerSocialTwitter"><a href="https://twitter.com/strimmtv" target="_blank"></a></li>
<li class="footerSocial footerSocialGoogle"><a href="https://plus.google.com/+StrimmTV/posts" target="_blank"></a></li>
<li class="footerSocial footerSocialPinterest"><a href="https://pinterest.com/strimmTV" target="_blank"></a></li>
                    </ul>
                </div>
  </div>

                <div class="column columnHN">
                    <h3>About Us</h3>
                    <ul>
                        <li><a href="/company">Company</a></li>
                        <li><a href="/press">Press </a></li>
                        <li><a href="/contact-us">Contact</a></li>
                    </ul>
                </div>


                <div class="column columnHN">
                    <h3>How It Works</h3>
                    <ul>
                        <li><a href="/learn-more">Become a Producer</a></li>
                        <li><a href="/faq">FAQ</a></li>
                        <li><a href="/guides">How To</a></li>
                    </ul>
        </div>




<%--                <div class="column columnHN columnFooterSocial">
                    <h3>Social</h3>
                    <ul class="footerSocialHolder">
<li class="footerSocial footerSocialFacebook"><a href="https://www.facebook.com/strimmTV" target="_blank"></a></li>
<li class="footerSocial footerSocialTwitter"><a href="https://twitter.com/strimmtv" target="_blank"></a></li>
<li class="footerSocial footerSocialGoogle"><a href="https://plus.google.com/+StrimmTV/posts" target="_blank"></a></li>
<li class="footerSocial footerSocialPinterest"><a href="https://plus.google.com/+StrimmTV/posts" target="_blank"></a></li>
                    </ul>
                </div>--%>

            



                <div class="column columnHN">
                    <h3>Legal</h3>
                    <ul>
                        <li><a href="/copyright">Copyright Policy</a></li>
                        <li><a href="/privacy-policy">Privacy Policy</a></li>
                        <li><a href="/terms-of-use">Terms of Use</a></li>

                    </ul>
    </div>

                <div id="divAllRights">
                    <span>&#169;2015-2016 Strimm, Inc. |  All Rights reserved </span>
                    <%--  <a href="Copyrights.aspx" class="copyrights"> / &nbsp; copyright policy</a>--%>
                </div>
            </div>
        </div>
</div>
    <!-- AddToAny BEGIN -->
    <%--<div class="a2a_kit a2a_kit_size_32 a2a_default_style">
    <a class="a2a_dd" href="https://www.addtoany.com/share_save"></a>
    <a class="a2a_button_facebook"></a>
    <a class="a2a_button_twitter"></a>
    <a class="a2a_button_google_plus"></a>
    </div>
    <script type="text/javascript" src="//static.addtoany.com/menu/page.js"></script>--%>
    <!-- AddToAny END -->

</asp:Content>



