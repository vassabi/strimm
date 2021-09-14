
var WebMethodGetCurrentlyPlayingChannels = "/WebServices/ChannelWebService.asmx/GetCurrentlyPlayingChannels";
var webMethodGetTopChannelsOnTheAirJson = "/WebServices/ChannelWebService.asmx/GetTopChannelsOnTheAir";
var WebMethodGetCurrentlyPlayingChannelsByCategoryName = "/WebServices/ChannelWebService.asmx/GetCurrentlyPlayingChannelsByCategoryName";
var WebMethodGetCurrentlyPlayingChannelsByCategoryNameAndSelectedLanguage = "/WebServices/ChannelWebService.asmx/GetCurrentlyPlayingChannelsByCategoryNameAndSelectedLanguage";
var webMethodGetChannelLanguages = "/WebServices/ChannelWebService.asmx/GetChannelLanguages";

var allChannels;
var languages = [];

$(document).ready(function () {
    //get query string
    $("select option:first").attr('selected', 'selected');
    GetChannels();
    var opts = {
        lines: 12             // The number of lines to draw
       , length: 7             // The length of each line
       , width: 5              // The line thickness
       , radius: 10            // The radius of the inner circle
       , scale: 1.0            // Scales overall size of the spinner
       , corners: 1            // Roundness (0..1)
       , color: '#000'         // #rgb or #rrggbb
       , opacity: 1          // Opacity of the lines
       , rotate: 0             // Rotation offset
       , direction: 1          // 1: clockwise, -1: counterclockwise
       , speed: 1              // Rounds per second
       , trail: 100            // Afterglow percentage
       , fps: 20               // Frames per second when using setTimeout()
       , zIndex: 2e9           // Use a high z-index by default
       , className: 'spinner'  // CSS class to assign to the element
       , top: '50%'            // center vertically
       , left: '50%'           // center horizontally
       , shadow: false         // Whether to render a shadow
       , hwaccel: false        // Whether to use hardware acceleration (might be buggy)
       , position: 'absolute'  // Element positioning
    };

    var target = document.getElementById('loadingDiv')
    var spinner = new Spinner(opts).spin(target);

});
function GetChannels() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('=');

    var loader = $("#loadingDiv");

    loader.show();

    var categoryName = hashes[1].replace(/%20/g, " ");
    $("select option[value='0']").attr('selected', 'selected');
    var clientTime = getClientTime();
    $.ajax({
        type: "POST",
        url: WebMethodGetCurrentlyPlayingChannelsByCategoryName,
        data: '{"clientTime":' + "'" + clientTime + "'" + ',"categoryName":\'' + categoryName + '\'}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            var currentlyPlayingChannels = Controls.BuildChannelControlForBrowseResultsPage(response.d, true);
            var channelCount = 0;

            if (categoryName != 'All Channels') {
                $("#lblCategoryName").html("").html("'" + categoryName + "'  Channels");
            }
            else {
                $("#lblCategoryName").html("").html("Channel Gallery");
            }
            $(".channelHolder").html("");

            if (currentlyPlayingChannels && currentlyPlayingChannels.length > 0) {
                allChannels = response.d;
                $(".channelHolder").html(currentlyPlayingChannels);
                channelCount = response.d.length;
                PopulateLanguages();
                $("#ddlLang").show();
            }
            else {
                $("#lblmessage").html("Sorry, there are no channels playing at this moment. Please check back again soon.");
            }

            $("#lblChannelCount").html("").html(channelCount);
        },
        complete: function () {
            loader.hide();
        }
    });
}
function GetChannelsBySelectedLanguage(select) {

    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('=');

    var loader = $("#loadingDiv");

    loader.show();

   

    var clientTime = getClientTime();
    var value;
    var selectedValue = select.value;
    var categoryName = hashes[1].replace(/%20/g, " ");
    value = (selectedValue !== null && selectedValue !== undefined) ? selectedValue : 0;
   if(value!=0)
   {
       $.ajax({
           type: "POST",
           url: WebMethodGetCurrentlyPlayingChannelsByCategoryNameAndSelectedLanguage,
           data: '{"clientTime":' + "'" + clientTime + "'" + ',"languageId":' + value + '}',
           dataType: "json",
           contentType: "application/json; charset=utf-8",
           success: function (response) {
               void 0
               if (response.d) {
                   var wrapper = $(".channelHolder");



                   var currentlyPlayingChannels = Controls.BuildChannelControlForBrowseResultsPage(response.d, true);

                   if (currentlyPlayingChannels != '') {
                       $("#lblmessage").html('');
                       $("#lblmessage").hide();
                       wrapper.html('').html(currentlyPlayingChannels);
                   }
                   else {
                       var language = 'English';

                       $.each(languages, function (c, i) {
                           if (i.LanguageId == value) {
                               language = i.Name;
                           }
                       });

                       wrapper.html('');
                       $("#lblmessage").html('Sorry, there are currently no channels in "' + language + '" language playing at this moment. Please check back again soon.');
                       $("#lblmessage").show();
                   }
               }


               else {
                   GetChannels();
               }
           },
           complete: function () {
               loader.hide();
           }
       });
   }
    else
   {
       GetChannels();
   }
   
}
function GetTopChannels() {
    var clientTime = getClientTime();

    ajaxTopChannels = $.ajax({
        type: "POST",
        url: webMethodGetTopChannelsOnTheAirJson,
        data: '{"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            currentlyPlayingChannels = Controls.BuildChannelControlForChannelPage(response, true);
            if (currentlyPlayingChannels.length > 0) {
                $(".channelHolder").html("").html(currentlyPlayingChannels);
                $("h1.titleChannel").html("").html("most popular channels (" + currentlyPlayingChannels.length + ")");

            }
            else {
                $(".channelHolder").html("");
                $("#lblmessage").html("Sorry, there are no channels playing at this moment. Please check back again soon.");
            }
        },
        complete: function () {
        },
        error: function () {
        }
    });

};
function sortChannelBoxes(select) {
    var wrapper = $(".channelHolder");
    var value;
    var selectedValue = select.value;
    if (selectedValue !== null && selectedValue !== undefined) {
        value = selectedValue;
    }
    else {
        value = 0;
    }
    var channelBoxes = $(".channelHolder").find(".chanBox");
    sortChannels(channelBoxes, value, wrapper);
}
function sortChannels(channelBoxes, select, wrapper) {
    void 0
    switch (select) {
        case "3":
            var sorted = channelBoxes.sort(function (a, b) {
                var aDuration = a.getAttribute('data-views');
                var bDuration = b.getAttribute('data-views');
                var result = bDuration - aDuration;
                return result;
            });
            wrapper.empty().append(sorted);
            break;
        case "4":
            var sorted = channelBoxes.sort(function (a, b) {
                var aDuration = a.getAttribute('data-fans');
                var bDuration = b.getAttribute('data-fans');
                var result = bDuration - aDuration;
                void 0;
                return result;
            });
            wrapper.empty().append(sorted);
            break;

        case "5":
            var sorted = channelBoxes.sort(function (a, b) {
                var aDateAdded = ((new Date(a.getAttribute('data-createddate'))).getTime() * 10000) + 621355968000000000;
                var bDateAdded = ((new Date(b.getAttribute('data-createddate'))).getTime() * 10000) + 621355968000000000;
                var result = -bDateAdded + aDateAdded;
                return result;
            });
            wrapper.empty().append(sorted);
            break;
        case "6":
            var sorted = channelBoxes.sort(function (a, b) {
                var aDateAdded = ((new Date(a.getAttribute('data-createddate'))).getTime() * 10000) + 621355968000000000;
                var bDateAdded = ((new Date(b.getAttribute('data-createddate'))).getTime() * 10000) + 621355968000000000;
                var result = bDateAdded - aDateAdded;
                return result;
            });
            wrapper.empty().append(sorted);
            break;
    }
};

