// Write your Javascript code.

$(document).ready(function () {
    var jobCategorySelect = $("#workLoadJobCategorySelect");
    var jobClassSelect = $("#workLoadJobClassSelect");
    if (jobCategorySelect.size() > 0) {

        $.getJSON("../../../api/jobCategory")
            .success(function (data) {
                if (data.length > 0) {
                    jobCategorySelect.empty();
                    $.each(data, function (i, jobCateory) {
                        $("<option></option>").val(jobCateory.id).text(jobCateory.name).appendTo(jobCategorySelect);
                    });

                    getWorkLoadJobClassList();
                }
            });

        jobCategorySelect.change(function () {
            getWorkLoadJobClassList();
        });
        jobClassSelect.change(function () {
            getWorkLoadJobMeasurementList();
        });
    }
});

function getWorkLoadJobClassList() {
    var jobCategorySelect = $("#workLoadJobCategorySelect");
    var jobClassSelect = $("#workLoadJobClassSelect");
    $.getJSON("../../../api/jobClass", { categoryId: jobCategorySelect.val() })
        .success(function (data) {
            if (data) {
                jobClassSelect.empty();

                if (data.length > 0) {
                    $.each(data, function (i, jobClass) {
                        $("<option></option>").val(jobClass.id).text(jobClass.name).appendTo(jobClassSelect);
                    });
                }

                getWorkLoadJobMeasurementList();
            }
        });
}

function getWorkLoadJobMeasurementList() {
    var jobClassSelect = $("#workLoadJobClassSelect");
    var jobMeasurementSelect = $("#workLoadJobMeasurementSelect");
    $.getJSON("../../../api/jobMeasurement", { classId: jobClassSelect.val() })
        .success(function (data) {
            if (data) {
                jobMeasurementSelect.empty();
                if (data.length > 0) {
                    $.each(data, function (i, jobMeasurement) {
                        appendJobMeasurementItem(jobMeasurement);
                    });
                }
            }
        });
}

function appendJobMeasurementItem(jobMeasurement) {
    var jobCategorySelect = $("#workLoadJobCategorySelect");
    var jobClassSelect = $("#workLoadJobClassSelect");
    var jobMeasurementSelect = $("#workLoadJobMeasurementSelect");
    var workLoads = $("#WorkLoads");

    var userWorkLoadLi = workLoads.find("#li_" + jobMeasurement.id);

    var input = $("<input type=\"checkbox\" />")
        .val(jobMeasurement.id)
        .text(jobMeasurement.name)
        .attr("id", "" + jobMeasurement.id)
        .prop("checked", userWorkLoadLi.size() != 0)
        .click(function () {

            if ($(this).prop("checked")) {
                if (workLoads.find("#li_" + jobMeasurement.id).size() != 0) {
                    return;
                }
                var jobMeasurementId = jobMeasurement.id;
                var jobMeasurementName = jobMeasurement.name;
                var amount = 1;
                var unit = jobMeasurement.unit;
                var orderId = $("#Id").val();

                var amountInput = $('<input type="text" class="form-control" />').val(amount)

                var removeBtn = $("<span class=\"input-group-btn\"></span>")
                    .append($("<button class=\"btn btn-default remove-workload\" type=\"button\">&times;</button>")
                        .val("" + jobMeasurementId).click(function () {
                            workLoads.find("#li_" + jobMeasurementId).remove();
                            jobMeasurementSelect.find("#" + jobMeasurementId).prop("checked", false);
                            AdjustWorkLoadHiddenInputName();
                        }));

                var inputGroup = $("<div class=\"input-group\"></div>")
                    .append($("<span class=\"input-group-addon\"></span>")
                        .text(jobMeasurementName))
                    .append(amountInput)
                    .append($("<span class=\"input-group-addon\"></span>")
                        .text(unit))
                    .append(removeBtn);

                $("<li></li>")
                    //.append($("<input type=\"hidden\" />").val(jobCategoryId))
                    //.append($("<input type=\"hidden\" />").val(jobCategoryName))
                    //.append($("<input type=\"hidden\" />").val(jobClassId))
                    //.append($("<input type=\"hidden\" />").val(jobClassName))
                    .append($("<input type=\"hidden\" />").val(jobMeasurementId))
                    .append($("<input type=\"hidden\" />").val(jobMeasurementName))
                    .append($("<input type=\"hidden\" />").val(orderId))
                    .append($("<input type=\"hidden\" />").val(null))
                    .append(inputGroup)
                    .attr("id", "li_" + jobMeasurementId)
                    .appendTo(workLoads);

                AdjustWorkLoadHiddenInputName();
                AttachJobGradeSelectEvent();

            } else {
                workLoads.find("#li_" + jobMeasurement.id).remove();
                AdjustWorkLoadHiddenInputName();
            }
        });


    $("<li></li>")
        .append($("<div class=\"checkbox\"></div>")
            .append(input)
            .append($("<label><label>").attr("for", "" + jobMeasurement.id).text(jobMeasurement.name)))
        .appendTo(jobMeasurementSelect);
}

function AdjustWorkLoadHiddenInputName() {
    $("#WorkLoads").children("li").each(function (i, li) {
        $(this).find("input[type=text]")
            .first().attr("name", "WorkLoads[" + i + "].Amount")
        $(this).find("input[type=hidden]")
            //.first().attr("name", "WorkLoads[" + i + "].JobCategoryId")
            //.next().attr("name", "WorkLoads[" + i + "].JobCategoryName")
            //.next().attr("name", "WorkLoads[" + i + "].JobClassId")
            //.next().attr("name", "WorkLoads[" + i + "].JobClassName")
            .first().attr("name", "WorkLoads[" + i + "].JobMeasurementId")
            .next().attr("name", "WorkLoads[" + i + "].JobMeasurementName")
            .next().attr("name", "WorkLoads[" + i + "].OrderId")
            .next().attr("name", "WorkLoads[" + i + "].WorkerId")
    });
}

$(document).ready(function () {
    $(".remove-workload").click(function () {
        var jobMeasurementId = $(this).val();
        $("#WorkLoads").find("#li_" + jobMeasurementId).remove();
        $("#workLoadJobMeasurementSelect").find("#" + jobMeasurementId).prop("checked", false);
        AdjustWorkLoadHiddenInputName();
    });
});

