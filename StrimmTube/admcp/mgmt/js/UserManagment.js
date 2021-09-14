var WebMethodGetUserInfo = "WebServiceAdmin/UserManagmentService.asmx/GetUserInfo";
var WebMethodGetUserChannels = "WebServiceAdmin/UserManagmentService.asmx/GetUserChannels";
var WebMethodClearSchedules = "WebServiceAdmin/UserManagmentService.asmx/ClearSchedules";
var WebMethodDeleteChannels = "WebServiceAdmin/UserManagmentService.asmx/DeleteChannels";
var WebMethodLockUnlockUser = "WebServiceAdmin/UserManagmentService.asmx/LockUnclockUser";
var WebMethodEnableProForUser = "WebServiceAdmin/UserManagmentService.asmx/EnableProForUser";
var WebMethodAddSubscriptionUser = "WebServiceAdmin/UserManagmentService.asmx/AddSubscriptionToUser";
var WebMethodSaveNotes = "WebServiceAdmin/UserManagmentService.asmx/SaveNotes";
var WebMethodDeleteAccount = "WebServiceAdmin/UserManagmentService.asmx/DeleteAccount";
var webMethodGetAdminActivity = "WebServiceAdmin/AdminWebService.asmx/GetAdminUserNotesForUserByUserId";
var WebMethodSaveUpdateNotes = "WebServiceAdmin/AdminWebService.asmx/SaveAdminActivity";
var userid;
var WebMethodSendWelcomeEmail = "WebServiceAdmin/UserManagmentService.asmx/SendWelcomeEmailToUser";
var WebMethodGetSubscriberDomains = "WebServiceAdmin/UserManagmentService.asmx/GetAuthorizedDomainsForUser";
var WebMethodAuthorizeNewDomainForUser = "WebServiceAdmin/UserManagmentService.asmx/AuthorizedNewDomainForUser";
var WebMethodRemoveDomainAuthorizationForUser = "WebServiceAdmin/UserManagmentService.asmx/RemoveDomainAuthorizationForUser";
var webMethodGetUserProductSubscriptionsByUserId = "../../WebServices/UserService.asmx/GetUserProductSubscriptions";
var webMethodGetGetOrderPosByUserIdForAdmin = "../../WebServices/OrderWebService.asmx/GetOrderPosByUserIdForAdmin";//GetOrderPosByUserIdForAdmin
var WebMethodUpdateOrderById = "../../WebServices/OrderWebService.asmx/UpdateOrderById";
var WebMethodsCreateTrialSubscripionRequest = "../../WebServices/OrderWebService.asmx/CreateTrialSubscripionRequest";
var WebMethodsCreateNManualSubscribtion = "../../WebServices/UserService.asmx/CreateNManualSubscribtion";
var WebMethodCreateSubscripionRequest = "../../WebServices/OrderWebService.asmx/CreateSubscripionRequest";
var WebMethodGetProducts = "../../WebServices/OrderWebService.asmx/GetProducts";
var subscribtionPlan;
$(document).ready(function () {
   
    $('#DivSearchUser option[value="' + 0 + '"]').prop('selected', true);
    $("#txtSearch").val("");
    $("#channelBoxHolder").html("");
    if (isRedirected == 'True') {
        $.ajax({
            type: "POST",
            url: WebMethodGetUserInfo,
            async:false,
            data: '{"inputText":' + "'" + email + "'" + ',"selectedOption":' + 3 + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var json = response.d;
                var data = JSON.parse(json);

                if (response.d != 0) {
                    userid = data[0].userId;
                    $("#spnName").text(data[0].name);
                    $("#spnGender").text(data[0].gender);
                    $("#spnAge").text(data[0].age);
                    $("#spnUserName").text(data[0].userName);
                    $("#spnAccountNum").text(data[0].accountNum);
                    $("#spnEmail").text(data[0].email);
                    $("#spnDateofSignUp").text(data[0].dateOfSignUp);
                    $("#spnIsProEnabled").text(data[0].isProEnabled);
                    $("#spnIsSubscriber").text(data[0].isSubscriber);
                    $("#spnSubscriberDomainCount").text(data[0].SubscriberDomainCount);
                    $("#spnCompany").text(data[0].company);
                    $("#spnCountry").text(data[0].country);
                    $("#spnState").text(data[0].state);
                    $("#spnAddress").text(data[0].address);
                    $("#spnCity").text(data[0].city);
                    $("#spnZipCode").text(data[0].zipCode);
                    $("#spnChannels").text(data[0].channels);
                    $("#spnBoard").text(data[0].board);
                    $("#spnAccountStatus").text(data[0].accountStatus);


                    $("#spnStatus").text("").text(data[0].accountStatus);
                    if (data[0].islocked == false) {

                        $("#ancLockUnlockUser").text("").text("lock access to account").removeAttr("onclick").attr("onclick", "LockUser()");
                    }
                    else {
                        $("#spnStatus").text("").text("locked access");
                        $("#ancLockUnlockUser").text("").text("unlock access to account").removeAttr("onclick").attr("onclick", "UnLockUser()");
                    }

                    if (data[0].accountStatus == "deleted") {
                        $("#channelActionsDiv >* ").attr("disabled", "disabled").removeAttr("onclick").addClass("disabled")
                    }

                  

                    // get channels
                    var html = "";
                    $.ajax({
                        type: "POST",
                        url: WebMethodGetUserChannels,
                        data: '{"userId":' + data[0].userId + '}',
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (response) {
                           

                            $.each(response.d, function (i, d) {
                                if (d.IsDeleted == false) {
                                    html += '<div id="channelContentHolder_' + d.ChannelTubeId + '>" class="contentHolder">';
                                    html += '<a href="' + d.Url + '" class="channelNameAnc">' + d.Name + '</a>';
                                    html += '<input type="radio" id="checkBox_' + d.ChannelTubeId + '" class="chkChannel" name="checkDelete" value="' + d.ChannelTubeId + '" /></div>';
                                }
                                else {
                                    html += '<div id="channelContentHolder_' + d.ChannelTubeId + '>" class="contentHolder">';
                                    html += '<a " class="channelNameAnc">' + d.Name + '</a>';
                                    html += '<span>deleted channel</span></div>';
                                }

});
                            $("#channelBoxHolder").html(html);
}
                    });
                    $("#divShowUser").show();
                }

                else {
                    alert("This user does not exist in the system");
                }

            },
            complete: function()
{
                GetNotesForUser();
            }

        });
        subscribtionPlan = $.ajax({
            //userId=33 - no subscr
            //userId = 29 subscr
            //change after that to userId
            type: "POST",
            url: webMethodGetUserProductSubscriptionsByUserId,
            cashe: false,
            async: false,
            data: '{"userId":' + userid + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {



            },
            error: function (request, status, error) {

            }
        });
        console.log(subscribtionPlan.responseJSON.d);

    }
    $.ajax({
        type: "POST",
        url: WebMethodGetProducts,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async:false,
        success: function (response) {
            
            var option = "";
            $.each(response.d, function (i, d) {
                option += '<option value="' + d.ProductId + '">' + d.Name + '</option>';

            });
            $('#subscribtionSelect').append(option);
        }
    });
  
});

