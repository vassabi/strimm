var webMethodGetProductSubscribtionsStatisticsByDateRange = "/WebServices/OrderWebService.asmx/GetProductSubscribtionsStatisticsByDateRange";
var webMethodGetOrderStatisticsByDateRange = "../../WebServices/OrderWebService.asmx/GetOrderStatisticsByDateRange";
var webMethodGetOrderTransactionPosByOrderId = "../../WebServices/OrderWebService.asmx/GetOrderTransactionPosByOrderId";
var webMethodGetChannelTubePosByOrderId = "../../WebServices/OrderWebService.asmx/GetChannelTubePosByOrderId";
var webMethodGetChannelTubeCountsByCategoryForExistingSubscriptions = "../../WebServices/OrderWebService.asmx/GetChannelTubeCountsByCategoryForExistingSubscriptions"



var toggledBrowseChannelsMenuVisible = false;
var catetoriesWithChannelCounts;
var dateNow = new Date();
var clientTime = dateNow.format("m-d-Y-H-i");

$("#divChannelsCategory").hide();

//$('html').click(function () {

//    if (toggledBrowseChannelsMenuVisible) {
//        ToggleChannels();
//    }

//});

//function GetChannelCategories() {
//    var clientTime = setClientTime();
//    //$("#divChannelsCategory").addClass("loading");
//    $.ajax({
//        type: "POST",
//        url: webMethodGetChannelCategoriesWithCurrentlyPlayingChannelCount,
//        cashe: false,
//        data: '{"clientDateAndTime":"' + clientTime + '"}',
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        success: function (response) {
//            if (response.d) {
//                //$("#divChannelsCategory").removeClass("loading");
//                catetoriesWithChannelCounts = response.d;

//            }
//        }
//    });
//};

function GetTotalSubscribtionsByDateRange(startDate, endDate) {
    var result = $.ajax({
        type: "POST",
        url: webMethodGetProductSubscribtionsStatisticsByDateRange,
        cashe: false,
        dataType: "json",
        data: '{"startDate":' + "'" + startDate + "'" + ',"endDate":' + "'" + endDate + "'" + '}',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            // console.log(response);
            result = response.d;


        },
        complete: function () {

        }
    });
    console.log(result)
    return result.responseJSON.d;
};
function CreateTotalSubscribtionTable() {
    var dataset = [];
    var date = new Date();
    var startDate = date.getDate() + 30;
    var endDate = date.getDate();
    var result = GetTotalSubscribtionsByDateRange(null, null);
    $("#lblTotalPaidSubscribers").text("").text(result.TotalSubscriberCount);
    $("#lblTotalSubsribedChannels").text("").text(result.TotalSubscribedChannelCount);
    $("#TotalPaidSubscribtions").text("").text(result.TotalSubscriptionsCount);



};
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



