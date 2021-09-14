<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="PaypalPayment.aspx.cs" Inherits="StrimmTube.PaypalPayment" %>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Strimm Online TV Solution for Businesses and Organizations of any kind" />
    <meta name="robots" content="FOLLOW, INDEX, NOODP, NOYDIR" />
    <meta name="GOOGLEBOT" content="FOLLOW, INDEX, NOODP, NOYDIR" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://www.strimm.com/company.aspx" />
</asp:Content>


<asp:Content ID="Content6" ContentPlaceHolderID="head" runat="server">
   
    <link href="/css/paymentConfirmation.css" rel="stylesheet" />
    <script src="/JS/Business.js" type="text/javascript"></script>
    <script src="/JS/OrderProcessing.js" type="text/javascript"></script>
   <%-- <script src="https://js.braintreegateway.com/web/3.3.0/js/client.min.js"></script>
<script src="https://js.braintreegateway.com/web/3.3.0/js/paypal.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/require.js/2.3.1/require.min.js"></script>
    <script src="https://www.paypalobjects.com/api/button.js?"
     data-merchant="braintree"
     data-id="paypal-button"
     data-button="checkout"
     data-color="blue"
     data-size="medium"
     data-shape="pill"
     data-button_type="submit"
     data-button_disabled="false"
 ></script>--%>
 <script type="text/javascript">
     var userId = '<%=userId%>';

     $(document).ready(function () {
         //require.config({
         //    paths: {
         //        braintreeClient: 'https://js.braintreegateway.com/web/3.3.0/js/client.min',
         //        braintreePaypal: 'https://js.braintreegateway.com/web/3.3.0/js/paypal.min'
         //    }
         //});
         //var braintree = require('braintree-web');

         //braintree.client.create({
         //    authorization: 'access_token$sandbox$mfq6xfx9d7858t9b$0bb6cefd1550fbcd0d85dfd826398bd8'
         //}, function (err, clientInstance) {
         //    braintree.paypal.create(/* ... */);
         //});
         //$("#form1").removeAttr("action").attr("action", "https://www.sandbox.paypal.com/cgi-bin/webscr");
         $("#form1").removeAttr("action").attr("action", "https://www.paypal.com/cgi-bin/webscr");
         $("#form1").attr("target", "_blank");
         $("#form1").attr("method", "post");
     });
     // Fetch the button you are using to initiate the PayPal flow
     //var paypalButton = document.getElementById('paypalBtn');

     //// Create a Client component
     //braintree.client.create({
     //    authorization: 'access_token$sandbox$mfq6xfx9d7858t9b$0bb6cefd1550fbcd0d85dfd826398bd8'
     //}, function (clientErr, clientInstance) {
     //    // Create PayPal component
     //    braintree.paypal.create({
     //        client: clientInstance
     //    }, function (err, paypalInstance) {
     //        paypalButton.addEventListener('click', function () {
     //            // Tokenize here!
     //            paypalInstance.tokenize({
     //                flow: 'checkout', // Required
     //                amount: 10.00, // Required
     //                currency: 'USD', // Required
     //                locale: 'en_US',
     //                enableShippingAddress: true,
     //                shippingAddressEditable: false,
     //                shippingAddressOverride: {
     //                    recipientName: 'Scruff McGruff',
     //                    line1: '1234 Main St.',
     //                    line2: 'Unit 1',
     //                    city: 'Chicago',
     //                    countryCode: 'US',
     //                    postalCode: '60652',
     //                    state: 'IL',
     //                    phone: '123.456.7890'
     //                }
     //            }, function (err, tokenizationPayload) {
     //                // Tokenization complete
     //                // Send tokenizationPayload.nonce to server
     //            });
     //        });
     //    });
     //});

   
     
               // $("#form1").submit(); //or whatever your WebForms form element is called
          
     
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


   <%-- <form action="https://www.paypal.com/cgi-bin/webscr" method="post" target="_top">--%>

<%--<input type="hidden" name="cmd" value="_s-xclick">
<input type="hidden" name="hosted_button_id" value="YFLQNKZKAFXNA">
<input type="image" src="https://www.sandbox.paypal.com/en_US/i/btn/btn_buynowCC_LG.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!">
<img alt="" border="0" src="https://www.sandbox.paypal.com/en_US/i/scr/pixel.gif" width="1" height="1">
--%>

<%-- </form>
 --%>

<%--    <form action="https://www.paypal.com/cgi-bin/webscr" method="post" target="_top">--%>
<input type="hidden" name="cmd" value="_s-xclick">
<input type="hidden" name="hosted_button_id" value="LQVBN2PDCZ5FN">
<table style="display:none">
<tr><td><input type="hidden" name="on0" value="OrderNumber" style="display:none">OrderNumber</td></tr><tr><td><input type="hidden" id="order" name="os0"/></td></tr>
</table>
<input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_subscribeCC_LG.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!" style="display:none">
<img alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1" style="display:none">
<%--</form>--%>
<input type="button" id="makePayment" value="Make Payment" />
<%--    <script type="text/javascript">
        var createOrder = "/WebServices/OrderWebService.asmx/PlaceOrder";

        $(document).ready(function () {
            $('#makePayment').click(function () {
                $('#order').val('My Order');
                $("#form1").submit();
            });
        });

        function placeOrder() {
            var request = {
                UserId: 1,
                ProductId: 1
            };
            $.ajax({
                type: "POST",
                url: createOrder,
                cashe: false,
                data: '{"model":' + JSON.stringify(request) + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d != undefined) {

                    }
                },
                complete: function (response) {
                }
            });

        }
    </script>--%>
   
</asp:Content>





