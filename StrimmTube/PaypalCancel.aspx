<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="PaypalCancel.aspx.cs" Inherits="StrimmTube.PaypalCancel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
Subscription Canceled| Strimm Online TV
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
<meta name="description" content="Strimm Online TV Solution for Businesses and Organizations of any kind" />
<meta name="robots" content="FOLLOW, INDEX, NOODP, NOYDIR" />
<meta name="GOOGLEBOT" content="FOLLOW, INDEX, NOODP, NOYDIR" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://www.strimm.com/company.aspx" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    <script src="/JS/swfobject.js" type="text/javascript"></script>
    <script src="//www.google.com/jsapi" type="text/javascript"></script>
   
   <%-- <script src="/JS/Froogaloop.js"></script> --%>
    <script type="text/javascript">
        google.load("swfobject", "2.1");
    </script>
    <link href="css/paymentConfirmation.css" rel="stylesheet" />
    <script src="/JS/Business.js" type="text/javascript"></script>
    <script src="/JS/OrderProcessing.js" type="text/javascript"></script>
    <script src="/JS/Main.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var canceled = '<%=canceled%>';
        if (canceled != undefined && canceled == 'True') {
            deleteCookie('OrderDetails');
        }
    </script>
    <div id="contentWrapper">
        <div id="divTitleUrlHolder">
        <h1 class="pageTitle">Subscription Cancellation is Confirmed</h1>

    </div>
        <div id="successContent">

            <p>
Your subscription has been cancelled. If you have a PayPal subscription, make sure you login to PayPal.com directly to cancel it.

            </p>
            <p>
Please contact us at <a href="mailto:support@strimm.com?Subject=Payment%20Support">support@strimm.com</a>  if you have any questions.
            </p>
        </div>
        <div id="successLinksHolder">
            <a href="/<%=userUrl%>/profile" id="btnGoToProfile">
                Go To My Profile
            </a>
            <a href="/<%=boardUrl%>" id="btnGoToMy Network">
                Go To My Network
            </a>
        </div>

    </div></asp:Content>
