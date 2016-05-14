using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.Biz.Contract
{
  public  interface ISurveyBizManager
    {

        List<SurveyVO> GetAllSurvey(long? SurveyId=null, int? CategoryId=null, Boolean? IsDeleted=null, Boolean? IsActive=null);
        Boolean CreateUpdateSurvey(SurveyVO Survey);
        bool DeleteSurvey(int SurveyId, bool IsSoftDelete);
        long GetLastGeneratedSurveyId();
        Boolean VoteOnSurvey(long surveyId, long SurveyOptionId);
        bool IsValidUser(string userName, string password);
    }
}
