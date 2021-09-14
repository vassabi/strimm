<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChannelManagementUC.ascx.cs" Inherits="StrimmTube.UC.ChannelManagementUC" %>

<%@ Register Src="~/UC/UpdateChannelUC.ascx" TagPrefix="uc2" TagName="UpdateChannelUC" %>


<%: System.Web.Optimization.Styles.Render("~/bundles/channelmanagmentUC/css") %>

<%: System.Web.Optimization.Scripts.Render("~/bundles/channelmanagmentUC/js") %>


<link href="/../Plugins/DHTMLXCalendar/codebase/dhtmlxcalendar.css" rel="stylesheet" />
<link rel="stylesheet" type="text/css" href="/../Plugins/DHTMLXCalendar/sources/dhtmlxCalendar/codebase/skins/dhtmlxcalendar_dhx_skyblue.css" />
<link href="/Flowplayer7/skin/skin.css" rel="stylesheet" />
<link href="/css/flowplayer.override.css" rel="stylesheet" />
<%--<script src="/../Plugins/DHTMLXCalendar/sources/dhtmlxCalendar/codebase/dhtmlxcalendar.js"></script>
    <script src="/../Plugins/DHTMLXCalendar/sources/dhtmlxcommon/codebase/dhtmlxcommon.js"></script>
    <script src="/../Plugins/DHTMLXCalendar/sources/dhtmlxcommonpopup.js"></script>
    <script src="/../Plugins/DHTMLXCalendar/sources/dhtmlxpopup.js"></script>--%>

<%--<script src="/../Plugins/popup/jquery.lightbox_me.js"></script>
    <script src="/../JS/Controls.js"></script>--%>
<link href="/../Plugins/DHTMLXCalendar/sources/dhtmlxpopup_dhx_terrace.css" rel="stylesheet" />

<script src="/JS/swfobject.js" type="text/javascript"></script>
<script src="//www.google.com/jsapi" type="text/javascript"></script>
<script src="https://www.youtube.com/iframe_api"></script>

<%--<script src="http://crypto-js.googlecode.com/svn/tags/3.0.2/build/components/core-min.js"></script>
<script src="http://crypto-js.googlecode.com/svn/tags/3.0.2/build/components/enc-base64-min.js"></script>
<script src="http://crypto-js.googlecode.com/svn/tags/3.0.2/build/rollups/sha256.js"></script>--%>


<script type="text/javascript">
    google.load("swfobject", "2.1");
</script>
<style>
    .entry-header {
        display: none;
    }
</style>

<script type="text/javascript">

    var channelTubeId = '<%=channelTubeId%>';
    var accountNumber = "<%=accountNumber%>";
    var userName = "<%=userName%>";
    var strimmDomain = '<%=strimmDomain%>';
    var channelTubeUrl = '<%=channelTubeUrl%>';
    var categoryId = "<%=categoryId%>";
    var channelName = "<%=channelName%>";
    var channelUrl = "<%=channelTubeUrl%>";
    var channelPictureUrl = "<%=channelPictureUrl%>";
    var customLogoUrl = "<%=customLogoUrl%>";
    var channelCategoryValue = "<%=channelCategoryValue%>";
    var languageId = '<%=languageId%>';
    var str = '<%=isWhiteLabeled%>';
    var isWhiteLabeled = (str + '').toLowerCase() === 'true';
    var strIsEmbedded = '<%=embedEnable%>';
    var matureChannelContentEnabled = JSON.parse("<%=matureChannelContentEnabled%>".toLowerCase());
        var embedEnable = (strIsEmbedded + '').toLowerCase() === 'true';
        var strPasswordProtect = '<%=isPasswordProtect%>';
        var isPasswordProtected = (strPasswordProtect + '').toLowerCase() === 'true';
        var strMuteOnStartup = "<%=muteOnStartup%>";
        var muteOnStartup = (strMuteOnStartup + '').toLowerCase() === 'true';
        var channelPassword = '<%=channelPassword%>';
        var subscribtionOrderCount = 2;
        var availableWhiteLableOptions = 2;
        var strenabElCutomBranding = '<%=enabelCutomBranding%>';
        var enabelCutomBranding = (strenabElCutomBranding + '').toLowerCase() === 'true';
        var webMethodGetChannelCategoriesJsonForCreateChannel = "/WebServices/ChannelWebService.asmx/GetChannelCategories";
        var showPrivateVideoMode = JSON.parse("<%=showPrivateVideoMode%>".toLowerCase());
        var strIsLogoModeActive = "<%=isLogoModeActive%>";
    var isLogoModeActive = (strIsLogoModeActive + '').toLowerCase() === 'true';
    var liveFirst = ('<%=LiveFirst%>').toLowerCase()==='true';


