using ID.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.ValueObjects
{
    public class EmailVO
    {
        public String UserName { get; set; }
        public String UserEmail { get; set; }
        public String FromName { get; set; }
        public String FromEmail { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public String To
        {
            get
            {
                return UserName + " <" + UserEmail + ">";
            }
        }
        public ContactForEnum ContactFor { get; set; }
        public String From
        {
            get
            {
                return FromName + " <" + FromEmail + ">";
            }
        }
    }
}
