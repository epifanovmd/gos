$(document).ready(function() {
    $('#name').html(get_cookie("name"));
    $('<audio id="chatAudio"><source src="static/sounds/notify.wav" type="audio/mpeg"></audio>').appendTo('body');
    loadMessages();
    window.setInterval(loadNewMessage, 1000);
    setViewMessagesHeight();
});

$('#messages').scroll(function() {
    if ($('#messages').scrollTop() == 0) {
        setTimeout(function() {
            loadOldMessages();
        }, 200);

    }
    if ($('#messages').height() + $('#messages').scrollTop() == $('#messages')[0].scrollHeight) {
        $('.newMsg').css({
            width: "140px"
        });
        setTimeout(function() {
            $('.newMsg').css({
                left: "-300px",
                opacity: 0
            });
        }, 500);
    }
});

function loadOldMessages() {
    if (msgCount < 0) {
        return;
    }
    ofsetMsg = msgId - msgCount
    var arr = {
        ofsetMsg: ofsetMsg
    };
    $.ajax({
        url: '/api/messages/oldmessagelist',
        type: 'POST',
        data: JSON.stringify(arr),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false
    }).done(function(data) {
        if (data.error == "False") {
            return;
        }
        for (var i = 0; i < data.messages.length; i++) {
            var t = $('#messages').scrollTop();
            var message = document.createElement('div');
            msg = data.messages[i][1];
            message.innerHTML = "<em>" + data.messages[i][2] + "</em><br><div><strong>" + data.messages[i][0] + "</strong>&nbsp;&nbsp;:&nbsp;&nbsp;" + "<div>" + msg.replace(/\n/g, "<br>") + "</div></div>";
            message.className = "message";
            $('#messages').prepend(message);
            var h = $(message).outerHeight();
            $('#messages').scrollTop(t + h + 15);
        }
        msgCount = msgCount - 10;
    });
}

$(function() {
    $('#msg').keydown(function(e) {
        if (e.keyCode == 13) {
            if (e.ctrlKey && e.keyCode == 13) {
                if (BoxMsgIsEmpty('#msg')) {
                    return;
                }
                $('#msg').val($('#msg').val() + "\n");
                return;
            }
            e.preventDefault();
            send_message();
        }
    });
});

$(function() {
    $(window).resize(setViewMessagesHeight);
});

$(function() {
    $('.newMsg').click(function() {
        $('#messages').scrollTop(999999999999);
        $('.newMsg').css({
            width: "140px"
        });
        setTimeout(function() {
            $('.newMsg').css({
                left: "-300px",
                opacity: 0
            });
        }, 500);
    });
});

function setViewMessagesHeight() {
    $('#messages').css({
        height: $(window).height() - 142
    });
    $('.newMsg').css({
        top: $('#messages').height() + 27
    });
}

function BoxMsgIsEmpty(object) {
    if ($(object).val() == "" || !/./.test($(object).val())) {
        $(object).addClass('err');
        setTimeout(function() {
            $(object).removeClass('err');
        }, 3000);
        return 1;
    } else {
        return 0;
    }
}
var msgCount = 0;
var msgId = 0;

function loadMessages() {
    $.getJSON('/api/messages/list', function(data) {
        if (data.error == "False") {
            return;
        }
        for (var i = 0; i < data.messages.length; i++) {
            var message = document.createElement('div');
            msg = data.messages[i][1];
            message.innerHTML = "<em>" + data.messages[i][2] + "</em><br><div><strong>" + data.messages[i][0] + "</strong>&nbsp;&nbsp;:&nbsp;&nbsp;" + "<div>" + msg.replace(/\n/g, "<br>") + "</div></div>";
            message.className = "message";
            $('#messages').prepend(message);
            $('#messages').scrollTop(999999999999);
            msgId = data.messageId;
            msgCount = data.messageId - 10;
        }
    });
}

function loadNewMessage() {
    $.getJSON('/api/messages/NewMessage', function(data) {
        if (data.error == "False") {
            $('#messages').empty();
            return;
        }
        if (data.messageId > msgId) {
            msgId = data.messageId;
            var message = document.createElement('p');
            msg = data.message[0][1];
            message.innerHTML = "<em>" + data.message[0][2] + "</em><br><div><strong>" + data.message[0][0] + "</strong>&nbsp;&nbsp;:&nbsp;&nbsp;" + "<div>" + msg.replace(/\n/g, "<br>") + "</div></div>";
            message.className = "message";
            if ($('#messages').height() + $('#messages').scrollTop() == $('#messages')[0].scrollHeight) {
                $('#messages').append(message);
                $('#messages').scrollTop(999999999999);
            } else {
                $('#messages').append(message);
                $('.newMsg').css({
                    left: "20px",
                    opacity: 1
                });
                setTimeout(function() {
                    $('.newMsg').css({
                        width: $('#messages').width() - 10
                    });
                }, 500);
            }
            name = data.message[0][0];
            if (name != get_cookie("name")) {
                $('#chatAudio')[0].play();
            }
        }
    });
}

function get_cookie(cookie_name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + cookie_name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

function send_message() {
    var name = get_cookie("name");
    var message = $('#msg').val();
    if (name == "None")
        $(location).attr("href", "/login");
    if (BoxMsgIsEmpty('#msg')) {
        return;
    }
    var arr = {
        name: name,
        message: message
    };
    $.ajax({
        url: '/api/messages/add',
        type: 'POST',
        data: JSON.stringify(arr),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false
    });
    $('#msg').val("");
    loadNewMessage();
}

$(function() {
    $('.setBtn').click(function() {
        $('.help').css({
            opacity: 0
        });
        $('#forms input[type="text"], input[type="password"]').val("");
        $('.setBox').css("display", "block");
        setTimeout(function() {
            $('.setBox').css("opacity", "1");
        }, 1);
        setTimeout(function() {
            $('.settings').css("top", "10%");
        }, 500);
    });

    $('.close').click(function() {
        $('.settings').css("top", "-600px");
        setTimeout(function() {
            $('.setBox').css("opacity", "0");
        }, 500);
        setTimeout(function() {
            $('.setBox').css("display", "none");
        }, 1000);
    });
});

function logout() {
    $.ajax({
        url: '/logout',
        type: 'POST',
        data: "",
        contentType: 'application/json; charset=utf-8',
        dataType: 'text',
        async: false
    }).done(function(data) {
        if (data == "OK") {
            $(location).attr("href", "/login");
        }
    });
}