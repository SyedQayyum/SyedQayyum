
$(document).ready(function () {


    $('.EditCategory').click(function () {
        debugger;
        $('#AddCategory').val("Update Category");
        var categoryId = $($(this).parents('tr').find('td')[0]).text();
        $.getJSON("../Category/GetCategory?CategoryId=" + categoryId, function (category) {
            debugger;
            $('#CategoryName').val(category.CategoryName);
            $('#ParentCategory').val(category.ParentCategory==0?"":category.ParentCategory);
            $('#CategoryId').val(category.CategoryId);

        });

    });


    $('.DeleteCategory').click(function () {

        var categoryId = $($(this).parents('tr').find('td')[0]).text();
        $('#CategoryIdDelete').val(categoryId);
        $('#DeleteModal').modal('show');
    });

});


