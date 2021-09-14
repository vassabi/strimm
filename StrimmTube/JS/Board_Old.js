var webMethodGetOffset = "../WebServices/ChannelWebService.asmx/GetOffset";
var WebMethodSignOut = "../WebServices/UserService.asmx/SignOut";
var webMethodFollowuser = "../WebServices/UserService.asmx/FollowUser";
var webMethodUnFollowUser = "../WebServices/UserService.asmx/UnFollowUser";
var webMethodPollRings = "../WebServices/ChannelWebService.asmx/PollRings";
var webMethodSendFeedback = "../WebServices/UserService.asmx/SendFeedback";
function GetOffSet() {
    var dt = new Date();
    var tz = dt.getTimezoneOffset();
    return tz;

};
function ScrollerUp() {
    $(".nano").nanoScroller({ alwaysVisible: true });
};

function SignOut() {
    $.ajax({
        type: "POST",
        url: WebMethodSignOut,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
        },
        complete: function () {
            window.location.href = "/home";
        }
    });
};
function ToggleChannels() {
    if ($("#divChannelsCategory").is(":visible")) {
        $("#divChannelsCategory").hide();
        $("#divBrowseChannels a #divImg").text(" ").text("▼");
    }
    else {
        $("#divChannelsCategory").show();
        $("#divBrowseChannels a #divImg").text(" ").text("▲");
    }
};
function ShowFileUploadAvatar() {
    if ($("#uploadAvatarHolder").is(":visible")) {
        $("#uploadAvatarHolder").hide();
        $("#ancEditAvatar").text("").text("edit photo");
    }
    else {
        $("#uploadAvatarHolder").show();
        $("#ancEditAvatar").text("").text("cancel");
    }
};
function ShowEditTitle() {
    if ($("#titleEditHolder").is(":visible")) {
        $("#titleEditHolder, #storyHolderEdit").hide();
        $("#ancEditTitle").text("").text("edit");
        $("#divStory h1").css("display", "block");
        $(".lblStory").show();
    }
    else {
        $("#titleEditHolder, #storyHolderEdit").show();
        $("#ancEditTitle").text("").text("cancel");
        $("#divStory h1").css("display", "none");
        $(".lblStory").hide();
    }
}
function ShowAddNewPost() {
    if ($("#divTodayPost").is(":visible")) {
        $("#divTodayPost").hide();
        $("#ancAddNewPost").text("").text("add new post");
        $("#lblPost").show();
    }
    else {
        $("#divTodayPost").show();
        $("#ancAddNewPost").text("").text("cancel");
        $("#lblPost").hide();
    }
}
function readUrlAvatar() {
    var input = $("#fuImgAvatar")[0];
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("#imgUserAvatar").attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
        $("#imgUserAvatar").attr('src', reader.result);
    }
};
$("#imgUserAvatar").change(function () {
    readUrlAvatar(this);
});
function readURL(input) {
   
    var elementClass = $("#" + input.id).attr("class");
    var elementImg = "img." + elementClass;
   // //console.log(elementImg);
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(elementImg)
            .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
$("#imgPost").change(function () {
    readUrlPost(this);
});
function followUser(userUrl) {
    var offset = GetOffSet();
    $.ajax({
        type: "POST",
        url: webMethodFollowuser,
        data: '{"userUrl":' + "'"+userUrl+"'" + ',"offset":' +  offset +'}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#divContactBtns a#btnFollow").text(" ").text("unfollow").removeAttr("onclick").attr("onclick", "Unfollow('" + userUrl + "')");
            var count = $("#imgLinksFollowings").children();
            var first = count[0];
            var last = count.last();
          
            $(response.d).prependTo("#imgLinksFollowings");
            count = $("#imgLinksFollowings").children();
            if (count.length >= 9) {
                var id = last.attr("id")
                $("#" + id).css("display", "none");
            }
            //$("#imgLinksFollowings").insertBefore( $( "#"+first ) ).append(response.d);
            var followerCount =parseInt( $("#lblFollowingCount").text());
            $("#lblFollowingCount").text("").text(followerCount + 1);
          //  //console.log(count.lenght);
        }
    });
}
function Unfollow(userUrl) {
  //  //console.log(userUrl);
    $.ajax({
        type: "POST",
        url: webMethodUnFollowUser,
        data: '{"userUrl":' + "'" + userUrl + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#anc_" + response.d).css("display", "none");
            var followerCount = parseInt($("#lblFollowingCount").text());
            $("#lblFollowingCount").text("").text(followerCount - 1);
            $("#divContactBtns a#btnFollow").text(" ").text("follow").removeAttr("onclick").attr("onclick", "followUser('" + userUrl + "')");
        }
    });
}
function ShowFollowerModal()
{
    $('#followersModal').lightbox_me({
        centered: true,
        onLoad: function () {
            $(".nano.modal").nanoScroller({ alwaysVisible: true });
        }
    });
}
