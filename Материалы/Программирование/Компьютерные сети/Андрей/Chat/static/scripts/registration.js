$(function() {
    $('html').keydown(function(e) {
        if (e.keyCode == 13) {
            registration();
        }
    });
});

$(function() {
    $('#confirm').keydown(function(e) {
        if ($('#password').val() != $('#confirm').val()) {
            $('#confirm').css('background-color', 'rgba(255, 0, 0, 0.1)');
        } else {
            $('#confirm').css('background-color', '#3B4148');
        }
    });
    $('#confirm').keyup(function(e) {
        if ($('#password').val() != $('#confirm').val()) {
            $('#confirm').css('background-color', 'rgba(255, 0, 0, 0.1)');
        } else {
            $('#confirm').css('background-color', '#3B4148');
        }
    });
    $('#confirm').focusin(function(e) {
        if ($('#password').val() != $('#confirm').val()) {
            $('#confirm').css('background-color', 'rgba(255, 0, 0, 0.1)');
        } else {
            $('#confirm').css('background-color', '#3B4148');
        }
    });
});

function registration() {
    var name = $('#name').val();
    var username = $('#username').val();
    var password = $('#password').val();
    var confirm = $('#confirm').val();


    if (name == "" || username == "" || password == "" || confirm == "") {
        $('.error').html("Не все поля заполнены");
        $('.error').css({
            opacity: 1
        });
        return;
    }
    if (username.length < 6) {
        $('.error').html("Логин должен иметь как минимум 6 символов");
        $('.error').css({
            opacity: 1
        });
        return;
    }
    if (password.length < 6) {
        $('.error').html("Пароль должен иметь как минимум 6 символов");
        $('.error').css({
            opacity: 1
        });
        $('#password').val("");
        $('#confirm').val("");
        return;
    }
    if (password != confirm) {
        $('.error').html("Пароли не совпадают");
        $('.error').css({
            opacity: 1
        });
        $('#password').val("");
        $('#confirm').val("");
        return;
    }
    var arr = {
        name: name,
        username: username,
        password: password,
        confirm: confirm
    };
    $.ajax({
        url: '/registration',
        type: 'POST',
        data: JSON.stringify(arr),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false
    }).done(function(data) {
        if (data.response == "Error") {
            $('.error').html("Логин уже сущесствует");
            $('.error').css({
                opacity: 1
            });
            $('#username').val("");
            $('#password').val("");
            $('#confirm').val("");
            return;
        }
        $(location).attr("href", "/login");
    });
}