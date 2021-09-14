<%@ Page Title="" Language="C#" MasterPageFile="~/admcp/mgmt/Admin.Master" AutoEventWireup="true" CodeBehind="Subscribtions.aspx.cs" Inherits="StrimmTube.admcp.mgmt.Subscribtions" %>
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
    <script src="js/subscribtions.js"></script>

    
    <script src="js/date.format.js"></script>
    <style  >
        table.dataTable.row-border tbody th, table.dataTable.row-border tbody td, table.dataTable.display tbody th, table.dataTable.display tbody td {
            border-top: 1px solid #ddd;
            font-size: 14px;
            text-align: center;
            width: 20%;
        }

        #menuTopList ul {
            width: 205px;
        }
        .statisticsTitle {
  color: #fff;
  font-size: 34px;
  font-weight: normal;
  height: 200px;
  letter-spacing: 1px;
  line-height: 250px;
  margin: auto;
  text-align: center;
  width: 600px;
}
        #tblTimePeriod td{
text-align:center;
height:40px;
}
        #resetCalendar
        {
           float: right;
margin-right: 5%;
margin-top: 20px;
        }

         #tblTimePeriod
         {
             width:100%;
            margin:auto;
         }

         .ddHolder {
	display: block;
	margin-top: -6px;
}
#divChannelsCategory {
display: none;
left: 0;
margin: auto;
position: absolute;
    top: 6px;
width: 100%;
    background: #2a99bd;
    opacity: 0.97;
    border-top: 1px solid #2a99bd;
    height: 170px;
}

#divChannelsCategory.loading{
    background:#2a99bd url("../images/loading-spinWhite.svg") no-repeat center;
}

    #divChannelsCategory ul li {
float: left;
height: 55px;
line-height: 20px;
width: 340px;
    }

        #divChannelsCategory ul li:hover {
            color: #fff;
        }

        #divChannelsCategory ul li.last {
            border-bottom: none;
        }



    /*#divChannelsCategory ul li.first {
        border-bottom: 1px solid #eee;
    }*/

    /*#divChannelsCategory ul li.last {
        border-top: 1px solid #f9f9f9;
    }*/

    #divChannelsCategory ul li.lastColumn {
        border-right: none;
    }

    #divChannelsCategory ul li.firstColumn {
        border-left: none;
    }

    #divChannelsCategory ul li a {
color: #eee;
display: block;
    font-size: 15px;
    height: 20px;
    line-height: 24px;
margin: auto;
    padding: 14px 0 7px;
position: relative;
text-align: left;
text-decoration: none;
text-transform: capitalize;
width: 160px;
    line-height: 27px;
    }

        #divChannelsCategory ul li a:hover {
            color: #fff;
           text-decoration: underline;
        }

.embDivHolder {
    display: block;
    float: left;
    width: 100%;
    height: 650px;
}

    #divChannelsCategory ul {
display: block;
margin: auto;
    padding-top: 30px;
    width: 100%;
    height: 100%;
    }

#divChannelsCategory .galleryView {
    display: block;
    width: 100%;
    height: 50px;
    position: absolute;
    bottom: 45px;
}

    #divChannelsCategory .btnGalleryView {
        display: block;
        margin: auto;
        width: 300px;
        height: 50px;
        line-height: 50px;
        color: #fff600;
        text-align: center;
        background-image: url('/images/social/channelGallery.png');
        background-position: 40px;
        background-repeat: no-repeat;
        cursor: pointer;
        font-size: 15px;
            font-weight: bold;
    }

    #divChannelsCategory .btnGalleryView:hover {
        text-decoration: underline;
    }
        
    </style>
 
    <script>
        var browseMenuHtml = '<%=browseMenuHtml%>';
        $(document).ready(function () {
            console.log(browseMenuHtml);
            $("#divChannelsCategory").html("").html(browseMenuHtml);

            ResetTimePeriod();
            CreateTotalSubscribtionTable();
            GetChannelTubeCountsByCategoryForExistingSubscriptions();
            CreateOrderTable(null, null);

        });
    </script>

     <div class="statisticsHolder">
    <h1 class="statisticsTitle">Embedded Channels Subscribers</h1>


