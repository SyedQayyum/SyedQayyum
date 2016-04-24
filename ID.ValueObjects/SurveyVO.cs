﻿using System;
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
        public String ShortSurveyQuestion
        {
            get
            {
                if (SurveyQuestion.Length > 20)
                    return SurveyQuestion.Substring(1, 20) + "...";
                else
                    return SurveyQuestion;
            }
        }
        public String PicturePath { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public Byte Rating { get; set; }
    }
}