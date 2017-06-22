var host = location.href;
function BoxIsEmpty(object) {
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

$(function() {
    $('#url').keydown(function(e) {
        if (e.keyCode == 13) {
			
            if (BoxMsgIsEmpty('#url')) {
                return;
                }
        }
    });
});

function loadShortUrl() {
    $.getJSON('/api/urls/shorturl', function(data) {
		$('#url').val(host+data.shorturl);
    });
}

function create_url() {
    var url = $('#url').val();
    if (BoxIsEmpty('#url')) {
        return;
    }
    var arr = {
        url: url
    };
    $.ajax({
        url: '/api/urls/add',
        type: 'POST',
        data: JSON.stringify(arr),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: false
    });
    $('#url').val("");
    loadShortUrl();
}