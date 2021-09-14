<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearcheChannelUC.ascx.cs" Inherits="StrimmTube.UC.SearcheChannelUC" %>
<div class="divChannel">
    <img  src="<%=channelImage%>"/>

    <div class="channelViewsHolder">
                        <a href="<%=channelHref%>"><%=channelName%></a>
                        
                       
                        <span>views:<em><%=views%> </em></span>
                      </div> 
                    </div>