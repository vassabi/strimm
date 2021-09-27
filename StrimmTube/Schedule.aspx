<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="StrimmTube.Schedule" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Production Studio | Strimm
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Strimm TV - production studio" />
    <meta name="google-signin-callback" content="handleAuthResult" />
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="/css/dragula.css" rel="stylesheet" />
    <link href="/reactplayer/css/main.317ac729.chunk.css" rel="stylesheet">
    <link href="/css/dropdown.css" rel="stylesheet" />
    <script src="/JS/dragula.js"></script>
    <%= new StrimmTube.CorsUpload { }.ToString()%>
    <script src="https://api.dmcdn.net/all.js"></script>
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <%: System.Web.Optimization.Styles.Render("~/bundles/schedulePage/css") %>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/schedulePage/js") %>
    <script src="/Flowplayer7/flowplayer.js"></script>
    <script src="//cdn.flowplayer.com/releases/native/stable/plugins/hls.min.js"></script>

    <script src="/JS/dropdown.js"></script>
    <script src="/Flowplayer7/cc-button-7.2.5.js"></script>
    <script src="/Flowplayer7/settingsmenu-7.2.5.js"></script>
    <script src="/Flowplayer7/embed.min.js"></script>
    <script src="/Flowplayer7/vimeo-7.0.0.js"></script>
    <script src="/Flowplayer7/youtube-7.0.0.js"></script>
    <script src="/Flowplayer7/dailymotion-7.0.0.js"></script>
    <script src="https://apis.google.com/js/platform.js" async defer></script>
                   
    <script>
        DM.init({
            apiKey: dmApiKey,
            status: true, // check login status
            cookie: true // enable cookies to allow the server to access the session
        });

    </script>

    <script src="/JS/Dmotion.js"></script>

    <%--<link href="/css/Schedule.css" rel="stylesheet" />
    <link href="/Plugins/Scroller/scroller.css" rel="stylesheet" />--%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <%--  <script src="/Bootstrap/bootstrap-datepicker.js"></script>
    <link href="/Bootstrap/bootstrap-datepicker.css" rel="stylesheet" />--%>

   <%-- <script src="/jquery/jquery.timepicker.js"></script>
    <link href="/jquery/jquery.timepicker.css" rel="stylesheet" />--%>

    <%--<script src="/JS/Schedule.js" type="text/javascript"></script>
    <script src="/JS/swfobject.js" type="text/javascript"></script>--%>
   
  
  <script src="/plugins/date.format.js"></script>

      <link href="/jquery/jquery.timepicker.css" rel="stylesheet" />

    <%--    <script src="/Plugins/DHTMLXCalendar/sources/htmlxCalendar/codebase/dhtmlxcalendar.js" type="text/javascript"></script>
    <script src="/Plugins/DHTMLXCalendar/sources/dhtmlxcommon/codebase/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="/Plugins/DHTMLXCalendar/sources/dhtmlxpopup.js" type="text/javascript"></script>
    <script src="/Plugins/DHTMLXCalendar/sources/dhtmlxcommonpopup.js" type="text/javascript"></script>--%>

   
    
    <script type="text/javascript">
        var userId = '<%=userId%>';
        var channelId = '<%=channelId%>';
        var channelName = '<%=channelName%>';
        var isEditPage = '<%=isEditPage%>';
        var editdateVideoSchedule = "<%=dateVideoSchedule%>";
        var startIndexByCategory = 0;
        var isautopilot0N = '<%=isAutoPilotOn%>';
        var userName = "<%=userName%>";
        var isNewChannel = '<%=isNewChannel%>';
        var isProviderEnable = '<%=isProviderEnable%>';
        var isYoutubeActive = JSON.parse("<%=IsYoutubeActive%>".toLowerCase());
        var isVimeoActive = JSON.parse("<%=IsVimeoActive%>".toLowerCase());
        var isDmotionActive = JSON.parse("<%=IsDmotionActive%>".toLowerCase());
        var availableProvidersArr = JSON.parse('<%=availableProvidersArrData%>');
        var channelDescription = `<%=channelDescription%>`;
        
      

        function ShowStartTimeCalendarForLiveVideo(btn) {


            var final="";
            final+="<div class='liveStartTimePicker'>";
            final+="<input type='text' placeHolder='add start date' class='liveStartDate'/>";
            final+="<input type='text' placeHolder='add start time' class='liveStartTime'/>";
            final+="</div>";
            final+="<span>-</span>";
            final+="<div class='liveEndTimePicker'>";
            final+=" <input type='text' placeHolder='add end date' class='liveEndDate'/>";
            final += "<input type='text' placeHolder='add end time' class='liveEndStartTime'/>";
            final += "</div>";
            $("#dateTimePickerHolder").html("").html(final);
            var btnId = btn.id;
            var idArr = btnId.split("_");
            var providerVideoId = "";

            if (idArr.length == 2) {
                providerVideoId = idArr[1];
            }
            else if (idArr.length > 2) {
                $.each(idArr, function (i, p) {
                    if (i > 0) {
                        providerVideoId += p;
                    }

                    if (i > 0 && i < idArr.length - 1) {
                        providerVideoId += "_";
                    }
                });
            }
            var categoryId = 0;
            if (activeTab == 1) {
                categoryId = $("#ddlByKeywordsCategory option:selected").val();
            }
            else if (activeTab == 2) {
                categoryId = $("#ddlByUrlCategory option:selected").val();
                var isuloadsselected = $('.importYourVideosradio').is(':checked');
                if (isuloadsselected == true) {
                    categoryId = $("#ddlYoutubeUserUploadsCategory option:selected").val();
                }
            }


            if (categoryId == 0 || categoryId == undefined) {
                alertify.alert("Please select a category.");
            }
            else {
                var today = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate()).getTime();
                $('.startDateTimePickerHolderLive').lightbox_me({
                    centered: true,
                    onLoad: function () {
                        $(".startDateTimePickerHolderLive .spnError").text("");
                        $('.liveStartTime, .liveEndStartTime').timepicker('setTime', null);
                        $(".liveStartDate, .liveEndDate").datepicker("setDate", null);
                        $(".liveStartDate").datepicker(
                            {
                                dateFormat: 'MM dd yy',
                                yearRange: '-20:+0',
                                changeYear: true,
                                changeMonth: true,
                                minDate: 0,
                                onSelect: function (dateText) {

                                    var selected = new Date(dateText).getTime();

                                        if (today == selected)
                                        {
                                          //  console.log("eval")
                                            $('.liveStartTime').timepicker('option', "minTime", getCurrentTime(new Date()));
                                        }
                                        else if(selected>today)
                                        {
                                            //console.log("after day")
                                            $('.liveStartTime').timepicker('option', "minTime", 0);
                                        }
                                }

                            });
                        $(".liveEndDate").datepicker(
                            {
                                dateFormat: 'MM dd yy',
                                yearRange: '-20:+0',
                                changeYear: true,
                                changeMonth: true,
                                minDate: 0,
                                onSelect: function (dateText) {

                                    var selected = new Date(dateText).getTime();

                                    if (today == selected) {
                                      //  console.log("eval")
                                        $('.liveEndStartTime').timepicker('option', "minTime", getCurrentTime(new Date()));
                                    }
                                    else if (selected > today) {

                                        $('.liveEndStartTime').timepicker('option', "minTime", 0);
                                    }
                                }

                            });
                      
                        var videoIsLive = $("#addBoxContent_" + providerVideoId).attr("data-isLiveVide")


                        if (videoIsLive=="true")
                        {
                            $('.liveStartTime').timepicker('setTime', getCurrentTime(new Date()));
                            $(".liveStartDate").datepicker("setDate", new Date());
                        }
                        if (today)
                        {
                            $(".liveStartTime, .liveEndStartTime").timepicker({

                                'step': 10,
                                'minTime': getCurrentTime(new Date()),
                                'setTime': getCurrentTime(new Date()),
                                'forceRoundTime': true,
                                'typeaheadHighlight': true
                            });
                        }
                        else
                        {
                            $(".liveStartTime, .liveEndStartTime").timepicker({

                                'step': 10,
                                // 'minTime': getCurrentTime(new Date()),
                                'forceRoundTime': true,
                                'typeaheadHighlight': true
                            });
                        }

                        $("#saveLiveVideo").removeAttr("onclick").attr("onclick", "SaveLiveVideo('"+providerVideoId+"')")

                    },
                    overlayCSS: {
                        background: 'black',
                        opacity: .8
                    },
                    onClose: function () {
                        $('#customScheduleCreatePopup').hide();
                        RemoveOverlay();
                    }
                });
            }
           // alert("This video is LIVE and is not supported currently. Please select another one");

        }

        //console.log(isYoutubeActive, isVimeoActive, isYoutubeActive)
        //Appsettings for AllowPRoviderSelection

        //FB.api(
        //    "/me/objects/object",
        //    "POST",
        //    {
        //        "object": "{\"fb:app_id\":\"302184056577324\",\"og:type\":\"object\",\"og:url\":\"Put your own URL to the object here\",\"og:title\":\"Sample Object\",\"og:image\":\"https:\\\/\\\/s-static.ak.fbcdn.net\\\/images\\\/devsite\\\/attachment_blank.png\"}"
        //    },
        //    function (response) {
        //        if (response && !response.error) {
        //            /* handle the result */
        //        }
        //    }
        //);
    </script>

    <script type="text/javascript">
        (function () {
            var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
            po.src = 'https://apis.google.com/js/client.js?onload=onLoadCallback';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
        })();
    </script>

    <div runat="server" id="divBackendmenuHolder" class="backendmenuHolder">
    </div>

    <div id="contentShcedule" class="contentShcedule">
        <div class="pageNavHolderMolile">
            <div class="mainNavHolderMobile">
                <a class="spnADdVideoPopup" onclick="ShowAddVideoPopup()"><span>+</span> Get New Videos</a>
                <a id="customScheduleM" class="btncustomSchedule" onclick="displayCustomSchedulePopup()" title="Create a single or multiple schedule for the selected day by picking custom start time and individual videos">custom schedule</a>
                <a id="btnMainNavMobile" class="bntBasicInstantSchedule" onclick="CreateInstantSchedule()">instant schedule</a>
            </div>
            <div class="modeHolderMobile">
                <div id="newScheduleActionsM">
                    <div class="advancedModeHolder">
                        <div id="toggleAdvancedModeM" class="inputAdvancedWrapperOFF" onclick="setWorkingMode()">
                        </div>
                        <span class="spnAdvancedMode">Advanced Mode</span>
                    </div>
                    <div id="divAutopilotM" class="autopilotBasic">
                        <span class="spnAutopilotBasic">Autopilot</span>
                        <div class="infoI autopilotInfo infoBasic">
                        </div>
                        <div class="checkboxHolder">
                            <div id="btnAutopilotM" class="checkboxHolderON" ></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        

        <div class="scheduleHolder">
        <div class="leftSchedule" id="leftSchedule">
            <div class="divGetVideosBtnHolder">

                <div id="divAddVideosMenuHolder" class="divAddVideosMenuHolderAdv">
                    <span id="spnADdVideoPopup" class="spnADdVideoPopup" onclick="ShowAddVideoPopup()"><span>+</span> Get New Videos</span>
                </div>
                <%--<div id="divGetHlsLinkMenuHolder" class="divAddVideosMenuHolderAdv">
                    <span id="spnGetHLSLinkPopup" class="spnADdVideoPopup" onclick="displayHLSLinkPopup()">Get HLS Link</span>
                </div>--%>
                <div id="drafanddropInfo" style="margin-top:35px; float:right;">
                    <a onclick="showDragPopup()">
                        <span>How Drag & Drop Works</span>
                        <div class="infoI autopilotInfo infoBasic">
                        </div>
                    </a>
                     
                </div>
            </div>

            <%--            <div id="divAddVideosMenuHolder">
                        <span onclick="ShowAddVideoPopup()">Get New Videos</span>
                   </div>--%>


            <div class="mobileNavSchedule">
                <a class="mobileNavScheduleView" onclick="ToggleVideosMobile()">Videos</a>

                <a class="mobileNavScheduleView scheduleView" onclick="ToggleScheduleMobile()">Schedules<span class="mobileScheduleCount">76</span></a>
            </div>

            <div class="mobileScheduleSortingHolder">
                <div class="mobileScheduleSorting sortCategory">category</div>
                <div class="mobileScheduleSorting sortClear">clear</div>
                <div class="mobileScheduleSorting sortSearch">search</div>
                <div class="mobileScheduleSorting sortSort">sort</div>
            </div>


            <div class="leftSortActionHolder">



                <div class="leftSortingHolder styled-selectOpt">
                    <div class="leftCategory">
                        <asp:DropDownList runat="server" ID="ddlCategory" CssClass="ddlCategory" AutoPostBack="false" onChange="GetVideoByGategory()" ClientIDMode="Static">
                        </asp:DropDownList>
                    </div>
                </div>

                <%--   <div class="actionMiddleHolder">--%>

                <div class="leftClearHolder"><a onclick="ToggleClearVideosMenu()" class="removeAll">clear all</a></div>
                <div class="removeOptions">
                    <ul>
                        <li><a id="aRemoveAllVideos" onclick="ClearAllVideosFromChannel()">remove all videos</a></li>
                        <li><a id="aRemoveRestrictedVideos" onclick="RemoveRestrictedVideosFromChannel()">remove restricted videos</a></li>
                    </ul>
                </div>


                <%--</div>--%>


                <div id="divSearchVideobyKeywordForChannelHolder">
                    <input type="text" id="txtSearchVideoByKeywordForChannel" onkeyup="channelSearchInputKeyUp()" placeholder="Search for videos on this page" />
                    <a id="btnClearSerachedVideosForChannel" onclick="ClearSearchedVideosForChannel(true)">x</a>
                    <a id="btnSearchVideoByKeywordForChannel" onclick="LoadMoreVideos(true)"></a>
                    <!--
                    <input id="txtSchVideoSearchKeywords" class="schSearch" type="text" placeholder="Enter keyword or name" />
                    <a id="search" class="schAncSearch" onclick="LoadMoreVideos(true)">search</a>
                -->

                </div>
                <div class="closeSearchHolder" onclick="hideSearchControls()"></div>

                <div class="iconSearch" onclick="showSearchControls()"></div>



                <%-- <div class="leftAddRemoveActionHolder">--%>



                <div class="styled-selectSort sortAbsolute">

                    <select id="ddlChannelVideos" <%--class="styled-selectSort"--%> onchange="SortVideosForChannel(this)">
                        <option value="0">sort results</option>
                        <option value="7">get all videos</option>
                        <option value="1">longer to shorter</option>
                        <option value="2">shorter to longer</option>
                        <option value="3">most viewed</option>
                        <option value="4">recently added</option>
                         <option value="5">live events</option>
                        <option value="6">private videos</option>
                        
                    </select>

                </div>
                <%--  </div>--%>
            </div>



            <div runat="server" id="videoBoxHolder" class="videoBoxHolder channelsHolder ">
            </div>
            <div class="ancLoadMoreHolder">
                <a id="ancLoadMore" class="classancLoadMore" onclick="LoadMoreVideos()">Load More</a>
            </div>
        </div>
    </div>

        <div class="rightnewSchedulePopup">
        <div id="newSchedulePopup">

            <!--Basic Mode Instant Schedule Button -->
            <div id="basicInstantBntHolder" class="basicInstantBntHolder">
                <a id="customSchedule" class="btncustomSchedule" onclick="displayCustomSchedulePopup()" title="Create a single or multiple schedule for the selected day by picking custom start time and individual videos">custom schedule</a>
                <a id="btnInstantSchedule" class="bntBasicInstantSchedule" onclick="CreateInstantSchedule()" title="Instantly create a full day schedule for the selected day, from the random videos in your channel">instant schedule</a>
            </div>

            <!-- Advanced Schedule Options -->
            <div id="newScheduleActions">
                <div class="advancedModeHolder">
                    <div id="toggleAdvancedMode" class="inputAdvancedWrapperOFF" onclick="setWorkingMode()">
                    </div>
                    <span class="spnAdvancedMode">Advanced Mode</span>
                </div>
                <div id="divAutopilot" class="autopilotBasic">
                    <span class="spnAutopilotBasic">Autopilot</span>
                    <div class="infoI autopilotInfo infoBasic" title="If Autopilot is ON, then at 11:00am (Central Time) daily, 
