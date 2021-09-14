<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Default_OLD2.aspx.cs" Inherits="StrimmTube.DefaultNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Strimm – Free Online Video Platform to Create Your Own TV Network
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Strimm is a free online video platform to create your own TV network or watch channels online." />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://www.strimm.com" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <%: System.Web.Optimization.Styles.Render("~/bundles/Default_OLD2_CSS/css") %>



<%--    <link href="css/DefaultCSS.css" rel="stylesheet" />--%>
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="//www.google.com/jsapi" type="text/javascript"></script>
    <script src="https://www.youtube.com/iframe_api"></script>
    <script src="/JS/Main.js"></script>
    <script type="text/javascript">
        google.load("swfobject", "2.1");
    </script>

    <script type="text/javascript">
        var username = "<%=UserName%>";
        var hideFooter = "<%=HideOldFooter%>";

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
                $(".bottomSignUpNH").hide();
                $(".learnMoreHomeNH").removeClass('learnMoreHomeNH').addClass('learnMoreHomeAL');
            }

            if (hideFooter == "True") {
                $('#divFooter').hide();
            }
        };

      
    </script>
    <style>
        .video-box-h1 {
           color: black;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="/JS/Controls.js" type="text/javascript">


    </script>


    <div id="homesliderHN">
        <div class="logoTV">

        </div>
        <div class="slogan">
Television Created By People Like You
        </div>

        <div class="channels">
            <div class="watchNowHolder">
            <div class="channelsFreeTvChannels"> 700+ Free TV Channels</div>
            <div class="channelsWhatchNow"> Watch Now</div>
                </div>
        </div>
        <div class="roller">
            <div class="rollerBlock rollerBlockFirst social">Social</div>
            <div class="rollerBlock whatISStrimm">What is Strimm</div>
            <div class="rollerBlock channelGallery">Channel Gallery</div>
            <div class="rollerBlock gotaBusiness">Got a business?</div>
            <div class="rollerBlock createChannel">Create Channel</div>
             <div class="rollerBlock whatchNow">Watch Now</div>
        </div>
        <div class="sideNavigation">
            <div class="sideNavigationBlock">My Network</div>
            <div class="sideNavigationBlock">Priduction Studio</div>
            <div class="sideNavigationBlock">Favorite Channels</div>
            <div class="sideNavigationBlock">Whatch Later</div>
            <div class="sideNavigationBlock sideNavigationBlockGuide">TV Guide</div>
            <div class="sideNavigationBlock">Video Room</div>
            <div class="sideNavigationBlock">Video Store </div>
            <div class="sideNavigationBlock">How To</div>
            <div class="sideNavigationBlock sideNavigationBlockNull"></div>
            <div class="sideNavigationBlock sideNavigationBlockNull"></div>
 
         
        </div>

     
    </div>



    <div class="channelsHolderO_2">
            <a class="whatsPlayingNowIconHN" href="home#entertainmentGroup"></a>
    <div class="channelsBG">
        <div class="whatsPlayingNowHN">
            <h2 class="whatsPlayingNowH2">See what's playing on Strimm now!</h2>
        
        </div>




        <a name="entertainmentGroup"></a>
        <div class="homeBlocksHolder" id="homeBlocksHolder" runat="server">
            <a href="browse-channel?category=Most%20Popular" class="seeAllChannels"> see all channels</a>
            <%--  <div id="channelGroupTop" class="entertainment">
                <h1 id="channelGroupTopTitle" class="video-box-h1">TOP Channels</h1>
                <div id="channelHolderTop" runat="server">
                </div>
            </div>--%>
            <%--  REPLACED BY UC CHANNELSHOLDERCATEGORYUC--%>             
        </div>            
        <div class="spacer" style="height: 30px;"></div>
    </div>
    </div>



<div class="embeddedView">
      <h2 class="emneddedTextH2">Solution for Businesses and Organizations  </h2> 
    <div class="emneddedImg"></div>
     <div class="emneddedText">
<%--  <h2 class="emneddedTextH2">Solution for Businesses and Organizations  </h2>  --%>   
<p>Strimm helps businesses and organizations of all kinds to have visitors coming back to their site. Entertain and engage your audience in the new and modern way with your own 24/7 broadcasting TV channel. </p>

<p>Strimm allows easy channel embedding on any site. While all backend work can be done on Strimm platform, the channel can be embedded and broadcasted directly on your site. Promote your products and services to your customers by adding your video commercials in the programming. </p>

<p>Schedule video training for your employees on certain day and time or simply entertain your community and keep your visitors coming back to you.</p>
     </div>
</div>






    <div class="homeBlocksHolder">
        <div id="divFooterNH" class="default regular">

            <h1 class="paragraphSloganBlackNH plainNH">Continuous broadcast from your own channel</h1>
            <div class="bottomButtonsHolderNH">
                <a class="bottomSignUpNH" href="/sign-up">Sign Up. It's Free </a>
                <a class="learnMoreHomeNH" href="/learn-more">Learn More</a>
            </div>

            <div class="holderNH">

                <div class="column columnHN">
                    <a href="Default.aspx" class="logoFooterLInk">
                        <div class="logoFooter"></div>
                    </a>

                    <div class="column columnHN columnFooterSocial">

                        <ul class="footerSocialHolder">
                            <li class="footerSocial footerSocialFacebook"><a href="https://www.facebook.com/strimmTV" target="_blank"></a></li>
                            <li class="footerSocial footerSocialTwitter"><a href="https://twitter.com/strimmtv" target="_blank"></a></li>
                            <li class="footerSocial footerSocialGoogle"><a href="https://plus.google.com/+StrimmTV/posts" target="_blank"></a></li>
                            <li class="footerSocial footerSocialPinterest"><a href="https://pinterest.com/strimmTV" target="_blank"></a></li>
                        </ul>
                    </div>
                </div>

                <div class="column columnHN">
                    <h3>About Us</h3>
                    <ul>
                        <li><a href="/company">Company</a></li>
                        <li><a href="/press-release">Press</a></li>
                        <li><a href="/contact-us">Contact</a></li>
                    </ul>
                </div>


                <div class="column columnHN">
                    <h3>How It Works</h3>
                    <ul>
                        <li><a href="/learn-more">Become a Producer</a></li>
                        <li><a href="/faq">FAQ</a></li>
                        <li><a href="/guides">How To</a></li>
                    </ul>
                </div>









                <div class="column columnHN">
                    <h3>Legal</h3>
                    <ul>
                        <li><a href="/copyright">Copyright Policy</a></li>
                        <li><a href="/privacy-policy">Privacy Policy</a></li>
                        <li><a href="/terms-of-use">Terms of Use</a></li>

                    </ul>
                </div>

                <div id="divAllRights">
                    <span>&#169;2015-2016 Strimm, Inc. |  All Rights reserved </span>
                    <%--  <a href="Copyrights.aspx" class="copyrights"> / &nbsp; copyright policy</a>--%>
                </div>
            </div>
        </div>

    </div>



</asp:Content>
