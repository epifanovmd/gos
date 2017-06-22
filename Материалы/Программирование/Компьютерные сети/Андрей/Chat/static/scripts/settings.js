function changeName() {
    newName = $('#newName').val();
    if (newName == "") {
        $('.viewSet').scrollTop(0);
        $('.help').html("Имя не может быть пустым");
        $('.help').css("background-color", "rgba(255, 0, 0, 0.1)");
        $('.help').css("border", "1px solid rgba(255, 0, 0, 0.3)");
        $('.help').css("color", "red");
        $('.help').css("opacity", "1");
        return;
    }
    var arr = {
        name: newName
    };
    $.ajax({
        url: '/api/change/name',
        type: 'POST',
        data: JSON.stringify(arr),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false
    }).done(function(data) {
        if (data.response == "Error") {
            $('.viewSet').scrollTop(0);
            $('.help').html("Ошибка сервера");
            $('.help').css("background-color", "rgba(255, 0, 0, 0.1)");
            $('.help').css("border", "1px solid rgba(255, 0, 0, 0.3)");
            $('.help').css("color", "red");
            $('.help').css("opacity", "1");
            return;
        } else {
            $('.viewSet').scrollTop(0);
            $('#name').html(get_cookie("name"));
            $('#newName').val("");
            $('.help').html("Имя успешно изменено");
            $('.help').css("background-color", "rgba(0, 255, 0, 0.1)");
            $('.help').css("border", "1px solid rgba(0, 255, 0, 0.3)");
            $('.help').css("color", "green");
            $('.help').css("opacity", "1");
        }
    });

}

function changePsw() {
    oldPsw = $('#oldPsw').val();
    newPsw = $('#newPsw').val();
    confirm = $('#confirm').val();

    if (oldPsw == "" || newPsw == "" || confirm == "") {
        $('.viewSet').scrollTop(0);
        $('.help').html("Не все поля заполнены");
        $('.help').css("background-color", "rgba(255, 0, 0, 0.1)");
        $('.help').css("border", "1px solid rgba(255, 0, 0, 0.3)");
        $('.help').css("color", "red");
        $('.help').css("opacity", "1");
        return;
    }
    if (newPsw.length < 6 || oldPsw.length < 6) {
        $('.viewSet').scrollTop(0);
        $('.help').html("Пароль должен иметь как минимум 6 символов");
        $('.help').css("background-color", "rgba(255, 0, 0, 0.1)");
        $('.help').css("border", "1px solid rgba(255, 0, 0, 0.3)");
        $('.help').css("color", "red");
        $('.help').css("opacity", "1");
        $('#newPsw').val("");
        $('#confirm').val("");
        return;
    }
    if (newPsw != confirm) {
        $('.viewSet').scrollTop(0);
        $('.help').html("Пароли не совпадают");
        $('.help').css("background-color", "rgba(255, 0, 0, 0.1)");
        $('.help').css("border", "1px solid rgba(255, 0, 0, 0.3)");
        $('.help').css("color", "red");
        $('.help').css("opacity", "1");
        $('#newPsw').val("");
        $('#confirm').val("");
        return;
    }

    var arr = {
        oldPsw: oldPsw,
        newPsw: newPsw
    };
    $.ajax({
        url: '/api/change/password',
        type: 'POST',
        data: JSON.stringify(arr),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false
    }).done(function(data) {
        if (data.response == "Error") {
            $('.viewSet').scrollTop(0);
            $('#oldPsw').val("");
            $('.help').html("Текущий пароль введен неверно");
            $('.help').css("background-color", "rgba(255, 0, 0, 0.1)");
            $('.help').css("border", "1px solid rgba(255, 0, 0, 0.3)");
            $('.help').css("color", "red");
            $('.help').css("opacity", "1");
            return;
        } else {
            $('.viewSet').scrollTop(0);
            $('#oldPsw').val("");
            $('#newPsw').val("");
            $('#confirm').val("");
            $('.help').html("Пароль успешно изменен");
            $('.help').css("background-color", "rgba(0, 255, 0, 0.1)");
            $('.help').css("border", "1px solid rgba(0, 255, 0, 0.3)");
            $('.help').css("color", "green");
            $('.help').css("opacity", "1");
        }
    });

}