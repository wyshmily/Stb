// Write your Javascript code.

//

var districtUtil = {
    provinceInited: false,
    cityInited: false,
    districtInited: false,
    platoonInited: false,
};

$(document).ready(function () {

    var provinceSelect = $("#provinceSelect");
    if (provinceSelect.size() > 0) {
        var citySelect = $("#citySelect");
        var districtSelect = $("#districtSelect");
        $.getJSON("http://restapi.amap.com/v3/config/district?key=1ff97f3d9068e5e12e21f3c34480096a&subdistrict=1")
            .success(function (data) {
                if (data.status == 1) {
                    $("<option></option>").val(null).text("请选择所在省（直辖市）").appendTo(provinceSelect)
                    $.each(data.districts[0].districts, function (i, district) {
                        $("<option></option>").val(district.adcode).text(district.name).appendTo(provinceSelect);
                    });

                    if (!districtUtil.provinceInited) {
                        districtUtil.provinceInited = true;
                        var province = $("#District_ProvinceAdcode");
                        if (province.val()) {
                            provinceSelect.find("option[value=" + province.val() + "]").attr("selected", true);
                            getCityList();
                        }
                    }
                }
            });

        provinceSelect.change(function () {
            $("#District_ProvinceAdcode").val($(this).find("option:selected").val());
            $("#District_CityAdcode").val(null);
            $("#District_DistrictAdcode").val(null);
            $("#citySelect").empty();
            $("#districtSelect").empty();
            clearPlatoon();
            getCityList();
        });

        citySelect.change(function () {
            $("#District_CityAdcode").val($(this).find("option:selected").val());
            $("#districtSelect").empty();
            clearPlatoon();
            getDistrictList();
        });

        districtSelect.change(function () {
            $("#District_DistrictAdcode").val($(this).find("option:selected").val());

            clearPlatoon();
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


function clearPlatoon() {
    var platoonTypeahead = $("#PlatoonTypeahead");
    if (platoonTypeahead) {
        $("#PlatoonTypeahead").typeahead('val', "-1");
        $("#PlatoonTypeahead").typeahead('val', "");
        $("#PlatoonId").val(null);
        $("#PlatoonName").val(null);
    }
}

function getCityList() {
    var provinceSelect = $("#provinceSelect");
    var citySelect = $("#citySelect");
    if (!provinceSelect.find("option:selected").val()) {
        return;
    }

    var districtSelect = $("#districtSelect");
    $.getJSON("http://restapi.amap.com/v3/config/district?key=1ff97f3d9068e5e12e21f3c34480096a&subdistrict=1&level=province&keywords="
        + provinceSelect.val())
        .success(function (data) {
            if (data.status == 1) {

                if (data.districts[0].districts.length > 0) {
                    $("<option></option>").val(null).text("请选择所在城市").appendTo(citySelect)
                    $.each(data.districts[0].districts, function (i, district) {
                        $("<option></option>").val(district.adcode).text(district.name).appendTo(citySelect);
                    });
                } else {
                    var district = data.districts[0];
                    $("#District_CityAdcode").val(district.adcode);
                    $("#District_DistrictAdcode").val(district.adcode);
                    $("<option></option>").val(district.adcode).text(district.name).appendTo(citySelect);
                    appendDistrictItem(district, districtSelect.get(0).tagName);
                }

                if (!districtUtil.cityInited) {
                    districtUtil.cityInited = true;
                    var city = $("#District_CityAdcode");
                    if (city.val()) {
                        citySelect.find("option[value=" + city.val() + "]").attr("selected", true);
                        getDistrictList();
                    }
                }
            }
        });
}

function getDistrictList() {
    var citySelect = $("#citySelect");
    var districtSelect = $("#districtSelect");

    if (!citySelect.find("option:selected").val()) {
        return;
    }


    $.getJSON("http://restapi.amap.com/v3/config/district?key=1ff97f3d9068e5e12e21f3c34480096a&subdistrict=1&level=city&keywords="
        + citySelect.val())
        .success(function (data) {
            if (data.status == 1) {


                if (data.districts[0].districts.length > 0) {
                    $("<option></option>").val(null).text("请选择所在区（县）").appendTo(districtSelect)
                    $.each(data.districts[0].districts, function (i, district) {
                        appendDistrictItem(district, districtSelect.get(0).tagName);
                    });
                } else {
                    var district = data.districts[0];
                    $("#District_DistrictAdcode").val(district.adcode);
                    appendDistrictItem(district, districtSelect.get(0).tagName);
                }

                if (!districtUtil.districtInited) {
                    districtUtil.districtInited = true;
                    var district = $("#District_DistrictAdcode");
                    if (district.val()) {
                        console.log(district.val());
                        districtSelect.find("option[value=" + district.val() + "]").attr("selected", true);

                        if (!districtUtil.platoonInited) {
                            var platoonTypeahead = $("#PlatoonTypeahead");
                            if (platoonTypeahead) {
                                if ($("#PlatoonName").val()) {
                                    $("#PlatoonTypeahead").typeahead('val', "-1");
                                    $("#PlatoonTypeahead").typeahead('val', $("#PlatoonName").val());
                                }
                            }
                        }
                        if (lbs && !lbs.locationInited) {
                            lbs.locationInited = true;
                            var workAddress = $("#WorkAddress");
                            lbs.geocoder.setCity(district.val());
                            console.log(workAddress.val());
                            if (workAddress.val()) {
                                getAddressLocation();
                            } else if (district.val()) {
                                lbs.map.setCity(district.val());
                            } else {
                                lbs.map.setCity("北京市");
                            }
                        }
                    }
                }
            }
        });
}

function appendDistrictItem(district, tag) {
    var provinceSelect = $("#provinceSelect");
    var citySelect = $("#citySelect");
    var districtSelect = $("#districtSelect");
    var districts = $("#Districts");

    if (tag.toLowerCase() == "select") {
        $("<option></option>").val(district.adcode).text(district.name).appendTo(districtSelect)
        return;
    }

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
    $("#Districts").children("li").each(function (i, li) {
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