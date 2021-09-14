<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RemoveUC.ascx.cs" Inherits="StrimmTube.UC.RemoveUC" %>
<script>
    function showAlert()
    {
        var r = confirm("Restricted/Removed videos will be permanently deleted from both locations: Schedule list and Video Room");
        if (r == true) {
          
        } else {
            return;
        }
    }
</script>

<asp:Button runat="server" ID="removeRestrictedVideos" Text="Delete restricted & removed videos" CssClass="removeAll" OnClientClick="javascript:showAlert()" OnClick="removeRestrictedVideos_Click"/>

