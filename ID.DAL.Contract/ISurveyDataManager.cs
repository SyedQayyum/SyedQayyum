using ID.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.DAL.Contract
{
   public interface ISurveyDataManager
    {
        List<Survey> GetAllSurvey(long? SurveyId, int? CategoryId, Boolean? IsDeleted, Boolean? IsActive);
        Boolean CreateUpdateSurvey(Survey Survey);
        bool DeleteSurvey(int SurveyId, bool IsSoftDelete);
        long GetLastGeneratedSurveyId();
        bool VoteOnSurvey(long surveyId, long OptionId);
        bool IsValidUser(string userName, string password);
        bool RatingOnSurvey(long surveyId, short rating);
    }
}