the system will automatically create an Instant Schedule 
for the next day. Remember to constantly add fresh 
videos to your channel list." onclick="ShowSnippetPopup(this)">
                    </div>
                    <div class="checkboxHolder">
                        <div id="btnAutopilot" class="checkboxHolderON" ></div>
                    </div>
                </div>
            </div>
        </div>

        <div class="right">
            <div id="calendarPicker"></div>

            <div id="divPickedDate">
                <span id="spnPickedDate"></span>
            </div>

            <div runat="server" id="scheduleThumbHolder" class="scheduleThumbHolder" style="min-height: 735px; /*max-height: 2000px; */">
                <span id="spnStartTime"></span>
            </div>
        </div>
    </div>

     </div>

    <div class="playerbox" style="display: none;">
        <div id="related" class="player"></div>
        <div id="content-container">
        </div>
        <a id="close_x" class="close close_x closePlayerBox" href="#">close</a>
    </div>
    <div class="playerboxVimeo " id="vimeoBox" style="display: none;">
        <div class="playerVimeo">
        </div>
        <a id="close_x" class="close close_x closePlayerBox" href="#">close</a>
    </div>
    <div class="playerBoxDMotion " id="dMotionBox" style="display: none;">
        <div class="playerDmotion" id="playerDmotion">
        </div>
        <a id="close_x" class="close close_x closePlayerBox" href="#"></a>
    </div>
    <div class="playerBoxFlowplayer" id="flowPlayerBox">
        <div class="playerFlowpplayer" id="playerFP">
        </div>
        <a id="close_x" class="close close_x closePlayerBox" href="#"></a>
    </div>
    <div class="playerBoxReactplayer" id="reactPlayerBox" style="display: none;">
        <div id="STRIMM_PLAYER_ROOT" class="fixed-800">
        </div>
        <a id="close_x" class="close close_x closePlayerBox" href="#"></a>
    </div>
    <uc:FeedBack runat="server" ID="feedBack" pageName="Production Studio" />

    <div class="spacer"></div>

    <div class="repeatBox">
        <a id="A1" class="close close_x" href="#"></a>
        <div class="repeatHolder">
            <span class="repeat">Please choose date</span>
            <input class="txtMonth" type="text" placeholder="mm/dd/yyyy hh:mm" id="txtReapeatDate" />
        </div>
        <a class="submitRepeat" onclick="RepeatSchedule()">Submit</a>
    </div>

    <div class="showInfoDiv">

        <a id="A3" class="close close_x" href="#"></a>
        <span id="spnInfo">An Instant Schedule is an option to fulfil the schedule for the selected day, until midnight, randomly, using videos in the schedule library.
            <br />
            Minimum of 3 unrestricted videos are required, while 50 or more videos are recommended</span>
    </div>

    <div id="popUpOverlay"></div>
 
    <!-- *********** -->
    <!-- STEPS  TOUR -->
    <!-- *********** -->
    <div id="step-one" class="single-step ltr-text">

        <p class="takeTourP pCenter">
            <span class="welcomeWalkTour">Welcome to your own Production Studio!</span>
            Welcome to your Production Studio!
            <br />
            This is where all the magic happens. I am Sam and will be your guide. Follow me to learn all about what you can create.
        </p>
        <div class="guideSam sam05"></div>

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
            Anytime you want a refresher of how to use your Production Studio Page,
            <br />
            just click the Watch Tutorial or Take a Tour buttons.<br />
            These are always here to help.
        </p>
        <%-- <div class="guideSam sam04"></div>--%>
        <footer>
            <div class="pull-left">

                <a href="#" class="btn btn-default prev" data-powertour-action="prev">Prev</a>
                <a href="#" class="btn btn-primary next" data-powertour-action="next">Next</a>
            </div>
            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>
        </footer>
    </div>
    <div id="step-three" class="single-step">
        <span class="connectorarrow-lt">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            <%--    If you want to update your channel category, channel avatar image or delete your channel just click the gear icon to open the channel settings box.--%>
   If you want to update your channel information or delete your channel just click the gear icon.
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
        <span class="connectorarrow-tm">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            <%--   Did you know that you can switch between all of your channels by simply clicking the drop down arrow next to your channel name? <br /> The arrow will bring up all of your existing channels and you can work on any of your channels without ever leaving the production studio. <br /> One easy place to do it all!--%>
                You can easily switch between all of your channels here.
        </p>
        <div class="guideSam sam06"></div>
        <footer>
            <div class="pull-left">

                <a href="#" class="btn btn-default prev" data-powertour-action="prev">Prev</a>
                <a href="#" class="btn btn-primary next" data-powertour-action="next">Next</a>
            </div>
            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>
        </footer>
    </div>
    <div id="step-five" class="single-step ltr-text">
        <span class="connectorarrow-tm">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            <%--   How do you add videos to your channel?  <br />Strimm makes it easy!<br />  Just click the “Get New Videos” button to add video content to your channel.  You have a multitude of ways to search for videos.  You can search the Strimm Public Library, which contains videos pre-selected by us, you can search for your own by using keywords from multiple sources or you can upload a url of your very own. --%>

             By clicking   <span class="point">“Get New Videos”</span> button you can add video content to your channel. 
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
        <span class="connectorarrow-tm">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            <%-- Instant Schedule is a BASIC feature that allows you to instantly create a new, full day schedule for your channel  by simply clicking the instant schedule button. <br /> It will create a random schedule from videos that you had added to your channel.  (note we add 25 videos to your first channel as a jump start).<br /> <strong>Remember:</strong> don’t forget to click the publish button when done.--%>
            <span class="point">Instant Schedule</span> will create a random schedule from videos that you had added to your channel.(note: we add 25 videos to your first channel as a jump start)<br />
            <strong>Remember:</strong> click  "publish" button when done.
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
    <div id="step-seven" class="single-step ltr-text">
        <span class="connectorarrow-tr">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            <span class="point">Autopilot.</span>  If Autopilot is ON, then at 11:00am (Central Time) daily, the system will automatically create an Instant Schedule for the next day. <%--<br />
                 <strong>Remember:</strong> some publicly shared videos in your list can be removed by video provider. This can cause an interruption in your daily broadcast.  So make sure to keep adding fresh video content to your channel and grow your list. --%>
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
        <span class="connectorarrow-tm">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            <%--  Would you like to have the opportunity to create a custom schedule or have more options? Then switch to ADVANCED Mode. <br /> 
                It is perfect for you.  Here you will be able to create a completely Custom Schedule. <br /><strong>Remember:</strong> to click the "Take a Tour" button once in Advanced Mode so that I can guide you through all the additional features.--%>
                To create a custom schedule switch to <span class="point">ADVANCED Mode</span>.
            <br />
            Here you will be able to create a completely Custom Schedule.
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
    <div id="step-nine" class="single-step ltr-text">
        <p class="takeTourP pCenter">
            Creating your own channel is fun and exciting.
            <br />
            We cannot wait to see yours!<br />
            <strong>Remember:</strong>  we are always here for you, if you need any help, please contact us or watch the tutorial or visit our How To & FAQ pages for more.<br />
            Happy Creating!
        </p>
        <div class="guideSam sam04"></div>

        <footer>

            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>


        </footer>
    </div>



    <%--ADVANCED TOUR--%>
    <div id="step-ten" class="single-step ltr-text">
        <p class="takeTourP ">
            <span class="welcomeWalkTour pCenter">Welcome to your own Production Studio!</span>
            Welcome to your Production Studio!  This is where all the magic happens. I am Sam and will be your guide. Follow me to learn all about what you can create.
        </p>
        <div class="guideSam sam05"></div>

        <footer>
            <a href="#" class="btn btn-default endTour" data-powertour-action="stop">Close</a>
            <a href="#" id="btnStartTour" class="btn btn-primary pull-right next" data-powertour-action="next">Take a tour</a>


        </footer>
    </div>
    <div id="step-eleven" class="single-step ltr-text">
        <span class="connectorarrow-tm">
            <!-- custom -->
        </span>


        <p class="takeTourP">Anytime you want a refresher of how to use your Production Studio Page, just click the Watch Tutorial or Take a Tour buttons. These are always here to help.</p>
        <%-- <div class="guideSam sam04"></div>--%>
        <footer>
            <div class="pull-left">

                <a href="#" class="btn btn-default prev" data-powertour-action="prev">Prev</a>
                <a href="#" class="btn btn-primary next" data-powertour-action="next">Next</a>
            </div>
            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>
        </footer>
    </div>
    <div id="step-thirteen" class="single-step">
        <span class="connectorarrow-lt">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            If you want to update your channel information or delete your channel just click the gear icon.
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
    <div id="step-fourteen" class="single-step ltr-text">
        <span class="connectorarrow-tm">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            <%--   Did you know that you can switch between all of your channels by simply clicking the drop down arrow next to your channel name?  The arrow will bring up all of your existing channels and you can work on any of your channels without ever leaving the production studio.  One easy place to do it all!--%>
                  You can easily switch between all of your channels here.
        </p>
        <div class="guideSam sam06"></div>
        <footer>
            <div class="pull-left">

                <a href="#" class="btn btn-default prev" data-powertour-action="prev">Prev</a>
                <a href="#" class="btn btn-primary next" data-powertour-action="next">Next</a>
            </div>
            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>
        </footer>
    </div>
    <div id="step-fifveteen" class="single-step ltr-text">
        <span class="connectorarrow-tm">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            <%--  How do you add videos to your channel?  Strimm makes it easy!  Just click the “Get New Videos” button to add video content to your channel.  You have a multitude of ways to search for videos.  You can search the Strimm Public Library, which contains videos pre-selected by us, you can search for your own by using keywords from multiple sources or you can upload a url of your very own. --%>
                    By clicking  <span class="point">“Get New Videos”</span> button you can add video content to your channel. 
      
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
    <div id="step-sixteen" class="single-step ltr-text">
        <span class="connectorarrow-lt">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            <%--Let me direct your attention to all of your video management tools.  Using these tools you can search for videos in your channel, sort, organize and remove  videos on this channel.--%>
                 Using  <span class="point">video management tools</span> you can search for videos in your channel, sort, organize and remove  videos on this channel.

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

    <div id="step-seventeen" class="single-step ltr-text">
        <span class="connectorarrow-rt">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            <%--  Now lets take a look at your playlist calendar. You can use it to preview all scheduled broadcasts – each day that has an orange tick mark on the date has an existing schedules(s). No broadcast scheduled yet?  No problem!  Follow me to get started.--%>
            <span class="point">Playlist calendar </span>allow you to preview all your active schedules.
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
    <div id="step-eighteen" class="single-step ltr-text">
        <span class="connectorarrow-rt">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            <%-- All scheduling begins with picking a start date on the calendar.  Once you have selected the day you can move down and select how you would like to create your schedule.  You have 3 options: custom, instant schedule or Autopilot. --%>
