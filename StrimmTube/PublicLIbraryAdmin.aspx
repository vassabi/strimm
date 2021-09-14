<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="PublicLIbraryAdmin.aspx.cs" Inherits="StrimmTube.PublicLIbraryAdmin" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
  
      <link href="Plugins/Scroller/scroller.css" rel="stylesheet" />
 
     <link href="css/youtubeSearch.css" rel="stylesheet" />
  
      <script src="Plugins/Scroller/nanoscroller.min.js"></script>
    <script src="../admcp/mgmt/js/publicLib.js"></script>
    <script src="../admcp/mgmt/js/SearchVideo.js"></script>

     <style>
      
       #divTopNav
       {
           display:none;
       }
       .divTitle .originTitle, .divCustomize .originDesc {
  display: block;
  height: 14px;
  overflow: hidden;
  padding-left: 0;
  text-overflow: ellipsis;
  white-space: nowrap;
  width: 55%;
}
       #importUrl, #SearchByKeyWords {
   clear: both;
float: left;
margin-bottom: 10px;
margin-top: 10px;
position: relative;
width: 90%;
}
      #divCustomize .actions a {
 
  width: 102px;
}
      .divTitle .originalTitle, .divDesc .originalTitle 
      {
          display: block;
height: 14px;
overflow: hidden;
padding-left: 0;
text-overflow: ellipsis;
white-space: nowrap;
width: 60%;

      }
    
   body .divBoxContent.publicLib  {
  background-color: #eeeeee;
  border: 1px solid #2a99bd;
  border-radius: 5px;
  float: left; 
  margin: 4px 0.5%;
  max-width: 900px;
  min-width: 250px;
  padding: 0.5%;
  position: relative;
  width: 22%;
}
   .publicLib  .divVideoInformation {
  float: left;
  margin-left: 15px;
  width: 177px;
}
  .publicLib .divTitle .originalTitle, .publicLib .divDesc .originalTitle {
 
  width: 53%;
}
  .publicLib .divCustomize .actions a {
  clear: both;
  display: block;
  margin-top: 6px;
  width: 39px;
   background: none repeat scroll 0 0 #ececec;
border-color: #ffffff;
border-radius: 3px;
border-style: solid;
border-width: 1px;
color: #2a99bd;
cursor: pointer;
}
  .publicLib .divCustomize .actions a:hover
  {
      background-color:#fff;
  }
 
  .logout
  {
      cursor: pointer;
display: block;
float: right;
height: 30px;
margin-right: 20px;
margin-top: 3px;
  }

   </style>
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divTitleUrlHolder">
       <asp:Button runat="server" ID="btnLogOut" Text="log out" OnClick="btnLogOut_Click" CssClass="logout"/>
        <h1 class="pageTitle">public library</h1>
       <%-- <ul id="providers">
            <li><a href="PublicLibrary.aspx" class="active">youtube</a></li>
            <li><a href="PublicLibraryVimeo.aspx">vimeo</a></li>
            <li><a href="PublicLibraryDailymotion.aspx">dailymotion</a></li>
        </ul>--%>
    </div>
    <div id="content">
      
        <div id="importUrl">
            <!-- content -->
            <div class="inputHolder">
                <span class="spnTitle">Submit video URL link </span>
                <input type="text" id="txtURL" />
                <input type="button" id="btnSearchByURL" class="btn" value="GO" onclick="getYouTubeVideoByURL()" />
            </div>
            <%--    <span class="spnExplanation"> The videos should be imported one by one.</span>--%>
            <div id="divShowImage"></div>
            <div class="result" id="divResultURL">
            </div>
        </div>
        <div class="videoRoom">
            <asp:UpdateProgress ID="upProgress" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                        <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #FFFFFF; font-size: 36px; left: 40%; top: 40%;">please wait...</span>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Always" ChildrenAsTriggers="true">

                <ContentTemplate>
                    <div class="inputHolder">
                        
                        <span class="spnChooseCategory">Choose Category:</span>
                        <asp:DropDownList runat="server" ID="ddlCategory" CssClass="ddlCategory" AutoPostBack="true" onChange="ScrollerUp()" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSortVideo" runat="server" CssClass="ddlSortVideo" AutoPostBack="true" onChange="ScrollerUp()" OnSelectedIndexChanged="ddlSortVideo_SelectedIndexChanged">
                            <asp:ListItem Value="0">Sort By</asp:ListItem>
                            <asp:ListItem Value="1">newest</asp:ListItem>
                            <asp:ListItem Value="2">oldest</asp:ListItem>
                            <asp:ListItem Value="3">duration A-Z</asp:ListItem>
                            <asp:ListItem Value="4">duration Z-A</asp:ListItem>
                            <asp:ListItem Value="5">most viewed</asp:ListItem>

                        </asp:DropDownList>
                        <span class="spnChooseCategory">total videos:&nbsp</span>
                        <asp:Label runat="server" ID="lblVideoCount"></asp:Label>
                    </div>

                    <div class="resultHolder">
                       
                                <div runat="server" id="divResulSearchURL" ClientModeId="static" class="videoBoxHolder channelsHolder">
                               
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlSortVideo" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlCategory" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>


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

        </div>
    </div>
</asp:Content>
