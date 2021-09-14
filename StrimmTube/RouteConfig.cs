using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.FriendlyUrls;
using System.Web.Routing;
namespace StrimmTube
{
    public static class RouteConfig
    {
        
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

            routes.MapPageRoute("emailPreferences", "email-preferences", "~/unsubscribe.aspx");
            routes.MapPageRoute("preferencesUpdateSuccess", "preferences-update-success", "~/updatedPref.aspx");
            routes.MapPageRoute("emailsFullUnsubscribe", "emails-full-unsubscribe", "~/unsubscribeFull.aspx");
            routes.MapPageRoute("emailPreferencesWithEmail", "email-preferences/email={UserEmail}", "~/unsubscribe.aspx");
            //routes.MapPageRoute("businessRoute", "create-tv", "~/businessPage.aspx");
            routes.MapPageRoute("religiousOrganizations", "religion", "~/ReligiousOrganizations.aspx");
            routes.MapPageRoute("businessPricing", "create-tv/pricing", "~/business-pricing.aspx");
            //business-features.aspx.
            routes.MapPageRoute("businessFeatures", "create-tv/features-and-benefits", "~/business-features.aspx");
            routes.MapPageRoute("NoticeAndTakedown", "notice-and-takedown", "~/NoticeAndTakedown.aspx");

            routes.MapPageRoute("HLS_M3U8", "hls/{ChannelId}.m3u8", "~/HLS.aspx");
            routes.MapPageRoute("HLS_JSON", "hls/{ChannelId}.json", "~/HLS.aspx");

            #region(footer links)
            //about            
            routes.MapPageRoute("about", "about", "~/about.aspx");

            //how it works            
            routes.MapPageRoute("howitworks", "how-it-works", "~/HowItWorks.aspx");

            //create
            routes.MapPageRoute("create", "create", "~/Create.aspx");

            //tips
            routes.MapPageRoute("tips", "tips", "~/Tips.aspx");

            //contact
            routes.MapPageRoute("contactus", "contact-us", "~/Contact.aspx");

            //terms of use
            routes.MapPageRoute("termsofuse", "terms-of-use", "~/Terms.aspx");
            //FAQ
            routes.MapPageRoute("FAQ", "FAQ", "~/FAQ.aspx");
            //company
            routes.MapPageRoute("company", "company", "~/company.aspx");

            //indie
            routes.MapPageRoute("indie", "indie", "~/indie.aspx");

            //featured channels
            routes.MapPageRoute("featuredchannels", "featured-channels", "~/FeaturedChannels.aspx");
            routes.MapPageRoute("learnmore", "learn-more", "~/LearnMore.aspx");
           // PRESSREALEASE

            //copy              name of routing   url name   page
            routes.MapPageRoute("pressrelease", "press", "~/Press Release.aspx");

            routes.MapPageRoute("october_20_15", "press/strimm-inc-introduces-new-game-changing-online-video-platform", "~/PressRelease/October_20_2015.aspx");
            routes.MapPageRoute("february_02_16", "press/strimm-tv-announces-new-video-providers-for-its-growing-public-television-platform", "~/PressRelease/February_02_2016.aspx");
            routes.MapPageRoute("august_02_2016", "press/strimm-tv-emerging-leader-in-internet-latest-new-frontier-social-internet-television", "~/PressRelease/August_02_2016.aspx");
            routes.MapPageRoute("october_11_2016", "press/new-type-of-customer-outreach-offered-by-social-internet-television-strimm-tv", "~/PressRelease/October_11_2016.aspx");
           //END

            routes.MapPageRoute("blog", "blog", "~/Blog.aspx");

            routes.MapPageRoute("copyright", "copyright", "~/Copyrights.aspx");

            //privacy policy
            routes.MapPageRoute("privacypolicy", "privacy-policy", "~/PrivacyPolicy.aspx");
            #endregion
            //home
              routes.MapPageRoute("home", "home", "~/Default.aspx");
              //routes.MapPageRoute("homeTest", "home-slider", "~/Default_OLD2.aspx");
              routes.MapPageRoute("homeTest1", "home2", "~/Default_OLD3.aspx");
            // search videos
            routes.MapPageRoute("advancedSearch", "advanced-search", "~/Search.aspx");
            //sign up
            routes.MapPageRoute("signup", "sign-up", "~/SignUp.aspx");
            //confirmation page
            routes.MapPageRoute("welcome", "welcome", "~/Confirmation.aspx");
            //thank you page
            routes.MapPageRoute("thankyou", "thank-you", "~/ThankYou.aspx");
            
            // browse channel
            routes.MapPageRoute("allchannels", "all-channels", "~/BrowseChannel.aspx");
            routes.MapPageRoute("browsechannel", "browse-channel", "~/channelPageNew.aspx"); //"~/BrowseChannel.aspx");
           
