$(function() {
    $('html').keydown(function(e) {
        if (e.keyCode == 13) {
            login();
        }
    });
});

function login() {
    var username = $('#username').val();
    var password = $('#password').val();
    if (username == "" || password == "") {
        return;
    }
    var arr = {
        username: username,
        password: password
    };
    $.ajax({
        url: '/login',
        type: 'POST',
        data: JSON.stringify(arr),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false
    }).done(function(data) {
        if (data.response == "Error") {
            $('.error').html("Логин или пароль введен не верно");
            $('.error').css({
                opacity: 1
            });
            $('#password').val("");
            return;
        }
        $(location).attr("href", "/");
    });
}