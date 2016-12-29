bootstrap_alert = function () { }

$(document).ready(function () {
    $.getJSON("http://42.96.155.165:40003/Opark/Test/Carouses")
        .success(function (data) {
            console.log(data);
        })

    var timer;
    bootstrap_alert.warning = function (holder, message, autoDismiss) {
        $(holder)//.addClass("place-holder")
            .html('<div class="alert alert-warning fade in"><span>' + message + '</span></div>')
        window.clearTimeout(timer);
        if (autoDismiss) {
            timer = window.setTimeout(function () {
                $(holder).children().first().alert('close');
            }, autoDismiss);
        }
    };

    bootstrap_alert.danger = function (holder, message, autoDismiss) {
        $(holder)//.addClass("place-holder")
            .html('<div class="alert alert-danger fade in"><span>' + message + '</span></div>')
        window.clearTimeout(timer);
        if (autoDismiss) {
            timer = window.setTimeout(function () {
                $(holder).children().first().alert('close');
            }, autoDismiss)
        }
    };

    bootstrap_alert.success = function (holder, message, autoDismiss) {
        $(holder)//.addClass("place-holder")
            .html('<div class="alert alert-success fade in"><span>' + message + '</span></div>')
        window.clearTimeout(timer);
        if (autoDismiss) {
            timer = window.setTimeout(function () {
                $(holder).children().first().alert('close');
            }, autoDismiss)
        }
    };
});
