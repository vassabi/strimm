<%@ Page Title="" Language="C#" MasterPageFile="~/admcp/mgmt/Admin.Master" AutoEventWireup="true" CodeBehind="StatisticsNew.aspx.cs" Inherits="StrimmTube.admcp.mgmt.StatisticsNew" %>
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
     <script src="js/date.format.js"></script>
    <style>
        table.dataTable.row-border tbody th, table.dataTable.row-border tbody td, table.dataTable.display tbody th, table.dataTable.display tbody td {
  border-top: 1px solid #ddd;
  font-size: 14px;
  text-align: left;
  width:20%;

}
    </style>
    <script type="text/javascript">
        var webMethodGetAllUsersJson = "../../WebServices/UserService.asmx/GetAllUsers";
        var data;
        var totalusers=0;
        var usersWithChannels = 0;
        var viewersOnly = 0;
        var totalChannels = 0;
        var usersByCountry = 0;
        var now = new Date();
        var clientTime = now.format("m-d-Y-H-i");
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
                        
                        var obj=Array();
                        var channelNames="";
                        obj[0] = c.firstName;
                        obj[1] = c.lastName;
                        obj[2] = c.gender;
                        obj[3] = c.age;
                        obj[4] = c.publicName;
                        obj[5] = c.email;
                        obj[6] = c.dateofsignup;
                        obj[7] = c.isfilmmaker == true ? 'Yes' : '';
                        obj[8] = c.country;
                        obj[9] = c.address;
                        obj[10] = c.state;                      
                        obj[11] = c.city;
                        obj[12] = c.zipCode;
                        obj[13] = c.numbOfChannels;

                        $.each(c.channelTubeList, function (k, j)
                        {
                            channelNames += j.Name + ", ";
                        })
                        obj[14] = channelNames;
                        obj[15] = c.accountStatus;
                        obj[16] = c.signUpGeoLocation;
                        obj[17] = c.lastGeoLocation;
                        obj[18] = "<a target='_blank' href='UserManagement.aspx?id=" + c.userId + "'>edit</a>";
                       
                        dataset.push(obj);
                        if (c.numbOfChannels > 0)
                        {
                            usersWithChannels++
                        }
                        else
                        {
                            viewersOnly++;
                        }
                       
                        totalChannels = totalChannels+parseInt(c.numbOfChannels, 10);
                       
                    });
                },
                complete: function () {
                   
                    totalusers = dataset.length;
                 
                    $("#lblTotalUsers").text(totalusers.toString());
                    $("#lblWithChannels").text(usersWithChannels);
                    $("#lblViewersOnly").text(viewersOnly);
                    $("#lblTotalChannels").text(totalChannels);

                    $('#userTable').html('<table cellpadding="0" cellspacing="0" border="0" class="display" id="example"></table>');
                    $('#example').dataTable({
                        "dom": 'T<"clear">lfrtip',
                       
                        "data": dataset,
                        "columns": [
                            { "title": "First Name" },
                            { "title": "Last Name" },
                            { "title": "Gender" },
                            { "title": "Age" },
                            { "title": "Public Name", "class": "center" },
                            { "title": "email", "class": "center" },
                            { "title": "Date of Sign Up" },
                            { "title": "Is Film Maker" },
                            { "title": "Country" },
                            { "title": "Address", "class": "center" },
                            { "title": "State", "class": "center" },
                            { "title": "City", "class": "center" },
                            { "title": "Zip/Postal Code" },
                            { "title": "Number of Channels" },
                            { "title": "Name of Channels" },
                            { "title": "Account Status" },
                            { "title": "SignUp GEO Location" },
                            { "title": "Last GEO Location" },
                            { "title": "Edit" }
                            
                        ]
                    });
                }
            });
        }
                    

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
                
           
        
      
        $(document).ready(function () {
            GetAllUsers();
            GetCountries();
          
        });

     
    </script>




        <div class="statisticsHolder">
    <h1 class="statisticsTitle">Statistics</h1>


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
    <div id="totalUsersByLocation">
        <h1>Total Users by location</h1>
        <select id="pickTheCountry" onchange="GetStatesAndUsers()">
           
        </select>
        <select id="pickState" onchange="GetUsersByState()" style="display:none">

        </select>
        <span id="spnUserCount">0</span>
       <a id="ancResetUsers" class="button blue" onclick="ResetUsersCount()">reset</a>
    </div>
    <div id="TotalUsersByCalendar">
        <h1>Total Users By Calendar</h1>
        <select id="selectTotalByCalendar" onchange="GetUsersByCalendar()">
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
        <span id="spnTotalCalendar">0</span>
        <a id="resetCalendar" class="button blue" onclick="ResetCalendar()">reset</a>
    </div>

    
    <div id="userTable">

    </div>

</asp:Content>
