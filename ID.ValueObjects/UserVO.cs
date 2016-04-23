using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.ValueObjects
{
    public class UserVO : BaseVO
    {
        public Int16 UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String UserEmail { get; set; }
        public String UserPassword { get; set; }


    }
}