</div>
      <div runat="server" id="divCommonStatHolder">
     <table id="table1" class="tinytable" cellspacing ="0" cellpadding="0" border="0">
         <thead">
             <tr>
                 <th>
                     <h3>
                        Total Paid Subscribers
                     </h3>
                 </th>
                  <th>
                     <h3>
                        Total Subsribed Channels
                     </h3>
                 </th>

                  <th>
                     
                        
                       
                             <h3 style="float:left;">Total Paid Subscriptions</h3>
                            
                             
                            
                                 
                           
                            
                         
                     
                     <%--▲--%>
                       
                      
                      
                 </th>
                
                 
             </tr>
         </thead>
         <tbody>
             <tr>
                 <td>
                    <div id="lblTotalPaidSubscribers"></div>
                 </td>
                  <td>
                     <span id="lblTotalSubsribedChannels"></span>
                       <a style="float:right;" onclick="ToggleChannels()">Categories ▼</a>
                          <div id="divChannelsCategory">
                   <div class="ddHolder"> <span class="trBrowseChannels"></span></div>
                    <ul>
                    </ul>
                
                </div>   
                 </td>
                   <td>
                     <span id="TotalPaidSubscribtions"></span>
                 </td>
                 
             </tr>
         </tbody>
     </table>

    </div>
 
    <div id="TotalUsersByCalendar">

        <table id="tblTimePeriod" class="tinytable">
            <tr>
                <th id="timePeriod"><h3>Time Period</h3></th>
                <th id="trialSubscrib"><h3>In Trial Subscription</h3></th>
                <th id="activeSubscr"><h3>Active Subscription</h3></th>
                <th id="canceledSubscr"><h3>Canceled Subscription</h3></th>
                <th id="embededChannelCount"><h3>Embedded Channel Count</h3></th>

             </tr>
            <tr>
                <td rowspan="3" headers="timePeriod">
                    <div style="position:relative; width:100%;height:100%;">
                           <select id="selectTotalByCalendar" onchange="GetSubscribtionsByCalendar()" style="padding:4px 6px 6px">
                        <option value="0">Select</option>
                        <option value="1">Custom</option>
                        <option value="2">Today</option>
                        <option value="3">Yesterday</option>
                        <option value="4">This month</option>
                        <option value="5">Last month</option>
                    </select>
                    <div id="customOption" style="display: none; top: 45px; width:80%; position:absolute;">
                        <span style="display:block; float:left; clear:both;">from:</span>
                        <input style="display: block;float: right;width: 95%;margin-bottom: 10px;" type="text" id="inputFrom" />
                        <span style="display:block; float:left; clear:both;">to:</span>
                        <input style="display: block;float: right;width: 95%;margin-bottom: 10px;"  type="text" id="inputTo" />
                        <a style="clear: both;color: white;display: block;float: right;height: 23px;line-height: 23px;width: 33px;"  id="ancGo" class="button blue">go</a>
                    </div>
                    </div>
                 
                </td>          
                <td headers="trialSubscrib">
                    
                    <span id="spnTrial">0</span>
                </td>
                <td headers="activeSubscr">                    
                    <span id="spnActive">0</span>
                </td>
                <td headers="canceledSubscr">                  
                    <span id="spnCanceled">0</span>
                </td>
                <td headers="embededChannelCount">                   
        <span id="spnEmbeddedChannelCount">0</span>
                </td>
                
            </tr>
            <tr>
                <th id="whiteLabelChannelCount"><h3>White Label Channel Count</h3></th>
                <th id="pswrdProtctionChannelCount"><h3>Password Protection Channel Count</h3></th>
                <th id="customLabelCount"><h3>Custom Label Channel Count</h3></th>
                <th id="mutedCount"><h3>Muted Channel Count</h3></th>
              
            </tr>
            <tr>
                <td headers="whiteLabelChannelCount">
                   <span id="spnWhiteLabelCount">0</span>
                </td>
                <td headers="pswrdProtctionChannelCount">
                    <span id="spnPassProtectedCount">0</span>
                </td>
                <td headers="customLabelCount">
                    <span id="spnCustomLabelCount">0</span>
                </td>
                <td headers="mutedCount">
                    <span id="spnMutedCount">0</span>
                </td>
               
            </tr>
        </table>
   
                       <a id="resetCalendar" class="button blue" onclick="ResetTimePeriod ()">reset</a>
             
   <%--     <div id="timePeriodFirstLine">
            
        </div>
         <div id="timePeriodSecondLine">
             
        
        
             
             </div>--%>


      
    </div>
    <div id="transactionDetailsPopup" style="width:70%; height:auto; padding:50px; display:none; background-color:#fff;">
         <a id="close_x" class="close close_x" href="#" onclick="ClosePopup()">x</a>
        <h2>transaction details</h2>
        <div id="transactionDetails">

        </div>
    </div>

     <div id="channelDetailsPopup" style="width:70%; height:auto; padding:50px; display:none; background-color:#fff;">
          <a id="close_x" class="close close_x" href="#" onclick="ClosePopup()">x</a>
         <h2>channel details</h2>
         <div id="channelDetails">

         </div>
    </div>
    
    <div id="subscribtionStatistics">

    </div>

</asp:Content>