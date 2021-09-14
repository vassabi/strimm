<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Guides.aspx.cs" Inherits="StrimmTube.Guides" %>


<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
How To Create TV Online | Online TV | Internet TV
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Learn how to create tv online, create live online tv channels and your own internet tv | Strimm TV" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
 <link rel="canonical" href="https://strimm.com/guides" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
<script src="Plugins/popup/jquery.lightbox_me.js"></script>
    <link href="css/guidesCSS.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <div class="learnMoreButtonHolderNoMarg">
  
     
            <h1 class="h1FAQ">How to</h1>
     
      <div class="walkthruHolder">
            <h4>Let us walk you through the major elements of the platform</h4>
            
            <div class="walkthruUL">
                <a class="faq" href="guides#MyNetwork">My Network</a>
                <a class="faq" href="guides#MyStudio">Production Studio</a>
               <a class="faq margin5 " href="guides#Channel">Channel </a> 
                <a class="faq margin5 " href="guides#Embedding">Channel Embedding </a> 
            </div>
          </div>
</div>

     <a name="MyNetwork"></a>

            <div id="content">
    <div id="divBoardContent">
        <div id="divContentWrapper">






            <div class="miniBlock">
         <div class="pageTitleHolder">
            <h5 class="howToMiniHead">My Network
                <div class="btnWatchVideoHolderHToo">
                <a onclick="ShowTutorialPlayer('<%=howToPageTutorialVideoId %>', false)" class="ancHowTo"> watch tutorial </a>
                </div>
             </h5>
             
           </div> 
                <div class="howToImgRSide flowLeft"><img src="images/whatIsIcons/whatIsIcons-create.png" /></div>
                
               
              <p> The My Network page contains information about you or your business, as well as a complete list of channels that  you have created. Your Public Name serves as part of the URL for this page.  </p> 
<h6>Upload Your Profile Images</h6> 
<p>After creating your account, the first step is to upload your personal avatar and banner. In the header area of the My Network page, find and click the camera icons to begin uploading your images for each section.</p>  
<h6>Create a Channel</h6>
                <p>Below your new avatar is a “Create Channel” button. Click this to open up the Create Channel form. Make sure to complete all necessary  fields.  You may create and manage as many channels as needed.</p>
<p>Once your channel is created, the channel thumbnail will feature two icons: a gear in the top-right corner which leads to your Production Studio and a play button in the top-left corner to watch your channel.</p>
<%-- <ol>
<li>The box with “+” button opens a “create channel” form.  Up to 12 channels can be created per account.</li>
<li>The Channel box (once channel is created) has the following icons:</li>
           
<li>a) “Gear” – opens Production Studio page </li>
<li>b) “Play” - opens Channel Preview page</li>
                </ol> --%>
                  <a name="MyStudio""></a>
<h6>Bio, Top Channels, and More </h6>
<p>On the right side of your My Network page, you have the opportunity to share your story with followers through adding a bio. For convenience and ease of  access, we have also added our Top Channels and a list of your Favorites right on the My Network page.</p>
   </div>        


            <div class="miniBlock">
         <div class="pageTitleHolder">
            <h5 class="howToMiniHead">Production Studio

                    <div class="btnWatchVideoHolderHToo">
                <a onclick="ShowTutorialPlayer('<%=studioPageTutorialVideoId %>', false)" class="ancHowTo"> watch tutorial </a>
                </div>
            </h5>
</div>
                  <div class="howToImgRSide"><img src="images/mobile/producerChair.png" /></div>
          <p>The Production Studio is where the creativity begins. In your personal studio, you are the producer. Please note, you must have a channel created in order to access the Production Studio, within which there are two (2) different creation modes: Basic and Advanced.</p> 
               
                <h6>Basic</h6>
                     <p>The <strong>Basic</strong> mode allows you to instantly create a schedule for the current day and watch it immediately. In Basic mode, you may use the Autopilot feature on the right side of the screen to tell the system to automatically create schedules for you on a daily basis.  </p>
                 <h6>Advanced</h6>
                     <p>The <strong>Advanced</strong> mode provides a method for building a Custom Schedule, which may be applied to the current date or a future date of your choice. Strimm has built in the flexibility to allow you to switch seamlessly between the modes per your needs. Simply click the “Advanced Mode” switch below the Instant Schedule button on the right side of the screen to change modes.The <strong>Advanced</strong> mode allows you to create a Custom Schedule for today or any day in the future. You can easily switch from one mode to another by clicking on the “Advanced Mode” switch below the Instant Schedule button.  
</p>  






<h6>Get to Know Your Production Studio in Basic Mode (Core Elements)</h6>
            <ol>
