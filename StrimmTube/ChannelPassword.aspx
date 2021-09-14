<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="ChannelPassword.aspx.cs" Inherits="StrimmTube.ChannelPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server" ClientIDMode="Static">
    <link href="css/channelPageNew.css" rel="stylesheet" />
    <style>
        #contentWrapper {
            width: 100%;
            height: 800px;
            background-color: #111;
            position: relative;
        }

        #channelPasswordBox {
            /*add div in a middle of the screen*/
            position: absolute;
            top: 50%;
            left: 50%;
            width: 400px;
            height: auto;
            margin-top: -9em; /*set to a negative number 1/2 of your height*/
            margin-left: -15em; /*set to a negative number 1/2 of your width*/
            border: 2px solid #2a99bd;
            background-color: #f3f3f3;
            padding: 20px;
        }
    </style>

 <script type="text/javascript">
     var webMethodValidateChannelPasswordByChannelName = "/WebServices/ChannelWebService.asmx/ValidateChannelPasswordByChannelName";
    
     var channelUrl = '<%=channelUrl%>';
     var channelOwnerUserName = '<%=channelOwnerUserName%>'

     function ValidateChannelPassword()
     {
         var addedChannelPassword = $("#channelPasswordInput").val();
         var params = '{"channelName":' + "'" + channelUrl + "'" + ',"password":' + "'" + addedChannelPassword + "'" + '}';
        
       
             $.ajax({
                 type: "POST",
                 url: webMethodValidateChannelPasswordByChannelName,
                 data: params,
                 dataType: "json",
                 contentType: "application/json; charset=utf-8",
                 success: function (response) {
                     var channelPass;
                     if(response.d==true)
                     {
                                            
                         setCookie(channelUrl, addedChannelPassword, -1);
                         setCookie(channelUrl, addedChannelPassword, 30);
                         var passwordCh = getCookie(channelUrl);
                         console.log(passwordCh);
                       

                      


                        window.location.href = "/" + channelOwnerUserName + "/" + channelUrl;
                     }
                     else
                     {
                         alertify.error("the password is not correct")
                     }

                 },
                 error: function (jqXHR, textStatus, errorThrown) {
                 }
             });

     }

 </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="contentWrapper">
        <div id="channelPasswordBox">
            <h1 class="popupHeader channelProtectedTitle">Please enter a password to access this channel</h1>
            <input type="password" id="channelPasswordInput" />
            <a onclick="ValidateChannelPassword()" class="passwordChannelGo">Go</a>
            <%--  <a href="/<%=userName%>">contact channel owner</a>--%>
            <span id="passMessage"></span>
        </div>
    </div>
</asp:Content>
