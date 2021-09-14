<%@ Page Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="LearnMore.aspx.cs" Inherits="StrimmTube.LearnMore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Create Your Own TV Station | TV Creation Platform
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Strimm is Live TV Creation Platform to Create Your Own TV Station, Build Live TV Streaming or watch channels created by others.  Create Live TV Station Today!" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://strimm.com/learn-more" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="css/textPagesCSS.css" rel="stylesheet" />
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="//www.google.com/jsapi" type="text/javascript"></script>
    <script src="https://www.youtube.com/iframe_api"></script>
      <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    
    <script type="text/javascript">
        google.load("swfobject", "2.1");
        var isUserLogedIn = JSON.parse("<%=isUserLogedIn%>".toLowerCase());
       $(document).ready(function () {
           console.log(isUserLogedIn)
           if (isUserLogedIn == true) {
               $(".actionBtnHolder a.actionBtn").removeAttr("onclick").attr("onclick", "CreateChannel.RedirectToCreateChannel()");
           }
           else {
               $(".actionBtnHolder a.actionBtn").removeAttr("onclick").attr("onclick", "loginModal('create-channel')");
           }
       });
       //function ToggleTVGuideMobileMenu() {
       //    console.log("MOBILE MENU TRIGER");
       //    if ($(".subBlockTVGuide").is(":visible")) {
       //        $(".subBlockTVGuide").hide();
       //        $(".mobileNavHeader.tvGuide").removeClass("active");
       //    }
       //    else {
       //        $(".subBlockTVGuide").show();
       //        $(".mobileNavHeader.tvGuide").addClass("active");
       //    }

       //};
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<style>
    .pageHolder {
    max-width: 100%;
    width: 100%;
    padding: 0;
}

</style>

    <div class="learnMoreButtonHolder">
        <div class="whatIs">Become a Producer of Your Own TV Network</div>
        <div class="buttonsHolder">
            <a href="#" class="learmMoreWatchVideo" onclick="ShowTutorialPlayer('<%=promoVideoUrl %>', false)">Watch Video <i class="fa fa-play-circle" style="color:#fff;line-height: 40px;"></i></a>

            <%--  <a href="/Guides.aspx" class="learmMoreHowTo">how to</a>--%>

            <%--        <div class="buttonsContent">Watch this 2-min infomercial video to get quick introduction to Strimm.</div>
        <div class="buttonsContent">Visit "How To" page to get detailed information about major Strimm features and functionalities. </div>--%>
        </div>

    </div>

    <div class="pageHolder">








        <div class="platform">
            <div class="platformHead marginHead">What is Strimm?</div>
            <div class="platformImg leftfloat">
            </div>
            <div class="platformContent leftfloat">

                <p class="paragraphText">
                    Strimm is a FREE video platform, which gives everyone the opportunity to become a producer of their very own online TV channel. With Strimm, users can quickly and easily create a continuous 24/7 broadcast with content they choose from their own videos or their favorites from the web. Introducing an entirely new way to reach your audience, gain new fans, and produce a channel you love. Share your channel, connect with others, post messages, collaborate with talented producers and viewers, and watch something new every day. The possibilities are endless with Strimm.  

                </p>
            </div>


        </div>

        <div class="broadcast grey">
            <div class="contentHead">solution for businesses</div>
            <div class="businessImg rightfloat">
            </div>

            <div class="contentContent rightfloat">

                <p class="paragraphText">
                    <span class="whatIsSubTitle">Transform your daily visitors into fans. Add a TV channel to your website! </span>Strimm’s goal is to assist businesses and organizations in engaging first-time visitors that come to their site and turning them into repeat users.  Our platform empowers you to entertain, educate and engage your audience with your own custom 24/7 streaming channel. Doctors can create channels about wellness and healthy living; clothing boutiques can make fashion channels; restaurants of all kinds can make food and culture-related channels. There is virtually limitless possibility to find a niche-related topic.</br>

