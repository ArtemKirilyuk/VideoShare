var state = $("#like-state").text();
if (state === "True") {
    $("#full-like").removeClass("invisible");
    $("#empty-like").addClass("invisible");
}
else {
    $("#full-like").addClass("invisible");
    $("#empty-like").removeClass("invisible");
}

$("#empty-like").click(function () {
    $("#Liked").val(true);
    $("#empty-like").toggleClass("invisible");
    $("#full-like").toggleClass("invisible");
});
$("#full-like").click(function () {
    $("#Liked").val(false);
    $("#empty-like").toggleClass("invisible");
    $("#full-like").toggleClass("invisible");
});
