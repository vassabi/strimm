<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="businessPage_OLD.aspx.cs" Inherits="StrimmTube.businessPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
   Create TV Channel & Broadcast On Website | Strimm Online TV Network
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Use Strimm to Create a Private TV Channel Online and Add it to a Website for a 24/7 Broadcast in Real Time. Boost Your Business and Expand your Reach" />
    <meta name="robots" content="FOLLOW, INDEX, NOODP, NOYDIR" />
    <meta name="GOOGLEBOT" content="FOLLOW, INDEX, NOODP, NOYDIR" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://www.strimm.com/company.aspx" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="//www.google.com/jsapi" type="text/javascript"></script>
    <script src="https://www.youtube.com/iframe_api"></script>
    <script src="/JS/Froogaloop.js"></script>
    <script type="text/javascript">
        google.load("swfobject", "2.1");
    </script>
    <link href="css/BusinessPageCSS.css" rel="stylesheet" />
    <script src="/JS/Business.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        var busManager = window.getManager();
        $(document).ready(function () {
            var iframeSrcForBuisnessPage = "";
            var protocol = window.location.protocol;
            if (protocol == "https:")
            {
                iframeSrcForBuisnessPage = 'https://<%=DomainName%>/embedded/TVWorld/BusinessWorld?embed=true&account=77363TDK';
                
                $("#embeddedDiv iframe").removeAttr("src").attr("src", iframeSrcForBuisnessPage);
            }
            else {
                iframeSrcForBuisnessPage = 'https://<%=DomainName%>/embedded/TVWorld/BusinessWorld?embed=true&account=77363TDK';

                $("#embeddedDiv iframe").removeAttr("src").attr("src", iframeSrcForBuisnessPage);
            }
          
            console.log("PROTOCOL: "+location.protocol);   
        });
    </script>

        <style>
        .businessNavigationA{
    color: #FF5722;
    text-decoration: underline;
}
    </style>

    <div class="companyHolder">
        <%--         <h1 class="h1businessPage">Solution for Businesses and Organizations
        </h1>--%>

        <div class="ancBoxesHolder">
            <a class="ancBox ancBoxWhyStrimm" href="#whyBlock">
                <img src="images/businessIcons/whyStrimm.png" />
                <span class="ancBoxText">Why Strimm</span></a>

            <a class="ancBox ancBoxExperience" href="#experienceBlock">
                <img src="images/businessIcons/experience.png" />
                <span class="ancBoxText">Experience</span></a>

            <a class="ancBox ancBoxBenefits" href="#benefitsBlock">
                <img src="images/businessIcons/benefits.png" />
                <span class="ancBoxText">Features & Benefits</span></a>

            <a class="ancBox ancBoxPackages" href="#pricingBlock">
                <img src="images/businessIcons/packages.png" />
                <span class="ancBoxText">Packages & Pricing</span></a>

        </div>
    </div>


    <div id="divBoardContent">
        <div class="businessTopImgHoder">
            <h1 class="businessPageTitle">Add An Interactive TV Channel</br> To Your Website</h1>
        </div>
        <div class="businessBlock businessBlockWhite">
            <div class="maxBlockHolder">
             
            <div class="businessImageBlock  businessImageBlock01">

             <%--   <img src="images/businessImages/05.jpg" />--%>
            </div>
            <div class="businessTXTBlock">
               <%-- <h2>Add An Interactive TV Channel</br> To Your Website</h2>--%>

                <div class="pageSumHolder">
                    <div class="pageSum">
                        <img src="images/businessIcons/create.png" />
                        <span class="pageSumTitle">Create Your Own TV </span>
                        <span class="pageSumText">In just a few steps you can create a TV channel and operate a broadcast to any device, anywhere in the world.</span>
                    </div>
                    <div class="pageSum">
                        <img src="images/businessIcons/embed.png" />
                        <span class="pageSumTitle">Embed Your TV Channel </span>
                        <span class="pageSumText">Embed your channel on your website. The broadcast will appear on both your site and Strimm TV.</span>
                    </div>
                    <div class="pageSum">
                        <img src="images/businessIcons/grow.png" />
                        <span class="pageSumTitle">Grow Your Audience  </span>
                        <a name="whyBlock"></a>
                        <span class="pageSumText">Your channel will entertain, educate and engage your audience and will have them coming back.
