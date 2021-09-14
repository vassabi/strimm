<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmbeddedChannelPassword.aspx.cs" Inherits="StrimmTube.EmbeddedChannelPassword" %>

<!DOCTYPE html>

<html >

<head runat="server">

    <link href="/Plugins/slider/css/jquery.bxslider.css" rel="stylesheet" />
    
   <%-- <script src="https://api.dmcdn.net/all.js"></script>--%>
    <script src="Plugins/slider/js/jquery.bxslider.min.js"></script>
    <script src="jquery/jquery.timepicker.js"></script>
    <script src="Plugins/Spin/spin.js"></script>

    <style>
        #contentWrapper {
            width: 1024px;
            height: 750px;
            background-color: #111;
            position: relative;
        }

        #channelPasswordBox {
            position: absolute;
            top: 50%;
            left: 50%;
            width: 400px;
            height: auto;
            margin-top: -9em;
            margin-left: -15em;
            border: 2px solid #2a99bd;
            background-color: #f3f3f3;
            padding: 20px;
        }

        .popupHeaderEmbedded {
            font-size: 17px;
        }

        .btnPasswordValidOK {
            display: block;
            float: left;
            width: 37px;
            height: 37px;
            line-height: 37px;
            text-transform: capitalize;
            background: #2a99bd;
            text-align: center;
            color: #fff;
            margin-left: 10px;
        }

            .btnPasswordValidOK:hover {
                background: #1f677e;
            }

        #channelPasswordInput {
            width: 280px;
            height: 37px;
            border: 1px solid #ddd;
            line-height: 37px;
            padding-left: 10px;
            float: left;
        }
        #passMessage  {
  color: #f25d06;
  display: block;
  font-size: 16px;
  height: 15px;
  line-height: 15px;
  margin-top: 5px;
  float:left;
}
    </style>



    <title></title>




</head>

<body>
    <%= new StrimmTube.CorsUpload { }.ToString()%>
    <script type="text/javascript">

        var webMethodValidateChannelPasswordByChannelName = "/WebServices/ChannelWebService.asmx/ValidateChannelPasswordByChannelName";
        var webMethodGetChannelTubePoByChannelUrl = "/WebServices/ChannelWebService.asmx/GetChannelTubePoByChannelUrl";
        var url;
        var embedUrl;
        var channelUrl;
        var channelTube;
        var userName = '<%=channelOwnerUserName%>';
         var accountNumber = '<%=accountNumber%>'
        var channelPassword = '<%=channelPassword%>';

        $(document).ready(function () {
            $("#channelPasswordBox #channelPasswordInput").keypress(function (event) {

                if (event.which == 13) {
                    event.preventDefault();
                    ValidateChannelPassword();
                    //  return false;
                }

            });
            url = this.referrer;
            embedUrl = this.documentURI;
            if (url != undefined && embedUrl != undefined) {
                channelUrl = getParameterByName("channelName", embedUrl);
                //username = getParameterByName("userName", embedUrl)
                console.log(userName)

            }

            var params = '{"channelUrl":' + "'" + channelUrl + "'" + '}';
            channelTube = $.ajax({
                type: "POST",
                url: webMethodGetChannelTubePoByChannelUrl,
                data: params,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (response) {


                },
                error: function (jqXHR, textStatus, errorThrown) {
                }
            });
        })
        function getParameterByName(name, url) {

            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)", "i"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        };

        function getCookie(cname) {
            return $.cookie(cname)
        }

        function setCookie(c_name, value, exdays) {
            $.cookie(c_name, value, { path: '/', expires: exdays });
        }

        function ValidateChannelPassword() {


            var cookiya = getCookie(channelUrl);
            console.log(cookiya);
            channelPassword = channelTube.responseJSON.d.ChannelPassword;
            txtChannelPasswordInputVal = $("#channelPasswordInput").val();
            var params = '{"channelName":' + "'" + channelUrl + "'" + ',"password":' + "'" + txtChannelPasswordInputVal + "'" + '}';
            console.log(txtChannelPasswordInputVal)
            $("#passMessage").text("");

            $.ajax({
                type: "POST",
                url: webMethodValidateChannelPasswordByChannelName,
                data: params,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (response) {
                    var channelPass;
                    if (response.d == true) {
                        console.log(response.d);
                        setCookie(channelUrl, txtChannelPasswordInputVal, -1);
                        setCookie(channelUrl, txtChannelPasswordInputVal, 30);
                        var passwordCh = getCookie(channelUrl);
                        var href = "/embedded/" + userName + "/" + channelUrl + "?accountNumber=" + accountNumber;
                        //window.location.reload();
                        window.location.href = href;
                        console.log("location href: " + href);
                    }
                    else {
                      
                        setCookie(channelUrl, channelPassword, -1);
                        $("#passMessage").text("").text("The password is incorrect. Please try again");
                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                }
            });

        };



    </script>

    <form>
        <div id="contentWrapper">
            <div id="channelPasswordBox">
                <h1 class="popupHeader popupHeaderEmbedded">Please enter a password to access this channel</h1>
                <span id="passMessage"></span>
                <input type="password" id="channelPasswordInput" />
                <a id="btnPasswordValid" class="btnPasswordValidOK" onclick="ValidateChannelPassword()">Go</a>
                <%--  <a href="/<%=userName%>">contact channel owner</a>--%>
                
            </div>
        </div>
    </form>
</body>
</html>
