<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeaturedChannelBoxUC.ascx.cs" Inherits="StrimmTube.UC.FeaturedChannelBoxUC" %>
<div class="channelBox">
    <a href="<%=channelUrl%>">

        <h5>
            <img class="PLAY-ICON"  <img src="/images/PLAY-ICON(!).png" />
              <img class="IMG" src="<%=channelImageSrc%>" />


        </h5>
    </a>

    <div class="featuredChannelInfo">
        <span class="feturedChannelName"><span class="fchannelName"><%=channelName%></span></span>
        <span class="feturedChannelcategory">category:<span class="fchannelcategory"> <%=channelCategory%></span></span>

    </div>

   <%-- <a class="ReadMoreBtn" href="<%=channelUrl%>">go to channel &#8250;&#8250;</a>--%>
</div>
