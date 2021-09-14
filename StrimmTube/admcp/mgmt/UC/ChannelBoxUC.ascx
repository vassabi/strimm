<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChannelBoxUC.ascx.cs" Inherits="StrimmTube.admcp.mgmt.UC.ChannelBoxUC" %>
 <div id="channelContentHolder_<%=channelId%>" class="contentHolder">
            <a href="<%=channelUrl%>" class="channelNameAnc"><%=channelName%></a>
            <input type="radio" id="checkBox_<%=channelId%>" class="chkChannel" name="checkDelete" value="<%=channelId%>" />
        </div>
