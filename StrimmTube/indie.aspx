<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="indie.aspx.cs" Inherits="StrimmTube.indie" %>




<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
Indie - Independent Film Makers and Producers | Strimm Online TV
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
<meta name="description" content="Are you Independent Film Makers and Producers? Express Yourself on Strimm TV. " />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
 <link rel="canonical" href="https://www.strimm.com/indie.aspx"/>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="css/indie.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="companyHolder">

       <h1 class="h1company">Indie</h1>

         <h1 class="indieHolder"> Join Our Community </br> of Independent Creators and Producers</h1>

    </div>
    <div id="divBoardContent">

       

        <div id="divContentWrapper">

<div class="indieBlock">
<h5 class="companySubTitle">Step 1</h5>
<p>Sign up on Strimm, if you have not done so yet. Please make sure to check the box on the bottom of Sign Up page, to let us know that you are an “indie”.   </p>
</div>

<div class="indieBlock">
<h5 class="companySubTitle">Step 2</h5>
<p class="pTitle">Independent film makers</p>
  <p>  Ready to share your story? We've got great tools to help you bring it to life and financially benefit from it. Make sure to send us links to your best videos for review. Once approved, you will get you plugged into our marketplace and community of like-minded creators to help you build your digital presence and financially benefit from it.</p>
<p>Strimm is going to let independent creators to upload their original content to our site and let talented producers to use it on their channels and promote it to their audience. You keep all the rights of creations and you do not have to promote it yourself in order to monetize.  We believe that everyone should do what they do best – film makers should create high quality videos and films, while talented producers should produce a good programming on their Strimm channels and promote them to their audience for mutual benefits. 
  </p>
          <p class="pTitle">  Independent Producers</p>
<p>It is your time to shine! Use your marketing talent and grow your audience. Share your channels online and show everyone how good you are. Make advertisers bid to advertise on your channels. The best part is that you do not have to make your own content and promote single videos. Market your entire broadcasting channel or network and acquire more fans. Start with a standard account right away and use available public video providers directly on Strimm platform, like Youtube, Vimeo and Dailymotion and grow your audience. These video providers have virtually unlimited videos in every category. Once your channel get to the level of steady 50,000 views per month or more for 3 month in the row, you can submit your account for an upgrade and once approved, you can start monetizing your channels.</p> 
</div>

<div class="indieBlock">

<h5 class="companySubTitle">Step 3</h5>
<p>Enjoy Strimm and Its community! Get cozy and enjoy channels created by other talented people, share them with your friends and build your own community.  
</p>

</div>


<div class="indieActionsHolder"><a runat="server" id="ancCreateOrSignUp" class="indieSignUp" clientidmode="Static" href="/sign-up">Sign Up. It is Free</a></div>

</div>
    </div>
</asp:Content>