function Reset() {
    location.reload();
};
function getQueryStringParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
};
function AuthorizeNewDomainForUser() {
    var input = $("#newSubscriberDomain");
    var newDomain = input.val();
    if (newDomain && newDomain != '' && userid && userid) {
        $.ajax({
            type: "POST",
            url: WebMethodAuthorizeNewDomainForUser,
            data: '{"userId":' + userid + ', "domain":' + "'" + newDomain + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var data = response.d;
                var container = $("#domainHolder");

                if (data && data.length > 0) {
                    var subscriberList = '<ul id="authorizedDomains" >';
                    $.each(data, function (i, d) {
                        subscriberList += '<li>'
                        subscriberList += '<div id="authDomain_' + d.SubscriberDomainId + '">';
                        subscriberList += '<label>' + d.UserDomain + '</label>';
                        subscriberList += '<a onclick="RemoveDomainAuthorizationForUser(' + d.SubscriberDomainId + ')" style="color:red;">x</a>';
                        subscriberList += '</div>';
                        subscriberList += '</li>';
                    });
                    subscriberList += '</ul>';
                    container.html('').html(subscriberList);
                    input.val('');
}
                else {
                    container.html('').html('User does not have any authorized domains');
                }
            }
        });
    }
}

function RemoveDomainAuthorizationForUser(domainId) {
    if (domainId && domainId > 0 && userid && userid > 0) {
        $.ajax({
            type: "POST",
            url: WebMethodRemoveDomainAuthorizationForUser,
            data: '{"domainId":' + domainId + ', "userId":' + userid + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var data = response.d;
                var container = $("#domainHolder");

                if (data && data.length > 0) {
                    var subscriberList = '<ul id="authorizedDomains" >';
                    $.each(data, function (i, d) {
                        subscriberList += '<li>'
                        subscriberList += '<div id="authDomain_' + d.SubscriberDomainId + '">';
                        subscriberList += '<label>' + d.UserDomain + '</label>';
                        subscriberList += '<a onclick="RemoveDomainAuthorizationForUser(' + d.SubscriberDomainId + ')" style="color:red;">x</a>';
                        subscriberList += '</div>';
                        subscriberList += '</li>';
                    });
                    subscriberList += '</ul>';
                    container.html('').html(subscriberList);
                }
                else {
                    container.html('').html('User does not have any authorized domains');
                }
            }
        });

    }
}

