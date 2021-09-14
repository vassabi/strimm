<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="StrimmTube.ThankYou" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Strimm | Thank you for your registration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Strimm | Thank you for your registration" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://strimm.com/thank-you" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <%-- <link href="css/commonCSS.css" rel="stylesheet" />--%>

    <style>

        #divBoardContent {
            max-width: 96%;
            min-height: 600px;
            width: 100%;
            padding: 2%;
            margin: auto;
        }

        .pageTitle {
            color: #2a99bd;
            display: block;
            text-align: center;
            width: 100%;
        }

        #lblMessage {
            color: #FF6A00;
            display: block;
            margin: 20px auto;
            width: 159px;
        }

        .spnConfirmationLink {
            display: block;
            line-height: 24px;
            text-align: center;
            width: 100%;
        }

        #divBtnResend {
            display: block;
            margin-left: auto;
            margin-right: auto;
            width: 180px;
        }

        #btnResend {
            background-image: url("/images/ButtonBG.jpg");
            background-repeat: repeat-x;
            border-color: #55AAC5 #2A99BD #2A99BD #55AAC5;
            border-radius: 5px;
            border-style: solid;
            border-width: 1px;
            color: #F3F3F3;
            font-size: 15px;
            height: 30px;
            line-height: 30px;
            padding-left: 5px;
            padding-right: 5px;
            text-transform: capitalize;
            width: 180px;
        }

            #btnResend:hover {
                background-image: url("/images/ButtonBGhover.jpg");
                color: #fff;
            }

        .pleaseNote {
            display: block;
            line-height: 24px;
            text-align: center;
}

        .goToLoginBox {
            display: block;
            line-height: 20px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="divBoardContent">
        <h1 class="pageTitle">Welcome Aboard and Congratulations!</h1>
        <span class="pleaseNote">Your account is activated.
            <br />
            <br />
            Feel free to login with your credentials and start building your TV empire online!
            <br />
            <br />
            You can broadcast on Strimm.com or subscribe to one of our packages and embed your TV network on your own website.
            <br />
            <br />
            Please click on <a href="https://strimm.us/steps">NEXT STEPS </a>to learn more about channel creation and embedding.</span>
    </div>


    <!-- Google Code for Sign up Conversion Page -->
    <script type="text/javascript">
        /* <![CDATA[ */
        var google_conversion_id = 974088209;
        var google_conversion_language = "en";
        var google_conversion_format = "3";
        var google_conversion_color = "ffffff";
        var google_conversion_label = "78gvCLf3nGAQkdC90AM";
        var google_remarketing_only = false;
        /* ]]> */
    </script>
    <script type="text/javascript" src="//www.googleadservices.com/pagead/conversion.js"></script>
    <noscript>
        <div style="display:inline;">
            <img height="1" width="1" style="border-style:none;" alt="" src="//www.googleadservices.com/pagead/conversion/974088209/?label=78gvCLf3nGAQkdC90AM&guid=ON&script=0"/>
        </div>
    </noscript>

    <script type="text/javascript">
        <!--fbq('track', 'CompleteRegistration');-->
    </script>
</asp:Content>
