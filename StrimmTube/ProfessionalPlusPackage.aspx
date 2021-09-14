<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="ProfessionalPlusPackage.aspx.cs" Inherits="StrimmTube.ProfessionalPlusPackage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Strimm Online TV Solution for Businesses and Organizations of any kind" />
    <meta name="robots" content="FOLLOW, INDEX, NOODP, NOYDIR" />
    <meta name="GOOGLEBOT" content="FOLLOW, INDEX, NOODP, NOYDIR" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="canonicalHolder" runat="server">
    <link rel="canonical" href="https://www.strimm.com/company.aspx" />
</asp:Content>


<asp:Content ID="Content6" ContentPlaceHolderID="head" runat="server">
    <link href="/css/BusinessPageCSS.css" rel="stylesheet" />
    <link href="/css/paymentConfirmation.css" rel="stylesheet" />
    <link href="/css/packagesTrialConfirmation.css" rel="stylesheet" />
    <script src="/JS/Business.js" type="text/javascript"></script>
    <script src="/JS/OrderProcessing.js" type="text/javascript"></script>
    <script src="/JS/Package.js" type="text/javascript"></script>

    <script type="text/javascript">
        var userId = '<%=userId%>';
        var isAnnual = '<%=isAnnual%>';
        var productId = 7;
        var subscriptionId = null;

        $(document).ready(function () {
            $("#form1").removeAttr("action").attr("action", "<%=payPalServiceUrl%>");
            $("#form1").attr("target", "_blank");
            $("#form1").attr("method", "post");

            var prefs = window.getPreferences();
            prefs.init(userId, productId, isAnnual);

            subscriptionId = prefs.getSubscriberId();
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="contentWrapper">
        <div id="divTitleUrlHolder" class="packageTrialTitle">
            <h1 class="pageTitle">Professional Plus Package</h1>
            <span id="packagePrice" class="packagePriceConf"></span>
            <h2>You are just  few steps away from having your own TV channel on your website!
            </h2>

        </div>
        <div id="packageContent">
            <p id="subMsg" class="packageContentP">
            </p>


            <h2>What's included with your Professional Plus Package  </h2>



            <div class="packageContentUL">
                <ul>
                    <li> <span class="check"></span>100 Channels</li>
                    <li> <span class="check"></span>TV Channel in any category</li>
                    <li> <span class="check"></span>Linear TV Broadcast</li>
                    <li> <span class="check"></span>Embedded TV Guide</li>
                    <li> <span class="check"></span>Embedded Daily Schedule</li>
                    <li> <span class="check"></span>Instant Schedule </li>
                    <li> <span class="check"></span>Autopilot</li>
                    <li> <span class="check"></span>Extended Reach</li>
                    <li> <span class="check"></span>Unlimited Email Support</li>
                    <li> <span class="check"></span>Use of Commercials</li>
                    <li> <span class="check"></span>Mute Control</li>
                    <li> <span class="check"></span>Password-protection</li>
                    <li> <span class="check"></span>Embed-only option</li>
                    <li> <span class="check"></span>Multiple domains</li>
                    <li> <span class="check"></span>Dedicated account manager</li>
                    <li> <span class="check"></span>Player controls</li>
                    <li> <span class="check"></span>Free Promotion</li>
                    <li> <span class="check"></span>Broadcast of private videos</li>
                    <li> <span class="check"></span>Direct links use</li>
                    <li> <span class="check"></span>Unristricted video content</li>
                    <li> <span class="check"></span>White-Label</li>
                    <li><span class="check"></span>Custom Branding</li>
                    <li> <span class="check"></span>Access to API</li>
                    <li><span class="check"></span>Custom-Branded Roku App</li>
                </ul>
            </div>

            <%--            <select name="os0">
	            <option value="Single (1) Channel">Single (1) Channel : $89.99 USD - monthly</option>
	            <option value="Two (2) Channels">Two (2) Channels : $179.98 USD - monthly</option>
	            <option value="Three (3) Channels">Three (3) Channels : $269.97 USD - monthly</option>
	            <option value="Four (4) Channels">Four (4) Channels : $359.96 USD - monthly</option>
	            <option value="Five (5) Channels">Five (5) Channels : $449.95 USD - monthly</option>
	            <option value="Six (6) Channels">Six (6) Channels : $539.94 USD - monthly</option>
	            <option value="Seven (7) Channels">Seven (7) Channels : $629.93 USD - monthly</option>
	            <option value="Eight (8) Channels">Eight (8) Channels : $719.92 USD - monthly</option>
	            <option value="Night (9) Channels">Night (9) Channels : $809.91 USD - monthly</option>
	            <option value="Ten (10) Channels">Ten (10) Channels : $899.90 USD - monthly</option>
            </select>--%>
            <div id="continueHolder">
                <a id="btnContinue">Continue to Payment</a>
                 <span id="spnAllowPopup">(Allow popup in your browser to continue)</span>
                <%--                <div style="display:none">
                    <input type="hidden" name="cmd" value="_s-xclick">
                    <input type="hidden" id="hosted_button_id" name="hosted_button_id" value="">
                    <table>
                    <tr><td><input type="hidden" name="on0" value="Channel Count" style="display:none" >Select Amount Of Channels</td></tr>
                    <tr><td>--%>
                <%--                        <select name="os0">
	                    <option value="Single (1) Channel">Single (1) Channel : $89.99 USD - monthly</option>
	                    <option value="Two (2) Channels">Two (2) Channels : $179.98 USD - monthly</option>
	                    <option value="Three (3) Channels">Three (3) Channels : $269.97 USD - monthly</option>
	                    <option value="Four (4) Channels">Four (4) Channels : $359.96 USD - monthly</option>
	                    <option value="Five (5) Channels">Five (5) Channels : $449.95 USD - monthly</option>
	                    <option value="Six (6) Channels">Six (6) Channels : $539.94 USD - monthly</option>
	                    <option value="Seven (7) Channels">Seven (7) Channels : $629.93 USD - monthly</option>
	                    <option value="Eight (8) Channels">Eight (8) Channels : $719.92 USD - monthly</option>
	                    <option value="Night (9) Channels">Night (9) Channels : $809.91 USD - monthly</option>
	                    <option value="Ten (10) Channels">Ten (10) Channels : $899.90 USD - monthly</option>
                    </select> --%>
                <%--                    </td></tr>
                    <tr><td>
                        <input type="hidden" id="on1" name="on1" value="OrderNumber" style="display:none" >OrderNumber
                    </td></tr>
                    <tr><td>
                        <input type="hidden" id="os1" name="os1" maxlength="200">
                    </td></tr>
                    </table>
                    <input type="hidden" name="currency_code" value="USD">
                    <input type="image" style="display:none" src="https://www.paypalobjects.com/en_US/i/btn/btn_subscribeCC_LG.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!">
                    <img style="display:none" alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1">
                </div>--%>
                <div style="display: none">
                    <%--                    <form action="https://www.paypal.com/cgi-bin/webscr" method="post" target="_top">--%>
                    <input type="hidden" name="cmd" value="_s-xclick">
                    <input type="hidden" id="hosted_button_id" name="hosted_button_id" value="">
                    <table>
                        <tr>
                            <td>
                                <input type="hidden" name="on0" value="OrderNumber">OrderNumber</td>
                        </tr>
                        <tr>
                            <td>
                                <input type="hidden" id="os0" name="os0" maxlength="200"></td>
                        </tr>
                    </table>
                    <input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_subscribeCC_LG.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!">
                    <img alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1">
                    <%--                    </form>--%>
                </div>
            </div>
            <p class="pbyclicking">
                 By clicking on “Continue to Payment” you agree to Strimm’s <a class="termsadnpolicy" target="_blank" href="/terms-of-use">Terms of Use</a> and <a class="termsadnpolicy" target="_blank" href="privacy-policy">Privacy Policy</a>. You agree to be charged US<span id="byClickingPrice"></span>, starting once your 14-day trial expires, until you cancel your plan. 
            </p>
        </div>
    </div>
</asp:Content>
