using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.ValueObjects
{
    public class OptionVO
    {
        public long OptionId { get; set; }
        public long SurveyId { get; set; }
        public string OptionName { get; set; }
        public bool IsActive { get; set; }
        public String CreatedBy { get; set; }
    }

    public class OptionList
    {
        public List<OptionVO> Options { get; set; }
        public Pager Pager { get; set; }

    }
}
