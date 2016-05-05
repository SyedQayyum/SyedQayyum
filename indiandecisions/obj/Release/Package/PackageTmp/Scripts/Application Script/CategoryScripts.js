``
$(document).ready(function () {


    $.getJSON("../../../Category/GetAllCategories", function (categories) {
        for (var i = 0; i < categories.length; i++) {
            $("#Categories").append('<li><a href="/survey/' + categories[i].CategoryId + '/get-survey/' + categories[i].CategoryName + '">' + categories[i].CategoryName + '</a></li>');
        }
    });


    $('.EditCategory').click(function () {

        $('#AddCategory').val("Update Category");
        var categoryId = $($(this).parents('tr').find('td')[0]).text();
        $.getJSON("../Category/GetCategory?CategoryId=" + categoryId, function (category) {

            $('#CategoryName').val(category.CategoryName);
            $('#ParentCategory').val(category.ParentCategory == 0 ? "" : category.ParentCategory);
            $('#CategoryId').val(category.CategoryId);

        });

    });




    $('.DeleteCategory').click(function () {

        var categoryId = $($(this).parents('tr').find('td')[0]).text();
        $('#CategoryIdDelete').val(categoryId);
        $('#DeleteModal').modal('show');
    });

});


