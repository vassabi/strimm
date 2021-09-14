<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Archive.aspx.cs" Inherits="StrimmTube.Archive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Watch Later videos | Strimm Online TV
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Strimm Online TV | Watch later videos" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <%-- <link href="/css/Schedule.css" rel="stylesheet" />--%>
    <%: System.Web.Optimization.Styles.Render("~/bundles/schedule/css") %>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/watchItLater/js") %>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<script src="/Plugins/popup/jquery.lightbox_me.js"></script>
 
    <script src="/JS/Controls.js"></script>
    <script src="/JS/WatchItLater.js"></script>
    <script src="/JS/jquery.cookie.js"></script>--%>
    <script type="text/javascript">
        var userId = '<%=UserId%>';
        $(document).ready(function () {
            GetWatchItLaterVideos();
            $("#txtSearchByText").val("");
            $("#ddlSortVideo option:first").attr('selected', 'selected');

        });
        $(document).keyup(function (event) {

            if (event.keyCode == 13) {
                $(".btnSearchByText").trigger('click');
            }
        });
    </script>
    <%--   <div runat="server" id="divBackendmenuHolder">
    </div>--%>
    <%--    <div id="divTitleUrlHolder">
        <h1>watch later</h1>
    </div>--%>
    <%--    
    <asp:ScriptManager runat="server" ID="scriptManager"></asp:ScriptManager>--%>
    <div class="mainContentWrapper">
        <div id="content" class="archive">
            <div class="pageInfoHolder">
                <h1>watch later videos</h1>
                <span id="lblChannelCount">0</span>
                <div class="txtSearchByTextHolder">
                    <input type="text" id="txtSearchByText" placeholder="Enter Keyword" />
                    <a class="btnSearchByText" onclick="ShowSearched()"></a>
                    <a class="clearSearch" onclick="ClearSearchResult()"> </a>
                </div>
                <div class="videoSorting">
                    <select id="ddlSortVideo" class="ddlSortVideo" onchange="SortWatchLaterVideos(this)">
                        <option value="1">recently added</option>
                        <option value="2">a-z</option>
                        <option value="3">z-a</option>
                    </select>

                </div>
            </div>
            <asp:Label runat="server" ID="lblmessage" ClientIDMode="Static"></asp:Label>

            <div class="left">
                <%-- <div class="nano" style="height: 600px;">
                        <div class="content">--%>
                <div id="videoBoxHolder" class="videoBoxHolder channelsHolder watchLater">
                </div>
            </div>
            <%--   </div>
                </div>--%>



            <script src="//www.google.com/jsapi" type="text/javascript"></script>
            <script src="https://www.youtube.com/iframe_api"></script>
            <script type="text/javascript">
                google.load("swfobject", "2.1");
            </script>
            <div class="playerbox" style="display: none;">
                <a id="related" class="player"></a>
                <div id="content-container">
                </div>
                 <a id="close_x" class="close close_x closePlayerBox" href="#">close</a>
            </div>
           
            <uc:FeedBack runat="server" ID="feedBack" pageName="watch it later" />
        </div>
        <div id="modalRemove">
            <span>Would you like to remove this video from the list? </span>
            <div class="confurmButtons">
                <a id="cancelRemove" onclick="CloseModalRemove()">Cancel</a>
                <a id="removeOk">Ok</a>
            </div>
            <div class="doNotShow">
                <input type="checkbox" id="msgRemove" />
                <label for="checkBox">Do not show this message again</label>
            </div>
        </div>
    </div>
</asp:Content>
