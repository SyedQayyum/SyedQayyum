using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.ValueObjects
{
   public class BaseVO
    {
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsActive { get; set; }
    }
}
