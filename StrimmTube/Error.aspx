<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="StrimmTube.Error" %>

<asp:Content ID="Content3" ContentPlaceHolderID="titleHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content=" Strimm is a Social Internet TV Network. It allows watching free video shows online continuously, create public TV streaming and socialize with friends" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">



        #content {
background-color: #2a99bd;
display: block;
float: none;
height: 700px;
margin: -25px auto 10px;
width: 100%;
        }

                .errorLeft,
                .errorRight {
display: block;
float: left;
height: 300px;
padding: 2%;
width: 45%;
margin-top: 100px;

        }

        .errorRight {
            border-left: 1px solid #eee;
        }

        .errorLeft {
            margin-top: 140px;
        }


                .error404 {
border: 2px solid #eee;
border-radius: 100px;
color: #fff;
font-family: opensans-regular;
font-size: 40px;
height: 200px;
line-height: 200px;
text-align: center;
text-transform: capitalize;
width: 200px;
margin: auto;
        }

        .error {
clear: both;
color: #fff;
display: block;
font-size: 30px;
height: 40px;
line-height: 40px;
margin-top: 40px;
text-align: center;
width: 100%;
        }

        #errorMsg {
display: block;
font-family: opensans-regular;
font-size: 15px;
line-height: 25px;
padding: 30px;
text-align: left;
width: 100%;
color: #eee;
        }

        #content a {
background-color: #fff;
color: #000;
cursor: pointer;
display: block;
font-size: 15px;
height: 37px;
line-height: 37px;
margin: auto;
text-align: center;
text-decoration: none;
width: 140px;
opacity: 0.7;
        }



            #content a:hover {opacity: 9.9;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        

    <div id="content">
        <div class="errorLeft">
        <h1 class="error404">404 error</h1>
            </div>
        <div class="errorRight">
        <h2 class="error">Page not found</h2>
        <span id="errorMsg">We are sorry, there is an error followed your request. Strimm Administration has been informed about this error. 
            <br />
            Please choose another request. We apologize for inconvenience.</span>
        <a href="/home">Go back</a>
            </div>
    </div>
</asp:Content>
