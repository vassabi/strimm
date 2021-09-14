<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardProfileUC.ascx.cs" Inherits="StrimmTube.UC.DashboardProfileUC" %>

<div id="dashboardProfileHolder">
    <ul>
        <li> <asp:TextBox runat="server" ID="txtTitle" placeholder="title (3-15 characters)"></asp:TextBox></li>
        <li><asp:FileUpload runat="server" ID="fuAvatar" placeholder="logo or avatar (1 mb max., .jpeg or .jpg)" /></li>
        <li><asp:FileUpload runat="server" ID="fuBackground" placeholder="backgroung image (size: xxxx)" /> </li>
        <li><asp:TextBox TextMode="MultiLine" Rows="9" Columns="45" runat="server" ID="txtBio" placeholder="bio or main story (250 characters max)"></asp:TextBox> </li>
        <li><asp:Button runat="server" ID="btnSave" Text="save" OnClick="btnSave_Click" /></li>
    </ul>
</div>