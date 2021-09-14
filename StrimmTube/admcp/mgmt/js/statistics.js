/// <reference path="../WebServiceAdmin/StatisticsWS.asmx" />
var WebMethodSendEmailToUser = "WebServiceAdmin/StatisticsWS.asmx/SenEmailToUser";
var WebMethodGetStates = "WebServiceAdmin/StatisticsWS.asmx/GetStatesByCountryId";
var WebMethodGetUsersCountByCountry = "WebServiceAdmin/StatisticsWS.asmx/GetUsersByCountry";
var WebMethodGetUsersByState = "WebServiceAdmin/StatisticsWS.asmx/GetUsersByState";
var WebMethodGetUsersByCalendar = "WebServiceAdmin/StatisticsWS.asmx/GetUsersByCalendar";
var WebMethodGetUsersByCustomCalendar = "WebServiceAdmin/StatisticsWS.asmx/GetCustomCalendarCount";
var WebMethodGetCountries = "WebServiceAdmin/StatisticsWS.asmx/GetCountries";
var WebMethodGetChannelsByCalendar = "../../WebServices/ChannelWebService.asmx/GetChannelsByCalendar";
var WebMethodGetEmbeddedChannelsByCalendar = "../../WebServices/ChannelWebService.asmx/GetEmbeddedChannelsByCalendar";
var WebMethodGetCustomChannelStatistics = "../../WebServices/ChannelWebService.asmx/GetCustomChannelStatistics";
$(document).ready(function () {
    ResetUsersCount();
    ResetCalendar();
});
function ResetUsersCount() {
    $("#pickTheCountry option[value=0]").attr("selected", "selected");
    $("#pickState").empty();
    $("#spnUserCount").text("0");
    $("#pickState").hide();
};
function ResetCalendar() {
    $("#selectTotalByCalendar option[value=0]").attr("selected", "selected");
    $("#customOption").hide();
    $("#inputFrom, #inputTo").empty();
    $("#spnTotalCalendar").text("0");

};
function ResetCalendarAndChannelStatistics() {
    $("#selectTotalByCalendar option[value=0]").attr("selected", "selected");
    $("#customOption").hide();
    $("#inputFrom, #inputTo").empty();
    $("#spnTotalCalendar").text("0");
    GetChannelStatistics();


};
function SendEmail() {
    var userEmail = $("p#email").text();
    var emailBody = $(".content-message textarea").val();
    var subject = $(".content-subject input").val();
    $.ajax({
        type: "POST",
        url: WebMethodSendEmailToUser,
        data: '{"userEmail":' + "'" + userEmail + "'" + ',"emailBody":' + "'" + emailBody + "'" + ',"subject":' + "'" + subject + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            alert(response.d);
        },

    });
};

var tableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,'
      , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
      , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
      , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
        window.location.href = uri + base64(format(template, ctx))
    }
})();
function GetCountries()
{
    $.ajax({
        type: "POST",
        url: WebMethodGetCountries,      
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
           
            $("#pickTheCountry").append($("<option></option>").val("0").html("select state"));
            $.each(response.d, function (i,c) {
                $("#pickTheCountry").append($("<option></option>").val(c.CountryId).html(c.Name));
            });

        }

    });
}
function GetStatesAndUsers() {
    var selectedCountryId = $("#pickTheCountry option:selected").val();
    var selectedCountryName =  $("#pickTheCountry option:selected").text();
    $.ajax({
        type: "POST",
        url: WebMethodGetStates,
        data: '{"selectedCountryId":' + selectedCountryId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#pickState").show()
            $("#pickState").empty().append($("<option></option>").val("0").html("select state"));
            $.each(response.d, function (i,c) {
                $("#pickState").append($("<option></option>").val(c.StateId).html(c.Name));
               

            });
           
         
            $.ajax({
                type: "POST",
                url: WebMethodGetUsersCountByCountry,
                data: '{"selectedCountryVal":' + "'" + selectedCountryName + "'" + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    $("#spnUserCount").empty();
                    $("#spnUserCount").text(response.d);
                },

            });

        },

    });
};

function GetUsersByState() {
    var selectedState = $("#pickState option:selected").text();
    $.ajax({
        type: "POST",
        url: WebMethodGetUsersByState,
        data: '{"state":' + "'" + selectedState + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#spnUserCount").empty();
            $("#spnUserCount").text(response.d);
        },

    });
};

