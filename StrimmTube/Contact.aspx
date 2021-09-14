<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="StrimmTube.Contact" %>

<%--<%@ Register Assembly="Recaptcha.Web" Namespace="Recaptcha.Web.UI.Controls" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
Contact Us To Learn About Strimm Online TV Platform | Live TV
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Contact Strimm if you have any questions about online live tv creation and linear tv broadcast" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
 <link rel="canonical" href="https://strimm.com/contact-us" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
   
 <link href="css/contactCSS.css" rel="stylesheet" />
     <script async src='https://www.google.com/recaptcha/api.js'></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <%--     <h1 class="pageTitle">contact us</h1>--%>

    <div class="learnMoreButtonHolder">
    <h1 class="contactTitle">Contact Us</h1>
<%--    <div class="buttonsHolder">
        <a href="#"  class="learmMoreWatchVideo" onclick="ShowTutorialPlayer('MTTeo-3NMfk')">watch video</a>
     
         <a href="/Guides.aspx" class="learmMoreHowTo">how to</a>
    </div>--%>

</div>

    <div id="divBoardContent">



<div class="contactFormRight">
 
        <div id="divContact">
                    <div class="emailAdress ">
                <p>
                    <strong>By Email</strong>

                </p>
            </div>
           
            <ul>
<%--                <li><span>your name</span>
                    <asp:TextBox runat="server" ID="txtName"></asp:TextBox></li>
                <li><span>your email</span>
                    <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox></li>--%>
                <li><asp:TextBox runat="server" ID="txtName" ClientIDMode="Static" placeholder=" Name"></asp:TextBox></li>

                <li> <asp:TextBox runat="server" ID="txtEmail" ClientIDMode="Static" placeholder=" Email"></asp:TextBox></li>
                <li>
                    
                    <asp:DropDownList runat="server" ID="ddlSubject" ClientIDMode="Static"  OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" AutoPostBack="false">
                 <asp:ListItem>subject</asp:ListItem>
                        <asp:ListItem>feedback</asp:ListItem>
                        <asp:ListItem>support question</asp:ListItem>
                        <asp:ListItem>technical issues (bugs or errors)</asp:ListItem>
                        <asp:ListItem>advertising</asp:ListItem>
                        <asp:ListItem>investment</asp:ListItem>
                        <asp:ListItem>marketing</asp:ListItem>
                        <asp:ListItem>copyright issues</asp:ListItem>
                        <asp:ListItem>other</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                  
                    <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Rows="9" Columns="45" ClientIDMode="Static" MaxLength="1000" placeholder="Your Message"></asp:TextBox>
                </li>

            </ul>
            <div class="spacer"></div>
            <div id="captcha">
                <%--<cc1:Recaptcha ID="Recaptcha1" runat="server" Theme="White" Language="en" />--%>
                <div class="g-recaptcha" data-sitekey="6LcXuAcTAAAAAJoZDhw0TcwPxwI0hxTISYLDGWEW"></div>
            </div>


            <div id="divMessage">
                <asp:Label runat="server" ID="lblMessage" ClientIDMode="Static"></asp:Label></div>
            <div id="btnHolder" class="buttonHolder">
                <asp:Button runat="server" ID="btnSend" Text="send" OnClick="btnSend_Click" /></div>
 </div>
</div>

                <div class="contactFormLeft">
        <div class="mailAdress">
                <p>
                    <strong>By Mail</strong></br>

Strimm, Inc.<br/>
113 McHenry Rd, Unit 147<br/>
Buffalo Grove, IL 60089
                </p>
            </div>
</div>
        
        <uc:FeedBack runat="server" ID="feedBack" pageName="contact" />


    </div>

    <script>
        function ShowConfirmationMessage(message) {
            alertify.success(message);
        }
        function ShowErrorMessage(message) {
            alertify.error(message);
        }
    </script>
</asp:Content>
