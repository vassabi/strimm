var createOrder = "/WebServices/OrderWebService.asmx/PlaceOrder";

$(document).ready(function () {
    $('#btnContinue').click(function () {
            placeOrder();
    });
});

function placeOrder() {
    var request = {
        UserId: userId,
        ProductId: productId,
        IsAnnual: isAnnual,
        IsUpdate: false
    };
    $.ajax({
        type: "POST",
        url: createOrder,
        cashe: false,
        data: '{"model":' + JSON.stringify(request) + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async:false,
        success: function (response) {
            if (response.d != undefined) {
                setCookie('OrderDetails', response.d.OrderNumber, 1);
                //$('#os1').val(response.d.OrderNumber);
                $('#os0').val(response.d.OrderNumber);
                $('#hosted_button_id').val(subscriptionId);
                $("#form1").submit();
            }
        },
        complete: function (response) {
        }
    });
}
