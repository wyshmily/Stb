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

    var userJobClassLi = jobClasses.find("#li_job_" + jobClass.id);

    var input = $("<input type=\"checkbox\" />")
        .val(jobClass.id)
        .text(jobClass.name)
        .attr("id", "job_" + jobClass.id)
        .prop("checked", userJobClassLi.size() != 0)
        .click(function () {

            if ($(this).prop("checked")) {
                if (jobClasses.find("#li_job_" + jobClass.id).size() != 0) {
                    return;
                }
                var jobCategoryId = jobCategorySelect.val();
                var jobCategoryName = jobCategorySelect.find("option:selected").text();
                var jobClassId = jobClass.id;
                var jobClassName = jobClass.name;
                var grade = 0;
                var gradeName = "标准工"


                var selectBtn = $("<div class=\"input-group-btn\"></span>")
                    .append($("<button type=\"button\" class=\"btn btn-default dropdown-toggle jobgrade-select\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">")
                        .append($("<span></span>").text(gradeName))
                        .append($("<span class=\"caret\"></span></button>")));

                var dropDown = $("<ul class=\"dropdown-menu dropdown-menu-right\">")
                    .append($("<li><a href=\"#\" data-jobgrade=\"0\" class=\"jobgrade-option\">标准工</a></li>"))
                    .append($("<li><a href=\"#\" data-jobgrade=\"1\" class=\"jobgrade-option\">大　工</a></li>"))
                    .append($("<li><a href=\"#\" data-jobgrade=\"2\" class=\"jobgrade-option\">小　工</a></li>"));

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
                    .append(selectBtn.append(dropDown))
                    .append(removeBtn);

                $("<li></li>")
                    .append($("<input type=\"hidden\" />").val(jobCategoryId))
                    .append($("<input type=\"hidden\" />").val(jobCategoryName))
                    .append($("<input type=\"hidden\" />").val(jobClassId))
                    .append($("<input type=\"hidden\" />").val(jobClassName))
                    .append($("<input type=\"hidden\" />").val(grade))
                    .append(inputGroup)
                    .attr("id", "li_job_" + jobClassId)
                    .appendTo(jobClasses);

                AdjustJobClassHiddenInputName();
                AttachJobGradeSelectEvent();


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
    $("#JobClasses").children("li").each(function (i, li) {
        $(this).find("select").attr("name", "JobClasses[" + i + "].Grade");
        $(this).find("input[type=hidden]")
            .first().attr("name", "JobClasses[" + i + "].JobCategoryId")
            .next().attr("name", "JobClasses[" + i + "].JobCategoryName")
            .next().attr("name", "JobClasses[" + i + "].JobClassId")
            .next().attr("name", "JobClasses[" + i + "].JobClassName")
            .next().attr("name", "JobClasses[" + i + "].Grade")
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

$(document).ready(function () {
    AttachJobGradeSelectEvent();
});

function AttachJobGradeSelectEvent() {
    $(".jobgrade-option").click(function () {
        event.preventDefault();
        var grade = $(this).attr("data-jobgrade");
        var gradeName = $(this).text();

        $(this).parents(".input-group-btn").children("button").find("span").first().text(gradeName);
        $(this).parents(".input-group").prev().val(grade);
    });
}