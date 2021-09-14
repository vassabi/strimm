<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="StrimmTube.Create" %>
<asp:Content ID="Content3" ContentPlaceHolderID="titleHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="metaHolder" runat="server">
<meta name="description" content=" Strimm is a Social Internet TV Network. It allows watching free video shows online continuously, create public TV streaming and socialize with friends" />
   </asp:Content> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="css/commonCSS.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div id="divBoardContent">
                <div id="divTitleUrlHolder">
         <h1 class="pageTitle">Create</h1>
                    </div>
 <div id="divContentWrapper">
    <div class="commingsoon"><span>Comming Soon</span></div>
   </div>
          <uc:FeedBack runat="server" ID="feedBack" pageName="create" />
        </div>
</asp:Content>
