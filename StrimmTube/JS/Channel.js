
var webMethodSubscribe = "WebServices/ChannelWebService.asmx/Subscribe";
var webMethodUnSubscribe = "WebServices/ChannelWebService.asmx/UnSubscribe";
var webMethodSendReport = "WebServices/ChannelWebService.asmx/SendReport";
var webMethodRingToFollowers = "WebServices/ChannelWebService.asmx/RingToFollowers";
function blink(selector) {
    for (var i = 0; i < 3; i++) {
        $(selector).fadeOut('fast', function () {
            $(this).css("background-color", "#2a99bd");
        });
        $(selector).fadeIn('fast', function () {
            $(this).css("background-color", "#ccc");
        });
    }     
};

function ShowHideSchedule()
{
    if ($(".divRings").is(':visible')) {
        $(".divRings").hide();
    }
    var divSchedule = $(".divSchedule");
    if ($(".divSchedule").is(':visible')) {
        $(".divSchedule").hide();
        ScrollerUp();
    }
    else {
        $(".divSchedule").show();
        ScrollerUp();
    }
}

function ShowHideRings() {
    if ($(".divSchedule").is(':visible')) {
        $(".divSchedule").hide();        
       
    }
    if($(".divRings").is(':visible'))
    {
        $(".divRings").hide();
    }
    else {
        $(".divRings").show();
        ScrollerUp();
    }
}
function ScrollerUp() {
    $(".nano").nanoScroller({ alwaysVisible: true });
}
function showDescripttion(id) {
    if ($("#span_" + id).is(":visible")) {
        $("#videoScheduleBox_" + id + " .ShowDescription").text(" ").text("▼");
        $("#span_" + id).hide();
        $("#videoScheduleBox_" + id).css("height", "50")
    }
    else {
        $("#videoScheduleBox_" + id + " .ShowDescription").text(" ").text("▲");
        $("#videoScheduleBox_" + id).css("height","100")
    var span = $("#span_" + id).show();
   // //console.log(span.text());
   }
}
function ShowNextSchedule() {
   
    var today = new Date();
    
    var tomorow;
    if ($(".divScheduleTomorrow").is(":visible")) {
        tomorow = null;
        $(".todaySchedule").show();
        $("#nextSchedule").text(" ").text("next day");
        $(".scheduleDate").text(today.toDateString("dd/MM/yyyy"));
        $(".divScheduleTomorrow").hide();
        ScrollerUp();
    }
    else {
        
        tomorow = AddDay(today, 1);
       
        //console.log(tomorow);
        $(".todaySchedule").hide();
        $("#nextSchedule").text(" ").text("back");
        $(".scheduleDate").text(tomorow.toDateString());
        $(".divScheduleTomorrow").show();
        ScrollerUp();
    }
}
function AddDay(myDate, days) {
    return new Date(myDate.getTime() + days * 24 * 60 * 60 * 1000);

}
function AddToArchive() {
    $.ajax({
        type: "POST",
        url: webMethodAddToArchive,
        data: '{"videoId":' + currVideo + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            void 0;
        }
    });
}
function SubscribeChannel() {
    
    $.ajax({
        type: "POST",
        url: webMethodSubscribe,
        data: '{"channelId":' + channelId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#divSubscribeButton a").text(" ").text("Unsubscribe").removeAttr("onclick").attr("onclick", "Unsubscribe()");
        }
    });
}
function Unsubscribe() {
    $.ajax({
        type: "POST",
        url: webMethodUnSubscribe,
        data: '{"channelId":' + channelId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#divSubscribeButton a").text(" ").text("subscribe").removeAttr("onclick").attr("onclick", "SubscribeChannel()");
        }
    });
}
function ShowAbuseModal() {
    //$('#abuseModal').lightbox_me({
    //    centered: true,
    //    onLoad: function () {

    //    }
    //});
    $('#abuseModal').slideDown();
    window.location.hash = "abuseDown";
}
function closeAbuse()
{
    $('#abuseModal').slideUp();
    $('#txtVideoTitle, #txtComments').val("");
    $("#lblMsg").text("");
}
function SendReport() {
    var selectedOption = $('#ddlCategory option:selected').text();
    var videoTitle = $("#txtVideoTitle").val();
    var comments = $("#txtComments").val();
    $('#abuseModal').ajaxStart(function () {
        $("#lblMsg").text("please wait");
    })
    $.ajax({
        type: "POST",
        url: webMethodSendReport,
        data: '{"channelId":' + channelId + ',"selectedOption":' + "'" + selectedOption + "'" + ',"videoTitle":' + "'" + videoTitle + "'" + ',"comments":' + "'" + comments + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#lblMsg").text(response.d);
            setTimeout(function () { closeAbuse() }, 5000);
        },
        
    });
}
function RingToFollowers() {
    $.ajax({
        type: "POST",
        url: webMethodRingToFollowers,
        data: '{"channelId":' + channelId + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            void 0;
        },

    });
}
function ShowHideChannelDecription() {
    
    if ($(".spnChannelDescription").is(":visible")) {
        $(".spnChannelDescription").css("display", "none");
        $("#showDescription").text(" ").text("show description ▼");
        
    }
    else {
        $(".spnChannelDescription").css("display", "block");
        $("#showDescription").text(" ").text("hide description ▲");
       
    }
}