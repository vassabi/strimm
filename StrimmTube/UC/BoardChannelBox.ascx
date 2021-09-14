<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoardChannelBox.ascx.cs" Inherits="StrimmTube.UC.BoardChannelBox" %>

<div class="chanBox">
    <img class="channelImg" src="<%=channelImage%>" />
    <div class="actionschanBoxHolder">
        <div class="chanBoxEdit" runat="server" id="chanBoxEdit"><asp:LinkButton runat="server" CssClass="ancEdit ancEditChannel " OnClick="ancEdit_Click"></asp:LinkButton></div>
        <div class="chanBoxPlay"> <a class="boxChannelName" href="<%=channelHref%>"></a></div>
    </div>
    <div class="chanBoxInfo">
        <span class="chanBoxtitle chanBoxtitleAdjust"><%=channelName%></span>
        <span class="chanBoxcateg"><strong><%=channelCategory%></strong></span>
        <span id="lblFans" runat="server" class="chanBoxsub">fans:<strong><%=subscribers %></strong></span>
        <span id="lblViews" runat="server" class="chanBoxviews">views:<strong><%=views%></strong></span>
        <span id="channelTiming" runat="server" class="channelTimingDB">on air</span>
    </div>   
</div>
