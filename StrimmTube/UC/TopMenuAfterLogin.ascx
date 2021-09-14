
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenuAfterLogin.ascx.cs" Inherits="StrimmTube.UC.TopMenuAfterLogin" %>
<%@ Register Src="~/UC/CreateChannelUC.ascx" TagPrefix="uc" TagName="CreateChannelUC" %>


<%--<script src="/../Plugins/dropdown/modernizr.custom.79639.js"></script>

<link href="/../css/CreateChannel.css" rel="stylesheet" />--%>

<link href="/Plugins/alertify/alertify.core.css" rel="stylesheet" />
<link href="/Plugins/alertify/alertify.default.css" rel="stylesheet"  id="toggleCSS" />
<script src="/Plugins/alertify/alertify.js"></script>

    <%--<link href="/flowplayer/html5/5.5.2/skin/functional.css" rel="stylesheet" />
    <script src="/flowplayer/html5/5.5.2/flowplayer.min.js"></script>--%>
   <!-- CSS for this demo -->
<%--   <link rel="stylesheet" href="//flowplayer.org/media/css/demos/playlist/js.css">--%>

   <!-- Flowplayer-->
<%--   <script src="//releases.flowplayer.org/6.0.3/flowplayer.min.js"></script>--%>

     <script type="text/javascript">
         var userUrl = "<%=userUrl%>";
         var maxUserChannelCount = '<%=maxUserChannels%>';
         var domainName = '<%=domainName%>';
         var channelCount = '<%=channelCount%>';
          var userId = '<%=userId%>';
         var isUserLocked = JSON.parse("<%=isUserlockedOut%>".toLowerCase());
         var isUserDeleted = JSON.parse("<%=isUserDeleted%>".toLowerCase());
         var isBefore = false;//for search control
         var isPro = JSON.parse("<%=isProEnabled%>".toLowerCase());
         var browseMenu = "<%=browseMenuHtml%>";
         var firstName = "<%=firstName%>";
         var hasInterests = "<%=hasInterests%>";
         var accountNumber = "<%=accountNumber%>";
         var interestManager;
         var isMenueVisible = false;
       
         $(document).ready(function () {
             $(".mobileMenu").click(function (e) {
                 console.log("TRIGGER MOBILE MENU")
                 e.preventDefault();
                 var $div = $(this).next('.mobileMenuOpen');
                 $(".mobileMenuOpen").not($div).hide();
                 console.log("hide menu")
                 if ($div.is(":visible")) {

                     $div.hide();
                     
                     $(".mobileMenu").removeClass("active")
                 } else {
                     $div.show();
                     
                     $(".mobileMenu").addClass("active");

                 }
             });

             $('html').click(function (e) {

                 if ($(e.target).closest('.mobileMenu').length != 0) {
                     return false;
                 }
                 else
                 {
                     var $div = $('.mobileMenuOpen');
                     if ($div.is(":visible") && isMenueVisible == true) {
                         if ($(".subBlockBusiness").is(":hidden")) {
                             $div.hide();
                             $(".mobileMenu").removeClass("active");
                             isMenueVisible = false;
                         }


                     }
                     else {
                         isMenueVisible = true;
                     }
                 }
                
             });
           
             interestManager = getInterests();
             interestManager.init(userId);

             if (hasInterests == "False") {
                 $("#userInterestsModal").lightbox_me({
                     centered: true,
                     onLoad: function () {
                     },
                     onClose: function () {
                         RemoveOverlay();
                     },
                     closeSelector: "close_xx"
                 });
             }

             if (isUserLocked || isUserDeleted) {
                 SignOut();
             }

             $("#divChannelsCategory ul").empty().html(browseMenu);
                                   
             if (isPro == false) {
                 $("#vrMenu").remove();
                 $("#vsMenu").remove();
             }

             CreateChannel.Init(channelCount, maxUserChannelCount);
             
         });

         function ShowConfirm(message) {
             reset();
             alertify.confirm(message, function (e) {
                 if (e) {
                     alertify.success("You've selected ok.");
                 } else {
                     alertify.error("You've selected cancel.");
                 }
             });
             return false;
         };

         function reset() {
             $("#toggleCSS").attr("href", "/Plugins/alertify/alertify.default.css");
             alertify.set({
                 labels: {
                     ok: "OK",
                     cancel: "Cancel"
                 },
                 delay: 5000,
                 buttonReverse: false,
                 buttonFocus: "ok"
             });
         }

         function displayNotification(message) {
             reset();
             alertify.set({ delay: 10000 });
             alertify.log(message);
             return false;
         };

         function displaySuccessNotification(message) {
             reset();
             alertify.set({ delay: 10000 });
             alertify.success(message);
             return false;
         };

         function showPopup() {
             var $modal = $find('myid');

             $('#imgChannelAvatar').attr('src', '/images/comingSoonBG.jpg')
             $('#txtChannelName').val('');
             $('#lblChannelUrl').text('');
             $('#ddlChannelCategory').prop('selectedIndex', 0);

             if ($modal) {
                 $modal.show()
             }
         }
    </script>  
    
   <div id="divTopWrapper" class="ShadowDropDown ">
        <div id="divTop">
          
       
                <a href="/">
                <div id="divLogo">  
                      <img src="/images/Srtimm-LOGO.svg" />
                </div>
                    </a>



          <div id="divTopNavAfterLogin">

     <div id="topLeftHolder">


              <div id="divTopMenuHolder">
                    <div id="divTopMenu" >
                        <a onclick="ToggleMenu()" id="ancMenu">menu
                        <div id="MenuImg" class="divImg">▼</div>
                        </a>
                    </div>
    <div id="menuTopList">
        <div class="ddHolder"> <span class="tr"></span></div>
						    <ul class="menuListDropdown">
                                <li><a href="<%=boardUrl%>">My Network</a></li>
                                <li><a runat="server" id="ancMyStudio">Production Studio</a></li>
                                <li><a href="/<%=userUrl%>/favorite-channels">favorite channels</a></li>
                                <li><a href="/<%=userUrl%>/watch-it-later">watch later</a></li>
                                <li id="vrMenu"><a href="/<%=userUrl%>/video-room">video room</a></li>
                                <li id="vsMenu"><a href="/video-store">video store</a></li>
                                <li><a href="/guides">how to</a></li>
                                <li><a href="/" class="liBusiness">Business Solutions</a></li>
                            </ul>
    </div>
                  </div>
             

                

    </div>
           
    <div id="topRightHolder">
        <div class="btnCreateChannel">
           
            <a onclick="CreateChannel.RedirectToCreateChannel()" id="createChannelLink" class="spnCreateChannel" > create channel</a>
                        </div>

       
            
       <div id="helloMenuList">
                    <div class="ddHolder"><span class="trHello"></span></div> 

						    <ul class="menuListDropdown" id="profMenuDdl">
                        <li><a href="/<%=userUrl%>/profile">my profile</a></li> 
                        <li class="last"><a id="a1" onclick="SignOut()" >sign out</a></li>
						    </ul>
    </div>  

        <a href="/" class="businessSolAfterLog">Business Solutions</a>

              </div>

            </div>

                
   
         
        </div>
    </div>