function GetAuthorizedDomainsForUser(userId) {
    if (userId && userId > 0) {
        $.ajax({
            type: "POST",
            url: WebMethodGetSubscriberDomains,
            data: '{"userId":' + userId + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var data = response.d;
                var container = $("#domainHolder");

                if (data && data.length > 0) {
                    var subscriberList = '<ul id="authorizedDomains" >';
                    $.each(data, function(i, d) {
                        subscriberList += '<li>'
                        subscriberList += '<div id="authDomain_' + d.SubscriberDomainId + '">';
                        subscriberList += '<label>' + d.UserDomain + '</label>';
                        subscriberList += '<a onclick="RemoveDomainAuthorizationForUser(' + d.SubscriberDomainId + ')" style="color:red;">x</a>';
                        subscriberList += '</div>';
                        subscriberList += '</li>';
                    });
                    subscriberList += '</ul>';
                    container.html('').html(subscriberList);
                }
                else {
                    container.html('').html('User does not have any authorized domains');
                }
            }
        });
    }
};
var order;
function GetOrderPosByUserIdForAdmin(userId) {

    order = $.ajax({
        type: "POST",
        url: webMethodGetGetOrderPosByUserIdForAdmin,
        data: '{"userId":' + userId + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
        
        },
        complete: function () {

        },

        onClose: function () {

        }
    });
};
function GetUserinfo() {

    var selectedOption = $("#DivSearchUser select option:selected").val();
    var inputText = $("#txtSearch").val();
    if (selectedOption == 0 || inputText == "") {
        alert("please check you selected option and filled input (Dima please send me the text for this alert)");
        
    }
    else {
        $.ajax({
            type: "POST",
            url: WebMethodGetUserInfo,
            data: '{"inputText":' + "'" + inputText + "'" + ',"selectedOption":' + selectedOption + '}',
            dataType: "json",
            async:false,
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                var json = response.d;                
                var data = JSON.parse(json);
               
                if (response.d != 0) {
                    userid = data[0].userId;
                    $("#spnName").text(data[0].name);
                    $("#spnGender").text(data[0].gender);
                    $("#spnAge").text(data[0].age);
                    $("#spnUserName").text(data[0].userName);
                    $("#spnAccountNum").text(data[0].accountNum);
                    $("#spnEmail").text(data[0].email);
                    $("#spnDateofSignUp").text(data[0].dateOfSignUp);
                    $("#spnIsProEnabled").text(data[0].isProEnabled);
                    $("#spnIsSubscriber").text(data[0].isSubscriber);
                    $("#spnCompany").text(data[0].company);
                    $("#spnCountry").text(data[0].country);
                    $("#spnState").text(data[0].state);
                    $("#spnAddress").text(data[0].address);
                    $("#spnCity").text(data[0].city);
                    $("#spnZipCode").text(data[0].zipCode);
                    $("#spnChannels").text(data[0].channels);
                    $("#spnBoard").text(data[0].board);
                    $("#spnAccountStatus").text(data[0].accountStatus);
                    $("#spnSubscriberDomainCount").text(data[0].SubscriberDomainCount);

                    $("#spnStatus").text("").text(data[0].accountStatus);
                    if (data[0].islocked == false) {

                        $("#ancLockUnlockUser").text("").text("lock access to account").removeAttr("onclick").attr("onclick", "LockUser()");
                    }
                    else {
                        $("#spnStatus").text("").text("locked access");
                        $("#ancLockUnlockUser").text("").text("unlock access to account").removeAttr("onclick").attr("onclick", "UnLockUser()");
                    }
                    
                    if (data[0].accountStatus == "deleted") {
                        $("#channelActionsDiv >* ").attr("disabled", "disabled").removeAttr("onclick").addClass("disabled")
                    }

                    if (data[0].isProEnabled == true) {
                        $("#ancEnabledDisablePro").text("").text("Disable Pro").removeAttr("onclick").attr("onclick", "EnablePro(false)");
                    }
                    else {
                        $("#ancEnabledDisablePro").text("").text("Enable Pro").removeAttr("onclick").attr("onclick", "EnablePro(true)");
                    }

                    if (data[0].isSubscriber == true) {
                        $("#ancAddRemoveSubscription").text("").text("Remove Subscription").removeAttr("onclick").attr("onclick", "EnableSubscription(false)");
                    }
                    else {
                        $("#ancAddRemoveSubscription").text("").text("Add Subscription").removeAttr("onclick").attr("onclick", "EnableSubscription(true)");
                    }

                    if (data[0].isSubscriber == true) {
                        $("#divSubscriberDomains").show();
                        GetAuthorizedDomainsForUser(userid);
                    }
                    else {
                        $("#divSubscriberDomains").hide();
                    }

                    // get channels
                    var html = "";
                    $.ajax({
                        type: "POST",
                        url: WebMethodGetUserChannels,
                        data: '{"userId":' + data[0].userId + '}',
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (response) {
                          

                            $.each(response.d, function (i, d) {
                                if (d.IsDeleted == false) {
                                    html += '<div id="channelContentHolder_' + d.ChannelTubeId + '>" class="contentHolder">';
                                    html += '<a href="' + d.Url + '" class="channelNameAnc">' + d.Name + '</a>';
                                    html += '<input type="radio" id="checkBox_' + d.ChannelTubeId + '" class="chkChannel" name="checkDelete" value="' + d.ChannelTubeId + '" /></div>';
                                }
                                else {
                                    html += '<div id="channelContentHolder_' + d.ChannelTubeId + '>" class="contentHolder">';
                                    html += '<a " class="channelNameAnc">' + d.Name + '</a>';
                                    html += '<span>deleted channel</span></div>';
                                }

                            });
                            $("#channelBoxHolder").html(html);
                        },
                        complete: function () {
                            GetNotesForUser();
                        }
                    });

                    $("#divShowUser").show();
                }                
                else {
                    alert("This user does not exist in the system");
                }
            },

        });
    }
    subscribtionPlan = $.ajax({
        //userId=33 - no subscr
        //userId = 29 subscr
        //change after that to userId
        type: "POST",
        url: webMethodGetUserProductSubscriptionsByUserId,
        cashe: false,
        async: false,
        data: '{"userId":' + userid + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

         
            //console.log(order);

        },
        error: function (request, status, error) {

        }
    });
    GetOrderPosByUserIdForAdmin(userid);
    if (order!=null)
    {
        var cdata = order.responseJSON.d;
        var dataset = [];

        $.each(cdata, function (i, d) {
           
            var obj = Array();
          

            //1	New	Newly created order
            //2	InTrial	Order is in trial period
            //3	Active	Order is active.Receiving payments
            //4	Pending	Order is pending payment resolution
            //5	Canceled	Order was canceled by user before being filled
            //6	Expired	Order was not renewed and has expired
         
            var selectedStatus = "";
            if (d.OrderStatusId == 1)
            {
               
               
                selectedStatus += '<div class="inputHolder">';
                selectedStatus += '<select id="selectedStatus_'+ d.OrderId + '">';
                selectedStatus += '<option selected="selected" value="1">New</option>';
                selectedStatus += '<option value="2">In Trial</option>';
                selectedStatus += '<option value="3">Active</option>';
                selectedStatus += '<option value="4">Pending</option>';
                selectedStatus += '<option value="5">Canceled</option>';
                selectedStatus += '<option  value="6">Expired</option>';
                selectedStatus += '</select >';//<span class="basicPackage" id="txtOrderStatus">' + d.Status + '</span></div > '
                obj[0] = '<div id="toggleNewBasic" class="inputBasicOFF" ></div><span>'+d.ProductName+'</span>';
            }
            if (d.OrderStatusId == 2) 
            {
               
                selectedStatus += '<div class="inputHolder">';
                selectedStatus += '<select id="selectedStatus_' + d.OrderId + '">';
                selectedStatus += '<option  value="1">New</option>';
                selectedStatus += '<option selected="selected"  value="2">In Trial</option>';
                selectedStatus += '<option value="3">Active</option>';
                selectedStatus += '<option value="4">Pending</option>';
                selectedStatus += '<option value="5">Canceled</option>';
                selectedStatus += '<option  value="6">Expired</option>';
                selectedStatus += '</select >';
               obj[0] = '<div id="toggleNewBasic" class="inputBasicON"></div><span>' + d.ProductName + '</span>';
           }
            if (d.OrderStatusId ==3)
            {
                selectedStatus += '<div class="inputHolder">';
                selectedStatus += '<select id="selectedStatus_' + d.OrderId + '">';
                selectedStatus += '<option  value="1">New</option>';
                selectedStatus += '<option  value="2">In Trial</option>';
                selectedStatus += '<option selected="selected" value="3">Active</option>';
                selectedStatus += '<option value="4">Pending</option>';
                selectedStatus += '<option value="5">Canceled</option>';
                selectedStatus += '<option  value="6">Expired</option>';
                selectedStatus += '</select >';
               obj[0] = '<div id="toggleNewBasic" class="inputBasicON" ></div><span>' + d.ProductName + '</span>';
           }
            if (d.OrderStatusId == 4)
            {
                
                selectedStatus += '<div class="inputHolder">';
                selectedStatus += '<select id="selectedStatus_' + d.OrderId + '">';
                selectedStatus += '<option  value="1">New</option>';
                selectedStatus += '<option  value="2">In Trial</option>';
                selectedStatus += '<option  value="3">Active</option>';
                selectedStatus += '<option selected="selected" value="4">Pending</option>';
                selectedStatus += '<option value="5">Canceled</option>';
                selectedStatus += '<option  value="6">Expired</option>';
                selectedStatus += '</select >';
               obj[0] = '<div id="toggleNewBasic" class="inputBasicOFF" ></div><span>' + d.ProductName + '</span>';
           }
            if (d.OrderStatusId == 5)
            {
                
                selectedStatus += '<div class="inputHolder">';
                selectedStatus += '<select id="selectedStatus_' + d.OrderId + '">';
                selectedStatus += '<option  value="1">New</option>';
                selectedStatus += '<option  value="2">In Trial</option>';
                selectedStatus += '<option  value="3">Active</option>';
                selectedStatus += '<option  value="4">Pending</option>';
                selectedStatus += '<option selected="selected" value="5">Canceled</option>';
                selectedStatus += '<option  value="6">Expired</option>';
                selectedStatus += '</select >';
               obj[0] = '<div id="toggleNewBasic" class="inputBasicOFF" ></div><span>' + d.ProductName + '</span>';
            }
            if (d.OrderStatusId == 6) {

                selectedStatus += '<div class="inputHolder">';
                selectedStatus += '<select id="selectedStatus_' + d.OrderId + '">';
                selectedStatus += '<option  value="1">New</option>';
                selectedStatus += '<option  value="2">In Trial</option>';
                selectedStatus += '<option  value="3">Active</option>';
                selectedStatus += '<option  value="4">Pending</option>';
                selectedStatus += '<option value="5">Canceled</option>';
                selectedStatus += '<option selected="selected" value="6">Expired</option>';
                selectedStatus += '</select >';
                obj[0] = '<div id="toggleNewBasic" class="inputBasicOFF"></div><span>' + d.ProductName + '</span>';
            }
            obj[1] = '<div class="inputHolder"><input class="basicPackage" id="txtChannelAmmount_' + d.OrderId +'" value= "' + d.ChannelCount + '" /></div > ';
            var creationDate = new Date(parseInt(d.CreatedDateTime.replace('/Date(', '')));
            obj[2] = '<div class="inputHolder"><span class="basicPackage" id="txtCreationDate">' + creationDate.toLocaleDateString() + '</span></div>';
           obj[3] = '<div class="inputHolder"><span class="basicPackage" id="txtAmmountPaid">' + d.Price + '</span></div>';              
          
           if (d.IsAnnual==false)
           {

               obj[4] = '<div class="inputHolder"><input type="radio" name="period_' + d.OrderId + '" checked="checked" class="basicPackage" id="radioMonth" /><label for="radioMonth">M</label><input type="radio" class="basicPackage" id="radioioYear"  name="period_' + d.OrderId +'"  /><label for="radioYear">Y</label></div>';
           }
           else
           {
               obj[4] = '<div class="inputHolder"><input type="radio" name="period_' + d.OrderId + '" class="basicPackage" id="radioMonth" /><label for="radioMonth">M</label><input type="radio" checked="checked" class="basicPackage" id="radioioYear"  name="period_' + d.OrderId +'"  /><label for="radioYear">Y</label></div>';
           }
           if (d.OrderExpirationDate == null)
           {
               obj[5] = '<div class="inputHolder"><input type="text"  name=date1 class="basicPackage" id="txtExpireDate_'+ d.OrderId+'"  value="none" /></div > ';
           }
           else
           {
              
               obj[5] = '<div class="inputHolder"><input type="text"  name=date1 class="basicPackage" id="txtExpireDate_' + d.OrderId + '"value="' + d.OrderExpirationDate +'" /> </div>'; 
           }

           obj[6] = '<div class="inputHolder"><span class="basicPackage" id="txtTransactionId">' + d.TransactionNumber + ' </span></div>'; 
           obj[7] = '<div class="inputHolder"><span class="basicPackage" id="txtOrderNumber">' + d.OrderNumber + ' </span></div>'; 
          
           obj[8] = selectedStatus;
           obj[9] = '<div class="createUpdateHolder"><a id="btnUpdate" class="export" onclick="UpdateSubscribtion(' + d.OrderId + ')">Update</a></div>';
          
            dataset.push(obj);
           
        });

        $('#userSubscribtionsHolder').html("").html('<table cellpadding="0" cellspacing="0" border="0" class="display" id="subscribtionTable"></table>');
        $('#subscribtionTable').dataTable({
            "dom": 'T<"clear">lfrtip',

            "data": dataset,
            "columns": [
                { "title": " subscribtion type", "class": "left" },
                { "title": "channels limit ", "class": "left" },
                { "title": "created date", "class": "left" },
                { "title": "ammount paid", "class": "left" },
                { "title": "period", "class": "left" },
                { "title": "expiration date", "class": "left" },
                { "title": "transaction id", "class": "left" },
                { "title": "order number", "class": "left" },
                { "title": "subscribtion status", "class": "left" },
            { "title": "update order", "class": "left" }
               



            ]
        });
        $('#subscribtionTable').find('input[name=date1]').datepicker({ dateFormat: 'yy-mm-dd', yearRange: '-20:+0', changeYear: true, changeMonth: true });
    }
   // console.log(order);
        
}
function CreateTrialSubscribtion() {
    var productId = 4;
    var userId = 4118
    var price = 99.99;
    var params = '{"productId":' + productId + ',"userId":' + userId + ',"price":' + price + '}';
    
    $.ajax({
        type: "POST",
        url: WebMethodsCreateTrialSubscripionRequest,
        data: params,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            console.log(response.d);
            if (response.d) {
               
                $.ajax({
                    type: "POST",
                    url: WebMethodsCreateNManualSubscribtion,
                    data: '{"payload":' + JSON.stringify(response.d) + '}',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        console.log(response)
                    }
                });
            }
        }
    });
    
}

