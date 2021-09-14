<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="StrimmTube.About" %>


<asp:Content ID="Content3" ContentPlaceHolderID="titleHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content=" Strimm is a Social Internet TV Network. It allows watching free video shows online continuously, create public TV streaming and socialize with friends" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divTitleUrlHolder">
        <h1 class="pageTitle">about</h1>

    </div>
    <%--<div id="divBoardContent">


         <div id="divContentWrapper">




        <div class="partContentAboutLeft ">            
  <p> <img src="/images/about/boardGroup.png" />Strimm was created by the group of entrepreneurs who were tired of
 wasting time to search for a new video online, once the previous short one was ended. 
 </p>
         </div>
  
     <div class="partContentAboutRight">        
<p>
<img src="/images/about/sofaTV.png" />Strimm is entirely new concept of online video entertainment. If you are a viewer, it brings you a TV-like experience with channels and continues broadcasts. It lets you creating your own streaming channel by using your videos or utilizing videos, created by others. </p>
         </div>
                     <div class="partContentAboutLeft">            
  <p> <img src="/images/about/groupON.png" />Strimm is a Social Network. 
      It lets you creating you public board, follow others, show the world what you like, share your favorite shows and channels and much more! 
 </p>
         </div>
  
     <div class="partContentAboutRight"">        
<p>
<img src="/images/about/moneyBoy.png" />
 Money-earning capabilities will be available to any active user of Strimm in the near future. 
So, start earlier to establish your audience!
 </p>
         </div>
                     <div class="partContentAboutLeft">            
  <p><img src="/images/about/rupper.png" />
Your dream can finally come true and you can become a Hollywood-like director and producer of your own TV shows! 
      Decide what to run on your channel and when to run it.
 Each show can be scheduled at its time and on certain day, directly from your chair, in minutes. 
 </p>
         </div>
  
     <div class="partContentAboutRight"">        
<p>
<img src="/images/about/tools.png" />
 Strimm offers range of innovative tools to easily create channel, broadcast it, socialize and much more.
     Since everything is online, you can operate your network, while lying on the beach somewhere in Bahamas… </p>
         </div>
                     <div class="partContentAboutLeft">            
  <p><img src="/images/about/girlWITHglobe.png" />
      Try Strimm, explore yourself and open the door for new opportunities and endless fun.
 </p>
         </div>
  
 <asp:Button runat="server" id="btnTryNow" Text="try now!" OnClick="btnTryNow_Click" ClientIDMode="Static"/>


    </div>
          <uc:FeedBack runat="server" ID="feedBack" pageName="about" />
        </div>--%>

    <div class="aboutHolder block">
        <a name="tv"></a>
        <div class="aboutBlock block">
            <img src="/images/about-tv.jpg" />
            <h6 class="aboutTitle">Search less,   watch more!</h6>
            <h6 class="aboutDescription">Enjoy video entertainment continuously.
                            Simple...  like watching TV online!
            </h6>

            <p>
                Strimm is an entirely new concept of online video entertainment. It brings you a TV-like experience with channels and a continuous broadcast.  It eliminates the need to constantly search for videos online since your channel will simply continue playing the next scheduled video.  The video shows are scheduled for a certain day and time, and will broadcast continuously, just like regular TV. 
     Make sure that you do not miss the start of the show! 
            </p>
        </div>


        <a name="social"></a>
        <div class="aboutBlock block">
            <img src="/images/about-social.jpg" />
            <h6 class="aboutTitle">Plug yourself 
                           into the world</h6>

            <h6 class="aboutDescription">Get social! Communicate with friends as the TV show goes on!
            </h6>
            <p>
                Strimm is a social network, allowing you to create your own social board, follow others, communicate with your friends, comment on the videos and share your posts. You can even “ring” the show you watch to your followers, so they can join you and watch what you are watching.   
            </p>
        </div>


        <a name="broadcast"></a>
        <div class="aboutBlock block">
            <img src="/images/about-broadcast.jpg" />
            <h6 class="aboutTitle">Stream your own TV easily</h6>
            <h6 class="aboutDescription">Flexible, all-in-one platform puts you in the producer seat!
            </h6>

            <p>
                Strimm is a unique platform with a range of innovative and easy-to-use tools, allowing you to create your own TV network without hiring hundreds of employees and even without having any videos to make!   Your dreams of becoming a Hollywood producer can finally come true!  Produce your own TV shows! Make your own content!  Decide what to run on your channel and when to run it. Each video show can be scheduled for broadcast in a matter of seconds. 
            </p>
        </div>
    </div>
</asp:Content>
