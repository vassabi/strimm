var IsUserNameUniqueService = "WebServices/UserService.asmx/IsUserNameUnique";
var IsPasswordStrongService = "WebServices/UserService.asmx/IsPasswordStrong";
var IsEmailValidService = "WebServices/UserService.asmx/IsEmailValid";
var IsUserDeletedService = "WebServices/UserService.asmx/IsUserDelete";
var windowWidth;
$(document).ready(function () {
    $("#chkBxShowHidePasswordSignUp").attr('checked', false);
    $("#txtPasswordSignUp").val("");

    $("#txtUserNameSignUp").attr("autocomplete", "off");


    windowWidth = window.screen.width < window.outerWidth ?
                 window.screen.width : window.outerWidth;
});
function validateAlphaNumeric(text) {
    var re = /^[a-zA-Z0-9 ]+$/;
    return re.test(text);
}

function validateText(text) {
    var re = /^[a-zA-Z ]+$/;
    return re.test(text);
}

function validatePublicName(text) {
    var re = /^([a-zA-Z0-9&\-\'\ ]+)$/;
    return re.test(text);
}

function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

$(document).on('blur', '#txtFirstName', function () {

    var firstName = $("#txtFirstName").val();
    if (firstName.trim().length == 0 || !validateText(firstName)) {
        $("#erFirstName.divError span").show();
        //$("#divSignUp li input#txtFirstName").addClass("errorActive");

        if (windowWidth < 500)//isMobile
        {
            var parentTag = $(this).parent().get(0);
            if (parentTag) {
                $(parentTag).css("margin-bottom", "15px");
                $(parentTag).css("margin-bottom", "45px");
                $(parentTag).find(".divError").show();
                // $(parentTag +" .divError").show();
            }
            void 0;
        }
        else {

            var parentTag = $(this).parent().get(0);
            $(parentTag).find(".divError").show();
        }

    }
    else {
        $("#erFirstName.divError span").hide();
        //$("#divSignUp li input#txtFirstName").removeClass("errorActive");
        var parentTag = $(this).parent().get(0);
        $(parentTag).css("margin-bottom", "15px");
        $(parentTag).find(".divError").hide();
    }
});

$(document).on('blur', '#txtLastName', function () {

    var lastName = $("#txtLastName").val();
    if (lastName.trim().length == 0 || !validateText(lastName)) {
        $(".divError#erLastName span").show();
        if (windowWidth < 500)//isMobile
        {
            var parentTag = $(this).parent().get(0);
            if (parentTag) {
                $(parentTag).css("margin-bottom", "15px");
                $(parentTag).css("margin-bottom", "45px");
                $(parentTag).find(".divError").show();

            }
            void 0;
        }
        else {

            var parentTag = $(this).parent().get(0);
            $(parentTag).find(".divError").show();
        }
    }
    else {
        $("#erFirstName.divError span").hide();
        //$("#divSignUp li input#txtFirstName").removeClass("errorActive");
        var parentTag = $(this).parent().get(0);
        $(parentTag).css("margin-bottom", "15px");
        $(parentTag).find(".divError").hide();
    }
});

$(document).on('blur', '#txtEmail', function () {
    var email = $.trim($("#txtEmail").val());

    if (email) {
        var isProperEmail = validateEmail(email);
        var isUserDeleted = false; //IsUserDeleted(email);

        if (isProperEmail) {
            var isValid = IsEmailValid(email);
            if (isValid) {
                void 0;
                var parentTag = $(this).parent().get(0);
                $(parentTag).css("margin-bottom", "15px");
                $(parentTag).find(".divError").hide();
               // $("#divSignUp li input#txtEmail").removeClass("errorActive");
            }
            else if (!isUserDeleted) {
                void 0;
                $(".divError#erEmail span").hide();
                var parentTag = $(this).parent().get(0);
                $(parentTag).css("margin-bottom", "15px");
                $(parentTag).find(".divError").hide();
            }
            else {
                void 0;
                $(".divError#erEmail span").text("").text("Invalid e-mail address entered.").show();
                if (windowWidth < 500)//isMobile
                {
                    var parentTag = $(this).parent().get(0);
                    if (parentTag) {
                        $(parentTag).css("margin-bottom", "15px");
                        $(parentTag).css("margin-bottom", "45px");
                        $(parentTag).find(".divError").show();

                    }
                    void 0;
                }
                else {

                    var parentTag = $(this).parent().get(0);
                    $(parentTag).find(".divError").show();
                }
            }
        }
        else {
            void 0;
            $(".divError#erEmail span").text("").text("Invalid e-mail address entered.").show();
            if (windowWidth < 500)//isMobile
            {
                var parentTag = $(this).parent().get(0);
                if (parentTag) {
                    $(parentTag).css("margin-bottom", "15px");
                    $(parentTag).css("margin-bottom", "45px");
                    $(parentTag).find(".divError").show();

                }
                void 0;
            }
            else {

                var parentTag = $(this).parent().get(0);
                $(parentTag).find(".divError").show();
            }
        }
    }
    else {
        void 0;
        $(".divError#erEmail span").text("").text("E-mail address is required.").show();
        if (windowWidth < 500)//isMobile
        {
            var parentTag = $(this).parent().get(0);
            if (parentTag) {
                $(parentTag).css("margin-bottom", "15px");
                $(parentTag).css("margin-bottom", "45px");
                $(parentTag).find(".divError").show();

            }
            void 0;
        }
        else {

            var parentTag = $(this).parent().get(0);
            $(parentTag).find(".divError").show();
        }
    }
});

$(document).on('blur', '#txtReenterEmail', function () {

    var emailVal = $.trim($("#txtEmail").val().toLowerCase());
    var reenterValue = $.trim($("#txtReenterEmail").val().toLowerCase());
    if (emailVal != reenterValue) {
        $(".divError#erReEmail span").show();
        if (windowWidth < 500)//isMobile
        {
            var parentTag = $(this).parent().get(0);
            if (parentTag) {

                $(parentTag).css("margin-bottom", "45px");
                $(".divError#erReEmail span").show();
                $(parentTag).find(".divError").show();

            }
            void 0;
        }

        else {

            var parentTag = $(this).parent().get(0);
            $(parentTag).find(".divError").show();
        }
    }
    else if (reenterValue.length == 0) {
        var parentTag = $(this).parent().get(0);
        $(parentTag).css("margin-bottom", "45px");
        $(parentTag).find(".divError").show();
        $(".divError#erReEmail span").show();
    }
    else {
        var parentTag = $(this).parent().get(0);
        $(parentTag).css("margin-bottom", "15px");
        $(".divError#erReEmail span").hide();
        $(parentTag).find(".divError").hide();
    }
});

function MakePasteError() {
    //console.log("here")
    $(".divError#erReEmail span").show().text("").text(" Please enter your email manually");
    if (windowWidth < 500)//isMobile
    {
        var parentTag = $(this).parent().get(0);
        if (parentTag) {
            $(parentTag).css("margin-bottom", "15px");
            $(parentTag).css("margin-bottom", "45px");
            $(".divError#erReEmail span").show();
            $(parentTag).find(".divError").show();

        }
        void 0;
    }
    else {

        var parentTag = $(this).parent().get(0);
        $(parentTag).find(".divError").show();
    }

};



//$("#txtReenterEmail").focus(function () {
//    $(".divError#erReEmail span").hide().text("").text("Emails do not match");
//    $("#divSignUp li input#txtReenterEmail").removeClass("errorActive");
//});
$(document).on('blur', '#txtUserNameSignUp', function () {

    var publicName = $("#txtUserNameSignUp").val();

    if (publicName) {
        var encodedName = encodeURIComponent(publicName).replace(/'/g, "%27");
        var userNameIsUnique = IsUserNameUnique(encodedName.trim());

        if (validatePublicName(publicName)) {
            if (publicName.trim().length < 3 || publicName.trim().length > 15) {
                $(".divError#erUserName span").text("").text("Public Name must be 3-15 letters and may include space, apostrophe, \', '-' and '&' characters only.").show();
                if (windowWidth < 500)//isMobile
                {
                    var parentTag = $(this).parent().get(0);
                    if (parentTag) {
                        $(parentTag).css("margin-bottom", "15px");
                        $(parentTag).css("margin-bottom", "45px");

                        $(parentTag).find(".divError").show();

                    }
                    void 0;
                }
                else {

                    var parentTag = $(this).parent().get(0);
                    $(parentTag).find(".divError").show();
                }

            }
            else if (!userNameIsUnique) {
                $(".divError#erUserName span").text("").text("Public Name is taken.").show();
                if (windowWidth < 500)//isMobile
                {
                    var parentTag = $(this).parent().get(0);
                    if (parentTag) {
                        $(parentTag).css("margin-bottom", "15px");
                        $(parentTag).css("margin-bottom", "45px");

                        $(parentTag).find(".divError").show();

                    }
                    void 0;
                }
                else {

                    var parentTag = $(this).parent().get(0);
                    $(parentTag).find(".divError").show();
                }
            }
            else {
                $(".divError#erUserName span").hide();
                var parentTag = $(this).parent().get(0);
                $(parentTag).css("margin-bottom", "15px");

                $(parentTag).find(".divError").hide();
            }
        }
        else {
            $(".divError#erUserName span").text("").text("Public Name must be 3-15 letters and may include space, apostrophe, \', '-' and '&' characters only.").show();
            if (windowWidth < 500)//isMobile
            {
                var parentTag = $(this).parent().get(0);
                if (parentTag) {
                    $(parentTag).css("margin-bottom", "15px");
                    $(parentTag).css("margin-bottom", "45px");

                    $(parentTag).find(".divError").show();

                }
                void 0;
            }
            else {

                var parentTag = $(this).parent().get(0);
                $(parentTag).find(".divError").show();
            }
        }
    }
    else {
        $(".divError#erUserName span").text("").text("Public Name is required.").show();
        if (windowWidth < 500)//isMobile
        {
            var parentTag = $(this).parent().get(0);
            if (parentTag) {
                $(parentTag).css("margin-bottom", "15px");
                $(parentTag).css("margin-bottom", "45px");

                $(parentTag).find(".divError").show();

            }
            void 0;
        }
        else {

            var parentTag = $(this).parent().get(0);
            $(parentTag).find(".divError").show();
        }
    }
});

$(document).on('blur', '#txtPasswordSignUp', function () {

    var password = $("#txtPasswordSignUp").val();
    var passwordIsStrong = IsPasswordStrong(password);
    void 0;
    if (password.trim().length < 8) {
        //  console.log("less8 ")
        $(".divError#erPassword span").show();
        if (windowWidth < 500)//isMobile
        {
            var parentTag = $(this).parent().get(0);
            if (parentTag) {
                $(parentTag).css("margin-bottom", "15px");
                $(parentTag).css("margin-bottom", "45px");

                $(parentTag).find(".divError").show();

            }
            void 0;
        }
        else {

            var parentTag = $(this).parent().get(0);
            $(parentTag).find(".divError").show();
        }
    }
    else if (!passwordIsStrong) {
        // (console.log("not strong"))

        $(".divError#erPassword span").show();
        if (windowWidth < 500)//isMobile
        {
            var parentTag = $(this).parent().get(0);
            if (parentTag) {
                $(parentTag).css("margin-bottom", "15px");
                $(parentTag).css("margin-bottom", "45px");

                $(parentTag).find(".divError").show();

            }
            void 0;
        }
        else {

            var parentTag = $(this).parent().get(0);
            $(parentTag).find(".divError").show();
        }
    }
    else {
        $(".divError#erPassword span").hide();
        var parentTag = $(this).parent().get(0);
        $(parentTag).css("margin-bottom", "15px");

        $(parentTag).find(".divError").hide();
    }

});

$(document).on('copy', '#txtEmail', function (e) {
    e.preventDefault();
});

$(document).on('copy', '#txtReenterEmail', function (e) {
    e.preventDefault();
});

$(document).on('paste', '#txtReenterEmail', function (e) {
    e.preventDefault();
});


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
    $("#validateDateLi").css("margin-bottom", "15px");

    dobDay = document.getElementById("ddlDay").value;
    dobMonth = document.getElementById("ddlMonth").value;
    dobYear = document.getElementById("ddlYear").value;

    if (dobDay == 0 && dobMonth == 0 && dobYear == 0) {

        if (windowWidth < 500)//isMobile
        {

            if (parentTag) {
                $(parentTag).css("margin-bottom", "15px");
                $(parentTag).css("margin-bottom", "55px");
                $(parentTag).find(".divError").show();
                $("#validateDateLi .divError span").show();

                void 0;
            }

        }
        else {divError

        $("#validateDateLi .divError span").show();
            $(parentTag).find(".divError").show();
        }
        return false;
    }
    else {
        $(parentTag).css("margin-bottom", "15px");
        if (GetAgeInYears() >= 18) {

            $(parentTag).css("margin-bottom", "15px");

            $(parentTag).find(".divError").hide();
            $("#validateDateLi .divError span").hide();

            return true;
        }
        else {
            $("#validateDateLi .divError span").text('You must be over the age of 16 in order to sign up for an account.');
            if (windowWidth < 500)//isMobile
            {

                if (parentTag) {
                    $(parentTag).css("margin-bottom", "15px");
                    $(parentTag).css("margin-bottom", "55px");
                    $("#erBirthday.divError span").show();
                    $(parentTag).find(".divError").show();
                    void 0;
                }

            }
            else {


                void 0;
                $("#validateDateLi .divError span").show();
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

function validateCountry(ddlCtrl) {
    country = $('.ddlCountry')[0].value;
    var parentTag = $("#validateCountryLi");
    if (country == '0') {
        $("#erCountry.divError span").show();

        if (windowWidth < 500)//isMobile
        {

            if (parentTag) {
                $(parentTag).css("margin-bottom", "15px");
                $(parentTag).css("margin-bottom", "55px");
                $("#erCountry.divError span").show();
                $(parentTag).find(".divError").show();
                void 0;
            }

        }
        else {


            void 0;
            $("#erCountry.divError span").show();
            $(parentTag).find(".divError").show();
        }
        return false;
    }
    else {
        $(parentTag).css("margin-bottom", "15px");
        $(parentTag).find(".divError").hide();
        $("#erCountry.divError span").hide();
    }
}

function validateGendar(ddlCtrl) {
    var listItemArray = null;
    var parentTag = $("#validateGenderLi");
    if (ddlCtrl) {
        listItemArray = document.getElementById(ddlCtrl.id);
    }
    else {
        listItemArray = document.getElementById('rdbGender');
    }

    var isItemChecked = false;

    for (var i = 0; i < listItemArray.children.length; i++) {
        var listItem = listItemArray.children[i];

        if (listItem.checked) {
            isItemChecked = true;
        }
    }

    if (isItemChecked == false) {
        $(parentTag).css("margin-bottom", "55px");
        $(parentTag).find(".divError").show();
        $("#erGender1.divError span").show();
        return false;
    }
    else {
        $(parentTag).css("margin-bottom", "15px");
        $(parentTag).find(".divError").hide();
        $("#erGender1.divError span").hide();
    }

    return true;
};

function genderChanged() {
    var parentTag = $("#validateGenderLi");
    $(parentTag).css("margin-bottom", "15px");
    $(parentTag).find(".divError").hide();
    $("#erGender1.divError span").hide();
}

function ValidateRequired() {
    var firstName = $("#txtFirstName").val();
    var lastName = $("#txtLastName").val();
    var email = $("#txtEmail").val();
    var reenterEmail = $("#txtReenterEmail").val();
    var password = $("#txtPasswordSignUp").val();
    var userName = $("#txtUserNameSignUp").val();

    var hasErrors = false;

    if (validateCountry() == false) {
        hasErrors = true;
    }

    if (checkForValidDate() == false) {
        hasErrors = true;
    }

    if (firstName.trim().length == 0) {
        if (windowWidth < 500)
        {
            $("#validateFirstNameLi").css("margin-bottom", "45px");
        }
     
        $("#erFirstName.divError span").show();
        $("#validateFirstNameLi .divError").show();
       
        hasErrors = true;
    }

    if (lastName.trim().length == 0) {
        $(".divError#erLastName span").show();
      

        if (windowWidth < 500) {
            $("#validateLastNameLi").css("margin-bottom", "45px");
        }

        $("#erLastName.divError span").show();
        $("#validateLastNameLi .divError").show();
        hasErrors = true;
    }

    if (email.trim().length == 0) {
        $(".divError#erEmail span").show();


        if (windowWidth < 500) {
            $("#validateEmailLi").css("margin-bottom", "45px");
        }

        $("#erEmail.divError span").show();
        $("#validateEmailLi .divError").show();          
        hasErrors = true;
    }

    if (reenterEmail.trim().length == 0) {
        $(".divError#erReEmail span").show();


        if (windowWidth < 500) {
            $("#validateReenterEmailLi").css("margin-bottom", "45px");
        }

        $("#erReEmail.divError span").show();
        $("#validateReenterEmailLi .divError").show();
        $(".divError#erReEmail span").show();        
        hasErrors = true;
    }

    if (userName.trim().length == 0) {
        
        if (windowWidth < 500) {
            $("#validateUserNameLi").css("margin-bottom", "45px");
        }

        $("#erUserName.divError span").show();
        $("#validateUserNameLi .divError").show();
        hasErrors = true;
    }

    if ((password === 'undefined') || (password == null) || password.trim().length == 0) {
        $(".divError#erPassword span").show();
        $(".divError#erPassword span").show();
        if (windowWidth < 500) {
            $("#validatePasswordLi").css("margin-bottom", "45px");
        }

        $("#erPassword.divError span").show();
        $("#validatePasswordLi .divError").show();
        hasErrors = true;
    }

    if (email.toLowerCase() != reenterEmail.toLowerCase()) {
        $(".divError#erReEmail span").show();
      
        hasErrors = true;
    }

    var validEmail = IsEmailValid(email);
    var isUserDeleted = false //IsUserDeleted(email);
    var userNameUnique = IsUserNameUnique(userName);
    var passwordStrong = IsPasswordStrong(password);

    if (validEmail == undefined || !validEmail) {
        $(".divError#erEmail span").text("").text("Please enter a valid e-mail address.").show();
       
        hasErrors = true;
    }
    if (isUserDeleted == undefined || isUserDeleted == true) {
        $(".divError#erEmail span").text("").text("User with this email was deleted by Strimm administrator.").show();
        if (windowWidth < 500) {
            $("#validateUserNameLi").css("margin-bottom", "45px");
        }

        $("#erUserName.divError span").show();
        $("#validateUserNameLi .divError").show();
        hasErrors = true;
    }
    if (userName == '' || userName == undefined) {
        $(".divError#erUserName span").text("").text("Public Name is required.").show();
        if (windowWidth < 500) {
            $("#validateUserNameLi").css("margin-bottom", "45px");
        }

        $("#erUserName.divError span").show();
        $("#validateUserNameLi .divError").show();
        hasErrors = true;
    }
    else if (userName != '' && (userNameUnique == undefined || !userNameUnique)) {
        $(".divError#erUserName span").text("").text("Public Name is taken.").show();
        if (windowWidth < 500) {
            $("#validateUserNameLi").css("margin-bottom", "45px");
        }

        $("#erUserName.divError span").show();
        $("#validateUserNameLi .divError").show();
        hasErrors = true;
    }
    if (passwordStrong == undefined || !passwordStrong) {
        $(".divError#erPassword span").show();
        if (windowWidth < 500) {
            $("#validatePasswordLi").css("margin-bottom", "45px");
        }

        $("#erPassword.divError span").show();
        $("#validatePasswordLi .divError").show();
        hasErrors = true;
    }
    //if (country == '0') {
    //    $(".divError#erCountry span").show();
    //    $("#divSignUp li input.ddlCountry").addClass("errorActive");
    //    hasErrors = true;
    //}

    if (hasErrors == false) {
        // $("#divSignUp li input").removeClass("errorActive");
        $("#divSignUp li").css("margin-bottom", "15px");
        $(".divError").hide();
        $(".divError span").hide();
    }

    return !hasErrors;
}

function IsUserDeleted(email) {
    var isUserDeleted = false;
    $.ajax({
        type: "POST",
        url: IsUserDeletedService,
        data: '{"email":' + "'" + email + "'" + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            void 0
            isUserDeleted = response.d;
            // delegate(response.d);
        },
        async: false,
        error: function (response) {
            //console.log(response.status + " " + response.statusText);
        }
    });
    void 0;
    return isUserDeleted;
}

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

function ShowHidePasswordSignUp() {



    if ($("#divSignUp #txtPasswordSignUp").attr("type") == "password") {
        $("#divSignUp #txtPasswordSignUp").attr("type", "text");
    }
    else {
        $("#divSignUp #txtPasswordSignUp").attr("type", "password");
    }

}

