using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ID.Model
{
    public class Survey : Base
    {
        

        public Int64 SurveyId { get; set; }
        public Int16 CategoryId { get; set; }
        public String CategoryName { get; set; }
        public String SurveyQuestion { get; set; }
        public String SurveyDescription { get; set; }
        public String PicturePath { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public Decimal Rating { get; set; }
        public Int32 RatingCount { get; set; }

        public long PositiveCount { get; set; }
        public long NegativeCount { get; set; }
        public long NeutralCount { get; set; }
    }

    public class SurveyOption
    {
        public long Id { get; set; }
        public long SurveyId { get; set; }
        public long OptionId { get; set; }
        public string OptionName { get; set; }
        public long SurveyOptionCount { get; set; }
    }
}