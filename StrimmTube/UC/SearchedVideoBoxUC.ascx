<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchedVideoBoxUC.ascx.cs" Inherits="StrimmTube.UC.SearchedVideoBoxUC" %>
<script type="text/javascript">
    var isrestricted = '<%=isRestricted%>';
    var isInVR = '<%=isInVr%>';
   // console.log("isrestricted"+isrestricted);
    if (isrestricted == "True")
    {
       
       // $(".divBoxContent#<%=boxContentId%> .divCustomize").empty();
        $(".divBoxContent#<%=boxContentId%> .actions input#<%=addId%>, #spnSelect<%=addId%>").remove();
        $(".divBoxContent#<%=boxContentId%> .actions .spnError").show();
    }
    if(isInVR=="True")
    {
        $("#<%=boxContentId%>  .actions input").css("display", "none");
        $("#<%=boxContentId%> .spnError").show().text("").text("video already added");
    }
   
</script>
<div class="divBoxContent <%=side%>" id="<%=boxContentId%>" data-duration="<%=durInt%>" data-views="<%=viewsInt%>" >
     <div class="actions " id="<%=actionId%>">
        <%--   <a onclick="ToggleVideoInfo(this)" id="<%=addId%>">select</a>--%>
        <input id="<%=addId%>" type="checkbox" onchange="AddRemoveSelectedVideoToArray(this)"/>
       <%-- <span id="spnSelect<%=addId%>">select</span>--%>
         <span class="spnError" >This video is restricted by provider</span>
    </div>
    <a class="ancClose" id="close_<%=closeId%>" title="close" onclick="RemoveVideoBox(this)">remove</a>
    <div class="divVideoThumb">
         <a class="btnPlay" id="<%=playId%>" onclick="showPlayer(this)">
              <img class="PLAY-ICON"  <img src="/images/PLAY-ICON(!).png" />
        <img class="imgThumb" src="<%=srcImage%>" />
       </a>
    </div>
    <div class="divVideoInformation">
        <span class="title" title="<%=originalTitle%>"><%=originalTitle%></span>
        <span class="views">views:&nbsp<%=views%></span>
        <span class="duration">duration:&nbsp<%=duration%></span>
        <input type="hidden" value="<%=doubleDuration%>" class="durationHidden"/>
    </div>
   
 <%--   <div class="divCustomize" runat="server" id="divCustomize">
        <div class="divTitle">
            <span>title:</span>
          
            <input type="text" id="<%=txtCustomTitle%>" maxlength="35"  placeholder="35 characters" class="customizeTitle"/>
            <span id="<%=txtCustomTitle%>" title="<%=originalTitle%>" class="originTitle" ><%=originalTitle%></span>
        </div>
        <div class="divDesc">
            <span>description:</span>
         <span   id="<%=txtCustomizeDescription%>" title="<%=originalDescriptiom%>" class="originDesc"><%=originalDescriptiom%></span>
            <input type="text" id="<%=txtCustomizeDescription%>" maxlength="120" placeholder="120 characters" class="makeDescription" />
        </div>
        <div class="divCategory">
            <span>choose category
            </span>
            <select id="<%=selectId%>" class="spnCat">
                <option>category</option>
            </select>
        </div>

         <a class="ancClose" onclick="HideCustomize(this)" title="go back" id="customClose_<%=closeId%>">&#10162; </a>
        
        <div class="actions">
            <a id="<%=addToSchedule%>" onclick="addToSchedule(this)" class="addToSchedule" title="add to schedule"></a>
            <a id="<%=addToVr%>" class="addToVr" onclick="addToVideoTube(this)" title="add to Video Room"> add to VR</a>
        </div>
        <div class="error">
            <span class="spnError"></span>
        </div>
        <div class="rated">
        <input type="checkbox" class="chkBox" /><span class="checkBoxValue">R-rated (Adult or violence content)</span>
            </div>
    </div>--%>
</div>
