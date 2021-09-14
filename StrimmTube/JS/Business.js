(function () {

    var SubmitBusinessContactRequest = "/WebServices/UserService.asmx/AddBusinessContactRequest";

    var BusinessManager = {

        submitRequest: function () {
            if (this.validateRequestData()) {
                var selectedPackage = '';

                var isBasic = $("#chkBasic").is(":checked");
                var isStandard = $("#chkStandard").is(":checked");
                var isEnterprise = $("#chkEnterprise").is(":checked");

                if (isBasic) {
                    selectedPackage += 'Basic';
                }

                if (isStandard) {
                    selectedPackage += (selectedPackage.trim().length == 0) ? 'Standard' : ', Standard';
                }

                if (isEnterprise) {
                    selectedPackage += (selectedPackage.trim().length == 0) ? 'Enterprise' : ', Enterprise';
                }

                if (selectedPackage.trim().length == 0) {
                    selectedPackage = 'None';
                }

                var request = {
                    Name: $("#txtName").val(),
                    Company: $("#txtCompany").val(),
                    Email: $('#txtEmail').val(),
                    WebsiteUrl: $('#txtWebsite').val(),
                    PhoneNumber: $("#txtPhoneNumber").val(),
                    PackageType: selectedPackage,
                    Comments: $('#txtComments').val()
                };

                $.ajax({
                    type: "POST",
                    url: SubmitBusinessContactRequest,
                    cashe: false,
                    data: '{"request":' + JSON.stringify(request) + '}',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        var isSuccess = response.d.IsSuccess;

                        if (isSuccess) {
                            clearFormData();

                            alertify.success("Thank you for submitting your information!");
                        }
                        else {
                            alertify.error("Something went wrong. Please try again!");
                        }
                    },
                    complete: function (response) {
                    }
                });
            }
        },

        validateRequestData: function () {
            var name = $("#txtName").val();
            var phone = $("#txtPhoneNumber").val();
            var email = $("#txtEmail").val();
            var content = $("#txtComments").val();
            var websiteUrl = $("#txtWebsite").val();

            var isDataValid = true;

            if (name.trim().length == 0 || !validateText(name)) {
                $("#erName.divError").show();
                $("#subInfoForm li input#txtName").addClass("errorActive");
                isDataValid = false;
            }

            if (phone.trim().length == 0) {
                $("#erPhone.divError").show();
                $("#subInfoForm li input#txtPhoneNumber").addClass("errorActive");
                isDataValid = false;
            }

            if (websiteUrl.trim().length == 0) {
                $("#erWebsite.divError").show();
                $("#subInfoForm li input#txtWebsite").addClass("errorActive");
                isDataValid = false;
            }

            if (content.trim().length == 0) {
                $("#erContent.divError").show();
                $("#subInfoForm li input#txtComments").addClass("errorActive");
                isDataValid = false;
            }

            var isProperEmail = validateEmail(email);

            if (email.trim().length == 0) {
                $(".divError#erEmail span").text("").text("E-mail address is required");
                $(".divError#erEmail").show();
                $("#divSignUp li input#txtEmail").addClass("errorActive");
                isDataValid = false;
            }
            else {
                if (!isProperEmail) {
                    $(".divError#erEmail span").text("").text("Invalid e-mail address entered");
                    $(".divError#erEmail").show();
                    $("#divSignUp li input#txtEmail").addClass("errorActive");
                    isDataValid = false;
                }
            }

            if (!isDataValid) {
                alertify.error('Please complete all required fields!');
            }
            return isDataValid;
        }
    };

    getManager = function () {
        return BusinessManager;
    };

})();

$(document).on('blur', '#txtName', function () {
    var name = $("#txtName").val();

    if (name.trim().length == 0 || !validateText(name)) {
        $("#erName.divError").show();
        $("#subInfoForm li input#txtName").addClass("errorActive");
    }
    else {
        $("#erName.divError").hide();
        $("#subInfoForm li input#txtName").removeClass("errorActive");
    }
});