<div class="mainTopHoplder">
    <a href="/browse-channel?category=AllChannels" class="absGuide">Watch Now</a>
    <div class="advancedSearch">
                    <a class="advanced" title="advanced search" href="#" onclick="GetSearchControlAfter()"> 
                        <span class="spnAdvSearch"> search </span>
                        <div class="AdvSearchImgHN"></div>
                    </a>
                        </div>
    <div class="advSearchFullHolder">
        <a href="/" class="divLogo"><img src="/images/Srtimm-LOGO.svg" /> </a>
<div class="advSearchFull"> Search

    <div class="advSearchButton">&#128269</div>
        <div class="advSearchOptions">Please Select  &#x25BC </div>

</div>
        <div class="advSearchClose"></div>
    </div>
                    
      <div id="divChannelsCategory">
          <div class="closeSymbol" onclick="CloseChannelCategoryMenu()">&#10006;</div>
                   <div class="ddHolder"> <span class="trBrowseChannels"></span></div>
                    <ul>
                    </ul>
                 <div class="galleryView">
                     <a href="/all-channels?category=All%20Channels" class="btnGalleryView">Channel Gallery</a>
                 </div>
    </div>
    <div class="subBlock subBlockBusiness" onclick="CloseSubBlockBusiness()">
                    <div class="closeSymbol">&#10006;</div>
                   <div class="ddHolder"> <span class="trBusinesses"></span></div>
        <div class="businessesMenuBlock">
              <a href="/" class="submobileNav">Plans and Pricing</a>
            <div class="businessesMenuBlockText">Bring your business or organization to a whole new level</div>
             <a href="/" class="businessesMenuBlockMore">learn more</a>
        </div>
              <div class="businessesMenuBlock">
            <a href="/create-tv/features-and-benefits" class="submobileNav">features & benefits</a>
            <div class="businessesMenuBlockText">Get access to unique features and benefits</div>
                  <a href="/create-tv/features-and-benefits" class="businessesMenuBlockMore">learn more</a>

                    </div>
                  <div class="businessesMenuBlock businessesMenuBlockNoBorder">
             <a href="/create-tv/pricing" class="submobileNav">plans & pricing</a>
            <div class="businessesMenuBlockText">Set yourself apart, and choose from affordable plans</div>
                      <a href="/create-tv/pricing" class="businessesMenuBlockMore">learn more</a>

                        </div>
                </div>
     <div class="mobileMenu" ></div>
