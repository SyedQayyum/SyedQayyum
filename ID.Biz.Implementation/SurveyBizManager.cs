using ID.Biz.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ID.ValueObjects;
using ID.DAL.Contract;
using ID.Model;
using Core.Common;
using AutoMapper;

namespace ID.Biz.Implementation
{
    public class SurveyBizManager : ISurveyBizManager
    {

        protected readonly ISurveyDataManager _surveyDataManager;

        public SurveyBizManager(ISurveyDataManager surveyDataManager)
        {
            _surveyDataManager = surveyDataManager;
            AppHelper.CreateMap<SurveyVO, Survey>();
        }

        public Boolean CreateUpdateSurvey(SurveyVO Survey)
        {
            return _surveyDataManager.CreateUpdateSurvey(Mapper.Map<SurveyVO, Survey>(Survey));
        }

        public bool DeleteSurvey(int SurveyId, bool IsSoftDelete)
        {
            return _surveyDataManager.DeleteSurvey(SurveyId, IsSoftDelete);
        }

        public List<SurveyVO> GetAllSurvey(long? SurveyId, int? CategoryId, Boolean? IsDeleted, Boolean? IsActive)
        {
            List<SurveyVO> objServeyList = Mapper.Map<List<Survey>, List<SurveyVO>>(_surveyDataManager.GetAllSurvey(SurveyId, CategoryId, IsDeleted, IsActive));
            foreach (SurveyVO Category in objServeyList)
            {

            }
            return objServeyList;
        }

        public long GetLastGeneratedSurveyId()
        {
            return _surveyDataManager.GetLastGeneratedSurveyId();
        }
    }
}
