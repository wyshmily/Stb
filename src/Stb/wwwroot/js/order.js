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
            url: '../../../api/Platoon/Search?search=%QUERY',
            wildcard: '%QUERY',
            rateLimitWait: 200
        }
    });

    $('#ContractorTypeahead').typeahead({
        hint: true,
        highlight: true,
        minLength: 0
    }, {
        name: 'contractors',
        display: 'name',
        source: contractors
    });

    $('#ContractorStaffTypeahead').typeahead({
        hint: true,
        highlight: true,
        minLength: 0
    }, {
        name: 'contractorStaffs',
        display: 'name',
        source: contractorStaffs
    });

    $('#PlatoonTypeahead').typeahead({
        hint: true,
        highlight: true,
        minLength: 0
    }, {
        name: 'platoons',
        display: 'name',
        source: platoons
    });

    if ($("#ContractorName").val()) {
        $("#ContractorTypeahead").typeahead('val', $("#ContractorName").val());
    }
    if ($("#ContractorStaffName").val()) {
        $("#ContractorStaffTypeahead").typeahead('val', $("#ContractorStaffName").val());
    }
    if ($("#PlatoonName").val()) {
        $("#PlatoonTypeahead").typeahead('val', $("#PlatoonName").val());
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



