<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="business-pricing.aspx.cs" Inherits="StrimmTube.business_pricing" %>



<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Create Your Own TV Channel Online | Create Live TV Channel
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Start Your Own TV Channel Online. Select a Strimm Package to Create Your Own TV Channel and add it to Your Website, and Bring Your Business to a Next Level." />
    <meta name="robots" content="FOLLOW, INDEX, NOODP, NOYDIR" />
    <meta name="GOOGLEBOT" content="FOLLOW, INDEX, NOODP, NOYDIR" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://strimm.com/create-tv/pricing" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="//www.google.com/jsapi" type="text/javascript"></script>
   
   <%-- <script src="/JS/Froogaloop.js"></script>--%>
    <script type="text/javascript">
        google.load("swfobject", "2.1");
    </script>
    <link href="/css/BusinessPageCSS.css" rel="stylesheet" />
    <link href="/css/DefaultCSS_2_21_16.css" rel="stylesheet" />
    <script src="/JS/Business.js" type="text/javascript"></script>
    <script src="/JS/Package.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        var busManager = window.getManager();
        var prefs;
        var prefsTrialOrder1;
        var prefsTrialOrder2;
        var prefsTrialOrder3;
        $(document).ready(function () {
            //if (userId == 0 || userId == null) {
            //    jQuery("input:radio").attr('disabled', true);
            //}
            $("input:radio").prop('checked', false);
            $("#radio10Monthly, #radio50Monthly, #radio100Monthly, #radioProfPlusMonthly").prop('checked', true);
            $("#radio10MonthlyDown, #radio50MonthlyDown, #radio100MonthlyDown, #radioProfPlusMonthlyDown").prop('checked', true);

            $("#radio10MonthlyMobile, #radio50MonthlyMobile, #radio100MonthlyMobile, #radioProfPlusMonthlyMobile").prop('checked', true);

            prefs = window.getPreferences();
          
            prefs.init(userId, 1, false);
            prefs.setMonthlyPricesOnPackages();
            var isTrialAllowed = prefs.isTrialAllowed();
          
            var buttonText = "Subscribe";
            if (isTrialAllowed == false) {
               
                $("#btnStartFreeTrial10").text("").text(buttonText)
                $("#btnStartFreeTrial70").text("").text(buttonText);
                $("#btnStartFreeTrialCustomUrl").text("").text(buttonText);
                $("#btnStartFreeTrial10Mobile").text("").text(buttonText)
                $("#btnStartFreeTrial70Mobile").text("").text(buttonText);
                $("#btnStartFreeTrialCustomUrlMobile").text("").text(buttonText);
                $("#btnStartFreeTrial10Down").text("").text(buttonText)
                $("#btnStartFreeTrial70Down").text("").text(buttonText);
                $("#btnStartFreeTrialCustomUrlDown").text("").text(buttonText);
                $("#btnStartFreeTrialProfPlus").text("").text(buttonText);
                $("#btnStartFreeTrialProfPlusDown").text("").text(buttonText);
            }
        });

        function TogglePricingDetails(collapserClassName, parentClassName) {
            if ($("." + collapserClassName).is(":visible")) {
                $("." + collapserClassName).hide();
                $("#" + parentClassName).removeClass("active");
            }
            else {
                $("." + collapserClassName).show();
                $("#" + parentClassName).addClass("active");
            }
        }

        function SetAnnual(boolAnnual, btnId, btnClassName,  productId, elementClass) {
            console.log(productId)
            if (userId)
            {
                var locationUrl = $("#" + btnId).attr("onclick");
                var len = locationUrl.length;
                console.log(locationUrl.split("=")[1] + "=" + boolAnnual + "'");

                var changeLocationUrl = "location=" + locationUrl.split("=")[1] + "=" + boolAnnual + "'";
                $("." + btnClassName).removeAttr("onclick").attr("onclick", changeLocationUrl);
                //var controlClassName = $("$"this).attr("class");
               
          
               
            }
            if (boolAnnual) {
                console.log(boolAnnual)
                switch (productId) {
                    case 1:
                        $(".basicAnnualMessage").text("Get 2 Months FREE!");
                        console.log("here basic annual")
                        break;
                    case 4:
                        $(".professionalAnnualMessage").text("Get 2 Months FREE!");
                        console.log("here professional annual")
                        break;
                    case 5:
                        $(".advancedAnnualMessage").text("Get 2 Months FREE!");
                        console.log("here advanced annual")
                        break;
                    case 7:
                        $(".professionalPlusAnnualMessage").text("Get 2 Months FREE!");
                        console.log("here professional plus annual")
                        break;
                }

            }
            else {
                switch (productId) {
                    case 1:
                        $(".basicAnnualMessage").text("");
                        break;
                    case 4:
                        $(".professionalAnnualMessage").text("");
                        break;
                    case 5:
                        $(".advancedAnnualMessage").text("");
                        break;
                    case 7:
                        $(".professionalPlusAnnualMessage").text("");
                        break;
                }
            }
            $("." + elementClass).attr("checked", "checked");
            prefs.setPackagePricing(productId, !boolAnnual);
        }
        function ShowMoreInfo(classname) {
            if ($('.featureHolder.' + classname).is(":visible")) {
                $(".moreInfo." + classname).removeClass('active');
                $('.featureHolder.' + classname).hide();
            }
            else {
                $(".moreInfo." + classname).addClass('active');
                $('.featureHolder.' + classname).show();
            }
        }

    </script>


    <style>
        .businessNavigationC {
            text-decoration: underline;
        }

        .whatIsTitle, .whatIsTitleWhite, .whatIsTitleYellow, .flagsTitle, .WSTitle, .guideViewTitle {
            font-weight: bold;
            color: #2db9e7;
            font-size: 35px;
        }

        .whatIsHolder {
            margin-top: 100px;
        }

        .whatIsDescription {
            width: 95%;
        }

        @media (max-width:550px) {
            .whatIsHolder {
                margin-top: 50px;
            }

            .whatIsTitle, .whatIsTitleWhite, .whatIsTitleYellow, .flagsTitle, .WSTitle, .guideViewTitle {
                font-size: 30px;
            }
        }
        .imgSeal
        {
            width:180px;
            height:180px;
            display:block;
        }
        .imgSeal#sealL
        {
            float:left;
        }
          .imgSeal#sealR
        {
            float:right;
        }

        @media (max-width:960px) {
            .imgSeal#sealL,
            .imgSeal#sealR {
                float: unset;
                margin: auto;
            }
        }


    </style>



    <div id="divBoardContent">




        <div id="divContentWrapper">

               
  
         
        <div class="whatIsHolder">
            <div class="whatIs">
                     <div class="whatIsTitle whatIsTitleMobile">Create & Broadcast  Your Own TV Channel</div>

                <div class="whatIsBlock">
                    <div class="whatIsIcon">

                        <div id="whatIsIconsCreate"></div>
                    </div>
                    <div class="whatIsStep">1. Register Your Channel</div>
                    <div class="whatIsDescription">Sign up for a free account. Create your first TV channel. Celebrate! You are a broadcaster now.
                   </div>

                </div>
                <div class="whatIsBlock">
               
                        <div class="whatIsIcon">
                            <div id="whatIsIconsAdd"></div>
                        </div>
                        <div class="whatIsStep">2. Add Video Content</div>
                        <div class="whatIsDescription"> Search for favorite videos directly on Strimm. Use videos from YouTube, Vimeo, Dailymotion or from your own server.</div>

              
                </div>
                <div class="whatIsBlock borderNone">
                    <div class="whatIsIcon">


                        <div id="whatIsIconsBroadcast"></div>
                    </div>
                    <div class="whatIsStep">3. Broadcast, Watch & Share</div>
                    <div class="whatIsDescription">  Broadcast & watch it on Strimm for free, along with thousands of other TV channels. Embed it on your own website to grow your business.
