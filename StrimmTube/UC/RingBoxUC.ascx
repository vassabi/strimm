<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RingBoxUC.ascx.cs" Inherits="StrimmTube.UC.RingBoxUC" %>

<div class="ringBoxChannel ringBoxBoard" id="<%=ringId%>">
    <a href="<%=channelImage%>" class="ancimgchannel">   <img class="channelImg" src="<%=channelImage%>" /></a>
<a class ="senderUrl" href="../board/<%=userNameRingSender%>"> <span class="spnUserName"><%=userNameRingSender%></span>
        <div class="timeAndUserName">
                        <span class="spnTime"><%=dateTimeOfRing%></span>
                       
                        </div>
    </a>

 <a href="<%=channelUrl%>" class="ancToChannel">
                          <div class="imgAndCannel">
                    <span class="spnChannelName"><%=channelName%></span>
                            </div>
                    
   
                        </a>
    </div>