<%@ Page Title="" Language="C#" MasterPageFile="~/admcp/mgmt/Admin.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="StrimmTube.admcp.mgmt.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Plugins/TablePlugin/css/styleRespScroll.css" rel="stylesheet" />
    <link href="Plugins/TablePlugin/css/stylesContact.css" rel="stylesheet" />
    <link href="css/statistics.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="Plugins/TablePlugin/js/modernizr.custom.63321.js"></script>
      
    <script src="js/statistics.js"></script>
   
     <div id="divTitleUrlHolder">
        <h1 class="pageTitle">Statistics</h1>
       
    </div>
    <div runat="server" id="divCommonStatHolder">
     <table id="table1" class="tinytable" cellspacing ="0" cellpadding="0" border="0">
         <thead>
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
                  <th>
                     <h3>
                         Total boards
                     </h3>
                 </th>
             </tr>
         </thead>
         <tbody>
             <tr>
                 <td>
                     <asp:Label runat="server" ID="lblTotalUsers"></asp:Label>
                 </td>
                  <td>
                     <asp:Label runat="server" ID="lblWithChannels"></asp:Label>
                 </td>
                  <td>
                     <asp:Label runat="server" ID="lblViewersOnly"></asp:Label>
                 </td>
                  <td>
                     <asp:Label runat="server" ID="lblTotalChannels"></asp:Label>
                 </td>
                  <td>
                     <asp:Label runat="server" ID="lblTotalBoards"></asp:Label>
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
    <div id="tablewrapper" >
           <h1>All Users</h1>
		<div id="tableheader">
        	<div class="search">
                <select id="columns" onchange="sorter.search('query')"></select>
                <input type="text" id="query" onkeyup="sorter.search('query')" />
            </div>
            <span class="details">
				<div>Records <span id="startrecord"></span>-<span id="endrecord"></span> of <span id="totalrecords"></span></div>
        		<div class="btn-reset"><a class="button blue" href="javascript:sorter.reset()">reset</a></div>
        	</span>
        </div>
        <section id="flip-scroll">
      <%--table here--%>
             <div runat="server" id="usersTableHolder">
       
   </div>
        </section>
        <div id="tablefooter">
         
          <div id="tablenav">
            	<div>
                    <img src="Plugins/TablePlugin/images/first.gif" width="16" height="16" alt="First Page" onclick="sorter.move(-1,true)" />
                    <img src="Plugins/TablePlugin/images/previous.gif" width="16" height="16" alt="First Page" onclick="sorter.move(-1)" />
                    <img src="Plugins/TablePlugin/images/next.gif" width="16" height="16" alt="First Page" onclick="sorter.move(1)" />
                    <img src="Plugins/TablePlugin/images/last.gif" width="16" height="16" alt="Last Page" onclick="sorter.move(1,true)" />
                </div>
                <div>
                	<select  id="pagedropdown"></select>
				</div>
                <div class="btn-reset"><a class="button blue" href="javascript:sorter.showall()">view all</a>
                </div>
            </div>
			<div id="tablelocation">
            <div>
                  <select onchange="sorter.size(this.value)">
                    <option value="5">5</option>
                        <option value="10" selected="selected">10</option>
                        <option value="20">20</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                    <span class="txt-page">Entries Per Page</span>
                </div>

            	
                <div class="page txt-txt">Page <span id="currentpage"></span> of <span id="totalpages"></span></div>
            </div>
            <input type="button" onclick="tableToExcel('table', 'all strimm users')" value="Export to .csv file"/>

        </div>
    </div>
    <div id="modal">
	<div id="heading" class="heading-color">
		 send an email
	</div>

	<div id="content">
        <div class="txt-subject">
        <p style="margin-left:10px;">Subject: </p></div> 
        <div class="content-subject">
        <input type="text"/></div>
		<div class="txt-email">
        <p style="margin-left:10px;">Email: </p></div> 
        <div class="content-email">
        <p id="email" style=" color:#464747; font:12px;"></p></div>
        <div class="txt-message"><p>Message: </p></div> 
        <div class="content-message">
        <textarea style="width:100%;background-color:#f7fbfe; margin-left:10px; height:100px;"></textarea></div>
        <div class="contact-img"><img src="Plugins/TablePlugin/images/email.png" class="img-contact" alt=""/></div>

		<div style="margin: 0 0 0 10px;"><a href="#" class="button blue position" onclick="SendEmail()">Send</a></div>
	</div>
        </div>
    <script src="Plugins/TablePlugin/script.js"></script>
    <script type="text/javascript">
        var sorter = new TINY.table.sorter('sorter', 'table', {
            headclass: 'head',
            ascclass: 'asc',
            descclass: 'desc',
            evenclass: 'evenrow',
            oddclass: 'oddrow',
            evenselclass: 'evenselected',
            oddselclass: 'oddselected',
            paginate: true,
            size: 10,
            colddid: 'columns',
            currentid: 'currentpage',
            totalid: 'totalpages',
            startingrecid: 'startrecord',
            endingrecid: 'endrecord',
            totalrecid: 'totalrecords',
            hoverid: 'selectedrow',
            pageddid: 'pagedropdown',
            navid: 'tablenav',
            sortcolumn: 1,
            sortdir: 1,
            columns: [{ index: 7, format: '%', decimals: 1 }, { index: 8, format: '$', decimals: 0 }],
            init: true
        });
  </script>
    <script src="Plugins/TablePlugin/js/jquery.reveal.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.button-email').click(function (e) { // Button which will activate our modal
                var title = $(this).attr('title');
                var title2 = $('.name').attr('title');
                document.getElementById("email").innerHTML = title.toString();
                $('#modal').reveal({ // The item which will be opened with reveal
                    animation: 'fade',                   // fade, fadeAndPop, none
                    animationspeed: 600,                       // how fast animtions are
                    closeonbackgroundclick: true,              // if you click background will modal close?
                    dismissmodalclass: 'close'    // the class of a button or element that will close an open modal
                });
                return false;
            });
        });
	</script> 
</asp:Content>

