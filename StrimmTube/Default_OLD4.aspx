<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Default_OLD4.aspx.cs" Inherits="StrimmTube.Default_2_21_16" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Strimm TV – Social Internet Television | Create Your Own TV For Free
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Strimm TV is a free social television and video platform to create your own online TV network and watch TV channels online for free." />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://www.strimm.com" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <%: System.Web.Optimization.Styles.Render("~/bundles/home_2_21_16/css") %>


    <!-- Google Analytics Content Experiment code -->
   <%-- <script>function utmx_section() { } function utmx() { } (function () {
        var
        k = '83902118-0', d = document, l = d.location, c = d.cookie;
        if (l.search.indexOf('utm_expid=' + k) > 0) return;
        function f(n) {
            if (c) {
                var i = c.indexOf(n + '='); if (i > -1) {
                    var j = c.
                    indexOf(';', i); return escape(c.substring(i + n.length + 1, j < 0 ? c.
                        length : j))
                }
            }
        } var x = f('__utmx'), xx = f('__utmxx'), h = l.hash; d.write(
        '<sc' + 'ript src="' + 'http' + (l.protocol == 'https:' ? 's://ssl' :
        '://www') + '.google-analytics.com/ga_exp.js?' + 'utmxkey=' + k +
        '&utmx=' + (x ? x : '') + '&utmxx=' + (xx ? xx : '') + '&utmxtime=' + new Date().
        valueOf() + (h ? '&utmxhash=' + escape(h.substr(1)) : '') +
        '" type="text/javascript" charset="utf-8"><\/sc' + 'ript>')
    })();
    </script><script>        utmx('url', 'A/B');</script>--%>
    <!-- End of Google Analytics Content Experiment code -->


 <%--use home_playerBlack/css--%>

<%--    <link href="css/DefaultCSS.css" rel="stylesheet" />--%>
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="//www.google.com/jsapi" type="text/javascript"></script>
    <script src="https://www.youtube.com/iframe_api"></script>
    <%--<script src="/JS/Main.js"></script>--%>
      <script src="/JS/Froogaloop.js"></script> 
    <script type="text/javascript">
        google.load("swfobject", "2.1");
    </script>
    <script src="Plugins/Scroller/nanoscroller.min.js"></script>
    <link href="Plugins/Scroller/scroller.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>


    <script type="text/javascript">
        var hideFooter = "<%=HideOldFooter%>";
        var username = "<%=UserName%>";

        function isMobile() {
            return (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino|android|ipad|playbook|silk/i.test(navigator.userAgent || navigator.vendor || window.opera) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test((navigator.userAgent || navigator.vendor || window.opera).substr(0, 4)))
        };

        $(document).ready(function () {
            if (hideFooter == "True") {
                $('#divFooter').hide();
            }

            if (username) {
                $(".bottomSignUpNH").hide();
                $("#ancCreateOrSignUpBottom").show();
                $('.btnCreateChannelFlags').attr("onclick", "CreateChannel.RedirectToCreateChannel()");
            }
            else {
                $('.btnCreateChannelFlags').attr("onclick", "loginModal('create-channel')");
            }
        });
 

    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="/JS/Controls.js" type="text/javascript">


    </script>

    <asp:HiddenField ID="hiddenTime" runat="server" ClientIDMode="Static" />

    

     
  <div id="homesliderHN">
     <%-- <div class="tvGuideHome"></div>--%>
      
        <div class="block">

           <%--<div class="howToBox watchNowBoxLeft">--%>
                                        <div class="boxAction noBG">

                                            <div class="avtionWording"><%--All Channels--%></div>
                                            <div class="boxDescription"></div>
                                        </div>
                                    </div>

              <div class="televisionByPeople">

<h1 class="pageH1"> Create Your Own TV Channel</h1>
<h2 class="pageH2">Bond With Your Audience. Grow Your Business. </h2> 

                  <div class="homeActionsHolder">

                      <a runat="server" id="ancCreateOrSignUp" clientidmode="Static"></a>
                    
                  </div>
              
            <a class="whatsPlayingNowIconHN" href="home#entertainmentGroup"></a>

<div class="homeGuideBusinessesHolder"> 
    <a class="homeGuide" href="/browse-channel?category=AllChannels">Watch Now</a>
    <a href="/" class="homeGBusinesses">Explore Business Options</a>
        </div>
      <div class="mobileActiveBtnHolder">
          <a class="mobileActiveBtn mobileGuide">TV Guide</a>
         <%--  <div class="mobileActiveBtn mobileHT">How To</div>--%>
           <a  class="mobileActiveBtn mobileBusinesses">Businesses</a>
           <div class="mobileHowToArrow"></div>

      </div>
<%--      <div class="nextSlide"> How It Works
      <div class="nextSlideArrow"> </div>
        </div>--%>
      </div>
        
     <div class="whatIsStrimmHolder">
         <div class="WSTitle">What is Strimm?</div>
         <div class="holderBlockOuter">
         <div class="holderBlock">
         <h3>The First Linear TV Network Created By Viewers. </h3>
         <p>Strimm (pronounced: “stream”) is an Internet Television Network with thousands of live TV channels in different languages and categories. All TV channels are created and curated by talented individuals and organizations from around the world and are streaming 24/7 to over 160 countries.</p>
             </div>
         <div class="holderBlock noBorderholderBlock">
         <h3>The TV Creation and Video Scheduling SaaS platform</h3>
         <p>Strimm gives any individual or an organization the power to become the producer of their very own online TV network and to schedule videos for a linear broadcast ahead of time. Entertain and engage your audience. Schedule video commercials on your retail website. Create webinars. Educate students. Stream local news. Monetize your channel. Broadcast it worldwide or locally on Strimm and your own website. </p>
             </div>
             </div>
         <div class="watchVideoIcon200Holder">
            <a class="watchVideoIcon200" onclick="ShowTutorialPlayer('<%=TutorialVideoId%>')"> 
             Watch Video
                <i class="fa fa-play-circle" style="color:#fff;line-height: 40px;"></i></a>