function CreateSubscribtion() {
   
    var productId = $("#subscribtionSelect option:selected").val()
   
    var price = $("#createNewSubscibtion #ammount").val();
    var params = '{"productId":' + productId + ',"userId":' + userid + ',"price":' + price + '}';
    var param2 =
        $.ajax({
            type: "POST",
            url: WebMethodCreateSubscripionRequest,
            data: params,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                console.log(response.d);
                if (response.d) {

                    $.ajax({
                        type: "POST",
                        url: WebMethodsCreateNManualSubscribtion,
                        data: '{"payload":' + JSON.stringify(response.d) + '}',
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (response) {
                            GetUserinfo();
                        }
                    });
                }
            }
        });

}

function UpdateSubscribtion(orderId) {
    var OrderPo = {};
     OrderPo.OrderId = orderId;
    OrderPo.OrderStatusId = $("#selectedStatus_"+orderId+" option:selected").val();
    OrderPo.ChannelCount = $("#txtChannelAmmount_" + orderId).val();
    var txtExpireDateVal = $("#txtExpireDate_" + orderId).val();
    var expireDate;
    if (isDate(txtExpireDateVal)) {
        expireDate = $("#txtExpireDate_" + orderId).datepicker("getDate");
    }
    else {
        expireDate = null;
    }

    OrderPo.OrderExpirationDate = expireDate;
    var r = confirm("Warning: are you sure you want update the subscription?");
   
    if (r == true) {
        $.ajax({
            type: "POST",
            url: WebMethodUpdateOrderById,
            data: '{"orderPo":' + JSON.stringify(OrderPo) + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response) {
                    GetUserinfo();
                }
            }
        });
    }
    
}
var isDate = function (date) {
    return !!(function (d) { return (d !== 'Invalid Date' && !isNaN(d)) })(new Date(date));
};