Custom schedule begins with picking a start date on the calendar.
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

    <div id="step-nineteen" class="single-step ltr-text">
        <span class="connectorarrow-rt">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            <%--Custom Schedule option allows you to create a completely custom schedule and manually arrange it. Simply click on the button and pick the start time for your new  schedule for the pre-selected day off the playlist calendar and start adding videos.
                 <br /><strong>Remember:</strong> don’t forget to click the PUBLISH button when done to publish your schedule.--%>
            <span class="point">Custom schedule</span>  option allows you to create a completely custom schedule and manually arrange it.
                 <br />
            <strong>Remember:</strong> don’t forget to click the PUBLISH button when done to publish your schedule.
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

    <div id="step-twenty" class="single-step ltr-text">
        <span class="connectorarrow-rt rtInstant">
            <!-- custom -->
        </span>

        <p class="takeTourP">


            <span class="point">Instant Schedule </span>will create a random schedule from videos that you had added to your channel.
                <br />
            <strong>Note:</strong>  we add 25 videos to your first channel as a jump start)
                <br />
            <strong>Remember:</strong> click  "publish" button when done.         
                
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

    <div id="step-twentyOne" class="single-step ltr-text">
        <span class="connectorarrow-tr">
            <!-- custom -->
        </span>

        <p class="takeTourP">


            <span class="point">Autopilot. </span>If Autopilot is ON, then at 11:00am (Central Time) daily, the system will automatically create an Instant Schedule for the next day. 
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

    <div id="step-twentyTwo" class="single-step ltr-text">
        <span class="connectorarrow-tm">
            <!-- custom -->
        </span>

        <p class="takeTourP">
            Here you can go back to the  <span class="point">BASIC Mode</span>, which allows you to create an instant schedule quickly. 
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

    <div id="step-twentyThree" class="single-step ltr-text">
        <p class="takeTourP pCenter">
            Creating your own channel is fun and exciting we cannot wait to see yours! 
            <br />

            <strong>Remember</strong> we are always here for you, if you need any help, please contact us or watch the tutorial or visit our How To & FAQ pages for more.<br />

            Happy Creating!
        </p>
        <div class="guideSam sam04"></div>

        <footer>

            <a href="#" class="btn btn-default pull-right endTour" data-powertour-action="stop">End tour</a>


        </footer>
    </div>

    <div id="getHLSLinkPopup" class="HLSLinkPopup">
        <a id="close_x" class="close close_x" href="#">close</a>
        <h1 class="pageTitle">HLS Playlist Link Generation</h1>

        <u>Attention:</u>
        <ol>
            <li>The videos for the HLS feed link are taken from the published schedule, in their scheduled order.</li>
            <li>Only videos in .m3u8 format are used for the HLS feed.</li>
            <li>The HLS feed wil auto-refresh daily based on the published schedule.</li>
        </ol>
        <br />
        <br />
        <div id="linksHolder">
            <label>Generated HLS (m3u8) link of the playlist</label><br />
            <br />
            <input type="text" id="hlsLinkText" class="hlsLinkHolder" readonly="readonly" />

            <br />
            <br />
            <br />
            <label>Generated JSON file (feed link) of the playlist</label><br />
        </div>
        <div id="planMissingHolder"></div>
        <br />
        <input type="text" id="jsonLinkText" class="hlsLinkHolder" readonly="readonly" />

        
    </div>

    <div id="customScheduleCreatePopup" class="customScheduleCreatePopup">
        <div class="backnextMobile">
            <a class="backSchedule">back</a>
            <a class="nextSchedule">next</a>
        </div>
        <div id="calendarHolderMobile">
        </div>
        <a id="close_x" class="close close_x" href="#">close</a>
        <div id="newScheduleHeader">
            <div class="newScheduleDateHolder">
                <%--<span class="createScheduleTitle">Create </span>--%>
                <span class="customSchedule">Custom Schedule </span>
                <span class="createScheduleTitle">for</span>
                <span id="newScheduleDate"></span>
                <span id="newScheduleFooter">(Use calendar to change date)</span>
            </div>
        </div>

        <!--Advanced Mode Custom Schedule Button -->
        <div id="newScheduleTimePicker">
            <input autocomplete="off" id="newScheduleStartTimePicker" class="time ui-timepicker-input" placeholder="Pick start time ▼" type="text" title="Pick start time of a custom schedule!" />
            <input type="button" id="btnStartScheduleOk" class="startScheduleOk" value="Create" onclick="StartNewScheduleCreation()" title="Click to create a custom schedule!" />
        </div>
    </div>
    <script src="https://apis.google.com/js/client.js"></script>
    <div id="editPrivateVideoModal" style="display: none;">
        <a class="close_x close">x</a>
        <h1 class="pageTitle">Edit Private Video</h1>
        <ul id="leftSide" class="ulImportVideoDetails">
            <li id="liImageEditor">

                <div class="image-editor">
                    <div class="image-size-label">
                    </div>
                    <div class="cropit-image-preview-container">
                        <div class="cropit-image-preview"></div>
                    </div>
                    <div class="minImgSize">(Minimum image size: 640px X 480px)</div>
                    <input type="range" class="cropit-image-zoom-input" />
                    <div class="image-size-label">Move cursor to resize image</div>
                    <input type="file" class="cropit-image-input" />
                    <div class="select-image-btn">Pick Video Thumbnail</div>
                </div>


            </li>
        </ul>
        <ul id="rightSide" class="ulImportVideoDetails">
            <li >
                <div id="pickProvider"  >
                    <div id='videoProviderHolder' class='pickProvidernoBorder'>
                        <span id="spnProvider">Custom Provider</span>
                    </div>
                </div>
            </li>
            <li>
                <div id="privateVideoDesc">
                    <input id="txtPrivateVideoTitle" style="" type="text" placeholder="Enter Video Title" />
                </div>
            </li>
            <li>
                <span id="privateVideoUrl"></span>
                </li>
              <li>
                <div id='divUrlCategory' style="margin-top: 0;">
                    <div id='ddlByUrlCategoryHolder' class='styled-selectSortSearch'>
                        <select id='ddlByUrlCategory' class='ddlCategory categoryPrVodeos ddlborder'></select>
                    </div>
                </div>
            </li>
            <li>
                <textarea style="" placeholder="Video Description"></textarea>
            </li>
            <li class="editModalcheckBoxHolder">
                <div id="checkBoxHolder">
                    <input type="checkbox" id="checkMatureContentForm" />
                    <label for="checkMatureContentForm">Mature Content</label>
                <%--    <span id="spnMatureInfo">i</span>--%>
                </div></li>
             <li class="editModalvideoDurationHolder">
                <div id="videoDurationHolder">
                    <span>duration:</span>
                    <input id="txtHour" placeholder="hh" type="number" value="0" max="99" />
                    <span>:</span>
                    <input type="number" max="59" id="txtmMinutes" value="0" placeholder="mm" />
                    <span>:</span>
                    <input type="number" max="59" id="txtmSeconds" value="0" placeholder="ss" />
                </div>
            </li>
            <li class="editModalUpdateVideo" ><a class="btnUpdateVideo export">Update Video</a></li>
        </ul>
    </div>

    <div class='startDateTimePickerHolderLive'>
        <h1 class="videoFinderH1 selectLiveEventH1">Please select start and end time for your Live event</h1>
        <a id="close_x" class="close close_x" href="#">close</a>
        <div id="dateTimePickerHolder">
        </div>

        <a id="saveLiveVideo">save </a>
 <%--       <span id="timeZoneName"></span>
        <a id="timeZoneNameChange" onclick="ShowTimeZoneSelect()">edit</a>--%>

  <%--      <select class="timeZoneSelect"></select>--%>
        <%--<a id="newTimeZone" onclick="ChangeTimeZoneNameAndTime()" style="display: none; float: left; margin-left: 10px;">ok</a>
        <a id="cancelTimeZone" onclick="CancelTimeZone()" style="display: none; float: left; margin-left: 10px;">cancel</a>--%>
        <span class="spnError"></span>
    </div>
      <script src="//www.google.com/jsapi" type="text/javascript"></script>
    <script src='https://cdnjs.cloudflare.com/ajax/libs/jstimezonedetect/1.0.4/jstz.min.js'></script>
     <script type="text/javascript">
         google.load("swfobject", "2.1");
    </script>
        <div id="modalDragInfo" style="display:none; background-color:#fff; width:600px;">
           <a id="close_x" class="close close_x" href="#"></a>
            <h1 class="pageTitle">How Drag & Drop Works</h1>
            <div style="width:90%; margin:auto; font-size:16px; ">
               Though adding videos to the playlist by clicking “+” on the video is recommended, you can use drag & drop functionality as well. Pick the video and slowly transfer it to the area in the playlist, where you want to schedule it. Do not move it too fast. Add the video to an available blank grey spot only. TIP: move the video up and down in the playlist before you drop it in order to “catch” the blank grey spot. If you want to reposition the video in the playlist, then simply drag the video and drop it in the spot of your preference.
            </div>
            <div class="dragInfoImage">
                
            </div>
            
        </div>

    <script>!function (e) {
            function r(r) {
                for (var n, i, a = r[0], c = r[1], l = r[2], s = 0, p = []; s < a.length; s++) i = a[s], Object.prototype.hasOwnProperty.call(o, i) && o[i] && p.push(o[i][0]), o[i] = 0;
                for (n in c) Object.prototype.hasOwnProperty.call(c, n) && (e[n] = c[n]);
                for (f && f(r); p.length;) p.shift()();
                return u.push.apply(u, l || []), t()
            }

            function t() {
                for (var e, r = 0; r < u.length; r++) {
                    for (var t = u[r], n = !0, a = 1; a < t.length; a++) {
                        var c = t[a];
                        0 !== o[c] && (n = !1)
                    }
                    n && (u.splice(r--, 1), e = i(i.s = t[0]))
                }
                return e
            }

            var n = {}, o = { 1: 0 }, u = [];

            function i(r) {
                if (n[r]) return n[r].exports;
                var t = n[r] = { i: r, l: !1, exports: {} };
                return e[r].call(t.exports, t, t.exports, i), t.l = !0, t.exports
            }

            i.e = function (e) {
                var r = [], t = o[e];
                if (0 !== t) if (t) r.push(t[2]); else {
                    var n = new Promise((function (r, n) {
                        t = o[e] = [r, n]
                    }));
                    r.push(t[2] = n);
                    var u, a = document.createElement("script");
                    a.charset = "utf-8", a.timeout = 120, i.nc && a.setAttribute("nonce", i.nc), a.src = function (e) {
                        return i.p + "static/js/" + ({}[e] || e) + "." + { 3: "c5fd260e" }[e] + ".chunk.js"
                    }(e);
                    var c = new Error;
                    u = function (r) {
                        a.onerror = a.onload = null, clearTimeout(l);
                        var t = o[e];
                        if (0 !== t) {
                            if (t) {
                                var n = r && ("load" === r.type ? "missing" : r.type), u = r && r.target && r.target.src;
                                c.message = "Loading chunk " + e + " failed.\n(" + n + ": " + u + ")", c.name = "ChunkLoadError", c.type = n, c.request = u, t[1](c)
                            }
                            o[e] = void 0
                        }
                    };
                    var l = setTimeout((function () {
                        u({ type: "timeout", target: a })
                    }), 12e4);
                    a.onerror = a.onload = u, document.head.appendChild(a)
                }
                return Promise.all(r)
            }, i.m = e, i.c = n, i.d = function (e, r, t) {
                i.o(e, r) || Object.defineProperty(e, r, { enumerable: !0, get: t })
            }, i.r = function (e) {
                "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e, Symbol.toStringTag, { value: "Module" }), Object.defineProperty(e, "__esModule", { value: !0 })
            }, i.t = function (e, r) {
                if (1 & r && (e = i(e)), 8 & r) return e;
                if (4 & r && "object" == typeof e && e && e.__esModule) return e;
                var t = Object.create(null);
                if (i.r(t), Object.defineProperty(t, "default", {
                    enumerable: !0,
                    value: e
                }), 2 & r && "string" != typeof e) for (var n in e) i.d(t, n, function (r) {
                    return e[r]
                }.bind(null, n));
                return t
            }, i.n = function (e) {
                var r = e && e.__esModule ? function () {
                    return e.default
                } : function () {
                    return e
                };
                return i.d(r, "a", r), r
            }, i.o = function (e, r) {
                return Object.prototype.hasOwnProperty.call(e, r)
            }, i.p = "/", i.oe = function (e) {
                throw console.error(e), e
            };
            var a = this["webpackJsonpstrimm.player"] = this["webpackJsonpstrimm.player"] || [], c = a.push.bind(a);
            a.push = r, a = a.slice();
            for (var l = 0; l < a.length; l++) r(a[l]);
            var f = c;
            t()
        }([])</script>
    <script src="/reactplayer/js/2.cd6796f3.chunk.js"></script>
    <script src="/reactplayer/js/main.4514192c.chunk.js"></script>
   
</asp:Content>
