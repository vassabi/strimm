<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="PaymentSuccess.aspx.cs" Inherits="StrimmTube.PaymentSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
Payment Confirmation| Strimm Online TV
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
<meta name="description" content="Strimm Online TV Solution for Businesses and Organizations of any kind" />
<meta name="robots" content="FOLLOW, INDEX, NOODP, NOYDIR" />
<meta name="GOOGLEBOT" content="FOLLOW, INDEX, NOODP, NOYDIR" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://www.strimm.com/company.aspx" />
</asp:Content>


<asp:Content ID="Content6" ContentPlaceHolderID="head" runat="server">
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="//www.google.com/jsapi" type="text/javascript"></script>
    
   <%-- <script src="/JS/Froogaloop.js"></script> --%>
    <script type="text/javascript">
        google.load("swfobject", "2.1");
    </script>
    <link href="css/paymentConfirmation.css" rel="stylesheet" />
    <script src="/JS/Business.js" type="text/javascript"></script>
    <script src="/JS/OrderProcessing.js" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="pageHolder">   
    <div id="contentWrapper">
        <div id="divTitleUrlHolder">
        <h1 class="pageTitle">Success!</h1>

    </div>
        <div id="successContent">
            <h1>
Congratulations and welcome to Strimm community of TV producers! 

            </h1>
            <h2>
Thank you for your business! 

            </h2>
            <p>
                Your Order # <asp:Label runat="server" ID="lblPaypalConfirmation" ClientIDMode="Static">number goes here</asp:Label>

            </p>
            <p>
                Subscription Plan: <asp:Label runat="server" ID="lblPlanType" ClientIDMode="Static">plan type goes here</asp:Label>

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
            <a class="successProfile" href="/steps" id="btnGoToProfile">
                NETX STEPS
            </a>
        </div>

    </div>
  </div>    
</asp:Content>