<li><span class="subtitlestrong">Channel Edit/Update</span> is located in the header of the page. Click the gear icon next to the channel name to access it. Here, you can update your channel’s avatar and category, and add or edit your channel description. You may also delete a channel. If you have your own website and have subscribed to one of our channel embedding plans, then you can use this section to get the embed code for your channel and activate all of your subscribed functions.</li>   

<%--<li><span class="subtitlestrong">Calendar.</span> This is the starting point of your schedule creation.  It is located in the top right corner of the page.  Click on any day to select a date for your broadcast or to see a previously created program. If the corner of the day is marked, then this day already has a created schedule. Mouse over that day to quickly see the scheduled programming. </li>--%>

<li><span class="subtitlestrong">Video List</span> is the area where your channel’s video content is located. These videos can be scheduled by you for a broadcast on your channel. To add video content to your channel, simply click the “Get New Videos” button and proceed to add videos from different sources. To remove videos, simply click the trash icon in the top-right corner of the thumbnail or use the “clear videos” option above. When there are more than 15 videos in a list, you may click “load more” to see the remaining videos.</li>
                  
<li><span class="subtitlestrong">Get New Videos</span>  is where new video content for your channel can be acquired. 
Choose from the several sources:
</li>
    
   <ol class="olNewVideos">
<li><span class="subtitlestrong subtitlestrongSecondary"> A) Search</span> 
<p>This is the area to search for video content by keywords. </p>
<p>Step 1. Select a video provider from the list. You may source videos from YouTube, Vimeo, or Dailymotion.</br>  
Step 2.Use the search bar to enter keywords of videos you would like to include on your channel. If you are searching for longer videos, be sure to check the box next to “> 20 min”.</br>
Step 3.Once your search results appear, select the category which best fits the chosen video(s) and click the + button on each video thumbnail (which appears  on mouse over) to add them to your channel. Only videos made public by the provider will be available to you. If video is not available, it will be marked as “Restricted by Provider”. </p></li> 

<li><span class="subtitlestrong subtitlestrongSecondary"> B) Import </span>
  
 <p><span class="subtitlestrong margin30 subTitleSecondary"> -Import via URL</span></p>
<p>Step 1.Select “Import by URL” and then select a video provider from the list. <br />
Step 2.  Copy and paste the URL of the video from the selected provider and click “import”.  Next, select the category from the dropdown menu and click the + button on the video box on mouse-over to add it to your channel.</p>

<%--<p><span class="subtitlestrong margin30 subTitleSecondary">-Import Your Entire YouTube Library</span><br /></p>

<p>You can easily import your entire library of YouTube videos to Strimm.</p>

<p>Step 1. Select "Import all of your YouTube videos" and click “Import Videos”. Make sure that your browser allows popups. <br />
Step 2. Login to your YouTube account to import your entire library of videos. If you have multiple accounts to which you would like to pull from, you may do so by repeating these steps for each account.</p>--%>

</li>




 <p><span class="subtitlestrong margin30 subTitleSecondary"> -Import Private Video</span></p>

       <p>
           This feature is only available to subscribers of a Professional Plan (paid plan). Using this option, you can add Private Videos (direct links) to your channel, such as videos from your own server or the servers you have authorization from or the videos, which are marked as “unlisted” on YouTube, Vimeo or Dailymotion. Please check the latest video “privacy” settings of each platform to allow the video to be embedded elsewhere and broadcast there. Some providers, especially Vimeo, have many types of privacy settings. Some of them may not allow broadcast outside of the original platform. As for YouTube, you should only mark videos as UNLISTED. This way, the video will be removed from the public search, but can be added as a direct link to your Strimm TV channel. 
       </p>
       <p>
           Please note that channels with TV programs featuring “private videos” will not broadcast on Strimm’s website and can only be shown on your own personal website, in the embed mode.
       </p>
       <p>
           If the video is added from your own private server, please make sure that the video link is a direct HTTPS link to the video. The private server should be setup for the video streaming with a built-in Content Delivery Network (CDN).  The video link should end in .mp4 and be in MP4 format. 
       </p>
<p>Step 1. Select “Import by URL” and then select “Import Private Video”<br />
Step 2. Complete all fields in the form and click “Add Video” button. Make sure to insert an exact duration for the video. </p>
      <p>If you would like to add mature content to your channel, then you need to switch “Allow Mature Content” to the ON position  in the channel settings  before adding the content.</p>  








  <p>  <span class="subtitlestrong subtitlestrongSecondary">C) My Videos. </span></p>
        
     <p>The My Videos tab is your personal video library sorted by category. It will display all of the videos you have chosen to add to your channels from all sources. You can access your videos at any time and add them to any channel you manage. If you accidentally delete a video from a channel, you can easily re-add it from the My Videos page.</p>
</ol>


<h6 class="subTitleH6">Schedule Creation Control Panel</h6>
<p>The Schedule Creation Control Panel is where you will schedule your broadcasts. It’s located on the right side of the Production Studio page. </p>

