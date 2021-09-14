//character left reminder in fields of channel registation
$(document).ready(function () {
    function limits(obj, limit, lbl) {
        var text = $(obj).val();
        var length = text.length;
        if (length > limit) {
            $(obj).val(text.substr(0, limit));
        } else { // alert the user of the remaining char.
            $(lbl).text(limit - length );
        }
    }
    $('#txtTitle').focus(function () { $("#spnTitleCount").text(""); }).keyup(function () {
        limits($(this), 50, $("#spnTitleCount"));
    }).focusout(function () {
        $("#spnTitleCount").text("");z
    });
    $('#txtStory').focus(function () { $("#spnCharCount").text(""); }).keyup(function () {
        limits($(this), 1000, $("#spnCharCount"));
    }).focusout(function () { $("#spnCharCount").text(""); });
    $('#txtPost').focus(function () { $("#spnPostCount").text(""); }).keyup(function () {
        limits($(this), 250, $("#spnPostCount"));
    }).focusout(function () { $("#spnPostCount").text(""); })
    //    $('#txtAboutMe').focus(function () { $("#lblError").text(""); }).keyup(function () {
    //        limits($(this), 500);
    //    }).focusout(function () { $("#lblError").text(""); })
});
// image preview
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
    //console.log(input.class);
    var elementClass = $("#"+input.id).attr("class");
    var elementImg = "img." + elementClass;
    //console.log(elementImg);
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