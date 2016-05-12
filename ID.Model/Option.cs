using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.Model
{
    public class Option 
    {
        public long OptionId { get; set; }
        public string OptionName { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }

    }
}
