using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.Model
{
    public class Result : Base
    {
        public Int64 ResultId { get; set; }
        public Int64 SurveyId { get; set; }
        public Int32 PositiveCount { get; set; }
        public Int32 NegativeCount { get; set; }
        public Int32 NeutralCount { get; set; }
        public DateTime PublishedDate { get; set; }

    }
}
