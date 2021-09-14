<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Monetize.aspx.cs" Inherits="StrimmTube.Monetize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content=" Strimm is a Social Internet TV Network. It allows watching free video shows online continuously, create public TV streaming and socialize with friends" />
    <meta name="keywords" content=" internet tv, social tv, social network, watch videos, tv network, videos online, online videos, schedule videos, schedule broadcast, create tv, playlist builder, free videos, watch videos online, tv streaming" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        h1 {
            color: #FFFFFF;
            display: block;
            font-size: 13px;
            line-height: 30px;
            margin: 0 auto;
            text-transform: uppercase;
            width: 837px;
        }

        #content {
            margin: 30px auto;
            width: 100%;
            min-height: 500px;
        }

        #spnTempMessage {
            color: #555555;
            display: block;
            line-height: 22px;
            margin: 20px auto;
            width: 80%;
            text-align: center;
        }

        #content img {
            display: block;
            margin: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divTitleUrlHolder">
    </div>
    <div id="content">
        <img src="/images/comingSoon.jpg" />
        <span id="spnTempMessage">This option is coming soon.<br />
            Start acquiring your fans and increase audience to be ready to monetize!</span>
    </div>
</asp:Content>
