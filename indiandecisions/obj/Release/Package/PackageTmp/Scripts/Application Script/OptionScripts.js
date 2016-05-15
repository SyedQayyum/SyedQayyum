
$(document).ready(function () {


    $.getJSON("../../../Option/GetAllOptions", function (options) {
        for (var i = 0; i < options.length; i++) {
            $("#Options").append('<li><a href="/survey/' + options[i].OptionId + '/get-survey/' + options[i].OptionName + '">' + options[i].OptionName + '</a></li>');
        }
    });


    $('.EditOption').click(function () {
        debugger;
        $('#AddOption').val("Update Option");
        var optionId = $($(this).parents('tr').find('td')[0]).text();
        $.getJSON("../Option/GetOption?OptionId=" + optionId, function (option) {
            debugger;
            $('#OptionName').val(option.OptionName);
            $('#OptionId').val(option.OptionId);

        });

    });


    $('.DeleteOption').click(function () {

        var optionId = $($(this).parents('tr').find('td')[0]).text();
        $('#OptionIdDelete').val(optionId);
        $('#DeleteModal').modal('show');

    });

    $('.OptionActive').change(function () {

        var OptionId = $($(this).parents('tr').find('td')[0]).text();
        var activeStatus = $(this).prop('checked');
        $.getJSON("../../../Option/SetOptionActiveStatus?OptionId=" + OptionId + "&ActiveStatus=" + activeStatus, function (isDone) {

        });
    });

});


