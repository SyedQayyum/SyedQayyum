using ID.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.DAL.Contract
{
    public interface ISurevyOptionsDataManager
    {
        List<Option> GetAllSurveyOption(long SurveyId);
        bool CreateUpdateSurveyOption(long Id, long OptionId, long SurveyId);
        bool DeleteOption(long? Id, long SurveyId);
    }
}
