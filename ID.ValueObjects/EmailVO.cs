using ID.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.ValueObjects
{
    public class EmailVO
    {
        [Required(ErrorMessage ="Please enter Name")]
        public String UserName { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        public String UserEmail { get; set; }
        public String FromName { get; set; }
        public String FromEmail { get; set; }
        public String Subject { get; set; }
        [Required(ErrorMessage = "Please enter Message")]
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

        public string Captcha { get; set; }
    }
}