            //channel password
            routes.MapPageRoute("channelprotect", "channel-protect", "~/ChannelPassword.aspx");
            routes.MapPageRoute("embedchannelprotect", "embed-channel-protect", "~/EmbeddedChannelPassword.aspx");

            
            //profile
            routes.MapPageRoute("profile", "{UserName}/profile", "~/Profile.aspx");

            //guides
            routes.MapPageRoute("guides", "guides", "~/Guides.aspx");
            //profile-other-country
            routes.MapPageRoute("profileothercountry", "profile-other-country", "~/OtherProfile.aspx");
            //create channel
            //routes.MapPageRoute("createchannel", "{UserName}/create-channel", "~/CreateChannel.aspx");
            
            //Monetize
            routes.MapPageRoute("monetize", "monetize", "~/Monetize.aspx");

            //public Library
            routes.MapPageRoute("publiclibrary", "public-library/{ChannelName}", "~/PublicLibrary.aspx");
            // schedule
            routes.MapPageRoute("schedule", "{UserName}/my-studio/{ChannelName}", "~/Schedule.aspx");
            routes.MapPageRoute("schedule2", "{UserName}/my-studio2/{ChannelName}", "~/Schedule2.aspx");
            // search videos
            //routes.MapPageRoute("addbykeywords", "add-by-keyword/{ChannelName}", "~/AddVideoByKeyword.aspx");
            //routes.MapPageRoute("addbyurl", "add-by-url/{ChannelName}", "~/AddVideoByUrl.aspx");
            //routes.MapPageRoute("videosearch", "add-videos", "~/VideoSearchTube.aspx");
          
            //archive
            routes.MapPageRoute("archiveTubes", "{UserName}/watch-it-later", "~/Archive.aspx");
            //control panel 
            routes.MapPageRoute("controlpanel", "control-panel", "~/ControlPanel.aspx");
            //followers
            routes.MapPageRoute("ifollow", "followers", "~/IFollow.aspx");
            //fave channels
            routes.MapPageRoute("favoritechannels", "{UserName}/favorite-channels", "~/FavoriteChannels.aspx");
            //timetables
            routes.MapPageRoute("timetables", "timetable", "~/timeTable.aspx");
            //ROUTS TO WELCOME ON UGLY PAGE
            routes.MapPageRoute("welcometostrimm", "welcome-to-strimm", "~/WelcomToStrimm.aspx");
            //player
            routes.MapPageRoute("passwordRecovery", "password-recovery", "~/PaswordRecovery.aspx"); 
            routes.MapPageRoute("player", "player", "~/player.aspx");
            routes.MapPageRoute("paypalPayment", "payment", "~/PaypalPayment.aspx");
            routes.MapPageRoute("basicPackage", "basic-package", "~/BasicPackage.aspx");
      

            routes.MapPageRoute("standardPackage", "standard-package", "~/StandardPackage.aspx");

            routes.MapPageRoute("enterprisePackage", "enterprise-package", "~/EnterprisePackage.aspx");
            routes.MapPageRoute("professionalPackage", "professional-package", "~/ProfessionalPackage.aspx");
            routes.MapPageRoute("professionalPlusPackage", "professional-plus-package", "~/ProfessionalPlusPackage.aspx");
            //routes.MapPageRoute("createboard", "create-board", "~/CreateBoard.aspx");
            //routes.MapPageRoute("boardUserId", "{userId}", "~/Board.aspx");

            routes.MapPageRoute("videoroom", "{UserName}/video-room", "~/VideoRoom.aspx");
            routes.MapPageRoute("videostore", "video-store", "~/VideoStore.aspx");
            
            //update channel
            routes.MapPageRoute("fchannel", "featured-channel", "~/FChannel.aspx");
            routes.MapPageRoute("prototype", "prototype/player", "~/PlayerPrototype.aspx");
            //routes.MapPageRoute("channel", "{ChannelOwnerUserName}/{ChannelURL}", "~/ChannelPage.aspx");
            routes.MapPageRoute("channel", "{ChannelOwnerUserName}/{ChannelURL}", "~/channelPageNew.aspx");
            routes.MapPageRoute("channelMobile", "mobile/{ChannelOwnerUserName}/{ChannelURL}", "~/ChannelPageMobile.aspx"); //"~/BrowseChannel.aspx");
            //routes.MapPageRoute("channeltest", "{ChannelOwnerUserName}/test/{ChannelURL}", "~/playerControlsTest.aspx");
            
            routes.MapPageRoute("embeddedChannel", "embedded/{ChannelOwnerUserName}/{ChannelURL}", "~/EmbededChannel.aspx");
           
            routes.MapPageRoute("boardUserUrl", "{UserName}", "~/Dashboard.aspx");

           
        }
    }
}
