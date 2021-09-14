<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublicLibBox.ascx.cs" Inherits="StrimmTube.UC.PublicLibBox" %>
<script type="text/javascript">
    var isrestricted = '<%=isRestricted%>';
    var isInVR = '<%=isInVr%>';
   // console.log("isrestricted" + isrestricted);
    if (isrestricted == "True") {

        $(".divBoxContent#<%=boxContentId%> .divCustomize").empty();
        $(".divBoxContent#<%=boxContentId%> .actions a#<%=addId%>").remove();
        $(".divBoxContent#<%=boxContentId%> .actions .spnError").show();
    }
    if(isInVR=="True")
    {
        $(".actions a#<%=addId%>").remove();
        $(".spnInScedule#spnInScedule_<%=addId%>").show();
    }

</script>
<div class="channelBox <%=side%>" id="<%=boxContentId%>">
     <div class="actions " id="<%=actionId%>">
        <input id="<%=addId%>" type="checkbox" onchange="AddRemoveSelectedVideoToArrayPL(this)"/>
        <span id="spnSelect<%=addId%>">select</span>
           <%--<a onclick="AddToVRFromPublic(this)" id="<%=addId%>" class="addTo">add</a>--%>
         <span class="spnError" >This video is restricted by provider</span>
    </div>

 
         <a class="btnPlay" id="<%=playId%>" onclick="showPlayer(this)">
              <img class="PLAY-ICON"  <img src="/images/PLAY-ICON(!).png" />
        <img class="IMG" src="<%=srcImage%>" />
       </a>
 
    <div class="divVideoInformation">
        <span class="title" title="kuyrkfutyv<%=originalTitle%>"><%=originalTitle%></span>
        <span class="views">views:&nbsp<%=views%></span>
        <span class="duration">duration:&nbsp<%=duration%></span>
        <input type="hidden" value="<%=doubleDuration%>" class="durationHidden"/>
       <%-- <span class="category">category:&nbsp<%=category%></span>--%>
        <input type="hidden" value="<%=hiddenCatVal%>" class ="hidden"/>
         
    </div>
    <span class="spnInScedule" id="spnInScedule_<%=addId%>" style="display: none;">in video room already</span>
  
</div>
