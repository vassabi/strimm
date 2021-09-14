<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="StrimmTube.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
Search for Channels or Users | Strimm Online TV
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
<meta name="description" content="Strimm | Advanced search for users or  channels" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
 <link rel="canonical" href="https://www.strimm.com/advanced-search"/>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
 <%--<link href="css/search.css" rel="stylesheet" />
<link href="Plugins/Scroller/scroller.css" rel="stylesheet" />--%>
<%--<%: System.Web.Optimization.Styles.Render("~/bundles/search/css") %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/search/js") %>--%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%-- <script src="/JS/Controls.js"></script>
    <script src="/JS/Search.js"></script>--%>
    <style>


.televisionByPeople {
    border-bottom: 1px solid #ddd;
    max-width: 1520px;
}

#content {
background-color: transparent;
box-shadow: none;
float: left;
margin: auto;
/*max-width: 1320px;*/
min-height: 600px;
width: 100%;
margin-top: 30px;
}

.chanBoxcateg {
    color: #eee;
    font-size: 15px;
}

.videoBoxNew {
    border: 1px solid #eee;
    display: block;
    float: left;
    height: 183px;
    margin: 5px;
    overflow: hidden;
    position: relative;
    text-align: left;
    width: 300px;
    margin-left: 0;
    margin-right: 10px;
}


.videoBoxInfo {
    bottom: 0;
    cursor: pointer;
    display: block;
    height: 70px;
    position: absolute;
    width: 100%;
    background-image: url('/images/videoInfoBG.png');
    background-repeat: repeat-x;
}
.videoBoxtitle {
    color: #25caff;
    float: left;
    
    font-size: 13px;
    font-weight: normal;
    line-height: 20px;
    max-width: 95%;
    overflow: hidden;
    padding-left: 3px;
    text-overflow: ellipsis;
    white-space: nowrap;
    font-weight: normal;
    display: block;
        clear: both;
}

   .videoBoxInfo a {
    color: #1acc31;
    float: left;
    
    font-size: 13px;
    font-weight: bold;
    line-height: 20px;
    max-width: 95%;
    overflow: hidden;
    padding-left: 3px;
    text-overflow: ellipsis;
    white-space: nowrap;
    font-weight: normal;
    display: block;
    clear: both;
    margin-bottom: 5px;

        }
.actionsVideoBoxHolder {
    cursor: pointer;
    display: block;
    float: left;
    height: 50px;
    position: absolute;
    width: 100%;
    z-index: 2;
    opacity: 0.8;
}

.VideoBoxRemove {
    background-image: url("/images/trash.png");
    background-position: right;
    background-repeat: no-repeat;
    background-size: 20% auto;
    cursor: pointer;
    display: block;
    height: 40px;
    position: absolute;
    right: 1px;
    top: 0;
    width: 50%;
    z-index: 2;
}

.VideoBoxPlay {
    background-image: url("/images/playbuttonWhiteNew.png");
    background-position: left 5px;
    background-repeat: no-repeat;
    background-size: 25% auto;
    cursor: pointer;
    display: block;
    height: 50px;
    left: 0;
    position: absolute;
    top: 0;
    width: 50%;
    z-index: 2;
}

.VideoBoxPlay:hover {
    background-image: url('/images/playbuttonGreenNew.png');
    opacity: 1;
}


.divProviderLogo {
    bottom: 2px;
    height: 20px;
    position: absolute;
    right: 2px;
    width: 20px;
    z-index: 999;
}
.divProviderLogo img {
    width: 100%;
    height: 100%;
}

#divControlsHolder{
    margin-top: 50px;
    width: 100%;
    margin: auto;
    max-width: 1520px;
}

.chanBox {
    border: none;
    clear: right;
    display: inline-block;
    float: none;
    height: auto;
    margin: 0.5%;
    /*max-width: 15.666%;*/
    min-width: 15.666%;
    overflow: hidden;
    position: relative;
    vertical-align: top;
    /*width: 15.666%;*/
    /*height: 200px;*/
}
.addButtonContainer {
    background-repeat: no-repeat;
    background-size: 100% auto;
    display: block;
    height: 200px;
    position: absolute;
    width: 100%;
    top: -25px;
}
.videoBoxduration, .videoBoxviews, .videoBoxcateg {
    color: #ddd;
    float: left;
    font-size: 12px;
    line-height: 15px;
    padding-left: 3px;
    width: 100%;
}



        @media (max-width: 1280px) {
      
.videoBoxNew {
    height: 183px;
    width: 300px;
}
  }
        @media (max-width: 1520px) {
       
.videoBoxNew {
    height: 170px;
    width: 285px;
}
.addButtonContainer {
    height: 170px;
}
 }
        @media (max-width: 1700px) {
     
.videoBoxNew {
    width: 255px;
    height: 150px;
}
.addButtonContainer, .addButtonHolder {
    height: 150px;
}
   }


        @media (max-width: 1280px) {
       
.actionsVideoBoxHolder, .VideoBoxPlay, .VideoBoxRemove {
    height: 50px;
}
.actionsVideoBoxHolder, .VideoBoxPlay, .VideoBoxRemove {
    height: 50px;
}

.actionsVideoBoxHolder, .VideoBoxPlay, .VideoBoxRemove {
    height: 50px;
}
.addButtonContainer {
    height: 200px;
}
 }







    </style>
  <div class="mainContentWrapper">

      <div class="televisionByPeople">
            <h2 class="televisionByPeopleH2">Channels Created By People Like You</h2>
            <div class="homeActionsHolder" >
                 <div class="btnCreate" onclick="TriggerCreateChannel()"> create  your own channel</div>
                 <a class="watchVideoIcon200"></a>
    </div>
            <a class="whatsPlayingNowIconHN" href="home#entertainmentGroup"></a>
        </div>
    <div id="content">

        <div id="divTitleUrlHolder">
        </div>


       <%-- <div id="divSearchOptionHolder">
            <div id="searchHolderUp"></div>
            
            <div id="searchHolder">
             
                <div id="searchBoxHolder" class="searchBoxHolderLoad">
                    <input type="text" id="txtKeyword" placeholder="Enter channel name"/>
                <a id="ancSearch" onclick="GetDataByRadioChecked()" >search</a>
                    <div class="searchSoartingHolder">
              
                <input id="chkChannels" name="radioSelect" type="radio" onchange="SetPlaceholder()" checked="checked"  value="channels" />
                <label for="chkChannels">Channels</label>

                          <input id="chkUsers" name="radioSelect" onchange="SetPlaceholder()" type="radio"  value="user" />
                <label for="chkUsers">User's name</label>
                        </div>
                </div>
                


      
                <span id="lblMsg"></span>
            </div>
           
           
        </div>--%>

       <%-- <div id="sortHolder" runat="server" class="divSortHolder" visible="false">
            <span id="sortBy">sort by</span>

        </div>--%>
     
                <div id="divControlsHolder">
                </div>
           
        <uc:FeedBack runat="server" ID="feedBack" pageName="search" />
    </div>
            </div>
</asp:Content>
