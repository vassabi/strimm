<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="VideoStore.aspx.cs" Inherits="StrimmTube.VideoStore" enableEventValidation="false" %>
<%@ Register Src="~/UC/CreateChannelUC.ascx" TagPrefix="uc" TagName="CreateChannelUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Video Store | Strimm
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Strimm TV - Video Store"/>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="/flowplayer/html5/5.5.2/skin/functional.css" rel="stylesheet" />
    <script src="/flowplayer/html5/5.5.2/flowplayer.min.js"></script>
    <script src="//cdn.flowplayer.com/releases/native/stable/plugins/hls.min.js"></script>
    <link href="/css/videoStore.css" rel="stylesheet" />
    <link href="/css/CSS.css" rel="stylesheet" />  
    <script src="https://www.youtube.com/iframe_api"></script>
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="/JS/Main.js"></script>
    <script src="/JS/VideoStore.js"></script>
    <script src="/JS/GridderUtils.js"></script>
   
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%= new StrimmTube.CorsUpload { }.ToString()%>

    <script type="text/javascript">
        google.load("swfobject", "2.1");

        var userId = '<%=UserId%>';
        var videoStore = window.getVideoStore();

        $(document).ready(function () {
            videoStore.init(userId);
        });
    </script>


    <div id="divHeader">

    <div class="vsPageTitle">
        Video Store
    </div>
            <div class="gradient"></div>

            <div id="DashboardBackgroungImg" >
                <a href="#"> <asp:Image runat="server" id="imgDashboard" ClientIDMode="Static"/></a>
            </div>
            <div id="headMenu">
                <asp:Label runat="server" ID="lblUserInfo" ClientIDMode="Static"></asp:Label>
                <asp:Label runat="server" ID="lblCountry" ClientIDMode="Static"></asp:Label>
            </div>
        </div>

<%--    <hr />--%>
    <div id="divActionsHolder">
          

        <div id="divActionRightContainer">

            <div class="styled-selectSortSearch categoryKeyword">
                <div id="divVideoCategoryFilter">
                    <asp:DropDownList runat="server" ID="ddlVideoStoreCategory" CssClass="ddlVideoStoreCategory" AutoPostBack="false" ClientIDMode="Static" onchange="videoStore.selectedCategoryChanged()">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="styled-selectSort">

                <div id="divVideoSort" class="sortBy styled-selectSort sortBySchedule vsSort">
                    <select id="ddlVideoStoreVideos" onchange="videoStore.sortVideos(this)">
                        <option value="0">sort results</option>
                        <option value="1">longer to shorter</option>
                        <option value="2">shorter to longer</option>
                        <option value="3">most viewed</option>
                        <option value="4">recently added</option>
                    </select>
                </div>
            </div>
        </div>  
    </div>

      <div id="divActionLeftContainer" class="actionSearchContainer">
            <div class="vsSearchMidHolder">
            <input id="txtVideoSearchKeywords" class="vsSearch" type="text" onchange="videoStore.findVideoByKeywords()" placeholder="Search Video Store" />
            <a id="search" class="vsAncSearch" onclick="videoStore.findVideosByKeywords()">search</a>
            <input id="rdVideosInVideoRoom" class="vsSearchVideosSortingInput" name="search" type="radio" value="videos" onchange="videoStore.searchTargetChanged()" checked/><span class="vsVideoSorting">Videos</span>
            <input id="rdVideoOwners" class="vsSearchVideosSortingInput" name="search" type="radio" value="owners" onchange="videoStore.searchTargetChanged()"/><span class="vsVideoSorting">Video Owners</span>
        </div>
            </div>
    <div id="divVideosHolder">
        <div id="divLoadMore" onclick="LoadMore()">Load More</div>
    </div>
 
    <uc:FeedBack ID="FeedBack1" runat="server" pageName="My Video Room"/>
</asp:Content>