</div>
         
        
     </div>  

  


    <div class="whatIsHolder">
    <div class="whatIs">
            <div class="whatIsTitle whatIsTitleMobile">Become a Producer of Your Own TV Network</div>

                <div class="whatIsBlock">
                    <div class="whatIsIcon">

                        <div id="whatIsIconsCreate"></div>
                    </div>
                    <div class="whatIsStep" > 1. Register Your Channel</div>
                     <div class="whatIsDescription">Sign up & create your own network of channels. <br/>It's free!</div>
 
                </div>
        <div class="whatIsBlock">
            <div class="whatIsIcon">
                <div class="whatIsIcon">
                     <div id="whatIsIconsAdd"></div>
                </div>
                     <div class="whatIsStep"> 2. Add Videos</div>
                    <div class="whatIsDescription"> Search for videos directly on Strimm<br/> and add them to your channel.</div>
            
                </div>
           </div>
             <div class="whatIsBlock borderNone">
                    <div class="whatIsIcon">
                        

                         <div id="whatIsIconsBroadcast"></div>
            </div>
                     <div class="whatIsStep"> 3. Broadcast</div>
                    <div class="whatIsDescription"> Schedule selected videos and go on air.<br/> Share your channel! </div>
         
        </div>
            <div class="whatIslearnMore">
                <a href="LearnMore.aspx" class="btnWhatIslearnMore">Learn More</a>
    </div>

            </div>
        </div>

      
      <div class="guideView">
          <div class="guideViewTitle">Watch Thousands of TV channels Created by People like You</div>
      </div>


<div class="embeddedView">
      <div class="whatIsTitle whatIsTitleMobile">Attract More People to Your Website </div> 

         
    <div class="emneddedImg">
        <img src="images/001.jpg" />
    </div>
     <div class="emneddedText">

   <p> <span class="whatIsSubTitle">Transform your daily visitors into fans </span> 
             Our platform allows for easy channel embedding on any 
site. Educate and engage your audience with your own TV 
channel. Turn your visitors into repeat users!
Religious organizations can reach their followers anywhere they are;
doctors can create channels about wellness and healthy 
living; clothing boutiques can make fashion channels; 
restaurants of all kinds can make food and culture-related 
channels. There is virtually limitless possibility to find a niche-related topic for your business or organization.</p>

        </div>
                     <div class="whatIslearnMore">
                <a href="/" class="btnWhatIslearnMore">Get Started</a>
    </div>


   



     </div>

      <div class="channelsBG">
    
 <a name="entertainmentGroup"></a>
       
        <div class="whatIskWrapper">
          <div class="whatIsTitle whatIsTitleWhite whatIsTitleMobile ">See What is Playing on Strimm Now</div>
            <div class="homeChannelsView"></div>
        <div class="homeBlocksHolder" id="homeBlocksHolder" runat="server">

            <div class="homeChannelsView"></div>

             
            </div>
            <a href="/all-channels?category=All%20Channels" class="seeAllChannels"> see all channels</a>
            <div class="spacer" style="height: 30px;"></div>
        </div>



            </div>

      <div class="flags">

       <div class="flagsTitle">Join a Worldwide Community of Strimm Channel Producers  </div>
     <div class="flagsSubTitle">People From Over 100 Countries Have Joined Strimm</div>
  
       
<div class="createChannelFlags">
    <div class="btnCreateChannelFlags" style="cursor:pointer;">Create Channel</div>
</div>
   
          
      <div class="flagsBG">
          <img src="images/flags.png" />
      </div>   




</div>


  



    



</div>





    <div id="indiContent" runat="server" class="embeddedView">
      <div class="whatIsTitle indieTitle">Join Our Community of Independent Creators and Producers </div> 

    <div class="indieImg">
        <img src="images/indieHome.jpg" />
        </div>
     <div class="emneddedText">

   <p> <span class="whatIsSubTitle">Ready to share your story?</span> 
If you are an Independent film maker or an Independent producer, use Strimm's innovative platform to get your story out! Strimm has great tools to help you bring your ideas to life and financially benefit from them. 
We believe that everyone should do what they do best – film makers should create high quality videos and films, while talented producers should produce a good programming on their Strimm channels and 
promote them to their audience for mutual benefits.

                <div class="whatIslearnMore">
                <a href="indie.aspx" class="btnWhatIslearnMore">Learn More</a>
    </div>
</p>



            </div>


</div>



    <div class="homeBlocksHolder footerHomeBlock">
        <div id="divFooterNH" class="default regular">


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
                        <li><a href="/press">Press</a></li>
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
                    <span>&#169;2015-2017 Strimm, Inc. |  All Rights reserved </span>
                </div>
            </div>
        </div>

    </div>



</asp:Content>