Strimm allows for easy channel embedding on any site. While all the behind the scenes scheduling work can be done directly on the Strimm platform, the channel can broadcast directly on your site. Promote your products and services to your customers by adding your own videos and commercials into the channel programming. </br>
    Entertain and engage your community and keep your visitors coming back.  
                </p>
           
            </div>
            <div class="actionButtonHolder">
                 <a class="actionButton">Get Started</a>
        </div>
        </div>

        <a name="create" id="create"></a>
        <div class="experience">
            <div class="platformHead">experience</div>
            <div class="experienceImg leftfloat">
            </div>
            <div class="platformContent leftfloat">
                <p class="paragraphText">
                    Want to know what makes Strimm the best online TV platform? The broadcast is made by people like you! Also, it is completely free for viewers and creators. From any device at any hour of the day, you can get inspired, be entertained, and enjoy streaming video shows.  There is always something on. 
You may also use our video-on-demand, "Watch Later", feature to watch any show of your choice at a later time. This is truly television on your own terms. Welcome to Strimm.
  
                </p>
            </div>

        </div>
        <a name="add" id="add"></a>
        <div class="createChannelLMore grey">
            <div class="contentHead margin300">create channel</div>
            <div class="createChannelLMoreImg rightfloat">
            </div>
            <div class="contentContent rightfloat">

                <p class="paragraphText">
                    You’ve dreamed of the day you’d have the chance to run your very own television broadcast. Well, here’s your chance. Strimm is a completely free service where you take the reigns on your very own channel. In just a few minutes, your new channel will be ready to go.  To begin, simply sign up for your free account, click “Create Channel”, and fill out a short form with your channel name, channel description and upload a familiar avatar for your fans to find you! 
            <%--    <a href="Guides.aspx" class="learnMoreDetails">Details</a>  --%>
                </p>
            </div>


        </div>
        <a name="broadcast" id="broadcast"></a>

        <div class="content">
            <div class="platformHead">add videos</div>
            <div class="contentImg leftfloat">
            </div>
            <div class="platformContent leftfloat">
                <p class="paragraphText">
                    Adding content and scheduling your broadcasts couldn't be easier. Say goodbye to long upload and download times. We have created a quick and simple way to resource a virtually unlimited selection of content from the top video providers on the web. In seconds, your channel will be populated with content of your choice. 
           <%--      <a href="Guides.aspx" class="learnMoreDetails">Details</a>--%>
                </p>
            </div>

        </div>




        <div class="broadcast grey">
            <div class="contentHead margin300">broadcast</div>
            <div class="broadcastImg rightfloat">
            </div>

            <div class="contentContent rightfloat">

                <p class="paragraphText">
                    Scheduling content for your channel is as easy as 1-2-3. Begin by going to your “Production Studio”. Then, choose a day and time to begin your broadcast before selecting videos you and your fans love. Your content can be original or borrowed. It’s completely up to you! Just remember, millions are watching and it’s your time to shine. After scheduling your broadcast, be sure to promote your channel on your favorite social media sites. Let the world know about you.  
Stay tuned for new features like live broadcasting and video conferencing from anywhere in the world on any device. We can’t wait to be a part of your success. 

                    <%-- <a href="Guides.aspx" class="learnMoreDetails">Details</a>--%>
                </p>
            </div>

        </div>




        <div class="bottomSignUpHolder">
            <a class="bottomSignUp" href="/sign-up">Sign Up. It's Free </a>
            <a id="ancCreateOrSignUpBottom" class="createChannelFooter" onclick="CreateChannel.RedirectToCreateChannel()" style="">create channel</a>
        </div>
        <%--  <div class="howToHolderLM">Visit <a href="Guides.aspx" class="howToGuides">how to</a> for platform details</div>--%>
        <div class="actionBtnHolder">
            <a class="actionBtn">create channel</a>
        </div>
    </div>
     <script async="async" src="https://www.youtube.com/iframe_api"></script>
</asp:Content>