</span>
                    </div>
                </div>
                    <div class="getStartedHolder">
                <a href="/create-tv/pricing" class="getStartedHome">get started!</a>
                    </div>
            </div>
        </div>

        </div>

        <div id="divContentWrapper">


            <div class="businessBlock businessBlockf5 businessBlockf5Height">
                 <div class="maxBlockHolder">
                   <div class="qestanswHolder">  
                    <h2 class="marg20">What can Strimm TV do for me? </h2>
                    <%--   <span class="strimmBenefP">Anything you want!</span>--%>
                      </div>
                <div class="businessImageBlock businessImageBlockRight businessImageBlockAppotun">

                    <img class="businessImagesSml" src="images/businessImages/02.png" />
                </div>
                <div class="businessTXTBlock businessTXTBlock50">
               
                   <p> Strimm can help your business or organization to educate and engage your online visitors and to keep them coming back.  </p>
                         <p>  Our platform equips you with a set of cutting-edge tools to create your own network of TV channels which can broadcast around the world, 24 hours a day, with unique daily programming. It is easier and more effective that creating daily blogs or constantly adding new videos to keep your site fresh. The video content you choose can be easily found directly on Strimm’s platform. </p>
                         <p>  To promote your business or organization, use your own video content and commercials. Create your own TV channel, embed it on your website and bring your business or organization to a whole new level.  Engage and grow your audience.</p>
                </div>
            </div>
                </div>
            <div class="businessBlock businessBlockWhite">
                 <div class="maxBlockHolder">
                    <div class="qestanswHolder">  
                    <h2 class="marg20">Who can benefit from Strimm TV?</h2>
                 <%--   <span class="strimmBenefP">Any business or organization can benefit from Strimm!
                   </span>--%>
                          </div>
                <div class="businessImageBlock businessImageBlockTarget">

               
                </div>
                <div class="businessTXTBlock  businessTXTBlockLong">
                  
                    <ol>

                       <li><a class="liLinkRO" href="/religion"><span class="subtitlestrongBisiness">•	 Religious Organizations</span></a>

Do you have a parish, ministry or a synagogue? What about a religious school? Take advantage of the new opportunities digital video era provides. Create a 24/7 broadcasting TV channel. Stream your educational material, events and ceremonies on a certain day and time. Add a TV channel to your website and let your community watch it anywhere and on any device. Expand your reach. Start it today!
                           <a href="/religion" class="religiousDetails">details</a></li>

                        <li><span class="subtitlestrongBisiness">•	Media Organizations</span>
                            TV/Video Production Studios, Newspapers and Magazines, Local TV stations and Video Sites!  You have created amazing content, don’t let it be forgotten.  Entertain your audience further with your own TV channel. You can create a 24/7 broadcasting entertainment or news channels with scheduled content, available from anywhere in the world, on any device!</li>

                        <li><span class="subtitlestrongBisiness">•	Powerful YouTubers </span>
                            You’ve done a great job on YouTube. Now, it is time to benefit further from your hard work. Create a single TV channel that will broadcast all of your videos with custom daily programming.  Link all your videos to your TV channel on Strimm in bulk. The linking process can be done in less than one minute, right from your Production Studio. Reap the benefits of your own hard work!</li>

                        <li><span class="subtitlestrongBisiness">•	Local Governments</span>
                            Your community is now online!  Give them a place to unite and enjoy with your own online TV channel, which can be watched anytime and from anywhere.  Show the community’s latest news and past events or simply educate them with different courses, available for villagers only.</li>

                        <li><span class="subtitlestrongBisiness">•	Educational Institutions </span>
                            Educate and entertain your students with training material or content related to the classes you offer or to your school.</li>
                        <a name="experienceBlock"></a>
                        <li><span class="subtitlestrongBisiness">•	Retailers, Manufacturers and Service Providers </span>
                            Start a TV channel related to your products or services or the market you are in. Educate your consumers and turn them into your fans!</li>

                    </ol>


                    <div class="backtotop">
                        <a href="/">back to top </a>
                    </div>
                </div>

            </div>
</div>
            <div class="businessBlock businessBlockf5 ">
                 <div class="maxBlockHolder">
                <div class="qestanswHolder">  <h2> Do you want to improve your visitor's experience?</h2>
                  <%--   <span class="strimmBenefP"> Add a TV channel to your site!</span>--%></div>
              
                <div class="businessIMG">
                    <img src="/images/embedCode.jpg" />
                </div>
                <p>
                    Your TV channel is designed to be easily embedded on any page of your website. Just subscribe to one of the packages from the options below, and start reaching your target audience with your own interactive TV channel. To schedule programming for your channel, proceed to your “Production Studio” page. There you can add video content, create a daily schedule and publish it for a continuous broadcast on your website and on Strimm.
      
                </p>
                <%--<div class="businessTutorial">
        <p> Watch this tutorial video to learn about scheduling or visit our How To page: </p>
            <div class="btnWatchVideoBusiness">
            <a onclick="ShowTutorialPlayer('<%=StudioTutorialVideoId%>', false)" class="ancHowTo"> watch tutorial </a>
            </div>
                </div>--%>



                <p class="subtitlestrongBisiness">Example of embedded channel</p>


                     <div id="embeddedDiv" style="position: relative; padding-bottom: 60%; height: 0;">
                         <meta name="viewport" content="width=device-width" />

<iframe data-frameborder='0' data-scrolling='no' allowfullscreen src='https://<%=DomainName%>/embedded/Robert/FashionWorld?accountNumber=77363TDK' style='position: absolute; top: 0; left: 0; width: 100%; height: 100%;'></iframe>

                     </div>



                <a name="benefitsBlock" class="ancBenefitsBlock"></a>
           <%--     <div class="ancDivider"></div>--%>

                </div>
                </div>
               <div class="businessBlock businessBlockWhite">
                    <div class="maxBlockHolder">
                <h2>What are you waiting for?</h2>
                <div class="getStartedHolder">
                <a href="/create-tv/pricing" class="getStartedHome">get started!</a>
                    </div>
                <div class="backtotop">
                    <a href="/">back to top </a>
                </div>
            </div>
            </div>

           
        </div>
    </div>
</asp:Content>

