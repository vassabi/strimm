<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LandingPageChannelBox.ascx.cs" Inherits="StrimmTube.UC.LandingPageChannelBox" %>

<div id="channelBox" runat="server" class="home-video-box-01">
    <div class="chanBox">
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