<div class="mobileMenuOpen">
              <div class="fullvh">
                    <a class="mobileNav mobileNavCreateChannel customIconNone" onclick="CreateChannel.RedirectToCreateChannel()">Create Channel</a>
                                                <div class="businessesMobileHolder" >
            <a href="/create-tv/pricing" class="mobileNav mobileNavHeader businessesMobileIcon  businessesMobileMenu" <%--onclick="ToggleBusinessMobileMenu()"--%> >Plans & Pricing
                <%--  <div id="divImgBusinesses" class="divImg divImgBusinesses">&#10148;</div>--%>
            </a>
               
                  </div>
                  <a href="/#FAQ" class="mobileNav customIconFAQ">FAQ </a>
                               <a class="mobileNav" href="<%=boardUrl%>">My Network</a>
                               <a  class="mobileNav customIconProdStudio" runat="server" id="ancMyStudioMobile">Production Studio</a>
                                <a  class="mobileNav customIconFav" href="/<%=userUrl%>/favorite-channels">favorite channels</a>
                                <a class="mobileNav customIconWLater" href="/<%=userUrl%>/watch-it-later">watch later</a>

           
              <div class="guideMobileHolder">
                  <div id="divBrowseChannelsHolder">
             
                    <div id="divBrowseChannels" >
                        <a class="mobileNav mobileNavHeader tvGuide" onclick="ToggleChannels()">Watch Now
                        <div id="divImg" class="divImg">&#10148;</div>
                        </a>
                    </div>
                  </div>
                
    
                  </div>
       
   <a href="/all-channels?category=All%20Channels" class="mobileNav customGallery">Channel Gallery</a>
            
                
              <a href="/guides" class="mobileNav customHowTo">How To</a>
            <a class="mobileNav customProfile" href="/<%=userUrl%>/profile">my profile</a>
                <a  class="mobileNav customSignOut" onclick="SignOut()" >sign out</a>
         </div>
 </div>
<a href="/" class="divLogo"><img src="/images/Srtimm-LOGO.svg" /> </a>
    <a href="/create-tv/pricing#pricingAnc" class="pricingHomeAfter" >Pricing </a>
<%--   <asp:Label runat="server" ClientIDMode="Static" Id="spnHello" onclick="ToggleProfMenu()">Hello, <%=userName%> <div id="helloImg" class="divImg">▼</div></asp:Label>--%>
   <div class="spnHelloHolder"> <asp:Label runat="server" ClientIDMode="Static" Id="spnHello" onclick="ToggleProfMenu()">Hello, <%=userName%></asp:Label> </div>
      <a href="/create-tv/pricing" id="exploreBusinessOpt">Get Started</a>
<%--<a class="createChannelTopRRight" onclick="CreateChannel.RedirectToCreateChannel()">Create Channel</a>--%>

 </div>
<div class="secondaryMenu">
    <div class="businessMenu">
             <%--       <a href="../home" class="businessNavigation homeIcon"></a>--%>
                    <a href="/" class="businessNavigation businessNavigationA"> business solutions</a>
                    <a href="/create-tv/features-and-benefits"  class="businessNavigation businessNavigationB">features & benefits</a>
                    <a href="/create-tv/pricing" class="businessNavigation businessNavigationC">plans & pricing</a>
        <a href="/#FAQ"   class="businessNavigation businessNavigationC">FAQ</a>
                     
                     
                </div>
