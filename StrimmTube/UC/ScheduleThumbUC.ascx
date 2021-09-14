<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScheduleThumbUC.ascx.cs" Inherits="StrimmTube.UC.ScheduleThumbUC" %>
<div class="divSchedule" runat="server" id="scheduleThumb" style="width:100%;">
     <asp:Label runat="server" ID="lblTime" CssClass="spnTime"><%=startDate%>-<%=endDate%></asp:Label>
     <asp:Label runat="server" ID="lblTitle" CssClass="spnTitle"><%=videoTitle%></asp:Label>
    <img src="<%=thumbSrc%>" class="imgScheduleThumb"/>
   
   
   <%-- <asp:Button runat="server" CssClass="btnRemove" Text="remove" ID="btnRemove" />--%>
    <a class="btnRemove" id="btnRemove" onclick="removeVideoSchedule(this,'<%=tempId%>')">&times;</a>
</div>

