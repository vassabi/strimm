<%@ Page Title="" Language="C#" MasterPageFile="~/admcp/mgmt/Admin.Master" AutoEventWireup="true" CodeBehind="EmbeddedChannels.aspx.cs" Inherits="StrimmTube.admcp.mgmt.EmbeddedChannels" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="DataTables-1.10.5/media/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css"/>
      <link href="css/statistics.css" rel="stylesheet" />
      <link href="Plugins/TablePlugin/css/styleRespScroll.css" rel="stylesheet" />
    <link href="Plugins/TablePlugin/css/stylesContact.css" rel="stylesheet" />
    <link href="DataTables-1.10.5/TableTools/css/dataTables.tableTools.min.css" rel="stylesheet" />
   
    <script src="DataTables-1.10.5/media/js/jquery.dataTables.min.js" charset="utf-8"></script>
    <script src="DataTables-1.10.5/TableTools/js/dataTables.tableTools.min.js"></script>
    <script src="js/statistics.js"></script>
    <script async="async" src="../../Plugins/date.format.js" type="text/javascript"></script>
   
    <style>
        table.dataTable.row-border tbody th, table.dataTable.row-border tbody td, table.dataTable.display tbody th, table.dataTable.display tbody td {
            border-top: 1px solid #ddd;
            font-size: 14px;
            text-align: center;
            width: 20%;
        }

        #menuTopList ul {
            width: 205px;
        }
    </style>
    <script type="text/javascript">
        var now = new Date();
        var clientTime = now.format("m-d-Y-H-i");
        var webMethodGetEmbeddedChannelsInfo = "../../WebServices/ChannelWebService.asmx/GetEmbeddedChannelsInfo";
        var channelNamesArr = [];
        var data;
        var loads = 0;



        function secondsToHms(totalSeconds) {
            var hours = Math.floor(totalSeconds / 3600);
            var minutes = Math.floor(totalSeconds % 3600 / 60);
            var seconds = Math.floor(totalSeconds % 3600 % 60);

            // round seconds
            //seconds = Math.round(seconds * 100) / 100

            var result = (hours < 10 ? "0" + hours : hours);
            result += ":" + (minutes < 10 ? "0" + minutes : minutes);
            result += ":" + (seconds < 10 ? "0" + seconds : seconds);
            return result;
        }


        function GetEmbeddedChannelsInfo() {
            var dataset = [];
            $.ajax({
                type: "POST",
                url: webMethodGetEmbeddedChannelsInfo,
                cashe: false,
                dataType: "json",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    console.log(response);
                    var cdata = response.d;


                    $.each(cdata, function (i, d) {

                        var obj = Array();
                        //embedded channels total (unuiqe)
                        if (!channelNamesArr.contains(d.Name)) {
                            channelNamesArr.push(d.Name);
                        }
                        obj[0] = d.Name;
                        var date = new Date(parseInt(d.DateOfEmbedding.replace('/Date(', '')));
                        obj[1] = date.toLocaleDateString();
                        obj[2] = d.EmbeddedHostUrl;
                        if (d.IsSubscribedDomain) {
                            obj[3] = "yes";
                        }
                        else {
                            obj[3] = "no";
                        }
                        obj[4] = d.UserCount;
                        obj[5] = d.Username;
                        obj[6] = d.AccountNumber;
                        obj[7] = d.LoadCount;
                        obj[8] = secondsToHms(d.VisitTime);
                        loads = loads + d.LoadCount;
                        dataset.push(obj);


                    });
                    $("#lblTotalChannels").text(channelNamesArr.length);
                    $("#lblTotalViewers").text(loads);
                },
                complete: function () {



                    $('#channelStatistics').html("").html('<table cellpadding="0" cellspacing="0" border="0" class="display" id="example"></table>');
                    $('#example').dataTable({
                        "dom": 'T<"clear">lfrtip',

                        "data": dataset,
                        "columns": [
                            { "title": "Channel Name", "class": "left" },
                            { "title": "Date of Embedding ", "class": "left" },
                            { "title": "Host Name", "class": "left" },
                            { "title": "Is Subscribed Domain", "class": "left" },
                            { "title": "Number of Users", "class": "left" },
                            { "title": "Username", "class": "left" },
                            { "title": "AccountNumber", "class": "left" },
                            { "title": "Number of Loads", "class": "left" },
                             { "title": "AVG visit time", "class": "left" },



                        ]
                    });
                }
            });
        }
        Array.prototype.contains = function (obj) {
            var i = this.length;
            while (i--) {
                if (this[i] == obj) {
                    return true;
                }
            }
            return false;
        }

        function GetTotalViewers() {
            $.ajax({
                type: "POST",
                url: webMethodGetEmbeddedChannelsInfo,
                cashe: false,
                dataType: "json",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    $("#lblTotalUsers").text(totalusers.toString());
                }
            });
        }




        $(document).ready(function () {

            GetEmbeddedChannelsInfo();


        });


    </script>


     <div class="statisticsHolder">
    <h1 class="statisticsTitle">Embedded Channels</h1>


</div>
      <div runat="server" id="divCommonStatHolder">
     <table id="table1" class="tinytable" cellspacing ="0" cellpadding="0" border="0">
         <thead">
             <tr>
                 <th>
                     <h3>
                         Total viewers
                     </h3>
                 </th>
                  <th>
                     <h3>
                        Total embedded channels
                     </h3>
                 </th>
               
                 
             </tr>
         </thead>
         <tbody>
             <tr>
                 <td>
                    <div id="lblTotalViewers"></div>
                 </td>
                  <td>
                     <span id="lblTotalChannels"></span>
                 </td>
                 
                 
             </tr>
         </tbody>
     </table>

    </div>
 
    <div id="TotalUsersByCalendar">
        <h1>Time Period</h1>
        <select id="selectTotalByCalendar" onchange="GetChannelsByCalendar()">
              <option value="0">Select</option>
            <option value="1">Custom</option>
            <option value="2">Today</option>
            <option value="3">Yesterday</option>
            <option value="4">This month</option>
            <option value="5">Last month</option>
        </select>
        <div id="customOption" style="display:none; float:left; margin-right:25px;">
            <span>from:</span>
            <input type="text" id="inputFrom" />
            <span>to:</span>
            <input type="text" id="inputTo" />
            <a id="ancGo" class="button blue">go</a>
        </div>
        <h1>Total  viewers</h1>
        <span id="spnUserCount">0</span>
         <h1>Total channel viewers</h1>
        <span id="spnTotalCalendar">0</span>
        <a id="resetCalendar" class="button blue" onclick="ResetCalendarAndChannelStatistics()">reset</a>
    </div>

    
    <div id="channelStatistics">

    </div>

</asp:Content>