function ClearSchedules() {
    var selected = GetSelectedChannelId();
  
    if (selected == undefined) {
        alert("please select at least one of the checkboxes");
       
    }
    else {
        var r = confirm("Warning: all schedules will be deleted");
        //ajax remove schedules
        if (r == true) {
            $.ajax({
                type: "POST",
                url: WebMethodClearSchedules,
                data: '{"selectedChannelId":' + "'" + selected + "'" + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    alert("All schedules of the selected channel were successfully deleted");
                    UpdateAdminActivity("Clear schedules for channel: " + selected, "");
                }
            });
        }
    }
    
   

}
function GetSelectedChannelId() {
    var checkArr = [];
    $(".chkChannel:checked").each(function () {
        checkArr.push($(this).val());
    });
    var selected = checkArr.join(",");
    if (selected.length > 1) {
             
        return selected;
    }
   
};
$("input:checkbox").change(function () {
    $("input:checkbox").attr("checked", false);
    $(this).attr("checked", true);
});
function DeleteChannel() {
    var selected = GetSelectedChannelId();
 
    if (selected == undefined) {
        alert("please select at least one of the checkboxes");

    }
    else {
        var r = confirm("Warning: all selected channels will be deleted");
        if (r == true) {
            $.ajax({
                type: "POST",
                url: WebMethodDeleteChannels,
                data: '{"selectedChannelId":' + "'" + selected + "'" + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    alert("The selected channel of this user was successfully deleted");
                    $(".chkChannel:checked").each(function () {
                        var channelId = $(this).val();
                        $("#channelContentHolder_" + channelId).remove();
                    });
                    $(".chkChannel").attr("check", false);
                    GetUserinfo();
                    UpdateAdminActivity("channel delete. channelId:" + selected, "");
                }
            });
        }
        else {
            return
        }
    }
   
}

