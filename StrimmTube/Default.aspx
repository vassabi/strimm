<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StrimmTube.forBusinesses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
  Create Online TV Channel | Watch Live TV Online Free | Internet TV 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Create TV Online, create online TV channel, watch live TV shows online free. Get a Strimm web TV player to make and broadcast your own Internet TV." />
    <meta name="robots" content="FOLLOW, INDEX, NOODP, NOYDIR" />
    <meta name="GOOGLEBOT" content="FOLLOW, INDEX, NOODP, NOYDIR" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://strimm.com" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="//www.google.com/jsapi" type="text/javascript"></script>
  
    <%--<script src="/JS/Froogaloop.js"></script>--%>
    <script type="text/javascript">
        google.load("swfobject", "2.1");
    </script>
    <link href="css/BusinessPageCSS.css" rel="stylesheet" />
    <link href="css/DefaultCSS_2_21_16.css" rel="stylesheet" />
    <script src="/JS/Business.js" type="text/javascript"></script>
    <script src="https://www.youtube.com/iframe_api"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


  <%--  <script>
        $(document).ready(function () {
            var iframeSrcForBuisnessPage = "";
            var protocol = window.location.protocol;
            if (protocol == "https:") {
                iframeSrcForBuisnessPage = 'https://<%=DomainName%>/embedded/TVWorld/BusinessWorld?embed=true&account=77363TDK';
                                            

                $("#embeddedDiv iframe").removeAttr("src").attr("src", iframeSrcForBuisnessPage);
            }
            else {
                iframeSrcForBuisnessPage = 'https://<%=DomainName%>/embedded/TVWorld/BusinessWorld?embed=true&account=77363TDK';

                $("#embeddedDiv iframe").removeAttr("src").attr("src", iframeSrcForBuisnessPage);
            }

            console.log("PROTOCOL: " + location.protocol);
        });
    </script>--%>

    <style>
        .businessNavigationA {
            text-decoration: underline;
        }
    </style>



    <div class="BPblock intro">
        <div class="introTextHolder">
            <div class="mainHHolder">
                <span class="istrimm"></span>
                <span class="myOwnTVChannel"></span>
                <h1 class="createYours">Create Your TV Channel. Tell Your Story.</h1>
            </div>
            <a href="/create-tv/pricing" class="startStrimmingBTN">
                <img src="images/Start-Srtimming-Today-BUTTON.svg" />
            </a>
            <a href="#" class="commercialVideoWrapper" onclick="ShowTutorialPlayer('OSZcAe4K5X0', false);">
                watch 2-min video
            </a>
        </div>
        <div class="stepsIntro">

            <div class="stepsIntroBlock">
                <div class="stepsIntroTitle">Create</div>
                <div class="stepsIntroP">
                    Create a channel on Strimm<br />
                    and  schedule a broadcast.
                </div>
            </div>
            <div class="stepsIntroBlock">
                <div class="stepsIntroTitle">Embed</div>
                <div class="stepsIntroP">
                    Embed TV channel on your website,
                    <br />
                    ensuring unique and intriguing content on every visit.

                </div>
            </div>
            <div class="stepsIntroBlock">
                <div class="stepsIntroTitle">Grow</div>
                <div class="stepsIntroP">
                    Grow your community like never before.<br />
                    Stay connected. Increase loyalty.
                </div>
            </div>
           
        </div>

    </div>
    

    <div class="BPblock embeddedIntro">
            <a class="embeddedAnc" name="embedded"></a>

        <div class="innerHolder1">
            <div class="textMain textMainFull">
                Your Own Built-in Guide
            </div>
            <div class="textSecondary">
                All of your channels and schedule in <strong style="color: #fff;">one place</strong>
            </div>
            <div class="textP embeddedBlk">
               Strimm provides you with a unique opportunity to create as many channels as you need.  Have your own TV Network! Our built-in GUIDE will showcase all of them in a single screen along with their daily schedules, just like a cable TV guide. Your viewers can easily switch between channels without leaving the page. 
            </div>
            <a href="/create-tv/features-and-benefits"  class="actionButton">Watch Live Demo</a>
        </div>
        <a href="create-tv/features-and-benefits" class="innerHolder2">
       
        </a>
        <%--<a href="create-tv/features-and-benefits" class="clickHereExample">Click the image for a preview! </a>--%>
    </div>
    <div class="BPblock billG">
        <div class="signatureText">
            <span class="qtMark">&ldquo; </span>Internet TV and the move to the digital approach is quite revolutionary. TV has historically has been a broadcast medium with everybody picking from a very finite number of channels. <span class="qtMark">&rdquo;</span>
            <span class="signature">Bill Gates</span>
        </div>
    </div>
    <div class="BPblock whatIsStrimmBP">
        <div class="introTextHolder">
            <div class="textMain">
                What is Strimm?
            </div>
            <div class="textP  textPWhatisStrimm">
               <p class="textPInner"> Strimm (pronounced: “stream”) is a TV AS A SERVICE... think of it as an INBOUND MARKETING TOOL for your business. It is a live TV creation, scheduling and broadcasting platform. It provides any individual or organization with easy-to-use tools to create linear TV channels with scheduled shows, which can broadcast in real time on the creator's own website and on Strimm.</p>
               <p class="textPInner"> Watch TV online for free! Strimm also provides viewers with a 24/7 broadcast of thousands of live TV channels, created by people from around the world. We stream TV to more than 160 countries! </p>
            </div>
            <a href="learn-more" class="actionButton">Learn More </a>
        </div>
    </div>
    <div class="BPblock whyStrimm">
        <div class="introTextHolder">
            <div class="textMain colorBlack">
                Why Strimm?
            </div>
            <div class="textP colorBlack textPWhyStrimm">
                Strimm provides an exceptional combination of linear TV features, ease-of-use and affordability, which cannot be found anywhere else. Having a fresh, pre-scheduled and valuable 24/7 broadcast on your website gives your visitors a great reason to come back. Join our community of thousands of TV producers! 
            </div>
            <a href="/create-tv/pricing" class="actionButton">Get Started</a>
        </div>
    </div>
    <div class="BPblock testimonials">
        <div class="testimonialsBlock">
            <div class="testimonialsP">
                “You guys are at the top of your game. This is such a great service.  I am able to realize a BIG dream. And that is to produce my own shows and broadcast them. television network.
                I never thought I would OWN a television network.”
               <div class="testInfoHolder">
                   <span class="testimonialsName">Reginald Smith</span>
                   <span class="testimonialsLink">ForLifeTV.com</span>
               </div>
            </div>
        </div>
        <div class="testimonialsBlock">
            <div class="testimonialsP">
                “Strimm has given us a creative outlet to allow us to be able to control our programming effectively and easily.<br /> By adding this service onto our site, we have found our traffic to increase dramatically.”
              <div class="testInfoHolder BeJelly">
                  <span class="testimonialsName">Brian A. Metcalf, CEO | Founder</span>
                  <span class="testimonialsLink">Be-Jelly.com</span>
              </div>

            </div>
        </div>
        <div class="testimonialsBlock">
            <div class="testimonialsP">
                “We are excited about the benefits in our association with Strimm. It not only helps to deliver wide exposure of our online video channels but also creates the possibilities of expanding our network offerings.”
                <div class="testInfoHolder DreamCatcher">
                    <span class="testimonialsName">Tony Angelo Taliaferro, President | CEO</span>
                    <span class="testimonialsLink">DreamCatcherMultimedia.company </span>
                </div>
            </div>
        </div>

    </div>
    <div class="BPblock hubspot">
        <div class="hubspotMain">The Highly Efficient Inbound Marketing Strategy</div>
        <div class="signatureText hubspot">
            <span class="qtMark">&ldquo; </span>Since 2006, inbound marketing has been the most effective marketing method for doing business online. Instead of buying ads, inbound marketing focuses on creating quality content that pulls people toward your company, where they naturally want to be. <span class="qtMark">&rdquo;</span>
        </div>
        <div class="signatureLOGO"></div>
    </div>
    <div class="BPblock sky">

        <div class="textMain sky">
            See Where Strimm Takes You 
        </div>
           <div class="textP textPSky">
        Subscribe to any of our affordable plans and move your organization to a whole new level!</div>
        <a href="/create-tv/pricing" class="actionButton sky">Start Free Trial</a>
        <a name="FAQ"></a>
    </div>
    <div class="BPblock FAQ">

        <div class="textMain FAQ">FAQ</div>
        <div class="FAQQuestion faq1" onclick="OpenFAQ('faq1')">How can I embed a channel on my own website? </div>
        <div class="FAQAnswer faq1">All you need is to create a free account with a channel on Strimm and to subscribe to a proper plan on <a href="/create-tv/pricing">https://www.strimm.com/create-tv/pricing</a> . Once a subscription is purchased, you will receive detailed instructions via email on how to embed the code. </div>

        <div class="FAQQuestion faq2" onclick="OpenFAQ('faq2')">Where can I search for content for my channel?</div>
        <div class="FAQAnswer faq2">You can make a cross-platform search for video content directly on Strimm. We provide you with an easy access to public video libraries like YouTube, Vimeo and Dailymotion at one place. Feel free to use videos located on your own server and use your own CDN.</div>

        <div class="FAQQuestion faq3" onclick="OpenFAQ('faq3')">Can I upload videos directly to Strimm?</div>
        <div class="FAQAnswer faq3">Currently, we only allow use of content previously uploaded to YouTube, Vimeo, Dailymotion or your own servers. This way, the broadcast is much more affordable to you. You can also use a direct link to virtually any other video source, which allows embedding of the video and providing its source URL. </div>

    <%--    <div class="FAQQuestion faq4" onclick="OpenFAQ('faq4')">Can I broadcast live events?</div>
        <div class="FAQAnswer faq4">Yes! This option is available thru YouTube Live.  It is also part of your embedded channel.</div>--%>

        <div class="FAQQuestion faq5" onclick="OpenFAQ('faq5')">How can I monetize my channel and make revenue? </div>
        <div class="FAQAnswer faq5">
            While a 24/7 TV broadcast can directly help your business grow by attracting your visitors to come back, there are many other options to financially benefit from your channel:
            <ol>
                <li>a.	Add your own video commercials between your video shows. You can promote your own brand or other brands and businesses.</li>
                <li>b.	Use our password-protection of the channel feature. This way you can build your own TV network with a valuable or entertaining content and to provide a password to access your Network to only the paid subscribers. </li>
                <li>c.	Use the password-protection feature to generate leads for you or your clients. Create specific TV channels and provide access to them during Opt-In. </li>
                <li>d.	Offer your channel as a distribution source for content creators.</li>
                <li>e.	If your channel generates over 100,000 unique visitors per month, we can discuss advertising options. </li>
            </ol>
        </div>

        <div class="FAQQuestion faq6" onclick="OpenFAQ('faq6')">Where can I learn more about the features of the platform?</div>
        <div class="FAQAnswer faq6">
            <a class="indpLinks" href="learn-more">https://www.strimm.com/learn-more </a>
            <a class="indpLinks" href="/create-tv/features-and-benefits">https://www.strimm.com/create-tv/features-and-benefits </a>
            <a class="indpLinks" href="/guides">https://www.strimm.com/guides</a>
            <a class="indpLinks" href="/faq">https://www.strimm.com/faq</a>
        </div>
        <div class="FAQQuestion faq7" onclick="OpenFAQ('faq7')">What plan is good for me?</div>
        <div class="FAQAnswer faq7">
            It depends on your needs and how quickly you want to benefit from your channel. The more features the plan has, the more opportunities you receive. 
