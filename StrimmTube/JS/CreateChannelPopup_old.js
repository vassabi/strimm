

//WEBSERVICES
var webMethodCheckChannelNameIsTaken = "/WebServices/ChannelWebService.asmx/CheckChannelNameIsTaken";
var webMethodCheckChannelNameIsReserved = "/WebServices/ChannelWebService.asmx/CheckChannelNameIsReserved";
var webMethodGetChannelCategoriesJsonForCreateChannel = "/WebServices/ChannelWebService.asmx/GetChannelCategories";
var webMethodCreateNewChannel = "/WebServices/ChannelWebService.asmx/CreateNewChannel";
var webMethodUpdateChannelForModal = "/WebServices/ChannelWebService.asmx/UpdateChannelForModal";
var webMethodDeleteChannelForModal = "/WebServices/ChannelWebService.asmx/DeleteChannelForModal";
//END WEBSERVICES


//GLOBAL VARIABLES
var channelName;
var channelUrl;
var channelId;
var isChannelNameTaken;
var isChannelNameReserved;
var b64;
var fileSize;
var fileName;
//END GLOBAL VARIABLES

function ClearAllCreateChannelFields()
{
    var queryString = getQueryStringParameterByName("create-channel")
    void 0;
    if (queryString == 1) {
        var url =  window.location.href.split('?')[0];
        window.location.replace(url);
        void 0;
    }
   
    $("#createChannelModal #txtChannelNameForm").val("");   
    $("#createChannelModal select option[value='0']").attr("selected", "selected");
    $("#createChannelModal #fuChannelAvatar").val("");
    $("#createChannelModal #imgChannelAvatar").removeAttr('src').attr('src', '/images/comingSoonBG.jpg');
    $("#createChannelModal #TextURL").text("").text("Channel URL");
    $("#spnChannelNameError").text("");
    $('#spnChannelLogoErr').hide();
    $("#createChannelModal #txtChannelLogo").removeClass("createChannelError");

    $('#createChannelModal').trigger('close');
  
}

var openFile = function (event) {
   
    var input = event.target;

    var reader = new FileReader();
    reader.onload = function () {

        var dataURL = reader.result;
        $("#createChannelModal #imgChannelAvatar").attr('src', dataURL)
                    .width(70)
                    .height(70);
        b64 = dataURL.split("base64,")[1];

    };
    var f = input.files[0];
    fileSize = f.size || f.fileSize;

    //var size = readImage(input.files[0]);
    reader.readAsDataURL(input.files[0]);

    ValidateImage(input.id,input.files[0].size);
};
//VALIDATIONS

$(document).on('blur', '.createChannelForm #txtChannelNameForm', function () {
    ValidateChannelName();
});