Array.prototype.contains = function (obj) {
    var i = this.length;
    while (i--) {
        if (this[i] == obj) {
            return true;
        }
    }
    return false;
}
function GetCustomSubscribtions() {
    var startDate, endDate = null;
    startDate = $("#inputFrom").val();
    endDate = $("#inputTo").val();
    var data = GetTotalSubscribtionsByDateRange(startDate, endDate);
    CreateOrderTable(startDate, endDate);
    FillTotalPeriodTable(data);
};
function GetSubscribtionsByCalendar() {

    $("#customOption").hide();
    var selectedOption = parseInt($("#selectTotalByCalendar option:selected").val());
    var startDate, endDate = null;

    switch (selectedOption) {
        case 0:
            startDate, endDate = null;
            ResetTables(); //reset all tables if option == 'select'
            CreateOrderTable(startDate, endDate)
            break;

        case 1: // custom time 
            console.log(selectedOption)
            $("#inputFrom, #inputTo").datepicker({ dateFormat: 'dd/mm/yy', yearRange: '-20:+0', changeYear: true, changeMonth: true });
            $("#ancGo").attr("onclick", "GetCustomSubscribtions()");
            $("#customOption").show();

            break;
        case 2:// today
            startDate, endDate = null;
            startDate = new Date();
            endDate = new Date();
            var sdate = startDate.format('d/m/Y');
            var eDate = endDate.format('d/m/Y')
            var data = GetTotalSubscribtionsByDateRange(sdate, eDate);
            CreateOrderTable(sdate, eDate);
            FillTotalPeriodTable(data);
            break;
        case 3: //yesterday
            startDate, endDate = null;
            //  var date = new Date();
            startDate = new Date(new Date().getTime() - 24 * 60 * 60 * 1000);
            endDate = new Date();
            var sdate = startDate.format('d/m/Y');
            var eDate = endDate.format('d/m/Y');
            var data = GetTotalSubscribtionsByDateRange(sdate, eDate);
            CreateOrderTable(sdate, eDate);
            FillTotalPeriodTable(data);
            break;
        case 4:// this month
            startDate, endDate = null;
            var date = new Date();
            startDate = new Date(date.getFullYear(), date.getMonth(), 1);
            endDate = new Date(date.getFullYear(), date.getMonth() + 1, 0);
            var sdate = startDate.format('d/m/Y');
            var eDate = endDate.format('d/m/Y');
            var data = GetTotalSubscribtionsByDateRange(sdate, eDate);
            CreateOrderTable(sdate, eDate);
            FillTotalPeriodTable(data);
            break;
        case 5:
            startDate, endDate = null;
            var date = new Date();
            startDate = new Date(date.getFullYear(), date.getMonth(), -1);
            endDate = new Date(date.getFullYear(), date.getMonth(), 1, 0);
            var sdate = startDate.format('d/m/Y');
            var eDate = endDate.format('d/m/Y')
            var data = GetTotalSubscribtionsByDateRange(sdate, eDate);
            CreateOrderTable(sdate, eDate);
            FillTotalPeriodTable(data);
            break;
    }

};
function ResetTimePeriod() {
    $("#selectTotalByCalendar option[value='0']").prop('selected', true);
    $("#spnTrial, #spnActive, #spnCanceled, #spnEmbeddedChannelCount, #spnWhiteLabelCount, #spnPassProtectedCount, #spnCustomLabelCount, #spnMutedCount").text("").text("0");

};
function FillTotalPeriodTable(data) {
    ResetTimePeriod();
    var inTrialSubscr = data.InTrialSubscriptionsCount;
    var activeSubscr = data.ActiveSubscriptionsCount;
    var canceledSubscr = data.CanceledSubscriptionsCount;
    var embeddedChannelCount = data.EmbeddedChannelsCount;
    var whiteLabelCount = data.WhiteLabeledChannelsCount;
    var passProtectionCount = data.PasswordProtectedChannelsCount;
    var customLabelCount = data.CustomLabeledChannelsCount;
    var mutedCount = data.MuteOnStartupChannelsCount;
    $("#spnTrial").text(inTrialSubscr);
    $("#spnActive").text(activeSubscr);
    $("#spnCanceled").text(canceledSubscr);
    $("#spnEmbeddedChannelCount").text(embeddedChannelCount);
    $("#spnPassProtectedCount").text(passProtectionCount);
    $("#spnCustomLabelCount").text(customLabelCount);
    $("#spnMutedCount").text(mutedCount);
    $("#spnWhiteLabelCount").text(whiteLabelCount);



    console.log(data);
};
function CreateOrderTable(startDate, endDate) {
    var dataset = [];
    $.ajax({
        type: "POST",
        url: webMethodGetOrderStatisticsByDateRange,
        cashe: false,
        dataType: "json",
        data: '{"startDate":' + "'" + startDate + "'" + ',"endDate":' + "'" + endDate + "'" + '}',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            // console.log(response);
            result = response.d;
            console.log(response.d);

            $.each(response.d, function (i, d) {
                var subscriberName = d.SubscriberName;
                var country = d.Country;
                var email = d.SubscriberEmail;
                var pubicName = d.PublicName;
                var orderNum = d.OrderNumber;
                var orderDate = new Date(parseInt(d.OrderDate.replace('/Date(', '')));
                var status = d.OrderStatus;
                var trialEndTime = new Date(parseInt(d.TrialEndDate.replace('/Date(', '')));
                var packageName = d.ProductName;
                var pricePerMonth = d.PricePerMonth;
                var numRecievedPaiments = d.PaymentsCount;
                var grossAmountPaidToDate = d.GrossPaymentAmount;
                var details;
                var orderId = d.OrderId;
                var obj = Array();


                obj[0] = subscriberName;

                obj[1] = country;//date.toLocaleDateString();
                obj[2] = email;
                obj[3] = pubicName;

                obj[4] = orderNum;
                obj[5] = orderDate.toLocaleDateString();
                obj[6] = status;
                obj[7] = trialEndTime.toLocaleDateString();
                obj[8] = packageName;
                obj[9] = "&#36;" + pricePerMonth;
                obj[10] = "<a class='ancPopup' onclick='GetOrderTransaction(" + orderId + ")'> " + numRecievedPaiments + "</a>";
                obj[11] = "<a class='ancPopup' onclick='GetOrderTransaction(" + orderId + ")'> &#36;" + grossAmountPaidToDate + "</a>";
                obj[12] = "<a class='ancPopup' onclick='GetChannelsByOrderId(" + orderId + ")'>details</a>";
                loads = loads + d.LoadCount;
                dataset.push(obj);


            });
            $("#lblTotalChannels").text(channelNamesArr.length);
            $("#lblTotalViewers").text(loads);
        },
        complete: function () {



            $('#subscribtionStatistics').html("").html('<table cellpadding="0" cellspacing="0" border="0" class="display" id="example"></table>');
            $('#example').dataTable({
                "dom": 'T<"clear">lfrtip',

                "data": dataset,
                "columns": [
                    { "title": "Subscriber Name", "class": "left" },
                    { "title": "Country ", "class": "left" },
                    { "title": "Subscriber Email", "class": "left" },
                    { "title": "Public Name", "class": "left" },
                    { "title": "Order No.", "class": "left" },
                    { "title": "Order Date", "class": "left" },
                    { "title": "Status", "class": "left" },
                    { "title": "Trial End Date", "class": "left" },
                     { "title": "Package Name", "class": "left" },

                      { "title": "Price/Month", "class": "left" },
                       { "title": "# of Payments Recieved", "class": "left" },
                        { "title": "Gross Amount Paid to date", "class": "left" },
                         { "title": "Details", "class": "left" }




                ]
            });
        }
    });
};
function GetOrderTransaction(orderid) {
    $('#transactionDetailsPopup').lightbox_me({
        centered: true,
        onLoad: function () {
            console.log("ONLOAD")
            var dataset = [];
            $.ajax({
                type: "POST",
                url: webMethodGetOrderTransactionPosByOrderId,
                data: '{"orderId":' + orderid + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {


                    $.each(response.d, function (i, d) {
                        var orderTransactionType = d.OrderTransactionType;
                        var orderTransactionStatus = d.OrderTransactionStatus;
                        var transactionPaymentMethod = d.TransactionPaymentMethod;
                        var orderTransactionId = d.OrderTransactionId;
                        var orderId = d.OrderId;
                        var orderTransactionTypeId = d.OrderTransactionTypeId;
                        var orderTransactionStatusId = d.OrderTransactionStatusId;
                        var transactionPaymentMethodId = d.TransactionPaymentMethodId;
                        var tpnMessageId = d.IpnMessageId;
                        var txnId = d.TxnId;
                        var paymentStatus = d.PaymentStatus;
                        var paymentDate = d.PaymentDate
                        var paymentNumber = d.PaymentNumber;
                        var paymentGross = d.PaymentGross;
                        var mcCurrency = d.McCurrency;
                        var paymentType = d.PaymentType;
                        var paymentFee = d.PaymentFee;
                        var payerStatus = d.PayerStatus;
                        var mcFee = d.McFee;
                        var mcGross = d.McGross;
                        var protectionEligibility = d.ProtectionEligibility;
                        var transactionSubject = d.TransactionSubject;
                        var obj = Array();


                        obj[0] = txnId;

                        obj[1] = orderTransactionType;
                        obj[2] = status;
                        obj[3] = transactionPaymentMethod;

                        obj[4] = paymentStatus;
                        obj[5] = paymentNumber
                        obj[6] = mcCurrency
                        obj[7] = paymentGross
                        obj[8] = mcFee;
                        obj[9] = mcGross;
                        obj[10] = paymentDate;

                        dataset.push(obj);


                    });

                },
                complete: function () {



                    $('#transactionDetails').html("").html('<table cellpadding="0" cellspacing="0" border="0" class="display" id="transactionTable"></table>');
                    $('#transactionTable').dataTable({
                        "dom": 'T<"clear">lfrtip',

                        "data": dataset,
                        "columns": [
                            { "title": "Txn Id", "class": "left" },
                            { "title": "Transaction Type ", "class": "left" },
                            { "title": "Status", "class": "left" },
                            { "title": "Payment Method", "class": "left" },
                            { "title": "Payment Status", "class": "left" },
                            { "title": "Payment Number", "class": "left" },
                            { "title": "Currency", "class": "left" },
                            { "title": "Gross Amount", "class": "left" },
                            { "title": "Fee", "class": "left" },
                            { "title": "Gross", "class": "left" },
                            { "title": "Payment Date", "class": "left" }
                        ]
                    });
                }
            });
        },
        onClose: function () {

        }
    });
};
function GetChannelsByOrderId(orderId) {
    $('#channelDetailsPopup').lightbox_me({
        centered: true,
        onLoad: function () {
            var dataset = [];
            $.ajax({
                type: "POST",
                url: webMethodGetChannelTubePosByOrderId,
                data: '{"orderId":' + orderId + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $.each(response.d, function (i, d) {
                        var channelName = d.Name;
                        var publicName = d.ChannelOwnerUserName;
                        var category = d.CategoryName;
                        var hostUrl = d.UserDomain;
                        var isWhitelabel;
                        if (d.IsWhiteLabeled) {
                            isWhitelabel = "Yes";
                        }
                        else {
                            isWhitelabel = "No";
                        }
                        var passwordProtected;
                        if (d.IsPrivate) {
                            passwordProtected = "Yes";
                        }
                        else {
                            passwordProtected = "No";
                        }

                        var customLabled;
                        if (d.CustomLabel) {
                            customLabled = "Yes";
                        }
                        else {
                            customLabled = "No";
                        }
                        var muted;
                        if (d.MuteOnStartup) {
                            muted = "Yes";
                        }
                        else {
                            muted = "No";
                        }

                        var obj = Array();

                        obj[0] = channelName;
                        obj[1] = publicName;
                        obj[2] = category;
                        obj[3] = hostUrl;
                        obj[4] = isWhitelabel;
                        obj[5] = passwordProtected;
                        obj[6] = customLabled;
                        obj[7] = muted;

                        dataset.push(obj);


                    });
                },
                complete: function () {



                    $('#channelDetails').html("").html('<table cellpadding="0" cellspacing="0" border="0" class="display" id="channelDetailsTable"></table>');
                    $('#channelDetailsTable').dataTable({
                        "dom": 'T<"clear">lfrtip',

                        "data": dataset,
                        "columns": [
                            { "title": "Channel Name", "class": "left" },
                            { "title": "Public Name", "class": "left" },
                            { "title": "Category", "class": "left" },
                            { "title": "Host URL", "class": "left" },
                            { "title": "Is White-Labeled?", "class": "left" },
                            { "title": "Password Protected?", "class": "left" },
                            { "title": "Custom labeled?", "class": "left" },
                            { "title": "Muted?", "class": "left" },

                        ]
                    });
                }
            });
        },
        onClose: function () {

        }
    });
};
function ClosePopup() {
    $("#transactionDetailsPopup, #channelDetails").trigger('close');
};

