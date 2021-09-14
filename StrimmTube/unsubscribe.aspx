<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="unsubscribe.aspx.cs" Inherits="StrimmTube.unsubscribe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="canonicalHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="/JS/EmailPreferences.js"></script>

    <style>
         .unsubscribeContent {
    width: 96%;
    margin: auto;
    max-width: 800px;
    margin-top: 40px;
    padding: 2%;
         }

         .unsubscribePageTitle {
                 display: block;
    width: 98%;
    padding-bottom: 30px;
    font-size: 22px;
    color: #2a99bd;
    text-align: left;
    font-weight: normal;
         }


             p strong {
                 color: #555;
             }

         .categoryBlock {
     display: block;
     float: left;
     width: 100%;
     position: relative;
     height: 70px;
     margin-bottom: 15px;

         }

         .checkTitle,
          .checkSubTitle {
    float: left;
    margin-left: 20px;
    font-size: 14px;
    display: block;
         }
         .categoryBlock .checkTitle {
    display: block;
    float: left;
    color: #555;
    font-size: 14px;
    padding-left: 20px;
    width: 100%;
    margin-bottom: 0;
    font-weight: bold;
}

         .checkSubTitle {
             font-weight: normal;
             top: 25px;
         }

         .checkBoxUncubscribe {
             display: block;
             float: left;
             width: 25px;
        height: 25px;
        border: 1px solid #ccc;
        position: absolute;
        left:0;
         }
  [type="checkbox"]{
    -webkit-appearance: none;
    outline: none;
    background-size: 100%;
    width: 20px;
    height: 20px;
        }

        [type="checkbox"]:checked {
    -webkit-appearance: none;
    outline: none;
    background-image: url(/images/checkIcons/check-mark-green.png);
    background-size: 100%;
    width: 20px;
    height: 20px;
        }

         .unchecked {
             margin-top: 20px;
         }

                  .updatePrefHolder {
             display: block;
             float: left;
             width: 100%;
             height: 50px;
             margin-bottom: 40px;

         }

         .updatePref {
             width:220px;
             background-color: #2a99bd;
            height: 50px;
            display: block;
            margin: auto;
            color: #fff;
            line-height: 50px;
            font-size: 17px;
            cursor: pointer;
            text-decoration: none;
            font-weight: normal;
            margin-top: 20px;
    text-align: center;
            margin-bottom: 15px;
            margin: auto;
            text-transform: capitalize;
         }





    </style>

    <script>
        var email = '<%=UserEmail%>';
        var id = '<%=UserId%>';
        var prefManager = window.getPreferences();

        $(document).ready(function () {
            console.log(id)
            prefManager.init(email, id);
        });
    </script>
    <div class="unsubscribeContent" >

        <h1 class="unsubscribePageTitle">
            Communication Preferences
        </h1>

        <p >
            Hello <%=UserName%>,
        </p>

        <p >
            Use this screen to manage your communication preferences for this site. Based on your selection, you will be removed or added to various e-mailing notifications from us. 
        </p>

        <p>
            <strong><span style="text-decoration: underline;"> Uncheck </span>the types of emails you <span style="text-decoration: underline;">do not want </span>to receive:</strong>
        </p>

        <div class="categoryBlock">
            <input id="chkNews" type="checkbox" class="checkBoxUncubscribe" checked="checked" onclick="prefManager.updateSelection()"/>
            <p class="checkTitle"> Newsletter</p>
            <p class="checkSubTitle">Our latest blogs, industry and site news</p> 
        </div> 

<%--        <div class="categoryBlock">
            <input id="chkServices" type="checkbox"  class="checkBoxUncubscribe" checked="checked" />
            <p class="checkTitle"> Product and Services updates</p>
            <p class="checkSubTitle">Get notification on our new services and products</p> 
        </div>--%>

        <div class="categoryBlock">
            <input id="chkGreetings" type="checkbox"  class="checkBoxUncubscribe" checked="checked" onclick="prefManager.updateSelection()"/>
            <p class="checkTitle"> Greetings</p>
            <p class="checkSubTitle">Holidays, birthday and special occasion greetings.</p> 
        </div>

        <div class="categoryBlock">
            <input id="chkMarketing" type="checkbox"  class="checkBoxUncubscribe" checked="checked" onclick="prefManager.updateSelection()"/>
            <p class="checkTitle"> Marketing Tips and Recommendations</p>
            <p class="checkSubTitle">Get advice and recommendations on how to promote your channel and benefit from it. </p> 
        </div>

        <div class="categoryBlock">
            <input id="chkReminders" type="checkbox"  class="checkBoxUncubscribe" checked="checked" onclick="prefManager.updateSelection()"/>
            <p class="checkTitle"> Reminders </p>
            <p class="checkSubTitle">Reminder to publish a schedule, create channel, upload avatar, etc. </p> 
        </div>

        <div class="categoryBlock">
            <input id="chkSocial" type="checkbox" class="checkBoxUncubscribe" checked="checked" onclick="prefManager.updateSelection()"/>
            <p class="checkTitle"> Social Notifications </p>
            <p class="checkSubTitle">Get notification when someone sends you a message, likes your channel or page or follows you</p> 
        </div>

        <div class="categoryBlock unchecked">
            <input id="chkAll" type="checkbox" class="checkBoxUncubscribe" onclick="prefManager.processFullUnsubscribe()"/>
            <p class="checkTitle"> Unsubscribe me from all mailing lists  </p>
        </div>
                 
        <div class="updatePrefHolder" onclick="prefManager.saveEmailPreferences()">
            <a id="btnSavePreferences" class="updatePref">update email preferences</a>
        </div>

        <p >
            Please note that you may receive occasional administrative emails related to your account at Strimm. 
            Such emails may include but are not limited to account issues, updates or a password reset email. 
        </p>

     </div>

</asp:Content>
