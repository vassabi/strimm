<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="StripePaymentSuccess.aspx.cs" Inherits="StrimmTube.StripePaymentSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="canonicalHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    <link href="css/paymentConfirmation.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pageHolder">
        <div id="contentWrapper">
            <div id="divTitleUrlHolder">
                <h1 class="pageTitle">Success!</h1>
            </div>
            <div id="successContent">
                <h1>Congratulations and welcome to Strimm community of TV producers! 

                </h1>
                <h2>Thank you for your business! 

                </h2>
                <p>
                    Your Order #
                    <asp:Label runat="server" ID="lblStripeConfirmation" ClientIDMode="Static">number goes here</asp:Label>

                </p>
                <p>
                    Subscription Plan:
                    <asp:Label runat="server" ID="lblPlanType" ClientIDMode="Static">plan type goes here</asp:Label>

                </p>
                <p>
                    Monthly charge: $<asp:Label runat="server" ID="lblCharge" ClientIDMode="Static">monthly charge goes here</asp:Label>

                </p>
                <p>
                    You should receive an email with instructions within 15 min. Please check your Spam folder to make sure that the email is not trapped there. 
                </p>
                <p>
                    Please contact us at <a href="mailto:support@strimm.com?Subject=Payment%20Support">support@strimm.com</a>  if you have any questions.
                </p>
            </div>
            <div id="successLinksHolder">
                <a class="successProfile" href="#" id="btnGoToProfile">Go To My Profile
                </a>
                <a class="successNetwork" href="#" id="btnGoToMy Network">Go To My Network
                </a>
            </div>

        </div>
    </div>
</asp:Content>
