<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="unsubscribeFull.aspx.cs" Inherits="StrimmTube.unsubscribeFull" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="canonicalHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
         .unsubscribeContent {
             width: 100%;
             margin: auto;
             max-width: 800px;
             margin-top: 100px;
             min-height: 500px;
         }
             p {
                 text-align: center;
                 font-family: Tahoma;
                 color: #999999;
                 font-size: 15px;
                 display: block;
                 line-height: 24px;
                 text-align: left;
             }

             .updatePrefHolder {
                 display: block;
                 float: left;
                 width: 100%;
                 height: 50px;
                 margin-bottom: 40px;
                 margin-top: 100px;
             }

             .updatePref {
                 width: 220px;
                 background-color: #2a99bd;
                 height: 50px;
                 display: block;
                 margin: auto;
                 color: #fff;
                 font-family: tahoma;
                 line-height: 50px;
                 font-size: 17px;
                 cursor: pointer;
                 text-decoration: none;
                 font-weight: normal;
                 margin-top: 20px;
                 text-align: center;
                 margin-bottom: 15px;
                 margin: auto;
                 text-transform: capitalize;
             }
                      .unsubscribePageTitle {
                 display: block;
    width: 98%;
    padding: 1%;
    font-family: Tahoma;
    font-size: 22px;
    color: #2a99bd;
    text-align: left;
    font-weight: normal;
         }
                 </style>


      <div class="unsubscribeContent" >
              <h1 class="unsubscribePageTitle">
                 
         Unsubscribe successful</h1>
<p>Your email <%=UserEmail%>, has been successfully unsubscribed from all email lists.</p>
<p>Please note that you may receive occasional administrative emails related to your account at Strimm. Such emails may include but are not limited to account issues, updates or a password reset email. 

                </p>

    <div class="updatePrefHolder">
                 <a href="/all-channels?category=All%20Channels" class="updatePref">
                  see all channels
                </a>
</div>

      <div class="unsubscribeContent" >

      </div></div>
</asp:Content>