Please visit our <a href="/create-tv/pricing" style="color: #2a99bd">Pricing page</a>  to select the best package for you.
        </div>
        <div class="FAQQuestion faq8" onclick="OpenFAQ('faq8')">Do I need to create an account and channel before subscription?</div>
        <div class="FAQAnswer faq8">
            Yes. The subscription is for embedding of already created channels.
        </div>
        <div class="FAQQuestion faq9" onclick="OpenFAQ('faq9')">Where can I learn more about setting up a channel and the schedule?</div>
        <div class="FAQAnswer faq9">
            Please visit our <a href="/guides" style="color: #2a99bd">How To page</a> for details of the main elements and features.

        </div>
        <div class="FAQQuestion faq10" onclick="OpenFAQ('faq10')">How will my embedded channel look on my website?</div>
        <div class="FAQAnswer faq10">
            It will look similar to an example shown on <a href="/#embedded" class="bsLi">this</a> page. 
If you purchase a White-Label package, the reference to Strimm TV will be removed.
        </div>



        <div class="FAQQuestion faq11" onclick="OpenFAQ('faq11')">Can I cancel a subscription?</div>
        <div class="FAQAnswer faq11">You can cancel a subscription at any time.</div>

        <div class="FAQQuestion faq12" onclick="OpenFAQ('faq12')">Can I embed several channels on my website?</div>
        <div class="FAQAnswer faq12">
            You can create a whole TV network of your own channels, 
broadcasting 24/7 in any category.
        </div>

        <div class="stillQuastions">Still have questions? Feel free to <a href="/contact-us">Contact Us</a> </div>

        <a href="/create-tv/pricing" class="actionButton FAQ">Get Started</a>

    </div>


</asp:Content>
