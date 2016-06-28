using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ID.ValueObjects
{
    public class SurveyVO : BaseVO
    {
        public Int64 SurveyId { get; set; }
        public Int16 CategoryId { get; set; }
        public String CategoryName { get; set; }
        public String SurveyQuestion { get; set; }  
        public String SurveyDescription { get; set; }
        public String ShortSurveyQuestion
        {
            get
            {
                if (SurveyQuestion!=null && SurveyQuestion.Length > 20)
                    return SurveyQuestion.Substring(1, 20) + "...";
                else
                    return SurveyQuestion;
            }
        }
        public String PicturePath { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public Decimal Rating { get; set; }
        public Int32 RatingCount { get; set; }
        public bool IsVoted { get; set; }
        public String VoteValue{ get; set; }
        public bool IsRated { get; set; }
        public String RateValue { get; set; }
        public List<SurveyOptionVO> SurveyOptions { get; set; }
    

    }


    public class SurveyList
    {
        public List<SurveyVO> ListSurvey { get; set; }
        public Pager Pager { get; set; }
    }

    public class SurveyOptionVO
    {
        public long Id { get; set; }
        public long SurveyId{ get; set; }
        public long OptionId { get; set; }
        public string OptionName { get; set; }
        public long SurveyOptionCount { get; set; }
    }

    public class SurveyResult
    {
        public string name { get; set; }
        public double y { get; set; }
    }
}