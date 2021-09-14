<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="BrowseChannel.aspx.cs" Inherits="StrimmTube.BrowseChannel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
Strimm | Browse Online TV Channels
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
<meta name="description" content="Select a channel to watch from this  category of online TV channels"/>
   </asp:Content> 

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--     <%: System.Web.Optimization.Styles.Render("~/bundles/channelpage/css") %>--%>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/browseChannel/js") %>
    <link href="css/BrowseChannels.css" rel="stylesheet" />
   <%-- <script src="/JS/BrowseChannels.js" type="text/javascript"></script>
    <script src="/JS/Controls.js" type="text/javascript"></script>
    <link href="css/ChannelPageCSS.css" rel="stylesheet" />--%>
        <%--<div id="loadingDivBrowse" class="loadingDivHolder">
            <img src="/images/ajax-loader(3).gif"/>
        </div>--%>


<%--    <div class="browseChannelsViewBCh">
        <div class="ChannelInfoBCh">
            <div class="channelNameBCh">channel<span> Fasion World</span></div>
            <div class="channelCodeBCh">channel code<span> FS1002</span></div>
            <div class="nowPlayingBCh">now plating<span>Ralph Lauren: How I Built a Fashion Empire</span></div>
            <div class="channelDescriptionBCh">
                description<span>This episode of Game Changers spotlights the career of fashion designer Ralph Lauren. Explore his meteoric rise, his personal and professional set-backs, as well as his successes as an ambassador for America.
                </span>
            </div>
        </div>
        <div class="VideoInfoBCh">
            <div class="videoBCh"></div>
            <div class="durationBCh">
                <div class="durationFullBCh"><span>1h 55min 16sec</span></div>
                <div class="durationTimelBCh"><span>7.55pm - 8.30pm</span></div>
            </div>

            <div class="actionsBCh">
                <div class="watchBCh"><span>watch channel</span></div>
                <div class="recordlBCh"><span class="ledBCh"></span><span>rec</span></div>
            </div>
        </div>


    </div>

    <div class="browseChannelsDublicate"></div>--%>
        <div class="mainContentWrapper">

           <div class="televisionByPeople">
            <h2 class="televisionByPeopleH2">Channels Created By People Like You</h2>
            <div class="homeActionsHolder" >
                 <div class="btnCreate" onclick="TriggerCreateChannel()"> create  your own channel</div>
                 <a class="watchVideoIcon200"></a>
    </div>
            <a class="whatsPlayingNowIconHN" href="home#entertainmentGroup"></a>
        </div>



    
    <div id="content" class="browseChannels">
       
                <div class="pageInfoHolder" >
            <h1 id="lblCategoryName">category name</h1>
            <span id="lblChannelCount">0</span>

                                            

       
                    <select id="ddlSortChannels" class="ddlSortChannels" onchange="sortChannelBoxes(this)">
                        <option value="3">Most Viewed</option>                        
                        <option value="5">Oldest First</option>
                        <option value="6">Newest First</option>
                        <option value="4">Most Subscribed</option>

                    </select>
                         <select id="ddlLang" onchange="GetChannelsBySelectedLanguage(this)"> </select>
        </div>
       
                <div class="inputHolder" id="inputHolder" runat="server">
                  
                    
                </div>

                <div class="left">
                    
                        <div class="content">

                            <div runat="server" id="channelsHolder" class="channelHolder">
                            </div>
         <asp:Label runat="server" ID="lblmessage" class="lblDefaultMessage" ClientIDMode="Static"></asp:Label>
                      
                    </div>
                </div>
           

  <uc:FeedBack runat="server" ID="feedBack" pageName="browse channel" />

       </div>
            </div>

</asp:Content>