function GetSelectedLanguage(select) {
    var wrapper = $(".channelHolder");

    var value;
    var selectedValue = select.value;

    value = (selectedValue !== null && selectedValue !== undefined) ? selectedValue : 0;

    if (value != 0) {
        var selected = [];

        $.each(allChannels, function (c, i) {
            if (i.LanguageId == value) {
                selected.push(i);
            }
        });

        var currentlyPlayingChannels = Controls.BuildChannelControlForBrowseResultsPage(selected, true);

        if (currentlyPlayingChannels != '') {
            $("#lblmessage").html('');
            $("#lblmessage").hide();
            wrapper.html('').html(currentlyPlayingChannels);
        }
        else {
            var language = 'English';

            $.each(languages, function (c, i) {
                if (i.LanguageId == value) {
                    language = i.Name;
                }
            });

            wrapper.html('');
            $("#lblmessage").html('Sorry, there are currently no channels in "' + language + '" language playing at this moment. Please check back again soon.');
            $("#lblmessage").show();
        }
    }
    else {
        GetChannels();
    }
}

function PopulateLanguages() {
    $.ajax({
        type: "POST",
        url: webMethodGetChannelLanguages,
        cashe: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            languages = response.d;
            $("#ddlLang").html("").html("<option value=0 selected>All Languages</option>")
            $.each(response.d, function (key, val) {
                if (key == 11) {
                    $("#ddlLang").append('<option style="border-bottom:1px solid black;" disabled="disabled">&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;&mdash;</option>');
                }
                $("#ddlLang").append($('<option>', { value: val.LanguageId }).text(val.Name));
            })

        },
        error: function (request, status, error) {

        }
    });
}