</script>

<style>
    #calendarHolder {
        height: 213px;
        position: relative;
        width: 100%;
    }


        #calendarHolder .dhtmlxcalendar_dhx_skyblue {
            background-color: #fff;
            overflow: hidden;
            width: 106%;
            font-weight: normal;
        }

        #calendarHolder .dhtmlxcalendar_month_cont {
            -moz-border-bottom-colors: none;
            -moz-border-left-colors: none;
            -moz-border-right-colors: none;
            -moz-border-top-colors: none;
            background-color: #eee;
            background-image: none;
            border-color: -moz-use-text-color -moz-use-text-color #fff;
            border-image: none;
            border-style: none none solid;
            border-width: medium medium 1px;
            width: 100%;
        }

        #calendarHolder .dhtmlxcalendar_days_cont {
            width: 105%;
        }

        #calendarHolder .dhtmlxcalendar_dates_cont {
            width: 100%;
        }

        #calendarHolder ul {
            width: 100%;
            height: 30px;
        }

        #calendarHolder li {
            width: 13.3%;
            height: 30px;
            font-size: 14px;
            line-height: 30px;
        }

    .dhtmlxcalendar_dhx_skyblue div.dhtmlxcalendar_selector_obj table.dhtmlxcalendar_selector_table td.dhtmlxcalendar_selector_cell_middle ul.dhtmlxcalendar_selector_line li.dhtmlxcalendar_selector_cell {
        width: 50px!important;
    }

        #calendarHolder .dhtmlxcalendar_cell {
        }

        #calendarHolder .dhtmlxcalendar_dhx_skyblue div.dhtmlxcalendar_month_cont ul.dhtmlxcalendar_line li.dhtmlxcalendar_cell.dhtmlxcalendar_month_hdr {
            width: 100%;
        }

        #calendarHolder .dhtmlxcalendar_dhx_skyblue div.dhtmlxcalendar_dates_cont ul.dhtmlxcalendar_line li.dhtmlxcalendar_cell.dhtmlxcalendar_cell_month {
            background-color: #f7f7f7;
            border: 1px solid #fff;
            font-size: 12px;
        }

        #calendarHolder .dhtmlxcalendar_dhx_skyblue div.dhtmlxcalendar_dates_cont ul.dhtmlxcalendar_line li.dhtmlxcalendar_cell.dhtmlxcalendar_cell_month_date {
            background-color: #40d304;
            border: 1px solid #f7f7f7;
            font-size: 12px;
            color: #fff;
        }

        #calendarHolder .dhtmlxcalendar_dhx_skyblue div.dhtmlxcalendar_dates_cont ul.dhtmlxcalendar_line li.dhtmlxcalendar_cell.dhtmlxcalendar_cell_month_weekend {
            background-color: #2a99bd;
            border: 1px solid #f9f9f9;
            color: #fff;
            font-size: 12px;
        }

    .dhtmlxcalendar_dhx_skyblue div.dhtmlxcalendar_days_cont ul.dhtmlxcalendar_line li {
        width: 29px;
        height: 19px;
        line-height: 19px;
        margin-right: 1px;
        font-size: 9px;
        background-color: #aaa;
        color: #fff;
    }

        .dhtmlxcalendar_dhx_skyblue div.dhtmlxcalendar_days_cont ul.dhtmlxcalendar_line li.dhtmlxcalendar_cell.dhtmlxcalendar_day_weekday_cell, .dhtmlxcalendar_dhx_skyblue div.dhtmlxcalendar_days_cont ul.dhtmlxcalendar_line li.dhtmlxcalendar_cell.dhtmlxcalendar_day_weekday_cell_first {
            color: #fff;
            background-color: #f18a11;
        }

    #calendarHolder .dhtmlxcalendar_dhx_skyblue div.dhtmlxcalendar_dates_cont ul.dhtmlxcalendar_line li {
        /*border: 2px solid #fff;*/
    }

        #calendarHolder .dhtmlxcalendar_dhx_skyblue div.dhtmlxcalendar_dates_cont ul.dhtmlxcalendar_line li.dhtmlxcalendar_cell.dhtmlxcalendar_cell_month_hover {
            background-color: #2a99bd;
            border: 1px solid #2a99bd;
            font-size: 12px;
            color: #fff;
        }


    #calendarHolder .dhtmlxcalendar_dhx_skyblue div.dhtmlxcalendar_dates_cont ul.dhtmlxcalendar_line {
        height: 24px;
    }
