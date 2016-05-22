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
        protected readonly ISurveyOptionsDataManager _surveyOptionDataManager;

        public SurveyBizManager(ISurveyDataManager surveyDataManager, ISurveyOptionsDataManager surveyOptionDataManager)
        {
            _surveyDataManager = surveyDataManager;
            _surveyOptionDataManager = surveyOptionDataManager;
            AppHelper.CreateMap<SurveyVO, Survey>();
            AppHelper.CreateMap<SurveyOptionVO, SurveyOption>();
        }

        public Boolean CreateUpdateSurvey(SurveyVO Survey)
        {

           Boolean IsDone =  _surveyDataManager.CreateUpdateSurvey(Mapper.Map<SurveyVO, Survey>(Survey));

            long SurveyId = Survey.SurveyId != 0 ? Survey.SurveyId  : GetLastGeneratedSurveyId();

            foreach (SurveyOptionVO SurveyOption in Survey.SurveyOptions)
            {
                if (SurveyOption.OptionId != 0)
                    _surveyOptionDataManager.CreateUpdateSurveyOption(SurveyOption.Id, SurveyOption.OptionId, SurveyId);
            }
            return IsDone;
        }

        public bool DeleteSurvey(int SurveyId, bool IsSoftDelete)
        {
            return _surveyDataManager.DeleteSurvey(SurveyId, IsSoftDelete);
        }

        public List<SurveyVO> GetAllSurvey(long? SurveyId, int? CategoryId, Boolean? IsDeleted, Boolean? IsActive)
        {
            List<SurveyVO> objServeyList = Mapper.Map<List<Survey>, List<SurveyVO>>(_surveyDataManager.GetAllSurvey(SurveyId, CategoryId, IsDeleted, IsActive));
            foreach (SurveyVO ObjSurvey in objServeyList)
            {
                ObjSurvey.SurveyOptions= Mapper.Map<List<SurveyOption>, List<SurveyOptionVO>>(_surveyOptionDataManager.GetAllSurveyOption(ObjSurvey.SurveyId));
            }
            return objServeyList;
        }

        public long GetLastGeneratedSurveyId()
        {
            return _surveyDataManager.GetLastGeneratedSurveyId();
        }

        public bool IsValidUser(string userName, string password)
        {
            return _surveyDataManager.IsValidUser(userName, password);
        }

        public bool RatingOnSurvey(long surveyId, short Rating)
        {
            return _surveyDataManager.RatingOnSurvey(surveyId, Rating);
        }

        public bool SetSurveyActiveStatus(long SurveyId, bool ActiveAtatus)
        {
            return _surveyDataManager.SetSurveyActiveStatus(SurveyId, ActiveAtatus);
        }

        public bool VoteOnSurvey(long surveyId, long OptionId)
        {
            return _surveyDataManager.VoteOnSurvey(surveyId, OptionId);
        }
    }
}
