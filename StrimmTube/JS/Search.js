
var webMethodGetChannelsByKeyword = "/WebServices/SearchWebService.asmx/GetChannelsByKeywords";
var webMethodGetUsersByKeyword = "/WebServices/SearchWebService.asmx/GetUsersByKeywords";
var webMethodGetVideosByKeyword = "/WebServices/SearchWebService.asmx/GetCurrentlyPlayingVideoTubeByKeyword";

var pagename = "";
var params;
$(document).ready(function () {
   // console.log(isBefore)
    $("#chkChannels").attr("checked", "checked");
    $("#txtKeyword").val("");
    var match = document.location.pathname.match(/[^\/]+$/);
    if (match) {
        pagename = document.location.pathname.match(/[^\/]+$/)[0];
    };

   
    params = GetCurrentUrlParams();
    if (params.length != 0 && params != undefined) {
        SetKeywordAndSelectedValue();
        GetSearchResultsByQuery();
        
    }
   
   

});


$(document).keyup(function (event) {

  
    if (event.keyCode == 13) {
        $(".btnSearch").trigger('click');
    };
 
   

});

function CheckIfInputEmptyAdvanced(element) {

    
    //if ($("#txtKeyword").val().length != 0) {
    //    $('.searchClearIcon').show();
    //}
    //else {
    //    $('.searchClearIcon').hide();
    //}

}
function SetKeywordAndSelectedValue()
{
    var selectedValue = params.kew;
    var selectedOption = params.selVal;
    if (selectedValue != undefined) {
        var keywords = selectedValue.replace(/,/g, " ");
       // console.log(keywords);
        if (pagename == "advanced-search") {

            if (isBefore) {
                GetSearchControlBefore();


            }
            else {
                GetSearchControlAfter();
            }
            $("#txtKeyword").val(keywords);
            $("#ddlSearchSelect").val(selectedOption).prop("selected", true);

        }
    }
}

function CheckIfSearchForChannelEmpty()
{
   
       
    if ($("#txtSearchVideoByKeywordForChannel").val().length != 0) {
        $('#btnClearSerachedVideosForChannel').show();
        }
        else {
        $('#btnClearSerachedVideosForChannel').hide();
        }
   
}

function GetDataByRadioChecked() {
    $("#lblMsg").html("");
    if ($("#chkUsers").is(":checked")) {
        GetUsersByKeyword();

    }
    else {
        GetChannelsByKeyword();
    }
    SetPlaceholder();
};

function GetChannelsByKeyword(keywords)
{
    //$("#searchBoxHolder").detach().appendTo('#searchHolderUp');
    //$("#searchBoxHolder").css({ margin: "20px 0 0 10px" });
    //$("#searchBoxHolder").removeClass("searchBoxHolderLoad").addClass("searchBoxHolderActive");

    var clientTime = getClientTime();   
    var arrKeywords = new Array();
    arrKeywords = keywords.split(" ");
    //console.log(arrKeywords)
    $.ajax({
        type: "POST",
        url: webMethodGetChannelsByKeyword,
        cashe: false,
        data: '{"keywords":' + JSON.stringify(arrKeywords) + ',"clientTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#loadingDiv").show();
        },
        success: function (response) {
            $("#loadingDiv").hide();
            if (response.d != "0") {
                var data = response.d;
                void 0;
                if (data) {
                    var channels = data.ChannelTubeModel;
                    void 0;
                    // var pageType = "video-room";
                    var channelControls = Controls.BuildChannelControl(response);
                    //console.log(channelControls);
                    $("#divControlsHolder").empty().html(channelControls);
                   // $("#divControlsHolder").html(channelControls);
                    if(channelControls.length==0)
                    {
                        var htmlMsg = "<span id='lblMsg'>Sorry! No search results found</span>";
                        $("#divControlsHolder").html("").html(htmlMsg);
                    }
                }
               
                

                //startIndex++;
                $("#loadingDiv").hide();
            }
           


        }
    });
};

function clearQuery()
{
    $("#txtKeyword").val("");
    $('.searchClearIcon').hide();
}

