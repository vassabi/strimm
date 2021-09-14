<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="ChannelStatisticsTest.aspx.cs" Inherits="StrimmTube.ChannelStatisticsTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="canonicalHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    <script src="/JS/canvasjs.min.js"></script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        window.onload = function () {

    var chart = new CanvasJS.Chart("chartContainer",
    {
      title:{
        text: "Simple Date-Time Chart"
    },
    axisX:{
        title: "timeline",
        gridThickness: 2
    },
    axisY: {
        title: "Downloads"
    },
    data: [
    {        
        type: "area",
        dataPoints: [//array
        { x: new Date(2012, 01, 1), y: 26},
        { x: new Date(2012, 01, 3), y: 38},
        { x: new Date(2012, 01, 5), y: 43},
        { x: new Date(2012, 01, 7), y: 29},
        { x: new Date(2012, 01, 11), y: 41},
        { x: new Date(2012, 01, 13), y: 54},
        { x: new Date(2012, 01, 20), y: 66},
        { x: new Date(2012, 01, 21), y: 60},
        { x: new Date(2012, 01, 25), y: 53},
        { x: new Date(2012, 01, 27), y: 60}

        ]
    }
    ]
});

    chart.render();
}
</script>
    <div id="chartContainer" style="height: 300px; width: 100%;">
</div>
</asp:Content>
