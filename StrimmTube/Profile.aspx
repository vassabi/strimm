<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="StrimmTube.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
    Personal profile | Strimm
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
    <meta name="description" content="Personal profile | Strimm" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <%: System.Web.Optimization.Styles.Render("~/bundles/profile/css") %>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/profile/js") %>
    <%--   <link href="https://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" rel="stylesheet" />--%>
    <%--<link href="/css/profileCSS.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <script src="DatePicker/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.widget.js" type="text/javascript"></script>--%>

    <%-- <script src="/JS/Profile.js" type="text/javascript"></script>
     <script src="/Plugins/popup/jquery.lightbox_me.js"></script>--%>

    <script type="text/javascript">
        var currUserPass = '<%=currUserPass%>';
        var userId = '<%=userId%>';
        var email = '<%=email%>';
        var isEx = '<%=isExternal%>';
        var subscribtion;
        var webMethodGetUserProductSubscriptionsByUserId = "/WebServices/UserService.asmx/GetUserProductSubscriptions";
        $(document).ready(function () {
            if (isEx == 'True') {
                $("#btnChangePass").hide();
            }
            subscribtion = $.ajax({
                //userId=33 - no subscr
                //userId = 29 subscr
                //change after that to userId
                type: "POST",
                url: webMethodGetUserProductSubscriptionsByUserId,
                cashe: false,
                async: false,
                data: '{"userId":' + userId + '}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {



                },
                error: function (request, status, error) {

                }
            });
            console.log(subscribtion);
        });
        function CloseSubscriptionPopup() {
            $('#subscriptionPopup').trigger("close");
        }
        function OpenSubscribtionPlan() {
            $('#subscriptionPopup').lightbox_me({
                centered: true,
                onLoad: function () {
                    console.log(subscribtion.responseJSON.d.Response);
                    if (subscribtion.responseJSON.d.Response.length === 0) {
                        var noSubscribtionHtml = "<div id='subscribTionMessage'>you don't have any subscription please <a href='/create-tv/pricing'>Subscribe</a></div>"
                        $("#subscribtionTable").html("").append(noSubscribtionHtml)
                    }
                    else {
                        var subscribtionHtml = "<table class='subscrTable'>"
                        subscribtionHtml += "<tr>"
                        subscribtionHtml += "<th class='colorUp2'>Subscription Plan</th>"
                        subscribtionHtml += "<th class='orderNumber'>Order Number</th>"
                        subscribtionHtml += "<th class='colorUp2'>Channel Count</th>"
                        subscribtionHtml += "<th>Start Date</th>"
                        subscribtionHtml += "<th class='colorUp2'>Billing Cycle</th>"
                        subscribtionHtml += "<th>Status</th>"
                        subscribtionHtml += "<th> </th>"
                        subscribtionHtml += "</tr>"

                        $.each(subscribtion.responseJSON.d.Response, function (i, d) {
                            //if (d.ProductSubscriptionStatus != 'Canceled') {
                            var productName = d.ProductName;
                            var orderNumber = d.OrderNumber;
                            var channelCount = d.ChannelCount;
                            if (channelCount == 1000) {
                                channelCount = 'UNLIMITED';
                            }
                            var createdDate = new Date(parseInt(d.CreatedDate.replace('/Date(', '')));
                            var billingCycle = "monthly";
                            subscribtionHtml += "<tr>";
                            subscribtionHtml += "<td class='colorUp'>" + productName + "</td>";
                            subscribtionHtml += "<td class='orderNumber'>" + orderNumber + "</td>";
                            subscribtionHtml += "<td class='colorUp'>" + channelCount + "</td>";
                            subscribtionHtml += "<td>" + createdDate.toLocaleDateString(); + "</td>";
                            subscribtionHtml += "<td class='colorUp'>" + billingCycle + "</td>";
                            subscribtionHtml += "<td>" + d.ProductSubscriptionStatus + "</td>";
                            if (d.ProductSubscriptionStatus == "Active") {
                                subscribtionHtml += "<td><A class='unsubscribe' HREF='https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_subscr-find&alias=" + d.UnSubscrButtonId + "\'>Unsubscribe</A></td>";
                            }
                            subscribtionHtml += "</tr>";
                            //}
                        });
                        subscribtionHtml += "</table>";
                        subscribtionHtml += "<span id='spnQuestion'>Questions about your subscriptions? Contact us at <a class='spnQuestion' href='mailTo:support@strimm.com'>support@strimm.com</a> </span>";
                        $("#subscribtionTable").html("").append(subscribtionHtml)
                    }
                },
                onClose: function () {
                    RemoveOverlay();
                }
            });
        }

        <%--function checkMessageStatus() {
            $("#<%=allowMatureVideo.ClientID %>").prop('checked', true);
            CloseAgeConfirmationPopup()
            
        }

        function uncheckMessageStatus() {
            $("#<%=allowMatureVideo.ClientID %>").prop('checked', false);
            CloseAgeConfirmationPopup()
            
        }--%>
    </script>

    <div class="learnMoreButtonHolder">
        <h1 class="pageTitle">my profile</h1>
        <%--    <div class="buttonsHolder">
        <a href="#"  class="learmMoreWatchVideo" onclick="ShowTutorialPlayer('MTTeo-3NMfk')">watch video</a>
     
         <a href="/Guides.aspx" class="learmMoreHowTo">how to</a>
    </div>--%>
    </div>

    <div class="mainContentWrapper">
        <div id="divBoardContent">

            <div id="divProfile">

                                <div class="personalPref">
                    <input type="button" id="btnChangePass" onclick="ShowChangePassword()" value="Change Password" />
                    <a class="emailPref" href="/email-preferences">Update e-mail Preferences</a>
                    <div class="subscribtionPlan">
                        <a class="subscribtion" onclick="OpenSubscribtionPlan()">Subscription Plan </a>


                    </div>
               
                </div>

                <ul>
                    <li>
                        <%--    <span>*public name</span>--%>
                        <asp:Label runat="server" ID="lblUserName" ClientIDMode="Static" Placeholder="Username">
                        </asp:Label>
                    </li>
                    <li><%--<span>*first name</span>--%><asp:TextBox runat="server" ClientIDMode="Static" ID="txtFirstName" Placeholder="First Name"></asp:TextBox>
                        <div class="divError" id="erFirstName"><span class="spnError">First Name is required</span></div>
                    </li>

                    <li><%--<span>*last name</span>--%><asp:TextBox runat="server" ClientIDMode="Static" ID="txtlastName" Placeholder="Last Name"></asp:TextBox>
                        <div class="divError" id="erLastName"><span class="spnError">Last Name is required</span></div>
                    </li>
                    <li><%--<span>company</span>--%><asp:TextBox runat="server" ClientIDMode="Static" ID="txtCompany" Placeholder="Company"></asp:TextBox></li>
                    <li class="liInput">
                        <%--      <span>*country</span>--%>
                        <asp:DropDownList runat="server" ID="ddlCountry" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select Country</asp:ListItem>
                        </asp:DropDownList>
                        <div class="divError" id="erCountry"><span class="spnError">Country is required</span></div>
                    </li>
                    <li>
                        <div runat="server" id="stateHolder">
                            <%-- <span>*state/Province</span>--%>
                            <asp:DropDownList runat="server" ID="ddlSates" ClientIDMode="Static"></asp:DropDownList>
                            <div class="divError" id="erState"><span class="spnError">State is required</span></div>
                        </div>
                    </li>
                    <li class="liTxtAdress"><%--<span>address</span>--%><asp:TextBox runat="server" ID="txtAddress" ClientIDMode="Static" Placeholder="Address"></asp:TextBox></li>
                    <li><%--<span>*city</span>--%><asp:TextBox runat="server" ID="txtCity" ClientIDMode="Static" Placeholder="City"></asp:TextBox></li>
                    <li><%--<span>*zip/Postal code</span>--%><asp:TextBox runat="server" ClientIDMode="Static" ID="txtZip" Placeholder="Zip/Postal Code"></asp:TextBox></li>
                    <li class="validateDateLi"><span class="birthdayProfile">birthdate</span>
                        <asp:UpdatePanel runat="server" ID="up">
                            <ContentTemplate>
                                <%-- <span class="spnBirthday">yyyy:</span>--%><asp:DropDownList runat="server" ID="ddlYear" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                                <%-- <span class="spnBirthday"> mm:</span>--%><asp:DropDownList runat="server" ID="ddlMonth" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                                <%-- <span class="spnBirthday"> d:</span>--%><asp:DropDownList runat="server" ID="ddlDay" ClientIDMode="Static" AutoPostBack="true"></asp:DropDownList>
                                  <div class="divError" id="erBirthday"> <div class="spnErrorCloseMobile"></div>
                                    <span class="spnError">Please select a valid date</span></div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlYear" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlMonth" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlDay" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </li>

                    <li><span></span><span class="spnBirthday">(year)</span><span class="spnBirthday">(month)</span><span class="spnBirthday">(day)</span></li>
                    <li><span class="spnGender birthdayProfile">gender</span>
                        <asp:RadioButtonList runat="server" ID="gender" ClientIDMode="Static" CssClass="radioList" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Male" Value="Male" class="ListItemMale"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="Female" class="ListItemFemale"></asp:ListItem>
                        </asp:RadioButtonList>
                    </li>
                    <li><%--<span>*email</span>--%><asp:TextBox runat="server" ClientIDMode="Static" ID="txtEmail" Placeholder="E-mail"></asp:TextBox>

                        <div class="divError" id="erEmail"><span class="spnError">Email is required</span></div>
                    </li>
                      </ul>