<ol>
<li><span class="subtitlestrong">A) Instant Schedule</span> is a feature that randomly combines videos from your Video List and creates a schedule for you. After cultivating the schedule, review it and click “Publish”. It will begin broadcasting immediately through midnight of the current day. In order for this feature to work, you must have at least 3 different and non-restricted videos in your Videos List and enough time to play them before midnight. When Instant Schedule is selected for a future date, the airtime is midnight to midnight on the day selected. </li>

<li><span class="subtitlestrong">B) Autopilot</span> is a feature that lets the system do the work for you. When activated, Autopilot will automatically generate a schedule for your channel for the following day at 11:00am (Central Time) daily. The schedule will begin broadcasting at midnight and go through midnight of the following day. In order for Autopilot to work, you must have at least 3 different and non-restricted videos in your Video List. Autopilot will continue creating schedules for your channel every day until it is turned off. If Custom Schedule is created on certain day (using Advanced Mode), the Autopilot will skip schedule generation for that day.  </br>
    
<span class="textUnderline">Remember:</span>  Videos in your list may be removed (become restricted) by the video provider at any time. Regularly add fresh video content to your channel to grow your list and to avoid broadcast interruption. It is not recommended to leave Autopilot on for an extended period for this reason. This may cause your channel to repeat similar programs daily or simply stop broadcasting altogether.  
</li>


   <li><span class="subtitlestrong"> C)	Delete. </span>  If you are unhappy with your schedule selection, you may delete it.</li>


</ol>       
  </ol>  
                
                
                
                
                        
  <h6>Get to Know Your Production Studio in Advanced Mode</h6> 
<p>Advanced mode has the same features as Basic mode and more! Additional Schedule Creation Control Panel elements are located on the right side of the Production Studio and allow you to schedule a broadcast in Advanced mode.</p>
<ol>
             <li><span class="subtitlestrong">   A)	Calendar</span>   is located in the top-right corner of the page. To view an existing schedule or select a new one, simply click to select  a date from the calendar. The dates that have a broadcast scheduled are clearly marked in the corners. Mouse over a marked date to see the scheduled programming.</li>   

<li><span class="subtitlestrong">B)	Custom Schedule.</span>  Would you like to be a true producer? With Strimm, you can be. Take complete control over the programming on your channel with the Custom Schedule feature. To begin, select a date from the calendar. Then, click on “Custom Schedule”, choose a start time, and click “Create”. Begin adding videos from your list of videos by clicking the + button (which appears on mouse over). The start and end time of each schedule (playlist) is up to you. You may stop adding videos and choose a new start time to run multiple schedules for the same day. Once you are finished scheduling your broadcasts for the chosen date, click “Publish” to activate each schedule.</li>

  <a name="Channel"></a>    
<li><span class="subtitlestrong">C)	Repeat.</span> Once you have created a schedule, you have the option to repeat it on any future dates. Simply click “Repeat” above a created schedule, select the desired repeat date, and click “OK”.  </li>

<li><span class="subtitlestrong">D)	Internal search for videos in the channel.</span> Looking to sort through your videos to quickly and easily find a particular one? Simply click the magnifying glass and enter the name of the video you are searching for.</li>

  </ol>  
                
                
   <h6>Broadcast LIVE on Strimm</h6>  
                
                   <p>  Would you like to stream a live event on your TV channel?  It is easy with Strimm! The event is streamed through YouTube Live and will appear on your Strimm TV channel. The steps are simple:</p>
                   
<span class="subtitlestrong indieFrase">A) Go to YouTube LIVE, schedule a live event and copy its URL. Alternatively, you can copy URL of any broadcasting live event.</span>
<span class="subtitlestrong indieFrase">B) Go to Production Studio of your Strimm channel, click “Get New Videos”:</span> 
                <ol> 
<li class="limarginLeft">1)  Select “Import” and YouTube as a provider.</li> 
<li class="limarginLeft">2) Paste the URL of a live event</li>
<li class="limarginLeft">3) Select category </li>
<li class="limarginLeft">4) Click on “+” button on video (appears on mouse over) and pick the start date and time (mandatory) and end date and time (optional) </li>
<li class="limarginLeft">5) Click “Save”. The scheduled live event will be added to your channel. No further scheduling is needed.</li> 
<span class="subtitlestrong indieFrase">C) To watch live stream: </span>
<p>Go to your TV channel (embedded on your website or on Strimm.com), click LIVE button and select the live event you want to watch.</p> 
<p><u>Note:</u> Please check the latest YouTube Live rules to see if you are eligible to embed YouTube Live stream outside of Youtube.com.  You may need to have a certain number of subscribers and channel views to be eligible. </p>
    </ol>
                
