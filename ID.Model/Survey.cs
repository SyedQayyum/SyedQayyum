﻿using System;
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
        public String PicturePath { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public Byte Rating { get; set; }
    }
}