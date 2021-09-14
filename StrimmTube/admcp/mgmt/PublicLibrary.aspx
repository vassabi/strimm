<%@ Page Title="" Language="C#" MasterPageFile="~/admcp/mgmt/Admin.Master" AutoEventWireup="true" CodeBehind="PublicLibrary.aspx.cs" Inherits="StrimmTube.admcp.mgmt.PublicLibrary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/Schedule.css" rel="stylesheet" />
    <link href="css/publicLibrary.css" rel="stylesheet" />
    <script src="../../JS/Controls.js"></script>
    <script src="/../Plugins/popup/jquery.lightbox_me.js"></script>
    <script src="js/publicLib.js"></script>
     <script src="//www.google.com/jsapi" type="text/javascript"></script>
  
       <script type="text/javascript">
           google.load("swfobject", "2.1");
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <%--<div id="loadingDiv">
        <div id="loadingDiv">
            <img src="..//images/ajax-loader(3).gif" />
        </div>
    </div>--%>
 <%--   <div class="styled-selectSortSearch categoryKeyword">--%>
    <select onchange="sortVideosForAdd(this)" id="sortSelect">
        <option value="0">sort results</option>
        <option value="1">longer to shorter</option>
        <option value="2">shorter to longer</option>
        <option value="3">most viewed</option>
        <option value="4">recently added</option>
    </select>
<%--   </div>--%>
    <select id='ddlPublicLibraryCategory' class='ddlCategory' onChange='ChangePublicLibraryCategoryHandler()'></select>
   
    <div class="loadedContent"></div>
   <%-- <div class="loadMoreBtnHolder">--%>
     <input id="loadMoreVideos" class="classloadMoreVideos" type="button" onclick="LoadMoreResults()" value="Load More">
       <%-- </div>--%>
     <div class="playerbox" style="display: none;">
        <div id="related" class="player"></div>
        <div id="content-container">
        </div>
  <%--       <a class="addVideoPL">add video</a>--%>
     
    </div>
       <a id="close_x" class="close close_x closePlayerBox" href="#">close</a>
</asp:Content>