function ValidateChannelName() {

    var $channelNameInput = $(".createChannelForm #txtChannelNameForm").val();
    var $channelNameControl = $(".createChannelForm #txtChannelNameForm");
    isChannelNameTaken = CheckIfChannelNameExist($channelNameInput);
    void 0;
    isChannelNameReserved = CheckIfChannelNameReserved($channelNameInput);
    var isChannelNameValid;
    var regexp = /^[a-z\d\-_\s]+$/i;

    if ($channelNameInput.search(regexp) == -1) {
        isChannelNameValid = false;
    }
    else {

        isChannelNameValid = true;
    }

    var isNameValid = $channelNameInput.match()
    //if empty
    if ($channelNameInput.trim() == "") {
        $channelNameControl.addClass("createChannelError");
        $("#divBoardContent ul li span#spnChannelNameErr").show();
        $("#spnChannelNameError").text("").text("*Please provide channel name");
        $("#btnCreate").removeAttr("onclick");
        return false;
    }
        //if too short
    else if ($channelNameInput.trim().length < 3 || $channelNameInput.trim().length > 20) {
        $channelNameControl.addClass("createChannelError");
        $("#divBoardContent ul li span#spnChannelNameErr").show();
        $("#spnChannelNameError").text("").text("*Channel name must have 3-20 characters only, including spacing");
        $("#btnCreate").removeAttr("onclick");
        return false;

    }
        //if isExist
    else if (isChannelNameTaken) {
        $channelNameControl.addClass("createChannelError");
        $("#divBoardContent ul li span#spnChannelNameErr").show();
        $("#spnChannelNameError").text("").text("*Channel name is not available, please choose another channel name");
        $("#btnCreate").removeAttr("onclick");
        return false;
    }
        //if isreserved
    else if (isChannelNameReserved) {
        $channelNameControl.addClass("createChannelError");
        $("#divBoardContent ul li span#spnChannelNameErr").show();
        $("#spnChannelNameError").text("").text("*This name may be reserved as a premium name for trademark holders. Please contact us with proof of legal rights to this name, if you wish to have it, or choose another name.");
        $("#btnCreate").removeAttr("onclick");
        return false;
    }
        //if has special characters
    else if (!isChannelNameValid) {
        $channelNameControl.addClass("createChannelError");
        $("#divBoardContent ul li span#spnChannelNameErr").show();
        $("#spnChannelNameError").text("").text("*No double spacing and special characters, like &,!,', /,#,? in the channel name");
        $("#btnCreate").removeAttr("onclick");
        return false;
    }
    else {
        $channelNameControl.removeClass("createChannelError");
        $("#divBoardContent ul li span#spnChannelNameErr").hide();
        $("#spnChannelNameError").text("");
        $("#createChannelModal #TextURL").text("").text(domainName+"/"+ userUrl+"/"+ $channelNameInput.replace(/\s/g, ""));       

        if (document.getElementById("btnCreate").hasAttribute("onclick") == false) {
            document.getElementById("btnCreate").setAttribute("onclick", "CreateChannel()");
        }

        return true;
    }

};



function CheckIfChannelNameExist(channelName) {
    var isExist = $.ajax({
        type: "POST",
        url: webMethodCheckChannelNameIsTaken,
        data: '{"channelName":' + "'" + channelName + "'" + '}',
        cashe: false,
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {


        },
        error: function (request, status, error) {
            void 0;
        }
    });
    return isExist.responseJSON.d;
};

function CheckIfChannelNameReserved(channelName) {
    var isExist = $.ajax({
        type: "POST",
        url: webMethodCheckChannelNameIsReserved,
        data: '{"channelName":' + "'" + channelName + "'" + '}',
        cashe: false,
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {

        },
        error: function (request, status, error) {
            void 0;
        }
    });
    return isExist.responseJSON.d;
};

function ValidateImage(fuId, fileSize) {
    //check image size
    var maxUploadSize = 300 * 1024;// max upload size 200kb
    filename = $("#createChannelModal #fuChannelAvatar").val();
    var validExtensions = /(\.jpg|\.jpeg|\.png)$/i;
    if (fileSize != null) {
        if (fileSize > maxUploadSize) {
            $("#createChannelModal #txtChannelLogo").addClass("createChannelError");
            $("#divBoardContent ul li span#spnChannelLogoErr").show();
            $("#spnChannelNameError").text("").text("*Image is too big. Please choose a smaller image. Max image size is 30MB");
            $("#btnCreate").removeAttr("onclick");
            return false;
        }
            //check image extention
        else if (!validExtensions.test(filename)) {
            $("#createChannelModal #txtChannelLogo").addClass("createChannelError");
            $("#divBoardContent ul li span#spnChannelLogoErr").show();
            $("#spnChannelNameError").text("").text("*Image have to be .jpg or .png files only");
            $("#btnCreate").removeAttr("onclick");
            return false;
        }
        else {
            $("#createChannelModal #txtChannelLogo").removeClass("createChannelError");
            $("#divBoardContent ul li span#spnChannelLogoErr").hide();
            $("#spnChannelNameError").text("");

            if (document.getElementById("btnCreate").hasAttribute("onclick") == false) {
                document.getElementById("btnCreate").setAttribute("onclick", "CreateChannel()");
            }

            return true;
        }
    }
    else {
        if (document.getElementById("btnCreate").hasAttribute("onclick") == false) {
            document.getElementById("btnCreate").setAttribute("onclick","CreateChannel()");
        }
        return true;
    }

};


