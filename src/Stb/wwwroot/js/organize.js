$(document).ready(function () {

    $("#ModelAddOrderWorker").bind('shown.bs.modal', function (event) {
        getWorkerList();
    });

    $("#buttonAddOrderWorker").click(function (event) {
        event.preventDefault();
        $("#ModelAddOrderWorker").modal('show');
    });
    var timer;
    var delay = 600; // 0.4 seconds delay after last input
    $('#WorkerSearchInput').bind('input', function () {
        window.clearTimeout(timer);
        timer = window.setTimeout(function () {
            getWorkerList();
        }, delay);
    });

    $('.btn-delete-worker').click(function () {
        event.preventDefault();
        $(this).parents("tr").remove();
    })

    $("#btn_set_workers").click(function (event) {
        event.preventDefault();
        var rows = $("#workerList").children("tr");
        var leaderId;
        var workerIds = $.map(rows, function (tr) {
            var isLeader = $(tr).attr("data-is-leader");
            if (isLeader && isLeader.toLowerCase() == "true")
                leaderId = $(tr).attr("data-worker-id");
            return $(tr).attr("data-worker-id");
        });
        if (!leaderId) {
            bootstrap_alert.danger("#SaveWorkersAlertHolder", "请为工单安排班长和工人", 2000);
            return;
        }
        $.post("../../../api/order/workers", {
            orderId: $("#OrderId").val(),
            leaderId: leaderId,
            workerIds: workerIds,
        }).error(function (data) {
            bootstrap_alert.danger("#SaveWorkersAlertHolder", "错误（" + data.status + "）：" + data.responseText, 2000);
        }).success(function (data) {
            //bootstrap_alert.success("#SaveWorkersAlertHolder", "已保存！", 1000);
            window.location = '../index';
        });
    });

    $(".btn-cancel-leader").click(function (event) {
        event.preventDefault();
        if ($(this).text() == "设为本工单班长") {
            $(this).text("取消本工单班长");
            $(this).addClass("text-success");
            $(this).parents("tr").attr("data-is-leader", true);
            $(this).parents("tr").children("td").first().children("span").removeClass("hidden");
        } else {
            $(this).text("设为本工单班长");
            $(this).removeClass("text-success");
            $(this).parents("tr").attr("data-is-leader", false);
            $(this).parents("tr").children("td").first().children("span").addClass("hidden");
        }
    })

});

function getWorkerList() {
    var searcher = $("#WorkerSearchInput");
    $.getJSON("../../../api/worker", { search: searcher.val() })
        .success(function (data) {
            var body = $("#WorkerTableBody");
            body.empty();
            $.each(data, function (i, worker) {
                var nameBtn = $("<a target=\"_blank\"></a>")
                    .attr("href", "/platform/Worker/Details/" + worker.id)
                    .text(worker.name)
                    .prepend("<span class=\"glyphicon glyphicon-user\" aria-hidden=\"true\"></span>");

                var type = "";
                if (worker.isHeader) {
                    type = "班长";
                } else if (worker.isCandidate) {
                    type = "侯选班长";
                } else {
                    type = "工人";
                }

                var addBtn = $("<a href=\"#\">添加</a>");
                addBtn.click(function (event) {
                    event.preventDefault();
                    if ($("#workerList").find("tr[data-worker-id = " + worker.id + "]").size() > 0) {
                        bootstrap_alert.warning("#SelectWorkerAlertHolder", "该工人已经在列表中！", 2000);
                        return;
                    }

                    var operateTr = $("<td></td>");
                    var delBtn = $("<a href=\"#\">删除</a>");
                    delBtn.click(function (event) {
                        event.preventDefault();
                        $(this).parents("tr").remove();
                    });
                    operateTr.append(delBtn);
                    if (worker.isHeader || worker.isCandidate) {
                        setLeaderBtn = $("<a href=\"#\">设为本工单班长</a>");
                        setLeaderBtn.click(function (event) {
                            event.preventDefault();
                            if ($(this).text() == "设为本工单班长") {
                                $(this).text("取消本工单班长");
                                $(this).addClass("text-success");
                                $(this).parents("tr").attr("data-is-leader", true);
                                $(this).parents("tr").children("td").first().children("span").removeClass("hidden");
                            } else {
                                $(this).text("设为本工单班长");
                                $(this).removeClass("text-success");
                                $(this).parents("tr").attr("data-is-leader", false);
                                $(this).parents("tr").children("td").first().children("span").addClass("hidden");
                            }

                        });
                        operateTr.append(" | ").append(setLeaderBtn);
                    }
                    var newWorker = $("<tr></tr>").attr("data-worker-id", worker.id)
                        .append($("<td><span class=\"text-warning hidden\">班长</span></td>"))
                        .append($("<td></td>").append(nameBtn.clone()))
                        .append($("<td></td>").text(worker.userName))
                        .append($("<td></td>").text(type))
                        .append(operateTr);
                    $("#workerList").append(newWorker);
                });

                $("<tr></tr>")
                    .append($("<td></td>").append(nameBtn))
                    .append($("<td></td>").text(worker.userName))
                    .append($("<td></td>").text(type))
                    .append($("<td></td>").append(addBtn))
                    .appendTo(body);
            });
        });
}

$(document).ready(function () {
    $(".btn-del-worker").click(function (event) {
        event.preventDefault();
        $(this).parents("tr").remove();
    });
});


