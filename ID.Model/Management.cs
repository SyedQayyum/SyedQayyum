using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.Model
{
    public class Management
    {
        public Int64 ManagementId { get; set; }
        public Int16 RupeeOrMinute { get; set; }
        public String Comment { get; set; }
        public Int16 ManagementCategoryId { get; set; }
    }
}
