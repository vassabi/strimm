<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="PaswordRecovery.aspx.cs" Inherits="StrimmTube.PaswordRecovery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .error {
color: #2a99bd;
display: block;
font-size: 30px;
height: 40px;
line-height: 40px;
padding-top: 40px;
text-align: center;
width: 100%;
        }


        #content {
float: none;
height: 700px;
margin: auto;
margin-bottom: 10px;
        }

        #errorMsg {
display: block;
font-family: opensans-regular;
line-height: 25px;
width: 100%;
text-align: center;
padding: 30px;
font-size: 17px;
        }

        #passwordForm {
            display: block;
            width: 560px;
            margin: auto;
            height: 500px;
            padding-top: 60px;
        }


        .anchHolder {
        display: block;
margin: auto;
text-align: center;
width: 560px;
        }

        #anchReset,
         #anchCancel {
background-color: #2a99bd;
color: #ffffff;
cursor: pointer;
display: block;
float: right;
font-size: 15px;
height: 37px;
line-height: 37px;
text-align: center;
text-decoration: none;
width: 140px;
margin-right: 15px;
      }

        #anchCancel {
background-color: #fff;
color: #555;
text-decoration: underline;
width: 100px;

        }

        .enterNewPass {
display: block;
font-size: 13px;
line-height: 22px;
text-align: left;
padding-left: 50px;
        }

            .enterNewPass strong {
                font-size: 14px;
            }

        .enterBlock {
height: 40px;
margin-bottom: 25px;
margin-top: 25px;
width: 560px;

        }

        #txtNewPassword,
        #txtReEnter {
border: 1px solid #ccc;
color: #999;
float: left;
font-family: OpenSans-Regular;
font-size: 17px;
height: 37px;
padding: 2px;
text-transform: capitalize;
width: 335px;
        }

        .spnNewpass {
float: left;
height: 40px;
line-height: 40px;
margin-right: 5px;
text-align: right;
width: 200px;
font-size: 13px;
        }


            #content a:hover {background-color: #1382a6;}
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#LogIn").removeAttr("onclick").attr("onclick", "loginModal('password-recovery')");
        });
    </script>

    <div id="content">
         <div id="passwordContent" />
        <ul id="passwordForm">
            <li><span class="enterNewPass"><strong>Enter your new password.</strong> A lengthy mix of numbers, symbols, and letters of the uppercase, lowcase symbols and numbers variety makes for secure password </span></li>
            <li class="enterBlock">
                <span class="spnNewpass">New Password</span>
                <input type="password" id="txtNewPassword" style="text-transform:none" />
            </li>
            <li class="enterBlock">
                <span class="spnNewpass">Re-enter New Password</span>
                <input type="password" id="txtReEnter" style="text-transform:none" />
            </li>
                 <li><span id="spnMsgPassword"></span></li>
            <li class="anchHolder">
                <a id="anchReset" onclick="ResetPassword('<%=etoken%>')">Reset Password</a>
                <a id="anchCancel" href="/home">Cancel</a>
            </li>
       
        </ul>
       
    </div>
</asp:Content>