function LockUser() {
    var userAccountNum = $("#spnAccountNum").text();
    var isLocking = true;
    var r = confirm("The access to account for this user will be locked!");
    if (r == true) {
        $.ajax({
            type: "POST",
            url: WebMethodLockUnlockUser,
            data: '{"acountNum":' + "'" + userAccountNum + "'" + ',"locking":' + isLocking + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                $("#spnStatus").text("").text("locked access");
                $("#ancLockUnlockUser").text("").text("unlock access to account").removeAttr("onclick").attr("onclick", "UnLockUser()");
                alert("The access to the account of this user was successfully locked");
                GetUserinfo();
                UpdateAdminActivity("lock user", "");
            }
        });
    }
    else {
        return;
    }
  
}

function UnLockUser() {
    var userAccountNum = $("#spnAccountNum").text();
    var isLocking = false;
    $.ajax({
        type: "POST",
        url: WebMethodLockUnlockUser,
        data: '{"acountNum":' + "'" + userAccountNum + "'" + ',"locking":' + isLocking + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
        
            $("#spnStatus").text("").text("active");
            $("#ancLockUnlockUser").text("").text("lock access to account").removeAttr("onclick").attr("onclick", "LockUser()");
            alert("The access to the account of this user was successfully unlocked");
            GetUserinfo();
            UpdateAdminActivity("unlock user", "");
        }
    });

}

