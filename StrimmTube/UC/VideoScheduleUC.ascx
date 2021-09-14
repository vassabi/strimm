<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoScheduleUC.ascx.cs" Inherits="StrimmTube.UC.VideoScheduleUC" %>
<div class="videoScheduleBox" id="videoScheduleBox_<%=id%>" >
    
    <span class="title"><%=title%></span>
     <div id="removedVideo" runat="server" visible="false">
         <span style="display:block; float:left; font-size:10px;width:50%;">this video was removed</span>
     </div>
    <span class="time"><%=startTime%>-<%=endTime%></span>
    <a class="ShowDescription" onclick="showDescripttion(<%=id%>)">&#9660;</a>
   <span  id="span_<%=id%>" class="description"><%=description%></span>
</div>
