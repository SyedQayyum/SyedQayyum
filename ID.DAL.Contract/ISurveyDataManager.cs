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
        List<Survey> GetAllSurvey(int? SurveyId);
        Boolean CreateUpdateSurvey(Survey Survey);
        bool DeleteSurvey(int SurveyId, bool IsSoftDelete);
        long GetLastGeneratedSurveyId();
    }
}
