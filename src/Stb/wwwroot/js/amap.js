var lbs = {
    map: null,
    geocoder: null,
    marker: null,
    infoWindow: null,
    locationInited: false,
};

function mapInit() {
    lbs.map = new AMap.Map('container', {
        zoom: 12
    });


    AMap.service('AMap.Geocoder', function () {//回调函数
        //实例化Geocoder
        lbs.geocoder = new AMap.Geocoder({
        });
        //TODO: 使用geocoder 对象完成相关功能
    })

    $("#provinceSelect").change(function () {
        var city = $(this).find("option:selected").val();
        if (city) {
            lbs.map.setCity(city);
            lbs.geocoder.setCity(city);
        }
    });

    $("#citySelect").change(function () {
        var city = $(this).find("option:selected").val();
        if (city) {
            lbs.map.setCity(city);
            lbs.geocoder.setCity(city);
        }
    });

    $("#districtSelect").change(function () {
        var city = $(this).find("option:selected").val();
        if (city) {
            lbs.map.setCity(city);
            lbs.geocoder.setCity(city);
        }
    });


    
}

$(document).ready(function () {
    

    var timer;
    var delay = 600; // 0.4 seconds delay after last input
    $('#WorkAddress').bind('input', function () {
        window.clearTimeout(timer);
        timer = window.setTimeout(function () {
            getAddressLocation();
        }, delay);
    });
})

function getAddressLocation() {
    lbs.geocoder.getLocation($("#WorkAddress").val(), function (status, result) {

        if (status == "complete") {

            var location = result.geocodes[0].location;
            var address = result.geocodes[0].formattedAddress;
            if (lbs.marker != null) {
                lbs.marker.setPosition(location);
                lbs.infoWindow.close();
                lbs.infoWindow.setContent("施工地址<br>   " + address);
            } else {
                lbs.marker = new AMap.Marker({
                    map: lbs.map,
                    position: result.geocodes[0].location,
                });
                lbs.infoWindow = new AMap.InfoWindow({
                    content: "施工地址<br>   " + address,
                    closeWhenClickMap: true,
                    offset: { x: 0, y: -30 }
                });
                lbs.marker.on("mouseover", function (e) {
                    lbs.infoWindow.open(lbs.map, lbs.marker.getPosition());
                });
            }
            lbs.map.setCenter(location);
            $("#WorkLocation").val(location);
            $("#WorkAddress").removeClass("form-contorl-text-danger");
        } else {
            $("#WorkAddress").addClass("form-contorl-text-danger");
        }
    });
}