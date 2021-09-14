var IsPasswordStrongService = "/WebServices/UserService.asmx/IsPasswordStrong";
var IsCurrentPasswordMatch = "/WebServices/UserService.asmx/IsCurrentPasswordMatch";
var IsEmailValidService = "/WebServices/UserService.asmx/IsEmailValid";
var webMethodChangeUserPassword = "/WebServices/UserService.asmx/UpdateUserPassword";
var windowWidth;
$(document).ready(function () {

    $(function () {
        $("#txtBirthday").datepicker({
            changeMonth: true,
            changeYear: true,
            yearRange: "-100",
            reverseYearRange: false,
            dateFormat: 'dd/mm/yy'
        });
    });
    windowWidth = window.screen.width < window.outerWidth ?
                window.screen.width : window.outerWidth;

    //char limits
    function limits(obj, limit, lbl) {
        var text = $(obj).val();
        var length = text.length;
        if (length > limit) {
            $(obj).val(text.substr(0, limit));
        } else { // alert the user of the remaining char.
            $(lbl).text(limit - length);
        }
    };

    $(document).on('blur', '#txtFirstName', function () {
        var firstName = $("#txtFirstName").val();
        if (firstName.trim().length == 0 || !validateText(firstName)) {
            $("#erFirstName.divError span").show();
            $("#divSignUp li input#txtFirstName").addClass("errorActive");
        }
        else {
            $("#erFirstName.divError span").hide();
            $("#divSignUp li input#txtFirstName").removeClass("errorActive");
        }
    });

    $(document).on('blur', '#txtlastName', function () {

        var lastName = $("#txtlastName").val();
        if (lastName.trim().length == 0 || !validateText(lastName)) {
            $(".divError#erLastName span").show();
            $("#divSignUp li input#txtLastName").addClass("errorActive");
        }
        else {
            $(".divError#erLastName span").hide();
            $("#divSignUp li input#txtLastName").removeClass("errorActive");
        }
    });

    $(document).on('blur', '#txtEmail', function () {
        var email = $("#txtEmail").val();
        var isProperEmail = validateEmail(email);

        if (isProperEmail) {
            var isValid = IsEmailValid(email);
            if (isValid) {
                $(".divError#erEmail span").hide();
                $("#divSignUp li input#txtEmail").removeClass("errorActive");
            }
            else {
                $(".divError#erEmail span").text("").text("Invalid e-mail address entered.").show();
                $("#divSignUp li input#txtEmail").addClass("errorActive");
            }
        }
        else {
            void 0;
            $(".divError#erEmail span").text("").text("Invalid e-mail address entered.").show();
            $("#divSignUp li input#txtEmail").addClass("errorActive");
        }
    });

    //show/hide change password
    $("#btnChangePass").click(function () {
        $("#divChangePassword").animate({ opacity: 1.0 }, 200).slideToggle(0, function () {
            // $("#btnChangePass").text($("#divChangePassword").is(':visible') ? "Hide" : "Show");
            //if ($("#divChangePassword").is(':visible')) {
            //    $("#btnChangePass").val("").val("cancel change");
            //}
            //else {
            //    $("#btnChangePass").val("").val("change password");
            //}
        });
    });

    $(document).on('blur', '#txtCurPass', function () {
        ValidateCurrentPassword();
    });

    function ValidateCurrentPassword()
    {
        var currPassword = $("#txtCurPass").val();
        var isCurrPassmatch = IsCurrPassMatch(currPassword);

        if (!isCurrPassmatch) {

            $(".divError#erCurrPassword span").show();
            $(".divError#erCurrPassword span").text("").text("Specified password is invalid.");
            $("#txtCurPass").addClass("errorActive");
            return false;
        }
        else {
            $(".divError#erCurrPassword span").hide();
            $(".divError#erCurrPassword span").text("");
            $("#txtCurPass").removeClass("errorActive");
            return true;
        }
    }

    function IsCurrPassMatch(currPass) {
        var isMatch;
        $.ajax({
            type: "POST",
            url: IsCurrentPasswordMatch,
            data: '{"userId":' + "'" + userId + "'" + ',"currentPassword":' + "'" + currPass + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.d) {
                    isMatch = response.d;
                }
            },
            async: false,
            error: function (response) {
                //console.log(response.status + " " + response.statusText);
            }
        });
        return isMatch;
    };

    $(document).on('blur', '#txtNewPass', function () {
        ValidateNewPassword();
    });
    
    function ValidateNewPassword()
    {
        var password = $("#txtNewPass").val();
        var passwordIsStrong = IsPasswordStrong(password);

        if (password.trim().length < 8 || !passwordIsStrong) {
            //  console.log("less8 ")
            $(".divError#erNewPassword span").show();
            $(".divError#erNewPassword span").text("").text("Password must have 8-12 characters with no spaces and can not contain any special characters.");
            $("#txtNewPass").addClass("errorActive");
            return false;
        }
        else {
            $(".divError#erNewPassword span").hide();
            $(".divError#erNewPassword span").text("");
            $("#txtNewPass").removeClass("errorActive");
            return true;
        }
    }

    function IsPasswordStrong(password) {
        var isStrong;
        $.ajax({
            type: "POST",
            url: IsPasswordStrongService,
            data: '{"password":' + "'" + password + "'" + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.d) {
                    isStrong = response.d;
                }
            },
            async: false,
            error: function (response) {
                //console.log(response.status + " " + response.statusText);
            }
        });
        return isStrong;
    }

    $(document).on('blur', '#txtReEnterPass', function () {
        ValidateReenteredpassword();
    });

    function ValidateReenteredpassword()
    {
        var newPassword = $("#txtNewPass").val();
        var reEnterPassword = $("#txtReEnterPass").val();

        if (newPassword != reEnterPassword){

            $(".divError#erReEnterPassword span").show();
            $(".divError#erReEnterPassword span").text("").text("Re-typed password does not match the new password.");
            $("#txtReEnterPass").addClass("errorActive");
            return false;
        }
        else {
            $(".divError#erReEnterPassword span").hide();
            $(".divError#erReEnterPassword span").text("");
            $("#txtReEnterPass").removeClass("errorActive");
            return true;
        }
    }

    //change password///kesem!!
    $("#btnPassSubmit").click(function () {
        var passToChange = $("#txtCurPass").val();
        var newPassword = $("#txtNewPass").val();
        var newPasswordLength = newPassword.length;
        var reEnterPassword = $("#txtReEnterPass").val();
        var isAllFieldsFilled = false;
        var isCurrValid = ValidateCurrentPassword();
        var isNewPassValid = ValidateNewPassword();
        var isReenter = ValidateReenteredpassword();
       // var isAgeValid = checkForValidDate();
        var curPass = passToChange;
        var clientTime = getClientTime();

        if (curPass.length > 0 && passToChange.length > 0 && reEnterPassword.length > 0) {
            isAllFieldsFilled = true;
        }

        if (isAllFieldsFilled) {
            
            if (IsCurrentPasswordMatch && IsCurrPassMatch && isReenter)
            {
                var params = '{"userId":' + userId + ',"clientDateTime":' + "'" + clientTime + "'" + ',"newPassword":' + "'" + newPassword + "'"
                     + ',"oldPassword":' + "'" + passToChange + "'" + ',"currPass":' + "'" + curPass + "'" + ',"email":' + "'" + email + "'" + '}';
                $.ajax({
                    type: "POST",
                    url: webMethodChangeUserPassword,
                    data: params,
                    cashe: false,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response.d) {
                            if (response.d.indexOf("Your password") == 0) {
                                alertify.success(response.d);
                                $("#divChangePassword").hide();
                            }
                            else {
                                alertify.error(response.d);
                            }
                        }
                        $("#txtCurPass").val("");
                        $("#txtNewPass").val("");
                        $("#txtReEnterPass").val("");
                    }
                });
            }
        }
        else {
            alertify.error("Please complete all required fields.");
        }
    });
           
});

