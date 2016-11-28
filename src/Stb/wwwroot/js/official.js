// Write your Javascript code.
function showError() {
    $("#publish_result").text("发布失败！系统出现异常，请稍后再试。").css("color", "red");
    $("#service_button").removeClass("disabled");
}

function showSuccess() {
    $("#publish_result").text("发布成功！稍后会有客服人员与您联系，请保持手机畅通！").css("color", "forestgreen")
    $("#service_button").removeClass("disabled");
}

function publishBegin() {
    $("#service_button").addClass("disabled");
}
