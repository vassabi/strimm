<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Default_OLD.aspx.cs" Inherits="StrimmTube.Default1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
Strimm – Free Online Video Platform to Create Your Own TV Network
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
<meta name="description" content="Strimm is a free online video platform to create your own TV network or watch channels online." />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
 <link rel="canonical" href="https://www.strimm.com"/>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
 <%: System.Web.Optimization.Styles.Render("~/bundles/home/css") %>


    <style>
        #topPlaceHolder {display: none;}
    </style>

   <script src="/JS/swfobject.js" type="text/javascript"></script>
   <script src="//www.google.com/jsapi" type="text/javascript"></script>
   <script src="https://www.youtube.com/iframe_api"></script>
   <script type="text/javascript">
         google.load("swfobject", "2.1");
   </script>

   <script type="text/javascript">
        var username = "<%=UserName%>";
       <%-- 
        var webMethodGetCurrentlyPlayingChannelsForLandingPage = "/../WebServices/ChannelWebService.asmx/GetCurrentlyPlayingChannelsForLandingPage";
        function GetFeaturedChannels() {
            ajaxTopChannels = $.ajax({
                type: "POST",
                url: webMethodGetCurrentlyPlayingChannelsForLandingPage,
                data: '{"clientTime":' + "'" + globalClientTime + "'" + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (response) {
                    if (response.d) {
                        var data = response.d;
                        if (data) {
                            var maxCount = 11;

                            if (data.ChannelGroup) {
                                var echannels = Controls.BuildChannelsForLandingPageControl(data.ChannelGroup, maxCount, data.GroupName);
                                $("#channelGroup").html("").html(echannels);
                            }
                        }
                    }
                },
                error: function () {
                }
            });
        };--%>
        window.onload = function () {
            //GetFeaturedChannels();
            if (username) {
                $(".bottomSignUp").hide();
                $(".learnMoreHome").removeClass('learnMoreHome').addClass('learnMoreHomeAL');
            }
        };
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="/JS/Controls.js" type="text/javascript">


    </script>

   
<div id="homeslider">
    <div class="webHolder">
    <img class="homeSliderImg" src="/images/On-Air.jpg" />
    <div class="sliderCaptions">
    
<h5> Create Your Own TV Network</h5>
        <h6>It is free and fun!</h6>
        <a class="sliderActionBtn" onclick="ShowTutorialPlayer('MTTeo-3NMfk',false)"> watch video </a>
    </div>

</div>
    </div>

    <div class="whatsPlayingNow">
<h2 class="whatsPlayingNowH2">See what's playing on Strimm now!</h2>
        <a class="whatsPlayingNowIcon" href="home#entertainmentGroup"> </a>
    </div>

  


 <a name="entertainmentGroup"></a>
<div class="homeBlocksHolder">

<div id="channelGroup" class="entertainment">
    <h1 id="channelGroupTitle" class="video-box-h1"><%=ChannelGroupName%></h1>
    <div id="channelHolder" runat="server">

    </div>
 </div>
    <div class="spacer" style="height: 30px;"></div>  
</div>



 <div class="homeBlocksHolder">

<div class="ovpHomeBlack">
    <img class="fullPageImg" src="/images/homeNewBanners-02.jpg" />

<div class="BGHolder">
<h1 class="paragraphSloganStudio">TV Production Studio<br /> From the comfort of your chair</h1>
    <a class="learnMoreHomeTop learnMoreProduction center" href="/learn-more">Learn more</a>
    </div>
</div>

</div>


<%--<div class="whatsPlayingNow">
<h2 class="whatsPlayingNowH2">More!</h2>
        <a href="#" class="whatsPlayingNowIcon">&#10095; </a>
    </div>--%>



<div class="homeBlocksHolder">

<div class="cyonHome">


<%--<h2 class="titleHome"> Create Your Own TV Network</h2>--%>
 
<div class="BGHolderBG">
    <img class="fullPageImg" src="/images/blueBanner.jpg" />
       <div class="blueBG">
 <h1 class="paragraphSloganBlack plain"> Continuous broadcast from your own channel</h1>
           <div class="bottomButtonsHolder">
               <a class="bottomSignUp" href="/sign-up">sign up </a>
               <a class="learnMoreHome" href="/learn-more">Learn more</a>
               </div>
  </div>
    </div>
</div>
</div>






<div class="homeBlocksHolder">

 
</div>



</asp:Content>