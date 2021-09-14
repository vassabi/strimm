<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Channel.aspx.cs" Inherits="StrimmTube.Channel" %>

<asp:Content ID="Content3" ContentPlaceHolderID="titleHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content=" Strimm is a Social Internet TV Network. It allows watching free video shows online continuously, create public TV streaming and socialize with friends" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        body {
            background-color: #555;
        }
    </style>
     <%: System.Web.Optimization.Styles.Render("~/bundles/channel/css") %>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/channel/js") %>
    <%--<link href="css/Channel.css" rel="stylesheet" />
    <link href="css/MyBoardCSS.css" rel="stylesheet" />
    <link href="Plugins/Scroller/scroller.css" rel="stylesheet" />--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <%--  <script src="Scripts/jquery-1.8.2.js"></script>
    <script src="Plugins/popup/jquery.lightbox_me.js"></script>
    <script src="Plugins/Scroller/nanoscroller.min.js"></script>--%>
    <script type="text/javascript">
        var channelScheduleId = parseInt('<%=channelScheduleId%>');
        var channelId = parseInt('<%=channelId%>');
        var currVideo;
        var isSubscribed = '<%=isSubscribed%>';
        var isMyChannel = '<%=isMyChannel%>';
    </script>
   <%-- <script src="JS/Channel.js"></script>--%>

    <div id="fb-root"></div>
    <script>
        var isIE8 = false;
        $(document).ready(function () {
            if (userId == 0)
            {

            }
            if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                var ieversion = new Number(RegExp.$1) // capture x.x portion and store as a number
                if (ieversion >= 8) {
                    isIE8 = true;
                    $("#tweetUpdates").empty().html("<span id=spnMsg style='display:block; color:#555; font-size:13px;'>Your current version of Internet Explorer does not support Facebook communication on our site. Please use a latest version of Internet Explorer or another browser, like Firefox or Chrome</span>");

                }
            }

            $('#slider3').Thumbelina({
                orientation: 'vertical',         // Use vertical mode (default horizontal).
                $bwdBut: $('#slider3 .top'),     // Selector to top button.
                $fwdBut: $('#slider3 .bottom')   // Selector to bottom button.
            });
        });




        //if (!isIE8) {
        //    window.fbAsyncInit = function () {
        //        FB.init({
        //            appId: '576305899083877', status: true, cookie: true,
        //            xfbml: true
        //        });
        //    };

        //    (function (d, s, id) {
        //        var js, fjs = d.getElementsByTagName(s)[0];
        //        if (d.getElementById(id)) return;
        //        js = d.createElement(s); js.id = id;
        //        js.src = "//connect.facebook.net/en_GB/all.js#xfbml=1";
        //        fjs.parentNode.insertBefore(js, fjs);
        //    }(document, 'script', 'facebook-jssdk'));
        //}

    </script>


    










    <div id="content" class="holder">

        <div id="playerHolder">


        <div class="playerbox" id="player">
                <%--<a id="related" class="player"></a>
                <div id="content-container">
                    <div id="description"></div>
                    <br />
                    <div id="category">
                        <b>Category:</b><br />
                        <span id="category-content"></span>
                    </div>
                    <br />
                    <div id="tags">
                        <b>Tags:</b><br />
                        <span id="tags-content"></span>
                    </div>
                </div>--%>
            </div>

            <div id="sideBar">
            <div  id="slider3">
                <div class="thumbelina-but vert top" style="width:inherit;margin-bottom:20px">&#708;</div>
                <ul id="ulSliderChannels" runat="server"></ul>
                <div class="thumbelina-but vert bottom" style="width:inherit;margin-top:20px">&#709;</div>
            </div>
            <div id="sliderCheckmarksHolder">
                <asp:CheckBox runat="server" ID="chkbxTop" Text="top channels" Checked="true" OnCheckedChanged="chkbxTop_CheckedChanged" AutoPostBack="true" />
                <asp:CheckBox runat="server" ID="chkbxFave" Text="favorite channels" OnCheckedChanged="chkbxFave_CheckedChanged" AutoPostBack="true"/>
                <asp:CheckBox runat="server" ID="chkbxMyChannels" Text="my channels" OnCheckedChanged="chkbxMyChannels_CheckedChanged" AutoPostBack="true"/>
            </div>
        </div>


           
            
            <div id="divAddSense">
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
            </div>

            <div id="divSocial">
                <%-- <div id="divRate" >
                                <a id="ancRateIt" class="disabled">rate it</a>
                            </div>--%>
                <div id="divRing">
                    <a id="ancRing" class="disabled" title="Invite followers to watch this video now">ring it</a>
                </div>
                <div id="divArchive">
                    <a id="ancArchive" class="disabled" title="Save video to watch it later">watch later</a>
                </div>
                <div id="r_rated">
                    <span class="r_rated"></span>
                </div>
                <div id="divAbuse">
                    <a id="ancAbuseReport" class="disabled" title="Report inappropriate content">Abuse report</a>
                </div>

                <div id="divfacebook">

                    <a href="#" id="share_button" class="shareLink">share link</a>
                    <%--<a id="ancFaceBook"></a>--%>
                </div>
                <div id="divTwitter">
                    <a href="https://twitter.com/share" class="twitter-share-button" data-via="StrimmInc" data-size="large" data-text="Strimm - Social TV Network of the 21st Century - ">Tweet</a>
                    <script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^https:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + '://platform.twitter.com/widgets.js'; fjs.parentNode.insertBefore(js, fjs); } }(document, 'script', 'twitter-wjs');</script>
                </div>
            </div>

            <div id="tweetUpdates">
                <div class="fb-comments" data-href="<%=fasebookUrl%>" data-colorscheme="light" data-numposts="10" data-width="728"></div>
            </div>

        </div>
        

        