</div>

            <div class="miniBlock">
        <div class="pageTitleHolder">
            <h5 class="howToMiniHead">Channel</h5>

            </div>
                  <div class="howToImgRSide flowLeft">
                
              <img src="images/mobile/experience-popcorn.png" />
                  </div>

            <p>The Channel page is where your work will be displayed for the world to watch! You may visit this page by clicking the avatar with the play button on it on the Network page or in the Production Studio or by using a direct link. Be sure to share your channel link with your friends and followers to attract viewers to your next broadcast. </p>
<p>The Channel page offers the following controls and features, located on the right side: </p> 


<p><strong>1. Guide.</strong> Click this button to easily access all channels on Strimm’s platform.</p> 
<p><strong>2. Channel Schedule.</strong> Click this button to see today’s schedule of the channel or save videos to watch them later in your personal “Watch Later” area.</p>
<p><strong>3. Channel Info.</strong> Click this button to learn more about the channel, rate it, like it, and add the channel to your Favorites or report abuse.</p>
<p><strong>4. Video Info.</strong> Click this button to learn more about the video.</p>
<p><strong>5. Chat. </strong>Interact with other users in the comment section of the chat.</p>



















<%--<h6>Left side options</h6>--%>
<%--<ol>

<li><span class="subtitlestrong">Channel Rating:</span> Rate a channel by clicking the stars.</li>
<li><span class="subtitlestrong">Like:</span> Click the thumbs up to show your appreciation for a channel by throwing the producer a “Like”.</li> 
<li><span class="subtitlestrong">Add to Favorites:</span> Click the Star to subscribe to a channel and add it to your Favorites. This provides easy access to a channel later on.</li>
<li><span class="subtitlestrong">Watch Later:</span> Click the small timer icon, which saves a videoon your Watch Later list. </li>
<li><span class="subtitlestrong">Report:</span> Click the balance icon to report an issue to the Strimm admins.</li>
<li><span class="subtitlestrong">Share:</span> Send this channel to your friends and family via your favorite social networks. </li>  
  
</ol>--%>

<%--<div class="RightSideColumnsBlock">
<h6>Right side columns</h6>
<ol>
<li><span class="subtitlestrong">Schedule:</span> View the daily broadcast schedule.</li>
<li><span class="subtitlestrong">Favorite Channels:</span> A condensed list of your favorite channels.</li>
<li><span class="subtitlestrong">Top Channels:</span> Check out the top channels as selected by Strimm.</li>
<li><span class="subtitlestrong ">My Channels:</span> Click here to quickly transition between your own channels.</li>
<li><span class="subtitlestrong ">Chat:</span>  Interact with other users in the comment section of the chat.</li> 
</ol>
    </div>--%>
          

        </div>



 <div class="miniBlock">

             <div class="pageTitleHolder">
               
            <h5 class="howToMiniHead"> Channel Embedding</h5>

            </div>
     <div class="howToImgRSide"> <img src="images/channel-screen-embedded-look.jpg" /></div>
    
            <a name="Embedding""></a>
<p>Do you have a website? Now, you can broadcast your TV channel on your own website and grow your brand like never before! First, select a plan on our
    <a href="create-tv/pricing"> Plans & Pricing page</a>  and subscribe to it. </p>

<h6 class="subTitleH6">Single Channel Embedding </h6>
<p>Follow the steps below in order to embed a channel on your website:</p>  
<p>1. Go to your channel’s Production Studio and click the gear icon next to channel name (settings).</p> 
<p>2. Switch the “Embed This Channel On My Website” to ON. </p> 
<p>3. Enter the URL of the website where you’d like your channel embedded.</p> 
<p>4. Copy the embed code and paste it in the area of your website, where you want your TV channel to appear.</p> 
<p>5. If you are subscribed to special features, like White-Label, Custom Branding, etc., then switch these features ON as well, if you wish. </p> 
<p>6. Click “Update” to save the settings.</p> 
<h6 class="subTitleH6">Multiple Channels Embedding.</h6>

     <ol>
<li class="subtitlestrong">A.	If you want all of your embedded channels to appear in the GUIDE area of your default channel and be watched without leaving the page, then embed the code of your default channel only (the channel, which you want to appear first on your screen). 
As for the remaining, non-default channels, simply follow the steps above (steps 1-6) and skip step # 4 (code embedding). </li>
<li class="subtitlestrong">B. If you need your channels to appear on different pages or different websites, then you would need to enter the URL of that  website in the channel settings of each channel and paste the embed code of each channel on the desired page of the website.</li>
</ol>
</div>
    </div>
     </div>
    </div>
       
         <script src="/JS/swfobject.js" type="text/javascript"></script>
     <script src="//www.google.com/jsapi" type="text/javascript"></script>
  
     <script type="text/javascript">
         google.load("swfobject", "2.1");
       </script>
     <script async="async" src="https://www.youtube.com/iframe_api"></script>
</asp:Content>

