<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Confirmation.aspx.cs" Inherits="StrimmTube.Confirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content=" Strimm is a Social Internet TV Network. It allows watching free video shows online continuously, create public TV streaming and socialize with friends" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">


    <%--  <link href="css/commonCSS.css" rel="stylesheet" />--%>
    <style>
                #divBoardContent {
margin: auto auto 10px;
max-width: 100%;
min-height: 600px;
min-width: 1024px;
width: 100%;
}

        .pageTitle {
color: #2a99bd;
display: block;
font-family: opensans-light;
height: 100px;
line-height: 150px;
text-align: center;
width: 100%;

        }
        .spnConfirmationLink {
            color: #555555;
            display: block;
            font-size: 15px;
            font-style: italic;
            height: 35px;
            line-height: 20px;
            margin: 20px auto;
            text-align: center;
        }

        #divStartHere {
            background-image: url("/images/ButtonBG.jpg");
            background-repeat: repeat-x;
            border-color: #55AAC5 #2A99BD #2A99BD #55AAC5;
            border-radius: 5px;
            border-style: solid;
            border-width: 1px;
            display: block;
            height: 30px;
            margin-left: auto;
            margin-right: auto;
            
            text-align: center;
            width: 180px;
            margin-bottom:50px;
        }

            #divStartHere:hover {
                background-image: url("/images/ButtonBGhover.jpg");
            }

            #divStartHere a {
                color: #F3F3F3;
                font-size: 15px;
                line-height: 30px;
                padding-left: 5px;
                padding-right: 5px;
                text-decoration: none;
                text-transform: capitalize;
            }

                #divStartHere a:hover {
                    color: #fff;
                }
        #divVIdeoHolder {
             height: 800px;
    margin: 60px auto;
    max-height: 580px;
    min-height: 500px;
    width: 728px;
    margin-bottom: 30px;
        }
        #spnVideo {
            color: #555555;
            display: block;
            font-size: 16px;
            margin-bottom: 17px;
            text-align: center;
            text-decoration: underline;
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div id="divBoardContent">

                    <h1 class="pageTitle">Welcome aboard!</h1>


        <span class="spnConfirmationLink">Hi <%=UserName%>,
             <br />
            <br />
            Welcome aboard! Thank you for confirming your registration with Strimm.<br />
            Please log in to create your own TV Network and show the world what you like!</span>
        <div id="divVIdeoHolder">
        <span id="spnVideo">Watch a quick video tour</span>
           
            <iframe width="728" height="546" src="//www.youtube.com/embed/BvmrFbCzjRw"></iframe>
            </div>
        <div id="divStartHere">
            <a href="welcome-to-strimm">start here</a>
        </div>
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

</asp:Content>
