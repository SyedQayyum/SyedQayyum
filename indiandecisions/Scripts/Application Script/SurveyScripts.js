
$(document).ready(function () {


    $('.VotesResult').each(function (voteResult) {

        CheckCookieAndPerformAction($(this).val())

    })


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
                setCookieForRating(surveyId, Rating);
                $("#UserRating_" + surveyId).empty();
                $("#UserRating_" + surveyId).append('<br/><label style="color:orange;font-weight:bold">Your Rating : ' + Rating.toUpperCase() + ' star</label>');

            }
        })
    })


});

function CheckCookieAndPerformAction(surveyId) {
    var vote = localStorage.getCacheItem("Voted_" + surveyId);
    var rating = localStorage.getCacheItem("Rated_" + surveyId);
    if (vote != null && vote != undefined) {
        $("#VoteSection1_" + surveyId).empty();
        $("#UserVote_" + surveyId).append('<br/><label style="color:#1AB91A;font-weight:bold">Your Vote : ' + vote.toUpperCase() + '</label>');
    }
    else {
        if ($('#VoteSection1_' + surveyId).length != 0)
            $("#VoteSection2_" + surveyId).empty();
        else {
            $("#VoteSection2_" + surveyId).append('<br/><label style="color:red;font-weight:bold">Sorry, You can not vote. This survey is expired</label>');
            $("#VoteSection2_" + surveyId).append('<br/><label style="color:red;font-weight:bold">You can have look on another survey !</label>');
        }
    }

    if (rating != null && rating != undefined) {
        if ($('#VoteSection1_' + surveyId).length != 0) {
            $("#UserRating_" + surveyId).empty();
            $("#UserRating_" + surveyId).append('<br/><label style="color:orange;font-weight:bold">Your Rating : ' + rating.toUpperCase() + ' star</label>');
        }
        else {
            $("#UserVote_" + surveyId).append('<br/><br/><label style="color:orange;font-weight:bold">Your Rating : ' + rating.toUpperCase() + ' star</label>');
        }
    }
}


function setCookie(surveyId, vote) {
    localStorage.setCacheItem("Voted_" + surveyId, vote, { days: 8 });
}

function setCookieForRating(surveyId, Rating) {
    localStorage.setCacheItem("Rated_" + surveyId, Rating, { days: 8 });
}


function viewSurveyDetials(surveyId, surveyQs) {
    var surveyQsString = surveyQs.split(' ').join('-');
    window.location.href = "../../../survey/" + surveyId + "/survey-details/" + surveyQsString;
    return false;
}


function voteOnSurvey(button, surveyId, surveyQs) {

    var OptionId = $(button).parent("div").find("input[type='radio']:checked").val();
    var OptionName = $(button).parent("div").find("input[type='radio']:checked").attr("id");

    if (OptionId != undefined) {
        $.getJSON('../../../survey/VoteOnSurvey?SurveyId=' + surveyId + '&OptionId=' + OptionId, function (isvoted) {
            if (isvoted == true) {
                setCookie(surveyId, OptionName);
                var surveyQsString = surveyQs.split(' ').join('-');
                window.location.href = "../../../survey/" + surveyId + "/survey-details/" + surveyQsString;
            }
        })
    } else {
        var errorDiv = $(button).parent('div').find('#ErrorMessage');
        errorDiv.empty();
        errorDiv.append('<label style="color:red">Please select your vote !</label>');
    }
}






