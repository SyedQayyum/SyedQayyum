
$(document).ready(function () {


    $('.EditManagement').click(function () {
        $('#AddManagement').val("Update Management");
        var managementId = $($(this).parents('tr').find('td')[0]).text();

        $.getJSON("../Management/GetManagement?ManagementId=" + managementId, function (management) {
            $('#ManagementCategoryId').val(management.ManagementCategoryId);
            $('#ManagementId').val(management.ManagementId);
            $('#RupeeOrMinute').val(management.RupeeOrMinute);
            $('#Comment').val(management.Comment);

        });

    });


    $('.DeleteManagement').click(function () {
        debugger;
        var managementId = $($(this).parents('tr').find('td')[0]).text();
        $('#ManagementIdDelete').val(managementId);
        $('#DeleteModal').modal('show');

    });

    $('.OptionActive').change(function () {

        var OptionId = $($(this).parents('tr').find('td')[0]).text();
        var activeStatus = $(this).prop('checked');
        $.getJSON("../../../Option/SetOptionActiveStatus?OptionId=" + OptionId + "&ActiveStatus=" + activeStatus, function (isDone) {

        });
    });

});


