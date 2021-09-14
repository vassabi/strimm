<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BackEndMenu.ascx.cs" Inherits="StrimmTube.UC.BackEndMenu" %>

<style type="text/css">
    ul li {
        display: inline;
    }

    #ancOK.close {
        background-color: #2A99BD;
        background-image: url("/images/ButtonBG.jpg");
        background-repeat: repeat-x;
        border-color: #55AAC5 #2A99BD #2A99BD #55AAC5;
        border-radius: 3px;
        border-style: solid;
        border-width: 1px;
        color: #FFFFFF;
        cursor: pointer;
        
        border: none;
        padding: 5px;
        padding-bottom: 3px;
        padding-top: 3px;
        background-position: -10px center;
        font-size: 13px;
    }

        #ancOK.close:hover {
            background-image: url("/images/ButtonBGhover.jpg");
            color: #fff;
        }

    #divMenuHolder ul li a.goToChannel {
        color: #ccc;
        cursor: pointer;
        font-size: 13px;
        padding-left: 5px;
        padding-right: 5px;
    }

        #divMenuHolder ul li a.goToChannel:hover {
            color: #fff;
        }

    .buttonNewChannel {display: inline;}

      #ancHowTo {
    
            display: block;
            float: right;
          
            text-decoration:underline;
            margin-left:20px;
        }

  
</style>
<script src="../Plugins/popup/jquery.lightbox_me.js"></script>
<script type="text/javascript">
    var channelUrl;
    $(document).ready(function () {
        <%-- var isChannelPicked = '<%=channelPickedId%>';
        if (isChannelPicked == 0) {
            $('.modalDDl').lightbox_me({
                centered: true,
                onLoad: function () {

                }
            });
        }--%>
        channelUrl = '<%=channelUrl%>';
        //console.log(channelUrl);
        var is10Channels = '<%=is10Channels%>';
        var isZeroChannels = '<%=isZeroChannels%>'
        if (is10Channels == "True" || isZeroChannels == "True") {
            $(".backBtn#createChannelBtn").removeAttr("onclick").addClass("disabled");
        }

    });
    function GoToChannel() {
        if (channelUrl == "") {
            alert("please pick the channel");
        }
        else {
            window.open(channelUrl, '_self');
        }
    };

    function SetChannel(element) {

        // alert("index: " + $("#"+element.id+" option:selected").index() + " value: " + $("#"+element.id).val());
        var selectedIndex = $("#" + element.id + " option:selected").index();
        var selectedValue = $("#" + element.id).val();
        var url = window.location.pathname;
        $.ajax({
            type: "POST",
            url: "WebServices/ChannelWebService.asmx/SetChannelSession",
            data: '{"selectedIndex":' + "'" + selectedIndex + "'" + ',"selectedValue":' + "'" + selectedValue + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {

            },
            error: function (response) {
                //console.log(response.status + " " + response.statusText);
            },
            complete: function () {
                window.location.href = url;
            }
        });

    }

    //function ShowModal() {
    //    $('.modalDDl').lightbox_me({
    //        centered: true,
    //        onLoad: function () {

    //        }
    //    });
    //}

</script>
<%--<div id="divMenuHolder">--%>
<div class="divMainButtonHolder">
    <ul >

        <li><a class="createChannel step1" href="create-channel" title="Channel profile creation and edit">
            <span class="step">step 1</span>
            <span class="stepDescription">Create/Update Channel Info</span></a></li>

        <li><a class="createSchedule step2" href="schedule" title="Channel profile creation and edit">
            <span class="step">step 2</span>
            <span class="stepDescription"> Create Schedule for Channel</span></a></li>

        <%--  <li><a class="schedule" href="schedule" title="Broadcast scheduling">--%>

        <li><a class="search managementTools addVideos" href="add-videos" title="Get content to broadcast">
            <span class="managementToolsTitle ">add videos</span>
            <span class="managementToolsDescription ">add videos to personal library</span>
            </a></li>
        <li><a class="videoRoom managementTools videoRoom" href="video-room?id=<%=vrId%>" title="Personal video library">
            <span class="managementToolsTitle ">video room</span>
            <span class="managementToolsDescription">personal video library</span>
            </a></li>
        <li><a class="timetables managementTools" href="timetable" title="Calendar of created broadcast schedules">
            <span class="managementToolsTitle">Calendars & Schedules</span>
            <span class="managementToolsDescription">view, edit & repeat schedules</span>
            </a></li>
       
            
        
        <%--  <li><a class="goToChannel" onclick="GoToChannel()"  title="Visit a channel as a viewer">preview channel </a></li>        
        <li><a class="guides" href="guides" title="Explanation of major features">how to</a></li>--%>
    </ul>


    
</div>

 <div id="btnWatchVideoHolder"><a id="ancHowTo" onclick="showPlayer()">watch video</a></div>




<div class="channelSelector"> 
   <h6 class="titleH6">select channel </h6>

    <div class="divSelectCustom">

     <asp:DropDownList ID="ddlChannels" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlChannels_SelectedIndexChanged" ClientIDMode="Static" CssClass="SelectCustom"></asp:DropDownList>

    </div> 
    <a class="grayButton" onclick="GoToChannel()"  title="Visit a channel as a viewer">veiw channel</a>
    <div class="buttonNewChannel">
    <asp:Button runat="server" ClientIDMode="Static" OnClick="btnCreateNew_Click" ID="btnCreateNew"   Text="create new channel" CssClass="blueButton"/></div>
</div>




<%--</div>--%>


<div id="modalDDL" runat="server" class="modalDDl">
    <a id="close_x" class="close close_x" href="#"></a>
    <div class="chooseChannelwrapper">
        <span id="spnCooseChannel">Please choose channel</span>

        <asp:DropDownList ID="ddlModal" ClientIDMode="Static" runat="server" onchange="SetChannel(this)" CssClass="selectModal"></asp:DropDownList>
        <a id="ancOK" class="close">ok</a>
    </div>
</div>