//END VALIDATIONS


//CREATE CHANNEL 
function CreateChannel() {
    var isChannelNameValid = ValidateChannelName();
    var isImageValid = ValidateImage();
    var selectedCategoryValue = $("#createChannelModal select option:selected").val();
    void 0;
    if(selectedCategoryValue=="0")
    {
        $("#createChannelModal #txtChannelCategory").addClass("categoryErr");       
        $("#spnChannelNameError").text("").text("*Please select channel category");
        return;
    }
    if(isChannelNameValid&&isImageValid)
    {
        $("#loadingDiv ").show();
        var categoryId = selectedCategoryValue;
        //var filename = $("#createChannelModal #fuChannelAvatar").val().split("\\").join("/");
        var filename = "";
        var channelName = $(".createChannelForm #txtChannelNameForm").val().replace(/ +/g, " ")
        var channelUrl = channelName.replace(/\s/g, "");
        if (b64 === undefined || b64 == "undefined")
        {
            b64 = "";
        }
        var params = '{"categoryId":' + categoryId + ',"channelName":' + "'" + channelName + "'" + ',"channelUrl":' + "'" + channelUrl + "'" + ',"channelImgData":' + "'" + b64 + "'" + ',"userId":' + userId + ',"fileName":' + "'" + filename + "'" + '}';
        if (isImageValid)
        {
            $.ajax({
                type: "POST",
                url: webMethodCreateNewChannel,
                data: params,
                cashe: false,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response.d == true) {
                        ClearAllCreateChannelFields();
                        window.location.href = "/" + userUrl + "/my-studio/" + channelUrl;

                    }
                    else {
                        $("#spnChannelNameError").text("").text("*Fail create new channel, please try again later");
                    }


                },
                complete: function () {
                    $("#loadingDiv").hide();
                },
                error: function (request, status, error) {
                    void 0;
                }
            });
        }
       
    }
    
    
}
//END CREATE CHANNEL


//UPDATE CHANNEL
function UpdateChannelForModal() {
    void 0;

    var filename = ""; //$("#createChannelModal #fuChannelAvatar").val().split("\\").join("/");
    var selectedCategoryValue = $("#createChannelModal select option:selected").val();
    categoryId = selectedCategoryValue;
    
    if (b64 === undefined || b64 == "undefined") {
        b64 = "";
    }
    //int channelId, string fileName, string imageData, int categoryId, int userId
    var params = '{"channelTubeId":' + channelTubeId + ',"fileName":' + "'" + filename + "'" + ',"imageData":' + "'" + b64 + "'" + ',"channelImgData":' + "'" + b64 + "'" + ',"categoryId":' + selectedCategoryValue + ',"userId":' + userId + '}';
    $.ajax({
        type: "POST",
        url: webMethodUpdateChannelForModal,
        data: params,
        cashe: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $.each(response, function (i, d) {
                if (d.PictureUrl == "") {
                    d.PictureUrl = '/images/comingSoonBG.jpg';
                }
                $("#imgChannelAvatar").removeAttr("src").attr("src", d.PictureUrl);
                $("#lblCategory").text("").text("category: " + d.CategoryName);
                alertify.success("Channel has been successfully updated.");

            })

        },
        complete: function () {


            $('#createChannelModal').trigger('close');
        },
        error: function (request, status, error) {
            void 0;
        }
    });

};
//END UPDATE CHANNEL

//DELETE CHANNEL

function DeleteChannel()
{
    alertify.confirm("If you proceed, the channel and all of its schedules will be deleted. Click 'Ok' to proceed or 'Cancel' to abort", function (e) {
        if (e) {
            var params = '{"channelTubeId":' + channelTubeId + '}';
            $.ajax({
                type: "POST",
                url: webMethodDeleteChannelForModal,
                data: params,
                cashe: false,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    window.location.href = "/" + userUrl;

                },
                complete: function () {


                    $('#createChannelModal').trigger('close');
                },
                error: function (request, status, error) {
                    void 0;
                }
            });
        }
    });
}

//END DELETE CHANNEL