</style>
<style type="text/css">
    #slider3 {
        position: relative;
        margin-top: 40px;
        width: 165px;
        height: 600px;
        border-left: 1px solid #aaa;
        border-right: 1px solid #aaa;
        margin-bottom: 40px;
        float: right;
        background-color: white;
    }
</style>


<div id="channelManagementContent">
    <div class="channelManagementContentUpper"></div>
    <div class="channelManagementContentInner">

        <div id="topHolder">
            <div class="btnTutorialTourHolder">

                <div id="btnWatchVideoHolder"><a id="ancHowTo" onclick="ShowTutorialPlayer('<%=studioPageTutorialVideoId %>', false)">watch tutorial &#9654;</a></div>
                <div id="btnStartTourHolderPS">
                    <a id="ancStartTour" onclick="TriggerWalkTruByWorkingMode()" class="ancStartTourProdStudio">or <span class="videoOrTour">take a tour &#9654;</span></a>

                    <a id="advancedTour" onclick="TriggerWalkTruByWorkingMode()" class="ancStartTourProdStudio">or <span class="videoOrTour">take a tour &#9654;</span></a>
                </div>
            </div>
            <div class="topWrapperHolder">
                <div id="leftWrapper">
                    <div id="channelInfoHolder">

                        <div id="innerLeftWrapper">
                            <a onclick="CreateChannel.GetUpdateChannelPopup()" class="ancEditBlack" id="channelSettingsBtn"></a>
                            <a onclick="CreateChannel.GetOTTSettingsPopup()" class="settingsOtt" id="ottSettingsBtn"></a>


                            <a id="channelLink" href="/<%=channelOwnerName%>/<%=channelTubeUrl%>">

                                <img class="playIconGoToChannel" />

                                <asp:Image runat="server" ClientIDMode="Static" ID="imgChannelAvatar" ImageUrl="/images/comingSoonBG.jpg"></asp:Image>

                                <a id="ancCropSchedule" onclick="ShowCropModalChannelAvatar()"></a>

                            </a>

                            <span id="clickPlayText">Click PLAY above to WATCH</span>

                        </div>
                        <div id="innerRightWrapper">



                            <div id="titleHolder" class="styled-select">
                                <select id="ddlAvailableChannels" class="ddlAvailableChannels" onchange="ChangeActiveChannel()"></select>
                                <%-- <a id="ancName"> <%=channelTubeName%></a>--%>
                            </div>
                            <div id="categoryHolder">
                                <asp:Label runat="server" ID="lblCategory" ClientIDMode="Static"></asp:Label>
                            </div>
                            <div id="channelInfo">
                                <asp:Label runat="server" ID="lblSubscribers" ClientIDMode="Static"></asp:Label>
                                <asp:Label runat="server" ID="lblViews" ClientIDMode="Static"></asp:Label>

                            </div>
                        </div>

                        <%--<a href="" title="Find Us on LinkedIn" class="header-social-icon social-linkedin"><i class="fa fa-linkedin"></i></a>--%>
                    </div>

                    <%--               <div id="channelMenuHolder">
                          
                </div>--%>
                </div>
                <div id="rightWrapper">
                    <div id="calendarHolder">
                    </div>
                </div>
            </div>
        </div>
        <%--  <div class="gradient"></div>--%>


        <div id="bottomHolder"></div>
        <div id="modalSchedule" style="display: none;">
            <a id="close7_x" class="close close_x" href="#"></a>
            <div id="scheduleHeader">
                <span id="spnHeaderDate"></span>
            </div>
            <div class="nano" style="min-height: 300px; max-height: 600px; padding-top: 20px;">
                <div class="content">
                    <div id="scheduleContentHolder" style="padding-top: 20px;">
                    </div>

                </div>
            </div>
        </div>

        <div>
        </div>
        <div id="divChannelCropContainer">
            <div class="image-editor">
                <a class="close close_x" href="#"></a>

                <div class="image-size-label">
                    <h1 class="popupHeader">Image Editor</h1>
                </div>

                <!-- .cropit-image-preview-container is needed for background image to work -->
                <div class="cropit-image-preview-container">
                    <div class="cropit-image-preview"></div>
                </div>

                <div class="select-image-btn">Upload Channel Image</div>
                <div class="minImgSize">(Minimum image size: 200px X 200px)</div>

                <input type="range" class="cropit-image-zoom-input" />

                <div class="image-size-label">Move cursor to resize image</div>

                <a class="export">Crop and Save</a>
                <input type="file" class="cropit-image-input" />
            </div>
        </div>
    </div>
</div>