<%--                    <div class="privateInfo">
                   <asp:CheckBox ID="allowPrivateViewMode" runat="server" ClientIDMode="Static" type="checkbox" Text="enable private video mode" name="checkPrivateVideoMode" CssClass="allowPrivateVideoMode" />

                    <asp:CheckBox runat="server" ID="allowMatureVideo" ClientIDMode="Static" type="checkbox" name="checkMatureVideo" Text="enable mature content videos" OnClick="ConfirmAgeControl(this);" CssClass="allowMatureVideo" />

                    </div>--%>

                    <div id="divSave">
                        <%--      <input type="button" id="btnChangePass" onclick="ShowChangePassword()" value="change password" />--%>
                        <asp:Button runat="server" ID="btnSave" ClientIDMode="Static" Text="Submit" OnClick="btnSave_Click" OnClientClick="JavaScript: return ValidateRequired()" />


                    </div>


              





                <div id="subscriptionPopup">
                    <a class="close_x" id="close" onclick="CloseSubscriptionPopup()"></a>
                    <h1 class="popupHeader">Subscription plan</h1>
                    <div id="subscribtionTable">
                    </div>
                </div>
                <div id="divChangePassword">
                    <div class="divChangePasswordContainer">
                        <a class="close_x" id="close" onclick="CloseChangePassword()"></a>
                        <h1 class="popupHeader">Change password</h1>
                        <div class="divChangePassFields">
                            <span class="spnChangePassword">current password  </span>
                            <asp:TextBox runat="server" TextMode="Password" ID="txtCurPass" ClientIDMode="Static"></asp:TextBox>
                            <div class="divError" id="erCurrPassword"><span clientidmode="static" runat="server" id="spnErCurrPass" class="spnError"></span></div>
                        </div>

                        <div class="divChangePassFields">
                            <span class="spnChangePassword">new password  </span>
                            <asp:TextBox runat="server" TextMode="Password" ID="txtNewPass" ClientIDMode="Static"></asp:TextBox>
                            <div class="divError" id="erNewPassword"><span clientidmode="static" runat="server" id="spnErNewPass" class="spnError"></span></div>
                        </div>

                        <div class="divChangePassFields">
                            <span class="spnChangePassword">Re-enter password  </span>
                            <asp:TextBox runat="server" TextMode="Password" ID="txtReEnterPass" ClientIDMode="Static"></asp:TextBox>
                            <div class="divError" id="erReEnterPassword"><span clientidmode="static" runat="server" id="spnErReEnterPass" class="spnError"></span></div>
                        </div>


                        <input type="button" id="btnPassSubmit" value="submit new password" />
                        <span id="passwordMessage"></span>
                    </div>
                </div>

                <asp:Label runat="server" ID="lblMessage" ClientIDMode="Static"></asp:Label>

            </div>
        </div>
    </div>
    <uc:FeedBack runat="server" ID="feedBack" pageName="profile" />
    <div id="AgeConfirmationPopup">
       
             <span id="spnConfirm">WARNING: Due to nature of the mature videos, all channels in your account will be available for embedding only with a paid "Professional" subscription. A public broadcast on Strimm will be restricted</span>
            <a id="close_x" class="close close_x" href="#" onclick="CloseAgeConfirmationPopup()"></a>

        <a id="btnAgeConfirm" onclick="checkMessageStatus()">Ok</a>
        <a id="btnCancelMessage" onclick="uncheckMessageStatus()">cancel</a>

    </div>
</asp:Content>
