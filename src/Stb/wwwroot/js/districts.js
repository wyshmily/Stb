// Write your Javascript code.

$(document).ready(function () {

    var provinceSelect = $("#provinceSelect");
    if (provinceSelect.size() > 0) {
        var citySelect = $("#citySelect");
        var districtSelect = $("#districtSelect");
        $.getJSON("http://restapi.amap.com/v3/config/district?key=1ff97f3d9068e5e12e21f3c34480096a&subdistrict=1")
        .success(function (data) {
            if (data.status == 1) {
                provinceSelect.empty();
                $.each(data.districts[0].districts, function (i, district) {
                    $("<option></option>").val(district.adcode).text(district.name).appendTo(provinceSelect);
                });

                getCityList();
            }
        });

        provinceSelect.change(function () {
            getCityList();
        });

        citySelect.change(function () {
            getDistrictList();
        });

        $("#selectAllDistrict").click(function () {
            if ($(this).prop("checked")) {
                districtSelect.find("input:unchecked").click();
            } else {
                districtSelect.find("input:checked").click();
            }
        });


    }
});



function getCityList() {
    var provinceSelect = $("#provinceSelect");
    var citySelect = $("#citySelect");
    var districtSelect = $("#districtSelect");
    $.getJSON("http://restapi.amap.com/v3/config/district?key=1ff97f3d9068e5e12e21f3c34480096a&subdistrict=1&level=province&keywords="
        + provinceSelect.val())
            .success(function (data) {
                if (data.status == 1) {
                    citySelect.empty();

                    if (data.districts[0].districts.length > 0) {
                        $.each(data.districts[0].districts, function (i, district) {
                            $("<option></option>").val(district.adcode).text(district.name).appendTo(citySelect);
                        });
                        getDistrictList();
                    } else {
                        var district = data.districts[0];
                        $("<option></option>").val(district.adcode).text(district.name).appendTo(citySelect);
                        districtSelect.empty();
                        appendDistrictItem(district);
                    }

                }
            });
}

function getDistrictList() {
    var citySelect = $("#citySelect");
    var districtSelect = $("#districtSelect");
    $.getJSON("http://restapi.amap.com/v3/config/district?key=1ff97f3d9068e5e12e21f3c34480096a&subdistrict=1&level=city&keywords="
        + citySelect.val())
            .success(function (data) {
                if (data.status == 1) {
                    districtSelect.empty();

                    if (data.districts[0].districts.length > 0) {
                        $.each(data.districts[0].districts, function (i, district) {
                            appendDistrictItem(district);
                        });
                    } else {
                        var district = data.districts[0];
                        appendDistrictItem(district);
                    }
                }
            });
}

function appendDistrictItem(district) {
    var provinceSelect = $("#provinceSelect");
    var citySelect = $("#citySelect");
    var districtSelect = $("#districtSelect");
    var districts = $("#Districts");


    var input = $("<input type=\"checkbox\" />")
        .val(district.adcode)
        .text(district.name)
        .attr("id", district.adcode)
        .prop("checked", districts.find("#li_" + district.adcode).size() != 0)
        .click(function () {

            if ($(this).prop("checked")) {
                if (districts.find("#li_" + district.adcode).size() != 0) {
                    return;
                }
                var provinceAdcode = provinceSelect.val();
                var provinceName = provinceSelect.find("option:selected").text();
                var cityAdcode = citySelect.val();
                var cityName = citySelect.find("option:selected").text();
                var districtAdcode = district.adcode;
                var districtName = district.name;

                var removeBtn = $("<span class=\"input-group-btn\"></span>")
                    .append($("<button class=\"btn btn-default remove-district\" type=\"button\">&times;</button>")
                        .val(districtAdcode).click(function () {
                            districts.find("#li_" + districtAdcode).remove();
                            districtSelect.find("#" + districtAdcode).prop("checked", false);
                            AdjustDistrictHiddenInputName();
                        }));

                var inputGroup = $("<div class=\"input-group\"></div>")
                    .append($("<input type=\"button\" class=\"text-left form-control\" />")
                        .val(provinceName + " " + cityName + " " + districtName))
                    .append(removeBtn);

                $("<li></li>").append(inputGroup)
                    .append($("<input type=\"hidden\" />").val(provinceAdcode))
                    .append($("<input type=\"hidden\" />").val(provinceName))
                    .append($("<input type=\"hidden\" />").val(cityAdcode))
                    .append($("<input type=\"hidden\" />").val(cityName))
                    .append($("<input type=\"hidden\" />").val(districtAdcode))
                    .append($("<input type=\"hidden\" />").val(districtName))
                    .attr("id", "li_" + districtAdcode)
                    .appendTo(districts);
                AdjustDistrictHiddenInputName();

            } else {
                districts.find("#li_" + district.adcode).remove();
                AdjustDistrictHiddenInputName();
            }
        });


    $("<li></li>")
        .append($("<div class=\"checkbox\"></div>")
            .append(input)
            .append($("<label><label>").attr("for", district.adcode).text(district.name)))
        .appendTo(districtSelect);
}

function AdjustDistrictHiddenInputName() {
    $("#Districts").find("li").each(function (i, li) {
        $(this).find("input[type=hidden]")
            .first().attr("name", "Districts[" + i + "].ProvinceAdcode")
            .next().attr("name", "Districts[" + i + "].ProvinceName")
            .next().attr("name", "Districts[" + i + "].CityAdcode")
            .next().attr("name", "Districts[" + i + "].CityName")
            .next().attr("name", "Districts[" + i + "].DistrictAdcode")
            .next().attr("name", "Districts[" + i + "].DistrictName");
    });
}

$(document).ready(function () {
    $(".remove-district").click(function () {
        var districtAdcode = $(this).val();
        $("#Districts").find("#li_" + districtAdcode).remove();
        $("#districtSelect").find("#" + districtAdcode).prop("checked", false);
        AdjustDistrictHiddenInputName();
    });
});