</div>


    <asp:Button ID="Button2" runat = "server" Text = ""  style="display:none" /> 

    <div id="modalInfo" >
        <img id="dialogImage"/>
        <span id="infoMessage"></span>
        <div class="confirmButtons">
            <a id="okButton" onclick="CloseMessage()">ok</a>
        </div>
    </div>

    <div class="playerboxtutorial" style="display: none;">
        <a id="relatedtutorial" class="playertutorial"></a>
        <div id="content-containertutorial">
        </div>
        <a id="close_x" class="close close_x closePlayerBox" href="#">close</a>
    </div>

    <div class="playerbox-youtube" style="display: none;">
        <div id="related-youtube" class="player"></div>
        <div id="content-container-youtube">
        </div>
        <a id="close_x-youtube" class="close close_x closePlayerBox" href="#">close</a>
    </div>

    <div class="playerbox-vimeo" id="vimeoBox" style="display: none;">
        <div class="playerVimeo"></div>
        <a id="close_x-vimeo" class="close close_x closePlayerBox" href="#">close</a>
    </div>

    <div class="playerbox-strimm" id="strimmBox" style="display: none;">
        <div id="playerFlow" class="player" style="height: 546px;width:728px;" data-ratio="0.4167" >
       </div>        
        <a id="close_x-strimm" class="close close_x closePlayerBox" href="#">close</a>
    </div>

    <div id="userInterestsModal"<%-- style="width: 1000px; display: none;--%>>
        <a id="close_xx" class="close close_x closeInterestsPopup" href="#" onclick="interestManager.closePopup()">close</a>
        <div id="userInterests_Body">
            <div id="userInterests_Welcome">
                <div id="userInterests_Greeting" class="pageTitle interestPopupTitle">Hi <%=firstName%></div>
                <div id="userInterests_Message">In order to provide you with the best watching and social experience, 
                    please select 5 categories you are most interested in:</div>
            </div>
            <div id="userInterests_Interests">
                <table id="userInterests_Container">
                    <tr>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkAnimalsAndPets" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Animals & Pets</p>
                            </div>
                        </td>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkHomeGarden" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Home & Gargen</p>
                            </div>
                        </td>                    
                    </tr>
                    <tr>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkAutomotive" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Automotive</p>
                            </div>
                        </td>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkHowTo" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">How To</p>
                            </div>
                        </td>                    
                    </tr>
                    <tr>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkComedy" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Comedy</p>
                            </div>
                        </td>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkKidsFamily" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Kids & Family</p>
                            </div>
                        </td>                    
                    </tr>
                    <tr>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkCookingFood" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Cooking & Food</p>
                            </div>
                        </td>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkMusicArt" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Music & Arts</p>
                            </div>
                        </td>                    
                    </tr>
                    <tr>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkDiscovery" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Discovery</p>
                            </div>
                        </td>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkOther" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Other</p>
                            </div>
                        </td>                    
                    </tr>
                    <tr>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkEducation" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Education</p>
                            </div>
                        </td>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkReligion" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Religion</p>
                            </div>
                        </td>                    
                    </tr>
                    <tr>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkEntertainment" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Entertainment</p>
                            </div>
                        </td>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkSports" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Sports</p>
                            </div>
                        </td>                    
                    </tr>
                    <tr>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkFashionStyle" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Fashion & Style</p>
                            </div>
                        </td>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkTechnology" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Technology</p>
                            </div>
                        </td>                    
                    </tr>
                    <tr>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkGaming" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Gaming</p>
                            </div>
                        </td>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkTravelLeisure" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.checkSelectedAtleastOne()"/>
                                <p class="checkTitle">Travel & Leisure</p>
                            </div>
                        </td>                    
                    </tr>
                    <tr>
                        <td>
                            <div class="categoryBlock">
                                <input id="chkHealthFitness" type="checkbox"  class="checkBoxInterestSelect chkGreenMarkStyle" checked="checked" onclick="interestManager.updateSelection()"/>
                                <p class="checkTitle">Health & Fitness</p>
                            </div>
                        </td>
                        <td>
                        </td>                    
                    </tr>
               </table>
            </div>
            <div id="userInterests_Actions">
                <div id="userInterests_Submit" class="userInterests_SubmitDisable" onclick="interestManager.saveUserInterests()">Submit</div>
            </div>
        </div>
    </div>
