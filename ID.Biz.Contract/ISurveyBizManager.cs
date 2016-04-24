﻿using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.Biz.Contract
{
  public  interface ISurveyBizManager
    {

        List<SurveyVO> GetAllSurvey(long? SurveyId, int? CategoryId, Boolean? IsDeleted, Boolean? IsActive);
        Boolean CreateUpdateSurvey(SurveyVO Survey);
        bool DeleteSurvey(int SurveyId, bool IsSoftDelete);
        long GetLastGeneratedSurveyId();
    }
}