function GetUsersByCalendar() {
    $("#customOption").hide();
    var selectedOption = $("#selectTotalByCalendar option:selected").val();
  
    if (selectedOption > 1) {
        $.ajax({
            type: "POST",
            url: WebMethodGetUsersByCalendar,
            data: '{"option":' + selectedOption + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#spnTotalCalendar").empty();
                $("#spnTotalCalendar").text(response.d);
            },

        });
    }
    if (selectedOption == 1) {
        $("#inputFrom, #inputTo").datepicker({ dateFormat: 'dd/mm/yy', yearRange: '-20:+0', changeYear: true, changeMonth: true });
        $("#ancGo").attr("onclick", "GetCustomChannelStatistics()");
        $("#customOption").show();
    }
    if (selectedOption == 0) {
        ResetCalendar();
    }

};
function GetChannelsByCalendar()
{
    $("#customOption").hide();
    var selectedOption = $("#selectTotalByCalendar option:selected").val();

    if (selectedOption > 1) {
        $.ajax({
            type: "POST",
            url: WebMethodGetChannelsByCalendar,
            data: '{"option":' + selectedOption+',"clientTime":' + "'" + clientTime +"'"+ '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                GetChannelStatisticsByCalendar(response.d);
            },

        });
    }
    if (selectedOption == 1) {
        $("#inputFrom, #inputTo").datepicker({ dateFormat: 'dd/mm/yy', yearRange: '-20:+0', changeYear: true, changeMonth: true });
        $("#ancGo").attr("onclick", "GetCustomChannelStatistics()");
        $("#customOption").show();
    }
    if (selectedOption == 0) {
        ResetCalendar();
    }

}
function GetEmbeddedChannelsByCalendar() {
    $("#customOption").hide();
    var selectedOption = $("#selectTotalByCalendar option:selected").val();

    if (selectedOption > 1) {
        $.ajax({
            type: "POST",
            url: WebMethodGetEmbeddedChannelsByCalendar,
            data: '{"option":' + selectedOption + ',"clientTime":' + "'" + clientTime + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                GetChannelEmbeddedStatisticsByCalendar(response.d);
            },

        });
    }
    if (selectedOption == 1) {
        $("#inputFrom, #inputTo").datepicker({ dateFormat: 'dd/mm/yy', yearRange: '-20:+0', changeYear: true, changeMonth: true });
        $("#ancGo").attr("onclick", "GetCustomEmbeddedChannelStatistics()");
        $("#customOption").show();
    }
    if (selectedOption == 0) {
        ResetCalendar();
    }

}

function GetChannelStatisticsByCalendar(data) {
    var dataset = [];
   
    var cdata = data;
            // console.log(cdata);
            //  var data = JSON.parse(cdata);
            var totalChannelViewers = 0;
            $.each(data, function (i, c) { 
                var obj = Array();
                var channelNames = "";
                obj[0] = c.Name;
                var d = new Date(parseInt(c.CreatedDate.replace('/Date(', '')));
                obj[1] = d.toLocaleDateString()
                obj[2] = c.IsOnAir;
                if (c.PictureUrl != "" || c.PictureUrl != null) {
                    obj[3] = "yes";
                }
                else {
                    obj[3] = "no";
                }

                obj[4] = c.IsProEnabled;
                if (c.LastVisit != null) {
                    var lv = new Date(parseInt(c.LastVisit.replace('/Date(', '')));
                    obj[5] = lv.toLocaleDateString();
                    ++totalChannelViewers;
                }
                else {
                    obj[5] = "not visited";
                }
                obj[6] = c.VisitorCount;
                obj[7] = c.ChannelViewsCount;
                var convertedTime = secondsToHms(c.VisitTime);
                obj[8] = convertedTime;
                obj[9] = c.SubscriberCount;
                obj[10] = c.LikeCount;
                obj[11] = Math.round(c.Rating).toFixed(1);;
                obj[12] = 0;
                obj[13] = c.ChannelOwnerUserName;
                obj[14] = c.ChannelOwnerFirstName;
                obj[15] = c.ChannelOwnerEmail;
                dataset.push(obj);
            });
            $("#spnTotalCalendar").text(totalChannelViewers);

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
function GetCustomChannelStatistics()
{
    var dateFrom = $("#inputFrom").val();
    var dateTo = $("#inputTo").val();
    $.ajax({
        type: "POST",
        url: WebMethodGetCustomChannelStatistics,
        data: '{"dateFrom":' + "'" + dateFrom + "'" + ',"dateTo":' + "'" + dateTo + "'" +  ',"clientTime":' + "'" + clientTime + "'"+'}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            GetChannelStatisticsByCalendar(response.d)
        },

    });
}
function GetChannelEmbeddedStatisticsByCalendar(embeddedData) {
    var dataset = [];

    var cdata = embeddedData;
    // console.log(cdata);
    //  var data = JSON.parse(cdata);
    var totalChannelViewers = 0;
    $.each(cdata, function (i, d) {

        var obj = Array();
        //embedded channels total (unuiqe)
        if (!channelNamesArr.contains(d.Name)) {
            channelNamesArr.push(d.Name);
        }
        obj[0] = d.Name;
        var dateOfEmbedding = new Date(parseInt(d.DateOfEmbedding.replace('/Date(', '')));
        obj[1] = dateOfEmbedding.toLocaleDateString();
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
function GetCustomEmbeddedChannelStatistics() {
    var dateFrom = $("#inputFrom").val();
    var dateTo = $("#inputTo").val();
    $.ajax({
        type: "POST",
        url: WebMethodGetCustomChannelStatistics,
        data: '{"dateFrom":' + "'" + dateFrom + "'" + ',"dateTo":' + "'" + dateTo + "'" + ',"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            GetChannelStatisticsByCalendar(response.d)
        },

    });
}
function GetCustomOption() {
    var dateFrom = $("#inputFrom").val();
    var dateTo = $("#inputTo").val();
    $.ajax({
        type: "POST",
        url: WebMethodGetUsersByCustomCalendar,
        data: '{"dateFrom":' + "'" + dateFrom + "'" + ',"dateTo":' + "'" + dateTo + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#spnTotalCalendar").empty();
            $("#spnTotalCalendar").text(response.d);
        },

    });
};
   