<div class="infoHolder">
     <div style="height: 30px; width: 728px; margin: auto;">
                <div id="videoInformation">
                    <div id="playlist1" class="playlist"></div>
                    <div id="divVideoTitle">
                        <span class="videoTitle">title:&nbsp;</span>
                        <span id="spnvideoTitle"></span>
                    </div>
                    <div id="views">
                        <span class="views">Strimm views&nbsp;</span>
                        <span id="spnViews"></span>

                    </div>
                    <%--<div id="rating">
                    <span>rating</span>
                    <span id="spnRating">9.8</span>
                </div>--%>
                </div>
            </div>

    <div class="ChannelID">
            <div id="channelimage">
                <asp:Image runat="server" ClientIDMode="Static" ID="imgChannel" ImageUrl="https://s3.amazonaws.com/tubestrimmtest/13/channel/lesmiserables.jpg" />
            </div>
            <div id="channelInfo">
                <asp:Label runat="server" ID="lblChannelName" ClientIDMode="Static"></asp:Label>
             <%--   <a id="showDescription" onclick="ShowHideChannelDecription()" style="display: block; margin-bottom: 10px;">show description ▼--%>
                            
                        <%--<asp:Label runat="server" ID="lblChannelDescription" ClientIDMode="Static"></asp:Label>--%>
               <%-- </a>
                <span class="spnChannelDescription" id="lblChannelDescription"><%=description%></span>--%>
            </div>
        </div>
            <ul id="ulInfo">
                <li><span class="spnCategory">category:</span><asp:Label ID="lblCategory" ClientIDMode="Static" runat="server"></asp:Label></li>
                <li><span class="spnSubscribers">fans:</span><asp:Label ID="lblSubscribers" ClientIDMode="Static" runat="server"></asp:Label></li>
                <li><span class="spnViews">channel views:</span><asp:Label ID="lblViews" ClientIDMode="Static" runat="server"></asp:Label></li>
                <%--<li><span class="spnRating">rating:</span><asp:Label ID="lblRating" ClientIDMode="Static" runat="server"></asp:Label></li>--%>
            </ul>
            <div id="divOwnerInfo">
                <span class="spnBy">by</span>
                <div id="divownerAvatar">
                    <asp:HyperLink runat="server" ID="lnkToOwnerWall">
                        <asp:Label runat="server" ClientIDMode="Static" ID="lblUserBoardName"></asp:Label>

                    </asp:HyperLink>
                </div>

            </div>
            <div id="divSubscribeButton">
                <a onclick="SubscribeChannel()">Add to Favorites</a>
            </div>

        </div>
        <uc:FeedBack runat="server" ID="feedBack" pageName="channel" />

    </div>

    <div id="leftSideStickers">
        <div id="divScheduleHolder">
            <%--    <div id="bulletSchedule" class="bulletSchedule">&#10095; </div>--%>
            <a id="ancSchedule" onclick="ShowHideSchedule()" title="Today's broadcast program">
                <h1>
                    <span>s</span>
                    <span>c</span>
                    <span>h</span>
                    <span>e</span>
                    <span>d</span>
                    <span>u</span>
                    <span>l</span>
                    <span>e</span>
                </h1>
            </a>

            <div class="divSchedule" style="display: none;">
                <asp:Label runat="server" ID="scheduleDate" CssClass="scheduleDate"></asp:Label>
                <div class="nano scheduleScroll" style="height: 350px;">
                    <div class="content">
                        <div id="divSchedule" runat="server" class="todaySchedule" style="display: block;"></div>
                        <div id="divScheduleTomorrow" class="divScheduleTomorrow" runat="server" style="display: none;"></div>
                    </div>

                </div>
                <a id="nextSchedule" onclick="ShowNextSchedule()">next day</a>
            </div>
        </div>
        <%--<div id="divRingsHolder">
            <%--<div id="bulletRings" class="bulletSchedule"> &#10094; </div>--%>
            <a id="ancRings" onclick="ShowHideRings()" title="Invitations to watch cool videos now">
                <h1>
                    <span>r</span>
                    <span>i</span>
                    <span>n</span>
                    <span>g</span>
                    <span>s</span>
                </h1>
            </a>

            <div id="divRings" class="divRings" style="display: none;">
                <asp:Label runat="server" ID="lblRings" CssClass="scheduleDate" Text="rings for today"></asp:Label>
                <div class="nano ringsScroll" style="clear: none; width: 100%; height: 360px;">
                    <div class="content">
                        <!---User Controls Css by classes--->
                        <div id="ringsHolder" runat="server" class="ringsHolder">
                        </div>
                        <!--End of user Controls-->

                    </div>

                </div>
            </div>
        </div>-
    

    <div id="rightSideStickers">
        <div id="divGrouponAdHolder" style="display: none;">
            <a id="ancGroupon">groupon ad</a>
            <div id="divGrouponAdd" class="divGrouponAdd" style="display: none;"></div>
        </div>
    </div>

    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="//www.google.com/jsapi" type="text/javascript"></script>
    <script src="https://www.youtube.com/iframe_api"></script>
    <script type="text/javascript">
        google.load("swfobject", "2.1");
        var webMethodToPoll = "/WebServices/ChannelWebService.asmx/PollChannelSchedule";
        var webMethodGetJson = "/WebServices/ChannelWebService.asmx/GetVideoScheduleJson";
        var webMethodAddVideoView = "/WebServices/ChannelWebService.asmx/AddVideoView";
        var webMethodAddChannelCount = "/WebServices/ChannelWebService.asmx/AddChannelCount";
        var webMethodAddToArchive = "/WebServices/ChannelWebService.asmx/AddToArchive";
        var webMethodPollRings = "/WebServices/ChannelWebService.asmx/PollRings";
        var webMethodIsNewRing = "/WebServices/ChannelWebService.asmx/IsNewRing";
        var player;
        $(document).ready(function () {
            $(".nano.scheduleScroll").nanoScroller({ alwaysVisible: true });
            $('#share_button').click(function (e) {
                e.preventDefault();
                FB.ui(
                {
                    method: 'feed',
                    name: 'Strimm - Social TV Network of the 21st Century',
                    link: ' <%=fasebookUrl%>',
                    picture: '<%=channelImageUrl%>',
                    caption: '<%=channelName%>',
                    description: '<%=channelDescription%>',
                    message: ''
                });
            });
            if (isSubscribed == "True") {
                $("#divSubscribeButton a").text(" ").text("Unsubscribe").removeAttr("onclick").attr("onclick", "Unsubscribe()");
            }
            if (isMyChannel == "True") {
                $("#divSubscribeButton a").text(" ").text("My Channel").removeAttr("onclick");
            }
            // //console.log(isMyChannel);
            (function poll() {
                var now = new Date();
                var utc = new Date(Date.UTC(
                                    now.getFullYear(),
                                    now.getMonth(),
                                    now.getDate(),
                                    now.getHours(),
                                    now.getMinutes()
                                ));
                var date = now.toDateString();
                var time = now.toLocaleTimeString();
                var dt = new Date();
                var tz = dt.getTimezoneOffset();
               // //console.log(now.format('d/m/Y H:i:s'));
                setTimeout(function () {
                    isMatch = false;
                    $.ajax({
                        type: 'POST',
                        url: webMethodToPoll,
                        dataType: 'json',
                        data: '{"channelScheduleId":' + channelScheduleId + ',"clientDate":' + "'" + now.format('d/m/Y H:i:s') + "'" + ',"timeZoneOffset":' + tz + '}',
                        contentType: "application/json; charset=utf-8",
                        success: function (response) {
                            if (response.d == true) {
                                isMatch = true;
                                $.ajax({
                                    type: "POST",
                                    url: webMethodGetJson,
                                    data: '{"channelScheduleId":' + channelScheduleId + '}',
                                    dataType: "json",
                                    contentType: "application/json; charset=utf-8",
                                    success: function (response) {
                                        if (isMyChannel == "False") {
                                            $("#ancRateIt, #ancRing, #ancArchive").removeClass("disabled").addClass("activeLink");

                                        }
                                        else {
                                            $("#ancRateIt, #ancRing").removeClass("disabled").addClass("activeLink");
                                            $("#ancArchive, #ancAbuseReport").hide();
                                        }

                                        $("#ancRing").attr("onclick", "RingToFollowers()");
                                        $("#ancAbuseReport").removeClass("disabled").addClass("activeLink").attr("onclick", "ShowAbuseModal()");
                                        $("#ancArchive").attr("onclick", "AddToArchive()");
                                        $("#ancRateIt").attr("onclick", "RateIt()");
                                        $("#videoInformation").show();
                                        var json = response.d;
                                        var jsonArray = new Array();
                                        jsonArray = json;
                                        var data = JSON.parse(json);
                                        var len = data.length
                                        var playlistArr = new Array();
                                        for (var i = 0; i < data.length; i++) {
                                            playlistArr[i] = data[i].url;
                                        }
                                        startTime = (data[0].start).toFixed(1);
                                        // //console.log(response.d);
                                       // //console.log(data);
                                        var tag = document.createElement('script');

                                        tag.src = "https://www.youtube.com/iframe_api";
                                        var firstScriptTag = document.getElementsByTagName('script')[0];
                                        firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
                                        // //console.log(tag);
                                        // 3. This function creates an <iframe> (and YouTube player)
                                        //    after the API code downloads.

                                        function onYouTubeIframeAPIReady() {

                                            if ($.browser.device = (/android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase()))) {
                                                player = new YT.Player('player', {
                                                    height: '546',
                                                    width: '728',

                                                    //videoId: 'M7lc1UVf-VE',
                                                    playerVars: {
                                                        //controls: 0,
                                                        showinfo: 0,
                                                        autoplay: 0
                                                      //  html5: 1

                                                    },
                                                    events: {
                                                        'onReady': onPlayerReady,
                                                        'onStateChange': onPlayerStateChange
                                                    }
                                                });
                                            }

                                            else {
                                                player = new YT.Player('player', {
                                                    height: '546',
                                                    width: '728',

                                                    //videoId: 'M7lc1UVf-VE',
                                                    playerVars: {
                                                        //controls: 0,
                                                        showinfo: 0,
                                                        autoplay: 1
                                                       // html5: 1

                                                    },
                                                    events: {
                                                        'onReady': onPlayerReady,
                                                        'onStateChange': onPlayerStateChange
                                                    }
                                                });
                                            }

                                           $('iframe').each(function () {
                                                var src = $(this).attr('src');
                                                $(this).attr('src', src + '&wmode=transparent');
                                            });
                                        }
                                        function onYouTubeIframeAPIReadyIE() {
                                            if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                                              //  //console.log("IE");
                                                $("iframe.playerbox").attr("allowfullscreen");
                                                player = new YT.Player('player', {
                                                    height: '546',
                                                    width: '728',

                                                    //videoId: 'M7lc1UVf-VE',
                                                    playerVars: {
                                                        //controls: 0,
                                                        showinfo: 0,
                                                        autoplay: 1
                                                      //  html5: 0




                                                    },
                                                    events: {
                                                        'onReady': onPlayerReady,
                                                        'onStateChange': onPlayerStateChange
                                                    }
                                                });
                                            }
                                             $('iframe').each(function () {
                                                var src = $(this).attr('src');
                                                $(this).attr('src', src + '&wmode=transparent');
                                            });
                                        }
                                        if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                                            onYouTubeIframeAPIReadyIE();
                                        }
                                        else {
                                            onYouTubeIframeAPIReady();
                                        }

                                        // 4. The API will call this function when the video player is ready.
                                        function onPlayerReady(event) {

                                            // //console.log("onPlayerReady");

                                            player.cuePlaylist(playlistArr, 0, startTime, "large");
                                            player.setLoop(false);
                                            event.target.playVideo();
                                            //event.target.playVideoAt(0);
                                            $("#spnvideoTitle").text(data[0].title)
                                            // //console.log(player);



                                        }

                                        // 5. The API calls this function when the player's state changes.
                                        //    The function indicates that when playing a video (state=1),
                                        //    the player should play for six seconds and then stop.
                                        var done = false;
                                        function onPlayerStateChange(event) {

                                            //if (event.data == YT.PlayerState.PLAYING && !done) {
                                            //    // setTimeout(stopVideo, 6000);

                                            //    done = true;
                                            //    ////console.log(done);
                                            //}

                                            currVideo = data[event.target.getPlaylistIndex()].videoId;
                                            // //console.log(currVideo);
                                            $("#spnvideoTitle").text("").text(data[event.target.getPlaylistIndex()].title);
                                            $("#spnViews").text(" ").text(data[event.target.getPlaylistIndex()].views);
                                            //  //console.log(event.data + " I am event data");
                                            if (event.data == 0) {
                                                $("#spnvideoTitle").text(data[event.target.getPlaylistIndex()].title);
                                                var videoId = data[event.target.getPlaylistIndex()].videoId;
                                                if (event.target.getPlaylistIndex() != len - 1) {
                                                    currVideo = data[event.target.getPlaylistIndex() + 1].videoId;
                                                    var r_rated = data[event.target.getPlaylistIndex() + 1].r_rated;
                                                }

                                                if (r_rated == true) {
                                                    $(".r_rated").text("r-rated");
                                                }
                                                else {
                                                    $(".r_rated").text("");
                                                }


                                                ////console.log(len);
                                                ////console.log(currVideo);
                                                $("#spnViews").text(" ").text(data[event.target.getPlaylistIndex()].views);
                                                $.ajax({
                                                    type: "POST",
                                                    url: webMethodAddVideoView,
                                                    data: '{"videoId":' + videoId + '}',
                                                    dataType: "json",
                                                    contentType: "application/json; charset=utf-8",
                                                    success: function (response) {
                                                        //  //console.log("+1");
                                                    },
                                                    complete: function () {
                                                      //  //console.log(event.data);
                                                        if ((event.target.getPlaylistIndex() == len - 1) && (event.data == 0)) {

                                                            $.ajax({
                                                                type: "POST",
                                                                url: webMethodAddChannelCount,
                                                                data: '{"channelId":' + channelId + '}',
                                                                dataType: "json",
                                                                contentType: "application/json; charset=utf-8",
                                                                success: function (response) {
                                                                    // //console.log("+1");
                                                                },
                                                                complete: function () {
                                                                    if (event.data == 0) {
                                                                        document.location.reload(true);
                                                                    }
                                                                }

                                                            });



                                                        }

                                                    }
                                                });

                                            }



                                            // //console.log(event.target.k.playlistIndex);

                                        }

                                        function stopVideo() {
                                            player.stopVideo();
                                        }
                                        function playMe() {
                                            player.playVideo();
                                        }
                                        //flowplayer("related", "flowplayer.youtube/flowplayer.swf", {
                                        //    playlist: data,
                                        //    onLoad: function () {
                                        //        $("#spnvideoTitle").text(" ").text(data[0].title);
                                        //        $("#spnViews").text(" ").text(data[0].views);
                                        //        var r_rated = data[0].r_rated;
                                        //        if (r_rated == true) {
                                        //            $(".r_rated").text("r-rated");
                                        //        }
                                        //        else {
                                        //            $(".r_rated").text("");
                                        //        }
                                        //        currVideo = (data[0].videoId);
                                        //        //console.log(currVideo);
                                        //    },
                                        //    plugins: {
                                        //        rtmp: {
                                        //            url: "flowplayer/flowplayer.rtmp/flowplayer.rtmp-3.2.12.swf",
                                        //            // netConnectionUrl defines where the streams are found
                                        //            netConnectionUrl: 'rtmp://sq4vda7ojciyf.cloudfront.net/cfx/st'
                                        //        },
                                        //        controls: {
                                        //            url: "flowplayer.youtube/flowplayer.controls.swf",
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
                                        //            width: 300,
                                        //            playlist: false
                                        //        },
                                        //        youtube: {
                                        //            url: "flowplayer.youtube/flowplayer.youtube-3.2.11.swf",

                                        //            // enableGdata: true,
                                        //            loadOnStart: true,
                                        //            onVideoRemoved: function () {
                                        //                //console.log("Video Removed");
                                        //            },
                                        //            onVideoError: function () {
                                        //                //console.log("Incorrect Video ID");
                                        //            },
                                        //            onEmbedError: function () {
                                        //                //console.log("Embed Not Allowed");
                                        //            }

                                        //        },
                                        //        onApiData: function (data) {
                                        //            // use the function defined above to show the related clips

                                        //            $("#spnvideoTitle").text(data.title)
                                        //            //console.log(data.title);
                                        //            //// use the function defined above to show the related clips
                                        //            // showInfo(data, "#playlist1");
                                        //            ////load the related list for the first clip only
                                        //            //// if (shownRelatedList) return;
                                        //            // showRelatedList(data, "#playlist1");
                                        //            // shownRelatedList = true;
                                        //        },
                                        //        displayErrors: true,

                                        //    },


                                        //    clip: {
                                        //        onBeforePause: function () {
                                        //            return false;
                                        //        },
                                        //        autoPlay: true,

                                        //        // start: 50
                                        //        //url: "ap:qzU9OrZlKb8",
                                        //        // provider:"youtube",
                                        //        onLastSecond: function (clip) {

                                        //            $("#spnvideoTitle").text(data[clip.index + 1].title);
                                        //            var videoId = data[clip.index].videoId;
                                        //            currVideo = data[clip.index + 1].videoId;
                                        //            var r_rated = data[clip.index + 1].r_rated;
                                        //            if (r_rated == true) {
                                        //                $(".r_rated").text("r-rated");
                                        //            }
                                        //            else {
                                        //                $(".r_rated").text("");
                                        //            }

                                        //            //console.log(len);
                                        //            //console.log(currVideo);
                                        //            $("#spnViews").text(" ").text(data[clip.index].views);
                                        //            $.ajax({
                                        //                type: "POST",
                                        //                url: webMethodAddVideoView,
                                        //                data: '{"videoId":' + videoId + '}',
                                        //                dataType: "json",
                                        //                contentType: "application/json; charset=utf-8",
                                        //                success: function (response) {
                                        //                    //console.log("+1");
                                        //                }
                                        //            });

                                        //        },
                                        //        onFinish: function (clip) {
                                        //            if (clip.index == len - 1) {
                                        //                $.ajax({
                                        //                    type: "POST",
                                        //                    url: webMethodAddChannelCount,
                                        //                    data: '{"channelId":' + channelId + '}',
                                        //                    dataType: "json",
                                        //                    contentType: "application/json; charset=utf-8",
                                        //                    success: function (response) {
                                        //                        //console.log("+1");
                                        //                    },
                                        //                    complete: function () {
                                        //                        document.location.reload(true);
                                        //                    }

                                        //                });

                                        //            }
                                        //            //console.log(clip.index);
                                        //        },
                                        //    }
                                        //});
                                    }
                                });
                            }
                        },
                        complete: function () {
                            if (isMatch == false) {
                                // //console.log("i am match "+isMatch)
                                poll();
                            }
                            if (isMatch == true) {
                                ////console.log("i am match " + isMatch)
                                $(this).stop();
                                return;
                            }
                        }
                    });
                }, 1000)

            })();



            (function pollRings() {

                // //console.log(ringId);
                setTimeout(function () {
                    $.ajax({
                        type: 'POST',
                        url: webMethodPollRings,
                        dataType: 'json',
                        contentType: "application/json; charset=utf-8",
                        success: function (response) {
                            if (response.d != "0") {
                                var firstRingBox = $(".ringsHolder div").first();
                                var ringIdOld = firstRingBox.attr('id');
                                $(".ringsHolder").html("").html(response.d);
                                var ringNewBox = $(".ringsHolder div").first();
                                var ringNew = ringNewBox.attr("id");
                                if (ringIdOld == ringNew) {
                                    // //console.log("no new");
                                }
                                else {
                                    // //console.log("new");

                                    blink("#divRingsHolder");

                                }

                                ScrollerUp();


                            }
                            pollRings();

                        }
                    });
                }, 30000)

            })();
        });

    </script>
    <style type="text/css">
        #slider3 {
            position: relative;
            width: 100%;
            background-color: #fff;
            margin-left: 10px;
            border-left: 1px solid #ddd;
        }
    </style>

    <div id="abuseModal" style="display: none">
      
        <a id="close_x" class="close close_x" href="#"></a>
        <div class="typeAbuse abuse">
            <h3>Select type of abuse</h3>
            <asp:DropDownList runat="server" ID="ddlCategory"  ClientIDMode="Static">
                <asp:ListItem>Copyright infringement</asp:ListItem>
                <asp:ListItem> Adult content</asp:ListItem>
                <asp:ListItem>Drug content</asp:ListItem>
                <asp:ListItem>Inappropriate content</asp:ListItem>
                <asp:ListItem>Other.</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="videoAbuse abuse">
            <h3>Video Title</h3>
            <asp:TextBox ID="txtVideoTitle" runat="server" ClientIDMode="Static"></asp:TextBox>
        </div>
        <div class="commentAbuse abuse">
            <h3>Comments</h3>
            <asp:TextBox ID="txtComments" runat="server" ClientIDMode="Static" TextMode="MultiLine" Rows="10" Columns="35"></asp:TextBox>
        </div>
        <div class="submitAbuse">
            <asp:Label runat="server" ID="lblMsg" ClientIDMode="Static"></asp:Label>
            <a onclick="SendReport()">report</a>

        </div>
    </div>
      

          

</asp:Content>
