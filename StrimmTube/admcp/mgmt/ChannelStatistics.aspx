<%@ Page Title="" Language="C#" MasterPageFile="~/admcp/mgmt/Admin.Master" AutoEventWireup="true" CodeBehind="ChannelStatistics.aspx.cs" Inherits="StrimmTube.admcp.mgmt.ChannelStatistics" %>
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
    <script src="../../Plugins/date.format.js" type="text/javascript"></script>
   
    <style>
        table.dataTable.row-border tbody th, table.dataTable.row-border tbody td, table.dataTable.display tbody th, table.dataTable.display tbody td {
  border-top: 1px solid #ddd;
  font-size: 14px;
  text-align: left;
  width:20%;

}
    </style>
    <script type="text/javascript">
        var now = new Date();
        var clientTime = now.format("m-d-Y-H-i");
        var webMethodGetChannelStatistics = "../../WebServices/ChannelWebService.asmx/GetChannelStatistics";
        var webMethodGetAllUsersJson = "../../WebServices/UserService.asmx/GetAllUsers";
        var webMethodGetAllVisitors = "../../WebServices/UserService.asmx/GetNumberOfNotChannelVisitors";
        var data;
        var totalusers = 0;
        var usersWithChannels = 0;
        var viewersOnly = 0;
        var totalChannels = 0;
        var usersByCountry = 0;
        var TotalVisitors = 0;

        //function secondsToHms(d) {
        //    d = Number(d);
        //    var h = Math.floor(d / 3600);
        //    var m = Math.floor(d % 3600 / 60);
        //    var s = Math.floor(d % 3600 % 60);
        //    return ((h > 0 ? h + "h:" + (m < 10 ? "0" : "") : "") + m + "m:" + (s < 10 ? "0" : "") + s+"s");
        //}
        function secondsToHms(totalSeconds) {
            var hours   = Math.floor(totalSeconds / 3600);
            var minutes = Math.floor(totalSeconds % 3600 / 60);
            var seconds = Math.floor(totalSeconds % 3600 % 60);

            // round seconds
            //seconds = Math.round(seconds * 100) / 100

            var result = (hours < 10 ? "0" + hours : hours);
            result += ":" + (minutes < 10 ? "0" + minutes : minutes);
            result += ":" + (seconds  < 10 ? "0" + seconds : seconds);
            return result;
        }
        function GetAllVisitors()
        {
            $.ajax({
                type: "POST",
                url: webMethodGetAllVisitors,
                cashe: false,
                
                dataType: "json",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    $("#spnUserCount").text(response.d);
                }
            });
        }

        function GetChannelStatistics() {
            var dataset = [];
            $.ajax({
                type: "POST",
                url: webMethodGetChannelStatistics,
                cashe: false,
                data: '{"clientTime":' + "'" + clientTime + "'" + '}',
                dataType: "json",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    var cdata = response.d;
                   // console.log(cdata);
                  //  var data = JSON.parse(cdata);
                    var totalChannelViewers = 0;
                    $.each(cdata, function (i, c) {
                        if (c.publicName == "valula") {
                            //console.log(c.channelTubeList);
                        }
                        //console.log(c.ChannelOwnerEmail);
                        var obj = Array();
                        var channelNames = "";
                        obj[0] = c.Name;
                        var d = new Date(parseInt(c.CreatedDate.replace('/Date(', '')));
                        obj[1] = d.toLocaleDateString()
                    
                        obj[2] = c.IsOnAir;
                        if (c.PictureUrl != "" || c.PictureUrl != null)
                        {
                            obj[3] = "yes";
                        }
                        else
                        {
                            obj[3] = "no";
                        }
                       
                        obj[4] = c.IsProEnabled;
                        if (c.LastVisit != null)
                        {
                            var lv = new Date(parseInt(c.LastVisit.replace('/Date(', '')));
                            obj[5] = lv.toLocaleDateString();
                            ++totalChannelViewers;
                        }
                        else
                        {
                            obj[5] = "not visited";
                        }
                        
                        obj[6] = c.VisitorCount;
                        obj[7] = c.ChannelViewsCount;
                        var convertedTime = secondsToHms(c.VisitTime);
                        obj[8] = convertedTime;
                        obj[9] = c.SubscriberCount;
                        obj[10] = c.LikeCount;
                        obj[11] = Math.round(c.Rating).toFixed(1);;
                        obj[12] =0;

                       
                        obj[13] = c.ChannelOwnerUserName;
                        obj[14] = c.ChannelOwnerFirstName;
                        obj[15] = c.ChannelOwnerEmail;
                        

                      

                        dataset.push(obj);
                       

                    });
                    $("#spnTotalCalendar").text(totalChannelViewers);
                },
                complete: function () {

                   
                   
                    $('#channelStatistics').html("").html('<table cellpadding="0" cellspacing="0" border="0" class="display" id="example"></table>');
                    $('#example').dataTable({
                        "dom": 'T<"clear">lfrtip',

                        "data": dataset,
                        "columns": [
                            { "title": "Channel" },
                            { "title": "Date Of Creation" },
                            { "title": "On Air" },
                            { "title": "Unique Avatar", "class": "center" },
                            { "title": "Connected to Pro", "class": "center" },
                            { "title": "Last Visit Date" },
                            { "title": "Visitors" },
                            { "title": "Views" },
                            { "title": "Avarage time per visit", "class": "center" },
                            { "title": "Fans", "class": "center" },
                            { "title": "likes", "class": "center" },
                            { "title": "Rating" },
                            { "title": "Abuse reports" },
                            { "title": "Public Name" },
                            { "title": "First Name" },
                            { "title": "Email" }
                           

                        ]
                    });
                }
            });
        }


        function GetAllUsers() {
            var dataset = [];
            $.ajax({
                type: "POST",
                url: webMethodGetAllUsersJson,
                cashe: false,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    var cdata = response.d;
                    //console.log(cdata);
                    var data = JSON.parse(cdata);

                    $.each(data, function (i, c) {
                       
                        var obj = Array();
                        
                       

                       
                        

                        dataset.push(obj);
                        if (c.numbOfChannels > 0) {
                            usersWithChannels++
                        }
                        else {
                            viewersOnly++;
                        }

                        totalChannels = totalChannels + parseInt(c.numbOfChannels, 10);

                    });
                },
                complete: function () {

                    totalusers = dataset.length;

                    $("#lblTotalUsers").text(totalusers.toString());
                    $("#lblWithChannels").text(usersWithChannels);
                    $("#lblViewersOnly").text(viewersOnly);
                    $("#lblTotalChannels").text(totalChannels);

                   
                }
            });
        }




        $(document).ready(function () {
            GetChannelStatistics();
            GetAllUsers();
            GetAllVisitors();

        });


    </script>




        <div class="statisticsHolder">
    <h1 class="statisticsTitle">Channel Statistics</h1>


</div>
      <div runat="server" id="divCommonStatHolder">
     <table id="table1" class="tinytable" cellspacing ="0" cellpadding="0" border="0">
         <thead">
             <tr>
                 <th>
                     <h3>
                         Total users
                     </h3>
                 </th>
                  <th>
                     <h3>
                        Users with Channel
                     </h3>
                 </th>
                  <th>
                     <h3>
                        Viewers only
                     </h3>
                 </th>
                  <th>
                     <h3>
                          Total Channels
                     </h3>
                 </th>
                 
             </tr>
         </thead>
         <tbody>
             <tr>
                 <td>
                    <div id="lblTotalUsers"></div>
                 </td>
                  <td>
                     <span id="lblWithChannels"></span>
                 </td>
                  <td>
                     <span id="lblViewersOnly"></span>
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
