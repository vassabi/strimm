<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Tips.aspx.cs" Inherits="StrimmTube.Tips" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">Strimm – Tips</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
<meta name="description" content=" Strimm is a Social Internet TV Network. It allows watching free video shows online continuously, create public TV streaming and socialize with friends" />
<meta name="keywords" content=" internet tv, social tv, social network, watch videos, tv network, videos online, online videos, schedule videos, schedule broadcast, create tv, playlist builder, free videos, watch videos online, tv streaming" />
   </asp:Content> 
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <link href="css/commonCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div id="divBoardContent">
                <div id="divTitleUrlHolder">
         <h1 class="pageTitle">tips</h1>
                    </div>
 <div id="divContentWrapper">
    <div class="commingsoon"><span>Comming Soon</span></div>
   </div>
          <uc:FeedBack runat="server" ID="feedBack" pageName="tips" />
        </div>
</asp:Content>