</div>

                </div>
                <div class="whatIslearnMore">
                    <a name="pricingAnc"></a>
                    <%-- <a href="LearnMore.aspx" class="btnWhatIslearnMore">Learn More</a>--%>
                </div>

            </div>
        </div>

            <%--<h2 class="selectPrice">Select A Plan For Your Own TV Online</h2>--%>
              <div class="whatIsTitle whatIsTitleMobile">Embed TV Channel On Your Own Website</div>

            <div class="textPSecondary">

                Easily differentiate yourself from your competition.
Create your own live TV channel with Strimm's proprietary application and stream TV from your own website.
Get the best package for your business from the options below.<br />
               All monthly packages come with cancel-anytime terms.
  


            </div>

           
            
            <div class="maxBlockHolder">
                <div class="pricingTopHolder">
                    <div class="pricingDescMain pricingDescMainBlank"></div>
                    <div class="pricingDescMain">
                        <div class="pricingTitle">Basic</div>
                        <div class="pricingDesc">Good for bloggers with a website.</div>
                        <div class="pricingPrice">
                            <span id="basicPkgPrice" class="priceAmount basicPkgPrice"></span>
                            <span class="basicAnnualMessage"></span>
                            <span class="pricePeriod"></span>
                        </div>
                        <div class="radioHolder">
                            <div class="radioHolderHalf">
                                <label for="radio10Monthly">monthly</label>
                                <input type="radio" id="radio10Monthly" class="radio10Monthly" name="radioMonthly1" onchange="SetAnnual(false,'btnStartFreeTrial10','basic',1,'radio10Monthly')" />
                            </div>
                            <div class="radioHolderHalf">
                                <label for="radio10Annual">annual</label>
                                <input type="radio" id="radio10Annual" class="radio10Annual" name="radioMonthly1" onchange="SetAnnual(true,'btnStartFreeTrial10','basic',1,'radio10Annual')" />
                            </div>
                        </div>
                        <a onclick="<%=btnStartFreeTrial10Url%>" class="pricingStart basic" id="btnStartFreeTrial10">Start 14-Day Free Trial</a>


                    </div>
                    <div class="pricingDescMain">
                        <div class="pricingTitle">Advanced</div>
                        <div class="pricingDesc">Good for non-profits. More channels. Basic monetization opportunity.</div>
                        <div class="pricingPrice">
                            <span id="advPkgPrice" class="priceAmount advPkgPrice"></span>
                            <span class="advancedAnnualMessage"></span>
                            <span class="pricePeriod"></span>
                        </div>
                        <div class="radioHolder">
                            <div class="radioHolderHalf">
                                <label for="radio10Monthly">monthly</label>
                                <input type="radio" id="radio50Monthly" class="radio50Monthly" name="radioMonthly2" onchange="SetAnnual(false,'btnStartFreeTrial70','advanced',5,'radio50Monthly')" />
                            </div>
                            <div class="radioHolderHalf">
                                <label for="radio50Annual">annual</label>
                                <input type="radio" id="radio50Annual" name="radioMonthly2" class="radio50Annual" onchange="SetAnnual(true,'btnStartFreeTrial70','advanced',5,'radio50Annual')" />
                            </div>
                        </div>
                        <a onclick="<%=btnStartFreeTrial70Url%>" class="pricingStart advanced" id="btnStartFreeTrial70">Start 14-Day Free Trial</a>
                    </div>
                    <div class="pricingDescMain">
                        <div class="pricingTitle">Professional</div>
                        <div class="pricingDesc">Same as Proffesional plan, but with X4 channels and exclusive, white-label Roku SDK app.</div>
                        <div class="pricingPrice">
                            <span id="profPkgPrice" class="priceAmount profPkgPrice"></span>
                             <span class="professionalAnnualMessage"></span>
                            <span class="pricePeriod"></span>
                        </div>
                        <div class="radioHolder">
                            <div class="radioHolderHalf">
                                <label for="radio100Monthly">monthly</label>
                                <input type="radio" id="radio100Monthly" class="radio100Monthly" name="radioMonthly3" onchange="SetAnnual(false,'btnStartFreeTrialCustomUrl','professional',4,'radio100Monthly')" />
                            </div>
                            <div class="radioHolderHalf">
                                <label for="radio100Annual">annual</label>
                                <input type="radio" id="radio100Annual" class="radio100Annual" name="radioMonthly3" onchange="SetAnnual(true, 'btnStartFreeTrialCustomUrl','professional',4,'radio100Annual')" />
                            </div>
                        </div>
                        <a onclick="<%=btnStartFreeTrialCustomUrl%>" class="pricingStart professional" id="btnStartFreeTrialCustomUrl">Start 14-Day Free Trial</a>

                    </div>
                    <div class="pricingDescMain">
                        <div class="pricingTitle">Professional Plus</div>
                        <div class="pricingDesc">Same as Proffesional plan, but with X4 channels and exclusive, white-label Roku SDK app.</div>
                        <div class="pricingPrice">
                            <span id="profplusPkgPrice" class="priceAmount profplusPkgPrice"></span>
                            <span class="professionalPlusAnnualMessage"></span>
                            <span class="pricePeriod"></span>
                        </div>
                        <div class="radioHolder">
                            <div class="radioHolderHalf">
                                <label for="radio100Monthly">monthly</label>
                                <input type="radio" id="radioProfPlusMonthly" class="radioProfPlusMonthly" name="radioMonthly4" onchange="SetAnnual(false,'btnStartFreeTrialProfPlus','professionalplus',7,'radioProfPlusMonthly')" />
                            </div>
                            <div class="radioHolderHalf">
                                <label for="radio100Annual">annual</label>
                                <input type="radio" id="radioProfPlusAnnual" class="radioProfPlusAnnual" name="radioMonthly4" onchange="SetAnnual(true, 'btnStartFreeTrialProfPlus','professionalplus',7,'radioProfPlusAnnual')" />
                            </div>
                        </div>
                        <a onclick="<%=btnStartFreeTrialProfPlusUrl%>" class="pricingStart professionalplus" id="btnStartFreeTrialProfPlus">Start 14-Day Free Trial</a>
                    </div>
                </div>

                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc blank">-</div>
                    <div class="pricingCell pricingCellChannelN">1 channel</div>
                    <div class="pricingCell pricingCellChannelN">10 channels</div>
                    <div class="pricingCell pricingCellChannelN">25 channels</div>
                    <div class="pricingCell pricingCellChannelN">100 channels</div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>TV Channel in any category</strong></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                 <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Unlimited Videos. </strong>Add as many online videos as you want to your channel library and create your own Internet TV.</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                     <div class="pricingCell pricingCellCheck"></div>
                     <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Advanced TV Programming.</strong> Schedule broadcast of your TV shows ahead of time, even for weeks and months ahead!</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Linear TV Broadcast.</strong> Broadcast all of your shows just like traditional TV, 24/7, in real time, per your schedules.</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                  <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Live Broadcast.</strong> Schedule and stream a live show from your camera thru YouTube Live, integrated into your Strimm TV Channel.</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                      <div class="pricingCell pricingCellCheck"></div>
                      <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Embedded TV Guide. </strong>Create your own TV Network. The embedded TV Guide will allow your viewers easily switch between your channels in the same screen.</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Embedded Daily Schedule.</strong>  No need to manually add your daily programming to the site. It is embedded with a TV guide.</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Instant Schedule.</strong> Create a whole day's schedule with a single click.</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Autopilot.</strong> Busy? Let our system automatically schedule your daily TV programming.  </div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Extended Reach.</strong> Your channel(s) are broadcasted simultaneously on your own website and on Strimm.</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Unlimited Email Support.</strong></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Use of Commercials.</strong> Add commercials or your brand idents between video shows to monetize from your channel or promote yourself.</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Password-protection. </strong>Benefit from your network directly. Provide password access to your channel/(s) to your own subscribers or students or Opt-in leads.</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Multiple domains.</strong> Easily embed each channel on its own website.</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Dedicated account manager.</strong></div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Player controls.</strong>  Show or hide player controls on your embedded channel. </div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Free Promotion.</strong> We will feature your embedded channel on Strimm’s Facebook page to over 90,000 of our followers.  Skyrocket your traffic! </div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Embed-only option.</strong> Private content? Show your TV channel on your website only (don't show it on Strimm).</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Broadcast of private videos.</strong> Broadcast videos, which are marked as unlisted or private on YouTube, Vimeo and Dailymotion (embed only).</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Direct links use.</strong> Broadcast videos from different online sources, including your own server, by importing their video source URL (embed only). </div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Unristricted video content. </strong>Show any type of content for different audiences (embed only; must obey relevant laws and regulations)</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>White-Label.</strong> Strimm branding is removed. </div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Custom Branding.</strong> Add your own branding on top of embedded channel.</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Access to API.</strong> Get an access to web API console to extract data needed to build your own app.</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <%--<div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>HLS Playlist Link.</strong> Get an HLS URL to output playlist of the scheduled TV program of the day, auto-refreshed daily.</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>--%>
                <div class="pricingRow">
                    <div class="pricingCell pricingCellDesc"><strong>Custom-Branded Roku App.</strong>. Download your own custom-branded Roku app, streaming your linear TV channels on Roku out of your m3u8 or mp4 video content., located on your server with CDN (direct links). Built-in Roku monetization option (RAF) <a href="#" class="seeDetailsLink" onclick="showRokuAppInfo();">See Details</a></div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell blank">-</div>
                    <div class="pricingCell pricingCellCheck"></div>
                </div>
                <%--<div class="pricingRow">
                    <div class="pricingCell pricingCellDesc pricingCellDescBottom">- </div>

                    <a onclick="<%=btnStartFreeTrial10Url%>" class="pricingCell pricingCellBottom">Start 14-Day Free Trial</a>
                    <a onclick="<%=btnStartFreeTrial70Url%>" class="pricingCell pricingCellBottom">Start 14-Day Free Trial</a>
                    <a onclick="<%=btnStartFreeTrialCustomUrl%>" class="pricingCell pricingCellBottom">Start 14-Day Free Trial</a>


                </div>--%>

                  <div class="pricingBottomHolder">
                         <div class="pricingDescMain  pricingDescMainBlankBottom"></div>

                                          <div class="pricingDescMain">
                        <div class="pricingTitleBottom">Basic</div>
                   
                        <div class="pricingPrice">
                            <span id="basicPkgPrice" class="priceAmount basicPkgPrice"></span>
                            <span class="basicAnnualMessage"></span>
                            <span class="pricePeriod"></span>
                        </div>
                        <div class="radioHolder">
                            <div class="radioHolderHalf">
                                <label for="radio10MonthlyDown">monthly</label>
                                <input type="radio" id="radio10MonthlyDown" class="radio10Monthly" name="radioMonthly1Down" onchange="SetAnnual(false,'btnStartFreeTrial10','basic',1,'radio10Monthly')" />
                            </div>
                            <div class="radioHolderHalf">
                                <label for="radio10AnnualDown">annual</label>
                                <input type="radio" id="radio10AnnualDown" class="radio10Annual" name="radioMonthly1Down" onchange="SetAnnual(true,'btnStartFreeTrial10','basic',1,'radio10Annual')" />
                            </div>
                        </div>
                        <a onclick="<%=btnStartFreeTrial10Url%>" class="pricingStart basic" id="btnStartFreeTrial10Down">Start 14-Day Free Trial</a>


                    </div>
                    <div class="pricingDescMain">
                        <div class="pricingTitleBottom">Advanced</div>
                
                        <div class="pricingPrice">
                            <span id="advPkgPrice" class="priceAmount advPkgPrice"></span>
                             <span class="advancedAnnualMessage"></span>
                            <span class="pricePeriod"></span>
                        </div>
                        <div class="radioHolder">
                            <div class="radioHolderHalf">
                                <label for="radio10MonthlyDown">monthly</label>
                                <input type="radio" id="radio50MonthlyDown" class="radio50Monthly" name="radioMonthly2Down" onchange="SetAnnual(false,'btnStartFreeTrial70','advanced',5,'radio50Monthly')" />
                            </div>
                            <div class="radioHolderHalf">
                                <label for="radio50AnnualDown">annual</label>
                                <input type="radio" id="radio50AnnualDown" name="radioMonthly2Down" class="radio50Annual" onchange="SetAnnual(true,'btnStartFreeTrial70','advanced',5,'radio50Annual')" />
                            </div>
                        </div>
                        <a onclick="<%=btnStartFreeTrial70Url%>" class="pricingStart advanced" id="btnStartFreeTrial70Down">Start 14-Day Free Trial</a>
                    </div>
                    <div class="pricingDescMain">
                        <div class="pricingTitleBottom">Professional</div>
            
                        <div class="pricingPrice">
                            <span id="profPkgPrice" class="priceAmount profPkgPrice"></span>
                             <span class="professionalAnnualMessage"></span>
                            <span class="pricePeriod"></span>
                        </div>
                        <div class="radioHolder">
                            <div class="radioHolderHalf">
                                <label for="radio100MonthlyDown">monthly</label>
                                <input type="radio" id="radio100MonthlyDown" class="radio100Monthly" name="radioMonthly3Down" onchange="SetAnnual(false,'btnStartFreeTrialCustomUrl','professional',4,'radio100Monthly')" />
                            </div>
                            <div class="radioHolderHalf">
                                <label for="radio100AnnualDown">annual</label>
                                <input type="radio" id="radio100AnnualDown" class="radio100Annual" name="radioMonthly3Down" onchange="SetAnnual(true, 'btnStartFreeTrialCustomUrl','professional',4,'radio100Annual')" />
                            </div>
                        </div>
                        <a onclick="<%=btnStartFreeTrialCustomUrl%>" class="pricingStart professional" id="btnStartFreeTrialCustomUrlDown">Start 14-Day Free Trial</a>

                    </div>
                      <div class="pricingDescMain">
                          <div class="pricingTitleBottom">Professional Plus</div>
                          <div class="pricingPrice">
                              <span id="profplusPkgPrice" class="priceAmount profplusPkgPrice"></span>
                              <span class="professionalPlusAnnualMessage"></span>
                              <span class="pricePeriod"></span>
                          </div>
                          <div class="radioHolder">
                              <div class="radioHolderHalf">
                                  <label for="radioMonthly4">monthly</label>
                                  <input type="radio" id="radioProfPlusMonthlyDown" class="radioProfPlusMonthly" name="radioMonthly4Down" onchange="SetAnnual(false,'btnStartFreeTrialProfPlus','professionalplus',7,'radioProfPlusMonthly')" />
                              </div>
                              <div class="radioHolderHalf">
                                  <label for="radioMonthly4">annual</label>
                                  <input type="radio" id="radioProfPlusAnnualDown" class="radioProfPlusAnnual" name="radioMonthly4Down" onchange="SetAnnual(true, 'btnStartFreeTrialProfPlus','professionalplus',7,'radioProfPlusAnnual')" />
                              </div>
                          </div>
                          <a onclick="<%=btnStartFreeTrialProfPlusUrl%>" class="pricingStart professionalplus" id="btnStartFreeTrialProfPlusDown">Start 14-Day Free Trial</a>
                      </div>
                  </div>


                <div class="maxBlockHolder">
                    <div id="continueHolder">

                        <%--  <div class="subInfo">Do you have more questions? <a href="/contact-us" class="licontactUs">Contact Us!</a></div>--%>
                    </div>
                </div>

            </div>
            

        </div>


        
        <div class="maxBlockHolderMobile">
            <div class="packMobileHolder">
                <div class="pricingTitleMobile">Basic</div>
                <div class="pricingDescMobile">Great for bloggers & individuals with a website.</div>
                <div class="pricingPriceMobile">
                    <span id="basicPkgPriceMobile" class="priceAmount basic"></span>
                    <span class="basicAnnualMessage"></span>
                    <span class="pricePeriod basic"></span>
                </div>
                <div class="radioHolder">
                    <div class="radioHolderHalf">
                        <label for="radio10MonthlyMobile">monthly</label>
                        <input type="radio" id="radio10MonthlyMobile" class="radio10MonthlyMobile" name="radioMonthly1Mobile" onchange="SetAnnual(false,'btnStartFreeTrial10Mobile','basic',1,'radioMonthly1Mobile')" />
                    </div>
                    <div class="radioHolderHalf">
                        <label for="radio10AnnualMobile">annual</label>
                        <input type="radio" id="radio10AnnualMobile" name="radioMonthly1Mobile" class="radioAnnualMobile" onchange="SetAnnual(true,'btnStartFreeTrial10Mobile','basic',1,'radioAnnualMobile')" />
                                                                                                                                              


                    </div>
                </div>
                <div class="pricingStartHolderMobile">
                    <a onclick="<%=btnStartFreeTrial10Url%>" class="pricingStart basic" id="btnStartFreeTrial10Mobile">Start 14-Day Free Trial</a>
                </div>
                <div class="moreInfo pack1" onclick="ShowMoreInfo('pack1')"></div>
                 <div class="pack1" onclick="ShowMoreInfo('pack1')"></div>
              
                <div class="featureHolder pack1">
                      <div class="pricingDescMobile" style="border-top:1px solid #ddd; font-weight:bold;" >1 Channel</div>
                    <div class="packMobileFeature"><strong>TV Channel in any category</strong></div>
                      <div class="packMobileFeature"><strong>Unlimited Videos. </strong>Add as many online videos as you want to your channel library and create your own Internet TV.</div>
                    <div class="packMobileFeature"><strong>Advanced TV Programming.</strong> Schedule broadcast of your TV shows ahead of time, even for weeks and months ahead!</div>
                    <div class="packMobileFeature"><strong>Linear TV Broadcast.</strong> Broadcast all of your shows just like traditional TV, 24/7, in real time, per your schedules.</div>
                    <div class="packMobileFeature"><strong>Live Broadcast.</strong> Schedule and stream a live show from your camera thru YouTube Live, integrated into your Strimm TV Channel.</div>
                    <div class="packMobileFeature"><strong>Embedded TV Guide.</strong> Create your own TV Network. The embedded TV Guide will allow your viewers easily switch between your channels in the same screen.</div>
                    <div class="packMobileFeature"><strong>Embedded Daily Schedule.</strong>  No need to manually add your daily programming to the site. It is embedded with a TV guide.</div>
                    <div class="packMobileFeature"><strong>Instant Schedule.</strong> Create a whole day's schedule with a single click.</div>
                    <div class="packMobileFeature"><strong>Autopilot. </strong>Busy? Let our system automatically schedule your daily TV programming. </div>
                    <div class="packMobileFeature"><strong>Extended Reach.</strong> Your channel(s) are broadcasted simultaneously on your own website and on Strimm.</div>
                    <div class="packMobileFeature"><strong>Unlimited Email Support.</strong></div>
                    <div class="packMobileFeature"><strong>Use of Commercials.</strong> Add commercials or your brand idents between video shows to monetize from your channel or promote yourself.</div>
             </div>

            </div>
            <div class="packMobileHolder">
                <div class="pricingTitleMobile">Advanced</div>
                <div class="pricingDescMobile">Great for small business and non-profits. Monetization opportunity.</div>
                <div class="pricingPriceMobile">
                    <span id="advPkgPriceMobile" class="priceAmount advancedPackage"></span>
                    <span class="advancedAnnualMessage"></span>
                </div>
                <div class="radioHolder">
                    <div class="radioHolderHalf">
                        <label for="radio10MonthlyMobile">monthly</label>
                        <input type="radio" id="radio50MonthlyMobile" class="radio50MonthlyMobile" name="radioMonthly2Mobile" onchange="SetAnnual(false,'btnStartFreeTrial70Mobile','advanced',5,'radio50MonthlyMobile')" />
                    </div>                                                                                                             
                    <div class="radioHolderHalf">
                        <label for="radio50AnnualMobile">annual</label>
                        <input type="radio" id="radio50AnnualMobile" name="radioMonthly2Mobile" class="radio50AnnualMobile" onchange="SetAnnual(true,'btnStartFreeTrial70Mobile','advanced',5,'radio50AnnualMobile')" />
                    </div>
                </div>
                <div class="pricingStartHolderMobile">

                    <a onclick="<%=btnStartFreeTrial70Url%>" class="pricingStart advanced" id="btnStartFreeTrial70Mobile">Start 14-Day Free Trial</a>
                </div>
                <div class="moreInfo pack2" onclick="ShowMoreInfo('pack2')"></div>
                <div class="featureHolder pack2">
                     <div class="pricingDescMobile" style="border-top:1px solid #ddd; font-weight:bold;" >10 Channels</div>
                    <div class="packMobileFeature"><strong>TV Channel in any category</strong></div>
                      <div class="packMobileFeature"><strong>Unlimited Videos. </strong>Add as many online videos as you want to your channel library and create your own Internet TV.</div>
                    <div class="packMobileFeature"><strong>Advanced TV Programming.</strong> Schedule broadcast of your TV shows ahead of time, even for weeks and months ahead!</div>
                    <div class="packMobileFeature"><strong>Linear TV Broadcast.</strong> Broadcast all of your shows just like traditional TV, 24/7, in real time, per your schedules.</div>
                    <div class="packMobileFeature"><strong>Live Broadcast.</strong> Schedule and stream a live show from your camera thru YouTube Live, integrated into your Strimm TV Channel.</div>
                    <div class="packMobileFeature"><strong>Embedded TV Guide.</strong> Create your own TV Network. The embedded TV Guide will allow your viewers easily switch between your channels in the same screen.</div>
                    <div class="packMobileFeature"><strong>Embedded Daily Schedule.</strong>  No need to manually add your daily programming to the site. It is embedded with a TV guide.</div>
                    <div class="packMobileFeature"><strong>Instant Schedule.</strong> Create a whole day's schedule with a single click.</div>
                    <div class="packMobileFeature"><strong>Autopilot. </strong>Busy? Let our system automatically schedule your daily TV programming. </div>
                    <div class="packMobileFeature"><strong>Extended Reach.</strong> Your channel(s) are broadcasted simultaneously on your own website and on Strimm.</div>
                    <div class="packMobileFeature"><strong>Unlimited Email Support.</strong></div>
                    <div class="packMobileFeature"><strong>Use of Commercials.</strong> Add commercials or your brand idents between video shows to monetize from your channel or promote yourself.</div>
                    <div class="packMobileFeature"><strong>Password-protection. </strong>Benefit from your network directly. Provide password access to your channel/(s) to your own subscribers or students or Opt-in leads.    </div>
                    <div class="packMobileFeature"><strong>Multiple domains.</strong> Easily embed each channel on its own website.</div>
                    <div class="packMobileFeature"><strong>Dedicated account manager.</strong></div>
                    <div class="packMobileFeature"><strong>Player controls.</strong>  Show or hide player controls on your embedded channel.</div>
                    <div class="packMobileFeature"><strong>Free Promotion.</strong> We will feature your embedded channel on Strimm’s Facebook page to over 90,000 of our followers.  Skyrocket your traffic!</div>
                    <div class="packMobileFeature"><strong>Embed-only option.</strong> Private content? Show your TV channel on your website only (don't show it on Strimm).</div>


                </div>



            </div>
            <div class="packMobileHolder">
                <div class="pricingTitleMobile">Professional</div>
                <div class="pricingDescMobile">Great for any business size. The best plan with all features, including white-label.</div>
                <div class="pricingPriceMobile">
                    <span id="profPkgPriceMobile" class="priceAmount professional"></span>
                    <span class="professionalAnnualMessage"></span>
                </div>
                <div class="radioHolder">
                    <div class="radioHolderHalf">
                        <label for="radio100MonthlyMobile">monthly</label>
                        <input type="radio" id="radio100MonthlyMobile" class="radio100MonthlyMobile" name="radioMonthly3Mobile" onchange="SetAnnual(false,'btnStartFreeTrialCustomUrlMobile','professional',4,'radio100MonthlyMobile')" />
                    </div>                                                                                                                
                    <div class="radioHolderHalf">
                        <label for="radio100AnnualMobile">annual</label>
                        <input type="radio" id="radio100AnnualMobile" class="radio100AnnualMobile" name="radioMonthly3Mobile" onchange="SetAnnual(true, 'btnStartFreeTrialCustomUrlMobile','professional',4,'radio100AnnualMobile')" />
                    </div>
                </div>
                <div class="pricingStartHolderMobile">

                    <a onclick="<%=btnStartFreeTrialCustomUrl%>" class="pricingStart professional" id="btnStartFreeTrialCustomUrlMobile">Start 14-Day Free Trial</a>
                </div>
                <div class="moreInfo pack3" onclick="ShowMoreInfo('pack3')"></div>
                <div class="featureHolder pack3">
                     <div class="pricingDescMobile" style="border-top:1px solid #ddd; font-weight:bold;" >25 Channels</div>
                    <div class="packMobileFeature"><strong>TV Channel in any category</strong></div>
                     <div class="packMobileFeature"><strong>Unlimited Videos. </strong>Add as many online videos as you want to your channel library and create your own Internet TV.</div>
                    <div class="packMobileFeature"><strong>Advanced TV Programming.</strong> Schedule broadcast of your TV shows ahead of time, even for weeks and months ahead!</div>
                    <div class="packMobileFeature"><strong>Linear TV Broadcast.</strong> Broadcast all of your shows just like traditional TV, 24/7, in real time, per your schedules.</div>
                    <div class="packMobileFeature"><strong>Live Broadcast.</strong> Schedule and stream a live show from your camera thru YouTube Live, integrated into your Strimm TV Channel.</div>
                    <div class="packMobileFeature"><strong>Embedded TV Guide.</strong> Create your own TV Network. The embedded TV Guide will allow your viewers easily switch between your channels in the same screen.</div>
                    <div class="packMobileFeature"><strong>Embedded Daily Schedule.</strong>  No need to manually add your daily programming to the site. It is embedded with a TV guide.</div>
                    <div class="packMobileFeature"><strong>Instant Schedule.</strong> Create a whole day's schedule with a single click.</div>
                    <div class="packMobileFeature"><strong>Autopilot. </strong>Busy? Let our system automatically schedule your daily TV programming. </div>
                    <div class="packMobileFeature"><strong>Extended Reach.</strong> Your channel(s) are broadcasted simultaneously on your own website and on Strimm.</div>
                    <div class="packMobileFeature"><strong>Unlimited Email Support.</strong></div>
                    <div class="packMobileFeature"><strong>Use of Commercials.</strong> Add commercials or your brand idents between video shows to monetize from your channel or promote yourself.</div>
                    <div class="packMobileFeature"><strong>Password-protection. </strong>Benefit from your network directly. Provide password access to your channel/(s) to your own subscribers or students or Opt-in leads.    </div>
                    <div class="packMobileFeature"><strong>Multiple domains.</strong> Easily embed each channel on its own website.</div>
                    <div class="packMobileFeature"><strong>Dedicated account manager.</strong></div>
                    <div class="packMobileFeature"><strong>Player controls.</strong>  Show or hide player controls on your embedded channel.</div>
                    <div class="packMobileFeature"><strong>Free Promotion.</strong> We will feature your embedded channel on Strimm’s Facebook page to over 90,000 of our followers.  Skyrocket your traffic!</div>
                    <div class="packMobileFeature"><strong>Embed-only option.</strong> Private content? Show your TV channel on your website only (don't show it on Strimm).</div>
                    <div class="packMobileFeature"><strong>Broadcast of private videos.</strong> Broadcast videos, which are marked as unlisted or private on YouTube, Vimeo and Dailymotion (embed only).</div>
                    <div class="packMobileFeature"><strong>Direct links use.</strong> Broadcast videos from different online sources, including your own server, by importing their video source URL (embed only).</div>
                    <div class="packMobileFeature"><strong>Unristricted video content. </strong>Show any type of content for different audiences (embed only; must obey relevant laws and regulations)</div>
                    <div class="packMobileFeature"><strong>White-Label.</strong> Strimm branding is removed. </div>
                    <div class="packMobileFeature"><strong>Custom Branding.</strong> Add your own branding on top of embedded channel.</div>
                </div>
    </div>
            <div class="packMobileHolder">
                <div class="pricingTitleMobile">Professional Plus</div>
                <div class="pricingDescMobile">Great for any business. Includes, Custom-Branded Roku app, white-label option, custom branding, and an access to API.</div>
                <div class="pricingPriceMobile">
                    <span id="profplusPkgPricemobile" class="priceAmount professionalplus"></span>
                    <span class="professionalPlusAnnualMessage"></span>
                </div>
                <div class="radioHolder">
                    <div class="radioHolderHalf">
                        <label for="radio100MonthlyMobile">monthly</label>
                        <input type="radio" id="radioProfPlusMonthlyMobile" class="radioProfPlusMonthlyMobile" name="radioMonthly4Mobile" onchange="SetAnnual(false,'btnStartFreeTrialCustomUrlMobile','professionalplus',7,'radioMonthly4Mobile')" />
                    </div>
                    <div class="radioHolderHalf">
                        <label for="radio100AnnualMobile">annual</label>
                        <input type="radio" id="radioProfPlusAnnualMobile" class="radioProfPlusAnnualMobile" name="radioMonthly4Mobile" onchange="SetAnnual(true, 'btnStartFreeTrialCustomUrlMobile','professionalplus',7,'radioMonthly4Mobile')" />
                    </div>
                </div>
                <div class="pricingStartHolderMobile">
                    <a onclick="<%=btnStartFreeTrialProfPlusUrl%>" class="pricingStart professional" id="btnStartFreeTrialProfPlusMobile">Start 14-Day Free Trial</a>
                </div>
                <div class="moreInfo pack3" onclick="ShowMoreInfo('pack3')"></div>
                <div class="featureHolder pack3">
                    <div class="pricingDescMobile" style="border-top: 1px solid #ddd; font-weight: bold;">25 Channels</div>
                    <div class="packMobileFeature"><strong>TV Channel in any category</strong></div>
                    <div class="packMobileFeature"><strong>Unlimited Videos. </strong>Add as many online videos as you want to your channel library and create your own Internet TV.</div>
                    <div class="packMobileFeature"><strong>Advanced TV Programming.</strong> Schedule broadcast of your TV shows ahead of time, even for weeks and months ahead!</div>
                    <div class="packMobileFeature"><strong>Linear TV Broadcast.</strong> Broadcast all of your shows just like traditional TV, 24/7, in real time, per your schedules.</div>
                    <div class="packMobileFeature"><strong>Live Broadcast.</strong> Schedule and stream a live show from your camera thru YouTube Live, integrated into your Strimm TV Channel.</div>
                    <div class="packMobileFeature"><strong>Embedded TV Guide.</strong> Create your own TV Network. The embedded TV Guide will allow your viewers easily switch between your channels in the same screen.</div>
                    <div class="packMobileFeature"><strong>Embedded Daily Schedule.</strong>  No need to manually add your daily programming to the site. It is embedded with a TV guide.</div>
                    <div class="packMobileFeature"><strong>Instant Schedule.</strong> Create a whole day's schedule with a single click.</div>
                    <div class="packMobileFeature"><strong>Autopilot. </strong>Busy? Let our system automatically schedule your daily TV programming. </div>
                    <div class="packMobileFeature"><strong>Extended Reach.</strong> Your channel(s) are broadcasted simultaneously on your own website and on Strimm.</div>
                    <div class="packMobileFeature"><strong>Unlimited Email Support.</strong></div>
                    <div class="packMobileFeature"><strong>Use of Commercials.</strong> Add commercials or your brand idents between video shows to monetize from your channel or promote yourself.</div>
                    <div class="packMobileFeature"><strong>Password-protection. </strong>Benefit from your network directly. Provide password access to your channel/(s) to your own subscribers or students or Opt-in leads.    </div>
                    <div class="packMobileFeature"><strong>Multiple domains.</strong> Easily embed each channel on its own website.</div>
                    <div class="packMobileFeature"><strong>Dedicated account manager.</strong></div>
                    <div class="packMobileFeature"><strong>Player controls.</strong>  Show or hide player controls on your embedded channel.</div>
                    <div class="packMobileFeature"><strong>Free Promotion.</strong> We will feature your embedded channel on Strimm’s Facebook page to over 90,000 of our followers.  Skyrocket your traffic!</div>
                    <div class="packMobileFeature"><strong>Embed-only option.</strong> Private content? Show your TV channel on your website only (don't show it on Strimm).</div>
                    <div class="packMobileFeature"><strong>Broadcast of private videos.</strong> Broadcast videos, which are marked as unlisted or private on YouTube, Vimeo and Dailymotion (embed only).</div>
                    <div class="packMobileFeature"><strong>Direct links use.</strong> Broadcast videos from different online sources, including your own server, by importing their video source URL (embed only).</div>
                    <div class="packMobileFeature"><strong>Unristricted video content. </strong>Show any type of content for different audiences (embed only; must obey relevant laws and regulations)</div>
                    <div class="packMobileFeature"><strong>White-Label.</strong> Strimm branding is removed. </div>
                    <div class="packMobileFeature"><strong>Custom Branding.</strong> Add your own branding on top of embedded channel.</div>
                </div>
            </div>

        </div>

        <div class="BPblock FAQ">
            <div class="textMain FAQ">FAQ</div>
            <div class="FAQQuestion faq1" onclick="OpenFAQ('faq1')">Do I need to sign up for free Strimm account before subscription?</div>
            <div class="FAQAnswer faq1">Yes. Please sign up. It’s free and will take just a minute.  </div>

            <div class="FAQQuestion faq2" onclick="OpenFAQ('faq2')">What if I need more channels than in the subscription plan?</div>
            <div class="FAQAnswer faq2">Feel free to purchase as many subscriptions as you need and create your own TV emprire!</div>
            <div class="FAQQuestion faq3" onclick="OpenFAQ('faq3')">Do I need to supply a payment information for a Free Trial?</div>

            <div class="FAQAnswer faq3">Yes. The Free Trial is a fully-featured subscription. To avoid hassles of re-subscribing for an active subscription, the payment will automatically start at the end of free trial. Please make sure to apply your active PayPal account or a valid credit card to avoid service interruption. </div>
        </div>

        <div id="rokuInfoPopup" class="rokuInfoPopup">
            <a id="close_x" class="close close_x" href="#">close</a>
            <h2>PUT YOUR OWN TV NETWORK ON ROKU</h2>
            <br />
            <img src="/images/roku_app-7.20.21-1.jpg" />
            <br />
            <h3>App Options & Features</h3>
            <ul>
                <li>A stylish look of your own branded Roku SDK app</li>
                <li>A TV guide, showing all of your Roku channels with their daily program</li>
                <li>Real-time broadcast of your linear TV channels, according to their schedules.</li>
                <li>Easily switch between channels.</li>
                <li>Add any channel to the “Favorites”.</li>
                <li>Automatic Fullscreen mode in 5 sec</li>
                <li>Show details about future shows by simply clicking on them.</li>
                <li>Use direct video links in .mp4 or .m3u8 format or your videos located on Vimeo</li>
                <li>Search for the channels and currently broadcasting shows</li>
                <li>Show your brand.</li>
                <li>Monetization capability thru integrated Roku Advertisement Framework</li>
            </ul>
        </div>

    </div>
</asp:Content>


