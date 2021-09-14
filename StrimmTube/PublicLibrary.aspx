<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="PublicLibrary.aspx.cs" Inherits="StrimmTube.PublicLibrary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content=" Strimm is a Social Internet TV Network. It allows watching free video shows online continuously, create public TV streaming and socialize with friends" />
    <meta name="keywords" content=" internet tv, social tv, social network, watch videos, tv network, videos online, online videos, schedule videos, schedule broadcast, create tv, playlist builder, free videos, watch videos online, tv streaming" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">   
    <link href="/css/youtubeSearch.css" rel="stylesheet" />
    <link href="/Plugins/Scroller/scroller.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <script src="/Plugins/Scroller/nanoscroller.min.js"></script>
    <script src="../Plugins/popup/jquery.lightbox_me.js"></script>
    <script src="/JS/PublicLibrary.js"></script>  
    <script src="/JS/Controls.js"></script>
    <script type="text/javascript">
        var channelTubeId = "<%=channelTubeId%>";
    </script>
    <div runat="server" id="divBackendmenuHolder">
        <uc:ChannelManagement runat="server" id="cm1"></uc:ChannelManagement>
    </div>
<%--    <div id="loadingDiv">
             <div id="loadingDivHolder">
                 <img src="/images/ajax-loader(3).gif" />
             </div>
         </div>--%>
    <div id="content" class="youtubeSearch">
         <div class="inputHolder">
                    
                    <div id="divSortByURL">
                      <span class="spnChooseCategory">  category</span> 
                    <asp:DropDownList runat="server" ID="ddlCategory" ClientIDMode="Static" CssClass="ddlCategory" AutoPostBack="false" onChange="GetPublicLibraryByCatregory()"></asp:DropDownList>
                      <%--   <a onclick="AddAllSelectedToVR('pubLib')">add selected videos</a>--%>
                    </div>
                   
                </div>
        <div id="resultHolder" runat="server" ClientIdMode="static">
             
         <div id="divResulSearchURL">

         </div>
                 
           
            </div>
         <a id="loadModeResults" onclick="GetPublicLibrary()" class="newresult">Get More results</a>
                   
           
        </div>
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
        <uc:FeedBack runat="server" ID="feedBack" pageName="video search" />
    </div>
</asp:Content>

