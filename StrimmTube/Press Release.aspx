<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Press Release.aspx.cs" Inherits="StrimmTube.Press_Release" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Press | Strimm Public TV Video Platform
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
<meta name="description" content="Find Strimm press releases here"/>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
 <link rel="canonical" href="https://www.strimm.com/Press Release.aspx"/>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="css/PressCSS.css" rel="stylesheet" />
    <script>
        
        function ToggleConsolePR()
        {
            if($(".pressBlock .PRBlock").is(":visible"))
            {
                $(".pressBlock .PRBlock").hide();
                $(".pressBlock .consolPR").removeClass("active");
            }
            else
            {
                $(".pressBlock .PRBlock").show();
                $(".pressBlock .consolPR").addClass("active");
            }

        }
        function ToggleMediaBlock()
        {
            if ($(".mediaBlock .PRBlock").is(":visible")) {
                $(".mediaBlock .PRBlock").hide();
                $(".mediaBlock .consolPR").removeClass("active");
            }
            else {
                $(".mediaBlock .PRBlock").show();
                $(".mediaBlock .consolPR").addClass("active");
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




        <div class="learnMoreButtonHolder">
    <h1 class="PRTitle">Press</h1>


</div>

    <div id="divBoardContent">
         <div id="divContentWrapper">
            <p class="introP">Strimm TV is constantly innovating and moving forward with its online video platform.</br> 
                Read on to learn about the developments of that we have shared with the media and the latest news about Strimm.</p> 


<div class="pressBlock">
<div class="consolPR" onclick="ToggleConsolePR()">
   <h2 class="PRBlockTitle">Press Release</h2> 
</div>

    <%--october 2016--%>
             <div class="PRBlock">
                     <div class="PRIMG">
           <img src="images/home/PRImg/october-11-2016.jpg" />
            </div>
                 <div class="prInfo">
             <a class="PRDate">October 11, 2016</a>
    <a href="/press/new-type-of-customer-outreach-offered-by-social-internet-television-strimm-tv" class="PRH2">
        New Type of Customer Outreach Offered by Social Internet Television - Strimm TV
    </a>
                 </div>
</div>


    <%--august--%>
             <div class="PRBlock">
                     <div class="PRIMG">
           <img src="images/home/PRImg/august-02-2016.jpg" />
            </div>
                 <div class="prInfo">
             <a class="PRDate">August 2, 2016</a>
    <a href="/press/strimm-tv-emerging-leader-in-internet-latest-new-frontier-social-internet-television" class="PRH2">
   Strimm TV Emerging Leader in Internet's Latest New Frontier: Social Television </a>
                 </div>
</div>

    <%--february--%>
             <div class="PRBlock">
                     <div class="PRIMG">
                           
                           <img src="images/home/PRImg/february-02-16.png" />  
            </div>
                 <div class="prInfo">
             <a class="PRDate">March 16, 2016</a>
    <a href="/press/strimm-tv-announces-new-video-providers-for-its-growing-public-television-platform" class="PRH2">
   Strimm TV Announces New Video Providers for Its Growing Public Television Platform </a>
                 </div>
</div>

    <%-- october 2015--%>
             <div class="PRBlock">

        <div class="PRIMG">
          
       
              <img src="images/home/PRImg/october-20-15.png" />
            </div>
                   <div class="prInfo">
             <a class="PRDate">October 27, 2015</a>
    <a href="/press/strimm-inc-introduces-new-game-changing-online-video-platform" class="PRH2">Strimm, Inc. Introduces New Game-Changing Online Video Platform </a>

</div>

       </div> 
    </div> 

<div class="mediaBlock">
    <div class="consolPR" onclick="ToggleMediaBlock()">
    <h2 class="PRBlockTitle">Media</h2>
</div>
    <div class="PRBlock">


        <div class="PRIMG mediaImg">
            <img src="images/home/PRImg/november-12-2016.jpg" /></div>

        <div class="prInfo">
            <a class="PRDate">November 12, 2016</a>
            <a href=" https://chicago.suntimes.com/news/sunday-sitdown-this-man-wants-you-to-create-your-own-tv-channel/ " target="_blank" class="PRH2"> Sunday Sitdown: This man wants you to create your own TV channel</a>


        </div>
    </div>

    <div class="PRBlock">


        <div class="PRIMG mediaImg">
            <img src="images/home/PRImg/august-3-2016.jpg" /></div>

        <div class="prInfo">
            <a class="PRDate">August 03, 2016</a>
            <a href=" https://www.rapidtvnews.com/2016080343849/strimm-cuts-loose-in-social-tv.html " target="_blank" class="PRH2">Strimm Cuts Loose in Social TV</a>


        </div>
    </div>
    <div class="PRBlock">


        <div class="PRIMG mediaImg">


            <img src="images/home/PRImg/march-28-16.png" />
        </div>
        <div class="prInfo">
            <a class="PRDate">March 28, 2016</a>
            <a href="https://reklamaconnect.com/Post/max-stolyarov-revolutionizing-the-entertainment-industry-by-alice-smelyansky " target="_blank" class="PRH2">Max Stolyarov: Revolutionizing the entertainment industry By Alice Smelyansky</a>


        </div>
    </div>
    <div class="PRBlock">


        <div class="PRIMG mediaImg">

            <img src="images/home/PRImg/march-17-16.png" />
        </div>
        <div class="prInfo">
            <a class="PRDate">March 17, 2016</a>
            <a href="https://advisor.tv/next-game-changer/s2e9-max-stolyarov-co-founder-of-strimm " target="_blank" class="PRH2">Next Game Changer - Interview on Advisor.TV</a>

        </div>
    </div>
    <div class="PRBlock">


        <div class="PRIMG mediaImg">


            <img src="images/home/PRImg/february-15-16.png" />
        </div>
        <div class="prInfo">
            <a class="PRDate">February 15, 2016</a>
            <a href="//www.inc.com/adam-fridman/3-effects-of-customers-embracing-an-on-demand-economy.html" target="_blank" class="PRH2">3 Effects Of Customers Embracing an On-Demand Economy</a>


        </div>
    </div>


    <%--   <div class="PRBlock"></div>--%>

</div>
                   
    </div>
        </div>


</asp:Content>
