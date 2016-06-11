$(document).ready(function () {


    $('.EditCategory').click(function () {

        $('#AddCategory').val("Update Category");
        var categoryId = $($(this).parents('tr').find('td')[0]).text();
        $.getJSON("../Category/GetCategory?CategoryId=" + categoryId, function (category) {

            $('#CategoryName').val(category.CategoryName);
            $('#ParentCategory').val(category.ParentCategory == 0 ? "" : category.ParentCategory);
            $('#CategoryId').val(category.CategoryId);
            $('#CategoryOrder').val(category.CategoryOrder);

        });

    });


    $('.DeleteCategory').click(function () {

        var categoryId = $($(this).parents('tr').find('td')[0]).text();
        $('#CategoryIdDelete').val(categoryId);
        $('#DeleteModal').modal('show');

    });

    $('.CategoryActive').change(function () {

        var categoryId = $($(this).parents('tr').find('td')[0]).text();
        var activeStatus = $(this).prop('checked');
        $.getJSON("../../../Category/SetCategoryActiveStatus?CategoryId=" + categoryId + "&ActiveStatus=" + activeStatus, function (isDone) {

        });
    });

});


