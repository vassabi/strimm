<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="maintenance.aspx.cs" Inherits="StrimmTube.maintenance" %>


<asp:Content ID="Content3" ContentPlaceHolderID="titleHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content=" Strimm is a Social Internet TV Network. It allows watching free video shows online continuously, create public TV streaming and socialize with friends" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">

body {
    background-color: #2a99bd;
}

        #divTopWrapper {
    background-image: url('/images/LogoPureMain.png');
    background-position: 5%;
    background-repeat: no-repeat;
    background-size: 200px;

        }

        #divTop {
            display: none;
        }

        #content {
background-color: #2a99bd;
display: block;
float: none;
height: 700px;
margin:auto;
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


        .errorLeft {
           background-image: url('/images/maintenance.jpg');
           background-position: bottom center;
           background-repeat: no-repeat;
        }




        .error {
     clear: both;
    color: #fff;
    display: block;
    font-size: 50px;
    height: 40px;
    line-height: 50px;
    text-align: left;
    width: 100%;
    margin-bottom: 60px;
    
        }

        p {
    display: block;
    font-family: opensans-regular;
    font-size: 19px;
    line-height: 30px;
    text-align: left;
    width: 100%;
    color: #eee;
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

        #divFooter.default {
            display: none;
        }


        .maintenanceLearnMore {
            display: block;
            text-decoration: underline;
            color: #fff;
        }



            #content a:hover {opacity: 9.9;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        

    <div id="content">
        <div class="errorLeft">
            </div>
        <div class="errorRight">
        <h2 class="error">Scheduled Maintenance</h2>

 <p>Dear Friends,</p>
 
<p>Thank you for being a valuable member of  Strimm community!</br>
We are working hard to improve your Strimm experience and performance of the site.</br>
Once site is back all channels and their schedules will continue performing as created. </br>
 
We are sorry for the inconvenience, but you and your fans will appreciate it later! </br></p>
            <p>
Want to learn about Strimm? <a class="maintenanceLearnMore" href="https://youtu.be/JgXAuLCWOsQ"> Watch this short video</a> 
</p>
            <p>Strimm Team</p>
            </div>
    </div>
</asp:Content>
