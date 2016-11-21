$(document).ready(function () {

    if ($("#ContractorTypeahead").size() == 0)
        return;

    if ($.validator) {
        $.validator.setDefaults({ ignore: '' });
    }



    var contractors = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 10,
        identify: function (datum) { return datum.id; },
        remote: {
            url: '../../../api/Contractor/Search?search=%QUERY',
            wildcard: '%QUERY',
            rateLimitWait: 200
        }
    });

    var contractorStaffs = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 10,
        identify: function (datum) { return datum.id; },
        remote: {
            url: '../../../api/ContractorStaff/Search?contractorId=%CONTRACTORID&search=%QUERY',
            prepare: function (query, settings) {
                settings.url = settings.url.replace("%CONTRACTORID", $("#ContractorId").val())
                    .replace("%QUERY", query);
                return settings;
            },
            rateLimitWait: 200
        }
    });

    var platoons = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        limit: 10,
        identify: function (datum) { return datum.id; },
        remote: {
            url: '../../../api/Platoon/Search?districtId=%DISTRICTID&search=%QUERY',
            prepare: function (query, settings) {
                settings.url = settings.url.replace("%DISTRICTID", $("#districtSelect").find("option:selected").val())
                    .replace("%QUERY", query);
                return settings;
            },
            rateLimitWait: 200
        }
    });

    $('#ContractorTypeahead').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
            name: 'contractors',
            display: 'name',
            source: contractors,
            templates: {
                notFound: [
                    '<div class="empty-message">',
                    '没有符合筛选条件的承包商<br />',
                    '直接填写名称，系统将自动保存为新承包商<br />',
                    '</div>'
                ].join('\n'),
                pending: [
                    '<div class="empty-message">',
                    '正在检索…',
                    '</div>'
                ].join('\n'),
                header: [
                    '<div class="empty-message">',
                    '选择工单承包商',
                    '</div>'
                ].join('\n')
            }
        });

    $('#ContractorStaffTypeahead').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
            name: 'contractorStaffs',
            display: 'name',
            source: contractorStaffs,
            templates: {
                notFound: [
                    '<div class="empty-message">',
                    '没有符合筛选条件的联系人<br />',
                    '直接填写姓名，系统将自动添加至该承包商<br />',
                    '</div>'
                ].join('\n'),
                pending: [
                    '<div class="empty-message">',
                    '正在检索…',
                    '</div>'
                ].join('\n'),
                header: [
                    '<div class="empty-message">',
                    '选择工单联系人',
                    '</div>'
                ].join('\n')
            }
        });

    $('#PlatoonTypeahead').typeahead({
        hint: false,
        highlight: true,
        minLength: 0
    }, {
            name: 'platoons',
            display: 'name',
            source: platoons,
            templates: {
                notFound: [
                    '<div class="empty-message">',
                    '所选地区没有符合筛选条件的排长<br />',
                    '</div>'
                ].join('\n'),
                pending: [
                    '<div class="empty-message">',
                    '正在检索…',
                    '</div>'
                ].join('\n'),
                header: [
                    '<div class="empty-message">',
                    '选择工单排长',
                    '</div>'
                ].join('\n')
            }
        });



    if ($("#ContractorName").val()) {
        console.log($("#ContractorName").val());
        $("#ContractorTypeahead").typeahead('val', $("#ContractorName").val());
    }
    if ($("#ContractorStaffName").val()) {
        $("#ContractorStaffTypeahead").typeahead('val', $("#ContractorStaffName").val());
    }
    

    $('#ContractorTypeahead').bind('typeahead:change', function (ev, query) {
        if ($("#ContractorName").val() != query) {
            contractors.search(query, function () { }, function (data) {
                for (i = 0; i < data.length; i++) {
                    if (data[i].name == query) {
                        onContractorSelected(data[i]);
                        return;
                    }
                }
                $("#ContractorId").val(null);
                $("#ContractorName").val(query);
                $("#ContractorStaffId").val(null);
                $("#ContractorStaffName").val(null);
                $("#ContractorStaffPhone").val(null);
                $('#ContractorStaffTypeahead').typeahead('val', '1');
                $('#ContractorStaffTypeahead').typeahead('val', '');
                $("#ContractorStaffTypeahead").focus();

            });
        }
    });

    $('#ContractorStaffTypeahead').bind('typeahead:change', function (ev, query) {
        if ($("#ContractorStaffName").val() != query) {
            contractorStaffs.search(query, function () { }, function (data) {
                for (i = 0; i < data.length; i++) {
                    if (data[i].name == query) {
                        onContractorStaffSelected(data[i]);
                        return;
                    }
                }
                $("#ContractorStaffId").val(null);
                $("#ContractorStaffName").val(query);
                $("#ContractorStaffPhone").val(null);
                $("#ContractorStaffPhone").focus();
            });
        }
    });

    $('#PlatoonTypeahead').bind('typeahead:change', function (ev, query) {
        if ($("#PlatoonName").val() != query) {
            platoons.search(query, function () { }, function (data) {
                for (i = 0; i < data.length; i++) {
                    if (data[i].name == query) {
                        onPlatoonSelected(data[i]);
                        return;
                    }
                }
                $("#PlatoonId").val(null);
                $("#PlatoonName").val(null);
                $("#PlatoonTypeahead").typeahead('val', '');
            });
        }
    });

    $('#ContractorTypeahead').bind('typeahead:autocomplete ', function (ev, suggestion) {
        onContractorSelected(suggestion);
    });
    $('#ContractorTypeahead').bind('typeahead:select', function (ev, suggestion) {
        onContractorSelected(suggestion);
    });
    $('#ContractorStaffTypeahead').bind('typeahead:autocomplete ', function (ev, suggestion) {
        onContractorStaffSelected(suggestion);
    });
    $('#ContractorStaffTypeahead').bind('typeahead:select', function (ev, suggestion) {
        onContractorStaffSelected(suggestion);
    });
    $('#PlatoonTypeahead').bind('typeahead:autocomplete ', function (ev, suggestion) {
        onPlatoonSelected(suggestion);
    });
    $('#PlatoonTypeahead').bind('typeahead:select ', function (ev, suggestion) {
        onPlatoonSelected(suggestion);
    });

});

function onContractorSelected(suggestion) {
    $("#ContractorId").val(suggestion.id);
    $("#ContractorName").val(suggestion.name);
    $("#ContractorStaffId").val(null);
    $("#ContractorStaffName").val(null);
    $("#ContractorStaffPhone").val(null);
    $("#ContractorStaffTypeahead").focus();
    $("#ContractorStaffTypeahead").typeahead('val', '1');
    $("#ContractorStaffTypeahead").typeahead('val', '');
}

function onContractorStaffSelected(suggestion) {
    $("#ContractorStaffId").val(suggestion.id);
    $("#ContractorStaffName").val(suggestion.name);
    $("#ContractorStaffPhone").val(suggestion.phone);
    $("#ContractorStaffPhone").focus();
}

function onPlatoonSelected(suggestion) {
    $("#PlatoonId").val(suggestion.id);
    $("#PlatoonName").val(suggestion.name);
}



