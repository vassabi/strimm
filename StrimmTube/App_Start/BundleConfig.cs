using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace StrimmTube.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Strimm.Master
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/master/js")
              .Include(
                "~/jquery/jquery-1.11.1.min.js",
                "~/jquery/jquery-migrate-1.2.1.min.js",
                "~/JQUICustom/jquery-ui.min.js",
                "~/JS/jquery.cookie.js",                
                "~/JS/Main.js",
                "~/JS/Controls.js",
                 "~/JS/CreateChannel.js",
                  "~/JS/Search.js",
                "~/Plugins/popup/jquery.lightbox_me.js",
                "~/jquery/Jcrop/js/jquery.Jcrop.min.js",
                 "~/Plugins/jquery.cropit.min.js",
                "~/Plugins/dropdown/modernizr.custom.79639.js",
                "~/JS/Facebook.js",
                    "~/jquery/jquery.query-object.js",
                    "~/Scripts/moment.min.js",
                    "~/Scripts/moment-timezone.js",
                     "~/Scripts/moment-timezone-with-data-2012-2022.min.js",
                     "~/Scripts/moment-timezone-with-data.min.js",                    
                    "~/JS/timezones.full.min.js"
                ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/master/css")
                 .Include(
          "~/JQUICustom/jquery-ui.structure.min.css",
          "~/JQUICustom/jquery-ui.theme.min.css",
          "~/css/reset.css",
          "~/css/CSS.css",
          "~/css/skeleton.css",
          "~/css/mediaCSS.css",
          "~/css/CreateChannel.css",
          "~/css/OttSettings.css",
           "~/css/search.css"
          ));

            //Watch it Later 
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/watchItLater/js")
            .Include(
              "~/JS/WatchItLater.js"
            ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/schedule/css")
                .Include(
                 "~/css/Schedule.css"
                ));

            //browse channels
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/browseChannel/js")
           .Include(
             "~/JS/BrowseChannels.js"
           ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/channelpage/css")
                .Include(
                 "~/css/ChannelPageCSS.css"
                ));
			BundleTable.Bundles.Add(new StyleBundle("~/bundles/channelpageNew/css")
               .Include(
                "~/css/channelPageNew.css",
                 "~/Plugins/RateIt/src/rateit.css"
               ));
			BundleTable.Bundles.Add(new StyleBundle("~/bundles/channelpageNewFP/css")
             .Include(
              "~/css/channelPageNew.css",
               "~/Plugins/RateIt/src/rateit.css",
             "~/Flowplayer7/skin/skin.css",
            "~/Flowplayer7/cc-button.css",
					"~/Flowplayer7/settingsmenu.css",
					"~/css/flowplayer.override.css"
            ));

            // embeddedChannel

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/embedded/css")
               .Include(
                    "~/JQUICustom/jquery-ui.structure.min.css",
                    "~/JQUICustom/jquery-ui.theme.min.css",
                    "~/css/reset.css",
                    "~/css/CSS.css",
                    "~/css/skeleton.css",
                    "~/css/mediaCSS.css",
                    "~/css/embeddedChannel.css",
                    "~/Plugins/RateIt/src/rateit.css",
                    "~/Plugins/Scroller/scroller.css"
               ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/embeddedFP/css")
               .Include(
                    "~/JQUICustom/jquery-ui.structure.min.css",
                    "~/JQUICustom/jquery-ui.theme.min.css",
                    "~/css/reset.css",
                    "~/css/CSS.css",
                    "~/css/skeleton.css",
                    "~/css/mediaCSS.css",
                    "~/css/embeddedChannel.css",
                    "~/Plugins/RateIt/src/rateit.css",
                    "~/Plugins/Scroller/scroller.css",
                    "~/Flowplayer7/skin/skin.css",
                    "~/Flowplayer7/cc-button.css",
					"~/Flowplayer7/settingsmenu.css",
					"~/css/flowplayer.override.css"
               ));

			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/embedded/js")
               .Include(
                    "~/jquery/jquery-1.11.1.min.js",
                    "~/jquery/jquery-migrate-1.2.1.min.js",
                    "~/JQUICustom/jquery-ui.min.js",
                    "~/JS/jquery.cookie.js",
                    "~/JS/Main.js",
                    "~/JS/Controls.js",
                    "~/Plugins/date.format.js",
                    "~/Plugins/popup/jquery.lightbox_me.js",
                    "~/Plugins/dropdown/modernizr.custom.79639.js",
                    "~/JS/Facebook.js",
                    "~/jquery/jquery.query-object.js",
                    "~/JS/EmbeddedChannel.js",
                    "~/Plugins/ResizeSensor/ResizeSensor.js",
                    "~/JS/swfobject.js",
                    "~/Plugins/RateIt/src/jquery.rateit.min.js",
                //"~/JS/Froogaloop.js",
                    "~/Plugins/Scroller/nanoscroller.min.js"
               ));
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/embeddedFP/js")
              .Include(
                   "~/jquery/jquery-1.11.1.min.js",
                   "~/jquery/jquery-migrate-1.2.1.min.js",
                   "~/JQUICustom/jquery-ui.min.js",
                   "~/JS/jquery.cookie.js",
                   "~/JS/Main.js",
                   "~/JS/Controls.js",
                   "~/JS/EmbeddedChannelFP.js",
                   "~/Plugins/date.format.js",
                   "~/Plugins/popup/jquery.lightbox_me.js",
                   "~/Plugins/dropdown/.custom.79639.js",
                   //"~/JS/Facebook.js",
                   //"~/jquery/jquery.query-object.js",
                  //"~/Plugins/ResizeSensor/ResizeSensor.js",
                   //"~/JS/swfobject.js",
                   //"~/Plugins/RateIt/src/jquery.rateit.min.js",
                   "~/Plugins/Scroller/nanoscroller.min.js",
                    //"~/Flowplayer7/flowplayer.js",
                    //"~/Flowplayer7/youtube-7.0.0.js",
                    //"~/Flowplayer7/vimeo-7.0.0.js",
                    //"~/Flowplayer7/dailymotion-7.0.0.js",
                    //"~/Flowplayer7/cc-button-7.0.0.js",
                    //"~/Flowplayer7/settingsmenu-7.0.0.js",
                    //"~/Flowplayer7/embed.min.js",
                      "~/Scripts/moment.min.js",
                    "~/Scripts/moment-timezone.js",
                     "~/Scripts/moment-timezone-with-data-2012-2022.min.js",
                     "~/Scripts/moment-timezone-with-data.min.js"
              ));
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/embeddedPass/js")
              .Include(
                   "~/jquery/jquery-1.11.1.min.js",
                   "~/jquery/jquery-migrate-1.2.1.min.js",
                   "~/JQUICustom/jquery-ui.min.js",
                   "~/JS/jquery.cookie.js",
                   "~/Plugins/date.format.js",
                   "~/Plugins/dropdown/modernizr.custom.79639.js",
                   "~/jquery/jquery.query-object.js",
                   "~/JS/swfobject.js",
                   "~/Plugins/Scroller/nanoscroller.min.js"
              ));

            //Channel
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/channelPage/js")
          .Include(
            "~/Plugins/ResizeSensor/ResizeSensor.js",
            "~/JS/ChannelPageJS.js",
            "~/JS/swfobject.js",
            "~/Plugins/RateIt/src/jquery.rateit.min.js"
          ));
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/channelPageFP/js")
          .Include(
            "~/Plugins/ResizeSensor/ResizeSensor.js",
            "~/JS/ChannelPageFP.js",
					"~/Plugins/RateIt/src/jquery.rateit.min.js" //,
           //"~/Flowplayer7/flowplayer.js",
           // "~/Flowplayer7/youtube-7.0.0.js",
           // "~/Flowplayer7/vimeo-7.0.0.js",
           // "~/Flowplayer7/dailymotion-7.0.0.js",
           // "~/Flowplayer7/cc-button-7.0.0.js",
           // "~/Flowplayer7/embed.min.js",
           // "~/Flowplayer7/settingsmenu-7.0.0.js"
          ));
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/playerControlsTest/js")
         .Include(
           "~/Plugins/ResizeSensor/ResizeSensor.js",
           "~/JS/playerControlsTest.js",
           "~/JS/swfobject.js",
           "~/Plugins/RateIt/src/jquery.rateit.min.js"
         ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/channelPage/css")
                .Include(
                "~/css/ChannelPageCSS.css",
                 "~/Plugins/RateIt/src/rateit.css"
                ));

			var optimizeCssJs = bool.Parse(ConfigurationManager.AppSettings["OptimizeCssJs"] ?? string.Empty);


            //delete after implementation
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/prototype/js")
         .Include(
           "~/Plugins/ResizeSensor/ResizeSensor.js",
           "~/JS/PlayerPrototype.js",
           "~/JS/swfobject.js",
           "~/Plugins/RateIt/src/jquery.rateit.min.js"
         ));

            //dashboard
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/dashboard/js")
         .Include(
           "~/JS/Board.js",
            "~/JS/swfobject.js",
            "~/Plugins/jquery.cropit.min.js",
             "~/Plugins/PowerTour_v3.2.0/js/powertour/powertour.3.2.0.min.js"
         ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/dashboard/css")
                .Include(
                "~/css/DashBoardCss.css"
                ));

            //default page/home
			BundleTable.Bundles.Add(new StyleBundle("~/bundles/home/css")
                .Include(
                "~/css/HomeNew.css",
                "~/css/DefaultCSS.css"
                ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/home_playerBlack/css")
    .Include(
    "~/css/HomeNew_playerBlack.css",
    "~/css/DefaultCSS_playerBlack.css"
    ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/home_2_21_16/css")
               .Include(
                   "~/css/home_2_21_16.css",
                   "~/css/DefaultCSS_2_21_16.css"
               ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/Default_OLD2_CSS/css")
.Include(
"~/css/Default_OLD2_CSS.css"
));
			BundleTable.Bundles.Add(new StyleBundle("~/bundles/homeNew_2_21_16/css")
               .Include(
               "~/css/HomeNew_2_21_16.css",
               "~/css/Default_New_2_21_16.css"
               ));
            //favorite channel
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/favoritechannel/js")
         .Include(
           "~/JS/FavoriteChannels.js"
         ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/favoritechannel/css")
                .Include(
                "~/css/DashBoardCss.css"
                ));

            //profile 
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/profile/js")
        .Include(
          "~/JS/Profile.js"
        ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/profile/css")
                .Include(
                "~/css/profileCSS.css"
                ));

            //schedule
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/schedulePage/js")
       .Include(
         "~/Plugins/popup/modernizr.custom.79639.js",
         //"~/Bootstrap/bootstrap-datepicker.js",
         "~/jquery/jquery.timepicker.js",
                    "~/JS/Controls.js",
          "~/JS/Schedule.js",
         "~/JS/swfobject.js",
         "~/Scripts/jquery.ui.core.js",
         "~/Scripts/jquery.ui.widget.js",
         "~/Scripts/jquery.ddslick.js",
         "~/JS/Froogaloop.js"
         //"~/Flowplayer7/flowplayer.js",
         //           "~/Flowplayer7/youtube-7.0.0.js",
         //           "~/Flowplayer7/vimeo-7.0.0.js",
         //           "~/Flowplayer7/dailymotion-7.0.0.js",
         //           "~/Flowplayer7/cc-button-7.0.0.js",
         //           "~/Flowplayer7/settingsmenu-7.0.0.js"
       ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/schedulePage/css")
                .Include(
                "~/css/reset.css",
                "~/JQUICustom/jquery-ui.structure.min.css",
                "~/css/skeleton.css",
                "~/JQUICustom/jquery-ui.theme.min.css",
                //"~/Bootstrap/bootstrap-datepicker.css",
                //"~/jquery/jquery.timepicker.css",
                "~/css/Schedule.css",
                "~/Plugins/Scroller/scroller.css",
                "~/Plugins/DHTMLXCalendar/sources/dhtmlxCalendar/codebase/dhtmlxcalendar.css",
                "~/Plugins/DHTMLXCalendar/sources/dhtmlxCalendar/skins/skyblue/dhtmlxcalendar.css",
                 "~/Flowplayer7/skin/skin.css",
            "~/Flowplayer7/cc-button.css",
					"~/Flowplayer7/settingsmenu.css",
					"~/css/flowplayer.override.css"
                ));

            //signup
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/signup/js")
     .Include(
       "~/JS/SignUp.js"
     ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/signup/css")
                .Include(
                "~/css/signUpCSS.css"
                ));

            //ChannelManagmentUC
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/channelmanagmentUC/js")
             .Include(
               "~/Plugins/DHTMLXCalendar/sources/dhtmlxCalendar/codebase/dhtmlxcalendar.js",
               "~/Plugins/DHTMLXCalendar/sources/dhtmlxcommon/codebase/dhtmlxcommon.js",
               "~/Plugins/DHTMLXCalendar/sources/dhtmlxcommonpopup.js",
               "~/Plugins/DHTMLXCalendar/sources/dhtmlxpopup.js",
               //"~/Plugins/WalkThru/js/powertour.2.5.1.js"
            "~/Plugins/PowerTour_v3.2.0/js/powertour/powertour.3.2.0.min.js"
                ));

			BundleTable.Bundles.Add(new StyleBundle("~/bundles/channelmanagmentUC/css")
                 .Include(
                //"~/Plugins/DHTMLXCalendar/sources/dhtmlxCalendar/codebase/skins/dhtmlxcalendar_dhx_skyblue.css",
                //"~/Plugins/DHTMLXCalendar/codebase/dhtmlxcalendar.css",
                //"~/Plugins/DHTMLXCalendar/sources/dhtmlxpopup_dhx_terrace.css",
            //"~/Plugins/WalkThru/css/powertour.2.5.1.css",
            //"~/Plugins/WalkThru/css/powertour-style-clean.css",
            //"~/Plugins/WalkThru/css/powertour-connectors.css",
            //"~/Plugins/WalkThru/css/animate.min.css"
            "~/Plugins/PowerTour_v3.2.0/css/powertour/animate.min.css",
            "~/Plugins/PowerTour_v3.2.0/css/powertour/powertour-connectors.min.css",
            "~/Plugins/PowerTour_v3.2.0/css/powertour/powertour-mobile.css",
            "~/Plugins/PowerTour_v3.2.0/css/powertour/powertour-style-clean.min.css",
            "~/Plugins/PowerTour_v3.2.0/css/powertour/powertour.min.3.2.0.css"
          ));

            BundleTable.EnableOptimizations = optimizeCssJs;  
            //BundleTable.EnableOptimizations = false;
        }
    }
}