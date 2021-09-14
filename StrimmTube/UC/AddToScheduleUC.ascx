<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddToScheduleUC.ascx.cs" Inherits="StrimmTube.UC.AddToScheduleUC" %>
<div class="addToScheduleContainer" id="<%=containerId%>">
    <a class="delete" id="delete_<%=schduleListId%>" onclick="DeleteSchduleList(this)"></a>
    <div class="thumbnail">
        <a class="anchPlay" id="<%=playId%>" onclick="showPlayer(this)" >
        <img class="addToScheduleImg" src="<%=thumbnailSrc%>" />
            </a>
    </div>
    <div class="divInfo">
        <span class="title" title="<%=title%>"><%=title%></span>
          <span class="views">strimm views:&nbsp<%=views%></span>
        <span class="duration">duration:&nbsp<%=showDuration%></span>
      <span class="rated"><%=r_rated%></span>
    </div>
    <div class="actions">
        <div runat="server" id="addVideo" visible="true"> 
        <a class="add" id="<%=addToScheduleId%>" onclick="addVideoScedule(this, '<%=startTime%>','<%=endTime%>', 'monthDate')">Add</a>
            </div>
        <div runat="server" id="removedDiv" visible="false">
            <span class="removedMsg">
                This video was removed by provider
            </span>
           
        </div>
        <div runat="server" id="restrictedDiv" visible="false">
               <span class="removedMsg">
                This video is restricted by provider
            </span>
        </div>
    </div>
</div>
 