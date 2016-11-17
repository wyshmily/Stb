$(document).ready(function () {

    var headSearchTimer;
    var delay = 600; // 0.4 seconds delay after last input

    $('#HeaderName').focusin(function () {
        window.clearTimeout(headSearchTimer);
        var searcher = $(this);
        headSearchTimer = window.setTimeout(function () {
            searcher.select();
            searchHeader();
        },100);
    })
    .focusout(function () {
        var searcher = $(this);
        window.setTimeout(function () {
            var old = searcher.attr("data-stb-header");
            if (old && old != searcher.val()) {
                searcher.val(old);
            }
        },300)
    });

    $('#HeaderName').bind('input', function () {
        window.clearTimeout(headSearchTimer);
        headSearchTimer = window.setTimeout(function () {
            //insert delayed input change action/event here
            searchHeader();

        }, delay);
    });

    $('#IsHead').click(function () {
        TriggerHeaderDisplay();
    })

    TriggerHeaderDisplay();
});

function TriggerHeaderDisplay() {
    if ($('#IsHead').prop("checked")) {
        $("#HeaderSearchDiv").slideUp();
    } else {
        $("#HeaderSearchDiv").slideDown();
    }
}

function searchHeader() {
    var searcher = $("#HeaderName");
    $.getJSON("../../../api/worker/header", { search: searcher.val() })
        .success(function (data) {
            var dropdown = $("#HeaderDropDown");
            dropdown.empty();
            
            $("<li></li>")
                   .append($("<button class=\"btn-link btn\"></button>")
                        .text("没有班长")
                        .click(function () {
                            console.info(2);
                            event.preventDefault();
                            searcher.val("");
                            searcher.attr("data-stb-header", "")
                            $("#HeaderId").val(null);
                        }))
                   .appendTo(dropdown);

            $.each(data, function (i, header) {
                var option = $("<button class=\"btn-link btn\"></button>")
                        .text(header.name + "   " + header.userName)
                        .click(function () {
                            event.preventDefault();
                            searcher.val(header.name);
                            searcher.attr("data-stb-header", header.name)
                            $("#HeaderId").val(header.id);
                        });
                if (header.userName == $("#UserName").val()) {
                    option.prop("disabled", "true");
                }
                $("<li></li>")
                    .append(option)
                    .appendTo(dropdown);
            });
        });
}