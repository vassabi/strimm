
var webMethodGetFavoriteChannels = "/WebServices/ChannelWebService.asmx/GetAllFavoriteChannelsForUserByUserIdAndClientTime";
var webMethodUnsuscribeFromChannel = "/WebServices/ChannelWebService.asmx/UnsubscribeUserFromChannel";

function GetFavoriteChannels() {
    var clientTime = getClientTime();
    var favoriteChannel = 0;
    $.ajax({
        type: "POST",
        url: webMethodGetFavoriteChannels,
        data: '{"userId":' + "'" + userId + "'" + ',"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

            favoriteChannel = Controls.BuildFavoriteChannelControl(response, true);
            $("#channelsHolder").html("").html(favoriteChannel);
            if (response.d.length > 0)
            {
                $("#lblChannelCount").text("").text(response.d.length);
            }
            else
            {
                if (userId && userId > 0) {
                    $("#lblMessage").text("You have no favorite channels yet");
                }
                else {
                    $("#lblMessage").text("You have no favorite channels yet.  Sign up or login to save your favorites");
                }

                $("#lblChannelCount").text("0");
            }
            void 0;
        },
        complete: function () {
        },
        error: function () {
        }
    });

};

function RemoveFromFavorite(userId, channelTubeId) {
    if ($("#modalRemove #msgRemove").is(':checked')) {
        setCookie('dontshowmsgfav', 'yes', 30);
    }
    var clientTime = getClientTime();
    $.ajax({
        type: "POST",
        url: webMethodUnsuscribeFromChannel,
        data: '{"userId":' + "'" + userId + "'" + ',"channelTubeId":' + "'" + channelTubeId + "'" + ',"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $(".chanBox#" + channelTubeId).hide();
          
            $('#modalRemove').trigger('close');
        },
        complete: function () {
            GetFavoriteChannels();
        },
        error: function () {
        }
    });
};

function SortFavoriteChannels(select) {
    var channelBoxes = $("#channelsHolder").find("div.chanBox");
    var wrapper = $('#channelsHolder');

    var value;
    var selectedValue = select.value;
    if (selectedValue !== null && selectedValue !== undefined) {
        value = selectedValue;
    }
    else {
        value = 0;
    }

    SortChannels(channelBoxes, value, wrapper);
};

function SortChannels(channelBoxes, select, wrapper) {

    switch (select) {
        case "1":
            var sorted = channelBoxes.sort(function (a, b) {
                var aDateAdded = ((new Date(b.getAttribute('date-added'))).getTime() * 10000) + 621355968000000000;
                var bDateAdded = ((new Date(a.getAttribute('date-added'))).getTime() * 10000) + 621355968000000000;
                var result = -bDateAdded + aDateAdded;
                void 0;
                return result;
            });
            wrapper.empty().append(sorted);
            break;
        case "2":
            var sorted = channelBoxes.sort(function (a, b) {
                var aPlaying = b.getAttribute('data-playing');
                var bPlaying = a.getAttribute('data-playing');
                var result = aPlaying - bPlaying;
                void 0;
                return result;
            });
            wrapper.empty().append(sorted);
            break;
        case "3":
            var sorted = channelBoxes.sort(function (a, b) {
                var aName = b.getAttribute('data-name');
                var bName = a.getAttribute('data-name');
                var result = aName < bName;
                void 0;
                return result;
                
            });
            wrapper.empty().append(sorted);
            break;
        case "4":
            var sorted = channelBoxes.sort(function (a, b) {
                var aName = b.getAttribute('data-name');
                var bName = a.getAttribute('data-name');               
                var result = aName > bName;
                void 0;
                return result;
                
            });
            wrapper.empty().append(sorted);
            break;
    }
};

function ShowModalRemove(userId, channelTubeId)
{
    void 0;
    if (!$.cookie('dontshowmsgfav')) {
        //moved modal stuff in if case
        $('#modalRemove').lightbox_me({
            centered: true,
            onLoad: function () {
                $("#removeOk").attr("onclick","RemoveFromFavorite(userId,"+ channelTubeId+")")
            },
            onClose: function () {
                RemoveOverlay();
            }
        });
    }
    else
    {
        RemoveFromFavorite(userId, channelTubeId);
    }
   
}

function CloseModalRemove()
{
    
    $("#modalRemove").hide();
    $('#modalRemove').trigger('close');
}