function GetUsersByKeyword(keywords) {
    //$("#searchBoxHolder").detach().appendTo('#searchHolderUp');
    //$("#searchBoxHolder").css({ margin: "20px 0 0 10px" });
    //$("#searchBoxHolder").removeClass("searchBoxHolderLoad").addClass("searchBoxHolderActive");

    var arrKeywords = new Array();
    arrKeywords = keywords.split(" ");
   

    $.ajax({
        type: "POST",
        url: webMethodGetUsersByKeyword,
        cashe: false,
        data: '{"keywords":' + JSON.stringify(arrKeywords) + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            $("#loadingDiv").show();
        },
        success: function (response) {
            $("#loadingDiv").hide();
            if (response.d != "0") {
                var data = response.d;
                void 0;
                if (data) {
                    var users = data.UserModel;

                    // var pageType = "video-room";
                    var userControls = Controls.BuildUserControls(response);
                  
                    $("#divControlsHolder").html("").html(userControls);
                    if (userControls.length == 0) {
                        var htmlMsg = "<span id='lblMsg'>Sorry! No search results found</span>";
                        $("#divControlsHolder").html("").html(htmlMsg);
                    }
                }
                else {
                    var htmlMsg = "<span id='lblMsg'>Sorry! No search results found</span>";
                    $("#divControlsHolder").html("").html(htmlMsg);
                }

                //startIndex++;
                $("#loadingDiv").hide();
            }



        }
    });
};

function GetPlayingVideosByKeywords(keywords) {

    var arrKeywords = new Array();
    arrKeywords = keywords.split(" ");
    var clientTime = getClientTime();

    $.ajax({
        type: "POST",
        url: webMethodGetVideosByKeyword,
        cashe: false,
        data: '{"keywords":' + JSON.stringify(arrKeywords) + ',"clientDateAndTime":' + "'" + clientTime + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
          
        },
        success: function (response) {
            void 0

            var videoControls = Controls.BuildVideoBoxControlForSearchPage(response.d);

            $("#divControlsHolder").html("").html(videoControls);
            if (videoControls.length == 0) {
                var htmlMsg = "<span id='lblMsg'>Sorry! No search results found</span>";
                $("#divControlsHolder").html("").html(htmlMsg);
            }
            }



       
    });
};

function SetPlaceholder() {
    $("#lblMsg").text("");
    if ($("#chkUsers").is(":checked")) {
        $("#txtKeyword").val("").removeAttr("placeholder").attr("placeholder", "Enter user's name or public name");
    }
    else {
        $("#txtKeyword").val("").removeAttr("placeholder").attr("placeholder", "Enter channel name");
    }
};

function GetSearchControlAfter() {

    var controlHtml = CreateSearchControl();
    //$("#topLeftHolder").hide();
    //$("#topRightHolder").hide();
    $(".advancedSearch").html("");
    $(".spnHelloHolder, .mobileMenu, .createChannelTopRRight, .divLogo, .absGuide").hide();
    $(".advancedSearch").addClass("activeSearch");
    $(".activeSearch").append(controlHtml);

};

function GetSearchControlBefore()
{
    var controlHtml = CreateSearchControl();

   
    //console.log(controlHtml);
    $(".divLogo").show();
    $("#divBrowseChannelsHolder").hide();
    $("#topRightHolderBL").hide();
    $(".spnAdvSearch").hide();
    $(".AdvSearchImgHN").hide();
    $(".advancedSearch").append(controlHtml);
}

function CloseSearchControl() {
    $(".advancedSearch.activeSearch").html("");
    var controlHtml = "";
   
    controlHtml += '<a class="advanced" title="advanced search" href="#" onclick="GetSearchControlAfter()">';
    controlHtml += '<span class="spnAdvSearch"> search </span>';
    controlHtml += '<div class="AdvSearchImgHN"></div>';
    controlHtml += '</a>';
    $(".spnHelloHolder, .mobileMenu, .createChannelTopRRight").show();
    $(".advancedSearch").removeClass("activeSearch").append(controlHtml);
    $(".divLogo, .absGuide").show();
};

function CreateSearchControl() {
    var controlHtml = "";

    controlHtml += '<div id="searchHolder">';
   
    controlHtml += '<div id="searchBoxHolder" class="searchBoxHolderLoad">';
    controlHtml += '<input  type="text" onkeyup="CheckIfInputEmptyAdvanced(this)" id="txtKeyword" class="txtKeywordMargin" placeholder="Search"/>';
   
    //controlHtml += '<a onclick="clearQuery()">';
    //controlHtml += '<div class="searchClearIcon">X</div>';
    //controlHtml += '</a></div>';
    //controlHtml += '<div class="styled-selectSortSearch mainSearch">';

    //controlHtml += '</div>';
    controlHtml += '<div class="btnSearch" onclick="searchSubmit()"></div>';
    controlHtml += '<select id="ddlSearchSelect" class="mainSelect" onchange="searchSubmit()">';
    controlHtml += '<option class="mainSelectOption" value="0" selected="selected">Please select</option>';
    controlHtml += '<option class="mainSelectOption" value="1">Channels</option>';
    controlHtml += '<option class="mainSelectOption" value="2">Users</option>';
    controlHtml += '<option class="mainSelectOption" value="3">Videos</option>';
    controlHtml += '</select>';
    controlHtml += '<div class="clearSearch" onclick="clearQuery()"></div>';
    //controlHtml += '<a id="ancSearch" onclick="GetDataByRadioChecked()" >search</a>';


    controlHtml += '</div>';

    controlHtml += '</div>';
    controlHtml += '<a id="ancCloseSearchControl" onclick="CloseSearchControl()"></a>';
    return controlHtml;
};



