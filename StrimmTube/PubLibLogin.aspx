<%@ Page Title="" Language="C#" MasterPageFile="~/Strimm.Master" AutoEventWireup="true" CodeBehind="PubLibLogin.aspx.cs" Inherits="StrimmTube.PubLibLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titleHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="metaHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <style>
       #loginBox {
  display: block;
  height: 251px;
  margin: 50px auto;
  width: 300px;
}
       #ancLogin {
  
  line-height: 18px;
 height:25px;
}
       #divTopNav
       {
           display:none;
       }
    </style>
    <script>
        $(document).ready(function () {
            $("#divTop #loginBox").hide();
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
          <div id="loginBox" class="publib" >
         <div id="divLoginLeft">
                <h3 id="see_id" class="sprited" >log in to public library</h3>
                 <div id="loginForm">
                   <label>username:  <asp:TextBox Id="txtUserName" runat="server" autocapitalize="none"  type="email" autocorrect="off"></asp:TextBox></label>
                      <label> password <asp:TextBox runat="server" TextMode="Password" Id="txtPassword" autocorrect="off" autocapitalize="none"></asp:TextBox></label>
                    <%--<asp:TextBox runat="server" ID="txtUsername" ClientIDMode="Static" ></asp:TextBox>
             <asp:TextBox runat="server" ID="txtPassword"  ClientIDMode="Static"></asp:TextBox>--%>
                    <div id="actions">
                        <asp:Button runat="server" ID="ancLogin" OnClick="ancLogin_Click" Text="Log in" ClientIDMode="Static" />    
                        <asp:Label ID="spanMessage" runat="server" ClientIDMode="Static"></asp:Label>                 
                      </div>
                    </div>
        </div>
 
    </div>
</asp:Content>
