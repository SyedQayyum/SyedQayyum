
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

    //var surveyVM = new SurveyVM();

    //surveyVM.GetSurveyDetails();
    //survey.GetSurvey(18);
    //ko.applyBindings(surveyVM, document.getElementById('surveyDetails'));
    //ko.applyBindings(surveyVM, document.getElementById('surveyInfo'))


});

function viewSurveyDetials(surveyId, surveyQs) {
    window.location.href = "../../../survey/" + surveyId + "/survey-details/" + surveyQs;
    return false;
}


function voteOnSurvey(button, surveyId, surveyQs) {

    var UserVote = $(button).parent("div").find("input[type='radio']:checked").val();

    if (UserVote != undefined) {
        $.getJSON('../../../survey/VoteOnSurvey?SurveyId=' + surveyId + '&UserVote=' + UserVote, function (isvoted) {
            if (isvoted == true) {
                window.location.href = "../../../survey/" + surveyId + "/survey-details/" + surveyQs;
            }
        })
    } else {
        
        var errorDiv = $(button).parent('div').nextAll('#ErrorMessage');
        errorDiv.empty();
        errorDiv.append('<label style="color:red">Please select your vote !</label>');
    }
}

//function SurveyVM() {

//    survey = this
//    survey.SurveyId = ko.observable();
//    survey.CategoryId = ko.observable();
//    survey.CategoryName = ko.observable();
//    survey.SurveyQuestion = ko.observable();
//    survey.PicturePath = ko.observable();
//    survey.StartDate = ko.observable();
//    survey.CloseDate = ko.observable();
//    survey.ExpireDate = ko.observable();
//    survey.Rating = ko.observable();
//    survey.PositiveCount = ko.observable();
//    survey.NegativeCount = ko.observable();
//    survey.NeutralCount = ko.observable();
//    survey.surveyArray = ko.observableArray();

//    $.ajaxSetup({ async: false });




//    survey.GetSurveyDetails = function () {
//        $.getJSON("../Survey/GetSurveyInformation", function (SurveyList) {
//            ko.utils.arrayMap(SurveyList, function (objSurvey) {
//                debugger;
//                objSurvey.Rating = parseInt(objSurvey.Rating) / 5 * 100 + '%';
//                survey.surveyArray.push(objSurvey);
//            })
//        })
//    }


//    survey.GetSurvey = function (surveyId) {
//        $.getJSON("../Survey/GetSurveyInformation?surveyId=" + surveyId, function (SurveyList) {
//            ko.utils.arrayMap(SurveyList, function (objSurvey) {

//                survey.SurveyQuestion(objSurvey.SurveyQuestion);
//                survey.PicturePath(objSurvey.PicturePath);
//                survey.PositiveCount(objSurvey.PositiveCount);
//                survey.NegativeCount(objSurvey.NegativeCount);
//                survey.NeutralCount(objSurvey.NeutralCount);
//            })
//        })
//    }
//}





