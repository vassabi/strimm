<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmbeddedPageChannelBox.ascx.cs" Inherits="StrimmTube.UC.EmbeddedPageChannelBox" %>

<div id="channelBoxEmbedded" runat="server" class="home-video-box-01">
    <div class="chanBoxEmbedded">
        <a class="btnPlay" target="_self" href="<%=channelHref%>">
            <img class="channelImg" src="<%=channelImage%>">
           <img class="PLAY-ICON" src="/images/PLAY-ICON(!).png">
            <div class="chanBoxInfo">
                <span class="chanBoxtitle"><%=channelName%></span>
                <span id="channelTiming" runat="server" class="channelTiming">on air</span>
            </div>
        </a>
    </div>
</div>

<a id="seeMoreLink" runat="server" href="/browse-channel?category=all channels"><div class="home-video-box-add"></div>

</a>