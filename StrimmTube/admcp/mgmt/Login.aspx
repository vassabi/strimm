<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StrimmTube.admcp.mgmt.Login" %>

<!DOCTYPE html>

<html >
<head runat="server">
    <title>Login to Strimm Admin Panel</title>
    <style type="text/css">
#divLogin {
    width: 400px;
    position: relative;
    margin: 200px auto;
    border: 1px solid #2A99BD;
    
    line-height: 40px;
}

#divLogin input {
    background-color: #2a99bd;
    line-height: 25px;
    width: 100px;
    
    border: 1px solid #2a99bd;
    padding: 3px;
    font-size: 17px;
}


    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divLogin">
    <asp:Login ID="Login1" runat="server" CheckBoxStyle-Font-Size="15px" FailureTextStyle-Font-Size="15px" Font-Size="15px"  LabelStyle-BackColor="White" style="color:#555;" TextBoxStyle-BackColor="ControlLight"
        CreateUserUrl="~/admcp/mgmt/Default.aspx" OnAuthenticate="Login1_Authenticate">

    </asp:Login>
    </div>
    </form>
</body>
</html>
