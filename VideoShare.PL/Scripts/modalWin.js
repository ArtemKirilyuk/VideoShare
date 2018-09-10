$(function () {
    $.ajaxSetup({cache: false});
    $("#btn-create-playlist").click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#dialogContent').html(data);
            $('#playlist-create').modal('show');
        })
        .done(function () {
            appendSubmitButton();
        });

    });
});

function sendPlaylistForm() {
    $.ajax({
        url: '/Playlist/AddPlaylist',
        type: "POST",
        data: $('form').serialize(),
        datatype: "json",
        success: function (result) {
            var form_result = $(result).find('#playlist-form');
            if (form_result.length > 0) {
                $("#dialogContent").html(result);
                appendSubmitButton();
            }
            else {
                $("#dialogContent").html(result);
                var parser = new DOMParser();
                var doc = parser.parseFromString(result,"text/html");
                var link = $(doc).find('#redirectLink').text();
                window.location.href = "../" + link;
            }
        }
    });
}

function appendSubmitButton() {
    var $input = $('<input type="button" id="send-playlist" class="btn btn-default" value="Save" />');
    $input.appendTo($('form'));
    $("#send-playlist").click(function () {
        sendPlaylistForm();
    });
}