function EnablePro(isProEnabled) {
    var userAccountNum = $("#spnAccountNum").text();

    isProEnabled = isProEnabled ? isProEnabled : false;

    var isLocking = true;
    var r = confirm("PRO Entitlements will be " + (isProEnabled ? "added to" : "removed from") + " this user. Please confirm!");
    if (r == true) {
        $.ajax({
            type: "POST",
            url: WebMethodEnableProForUser,
            data: '{"accountNum":' + "'" + userAccountNum + "'" + ',"isProEnabled":' + isProEnabled + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.d) {
                    var isSuccess = response.d.IsSuccess;
                    if (isSuccess) {

                        var button = $("#ancEnabledDisablePro");

                        if (isProEnabled) {
                            $("#spnStatus").text("").text("PRO features were successfully enabled for user");
                            button.text("").text("Disable Pro").removeAttr("onclick").attr("onclick", "EnablePro(false)");
                            UpdateAdminActivity("pro enabled for user", "");
                            alert("Pro features on this account were successfully enabled!");
                        }
                        else {
                            $("#spnStatus").text("").text("PRO features were successfully disabled for user");
                            button.text("").text("Enable Pro").removeAttr("onclick").attr("onclick", "EnablePro(true)");
                            UpdateAdminActivity("pro disabled for user", "");
                            alert("Pro features on this account were successfully disabled!");
                        }
                    }
                    else {
                        $("#spnStatus").text("").text("Error occured while processing the request: " + response.d.Message);
                    }
                }
                else {
                    $("#spnStatus").text("").text("Error occured while submitting the response");
                }
                GetUserinfo();
            }
        });
    }
    else {
        return;
    }

}