function ShowChangePassword() {
    $('#divChangePassword').lightbox_me({
        centered: true,
        onLoad: function () {
        },
        onClose: function () {
            RemoveOverlay();
        }
    });
};

function CloseChangePassword() {
    $("#txtCurPass, #txtNewPass, #txtReEnterPass").val("");
    $("#btnChangePass").val("").val("change password");
    $("#divChangePassword").hide();
    $('#divChangePassword').trigger('close');

};

function ValidateRequired() {

    var firstName = $("#txtFirstName").val();
    var lastName = $("#txtlastName").val();
    var email = $("#txtEmail").val();
    var country = $("#ddlCountry");
    var countryValue = $("#ddlCountry :selected").val();
    var stateValue = $("#ddlSates :selected").val();
    var isAgeValid = checkForValidDate();
    void 0
   
    if (firstName.trim().length == 0) {
        $("#erFirstName.divError span").show();
        $("#divProfile li input#txtFirstName").addClass("errorActive");
        return false;
    }

    if (lastName.trim().length == 0) {
        $(".divError#erLastName span").show();
        $("#divProfile li input#txtlastName").addClass("errorActive");
        return false;
    }
  
    else if (email.trim().length == 0) {
        $(".divError#erEmail span").show();
        $("#divProfile li input#txtEmail").addClass("errorActive");
        return false;
    }
    else if(countryValue==0)
    {      
        $(".divError#erCountry span").show();
        $("#divProfile li #ddlCountry").addClass("errorActive");
        return false;
    }
    else if (countryValue == "223" && stateValue == 0) {
        $(".divError#erState span").show();
        $("#divProfile li #ddlState").addClass("errorActive");
        return false;
    }
    else if (!isAgeValid) {
        return false;
    }
   
    else {
        $("#divProfile li input").removeClass("errorActive");
        $(".divError").hide();
        var validEmail = IsEmailValid(email);
       
        if (!validEmail) {
            $(".divError#erEmail span").text("").text("Please enter a valid e-mail address.").show();
            $("#divProfile li input#txtEmail").addClass("errorActive");
            return false;
        }
       
    }
};

