// Write your Javascript code.

$(document).ready(function () {

    var jobCategorySelect = $("#jobCategorySelect");
    if (jobCategorySelect.size() > 0) {
        var jobClassSelect = $("#jobClassSelect");
        $.getJSON("../../../api/jobCategory")
        .success(function (data) {
            if (data.length > 0) {
                jobCategorySelect.empty();
                $.each(data, function (i, jobCateory) {
                    $("<option></option>").val(jobCateory.id).text(jobCateory.name).appendTo(jobCategorySelect);
                });

                getJobClassList();
            }
        });

        jobCategorySelect.change(function () {
            getJobClassList();
        })

        $("#selectAllJobClass").click(function () {
            if ($(this).prop("checked")) {
                jobClassSelect.find("input:unchecked").click();
            } else {
                jobClassSelect.find("input:checked").click();
            }
        });
    }
});

function getJobClassList() {
    var jobCategorySelect = $("#jobCategorySelect");
    var jobClassSelect = $("#jobClassSelect");
    $.getJSON("../../../api/jobClass", { categoryId: jobCategorySelect.val() })
        .success(function (data) {
            if (data) {
                jobClassSelect.empty();

                if (data.length > 0) {
                    $.each(data, function (i, jobClass) {
                        appendJobClassItem(jobClass);
                    });
                }
            }
        });
}

function appendJobClassItem(jobClass) {
    var jobCategorySelect = $("#jobCategorySelect");
    var jobClassSelect = $("#jobClassSelect");
    var jobClasses = $("#JobClasses");


    var input = $("<input type=\"checkbox\" />")
        .val(jobClass.id)
        .text(jobClass.name)
        .attr("id", "job_" + jobClass.id)
        .prop("checked", jobClasses.find("#li_job_" + jobClass.id).size() != 0)
        .click(function () {

            if ($(this).prop("checked")) {
                if (jobClasses.find("#li_job_" + jobClass.id).size() != 0) {
                    return;
                }
                var jobCategoryId = jobCategorySelect.val();
                var jobCategoryName = jobCategorySelect.find("option:selected").text();
                var jobClassId = jobClass.id;
                var jobClassName = jobClass.name;

                var removeBtn = $("<span class=\"input-group-btn\"></span>")
                    .append($("<button class=\"btn btn-default remove-jobclass\" type=\"button\">&times;</button>")
                        .val("job_" + jobClassId).click(function () {
                            jobClasses.find("#li_job_" + jobClassId).remove();
                            jobClassSelect.find("#job_" + jobClassId).prop("checked", false);
                            AdjustJobClassHiddenInputName();
                        }));

                var inputGroup = $("<div class=\"input-group\"></div>")
                    .append($("<input type=\"button\" class=\"text-left form-control\" />")
                        .val(jobCategoryName + " " + jobClassName))
                    .append(removeBtn);

                $("<li></li>").append(inputGroup)
                    .append($("<input type=\"hidden\" />").val(jobCategoryId))
                    .append($("<input type=\"hidden\" />").val(jobCategoryName))
                    .append($("<input type=\"hidden\" />").val(jobClassId))
                    .append($("<input type=\"hidden\" />").val(jobClassName))
                    .attr("id", "li_job_" + jobClassId)
                    .appendTo(jobClasses);

                AdjustJobClassHiddenInputName();

            } else {
                jobClasses.find("#li_job_" + jobClass.id).remove();
                AdjustJobClassHiddenInputName();
            }
        });


    $("<li></li>")
         .append($("<div class=\"checkbox\"></div>")
             .append(input)
             .append($("<label><label>").attr("for", "job_" + jobClass.id).text(jobClass.name)))
         .appendTo(jobClassSelect);
}

function AdjustJobClassHiddenInputName() {
    $("#JobClasses").find("li").each(function (i, li) {
        $(this).find("input[type=hidden]")
            .first().attr("name", "JobClasses[" + i + "].JobCategoryId")
            .next().attr("name", "JobClasses[" + i + "].JobCategoryName")
            .next().attr("name", "JobClasses[" + i + "].JobClassId")
            .next().attr("name", "JobClasses[" + i + "].JobClassName")
    });
}

$(document).ready(function () {
    $(".remove-jobclass").click(function () {
        var jobClassId = $(this).val();
        $("#JobClasses").find("#li_" + jobClassId).remove();
        $("#jobClassSelect").find("#" + jobClassId).prop("checked", false);
        AdjustJobClassHiddenInputName();
    });
});