$(document).on('blur', '#txtWebsite', function () {
    var name = $("#txtWebsite").val();

    if (name.trim().length == 0 || !validateText(name)) {
        $("#erWebsite.divError").show();
        $("#subInfoForm li input#txtWebsite").addClass("errorActive");
    }
    else {
        $("#erWebsite.divError").hide();
        $("#subInfoForm li input#txtWebsite").removeClass("errorActive");
    }
});

$(document).on('blur', '#txtComments', function () {
    var name = $("#txtComments").val();

    if (name.trim().length == 0 || !validateText(name)) {
        $("#erContent.divError").show();
        $("#subInfoForm li input#txtComments").addClass("errorActive");
    }
    else {
        $("#erWebsite.divError").hide();
        $("#subInfoForm li input#txtComments").removeClass("errorActive");
    }
});
$(document).on('blur', '#txtPhoneNumber', function () {
    var phone = $("#txtPhoneNumber").val();

    if (phone.trim().length == 0) {
        $("#erPhone.divError").show();
        $("#subInfoForm li input#txtPhoneNumber").addClass("errorActive");
    }
    else {
        $("#erPhone.divError").hide();
        $("#subInfoForm li input#txtPhoneNumber").removeClass("errorActive");
    }
});

$(document).on('blur', '#txtEmail', function () {
    var email = $("#txtEmail").val();
    var isProperEmail = validateEmail(email);

    if (email.trim().length == 0) {
        $(".divError#erEmail span").text("").text("E-mail address is required")
        $(".divError#erEmail").show();
        $("#divSignUp li input#txtEmail").addClass("errorActive");
    }
    else {
        if (isProperEmail) {
            $(".divError#erEmail").hide();
            $("#divSignUp li input#txtEmail").removeClass("errorActive");
        }
        else {
            $(".divError#erEmail span").text("").text("Invalid e-mail address entered");
            $(".divError#erEmail").show();
            $("#divSignUp li input#txtEmail").addClass("errorActive");
        }
    }
});

var clearFormData = function () {
    $("#txtName").val('');
    $("#txtPhoneNumber").val('');
    $("#txtEmail").val('');
    $("#txtCompany").val('');
    $("#txtWebsite").val('');
    $("#txtComments").val('');

    $("#chkBasic").prop('checked', false);
    $("#chkStandard").prop('checked', false);
    $("#chkEnterprise").prop('checked', false);
};

function OpenFAQ(className) {
    $(".FAQQuestion").removeClass('active');
    $('.FAQAnswer').hide();
    $(".FAQQuestion." + className).addClass('active');
    $('.FAQAnswer.' + className).show();
    $(".FAQQuestion." + className).removeAttr('onclick').attr('onclick', 'CloseFAQ("' + className + '")');
};

function CloseFAQ(className) {
    $(".FAQQuestion").removeClass('active');
    $('.FAQAnswer').hide();
    $(".FAQQuestion." + className).removeAttr('onclick').attr('onclick', 'OpenFAQ("' + className + '")');
};

function showRokuAppInfo() {
    $('#rokuInfoPopup').show();
    $('#rokuInfoPopup').lightbox_me({
        centered: true,
        onLoad: function () {
        },
        overlayCSS: {
            background: 'black',
            opacity: .8
        },
        onClose: function () {
            $('#rokuInfoPopup').hide();
            RemoveOverlay();
        }
    });
};

$(window).scroll(function () {
    //if you hard code, then use console
    //.log to determine when you want the 
    //nav bar to stick.  
    //console.log($(window).scrollTop())
    if ($(window).scrollTop() > 70) {
        $('.secondaryMenu').addClass('secondaryMenuFixed');
        $('#topMenu').addClass('topMenuAbsolute');
    }
    if ($(window).scrollTop() < 70) {
        $('.secondaryMenu').removeClass('secondaryMenuFixed');
        $('#topMenu').removeClass('topMenuAbsolute');
    }

});