function GetChannelTubeCountsByCategoryForExistingSubscriptions() {
    $.ajax({
        type: "POST",
        url: webMethodGetChannelTubeCountsByCategoryForExistingSubscriptions,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response.d);
            var categories = response.d;

            var holder = $("#divChannelsCategory ul");
            var html = "";
            var currentColumn = 0;
            var currentRow = 0;
            var maxColumnCount = 6;
            var maxRowCount = 2;

            $.each(response.d, function (i, c) {
                console.log(c);
                var chCountElement = $("#ch" + c.CategoryId);
                if (chCountElement) {
                    var count = c.ChannelCount == 0 ? '' : c.ChannelCount;
                    chCountElement.text(count);
                }
            });
        }
    });

};
function ToggleChannels() {


    if ($("#divChannelsCategory").is(":visible")) {
        $("#divChannelsCategory").hide();
        $("#divBrowseChannels a #divImg").text(" ").text("▼");
        toggledBrowseChannelsMenuVisible = false;
    }
    else {
        //GetChannelTubeCountsByCategoryForExistingSubscriptions();
        $("#divChannelsCategory").show();
        $("#divBrowseChannels a #divImg").text(" ").text("▲");
        toggledBrowseChannelsMenuVisible = true;
    }
};
//function GetChannelCategoriesWithCounts() {
//    if (catetoriesWithChannelCounts && catetoriesWithChannelCounts != null) {
//        var categories = catetoriesWithChannelCounts;

//        var holder = $("#divChannelsCategory ul");
//        var html = "";
//        var currentColumn = 0;
//        var currentRow = 0;
//        var maxColumnCount = 6;
//        var maxRowCount = 2;

//        $.each(categories, function (i, c) {
//            var chCountElement = $("#ch" + c.CategoryId);
//            if (chCountElement) {
//                var count = c.ChannelCount == 0 ? '' : c.ChannelCount;
//                chCountElement.text(count);
//            }
//        });

//        GetChannelCategoriesWithCountsWithoutUIUpdate();
//    }
//};

