<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VRTubeBoxUC.ascx.cs" Inherits="StrimmTube.UC.VRTubeBoxUC" %>

<div class="divBoxContent <%=inSchedule%>" id="<%=boxContentId%>" >  
     <div runat="server" id="divInSchedule" visible="false">
        <span class="spnInScedule">
            in schedule list already
        </span>
    </div> 
     <div class="actions" id="<%=actionId%>">
        <div runat="server" id="addtoVR" visible="false">
       <%-- <a class="addToSchedule" id="<%=addToScheduleId%>" onclick="<%=addToScheduleOrVRFunc%>"><%=addToVideoRoomOrSchedule%></a>--%>
            <input type="checkbox" id="checkVr<%=actionId%>"/>
            </div>
      <%--  <div id="divEdit" runat="server" class="edit">
        <a id="<%=editId%>" onclick="EditVideo(this)">edit or remove</a>
            </div>--%>
        
       <div runat="server" id="divRemoveHolder" visible="false">
           <a id="remove_<%=remove%>" class="remove" onclick="RemoveFromArchive(this)">remove</a>
       </div>
    </div> 
    <div class="divVideoThumb">
         <a class="btnPlay" id="<%=playId%>" onclick="showPlayer(this)">
             <img class="PLAY-ICON" src="/images/PLAY-ICON(!).png" />
        <img class="imgThumb" src="<%=srcImage%>" />
       </a>
    </div>
    <div class="divVideoInformation">
        <span class="title"><%=originalTitle%></span>
        <span class="views">strimm views:&nbsp <%=views%></span>
        <span class="duration">duration:&nbsp <%=duration%></span>
        <span>ID:&nbsp <%=videoId%></span>
    </div>
      <div runat="server" id="removedVideo" visible="false" class="removedVideo">

        <span class="removedMsg">This video is no longer available</span>
       <%-- <a id="<%=remove%>" class="remove" onclick="RemoveVideo(this)">remove</a>--%>
    </div>
    <div runat="server" id="restrictedVideo" visible="false" class="removedVideo">
          <span class="removedMsg">This video is restricted by provider</span>
       <%-- <a id="<%=remove%>" class="remove" onclick="RemoveVideo(this)">remove</a>--%>
    </div>
   
   
   <%-- <div id="customizedDivHolder" runat="server" visible="true">
    <div class="divCustomize">
        <div class="divTitle">
            <span> title</span>            
            <span  id="<%=txtCustomTitle%>" class="originalTitle justTitle"  title="<%=originalTitle%>"><%=originalTitle%></span>
        </div>
        <div class="divDesc">
            <span>description</span>           
            <span  id="<%=txtCustomizeDescription%>" class="originalTitle" title="<%=originalDescription%>"><%=originalDescription%></span>
        </div>
        <div class="divCategory">
            <span>choose category
            </span>
            <select id="<%=selectId%>" class="spnCat">
                <option>choose category</option>
            </select>
        </div>
        
        <input type="checkbox" ID="chkBox" CssClass="chkBox" Text="R-rated" checked="<%=ischeked%>" />
         <a class="ancClose" onclick="HideCustomize(this)" id="<%=closeId%>">&#10162; </a>
        <div class="actions" runat="server">
            <a id="<%=remove%>" class="remove" onclick="RemoveVideo(this)">remove</a>
            <a id="<%=upadte%>" class="update" onclick="UpdateVideo(this)" >update</a>
        </div>
        <div class="error">
            <span class="spnError"></span>
        </div>
    </div>
        </div>--%>
 
   
      
</div>