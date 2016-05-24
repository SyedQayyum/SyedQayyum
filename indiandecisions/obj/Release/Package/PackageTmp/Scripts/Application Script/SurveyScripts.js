
$(document).ready(function () {

    $('.EditSurvey').click(function () {
        var SurveyId = $($(this).parents('tr').find('td')[0]).text();
        window.location.href = "../survey/create?Id=" + SurveyId;
    });

    $('.DeleteSurvey').click(function () {
        var SurveyId = $($(this).parents('tr').find('td')[0]).text();
        $('#SurveyIdDelete').val(SurveyId);
        $('#DeleteModal').modal('show');
    });


    $('.star').click(function () {

        var Rating = $(this).attr("id");
        var surveyId = $(this).closest('div .UserRatingInput').attr("id").split('_')[1];
        $.getJSON('../../../survey/RatingOnSurvey?SurveyId=' + surveyId + '&Rating=' + Rating, function (isRated) {
            if (isRated == true) {
                $("#UserRating_" + surveyId).empty();
                $("#UserRating_" + surveyId).append('<label style="color:orange;font-weight:bold">Your Rating : ' + Rating.toUpperCase() + ' star</label>');

            }
        })
    })

    $('.SurveyActive').change(function () {
        var SurveyId = $($(this).parents('tr').find('td')[0]).text();
        var activeStatus = $(this).prop('checked');
        $.getJSON("../../../Survey/SetSurveyActiveStatus?SurveyId=" + SurveyId + "&ActiveStatus=" + activeStatus, function (isDone) {

        });
    });


});


function viewSurveyDetials(surveyId, surveyQs) {
    var surveyQsString = surveyQs.split(' ').join('-');
    window.location.href = "../../../survey/" + surveyId + "/survey-details/" + surveyQsString;
    return false;
}


function voteOnSurvey(button, surveyId, surveyQs) {

    var OptionId = $(button).parent("div").find("input[type='radio']:checked").val();
    var OptionValue = $(button).parent("div").find("input[type='radio']:checked").attr("id");

    if (OptionId != undefined) {
        $.getJSON('../../../survey/VoteOnSurvey?SurveyId=' + surveyId + '&OptionId=' + OptionId + '&OptionValue=' + OptionValue, function (isvoted) {
            if (isvoted == true) {
                var surveyQsString = surveyQs.split(' ').join('-');
                window.location.href = "../../../survey/" + surveyId + "/survey-details/" + surveyQsString;
                return false;
            }
        })
    } else {
        var errorDiv = $(button).parent('div').find('#ErrorMessage');
        errorDiv.empty();
        errorDiv.append('<label style="color:red">Please select your vote !</label>');
    }
}






