<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="HlsTest.aspx.cs" Inherits="StrimmTube.HlsTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="canonicalHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    <!-- <head> -->
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="//cdn.flowplayer.com/releases/native/stable/style/flowplayer.css" />
    <!-- <body> -->
    <div class="hlsplr"></div>
    <script src="//cdn.flowplayer.com/releases/native/stable/default/flowplayer.min.js"></script>
    <script src="//cdn.flowplayer.com/releases/native/stable/plugins/hls.min.js"></script>

    <h1>Testing HLS videos</h1>

    <div class="hlsplr">
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            var options =
            {
                src: "//bitmovin-a.akamaihd.net/content/playhouse-vr/m3u8s/105560.m3u8"
            }

            window.player =
                flowplayer(".hlsplr", options)
        });
    </script>
</asp:Content>