function searchSubmit() {
    var selectedOptionValue = $("#ddlSearchSelect option:selected").val();
    var arrKeywords = new Array();
    var keyword = $("#txtKeyword").val();
    arrKeywords = $("#txtKeyword").val().split(" ");
    //console.log(selectedOptionValue, " ", arrKeywords)
    if (selectedOptionValue == "0") {
        alertify.alert("please select option");
        return;

    }
    else if (arrKeywords.length == 0 || keyword == "") {
        alertify.alert("please enter keyword");
        return;
    }
    else if (selectedOptionValue == "0" || arrKeywords.length == 0) {
        return;
    }
    else {
        if (pagename == "advanced-search") {
            history.pushState({ selVal: selectedOptionValue, kew: arrKeywords }, "search for channels or users", "/advanced-search?selVal=" + selectedOptionValue + "&kew=" + arrKeywords);
            UpdateQuery(selectedOptionValue, arrKeywords);
        }
        else {
            history.pushState({ selVal: selectedOptionValue, kew: arrKeywords }, "search for channels or users", "/advanced-search?selVal=" + selectedOptionValue + "&kew=" + arrKeywords);
            window.location.href = "/advanced-search?selVal=" + selectedOptionValue + "&kew=" + arrKeywords;
        }


    }
};

function CheckIfInputEmpty(element) {


    void 0;

}

function UpdateQuery(selectedOptionValue, arrKeywords) {
  
    params = GetCurrentUrlParams();
    var domainPath = location.host + "/advanced-search?selVal=" + selectedOptionValue + "&kew=" + arrKeywords;
    history.replaceState({ selVal: selectedOptionValue, kew: arrKeywords }, "search for channels or users", "?selVal=" + selectedOptionValue + "&kew=" + arrKeywords);

    history.pushState({ selVal: selectedOptionValue, kew: arrKeywords }, "search for channels or users", "?selVal=" + selectedOptionValue + "&kew=" + arrKeywords);
    GetSearchResultsByQuery()
   
};

function GetSearchResultsByQuery() {
    if (pagename == "advanced-search")
    {
        // params = GetCurrentUrlParams();
        var selectedValue = params.selVal;
        var keywords = params.kew;
       // console.log(keywords);
        // selected value:
        //0 - please select create message 
        //1 - playing channels
        //2 - users
        //3 - playing videos

        if (selectedValue == "0") {
            alertify.alert("Please select search option");
            return;
        }
        else if (keywords.length == 0 || keywords.length == "") {
            alertify.alert("Please enter search keywords");
            return;
        }
        else {
            switch (selectedValue) {
                case "1":
                    //console.log("davai channels");
                    GetChannelsByKeyword(keywords);

                    break;
                case "2":
                    //console.log("davai users");
                    GetUsersByKeyword(keywords);
                    break;
                case "3":
                    //console.log("davai videos");
                    GetPlayingVideosByKeywords(keywords)
                    break;
                default:
                    alertify.alert("please select the search option");
                    return;

            }
        }
    }
   



};

function GetCurrentUrlParams()
{
    var queryDict = {};
    location.search.substr(1).split("&").forEach(function (item) { queryDict[item.split("=")[0]] = item.split("=")[1] });
    return queryDict;

}

//get saved content on popstate
//function processAjaxData(response, urlPath) {
//    document.getElementById("content").innerHTML = response.html;
//    document.title = response.pageTitle;
//    window.history.pushState({ "html": response.html, "pageTitle": response.pageTitle }, "", urlPath);
//}
window.onpopstate = function (e) {
    if (e.state) {
        //document.getElementById("content").innerHTML = e.state.html;
        //document.title = e.state.pageTitle;
        //console.log(e.state)
        params = GetCurrentUrlParams();
        //console.log(params);
        GetSearchResultsByQuery();
    }
};


