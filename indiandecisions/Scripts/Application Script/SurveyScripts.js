
$(document).ready(function () {


    debugger;
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

});

function CheckCookieAndPerformAction(surveyId) {
    var vote = localStorage.getCacheItem("Voted_" + surveyId);
    if (vote != null && vote != undefined) {
        $("#VoteSection_" + surveyId).empty();
        $("#VoteSection_" + surveyId).append('<br/><label style="color:green;font-weight:bold">You have Voted : ' + vote.toUpperCase() + '</label>');
    }
}


function setCookie(surveyId,vote) {
    localStorage.setCacheItem("Voted_" + surveyId, vote, { days: 8 });
}


function viewSurveyDetials(surveyId, surveyQs) {
    var surveyQsString = surveyQs.split(' ').join('-');
    window.location.href = "../../../survey/" + surveyId + "/survey-details/" + surveyQsString;
    return false;
}


function voteOnSurvey(button, surveyId, surveyQs) {

    var UserVote = $(button).parent("div").find("input[type='radio']:checked").val();

    if (UserVote != undefined) {
        $.getJSON('../../../survey/VoteOnSurvey?SurveyId=' + surveyId + '&UserVote=' + UserVote, function (isvoted) {
            if (isvoted == true) {
                setCookie(surveyId, UserVote);
                var surveyQsString = surveyQs.split(' ').join('-');
                window.location.href = "../../../survey/" + surveyId + "/survey-details/" + surveyQsString;
            }
        })
    } else {

        var errorDiv = $(button).parent('div').nextAll('#ErrorMessage');
        errorDiv.empty();
        errorDiv.append('<label style="color:red">Please select your vote !</label>');
    }
}





