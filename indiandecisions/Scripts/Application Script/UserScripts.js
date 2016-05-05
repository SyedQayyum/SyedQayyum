$(document).ready(function () {


    $('.EditUser').click(function () {

        $('#AddUser').val("Update User");
        var UserId = $($(this).parents('tr').find('td')[0]).text();
        $.getJSON("../User/GetUser?UserId=" + UserId, function (User) {

            $('#FirstName').val(User.FirstName);
            $('#LastName').val(User.LastName);
            $('#UserEmail').val(User.UserEmail);
            $('#UserPassword').val(User.UserPassword);
            $('#UserId').val(User.UserId);

        });

    });




    $('.DeleteUser').click(function () {

        var UserId = $($(this).parents('tr').find('td')[0]).text();
        $('#UserIdDelete').val(UserId);
        $('#DeleteModal').modal('show');
    });

});


