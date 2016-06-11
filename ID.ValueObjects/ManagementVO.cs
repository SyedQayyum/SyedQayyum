using ID.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.ValueObjects
{
    public class ManagementVO
    {
        public Int64 ManagementId { get; set; }
        public Int16 RupeeOrMinute { get; set; }
        public String Comment { get; set; }
        public Int16 ManagementCategoryId { get; set; }
        public String MCategory { get; set; }
        public ManagementCategoryEnum ManagementCategory { get; set; }
    }

    public class ManagementList
    {
        public List<ManagementVO> Management { get; set; }
        public Pager Pager { get; set; }

    }
}