var pickedDay = false;
var pickedMonth = false;
var pickedYear = false;

var dobDay = 0;
var dobMonth = 0;
var dobYear = 0;

function validateDay(ddlCtrl) {
    void 0
    dobDay = document.getElementById(ddlCtrl.id).value;
    if (dobDay > 0) {
        pickedDay = true;
    }
    else {
        pickedDay = false;
    }

    $("#erBirthday.divError span").hide();
    $("#validateDateLi").css("margin-bottom", "15px");
    //checkForValidDate();
};

function validateMonth(ddlCtrl) {
    dobMonth = document.getElementById(ddlCtrl.id).value;
    if (dobMonth > 0) {
        pickedMonth = true;
    }
    else {
        pickedMonth = false;
    }

    $("#erBirthday.divError span").hide();
    $("#validateDateLi").css("margin-bottom", "15px");
    // checkForValidDate();
};

function validateYear(ddlCtrl) {
    dobYear = document.getElementById(ddlCtrl.id).value;
    if (dobYear > 0) {
        pickedYear = true;
    }
    else {
        pickedYear = false;
    }

    $("#erBirthday.divError span").hide();
    $("#validateDateLi").css("margin-bottom", "15px");
    // checkForValidDate();
};

function checkForValidDate() {
    var parentTag = $("#validateDateLi");
    

    dobDay = document.getElementById("ddlDay").value;
    dobMonth = document.getElementById("ddlMonth").value;
    dobYear = document.getElementById("ddlYear").value;

    if (dobDay == 0 && dobMonth == 0 && dobYear == 0) {

        if (windowWidth < 500)//isMobile
        {

            if (parentTag) {
               
                $("#erBirthday.divError span").show();

                void 0;
            }

        }
        else {

            $("#erBirthday.divError span").show();
            $(parentTag).find(".divError").show();
        }
        return false;
    }
    else {
     
        if (GetAgeInYears() >= 18) {

          
            $("#erBirthday.divError span").hide();

            return true;
        }
        else {
            $("#erBirthday.divError span").text('You must be over the age of 18.');
            if (windowWidth < 500)//isMobile
            {

                if (parentTag) {
                    
                    $("#erBirthday.divError span").show();
                    $(parentTag).find(".divError").show();
                   
                }

            }
            else {


               
                $("#erBirthday.divError span").show();
                $(parentTag).find(".divError").show();
            }
            return false;
        }
    }
};

function GetAgeInYears() {
    var now = new Date();
    var dob = new Date(dobYear, dobMonth, dobDay, 0, 0, 0, 0);
    var diff = now.getTime() - dob.getTime();

    return Math.floor(diff / (1000 * 60 * 60 * 24 * 365.25));
};

function IsEmailValid(email) {
    var isValidEmail = false;
    $.ajax({
        type: "POST",
        url: IsEmailValidService,
        data: '{"email":' + "'" + email + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            void 0
            isValidEmail = response.d;
            // delegate(response.d);
        },
        async: false,
        error: function (response) {
            //console.log(response.status + " " + response.statusText);
        }
    });
    void 0;
    return isValidEmail;
};

function ShowConfirmationMessage()
{
    alertify.success("Profile was successfully updated.");
}
function ConfirmAgeControl(obj)
{
    void 0;
    if (obj.checked) 
    {
        $('#AgeConfirmationPopup').lightbox_me({
            centered: true,
            onLoad: function () {

            },
            onClose: function () {

            }
        });
    }
   
}

function CloseAgeConfirmationPopup()
{
    $('#AgeConfirmationPopup').trigger("close");
}

        
   

