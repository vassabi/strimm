<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchedVideosUC.ascx.cs" Inherits="StrimmTube.UC.SearchedVideosUC" %>

   
<script type="text/javascript">
   
    var timeToShow;
    function startTicking() {
        if (timeToShow != 0) {
            var minus = timeToShow - 1;
            $("#spnStartTime<%=videoScheduleId%>_<%=channelId%>").html("");
           $("#spnStartTime<%=videoScheduleId%>_<%=channelId%>").html("start in: " + timeToShow + " min");
            timeToShow = minus; 
        }
        else {
            $("#spnStartTime<%=videoScheduleId%>_<%=channelId%>").html("this video started");
        }
        //console.log(timeToShow);
    };
    $(document).ready(function () {
        var startTime = new Date('<%=startTime%>');
        var clientTime = new Date("<%=clientTime%>");
        //console.log(startTime+ "start time,"+clientTime+"- client time");
        var milliseconds = startTime - clientTime;
        var diff = (milliseconds / 1000);
        //console.log(diff / 60);
        timeToShow = (diff / 60).toFixed(0);
        //console.log(timeToShow+":timetoshow");
        $(".desc<%=videoUploadId%>_<%=channelId%>").mouseover(function () {
            $(".allDesc<%=videoUploadId%>_<%=channelId%>").show();
        });
        $(".desc<%=videoUploadId%>_<%=channelId%>").mouseleave(function () {
            $(".allDesc<%=videoUploadId%>_<%=channelId%>").hide();
        });
        if (timeToShow > 0) {
            $("#spnStartTime<%=videoScheduleId%>_<%=channelId%> .time").html("start in: " + timeToShow + " min");
        }
        else {
            $("#spnStartTime<%=videoScheduleId%>_<%=channelId%> .time").html("video is playing now");
        }
    });

</script>

<div class="videoBox">
    
            
            <a title="<%=videoTitle%>" href="<%=channelurl%>">
                <img class="videoImg" src="<%=imgUrl%>" />                           
                   
            </a>
    <div class="left">
            <span class="spnVideoTitle" title="<%=videoTitle%>"><%=videoTitle%></span>
        
    <div class="DurationViewsHolder">
    <span class="spnDuration"> duration: <%=duration%> min</span>
                    <span class="spnViews">total views: <%=views%> </span>
        </div>
        </div>
    <div class="right">                    
                    
            <a href="<%=channelurl%>" class="channelname"><%=channelName%></a>
            <span class="spnStartTime" id="spnStartTime<%=videoScheduleId%>_<%=channelId%>"><span class="time"></span></span>
      
</div>
    </div>