function EnableSubscription(isSubscriber) {
    var userAccountNum = $("#spnAccountNum").text();

    isSubscriber = isSubscriber ? isSubscriber : false;

    var isLocking = true;
    var r = confirm("Subscription should be " + (isSubscriber ? "added to" : "removed from") + " this user. Please confirm!");
    if (r == true) {
        $.ajax({
            type: "POST",
            url: WebMethodAddSubscriptionUser,
            data: '{"accountNum":' + "'" + userAccountNum + "'" + ',"isSubscriber":' + isSubscriber + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.d) {
                    var isSuccess = response.d.IsSuccess;
                    if (isSuccess) {
                        var button = $("#ancAddRemoveSubscription");

                        if (isSubscriber) {
                            $("#spnStatus").text("").text("Subscription added to user");
                            button.text("").text("Remove Subscription").removeAttr("onclick").attr("onclick", "EnableSubscription(false)");
                            UpdateAdminActivity("subscription added to user", "");
                            alert("User is a subscriber now!");
                        }
                        else {
                            $("#spnStatus").text("").text("PRO features were successfully disabled for user");
                            button.text("").text("Add Subscription").removeAttr("onclick").attr("onclick", "EnableSubscription(true)");
                            UpdateAdminActivity("subscription removed from user", "");
                            alert("User is no longer a subscriber!");
                        }
                    }
                    else {
                        $("#spnStatus").text("").text("Error occured while processing the request: " + response.d.Message);
                    }
                }
                else {
                    $("#spnStatus").text("").text("Error occured while submitting the response");
                }

                GetUserinfo();
            }
        });
    }
    else {
        return;
    }

}

function ShowEdit() {
    $("#divEditNotes").show();
   
   // GetNotesForUser();

}
function GetNotesForUser() {
    var dataset = [];
    $.ajax({
        type: "POST",
        url: webMethodGetAdminActivity,
        data: '{"userId":' +  userid + '}',
        cashe: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var cdata = response.d;
            //console.log(cdata)
            //var data = JSON.parse(cdata);
            if (response.d != null)
            {
                $.each(cdata, function (i, c) {
            
                    var obj = Array();
                    var channelNames = "";
                    obj[0] = c.AdminUserName;
                    obj[1] = c.UserName;
                    obj[2] = c.Action;
                    if (c.Notes == null || c.Notes == "") {
                        obj[3] = "N/A"
                    }
                    else {
                        obj[3] = c.Notes;
        }
                   
                    var re = /-?\d+/;
                    var m = re.exec(c.CreatedDate);
                    var d = new Date(parseInt(m[0]));
                    obj[4] = d;
                    

                    dataset.push(obj);

    });

}
          
        },
        complete: function () {

       



            $('#adminActionTable').html(" ").html('<table cellpadding="0" cellspacing="0" border="0" class="display" id="example"></table>');
            $('#example').dataTable({
                "dom": 'T<"clear">lfrtip',
 
                "data": dataset,
                "columns": [
                   { "title": "Admin Name" },
                   { "title": "Public Name" },
                   { "title": "Action" },
                   { "title": "Note" },
                   { "title": "Last Activity" },
   
                ]
            });
}
    });
};


function SaveNotes() {
    var notes = $("#txtAreaAdminNotes").val();
    UpdateAdminActivity("note adding",notes)
};

function UpdateAdminActivity(action, notes)
{
    //console.log(adminUserId)
    $.ajax({
        type: "POST",
        url: WebMethodSaveUpdateNotes,
        data: '{"notes":' + "'" + notes + "'" + ',"userId":' + userid + ',"adminUserId":' + adminUserId + ',"action":' + "'" + action + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            GetNotesForUser();

        }
    });
}
function DeleteAccount() {
    var userName = $("#spnUserName").text();
    var r = confirm("Warning: user account will be deleted");
    if (r == true) {
        $.ajax({
            type: "POST",
            url: WebMethodDeleteAccount,
            data: '{"userName":' + "'" + userName + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                alert("The entire account of this user was successfully deleted");
                GetUserinfo();
                UpdateAdminActivity("account deltetion", "");
             a
            }
        });
    }
    else {
        return
    }
}

function SendWelcomeEmailToUser() {
    var userName = $("#spnUserName").text();
    if (userName && userName != "") {
        $.ajax({
            type: "POST",
            url: WebMethodSendWelcomeEmail,
            data: '{"userName":' + "'" + userName + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.d == true) {
                    alert("Welcome email was successfully send!");
                }
                else {
                    alert("Failed to send welcome email to user!");
                }                
            }
        });
    }
}

